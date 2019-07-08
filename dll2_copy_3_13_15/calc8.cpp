#include <stdio.h>
#include "class_model.cpp" 



#define ABS(a)   ((a) < 0.0) ? (-1*(a)) : (a))
#define EXP(a)    exp((double) (a))
#define SQRT(a)   sqrt((double) (a))
#define POW(a,b)  pow((double) (a), (double) (b))

#define ECBOUND   70.0


float erfcc(float x) {
    double t, z, ans;

       z = fabs(x);
       t = 1.0/(1.0 + 0.5 * z);
       ans = t * exp(-z * z - 1.26551223 + t *(1.00002368+t*(0.37409196+ t * (0.09678418+
             t*(-0.18628806+t*(0.27886807+t*(-1.13520398+ t * (1.48851587 +
             t * ( -0.82215223 + t* 0.17087277))) ))) )));


    return ( (x >= 0.0) ? ans : 2.0 - ans);
}

float cdf ( float z){
  return (1.0 - 0.5*erfcc(0.7071678*z));
}

double erlangC (double rho, double m)
{
  double numerator, denom, temp, mrho;
  int    i, k, mm;

   mrho = m*rho;
   mm = max( ((int) m), 1);  //  modified 4/5/04
   temp = 1.0;

   for (i=1; i<=mm; i++)
     temp  *= mrho / (double) i;

   numerator = temp / (1.0-rho);
   denom = 1.0;
   for (k=1; k<mm; k++){
     temp = 1.0;
     for (i=1; i<=k; i++)
        temp *= mrho / (double) i;
     denom += temp;
   }
   denom += numerator;
   return (numerator/denom);
}

double halfinwhitt( double rho, double m)
{
   double waitbeta;

   waitbeta = (1.0 - rho) * SQRT(m);
   return(1.0 / (1. + 2.5066 * waitbeta * cdf(waitbeta) *
       EXP(0.5 * waitbeta * waitbeta)));
}


double ggc (class_model* c1,  class_lab * tlabor)
{
  double probwait_m, meanwait_m, rho, ca_sq, cs_sq, c_sq, phi1, phi2;
  double phi3, phi4, phi, gamma, meanwait, pi1, pi2, pi3, pi4, pi5, pi6;
  double pi, z, probwait, xi, dscube, cd_sq, cw_sq, num, ret;

  rho= tlabor->ul;            // using different source
  num = tlabor->num_av;       // using num_av 4/5/0 
                              // using short name
  if (num<1) {
	rho = rho * num;
	num = 1;
  }

  ca_sq = tlabor->ca2;
  cs_sq = tlabor->cs2;

  if ((tlabor->xbarbar < EPSILON) || (rho <EPSILON)) {
        tlabor->ct2 = 0.0;
        ret = 0.0;
        return(ret);
  }
             

  if (num <=ECBOUND)
    probwait_m = erlangC(rho, num);
  else
    probwait_m = halfinwhitt(rho, num);

   meanwait_m = probwait_m * tlabor->xbarbar / (tlabor->num_av*(1 - rho));

   gamma = MIN(0.24, (1.0-rho)*(num-1) *
           (SQRT(4.0+5.0*num) - 2.0) / (16.0*num * rho));

   phi1 = 1.0 + gamma;
   phi2 = 1.0 - 4.0*gamma;
   phi3 = phi2 * EXP(-2.0*(1.0-rho)/(3.0*rho));
   phi4 = MIN(1.0, 0.5*(phi1+phi3));

   c_sq = 0.5 *(ca_sq + cs_sq);
   if (c_sq >= 1.0) xi = 1.0;
   else xi = POW(phi4, 2.0*(1.0-c_sq));

   if (ca_sq >=cs_sq)
       phi = 4.0*(ca_sq-cs_sq)*phi1/ (4.0*ca_sq - 3.0*cs_sq) +
           cs_sq * xi / (4.0*ca_sq - 3.0*cs_sq);
   else
       phi = (cs_sq-ca_sq)*phi3*.5 / (ca_sq+cs_sq) +
           (cs_sq+3.0*ca_sq) * xi* 0.5 / (ca_sq+cs_sq);

   meanwait = phi * c_sq * meanwait_m;

   z= (ca_sq + cs_sq) / ( 1.0 + cs_sq);
   gamma = (num - num*rho - .5) /SQRT(num * rho * z);
   pi6 = 1.0 - cdf(gamma);
   pi5 = MIN(1.0, (1.0-cdf(2.0*(1.0-rho)*SQRT(num)/ (1.0+ca_sq))) *
        probwait_m / (1.0-cdf((1.0-rho)*SQRT(num))));
   pi4 = MIN(1.0, (1.0-cdf((1.0+cs_sq)*(1.0-rho)*SQRT(num)/
      (ca_sq + cs_sq))) * probwait_m / (1.0-cdf((1.0-rho) * SQRT(num))));
   pi1 = rho*rho*pi4+(1.0-rho*rho) * pi5;
   pi2 = ca_sq * pi1 +(1.0-ca_sq)*pi6;
   pi3 = 2.0*(1.0-ca_sq)*(gamma-0.5) * pi2 + (1.0-2.0*(1.0-ca_sq)*(gamma -
          0.5))*pi1;

   if ((num <7) || (gamma<=0.5) || (ca_sq>=1.0))
     pi = pi1;
   else if ((num >= 7) && (gamma>= 1.0) && (ca_sq<1.0))
     pi = pi2;
   else if ((num >=7) && (ca_sq<1.0) &&(0.5<gamma) && (gamma <1.0))
     pi = pi3;
   else
        pi = pi1;
 

   probwait = MIN(pi, 1.0);
   if (probwait >EPSILON) {
     if (cs_sq>= 1)
        dscube = 3.00 * cs_sq * ( 1.0+cs_sq);
     else
        dscube = (2.0*cs_sq+1.0) * ( cs_sq+1.0);
     cd_sq = 2.0* rho - 1.0 + 4.0*(1.0-rho) * dscube/ (3.0*(cs_sq+1.0)*
           (cs_sq+1.0));
     cw_sq = (cd_sq + 1.0 - probwait)/probwait;
   }
   else
     cw_sq = 0.0;

   tlabor->ct2  = SQRT( tlabor->cs2*tlabor->xbarbar*tlabor->xbarbar + cw_sq*meanwait*meanwait )/
                        (tlabor->xbarbar+meanwait);


   ret = meanwait / tlabor->xbarbar;
        
   return(ret);
}


