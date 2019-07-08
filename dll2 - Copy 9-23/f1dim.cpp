/* note #undef's at end of file */
#define NRANSI


#include <iostream>
#include <fstream>

#include "class_model.cpp"

extern "C" { 

#include "nrutil.h"

extern int ncom;
extern float *pcom,*xicom,(*nrfunc)(float []);

float f1dim(float x)
{
	int j;
	float f,*xt;

	xt=vector(1,ncom);
	for (j=1;j<=ncom;j++) xt[j]=pcom[j]+x*xicom[j];
	f=(*nrfunc)(xt);
	free_vector(xt,1,ncom);
	return f;
}
} //  end of extern C
#undef NRANSI
