
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

		class_part ** partPointer;

        // lot1[]  offset .. starts at 1
		
		
		// saving initial TBATCH SIZES ...
			for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
	         	tpart->tbatch = tpart->init_tb;
			}


        for (i = 1 ;i<= c1->numberOfParameterAffected; i++)
        {
                j = i - 1;
                if (c1->isLotOrTbatch[j] ==0) {
                        partPointer[j]->lotsiz = lot1[i];
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
                }
                else if (c1->isLotOrTbatch[j] == 1) {
                        partPointer[j]->tbatch = MIN(lot1[i], partPointer[j]->lotsiz);
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
						if (lot1[i] > partPointer[j]->lotsiz) return (-1.0);
                }
				else if (c1->isLotOrTbatch[j] ==2) {
						partPointer[j]->tbatch = -1;
                        partPointer[j]->lotsiz = lot1[i];
						partPointer[j]->check_rout = TRUE;
                        if (lot1[i] < 0.0) return (-1.0);
                }

                partPointer[j]->check_rout = TRUE;
        }

        c1->INRUN = -1;
        c1->RESTART_CANCEL = FALSE;

        do_all_visits(c1);

        ok = TRUE;
        c1->FULL = TRUE;

        mpcalc(c1);


        if (ok == FALSE)
        {
                //Message("Internal error, calculation stopped");
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



  for (i = 0; i<c1->numberOfParameterAffected; i++) {

    partial = 0.0;

        tpart = c1->partPointer[i];

        if (c1->isLotOrTbatch[i] == 2 ) {
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

        if (c1->isLotOrTbatch[i] == 0 ) {
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

             if (tpart->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        }

        if (c1->isLotOrTbatch[i] == 1) {
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

              if (tpart->lotsiz <(1.0 + EPSILON))
                 partial = MIN(0.0, partial);
        }


        dif2[i+1] = partial;
   } // end i loop



return;
}


#include <math.h>
#define NRANSI


////////////////////////////////////////////////////////////////////////////////////////////////////


	
#define NR_END 1
#define FREE_ARG char*


//void free_vector(float *v, long nl, long nh);
// float *vector(long nl, long nh);

float *vector(long nl, long nh)
/* allocate a float vector with subscript range v[nl..nh] */
{
	float *v;

	v=(float *)malloc((size_t) ((nh-nl+1+NR_END)*sizeof(float)));
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

  //   moved to class_model  int ncom;  // move inside zsub ??

   //   moved to class_model float *pcom,*xicom;  //(*nrfunc)(float []);  //  moved inside sub ??  to class_model

   //float brent(float ax, float bx, float cx, float (*f)(float), float tol, float *xmin);
	//float brentgd(float ax, float bx, float cx,	float (*f)(float), float tol, float *xmin);
	//float brent(float ax, float bx, float cx, float tol, float *xmin);
	//float brentgd(float ax, float bx, float cx,	float tol, float *xmin);
	//float f1dim(float x);													 //  gwwd added
	 void mnbrak(class_model * c1, float *ax, float *bx, float *cx, float *fa, float *fb,       //   gwwd added 
		float *fc);  // removed , float (*func)(float));

void linmin(class_model * c1, float p[], float xi[], int n, float *fret) //, float (*func)(float []))
{
	
	int j;
	float xx,xmin,fx,fb,fa,bx,ax;



	c1->ncom=n;
	c1->pcom=vector(1,n);
	c1->xicom=vector(1,n);
	// nrfunc=func;
	for (j=1;j<=n;j++) {
		c1->pcom[j]=p[j];
		c1->xicom[j]=xi[j];
	}
	ax=0.0;
	xx=1.0;
	mnbrak(c1, &ax,&xx,&bx,&fa,&fx,&fb); //  ,f1dim);
	//*fret=brent(ax,xx,bx,f1dim,tolerance,&xmin);
	 *fret=brentgd(c1, ax,xx,bx,c1->tolerance,&xmin);  //  brentgd(ax,xx,bx,f1dim,tolerance,&xmin);
	for (j=1;j<=n;j++) {
		xi[j] *= xmin;
		p[j] += xi[j];
	}
	free_vector(c1->xicom,1,n);
	free_vector(c1->pcom,1,n);
}





void frprmn(class_model * c1, float p[], int n,  float *fret) 
	//float (*func)(int * ci, float []), void (*dfunc)(int * ci, float [], float []))
{
	
	int j,its;
	float gg,gam,fp,dgg;
	float *g,*h,*xi;

	g=vector(1,n);
	h=vector(1,n);
	xi=vector(1,n);

	fp =  fvalue_ci(c1,  c1->valueOfParameterAffected);  //gwwd added call  to func value 
	dif1_ci(c1, c1->valueOfParameterAffected, xi);

	for (j=1;j<=n;j++) {
		g[j] = -xi[j];
		xi[j]=h[j]=g[j];
	}
	for (its=1;its<=ITMAX;its++) {
		c1->iter=its;                                     //  gwwd change iter is class local...
		linmin(c1, c1->valueOfParameterAffected,xi,n,fret);  //,func);  //gwwd added c1
		if (2.0*fabs(*fret-fp) <= c1->tolerance*(fabs(*fret)+fabs(fp)+EPS)) {
			FREEALL
			return;
		}
		fp= *fret;
		dif1_ci(c1, c1->valueOfParameterAffected,xi);   //gwwd added c1
		dgg=gg=0.0;
		for (j=1;j<=n;j++) {
			gg += g[j]*g[j];
			dgg += (xi[j]+g[j])*xi[j];
		}
		if (gg == 0.0) {
			FREEALL
			return;
		}
		gam=dgg/gg;
		for (j=1;j<=n;j++) {
			g[j] = -xi[j];
			xi[j]=h[j]=g[j]+gam*h[j];
		}
	}
	//nrerror("Too many iterations in frprmn");
}




	
 
#undef ITMAX
#undef EPS
#undef FREEALL
#undef NRANSI 