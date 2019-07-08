



#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"


#define FALSE 0 
#define TRUE 1

float f1dim  (class_model * c1, float imterprot);

float f1dim_dif(class_model * c1, float p1);
//float sizetest (float p1);


//float  mnbrak(float ax,bx,cx,fa,fb,fc,func	float f1dim   (class_model * c1, float *lot1)
//float (*func)(float)*ax,*bx,*cx,*fa,*fb,*fc;




void mnbrak(class_model * c1, float *ax, float *bx, float *cx, float *fa, float *fb, float *fc  ) //, float (*func)(float))
{
        float p1, p2, p3, vp1, dp1, vp2, dp2, vp3, x1;
        int done, sign1, sign2;

		float * vecp1;
		float * vecp2;

		char buf1 [MESSAGE_SIZE];

		boolean wallright;
		boolean wallleft;
		float wallrightval;
		float wallleftval;
		boolean testpastmin;
		float p3a;
		float vp3a;

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

		wallright = wallleft = false;
		wallrightval = wallleftval = 0.0;

        vp2 = -1;  //  
		*bx *= 100.0;
        while (vp2 < 0){
		  *bx /= 10.0;
		   p2= *bx;
		  /*  out 4-23-15 if (wallright == true) {	
			  sprintf(buf1,  " in mnbrak wallright to f1dim 22  %.12f %.12f ", p2, p1);
			  write_check(c1->varlocal, buf1);
			 *bx = (p2+p1)/2; }
		  */

		     sprintf(buf1,  " in mnbrak value to f1dim 22  %.12f %.12f ", p2, p1);
			 write_check(c1->varlocal, buf1);
          vp2= f1dim(c1, p2);
		  /*  out 4-23-15if (vp2 <0) {
				wallright = true;	wallrightval = p2;
		  }*/
        }


        if (vp2 > vp1) {
            // set so we go down hill from ax to bx.
            x1 = vp1; vp1 = vp2; vp2 = x1;
            x1 = p1; p1 = p2; p2 = x1;
			sprintf(buf1,  " in mnbrak switch direction  %.5f %.5f ", p2, p1);
			 write_check(c1->varlocal, buf1);
        }

        p3 = (p2 - p1) * 20  + p2*0.9; 

		testpastmin = false;
		p3a = 0.0;
		vp3a = 0.0;

        while (!done) {
			 sprintf(buf1,  " in mnbrak value to f1dim 33  %.10f ", p3);
			 write_check(c1->varlocal, buf1);
            vp3 = f1dim(c1, p3);

			
						 

            if (vp3 == -1) {
               // too far left move back to right
				wallleft = true;   //  4-23/15 added
				wallleftval = p3;
				
               p3 = (p2+p3)/2;
			   sprintf(buf1,  " in mnbrak value  move back right to f1dim 33  %.5f %.5f %.5f ", p3, p2, wallleftval);
			   write_check(c1->varlocal, buf1);
            }
            else {
                if (vp3 >= vp2) {
                    done = TRUE;
                  }
                  else {
				    if (testpastmin == true) {
							  if ( ((vp3 - vp3a) > 0) && (abs (p3-p2) > abs ( p3a - p2)) ) { 
								 sprintf(buf1,  " in mnbrak value  past min  %f %f %f %f %f ", vp3, vp3a, p3, p3a, p2);
								 write_check(c1->varlocal, buf1);
								 done=true; break;}

							    //vp3 > vp3a  and p3 < p3a<p2  means  p3 is on left side of min 
					}
					
					
                    // not far enough left
                    p1 = p2;
					vp1 = vp2;
                    p2 = p3;
                    vp2 = vp3;
					testpastmin = true; p3a = p3; vp3a=vp3;
					  if (wallleft == true) {   //  4-23/15 added
						  p3 = (wallleftval + p3)/2; // p3->p2 &p2 ->p1
					     sprintf(buf1,  " in mnbrak value  move wall left to f1dim 33  %.10f %.5f  %.5f", p1, p2, p3);
					     write_check(c1->varlocal, buf1);
					  } else {
                         p3 = (p3 - p1) * 10 + p3; // p3->p2 &p2 ->p1  04-24-15
					     sprintf(buf1,  " in mnbrak value  move far left to f1dim 33  %.10f %.5f  %.5f", p1, p2, p3);
					     write_check(c1->varlocal, buf1);
					  }
                  }
            }
			
		
        } // end while

		     sprintf(buf1,  " in mnbrak value  results %.8f %.8f %.8f  \n    value of WIP: %f %f %f ", p1, p2, p3, vp1, vp2, vp3);
			 write_check(c1->varlocal, buf1);
   *ax = p1;
   *bx = p2;
   *cx = p3;
   *fa = vp1;
   *fb = vp2;
   *fc = vp3;
}


