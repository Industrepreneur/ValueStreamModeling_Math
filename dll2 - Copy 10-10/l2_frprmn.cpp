#include <math.h>
#define NRANSI


#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"

#define ITMAX 200
#define EPS 1.0e-10


#define FREEALL free_vector(xi,1,n);free_vector(h,1,n);free_vector(g,1,n);



void linmin( float xi[], int n, float *fret,
	float (*func)(int *, float [])); 

void frprmn( float p[], int n, float ftol, int *iter, float *fret,
	float (*func)(int * ci, float []), void (*dfunc)(int * ci, float [], float []))
{
	
	int j,its;
	float gg,gam,fp,dgg;
	float *g,*h,*xi;

	g=vector(1,n);
	h=vector(1,n);
	xi=vector(1,n);

	fp=(*func)( valueOfParameterAffected);  //gwwd added c1
	(*dfunc)(valueOfParameterAffectedxi);

	for (j=1;j<=n;j++) {
		g[j] = -xi[j];
		xi[j]=h[j]=g[j];
	}
	for (its=1;its<=ITMAX;its++) {
		*iter=its;
		linmin(valueOfParameterAffected,xi,n,fret,func);  //gwwd added c1
		if (2.0*fabs(*fret-fp) <= ftol*(fabs(*fret)+fabs(fp)+EPS)) {
			FREEALL
			return;
		}
		fp= *fret;
		(*dfunc)(valueOfParameterAffected,xi);   //gwwd added c1
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

