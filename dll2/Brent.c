
#define HUGE 
#include "math.h"
#include "nrutil.h"
#define ITMAX 100
#define CGOLD 0.3819660
#define ZEPS 1.0e-10
#define SHFT(a,b,c,d) (a)=(b);(b)=(c);(c)=(d);

float brent(float ax,float bx,float cx, 
			  float (*f)(float), float tol, float *xmin)

{
    int iter;
    float a,b,d,etemp,fu,fv,fw,fx,p,q,r,tol1,tol2,u,v,w,x,xm;
    float e=0.0;
    double fnew, fold;

    a=(ax < cx ? ax : cx);
    b=(ax > cx ? ax : cx);
    x=w=v=bx;
    fw=fv=fx=(*f)(x);
    fold = fx*1.10;                    // gd change
    fnew = fx;                         // gd change
    for (iter=1;iter<=ITMAX;iter++) {
        xm=0.5*(a+b);
        tol2=2.0*(tol1=tol*fabs(x)+ZEPS);            // gd change
        if (fabs((fold-fnew))/fold <= fabs(tol2)) {  // gd change
            *xmin=x;
            return fx;
        }
        if (fabs(e) > tol1) {
            r=(x-w)*(fx-fv);
            q=(x-v)*(fx-fw);
            p=(x-v)*q-(x-w)*r;
            q=2.0*(q-r);
            if (q > 0.0) p = -p;
            q=fabs(q);
            etemp=e;
            e=d;
            if (fabs(p) >= fabs(0.5*q*etemp) || p <= q*(a-x) || p >= q*(b-x))
                d=CGOLD*(e=(x >= xm ? a-x : b-x));
            else {
                d=p/q;
                u=x+d;
                if (u-a < tol2 || b-u < tol2)
                    d=SIGN(tol1,xm-x);
            }
        } else {
            d=CGOLD*(e=(x >= xm ? a-x : b-x));
        }
        u=(fabs(d) >= tol1 ? x+d : x+SIGN(tol1,d));

        if (u < a) u = a;   // gd add
        if (u > b) u = b;   // gd add

        fold = fnew;      // gd add
        fu=(*f)(u);
        fnew = fu;        // gd add
        while (fu < 0) {  // error !!
            u=(fabs(d) >= tol1 ? x+d/2 : x+SIGN(tol1,d));
            fu=(*f)(u);
            fnew = fu;       // gd add
        }

        if (fu <= fx) {
            if (u >= x) a=x; else b=x;
            SHFT(v,w,x,u)
            SHFT(fv,fw,fx,fu)
        } else {
            if (u < x) a=u; else b=u;
            if (fu <= fw || w == x) {
                v=w;
                w=u;
                fv=fw;
                fw=fu;
            } else if (fu <= fv || v == x || v == w) {
                v=u;
                fv=fu;
            }
        }
    }
    //  nrerror("Too many iterations in brent");
	return(-1.0);
}



float brentgd(float ax,float bx,float cx,  
			  float (*f)(float), float tol,float *xmin)

{
    int iter;
	double fu, u;
	double fa, fb, fc;


	fc = (*f) (cx);
	fb = (*f) (bx);
	fa = (*f) (ax);
  
    for (iter=1;iter<=ITMAX;iter++) {
 
		u = (cx + ax) /2.0;

		if ((fabs(u-bx) / fabs(cx - ax)) < .1) {
			if (fabs(cx -bx) > fabs(bx-ax) )
				u = (cx + bx)/2.0;
			else 
				u = (bx + ax)/2.0;
		}
		fu=(*f)(u);


		if ( fabs(cx - bx) < fabs(cx - u)) { 
			//c, b, u, x is the order 
			if (fu < fb) {
				fc = fb; 
				cx = bx;
				fb = fu;
				bx = u;
			}
			else {
				fa = fu;
				ax = u; 
			}
		} // end order
		else { 
			//c, u b, x is the order 
			if (fu < fb) {
				fa = fb; 
				ax = bx;
				fb = fu;
				bx = u;
			}
			else {
				fc = fu;
				cx = u; 
			}

		}

		*xmin = bx;		

		if ((fabs(fc - fa) / fc)  < 0.0001) 
			return(fb);


        
    }
  //  nrerror("Too many iterations in brent");
	return(fb);
}

#undef ITMAX
#undef CGOLD
#undef ZEPS
#undef SHFT