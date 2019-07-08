/* note #undef's at end of file */

#include <iostream>
#include <fstream>

#include "class_model.cpp"

extern "C" { 


#define NRANSI
#include "nrutil.h"
#define TOL 2.0e-4
#include <stdio.h>
#include <stddef.h>
#include <stdlib.h>

#define NR_END 1
#define FREE_ARG char*

int ncom;  // move inside zsub ??

// float *pcom,*xicom,(*nrfunc)(float []);  //   move inside sub ???

void linmin(float p[], float xi[], int n, float *fret, float (*func)(float []))
{
	float brent(float ax, float bx, float cx,
		float (*f)(float), float tol, float *xmin);
	float brentgd(float ax, float bx, float cx,
		float (*f)(float), float tol, float *xmin);
	float f1dim(float x);													 //  gwwd added
	void mnbrak(float *ax, float *bx, float *cx, float *fa, float *fb,       //   gwwd added 
		float *fc, float (*func)(float));
	int j;
	float xx,xmin,fx,fb,fa,bx,ax;

	int ncom;  // move inside zsub ??

   float *pcom,*xicom,(*nrfunc)(float []);  //  moved inside sub ??


	ncom=n;
	pcom=vector(1,n);
	xicom=vector(1,n);
	nrfunc=func;
	for (j=1;j<=n;j++) {
		pcom[j]=p[j];
		xicom[j]=xi[j];
	}
	ax=0.0;
	xx=1.0;
	mnbrak(&ax,&xx,&bx,&fa,&fx,&fb,f1dim);
	//*fret=brent(ax,xx,bx,f1dim,TOL,&xmin);
	*fret=brentgd(ax,xx,bx,f1dim,TOL,&xmin);
	for (j=1;j<=n;j++) {
		xi[j] *= xmin;
		p[j] += xi[j];
	}
	free_vector(xicom,1,n);
	free_vector(pcom,1,n);
}



//   transfered from nrutil.c

void free_vector(float *v, long nl, long nh)
/* free a float vector allocated with vector() */
{
	free((FREE_ARG) (v+nl-NR_END));
}


float *vector(long nl, long nh)
/* allocate a float vector with subscript range v[nl..nh] */
{
	float *v;

	v=(float *)malloc((size_t) ((nh-nl+1+NR_END)*sizeof(float)));
	 //  if (!v) nrerror("allocation failure in vector()");
	return v-nl+NR_END;
}

}  //  end of extern C
#undef TOL
#undef NRANSI
