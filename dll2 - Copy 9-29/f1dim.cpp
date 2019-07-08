




#include "class_model.cpp"

#define NRANSI

void free_vector(float *v, long nl, long nh);
 float *vector(long nl, long nh);

#define FALSE 0 
#define TRUE 1

//extern int ncom;
//extern float *pcom,*xicom,(*nrfunc)(float []);

float fvalue_ci  (class_model * c1, float *lot1);
void dif1_ci     (class_model * c1, float * vector, float * diff);

float f1dim(class_model * c1, float x)
{
	int j;
	float f,*xt;

		 write_check(c1->varlocal, " in f1 dim:  calls fvalue ");

	xt=vector(1,c1->ncom);
	for (j=1;j<=c1->ncom;j++) xt[j]=c1->pcom[j]+x*c1->xicom[j];
	f=fvalue_ci(c1, xt);

	 write_check(c1->varlocal, " free in f1 dim ");

	free_vector(xt,1,c1->ncom);  //  retest gwwd  error ?   crashes dll  9-27

	 write_check(c1->varlocal, " exit in f1 dim ");
	return f;
}

#undef NRANSI 
