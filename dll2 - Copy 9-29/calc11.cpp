
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

				write_check(c1->varlocal, " in details  check route  ");

                tpart->check_rout = TRUE;

				write_check(c1->varlocal, " in details after check route  ");
				sprintf(buf1, "was parameter %i  type %i  fvalue %f", j, c1->isLotOrTbatch[j],  lot1[i]);
			    write_check(c1->varlocal, buf1);
        }

		 write_check(c1->varlocal, " in fvalue  bbb");

        c1->INRUN = -1;
        c1->RESTART_CANCEL = FALSE;

        do_all_visits(c1);

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

	    write_check(c1->varlocal, " exiting fvalue fff ");

        return (v1);





};




///////////////////////////////////
	void dif1_ci    (class_model * c1, float *lot1, float * dif2)
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

   write_check(c1->varlocal, "into dif  c1 "); 

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

    write_check(c1->varlocal, "into dif  dd1 "); 

  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 

    o_start = tpart->oper_start;
	o_count = tpart->oper_cnt;
	i = o_start +3;
				
     for (i= o_start+3; i< o_start+o_count; i++ )             //gwwd  <=  no     should be <    
     {toper = c1->get_oper_num(i);
      teq = c1->get_eq_num(toper->eq_ptr);

	  	sprintf(buf1, " middle dif  ddd2  %i %s %s %f", i, teq->name, toper->name, toper->lvisit);
		write_check(c1->varlocal, buf1); 

      Vop = toper->lvisit; // arrival rate to operation
      teq->lambda += Vop * tpart->dlam;  //sum of arrival rate
      teq->sum_a_g +=tpart->weight * tpart->dlam * toper->lsize * toper->vpergood;  //sum of weights * good visits
    }
  }

   write_check(c1->varlocal, "into dif  eee1 "); 

   	sprintf(buf1, " middle dif ee2 %i  ", c1->numberOfParameterAffected);
	write_check(c1->varlocal, buf1); 

  for (i = 1; i<=c1->numberOfParameterAffected; i++) {

    partial = 0.0;

	
	//gwwd  fix 1, 0 index start !!!  9-25
	j = i-1;
	sprintf(buf1, " middle dif ee3 %i  %i  ", j, c1->isLotOrTbatch[j]);
	write_check(c1->varlocal, buf1); 

        tpart = c1->partPointer[j];

				sprintf(buf1, " middle dif ee4 %i %i %s  type : %i ", i,j, tpart->name, c1->isLotOrTbatch[j]);
				write_check(c1->varlocal, buf1); 

        if (c1->isLotOrTbatch[j] == 2 ) {
        // opt lotsizes with tbatch = -1
              o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i< o_start+o_count; i++ )    //gwwd  <=  no     should be <                  
              {toper = c1->get_oper_num(i);
                teq = c1->get_eq_num(toper->eq_ptr);

				sprintf(buf1, " middle dif aa %i %s %s ", i, teq->name, toper->name);
				write_check(c1->varlocal, buf1); 

				time_p_p = get_time_p_p(c1, tpart, toper);

				sprintf(buf1, " middle dif aa2 %i %s %s %f", i, teq->name, toper->name, time_p_p);
				write_check(c1->varlocal, buf1); 

                
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

        if (c1->isLotOrTbatch[j] == 0 ) {
        // opt lotsizes tbatch is assumed fixed.!!!
             o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i< o_start+o_count; i++ )   //gwwd  <=  no     should be <                  
              {toper = c1->get_oper_num(i);
                teq = c1->get_eq_num(toper->eq_ptr);
				sprintf(buf1, " middle dif  bb %i %s %s ", i, teq->name, toper->name);
				write_check(c1->varlocal, buf1); 

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

             if (tpart->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        }

        if (c1->isLotOrTbatch[j] == 1) {
        // opt tbatch sizes !!!
              o_start = tpart->oper_start;
			  o_count = tpart->oper_cnt;
			  i = o_start +3;
				
              for (i= o_start+3; i< o_start+o_count; i++ )                
                {toper = c1->get_oper_num(i);
				
                teq = c1->get_eq_num(toper->eq_ptr);

				sprintf(buf1, " middle dif cc %i %s %s ", i, teq->name, toper->name);
				write_check(c1->varlocal, buf1); 

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

              if (tpart->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        }

		sprintf(buf1, "end of dif  fff %i %f ", i, partial );
    	write_check(c1->varlocal, buf1); 

        dif2[i] = partial;  //  gwwd  error !!!  j is 0 based  use  i  1 based  NOT  I+1  !!!
   } // end i loop

      write_check(c1->varlocal, "exiting dif_ci"); 

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

	v=(float *)malloc((size_t) ((nh-nl+2+NR_END)*sizeof(float)));
	 //  if (!v) nrerror("allocation failure in vector()");
	return v-nl+NR_END;
}


void free_vector(float *v, long nl, long nh)
/* free a float vector allocated with vector() */
{
	free((FREE_ARG) (v+nl-NR_END));
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
		linmin(c1, c1->valueOfParameterAffected,xi,n,fret);  //,func);  //gwwd added c1  using xi
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