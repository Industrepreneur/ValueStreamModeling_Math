



#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"


#define FALSE 0 
#define TRUE 1

float f1dim  (class_model * c1, float imterprot);




//float  mnbrak(float ax,bx,cx,fa,fb,fc,func	float f1dim   (class_model * c1, float *lot1)
//float (*func)(float)*ax,*bx,*cx,*fa,*fb,*fc;




void mnbrak(class_model * c1, float *ax, float *bx, float *cx, float *fa, float *fb, float *fc  ) //, float (*func)(float))
{
        float p1, p2, p3, vp1, dp1, vp2, dp2, vp3, x1;
        int done, sign1, sign2;

		float * vecp1;
		float * vecp2;

		char buf1 [MESSAGE_SIZE];

        done = FALSE;

		p1 = *ax;

			 sprintf(buf1,  " in mnbrak value to f1dim ? %.10f ", p1);
			 write_check(c1->varlocal, buf1);

            vp1 = f1dim(c1, p1);   // ???  1 value not a vector ???
        if (vp1 < 0) {
          *ax = *bx = *cx = p1;
          *fa = *fb = *fc = vp1;
		  write_check(c1->varlocal, "exiting mnbrak - step 1 wip value <0 ???");
          return;
        }

        vp2 = -1;
		*bx *= 100.0;
        while (vp2 < 0){
		  *bx /= 10.0;
          p2= *bx;
		     sprintf(buf1,  " in mnbrak value to f1dim 22  %.12f ", p2);
			 write_check(c1->varlocal, buf1);
          vp2= f1dim(c1, p2);

        }

  /*      while ((fabs(vp2 - vp1) < 1e-5)  && (fabs(vp2 - vp1) > 1e-25)){
            p2 *= 10.0;
            vp2= f1dim(c1, p2);
            if (vp2<0){
                *ax = *bx = *cx = p1;
                *fa = *fb = *fc = vp1;
                return;
            }
        }
  */
        if (vp2 > vp1) {
            // set so we go down hill from ax to bx.
            x1 = vp1; vp1 = vp2; vp2 = x1;
            x1 = p1; p1 = p2; p2 = x1;
        }

        p3 = (p2 - p1) * 10  + p2;

        while (!done) {
			 sprintf(buf1,  " in mnbrak value to f1dim 33  %.10f ", p3);
			 write_check(c1->varlocal, buf1);
            vp3 = f1dim(c1, p3);
            if (vp3 == -1) {
               // too far right move back to left
               p3 = (p2+p3)/2;
            }
            else {
                if (vp3 >= vp2) {
                    done = TRUE;
                  }
                  else {
                    // not far enough right
                    //p1 = p2;
					//vp1 = vp2;
                    //p2 = p3;
                    //vp2 = vp3;
                    p3 = (p3 - p2) * 10 + p3; // p3->p2 &p2 ->p1
                  }
            }
        } // end while

		     sprintf(buf1,  " in mnbrak value  results %.8f %.8f %.8f  :value of WIP: %f %f %f ", p1, p2, p3, vp1, vp2, vp3);
			 write_check(c1->varlocal, buf1);
   *ax = p1;
   *bx = p2;
   *cx = p3;
   *fa = vp1;
   *fb = vp2;
   *fc = vp3;
}


