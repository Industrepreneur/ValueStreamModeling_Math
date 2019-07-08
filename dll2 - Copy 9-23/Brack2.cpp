



#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"


#define FALSE 0
#define TRUE 1

//float f1dim_dif(float p1);
//float sizetest (float p1);


//float  mnbrak(float ax,bx,cx,fa,fb,fc,func	float fvalue_ci   (class_model * c1, float *lot1)
//float (*func)(float)*ax,*bx,*cx,*fa,*fb,*fc;

	float fvalue_ci   (class_model * c1, float *lot1);


void mnbrak(class_model * c1, float *ax, float *bx, float *cx, float *fa, float *fb, float *fc  ) //, float (*func)(float))
{
        float p1, p2, p3, vp1, dp1, vp2, dp2, vp3, x1;
        int done, sign1, sign2;

        done = FALSE;

        p1 = *ax;
        vp1 = fvalue_ci(c1, p1);   // ???  1 value not a vector ???
        if (vp1 < 0) {
          *ax = *bx = *cx = p1;
          *fa = *fb = *fc = vp1;
          return;
        }

        vp2 = -1;
		*bx *= 100.0;
        while (vp2 < 0){
		  *bx /= 10.0;
          p2= *bx;
          vp2=(*func)(p2);

        }

  /*      while ((fabs(vp2 - vp1) < 1e-5)  && (fabs(vp2 - vp1) > 1e-25)){
            p2 *= 10.0;
            vp2=(*func)(p2);
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
            vp3 = (*func)(p3);
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
   *ax = p1;
   *bx = p2;
   *cx = p3;
   *fa = vp1;
   *fb = vp2;
   *fc = vp3;
}


