
/*Microsoft files*/
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// #include <math.h>


#include "class_model.cpp"




/*************************************************/
void  check_ops ( class_model * c1, class_part * tpart)
/*************************************************/
{
        int err, batches;
        class_oper *toper;
        class_lab *lab;
        class_eq *eq;
        char opname [NAME_SIZE];
        float xls, xlr, xes, xer, v1;

		int o_start; 
		int o_count;
		int i;

		char buf1[MESSAGE_SIZE];


        strcpy_s(opname, "DOCK");    //  gwwd code here 
        err=0;

        if (tpart == NULL) { /* NO STEPS OR ROUTES ... */
                return;
		}
    
     

        if (tpart->oper_start == -1) { /* NO STEPS OR ROUTES ... */
                return;
        }

        /* test if lab>eq and eq = 0  */
        if (tpart->tbatch > EPSILON)
             batches = (tpart->lotsiz / tpart->tbatch ) + 0.999;
        else
             batches = 1;

	    o_start = tpart->oper_start;
	    o_count = tpart->oper_cnt;
		i = o_start +3;

		//write_check(c1->varlocal, "in check_ops  start loop ");
				
         for (i= o_start+3; i< o_start+o_count; i++ )                
                {
				toper = c1->get_oper_num(i);
                 eq = c1->get_eq_num(toper->eq_ptr);
                 lab = c1->get_lab_num(eq->lab_ptr);
               


                xls = (toper->lsetup + toper->lsetbatch*batches + toper->lsetpiece*toper->lsize)
                       * lab->facset * eq->facset;
                xlr = (toper->lrlot + toper->lrbatch*batches +  toper->lpiece*toper->lsize)
                       * lab->facrun * eq->facrun;
                xes = (toper->esetup + toper->esetbatch*batches + toper->lsetpiece*toper->lsize) * eq->facset;
                xer = (toper->erlot + toper->erbatch*batches + toper->epiece*toper->lsize) * eq->facrun;

                        if ( ((xlr > EPSILON) && (xer < EPSILON)) ||
                            ((xls > EPSILON) && (xes < EPSILON)) ) {

                                sprintf_s (buf1, "WARNING: #(80)\n  Equipment Time=0.0 and Labor Time>0.0 for an operation  \n Please check operation time data ");
                                warn_err (c1, buf1, 1, lab->name, eq->name, tpart->name, toper->name, " ", " ");
                        } /* if */
                
        } /* for */

        /* test if lab>eq and eq > 0  */
        if (tpart->tbatch > EPSILON)
             batches = (tpart->lotsiz / tpart->tbatch ) + 0.999;
        else
             batches = 1;
        
		o_start = tpart->oper_start;
	    o_count = tpart->oper_cnt;
		i = o_start +3;
				
         for (i= o_start+3; i< o_start+o_count; i++ )                
                {
				toper = c1->get_oper_num(i);
				eq = c1->get_eq_num(toper->eq_ptr);
                lab = c1->get_lab_num(eq->lab_ptr);
               

                xls = (toper->lsetup + toper->lsetbatch*batches + toper->lsetpiece*toper->lsize)
                       * lab->facset * eq->facset;
                xlr = (toper->lrlot + toper->lrbatch*batches +  toper->lpiece*toper->lsize)
                       * lab->facrun * eq->facrun;
                xes = (toper->esetup + toper->esetbatch*batches + toper->lsetpiece*toper->lsize) * eq->facset;
                xer = (toper->erlot + toper->erbatch*batches + toper->epiece*toper->lsize) * eq->facrun;

                        if ((xlr > xer) ||
                            (xls > xes)) {
                                sprintf_s (buf1, "WARNING: #(83)\n Equipment Time is less than Labor Time for an operation. Be sure you want more than 1 person assigned to the operation.");
                                warn_err ( c1, buf1, 1, lab->name, eq->name, tpart->name, toper->name, " ", " ");
                            } // end if
             
        } /* for */


        return;

};


/*************************************************/
void check_eq (class_model * c1,  class_part * tpart)
/*************************************************/
{
        int err, batches;
        class_oper *toper;
        class_eq *eq;
        class_lab *lab;
        char opname [NAME_SIZE];
        float xls, xlr, xes, xer, v1;

		int o_count;
		int o_start;
		int i;

		char buf1[MESSAGE_SIZE];
		char buf2[MESSAGE_SIZE];


        c1->RESTART_CANCEL = FALSE;
        strcpy_s(opname, "DOCK");      //  gwwd code here 
        err=0;

		/*
		sprintf(buf1, "at check_eq start %s ", tpart->name);
		write_check(c1->varlocal, buf1);
		sprintf(buf1, "at check_eq start routes ?  %s %i %i ", tpart->name, tpart->route_start, tpart->route_cnt);
		write_check(c1->varlocal, buf1);
		*/

        if (tpart->route_start == -1) { /* NO STEPS OR ROUTES ... */
                sprintf_s(buf1, "MAJOR ERROR: #201 \n No operations for a product.");
                warn_err ( c1, buf1, 0,  " ",  " ", tpart->name, " ", " ", " ");
                sprintf(buf1, "at check_eq start error no routes ?  %s %i %i ", tpart->name, tpart->route_start, tpart->route_cnt);
		       write_check(c1->varlocal, buf1);
                return;
        }


		// write_check(c1->varlocal, "in check_eq  222");

restart1:
        c1->RESTART_CANCEL = FALSE;

        /* test if Lab.eq and lab-eq ratio > no. of laborers */
        if (tpart->tbatch > EPSILON)
             batches = (tpart->lotsiz / tpart->tbatch ) + 0.999;
        else
             batches = 1;

        o_start = tpart->oper_start;
	    o_count = tpart->oper_cnt;
		i = o_start +3;
				
         for (i= o_start+3; i< o_start+o_count; i++ )                
                {
				toper = c1->get_oper_num(i);
				eq = c1->get_eq_num(toper->eq_ptr);
                lab = c1->get_lab_num(eq->lab_ptr);
                

                xls = (toper->lsetup + toper->lsetbatch*batches + toper->lsetpiece*toper->lsize)
                       * lab->facset * eq->facset;
                xlr = (toper->lrlot + toper->lrbatch*batches +  toper->lpiece*toper->lsize)
                       * lab->facrun * eq->facrun;
                xes = (toper->esetup + toper->esetbatch*batches + toper->lsetpiece*toper->lsize) * eq->facset;
                xer = (toper->erlot + toper->erbatch*batches + toper->epiece*toper->lsize) * eq->facrun;

                if(   (lab->num > 0) &&
                    ( ( (xer>SSEPSILON) && ( (xlr/xer) > lab->num) ) ||
                    ( (xes>SSEPSILON) && ( (xls/xes) > lab->num) )) ) {

                        if ( (xlr/xer) > lab->num) {
                                sprintf_s(buf2, "run time");
                                v1 = xlr/xer;
                        }
                        else                     {
                                sprintf_s(buf2, "setup time");
                                v1 = xls/xes;
                        };

                        sprintf_s(buf1, "WARNING: #(70) \n The ratio of labor to equipment exceeds the available laborers");
                        //Message(buf1);
                        warn_err ( c1, buf1, 1,  lab->name,  eq->name, tpart->name, toper->name, " ", " ");
                } /* if */
                
        } /* while */


        /* test if equip assigned */
		o_start = tpart->oper_start;
	    o_count = tpart->oper_cnt;
		i = o_start +3;
				
         for (i= o_start+3; i< o_start+o_count; i++ )                
                {
				toper = c1->get_oper_num(i);
                eq = c1->get_eq_num(toper->eq_ptr);
                if ( eq ==  NULL) {
                        sprintf_s(buf1, "ERROR: #(42) \n No Equipment assigned for \n Part and Operation ");
                        //Message(buf1);
                        warn_err ( c1, buf1, 0,  " ",  " ", tpart->name, toper->name, " ", " ");
                        return;
                } /* if */
               
        } /* while */


        return;
}





