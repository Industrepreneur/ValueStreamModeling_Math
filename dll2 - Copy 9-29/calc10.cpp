
#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"



 void write_results (class_model * c1);

 void mpcalc   (class_model * c1);
    int over_u (class_model * c1);

  


float  get_totaltime (class_model * c1, class_part *tpart, class_oper * toper);
float  get_time_p_p (class_model * c1, class_part *tpart, class_oper * toper);
float  get_tb_time (class_model * c1, class_part *tpart, class_oper * toper);
float  get_dtdl (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdX (class_model * c1, class_part * rpart, class_oper * toper);
float  get_dtdb (class_model * c1, class_part * rpart, class_oper * toper);
float  DTDB (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT2(class_model * c1, class_part * rpart, class_oper * toper);



float *vector(long nl, long nh);
void free_vector(float *v, long nl, long nh); 

void frprmn(class_model * c1, float p[], int n,  float *fret);




void   calc_op ( class_model * c1, class_part *tpart, class_oper *  toper, float * v1, float * xbarsl, float *xbarrl, int abc);
float Optimization2(class_model * c1, 
						int             numberOfParts,          // the number of products available
                        int*            partID,                         // pointer to the head of the array of product ID numbers
                        float*          value,                          // the array of values
                        int*            optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                        int*            optimizeTbatch         // boolean variable which decides if transfer batch is to be optimized
                        );


void setparmstodo2(class_model * c1,
						int todo,  int             numberOfParts, 
                        int*            partID,        
                        float*          value,         
                        int*            optimizeLotSize,
                        int*            optimizeTbatch); 

//


float Optimization2(class_model * c1, 
						int             numberOfParts,          // the number of products available
                        int*            partID,                         // pointer to the head of the array of product ID numbers
                        float*          value,                          // the array of values
                        int*            optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                        int*            optimizeTbatch         // boolean variable which decides if transfer batch is to be optimized
                        )            
{
        class_part* tpart;
         
        int     i,j;
        float    v1, sav_limit, max_val;
		float finalvalue; 
        int     over_limit;
        char    partname[200];  
		

		
		c1->inOPT= TRUE;

        char xx1 [MESSAGE_SIZE];
		char buf1[MESSAGE_SIZE];
		char s0[MESSAGE_SIZE];


         strcpy (xx1, c1->varlocal);
		 strcat(xx1, "\\");
		 strcat(xx1, "LotAndTbatch.res");

  		ofstream outfile (xx1);
		if (outfile.is_open()== false) {
                //MessageBox( NULL, "Cannot open file for results data", "In MPX optimization", MB_OK | MB_TASKMODAL );
                return(0);
		}


        sprintf(buf1, "No. of part %d",  numberOfParts);
        //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );


        // set numberOfParameterAffected


        c1->numberOfParameterAffected = 0;
        for (i=0; i<numberOfParts; i++)
        {
                if(optimizeLotSize[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }
                if(optimizeTbatch[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }

        }

        sprintf(buf1, "No. of para %d",  c1->numberOfParameterAffected);
        write_check(c1->varlocal, buf1);
		//MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

        if (c1->numberOfParameterAffected <=0)
        {
                //MessageBox( NULL, "No products to optimize", "In MPX optimization", MB_OK | MB_TASKMODAL );
                sprintf(s0, "0");
				outfile << s0;
                outfile.close();
                return(0);
        }


        // copy value to weight
        for (i=0; i<numberOfParts; i++){
            sprintf(partname, "%i", partID[i]);
            tpart = c1->get_part_num(c1->find_part_name(partname));
			if (tpart != NULL)
                tpart->weight = value[i];
        }

		write_check(c1->varlocal, "getting mem in opt2");

        c1->valueOfParameterAffected = vector (1, c1->numberOfParameterAffected+5);  //  gwwd local 

            // create a list of pointer to parts and the type of parameter which will be optimized
            c1->partPointer= (class_part**)malloc(sizeof(class_part)*(c1->numberOfParameterAffected+2));   //gwwd  FREE  9-29
            c1->isLotOrTbatch=(int*)malloc(sizeof(int)*(c1->numberOfParameterAffected+2));			       //gwwd  FREE  9-29
            j=0;

			write_check(c1->varlocal, "got mem in opt2");
            for (i=0; i<numberOfParts; i++)
            {

                    sprintf(partname, "%d", partID[i]);
                    //xspace(partname);

					tpart = c1->get_part_num(c1->find_part_name(partname));
                    
                    if (tpart != NULL) sprintf(buf1, "name %s ", tpart->name);
                    else               sprintf(buf1, "NO TPART  %s ", partname);
				

                    tpart->check_rout = TRUE;

                    if ((optimizeLotSize[i]!=0) && ( (tpart->tbatch == tpart->lotsiz) || (tpart->tbatch == -1) ) 
						&& (optimizeTbatch[i]==0))
                    {//   flag = 2  lot changes and tbatch = -1
                     //        => tbatch is changing with lotsize

                            tpart->tbatch = -1;
	

                            c1->partPointer[j]=tpart; // find_part(partname);
                            c1->isLotOrTbatch[j]=2;

                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->lotsiz;

                            j++;
                    }
                    else if(optimizeLotSize[i]!=0)
                    {
                            c1->partPointer[j]=tpart;
                            c1->isLotOrTbatch[j]=0;
							
							if (tpart->tbatch == -1) {
								if (tpart->lotsiz < 2.0) {
									tpart->tbatch = 2;
								    tpart->lotsiz = 2;
								}
								else 
									tpart->tbatch = tpart->lotsiz;
							}

                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->lotsiz;

                            j++;
                    }


                    if(optimizeTbatch[i]!=0)
                    {
                            c1->partPointer[j]=tpart;
                            c1->isLotOrTbatch[j]=1;
							
							if (tpart->tbatch == -1) tpart->tbatch = tpart->lotsiz;



							if ((tpart->lotsiz - tpart->tbatch)  < .1)  tpart->tbatch *= .99;
                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->tbatch;

                            j++;
                    }
					
            }

			// saving initial TBATCH SIZES ...
			   for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
				tpart->init_tb  = tpart->tbatch;
			}

			sav_limit = c1->utlimit;
            c1->tolerance =	c1->numberOfParameterAffected * 0.000001;
            c1->utlimit = 99.999;



			/// do lots only
			setparmstodo2(c1, 0,       numberOfParts,          // the number of products available
                                  partID,                         // pointer to the head of the array of product ID numbers
                                  value,                          // the array of values
                                  optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                                  optimizeTbatch         // boolean variable which decides if transfer batch is to be optimized
                                  );

			
	
	 frprmn  (c1,  c1->valueOfParameterAffected, c1->numberOfParameterAffected, &c1->resultValue);  
       

			// do tbatch only
			setparmstodo2(c1, 1,       numberOfParts,          // the number of products available
                                  partID,                         // pointer to the head of the array of product ID numbers
                                  value,                          // the array of values
                                  optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                                  optimizeTbatch         // boolean variable which decides if transfer batch is to be optimized
                                  )  ;
				
			//frprmn  (ci, c1->valueOfParameterAffected, c1->numberOfParameterAffected, c1->tolerance, &c1->iter, &c1->resultValue, fn1, fn2 ); 
    	  frprmn  (c1,  c1->valueOfParameterAffected, c1->numberOfParameterAffected, &c1->resultValue);  

			//do all
			setparmstodo2(c1, 2,       numberOfParts,          // the number of products available
                                  partID,                         // pointer to the head of the array of product ID numbers
                                  value,                          // the array of values
                                  optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                                  optimizeTbatch         // boolean variable which decides if transfer batch is to be optimized
                                  )  ;
	       	
					//frprmn  (ci, c1->valueOfParameterAffected, c1->numberOfParameterAffected, c1->tolerance, &c1->iter, &c1->resultValue, fn1, fn2 ); 
    			  frprmn  (c1,  c1->valueOfParameterAffected, c1->numberOfParameterAffected, &c1->resultValue);  

                   v1 = 0;
				    for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                   
                         v1 += tpart->weight * tpart->qpart;
                    }
                    sprintf(buf1, " value %f", v1);
                    //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

                    //MessageBox( NULL, "returning value", "In MPX optimization", MB_OK | MB_TASKMODAL );
                     sprintf(s0, "%f", v1);
					 outfile << s0;
                     outfile.close();

					finalvalue = v1;

					over_limit = FALSE;
					max_val = 0.0;
					//MessageBox( NULL, "Cannot make opt u limit??",  "LLL", MB_OK | MB_TASKMODAL );
					over_limit = over_u (c1);

					
					strcpy (xx1, c1->varlocal);
					strcat(xx1, "\\");
					strcat(xx1, "str1.2");

  					ofstream outfile2 (xx1);
					if (outfile2.is_open()== false) 
					{
						// MessageBox( NULL, "Cannot open file for results data", "In MPX optimization", MB_OK | MB_TASKMODAL );
						return(0);
					}

					else {if (over_limit == TRUE) {
						      sprintf(s0, "%.2f", max_val);
								outfile2 << s0;
							}
						  else {
							   sprintf(s0, "0.0");
							   outfile2 << s0;
						  }

						 outfile2.close();
						
					}

            // output results to "LotAndTbatch.res"
					strcpy (xx1, c1->varlocal);
			    		strcat(xx1, "\\");
		        			strcat(xx1, "LotAndTbatch.res");

  					ofstream outfile3 (xx1);
					if (outfile.is_open()== false) 
					{
                           // Message("Error opening file for results data.");
                    return(0);
					}

            sprintf(s0, " PRODID, LOTSIZE, TBATCHSIZE\n");
			outfile << s0;

			  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
							tpart->bc_total = -2;
							tpart->bc_shipped = -2;
			}
   


            for (i = 1;i<= c1->numberOfParameterAffected; i++)
            {
               j = i - 1;
                if ((c1->isLotOrTbatch[j] == 0) || (c1->isLotOrTbatch[j] == 2)){
                    c1->partPointer[j]->bc_total = c1->partPointer[j]->lotsiz ;
                }
                else {
                    c1->partPointer[j]->bc_shipped = c1->partPointer[j]->tbatch;
                }
            }

            for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
				sprintf(s0, " %s, %.1f, %.1f \n", tpart->name, tpart->bc_total, tpart->bc_shipped);
			    outfile << s0;
			}
            outfile.close();


               write_results (c1);

            // free memory
			write_check(c1->varlocal, "free mem in opt2");
            free_vector (c1->valueOfParameterAffected, 1, c1->numberOfParameterAffected+5);  // ??  here gwwd 9-27
            free (c1->partPointer);
            free (c1->isLotOrTbatch);
			write_check(c1->varlocal, "freed mem in opt2");

            return(finalvalue);

}

//
//

void setparmstodo2(class_model * c1,
						int todo,  int             numberOfParts, 
                        int*            partID,        
                        float*          value,         
                        int*            optimizeLotSize,
                        int*            optimizeTbatch) 
{
  int i;
  int j;
  char partname [100];

  class_part * tpart;
  char s0[MESSAGE_SIZE];


//determine number we will change
  c1->numberOfParameterAffected = 0;

  if (todo ==0) {
	for (i=0; i<numberOfParts; i++)
        {
                if(optimizeLotSize[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }
        }
	}
  else if (todo == 1) {
	for (i=0; i<numberOfParts; i++)
        {               
                if(optimizeTbatch[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }
        }
	}
  else {
	for (i=0; i<numberOfParts; i++)
        {
                if(optimizeLotSize[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }
                if(optimizeTbatch[i]!=0)
                {
                        c1->numberOfParameterAffected++;
                }
        }
	}


	///have set numberOfParameterAffected

  write_check(c1->varlocal, "not getting mem in setprms...2");

// create a list of pointer to parts and the type of parameter which will be optimized
   //c1->partPointer= (class_part**)malloc(sizeof(class_part)*(c1->numberOfParameterAffected+2));    //gwwd  check free ? 
   //c1->isLotOrTbatch=(int*)malloc(sizeof(int)*(c1->numberOfParameterAffected+2));

   j=0;
   for (i=0; i<numberOfParts; i++)
   {
		 tpart = c1->get_part_num(c1->find_part_name(partname));

        if (tpart != NULL) sprintf(s0, "name %s ", tpart->name);
        else               sprintf(s0, "NO TPART  %s ", partname);
				
        tpart->check_rout = TRUE;

        if (((todo == 0) || (todo == 2)) && ((optimizeLotSize[i]!=0) && ( (tpart->tbatch == tpart->lotsiz) || (tpart->tbatch == -1) ) 
			  && (optimizeTbatch[i]==0)))
        {//   flag = 2  lot changes and tbatch = -1
         //        => tbatch is changing with lotsize

              tpart->tbatch = -1;
	
              c1->partPointer[j]=tpart; // find_part(partname);
              c1->isLotOrTbatch[j]=2;

              //  warning !!!!  index starts at 1
              c1->valueOfParameterAffected[j+1]=tpart->lotsiz;

              j++;
        }
        else if (((todo == 0) || (todo == 2)) && (optimizeLotSize[i]!=0))
        {
              c1->partPointer[j]=tpart;
              c1->isLotOrTbatch[j]=0;
							
		      if (tpart->tbatch == -1) {
					if (tpart->lotsiz < 2.0) {
						tpart->tbatch = 2;
					     tpart->lotsiz = 2;
					}
					else 
					tpart->tbatch = tpart->lotsiz;
			  }

              //  warning !!!!  index starts at 1
              c1->valueOfParameterAffected[j+1]=tpart->lotsiz;

              j++;
        }


                    if (((todo == 1) || (todo == 2)) && (optimizeTbatch[i]!=0))
                    {
                            c1->partPointer[j]=tpart;
                            c1->isLotOrTbatch[j]=1;
							
							if (tpart->tbatch == -1) tpart->tbatch = tpart->lotsiz;



							if ((tpart->lotsiz - tpart->tbatch)  < .1)  tpart->tbatch *= .99;
                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->tbatch;

                            j++;
                    }
					
            }
}
//
//
//-----------------------------------------------------------------
//
//