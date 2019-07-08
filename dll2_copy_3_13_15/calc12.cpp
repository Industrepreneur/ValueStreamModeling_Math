
#include <iostream>
#include <fstream>

#include "class_model.cpp"


#include "nrutil.h"


#define ITMAX 200
#define EPS 1.0e-10

 void write_results (class_model * c1);

 void mpcalc   (class_model * c1);
    int over_u (class_model * c1);



void dif1_ci_2    (class_model * c1, float *lot1, float * dif2) { 

		//  new dif  routine.

		// for each prameter in list. If lotsize ... 

		//for each different part
		//  save old lotsize 
		//  change all op lot sizes by *= (n+1)/n
		//   recalc everything ? 
		//  recalc WIP value

	


  char buf1 [1128];

  class_part * tpart_z;
  class_oper  * toper;
  class_part * tpart;

  double partial;
  double x1, x2;
  double partial_2;
  double partial_3;

  double p0;
  double p1;
  double p2;
  double p3;

    int i, j;

  int i_oper;
  int o_count;
  int o_start;

  mpcalc(c1);

  p0 =0.0;
  	 for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                 p0 += tpart->weight * tpart->qpart;
     }


  for (i = 1; i<=c1->numberOfParameterAffected; i++) {

    partial = 0.0;

		 
	 c1->inOPT = true;

	//gwwd  fix 1, 0 index start !!!  9-25
	j = i-1;

	


        tpart_z = c1->partPointer[j];

			
        if (c1->isLotOrTbatch[j] == 2 ) {

			  x1 = tpart_z->lotsiz;
			  x2 = (x1+1) / x1;
              o_start = tpart_z->oper_start;
			  o_count = tpart_z->oper_cnt;
			  i_oper = o_start +3;
			  
				
              for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )    //gwwd  <=  no     should be <                  
              { toper = c1->get_oper_num(i_oper);
			    toper->lsize *= x2;
			  }

			

              tpart_z->lotsiz +=1;
			  tpart_z->check_rout = false;

			  mpcalc(c1);

		
			  partial = 0;
			  
       
        
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                 partial += tpart->weight * tpart->qpart;
        }

	/*
	//////////////////////////////////////
		tpart_z->check_rout = true;
		mpcalc(c1);
		p1 = 0;
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
                 p1 += tpart->weight * tpart->qpart;
        }
   ///////////////////////////////////////
   */


		//  reset oper lotsizes.
		x1 = tpart_z->lotsiz;
		x2 = (x1-1) / x1;
        o_start = tpart_z->oper_start;
		o_count = tpart_z->oper_cnt;
		i_oper = o_start +3;

		
				
        for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )    //gwwd  <=  no     should be <                  
        { toper = c1->get_oper_num(i_oper);
		toper->lsize *= x2;
		}
        tpart_z->lotsiz -=1;
	
		dif2[i] = p0 - partial;

		sprintf(buf1, "partial short  %s  %f %f ", tpart_z->name, p0, partial);
		write_check(c1->varlocal, buf1);

	
		//-----------------------------------------------------------------------------------

		} //  end if type = 2 
		
  }//  parameter list end 


}
