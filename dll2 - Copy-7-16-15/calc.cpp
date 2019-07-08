
/* changed  5/2/91   GWWD   removed storing base case ... as single line on screen
                             added "Working" instead
*/


#include <stdio.h>
#include <string.h>
#include <stdlib.h>


#include "class_model.cpp"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define EPSILON       1e-6     /* a small number */
#define SSEPSILON     1e-20    /* a VERY small number */
#define ROUT_ERR      0.1      /* size for ROUTING and ASSIGN ERRORs */

#define MAX(a,b)    ( (a>b) ? a : b  )
#define MIN(a,b)    ( (a<b) ? a : b  )
#define  BUFLEN    100
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 4000


void Message (class_model * c1, char *mess) { 
	write_check(c1->varlocal, mess);
	return;
}

void xxMessage( char * message) {
	ofstream messagefile;
	messagefile.open("dllmessages.log", std::ios_base::app);
	messagefile << message << "\n";
	messagefile.flush();
	messagefile.close();
} ;

void xxMessage2(const char * message) {
	ofstream messagefile;
	messagefile.open("dllmessages.log", std::ios_base::app);
	messagefile << message << "\n";
	messagefile.flush();
	messagefile.close();
} ;


    void    do_all_visits (class_model * c1);
    //void    check_ibom (class_model * c1);
    void  set_demand (class_model * c1);
	int  mpc(class_model * c1);

	 void write_results (class_model * c1);

/* ---------------------------------------------- */
 void   mpcalc ( class_model * c1 )
/* ---------------------------------------------- */
{
	   
	class_lab * tlabor;
	class_eq * teq;
	class_part * tpart;
	class_oper * toper;
	class_route * troute;
	class_ibom * tibom;

	int i;

	int o_start;
	int o_count;

	int r_start;
	int r_count;

	int i_start;
	int i_count;

        int r, warn;
        int todo;
        char buf1[1256];

        //fstream logfile ("err_calc.log");
		//logfile << "calc begin\n";
		//logfile.flush();

        sprintf_s(buf1, "at mpcc0 1");
        write_check(c1->varlocal, buf1);

        

        if (c1->first_part_ptr() == NULL) {
                write_check(c1->varlocal, "Cannot Calculate anything.\n No Parts have been added yet.");
				//logfile << "no parts\n";
				//logfile.flush();
                return;
        };



		c1->CALC_CANCEL = false;
		c1->over_util_L = false;
		c1->over_util_E = false;
        

        //sprintf_s(buf1, "at mpcc0 2");
        //Message( c1,buf1);
		//logfile << "before do_all_visits\n";
		//logfile.flush();

		//write_check(c1->varlocal, "  gwwd test test at start do_all _visits");

	

        do_all_visits (c1);
		//logfile << "after do_all_visits\n";
		//logfile.flush();
			//write_check(c1->varlocal, "  gwwd test 2 after do_all _visits");
		
		//logfile << "CALC_CANCEL: " << c1->CALC_CANCEL << "\n";
		//logfile.flush();
		if (c1->CALC_CANCEL == true)  { write_check(c1->varlocal, "error before ...set demand start"); return; }


      //  check_ibom (c1);
      //  if (c1->CALC_CANCEL == true) return;
             

		 write_check(c1->varlocal, "set demand start");

        set_demand (c1);
		//logfile << "after set_demand\n";
		//logfile.flush();
		 if (c1->CALC_CANCEL == true) return;

		 write_check(c1->varlocal, " mpc start");

        r = mpc(c1);

		write_check(c1->varlocal, " mpc done");
		//return; //  www  gwwd 5-8-15 check 

		//logfile << "after mpc\n";
		//logfile.flush();
        if (c1->CALC_CANCEL == true) return;
        



		write_check(c1->varlocal, "do local mpcalc start");
     

        /* ************************************************************** */

         for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr()) { 
    
                if (tlabor->num > 0)
                {
                        tlabor->uset  *= 100.0;
                        tlabor->urun  *= 100.0;
                        tlabor->ul    *= 100.0;
                }
                else
                    {
                        tlabor->uset    *= 100.0;
                        tlabor->urun    *= 100.0;
                        tlabor->ul      *= 100.0;
                }
              
        }  //  end loop
		 //logfile << "after u labor loop\n";
		//logfile.flush();

          for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) { 
       
                if (teq->num > 0)
                {
                        teq->uset    *= 100.0;
                        teq->urun    *= 100.0;
                        teq->uwait   *= 100.0;
                        teq->udown   *= 100.0;
                        teq->u       *= 100.0;
                }  /* end of if */
                else
                {
                        teq->uset    = 0.0;
                        teq->urun    = 0.0;
                        teq->uwait   = 0.0;
                        teq->udown *= 100.0;
                        teq->u     *= 100.0;
                } /* end of else */
              
        } /* end of teq loop */
		//logfile << "after u eq loop\n";

		  write_check(c1->varlocal, "end mpcalc  util !!");

		//logfile.flush();
           for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {
                if (tpart->total < EPSILON ) tpart->tsgood=0.0;
                
        } 
		//logfile << "after part epsilon loop\n";
		//logfile.flush();
		//MessageBox( NULL, buf1, "after 0", MB_OK | MB_TASKMODAL );

        /* ************************************************************** */
        /* ************************************************************** */

        if (((c1->over_util_L != TRUE) && (c1->over_util_E != TRUE)) && (c1->utlimit > 95.0)) {
            // check if any util > 95.0
             warn = FALSE;
            	 for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr()) {
                 if ((tlabor->num > 0) && (tlabor->ul > 95.0)) warn = TRUE;
             }

              for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())  {
                 if ((teq->num > 0) && (teq->u > 95.0)) warn = TRUE;
             }
             if ((warn == TRUE)){
                 Message( c1," Value Stream Modeling EVALUATION IS COMPLETED\n However the resouce utilization is very high and the Manufacturing Cycle Time numbers are not as accurate.");
             }
        }

    

        if ((c1->over_util_L != TRUE) && (c1->over_util_E != TRUE)) {
                strcpy(buf1," Calculating Scrap Rates.\n Checking IBOM for Consistency.\n Calculating Production Requirements for Components.\n Calculating Labor Utilization.\n Calculating Equipment Utilization.\n");
                strcat(buf1," Calculating Operation WIP and Manufacturing Cycle Time.\n Storing Base Case results...");
                //Message( c1,buf1);
        }
        else {
                ;
        }


        strcpy(buf1, "");
        if (c1->over_util_L == TRUE)      strcat (buf1, "Warning: Production Level is NOT SUSTAINABLE. \nLabor resources will be overutilized. \nManufacturing Cycle Time and WIP cannot be calculated.\n");
        else if (c1->over_util_E == TRUE) strcat (buf1, "Warning: Production Level is NOT SUSTAINABLE. \nEquipment resources will be overutilized. \nManufacturing Cycle Time and WIP cannot be calculated.\n");
        else                          strcat (buf1, "Required Production Level is achievable.\n Estimates of Manufacturing Cycle Time and WIP have been calculated.\n");



        c1->total_pro = 0;
        c1->total_shi = 0;
        c1->total_scr = 0;
        c1->total_wip = 0;
        c1->total_ft  = 0;

		  write_check(c1->varlocal, "mpcalc totaling mct calc !!");

		for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
	 
	      c1->total_pro += tpart->ndmnd * tpart->facdem +
                    tpart->demd_ass +
                    tpart->demd_scrap;                /* total # of pieces made */
                c1->total_shi += tpart->ndmnd * tpart->facdem;  /* total # of pieces shipped */
                c1->total_scr += tpart->scrapped;
                /* total # scrapped in production */
                c1->total_wip +=  tpart->qpart;
                c1->total_ft  +=  tpart->tsgood * ( tpart->ndmnd * tpart->facdem + tpart->demd_ass + tpart->demd_scrap);
                
        }
        if (c1->total_pro > EPSILON) c1->total_ft  /= c1->total_pro;
        if (c1->total_wip < 0) c1->total_wip = 0;

	 

	/*  ????
        if (c1->over_util_L == TRUE) todo = mLABOR;
        else if (c1->over_util_E == TRUE) todo = mEQUIP;
        else todo = mUNLOCK;
		*/


        // if ((over_util_L != TRUE) && (over_util_E != TRUE))
        //        Message ("          MPX EVALUATION IS COMPLETED\n             PRODUCTION WAS ACHIEVED \n\n  Please use REPORTS and GRAPHS Menus to \n  examine the manufacturing system performance");

		write_check(c1->varlocal, "end mpcalc");

        return;
}
