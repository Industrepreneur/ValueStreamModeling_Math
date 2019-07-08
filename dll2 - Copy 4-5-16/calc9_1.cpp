
#include <iostream>
#include <fstream>

#include "class_model.cpp"
#include "nrutil.h"




 void write_results (class_model * c1);
 float fvalue  (class_model * c1, int * isLotOrTbatch,  float *lot1);
 void mpcalc   (class_model * c1);

 
int      numberOfParameterAffected;       // the number of parameters that will be changed
float*   valueOfParameterAffected;        // the actual value to be optimized
float*   weight;
class_part** partPointer;
int*     isLotOrTbatch;








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
        float   resultValue, v1;
        int     iter, ret;
        char    partname[200];
		char buf1[MESSAGE_SIZE];

		int numberOfParameterAffected;

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


        //sprintf(buf1, "No. of part %d",  numberOfParts);
        //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );


        // set numberOfParameterAffected


        numberOfParameterAffected = 0;
        for (i=0; i<numberOfParts; i++)
        {
                if(optimizeLotSize[i]!=0)
                {
                        numberOfParameterAffected++;
                }
                if(optimizeTbatch[i]!=0)
                {
                        numberOfParameterAffected++;
                }

        }

        sprintf(buf1, "No. of para %d",  numberOfParameterAffected);
        //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

        if (numberOfParameterAffected <=0)
        {
                //MessageBox( NULL, "No products to optimize", "In MPX optimization", MB_OK | MB_TASKMODAL );
                
				sprintf(s0 , " 0 \n");
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

        valueOfParameterAffected = vector (1, numberOfParameterAffected+5);

            // create a list of pointer to parts and the type of parameter which will be optimized
            partPointer= (class_part**)malloc(sizeof(class_part)*numberOfParameterAffected);
            isLotOrTbatch=(int*)malloc(sizeof(int)*numberOfParameterAffected);
            j=0;
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
	

                            partPointer[j]=tpart; // find_part(partname);
                            isLotOrTbatch[j]=2;

                            //  warning !!!!  index starts at 1
                            valueOfParameterAffected[j+1]=tpart->lotsiz;

                            j++;
                    }
                    else if(optimizeLotSize[i]!=0)
                    {
                            partPointer[j]=tpart;
                            isLotOrTbatch[j]=0;
							
							if (tpart->tbatch == -1) {
								if (tpart->lotsiz < 2.0) {
									tpart->tbatch = 1;
								    tpart->lotsiz = 2;
								}
								else 
									tpart->tbatch = 1;
							}

                            //  warning !!!!  index starts at 1
                            valueOfParameterAffected[j+1]=tpart->lotsiz;

                            j++;
                    }


                    if(optimizeTbatch[i]!=0)
                    {
                            partPointer[j]=tpart;
                            isLotOrTbatch[j]=1;
							
							if (tpart->tbatch == -1) tpart->tbatch = 1;
 
                            //  warning !!!!  index starts at 1
                            valueOfParameterAffected[j+1]=tpart->tbatch;

                            j++;
                    }
					
            }

			// saving initial TBATCH SIZES ...
			  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
				tpart->init_tb  = tpart->tbatch;
			}

            c1->tolerance = numberOfParameterAffected * 0.0001;
            c1->utlimit = 95.00;

			c1->call_frpmn(valueOfParameterAffected, numberOfParameterAffected, tolerance, &iter, &resultValue);

            

                    v1 = 0;
                    for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                                             v1 += tpart->weight * tpart->qpart;
                    }
                    sprintf(s0, " value %f", v1);
                    //MessageBox( NULL, buf1, "In MPX optimization", MB_OK | MB_TASKMODAL );

                    //MessageBox( NULL, "returning value", "In MPX optimization", MB_OK | MB_TASKMODAL );
                    sprintf(s0, "%f", v1);
                    outfile << s0;
					outfile.close();  //fclose(filePointer);

            // output results to "LotAndTbatch.res"
					 strcpy (xx1, c1->varlocal);
     strcat(xx1, "\\");
     strcat(xx1, "LotAndTbatch.res");

  	 ofstream outfile2 (xx1);
            if (outfile2.is_open() == false){
                    return(0);
            }

            sprintf(s0, " PRODID, LOTSIZE, TBATCHSIZE\n");
			outfile2 << s0;

            for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                    tpart->bc_total = -2;
                    tpart->bc_shipped = -2;
            }


            for (i = 1;i<= numberOfParameterAffected; i++)
            {
               j = i - 1;
                if ((isLotOrTbatch[j] == 0) || (isLotOrTbatch[j] == 2)){
                    partPointer[j]->bc_total = partPointer[j]->lotsiz ;
                }
                else {
                    partPointer[j]->bc_shipped = partPointer[j]->tbatch;
                }
            }

             for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                sprintf(s0, " %s, %f, %f \n", tpart->name, tpart->bc_total, tpart->bc_shipped);
				outfile2 << s0;
			 }
            outfile2.close();


             write_results (c1);

            // free memory

            free_vector (valueOfParameterAffected, 1, numberOfParameterAffected+5);
            free (partPointer);
            free (isLotOrTbatch);

            return(1);

}


///////////////////////////////////
float fvalue  (class_model * c1, int * isLotOrTbatch,  float *lot1)
///////////////////////////////////
{
        class_part * tpart;
        int i, j, ok;
        float v1, x1;

        char buf1 [123];

		class_part ** partPointer;

        // lot1[]  offset .. starts at 1
		
		
		// saving initial TBATCH SIZES ...
			for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
	         	tpart->tbatch = tpart->init_tb;
			}


        for (i = 1 ;i<= numberOfParameterAffected; i++)
        {
                j = i - 1;
                if (isLotOrTbatch[j] ==0) {
                        partPointer[j]->lotsiz = lot1[i];
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
                }
                else if (isLotOrTbatch[j] == 1) {
                        partPointer[j]->tbatch = min(lot1[i], partPointer[j]->lotsiz);
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
						if (lot1[i] > partPointer[j]->lotsiz) return (-1.0);
                }
				else if (isLotOrTbatch[j] ==2) {
						partPointer[j]->tbatch = -1;
                        partPointer[j]->lotsiz = lot1[i];
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
                }

                partPointer[j]->check_rout = TRUE;
        }

        c1->INRUN = TRUE;
        c1->RESTART_CANCEL = FALSE;

        do_all_visits(c1);

        ok = TRUE;
        c1->FULL = TRUE;

        mpcalc(c1);


        if (ok == FALSE)
        {
                Message("Internal error, calculation stopped");
                exit(1);
        }
        if ((c1->over_util_L == TRUE) || (c1->over_util_E == TRUE) )
        {
                return(-1);
        }

        v1 = 0;

        
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                 v1 += tpart->weight * tpart->qpart;
        }

        return (v1);
}



///////////////////////////////////
void  dif1  (class_model * c1, float *lot1, float * dif2)
///////////////////////////////////
{
  class_part  * tpart;
  class_eq * teq;
  class_oper  * toper;

  float Vop;
  float total_time;
  float partial;
  float time_p_p;
  float time_p_tb;
  float x1;
  float zij;
  float u1;
  float d1;
  int i, j;


  char buf1 [128];

  int o_count;
  int o_start;

   for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
      teq->lambda  = 0.0;  // sum arrival of opers to eq
      teq->sum_a_g = 0.0;  // sum value * no. of good visits

      if (teq->num> 0) {
         u1 = teq->u /100.0;

      teq->y1 = 0.5 * (teq->ca2 + teq->cs2) * NDI_POW(u1,(sqrt(2.0*(teq->num+1.0)) -1.0));
                //  cv * u ^ (sqrt (2*(n+1)) -1 )
      teq->y2 =  sqrt(2.0*(teq->num+1.0));
                 // sqrt (2*(n+1))
                 // sqrt (2*(n+1)) + u/(1-u) + 1

          teq->y3 =  teq->num * u1;
          // ni * ui
      }
      else {
          teq->y1 = 0.0;
          teq->y2 = 0.0;
          teq->y3 = 0.0;
      }
  }

  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 

    o_start = tpart->oper_start;
	o_count = tpart->oper_cnt;
	i = o_start +3;
				
     for (i= o_start+3; i<= o_start+o_count; i++ )                
     {toper = c1->get_oper_num(i);
      teq = c1->get_eq_num(toper->eq_ptr);
      Vop = toper->lvisit; // arrival rate to operation
      teq->lambda += Vop * tpart->dlam;  //sum of arrival rate
      teq->sum_a_g +=tpart->weight * tpart->dlam * toper->lsize * toper->vpergood;  //sum of weights * good visits
    }
  }



  for (i = 0; i<numberOfParameterAffected; i++) {

    partial = 0.0;

        tpart = partPointer[i];

        if (isLotOrTbatch[i] == 2 ) {
        // opt lotsizes with tbatch = -1
              o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i<= o_start+o_count; i++ )                
              {toper = c1->get_oper_num(i);
                teq = c1->get_eq_num(toper->eq_ptr);

                time_p_p = get_time_p_p(c1, tpart, toper);
                total_time = get_totaltime(c1, tpart, toper);
                x1 =  toper->lvisit * tpart->dlam;
                zij = x1 * (time_p_p - total_time / toper->lsize);

                partial +=  teq->y1 / teq->lambda * teq->sum_a_g
                              * (zij * teq->y2 + teq->y3 * x1 / (toper->lsize * teq->lambda))
                           +  time_p_p * tpart->weight * tpart->dlam * toper->lsize * toper->vpergood;
             } // for loop

            //zz if (tpart->lotsiz <(1.0 + EPSILON))
            //zz     partial = MIN(0.0, partial);
        }

        if (isLotOrTbatch[i] == 0 ) {
        // opt lotsizes tbatch is assumed fixed.!!!
             o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i<= o_start+o_count; i++ )                
              {toper = c1->get_oper_num(i);
                teq = c1->get_eq_num(toper->eq_ptr);

                time_p_p = get_time_p_p(c1, tpart, toper);
                time_p_tb = get_tb_time(c1, tpart, toper);
                total_time = get_totaltime(c1, tpart, toper);
                x1 =  toper->lvisit * tpart->dlam;
                zij = x1 * (time_p_tb/tpart->tbatch + time_p_p - total_time / toper->lsize);

                partial +=  teq->y1 / teq->lambda * teq->sum_a_g
                              * (zij * teq->y2 + teq->y3 * x1 / (toper->lsize * teq->lambda));
             } // for loop
			 d1 = max( 0.1, (tpart->lotsiz - tpart->tbatch));
             partial += tpart->weight * tpart->dlam * tpart->lotsiz  * tpart->tgather / d1;

            //zz if (tpart->lotsiz <(1.0 + EPSILON))
            //zz     partial = MIN(0.0, partial);
        }

        if (isLotOrTbatch[i] == 1) {
        // opt tbatch sizes !!!
              o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i<= o_start+o_count; i++ )                
                {toper = c1->get_oper_num(i);
				
                teq = c1->get_eq_num(toper->eq_ptr);

                time_p_p = get_time_p_p(c1, tpart, toper);
                time_p_tb = get_tb_time(c1, tpart, toper);
                x1 =  toper->lvisit * tpart->dlam;
                zij = time_p_tb * x1* tpart->lotsiz / (tpart->tbatch * tpart->tbatch);

                partial += -1 * teq->y1 / teq->lambda * teq->sum_a_g
                              *  zij * teq->y2;
                partial += tpart->weight * tpart->dlam * toper->lsize * toper->vpergood * time_p_p;
             } // for loop
			 d1 = max( 0.1, (tpart->lotsiz - tpart->tbatch));
             partial +=  tpart->weight * tpart->dlam * tpart->lotsiz  * -1.0 * tpart->tgather * (tpart->lotsiz / d1) / tpart->tbatch;

             //zz if (tpart->lotsiz <(1.0 + EPSILON))
             //zz     partial = MIN(0.0, partial);
        }


        dif2[i+1] = partial;
   } // end i loop



return;
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
