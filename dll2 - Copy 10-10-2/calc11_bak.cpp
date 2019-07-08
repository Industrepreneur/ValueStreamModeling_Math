
#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"


#define ITMAX 200
#define EPS 1.0e-10

 void write_results (class_model * c1);

 void mpcalc   (class_model * c1);
    int over_u (class_model * c1);


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void  do_all_visits ( class_model * c1 );
	 

float  get_totaltime (class_model * c1, class_part *tpart, class_oper * toper);
float  get_time_p_p (class_model * c1, class_part *tpart, class_oper * toper);
float  get_tb_time (class_model * c1, class_part *tpart, class_oper * toper);
float  get_dtdl (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdX (class_model * c1, class_part * rpart, class_oper * toper);
float  get_dtdb (class_model * c1, class_part * rpart, class_oper * toper);
float  DTDB (class_model * c1, class_part * rpart, class_oper * toper);
float  dJdT2(class_model * c1, class_part * rpart, class_oper * toper);

///////////////////////////////////
	float fvalue_ci   (class_model * c1, float *lot1)
///////////////////////////////////
{
	     
	    class_part * tpart;
        int i, j, ok;
        float v1, x1;

        char buf1 [123];


        // lot1[]  offset .. starts at 1
		
	 write_check(c1->varlocal, " in fvalue  ");

			
		// using ... initial TBATCH SIZES ...
			for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
	         	tpart->tbatch = tpart->init_tb;
			}

			sprintf(buf1, "no of parametrs %i", c1->numberOfParameterAffected);
			write_check(c1->varlocal, buf1);

        for (i = 1;i<= c1->numberOfParameterAffected; i++)
        {
				j = i - 1; 

                tpart = c1->partPointer[j];

			
				sprintf(buf1, "parameter %i:%i  type %i  fvalue %f  partname %s ",i, j, c1->isLotOrTbatch[j],  lot1[i], tpart->name );
			    write_check(c1->varlocal, buf1);

                
                if (c1->isLotOrTbatch[j] ==0) {
                        tpart->lotsiz = lot1[i];  //gwwd fix 9-25  no no no   index of lot starts at 1
						tpart->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
                }
                else if (c1->isLotOrTbatch[j] == 1) {
                        tpart->tbatch = MIN(lot1[i], tpart->lotsiz);
						tpart->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
						if (lot1[i] > tpart->lotsiz) return (-1.0);
                }
				else if (c1->isLotOrTbatch[j] ==2) {

					    // write_check(c1->varlocal, "test 1 ");
						tpart->tbatch = -1;
						// write_check(c1->varlocal, "test 2 ");
                        tpart->lotsiz = lot1[i];
						// write_check(c1->varlocal, "test 3 ");
						tpart->check_rout = TRUE;
						//write_check(c1->varlocal, buf1);
						//write_check(c1->varlocal, " in details ");
						if (lot1[i] < 0.0)  { write_check(c1->varlocal, " in details exit  "); return (-1.0);}
                }

				//write_check(c1->varlocal, " in details  check route  ");

                tpart->check_rout = TRUE;

				//write_check(c1->varlocal, " in details after check route  ");
				sprintf(buf1, "was parameter %i  type %i  lotsize %f", j, c1->isLotOrTbatch[j],  lot1[i]);
			    write_check(c1->varlocal, buf1);
        }

		 write_check(c1->varlocal, " in fvalue  bbb");

        c1->INRUN = -1;
        c1->RESTART_CANCEL = FALSE;

        //do_all_visits(c1);  inside mpcalc ...

		write_check(c1->varlocal, " in fvalue ccc ");

        ok = TRUE;
        c1->FULL = TRUE;

        mpcalc(c1);

			 write_check(c1->varlocal, " in fvalue dddd ");

        if (ok == FALSE)
        {
                write_check(c1->varlocal, " in fvalue Internal error, calculation stopped");
                exit(1);
        }
        if ((c1->over_util_L == TRUE) || (c1->over_util_E == TRUE) )
        {
				 write_check(c1->varlocal, " in fvalue eee ");
                return(-1);
        }

        v1 = 0;

        
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                 v1 += tpart->weight * tpart->qpart;
        }

		sprintf(buf1, " exiting fvalue fff %f  ", v1);
	    write_check(c1->varlocal, buf1);

        return (v1);





};




///////////////////////////////////
	void dif1_ci    (class_model * c1, float *lot1, float * dif2)
///////////////////////////////////
{
  class_part  * tpartall;
  class_lab * tlab;
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

  float a;
  float b;
  float b1, b2, b3, b4;
  float   xbars, xbarr, xbar1, xbar2, xbarsl, xbarrl;
  float   xprime, vlam1, xprsig;
  float t1;



  char buf1 [1128];

  class_part * tpart_z;

  double partial_0;
  double partial_1;
  double partial_2;
  double partial_3;


  int i_oper;
  int o_count;
  int o_start;

   write_check(c1->varlocal, "into dif  c1 "); 

   for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
      teq->lambda  = 0.0;  // sum arrival of opers to eq
      teq->sum_a_g = 0.0;  // sum value * no. of good visits

	  teq->dU_dL1 = 0.0;
	  teq->N_1 = 0.0;
	  teq->N_2 = 0.0;

      if (teq->num> 0) {
         u1 = teq->u /100.0;

      teq->y1 = 0.5 * (teq->ca2 + teq->cs2) * NDI_POW(u1,(sqrt(2.0*(teq->num+1.0)) -1.0));
                //  cv * u ^ (sqrt (2*(n+1)) -1 )

      teq->y2 =  sqrt(2.0*(teq->num+1.0));
                

      teq->y3 =  teq->num * u1;
          // ni * ui
      }
      else {
          teq->y1 = 0.0;
          teq->y2 = 0.0;
          teq->y3 = 0.0;
      }
  }

  //write_check(c1->varlocal, "into dif  dd1 "); 
   /*

  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 

    o_start = tpart->oper_start;
	o_count = tpart->oper_cnt;
	i_oper = o_start +3;
				
     for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )             //gwwd  <=  no     should be <    
     {toper = c1->get_oper_num(i_oper);
      teq = c1->get_eq_num(toper->eq_ptr);

	  	//sprintf(buf1, " middle dif  ddd2  %i %s %s %f", i, teq->name, toper->name, toper->lvisit);
		//write_check(c1->varlocal, buf1); 

      Vop = toper->lvisit; // arrival rate to operation
      teq->lambda += Vop * tpart->dlam;  //sum of arrival rate
      teq->sum_a_g +=tpart->weight * tpart->dlam * toper->lsize * toper->vpergood;  //sum of weights * good visits
    }
  }
  */

   //write_check(c1->varlocal, "into dif  eee1 "); 

   	//sprintf(buf1, " middle dif ee2 %i  ", c1->numberOfParameterAffected);
	//write_check(c1->varlocal, buf1); 

  for (i = 1; i<=c1->numberOfParameterAffected; i++) {

    partial = 0.0;

	 for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
      teq->lambda  = 0.0;  // sum arrival of opers to eq
      teq->sum_a_g = 0.0;  // sum value * no. of good visits

	  teq->dU_dL1 = 0.0;
	  teq->N_1 = 0.0;
	  
	 }

	//gwwd  fix 1, 0 index start !!!  9-25
	j = i-1;

	//sprintf(buf1, " middle dif ee3 %i  %i  ", j, c1->isLotOrTbatch[j]);
	//write_check(c1->varlocal, buf1); 




        tpart_z = c1->partPointer[j];

				sprintf(buf1, " middle dif ee4 %i %i %s  type : %i ", i,j, tpart_z->name, c1->isLotOrTbatch[j]);
				write_check(c1->varlocal, buf1); 

        if (c1->isLotOrTbatch[j] == 2 ) {

			  partial_0 = 0;
			 
              o_start = tpart_z->oper_start;
			  o_count = tpart_z->oper_cnt;
			  i_oper = o_start +3;
				
              for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )    //gwwd  <=  no     should be <                  
              { toper = c1->get_oper_num(i_oper);
                teq = c1->get_eq_num(toper->eq_ptr);
				tlab = c1->get_lab_num(teq->lab_ptr);

				calc_op (c1, tpart_z, toper, &vlam1, &xbarsl, &xbarrl, LABOR_T);
                xbarrl *= MAX(1.0, toper->lsize);

                calc_op (c1, tpart_z, toper, &vlam1, &xbars,  &xbarr,  EQUIP_T);
                xbarr *=  MAX(1.0, toper->lsize); 
				
				//  adding labor time ...  to setup only!!!  & equip down too
				xbar1 = (xbarsl) / (1.0+tlab->facovt/100.0); //  calc2 has multiply not divide.!!!  gwwd 10-6
                xbar2 = xbars / (1.0+teq->facovt/100.0);		//  missing calc2  ot ?? gwwd  10-6

				//xbar1 = labortotal, xbar2 = total equip

				
                xprime = calc_xprime(c1, tpart_z, toper, xbar1, xbar2);

				teq->dU_dL1 += -1 * vlam1 * xprime / toper->lsize;  //gwwd 10-6  or / tpart_z->lotsiz ?

				partial_0 +=  tpart_z->weight * xbarr * vlam1* toper->lsize;


			  }  //   end oper loop for tpartz 

			partial_1 = 0;
			partial_2 = 0;
			partial_3 = 0;

        // opt lotsizes with tbatch = -1
		for(tpartall  = c1->first_part_ptr(); tpartall != NULL; tpartall = c1->next_part_ptr()) { 


              o_start = tpartall->oper_start;
			  o_count = tpartall->oper_cnt;
			  i_oper = o_start +3;
				
              for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )    //gwwd  <=  no     should be <                  
              { toper = c1->get_oper_num(i_oper);
                teq = c1->get_eq_num(toper->eq_ptr);

				calc_op (c1, tpart_z, toper, &vlam1, &xbarsl, &xbarrl, LABOR_T);
                xbarrl *= MAX(1.0, toper->lsize);

                calc_op (c1, tpart_z, toper, &vlam1, &xbars,  &xbarr,  EQUIP_T);
                xbarr *=  MAX(1.0, toper->lsize); 

				partial_2 +=  teq->dU_dL1 * tpartall->weight * vlam1 * vlam1 * teq->wait * ( 1./(1. - teq->u/100.0) + teq->y2 / (teq->u/100.0) );
				 
				teq->N_1 +=  partial_2;

				//sprintf(buf1, " middle dif aa %i %s %s ", i, teq->name, toper->name);
				sprintf(buf1, "value, j, i %i %i ", j, i);
				write_check(c1->varlocal, buf1); 

				

			  }//  end op er loop 
		} // end part loop 


		
				------------------------------------------------------

		/*  old replaced !!		time_p_p = get_time_p_p(c1, tpart_z, toper);


                
                total_time = get_totaltime(c1, tpart_z, toper);
                x1 =  toper->lvisit * tpart_z->dlam;
                zij = x1 * (time_p_p - total_time / toper->lsize);
				b4 = time_p_p - total_time / toper->lsize;
				

				b1 =  teq->y1 / teq->lambda * teq->sum_a_g;
                 b2 =               (zij * teq->y2 + teq->y3 * x1 / (toper->lsize * teq->lambda))  ;
                 b3 =           +  time_p_p * tpart_z->weight * tpart_z->dlam * toper->lsize * toper->vpergood;
				partial += b1 * b2 + b3;

				sprintf(buf1, " middle dif aa type2 %i %i: %s %s  %f  %f  %f %f  %f  %f %f %f %f %f %f %f %f %f %f ", i,j,  teq->name, toper->name, b, partial, teq->y1, teq->lambda, teq->sum_a_g, zij, teq->y2, teq->y3, x1, toper->lsize,time_p_p, tpart_z->weight,tpart_z->dlam, toper->lsize, toper->vpergood );
				write_check(c1->varlocal, buf1); 

				sprintf(buf1, " middle dif aa type2a  %f  %f  %f %f", b1, b2, b3, b4);
				write_check(c1->varlocal, buf1); 

				} //  end tpart_z loop 
				else { 

				} 
              
             } // for  oper loop

            if (tpart_z->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        };  // for part list 

		*/   //  end of replaced

		}  // end if para 1

        else if (c1->isLotOrTbatch[j] == 0 ) {
        // opt lotsizes tbatch is assumed fixed.!!!
			for(tpartall  = c1->first_part_ptr(); tpartall != NULL; tpartall = c1->next_part_ptr()) { 


             o_start = tpartall->oper_start;
			  o_count = tpartall->oper_cnt;
			  i_oper = o_start +3;
				
              for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )   //gwwd  <=  no     should be <                  
              {toper = c1->get_oper_num(i_oper);
                teq = c1->get_eq_num(toper->eq_ptr);
				sprintf(buf1, " middle dif  bb %i %s %s ", i, teq->name, toper->name);
				write_check(c1->varlocal, buf1); 

				if (tpart_z == tpartall) { 

                time_p_p = get_time_p_p(c1, tpartall, toper);
                time_p_tb = get_tb_time(c1, tpartall, toper);
                total_time = get_totaltime(c1, tpartall, toper);
                x1 =  toper->lvisit * tpartall->dlam;
                zij = x1 * (time_p_tb/tpartall->tbatch + time_p_p - total_time / toper->lsize);

				a =  teq->y1 / teq->lambda * teq->sum_a_g
                              * (zij * teq->y2 + teq->y3 * x1 / (toper->lsize * teq->lambda));

				partial += a;

				sprintf(buf1, " middle dif aa3 %i %i: %s %s  %f  %f  %f %f %f %f  %f %f %f %f ", i,j,  teq->name, toper->name, a, partial, teq->y1, teq->lambda, teq->sum_a_g, zij, teq->y2, teq->y3, x1, toper->lsize);
				write_check(c1->varlocal, buf1); 
				}  // if loop 
			  }  // for oper loop 
             } // for part loop 

			 d1 = max( 0.1, (tpartall->lotsiz - tpartall->tbatch));
             partial += tpartall->weight * tpartall->dlam * tpartall->lotsiz  * tpartall->tgather / d1;  //   gwwd 10-2 ??  why ???



             if (tpartall->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);

        }  //  end of if

        else if (c1->isLotOrTbatch[j] == 1) {
        // opt tbatch sizes !!!
			for(tpartall  = c1->first_part_ptr(); tpartall != NULL; tpartall = c1->next_part_ptr()) { 

              o_start = tpartall->oper_start;
			  o_count = tpartall->oper_cnt;
			  i_oper = o_start +3;
				
              for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )                
                {toper = c1->get_oper_num(i_oper);
				
                teq = c1->get_eq_num(toper->eq_ptr);

				sprintf(buf1, " middle dif cc %i %i: %s %s ", i, j,  teq->name, toper->name);
				write_check(c1->varlocal, buf1); 

                time_p_p = get_time_p_p(c1, tpartall, toper);
                time_p_tb = get_tb_time(c1, tpartall, toper);
                x1 =  toper->lvisit * tpartall->dlam;
                zij = time_p_tb * x1* tpartall->lotsiz / (tpartall->tbatch * tpartall->tbatch);

                partial += -1 * teq->y1 / teq->lambda * teq->sum_a_g
                              *  zij * teq->y2;
                partial += tpartall->weight * tpartall->dlam * toper->lsize * toper->vpergood * time_p_p;

             } // for oper loop
			}// for tpartall loop 

			 d1 = max( 0.1, (tpart_z->lotsiz - tpart_z->tbatch));

	
			 a= tpart_z->weight * tpart_z->dlam * tpart_z->lotsiz  * -1.0 * tpart_z->tgather * (tpart_z->lotsiz / d1) / tpart_z->tbatch;
			 partial += a;
			 if (d1 >= 0.1)  {
				 sprintf (buf1, "gett tbatch gather %f %f  ", d1,a );
				 write_check(c1->varlocal, buf1);
			 }

              if (tpart_z->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        } //  end if partial type ...

		sprintf(buf1, "end of dif  fff %i %i:  %f ", i, j,  partial );   //   gwwd 10-1 XXX
    	write_check(c1->varlocal, buf1); 

        dif2[i] = partial;  //  gwwd  error !!!  j is 0 based  use  i  1 based  NOT  I+1  !!!
   } // end i loop

   write_check(c1->varlocal, "exiting dif_ci");   //  gwwd 10-1 XXX

return;
}


// #include <math.h>
#define NRANSI


////////////////////////////////////////////////////////////////////////////////////////////////////


	
#define NR_END 3       //  was 1  gwwd 9-29
#define FREE_ARG float *  //char*   // was char * gwwd 9-29


//void free_vector(float *v, long nl, long nh);
// float *vector(long nl, long nh);

float *vector(long nl, long nh)
/* allocate a float vector with subscript range v[nl..nh] */
{
	float *v;
	void * xxx;

	xxx=malloc((size_t) ((nh-nl+2+NR_END)*sizeof(float)));
	v = (float *) xxx;
	 //  if (!v) nrerror("allocation failure in vector()");
	return v-nl+NR_END;
}


void free_vector(float *v, long nl, long nh)
/* free a float vector allocated with vector() */
{
		void * xxx;
		xxx = (void *) (v+nl-NR_END);
		free (xxx);

	    // free((FREE_ARG) (v+nl-NR_END));
}


///////////////////////////////////////////////////////////



/////////////////////////////////////////////////////////////////////////////////////////////////

//	for opt stuff 

#include "nrutil.h"



#define FREEALL free_vector(xi,1,n);free_vector(h,1,n);free_vector(g,1,n);

	

	float brent(class_model * c1, float ax, float bx, float cx,  float tol, float *xmin);  //   float (*f)(float),
	float brentgd(class_model * c1, float ax, float bx, float cx,	 float tol, float *xmin); //  float (*f)(float),



void linmin(class_model * c1, float p[], float xi[], int n, float *fret);  	//float (*func)(int *, float [])); 

//   transfered from nrutil.c

//   inside  class so we get it

  //   moved to class_model  int ncom;  //  moved to class_model

   //   moved to class_model float *pcom,*xicom;  //(*nrfunc)(float []);  //  moved to class_model

   //float brent(float ax, float bx, float cx, float (*f)(float), float tol, float *xmin);
	//float brentgd(float ax, float bx, float cx,	float (*f)(float), float tol, float *xmin);

	float f1dim(class_model * c1,float x);													 //  gwwd added
	float brent(class_model * c1, float ax, float bx, float cx, float tol, float *xmin);
	float brentgd(class_model * c1, float ax, float bx, float cx,	float tol, float *xmin);
	 void mnbrak(class_model * c1, float *ax, float *bx, float *cx, float *fa, float *fb,       //   gwwd added 
		float *fc);  // removed , float (*func)(float));

void linmin(class_model * c1, float p[], float xi[], int n, float *fret) //, float (*func)(float []))
{
	
	int j;
	float xx,xmin,fx,fb,fa,bx,ax;

	write_check(c1->varlocal, "starting linmin");

	c1->ncom=n;
	c1->pcom=vector(1,n);   //gwwd  local 9-29
	c1->xicom=vector(1,n);
	// nrfunc=func;
	for (j=1;j<=n;j++) {
		c1->pcom[j]=p[j];
		c1->xicom[j]=xi[j];
	}
	ax=0.0;
	xx=1.0; // ?? bx?
	mnbrak(c1, &ax,&xx,&bx,&fa,&fx,&fb); //  ,f1dim);
	//*fret=brent(ax,xx,bx,f1dim,tolerance,&xmin);
	*fret=brentgd(c1, ax,xx,bx,c1->tolerance,&xmin);  //  brentgd(ax,xx,bx,f1dim,tolerance,&xmin);
	for (j=1;j<=n;j++) {
		xi[j] *= xmin;
		p[j] += xi[j];
	}
	free_vector(c1->xicom,1,n);  //??  gwwd ??  9-27
	free_vector(c1->pcom,1,n);   //??  gwwd ??  9-27

		write_check(c1->varlocal, "ending linmin");
}





void frprmn(class_model * c1, float p[], int n,  float *fret) 	//  float (*func)(int * ci, float []), void (*dfunc)(int * ci, float [], float []))
{
	
	int j,its;
	float gg,gam,fp,dgg;
	float *g,*h,*xi;

	g=vector(1,n);    //  gwwd  local 9-29
	h=vector(1,n);
	xi=vector(1,n);

	write_check(c1->varlocal, "starting frprmn");

	fp =  fvalue_ci(c1,  c1->valueOfParameterAffected);  //gwwd added call  to func value 
	dif1_ci(c1, c1->valueOfParameterAffected, xi);       //  here is the call to differential ...

	for (j=1;j<=n;j++) {
		g[j] = -xi[j];                        // using diff value  xi  to g  xi, h
		xi[j]=h[j]=g[j];
	}
	for (its=1;its<=ITMAX;its++) {
		c1->iter=its;                                        //  gwwd change iter is class local...
					 write_check(c1->varlocal, " about to linmin ");
		linmin(c1, c1->valueOfParameterAffected,xi,n,fret);  //,func);  //gwwd added c1  using xi XXX  gwwd 10-1 XXX
		if (2.0*fabs(*fret-fp) <= c1->tolerance*(fabs(*fret)+fabs(fp)+EPS)) {
			FREEALL   //??  gwwd crash ??  9-27
			return;
		}
		fp= *fret;
		dif1_ci(c1, c1->valueOfParameterAffected,xi);   //gwwd added c1  recalc differentiall  into xi
		dgg=gg=0.0;
		for (j=1;j<=n;j++) {
			gg += g[j]*g[j];                            //  ?   using g using older diferential ...
			dgg += (xi[j]+g[j])*xi[j];                  //   ?  using new and old differential ...
		}
		if (gg == 0.0) {
			write_check(c1->varlocal, "ending frprmn");
			FREEALL    //??  gwwd crash ??  9-27
			return;
		}
		gam=dgg/gg;
		for (j=1;j<=n;j++) {
			g[j] = -xi[j];                           //   ?  using new differential ...
			xi[j]=h[j]=g[j]+gam*h[j];                //   ?  using new and old differential ...
		}
	}

	write_check(c1->varlocal, "quiting frprmn");
	//nrerror("Too many iterations in frprmn");
}




	
 
#undef ITMAX
#undef EPS
#undef FREEALL
#undef NRANSI 