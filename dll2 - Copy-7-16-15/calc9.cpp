
#include <iostream>
#include <fstream>

#include "class_model.cpp"



	
void frprmn      (class_model * c1, float p[], int n,  float *fret); //, float (*func)(int * ci, float *lot1), void (*dfunc) (int * ci, float * vector2, float * diff)); 





float *vector(long nl, long nh);
void free_vector(float *v, long nl, long nh); 


 


 void write_results (class_model * c1);

 void mpcalc   (class_model * c1);

  

	

  
float  get_totaltime (class_model * c1, class_part *tpart, class_oper * toper);
float  get_time_p_p (class_model * c1, class_part *tpart, class_oper * toper);
float  get_tb_time (class_model * c1, class_part *tpart, class_oper * toper);
float  get_dtdl (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdX (class_model * c1, class_part * rpart, class_oper * toper);
float  get_dtdb (class_model * c1, class_part * rpart, class_oper * toper);
float  DTDB (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT2(class_model * c1, class_part * rpart, class_oper * toper);

void   calc_op ( class_model * c1, class_part *tpart, class_oper *  toper, float * v1, float * xbarsl, float *xbarrl, int abc);


int Optimization(class_model * c1,
										int                     numberOfParts,          // the number of products available
                                        int*            partID,                         // pointer to the head of the array of product ID numbers
                                        float*          value,                          // the array of values
                                        int*            optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                                        int*            optimizeTbatch,         // boolean variable which decides if transfer batch is to be optimized
                                        float*          lotsizeValue,           // original value of lotsize for each part
                                        float*          tbatchValue)            // original valye of tbatch for each part
{
        class_part* tpart;
         
         

        int     i,j,ok;
        float   v1;

		float   ret;
        char    partname[200];
		char buf1[MESSAGE_SIZE];

        char xx1[MESSAGE_SIZE];
		char s0[MESSAGE_SIZE];

		
     
	 strcpy (xx1, c1->varlocal);
     strcat(xx1, "\\");
     strcat(xx1, "LotAndTbatch.res");

  	ofstream outfile (xx1);
	if (outfile.is_open()== false) {
                //MessageBox( NULL, "Cannot open file for results data", "In MPX optimization", MB_OK | MB_TASKMODAL );
                return(0);
    }

	 sprintf(s0, "about to start writing to tlot.res");
	 outfile << s0;

	 outfile.close();  //  gwwd error 9-26

        sprintf(buf1, "No. of part %d",  numberOfParts);
        write_check(c1->varlocal, buf1); ////MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );


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
         write_check(c1->varlocal, buf1); //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

        if (c1->numberOfParameterAffected <=0)
        {
                sprintf(buf1, " %s %s ", "No products to optimize", "In Value Stream Modeling optimization");
			    write_check(c1->varlocal, buf1);
                
				sprintf(s0 , " 0 \n");
	        	outfile << s0;

        }


        // copy value to weight
        for (i=0; i<numberOfParts; i++){
            sprintf(partname, "%i", partID[i]);
            tpart = c1->get_part_num(c1->find_part_name(partname));
			if (tpart != NULL)
                tpart->weight = value[i];
        }

		write_check(c1->varlocal, " setting  changes ");

        c1->valueOfParameterAffected = vector (1, c1->numberOfParameterAffected+5);

            // create a list of pointer to parts and the type of parameter which will be optimized
            c1->partPointer= (class_part**)malloc(sizeof(class_part *)*(c1->numberOfParameterAffected+2));  //gwwd DID FREE  9-29
            c1->isLotOrTbatch=(int*)malloc(sizeof(int)*(c1->numberOfParameterAffected+2));			        //gwwd  DID FREE  9-29
            j=0;
            for (i=0; i<numberOfParts; i++)
            {

                    sprintf(partname, "%i", partID[i]);
                     write_check(c1->varlocal, partname);


                    tpart = c1->get_part_num(c1->find_part_name(partname));

                    if (tpart != NULL) sprintf(buf1, "name %s ", tpart->name);
                    else               sprintf(buf1, "NO TPART  %s ", partname);

					 write_check(c1->varlocal, buf1);
					

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
									tpart->tbatch = 1;
								    tpart->lotsiz = 2;
								}
								else 
									tpart->tbatch = 1;
							}

                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->lotsiz;

                            j++;
                    }


                    if(optimizeTbatch[i]!=0)
                    {
                            c1->partPointer[j]=tpart;
                            c1->isLotOrTbatch[j]=1;
							
							if (tpart->tbatch == -1) tpart->tbatch = 1;
 
                            //  warning !!!!  index starts at 1
                            c1->valueOfParameterAffected[j+1]=tpart->tbatch;

                            j++;
                    }
					
            }

			// saving initial TBATCH SIZES ...
			  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
				tpart->init_tb  = tpart->tbatch;
			}

			  write_check(c1->varlocal, " saving initial numbers ");

            c1->tolerance = c1->numberOfParameterAffected * 0.00001;
            c1->utlimit = 95.00;


		write_check(c1->varlocal, " starting opt   call to frprmn ");
			
	
	
	 frprmn  (c1,  c1->valueOfParameterAffected, c1->numberOfParameterAffected, &c1->resultValue);   //  vetor of parameters, number of values, return value 
	 //  void frprmn(class_model * c1, float p[], int n,  float *fret);
	 //  was   frprmn(valueOfParameterAffected, numberOfParameterAffected, tolerance, &iter, &resultValue, fvalue, dif1);

        

                    v1 = 0;
                    for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                                             v1 += tpart->weight * tpart->qpart;
                    }
                    sprintf(s0, " value %f", v1);
					 write_check(c1->varlocal, s0);
                    //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

                    //MessageBox( NULL, "returning value", "In MPX optimization", MB_OK | MB_TASKMODAL );
                    sprintf(s0, "%f", v1);
                    //outfile << s0;
					 //outfile.close();  //fclose(filePointer);

     // output results to "LotAndTbatch.res"
     strcpy (xx1, c1->varlocal);
     strcat(xx1, "\\");
     strcat(xx1, "LotAndTbatch.res");

	 write_check(c1->varlocal, "start ing to write lot.res");

  	 ofstream outfile2 (xx1);
            if (outfile2.is_open() == false){
					write_check(c1->varlocal, "can't write to lot.res");
                    return(0);
            }

			if (outfile2.is_open() == true){
            sprintf(s0, " PRODID, LOTSIZE, TBATCHSIZE\n");
			outfile2 << s0;
			 write_check(c1->varlocal, s0);

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
				outfile2 << s0;
				 write_check(c1->varlocal, s0);
			 }

            outfile2.close();
			}

             write_results (c1);

            // free memory

            free_vector (c1->valueOfParameterAffected, 1, c1->numberOfParameterAffected+5);  //??  gwwd ??  9-27
            free (c1->partPointer);
            free (c1->isLotOrTbatch);

			write_check(c1->varlocal," after free and free vector");

            return(1);

}




float  get_totaltime (class_model * c1, class_part *tpart, class_oper * toper)
{       float ret;
        float s1;
        float r1;
        float s2;
        float r2;
        float v1;



    calc_op (c1, tpart, toper, &v1, &s1, &r1, LABOR_T);
    calc_op (c1, tpart, toper, &v1, &s2, &r2, EQUIP_T);
    ret = calc_xprime(c1, tpart, toper, s1+r1*toper->lsize, s2+r2*toper->lsize);


return(ret);
}

float  get_time_p_p (class_model * c1, class_part *tpart, class_oper * toper)
{       float ret;
        float s1;
        float r1;
        float s2;
        float r2;
        float v1;
		char buf1 [MESSAGE_SIZE];

		//sprintf(buf1, " in get time pp %s %s ",  tpart->name, toper->name);
	    //write_check(c1->varlocal, buf1); 

    calc_op (c1, tpart, toper, &v1, &s1, &r1, LABOR_1P);
    calc_op (c1, tpart, toper, &v1, &s2, &r2, EQUIP_1P);
    ret = calc_xprime(c1, tpart, toper, s1+r1, s2+r2);


return(ret);
}

float  get_tb_time (class_model * c1, class_part *tpart, class_oper * toper)
{       float ret;
        float s1;
        float r1;
        float s2;
        float r2;
        float v1;
    calc_op (c1, tpart, toper, &v1, &s1, &r1, LABOR_1_TB);
    calc_op (c1, tpart, toper, &v1, &s2, &r2, EQUIP_1_TB);
    ret = calc_xprime(c1, tpart, toper, s1+r1, s2+r2);


return(ret);
}
///////////////////////////////////////////////////
float get_dtdl (class_model * c1, class_part * rpart, class_oper * toper)
{ float dif;
  float v1, se, re, sl, rl;

    calc_op (c1,  rpart, toper, &v1, &sl, &rl, LABOR_DIF);
    calc_op (c1,  rpart, toper, &v1, &se, &re, EQUIP_DIF);
    dif = calc_xprime(c1,  rpart, toper, sl+rl, se+re);

 return(dif);
}

///////////////////////////////////////////////////
float  dJdT (class_model * c1, class_part * rpart, class_oper * toper)
{ float dif;
  class_eq * teq;
  float t1;
  float u;
  float x1;

  teq = c1->get_eq_num(toper->eq_ptr);

  if (teq->num > 0) {
     u = teq->u/100.0;
     x1 = rpart->dlam*toper->lvisit;
     dif = teq->sum_a_g * teq->y1 * x1 * ( u + teq->y2*(1.-u) )
                / (teq->lambda * teq->num * (1.-u)*(1.-u));

     dif += rpart->weight*toper->vpergood;
  }
  else
     dif = rpart->weight * toper->vpergood;

 return(dif);
}

///////////////////////////////////////////////////
float  dJdX (class_model * c1, class_part * rpart, class_oper * toper)
{ float dif;
  class_eq * teq;
  float t1;
  float u;

  teq = c1->get_eq_num(toper->eq_ptr);
  if (teq->num > 0) {
     t1 = get_totaltime (c1, rpart, toper);
     u = teq->u/100.0;
     dif = teq->sum_a_g * teq->y1 * (
                t1 * teq->lambda * u
                + t1 * teq->y2 * (1.-u) * teq->lambda
                - u * (1. - u)
               ) / ( (1.-u)*(1.-u) *  teq->lambda*teq->lambda* teq->num);

  }
  else
     dif = 0.0;

 return(dif);
}
///////////////////////////////////////////////////
float get_dtdb (class_model * c1, class_part * rpart, class_oper * toper)
{ float dif, v1, se, re, sl, rl, rtot;

    calc_op (c1, rpart, toper, &v1, &sl, &rl, LABOR_DIF_T);
    calc_op (c1, rpart, toper, &v1, &se, &re, EQUIP_DIF_T);
    rtot = calc_xprime(c1, rpart, toper, sl+rl, se+re);

    dif = -1 * toper->lsize * rtot / (rpart->tbatch*rpart->tbatch);

 return(dif);
}
///////////////////////////////////////////////////
float DTDB (class_model * c1, class_part * rpart, class_oper * toper)
{ float dif, v1, se, re, sl, rl, rtot;

    calc_op (c1, rpart, toper, &v1, &sl, &rl, LABOR_DIF_1);
    calc_op (c1, rpart, toper, &v1, &se, &re, EQUIP_DIF_1);
    rtot = calc_xprime(c1, rpart, toper, sl+rl, se+re);

    dif = rtot;

 return(dif);
}

///////////////////////////////////////////////////
float  dJdT2(class_model * c1, class_part * rpart, class_oper * toper)
{ float dif;

    dif = dJdT(c1, rpart, toper);
    dif -= rpart->weight*toper->vpergood;

	
 return(dif);
}
