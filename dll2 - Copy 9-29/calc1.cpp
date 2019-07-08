
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
#define  MESSAGE_SIZE 400

#define EQ_LIMIT 6000

#define  LABOR_T   1
#define  EQUIP_T   2
#define  LABOR_T_GATHER_3	6	
#define  EQUIP_T_GATHER_3	7


#define T_BATCH_TOTAL_LABOR  13
#define T_BATCH_TOTAL_EQUIP  14

#define T_BATCH_PIECE   23
#define T_BATCH_WAIT_LOT  24

#define LABOR_T_GATHER_1  27

#define EQUIP_T_GATHER_1  37
#define EQUIP_T_GATHER_2  38

#define LABOR_1P	47
#define EQUIP_1P	48
#define LABOR_1_TB	57
#define	EQUIP_1_TB	58

#define LABOR_IJK	67
#define EQUIP_IJK	68

void Message( char * );

/* local prototypes */
void   set_xbar_cs(class_model * c1);
int  set_cacalc(class_model * c1);
void  lextra(class_model * c1);


float  get_gather (class_model * c1,  class_part * tpart);
void   do_gather  (class_model * c1,  class_part * tpart, class_oper  * frm_oper, float gathn, float prob);

double effabs (class_lab * tlabor);
void   calc_op ( class_model * c1, class_part *tpart, class_oper *  toper, float * v1, float * xbarsl, float *xbarrl, int abc);
float calc_xprime( class_model * c1, class_part * tpart, class_oper * toper, float x1,  float x2);

int  linex =0;

void  getx ( class_eq * teq);

/* ############################################################ */
int   mpc    ( class_model * c1 )
/* ############################################################ */
{
        /*
         --this is the calculation routine for manuplan
        */
        class_lab * tlabor;
        class_eq * teq;
        class_part  * tpart;
        class_oper  * toper;

		int o_count;
		int o_start;
		int i;

		char buf1[MESSAGE_SIZE];
		char buf2[MESSAGE_SIZE];

        int    ret;

        float  v1, queopr, x1, xprime, rpv, u1;
        float  x2, x_t_1, xtrans;
        float  xbarr, xbars, xbarrl, xbarsl;
        float  xbarr_t,  xbars_t,  xbarr_t1, xbars_t1;
		float  xbarr_t2, xbars_t2;
        float  xbarrl_t, xbarsl_t;
        float  xbar1, xbar2, no_batchs;

        float tbat_tot;

        float vlam1, xm_only, xl_only, v_visit, xprsqr, xbarbar, xbard, xsigbar, cs2;



        /* count lab, eq, part ? and get temp arrays ? */
        /* imttf, xbarbar, xbard, smbar, sigbar ? for eq, dlam for part ? */
        /*zz*/


        //write_check( c1->varlocal, " Calculating Scrap Rates.\n Checking IBOM for Consistency.\n Calculating Production Requirements for Components.\n Calculating Labor Utilization ...\n\n");

		int lcnt;
        lcnt = 0;

        for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr())
        {
                tlabor->index   = lcnt;
                tlabor->ql      = 0.0;
                tlabor->qwl     = 0.0;
                tlabor->qpl     = 0.0;
                tlabor->uset    = 0.0;
                tlabor->urun    = 0.0;
                tlabor->ul      = 0.0;

                tlabor->fac_eq_lab = 0.0;
                tlabor->num_av = 0.0;
                tlabor->cs2 = 0.0;
                tlabor->xbarbar = 0.0;
                tlabor->xbard = 0.0;
                tlabor->xsigbar = 0.0;
                tlabor->eq_cover = 0.0;
				tlabor->max_eq_ot = -100.0;
                if (tlabor->num > 0)
                        tlabor->num_av = tlabor->num;
                else
                    tlabor->num_av= -1.0;
               
                lcnt +=1;
        }


restart:
        // sprintf_s(buf1, "at mpcc1f 1");
        // write_check( c1->varlocal, buf1);


		int ecnt;
        ecnt = 0;
        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                teq->index   = ecnt;
                while  (teq->mttf < EPSILON) {
                        sprintf_s(buf1, "ERROR: #(41) \n Mean Time To Failure \n for equipment \n is too small. Please change value\n");
                        write_check( c1->varlocal, buf1);
                        warn_err (c1, buf1, 0,  " ", teq->name, " ", " ", " ", " ");
						c1->CALC_CANCEL = TRUE;
                        return(FALSE);
                };
                if  (teq->mttf < EPSILON) teq->mttf = 1.0;
                if  (teq->mttr < 0.0    ) teq->mttr = 0.0;
                teq->imttf   = 1.0 / teq->mttf;
                teq->xbarbar = 0.0;
                teq->xbard   = 0.0;
                teq->xsigbar = 0.0;
                teq->smbar   = 0.0;
                teq->smbard  = 0.0;
                teq->tpm     = 0.0;
                teq->q       = 0.0;
                teq->qw      = 0.0;
                teq->qp      = 0.0;
                teq->uset    = 0.0;
                teq->urun    = 0.0;
                teq->uwait   = 0.0;
				teq->num_av  = 0.0;  /* ave num in play */
                teq->udown = 0.0;
                teq->u     = 0.0;
                teq->wait = 0.0;
                teq->ca2 = 0.0;
                teq->cs2 = 0.0;
                teq->A = 0.0;
                teq->C0 = 0.0;
                teq->C2lam0 = 0.0;
                teq->lam0 = 0.0;
                teq->lam = 0.0;
                teq->nV = 0.0;
                teq->nW = 0.0;
                teq->oV = 0.0;
                teq->P0 = 0.0;
                teq->X = 0.0;
                if (teq->num > 0)
                        teq->udown  = 0.0;
                else
                {
                        x1 = teq->mttr;
                        teq->udown = x1 / (teq->mttf+x1);
                }

                tlabor  = c1->get_lab_num(teq->lab_ptr);
				tlabor->max_eq_ot = MAX( tlabor->max_eq_ot, teq->facovt);
                if ((teq->num > 0) && (tlabor->eq_cover >= 0.0))
                    tlabor->eq_cover += teq->num * (teq->facovt +100);
                else
                    tlabor->eq_cover = -1;
      
                ecnt +=1;
        } //  end teq loop


        if (ecnt > EQ_LIMIT) {
                sprintf_s(buf1, "ERROR: #(7)\n Number of types of equipment is %d and exceeds limit of %d \n Some internal routing equations cannot be solved due to lack of memory", ecnt, EQ_LIMIT);
                write_check( c1->varlocal, buf1);
                warn_err ( c1, buf1, 0,  " ", " ", " ", " ", " ", " ");
                return(FALSE);
        }


		// sprintf_s(buf1, "at mpcc1f 1a");
        // write_check( c1->varlocal, buf1);

        /* ************************************************************** */


        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {
                tpart->tsgood = 0.0;
                tpart->tsvar  = 0.0;
                tpart->w_labor = 0.0;
                tpart->w_equip = 0.0;
                tpart->w_setup = 0.0;
                tpart->w_run   = 0.0;
                tpart->w_lot   = 0.0;
                tpart->qpart  = 0.0;
                tpart->wip_lots = 0.0;
                tpart->tgather   = 0.0;
                tpart->t_w_asm  = 0.0;
                tpart->total    = (tpart->ndmnd * tpart->facdem) + tpart->demd_ass + tpart->demd_scrap;
                tpart->shipped  = tpart->ndmnd * tpart->facdem;
                if (tpart->scrap < SSEPSILON) tpart->scrapped = 0.0;
                else  tpart->scrapped = tpart->total * tpart->scrap;


                tpart->dlam = ( tpart->ndmnd * tpart->facdem + tpart->demd_ass + tpart->demd_scrap ) * (1.0 + tpart->scrap)
                        / ( tpart->lotsiz  * c1->t1  * c1->t2);

				char s0 [MESSAGE_SIZE];  //  gwwd added 9-18
				// sprintf(s0, "dlam %f  %f %f %f %f %f %f  %f %f", tpart->dlam,  tpart->ndmnd, tpart->facdem,  tpart->demd_ass, tpart->demd_scrap, tpart->scrap, tpart->lotsiz , c1->t1, c1->t2);
				// write_check(c1->varlocal, s0);
				
               o_start = tpart->oper_start;
			   o_count = tpart->oper_cnt;
			   i = o_start +3;
				
                for (i= o_start+3; i< o_start+o_count; i++ )                
                {
					toper = c1->get_oper_num(i);
                        toper->qpoper = 0.0;
                        toper->ueset = 0.0;
                        toper->uerun = 0.0;
                        toper->ulset = 0.0;
                        toper->ulrun = 0.0;
                        toper->flowtime = 0.0;
                        toper->n_setups = 0.0;
                        toper->w_run     = 0.0;
                        toper->w_setup   = 0.0;
                        toper->w_lot     = 0.0;
                        toper->w_equip   = 0.0;
                        toper->w_labor   = 0.0;

                        teq = c1->get_eq_num(toper->eq_ptr);
                        tlabor = c1->get_lab_num( teq->lab_ptr);

                        calc_op (c1,  tpart, toper, &v1, &xbarsl, &xbarrl, LABOR_T);
                        v1     *= MIN(1.0, toper->lsize);
                        xbarrl *= MAX(1.0, toper->lsize);
                        xbar1 = (xbarsl + xbarrl) * (1.0+tlabor->facovt/100);   /* at least 1 setup and 1or lotsize run  otfav 5/1/04*/
                        toper->ulset = v1 * xbarsl * 100.0;  /* lotsiz<1 implies change in V1 or LAM */
                        toper->ulrun = v1 * xbarrl * 100.0;

                        toper->n_setups = v1 * c1->t1*c1->t2;

                        calc_op (c1,  tpart, toper, &v1, &xbars, &xbarr, EQUIP_T);
                        v1    *= MIN(1.0, toper->lsize);
                        xbarr *= MAX(1.0, toper->lsize);
                        xbar2 = xbars + xbarr;
                        toper->ueset = v1 * xbars * 100.0;
                        toper->uerun = v1 * xbarr * 100.0;

                        tlabor->uset += v1 * xbarsl;
                        tlabor->urun += v1 * xbarrl;

                        teq->uset += v1 * xbars;
                        teq->urun += v1 * xbarr;

                        if ( xbar2 > SSEPSILON) x1 = MIN (xbar1, xbar2);
                        else                  x1 = xbar1;

                        //  change  absfactor added later ... gwwd 7/10/04
						teq->uwait += v1 * x1; //  removed7/10/04  * (tlabor->absrate/100) / (1.0 - tlabor->absrate/100.0);
						
						
                        /*   throughtput rate for machine nm. */
                        teq->tpm     +=  v1;
                        /* will divide by throughput */
                        teq->smbar   +=  v1 * x1 * 1.0 / (1.0 - tlabor->absrate/100.0);
                        teq->smbard  +=  v1 * x1 * 1.0 / (1.0 - tlabor->absrate/100.0);

                        tlabor->qpl += v1 * x1; /* DIFFERENCE */

     
                }  //  END  opers loop 

                              
        }  // end parts loop

        // sprintf_s(buf1, "at mpcc1f 2");
        // write_check( c1->varlocal, buf1);

        /*  **************************************** */

        c1->over_util_L = FALSE;

       for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr())
        {
                if(tlabor->num_av > 0.0)
                {


                        /*   --regular multiple server-- */
                        tlabor->uset  /= tlabor->num_av;
                        tlabor->urun  /= tlabor->num_av;

                        u1  = tlabor->uset + tlabor->urun + tlabor->absrate/100.0;

                        tlabor->ul     = u1;

                        if( u1 > (c1->utlimit/100.0))  c1->over_util_L = TRUE;

						tlabor->num_av = tlabor->num *(1.+tlabor->facovt/100.) / (1.+tlabor->max_eq_ot/100.);
						tlabor->eq_cover /= 100*(1.+tlabor->max_eq_ot/100.);
						

                }
                else { /*   --delay server-- */
                       /* tlabor->uset  = 0.0;
                          tlabor->urun  = 0.0;
                        */
                        tlabor->ul    = tlabor->absrate/100.0;
                        u1  = tlabor->uset + tlabor->urun + tlabor->absrate/100.0;
                        tlabor->ul     = u1;
                        if( tlabor->absrate > c1->utlimit)  c1->over_util_L = TRUE;
                }
      
        }  //  end of labor loop



        /* ********* */
        ///if ( c1->over_util_L == TRUE )
               // write_check( c1->varlocal, " Calculating Scrap Rates.\n Checking IBOM for Consistency.\n Calculating Production Requirements for Components.\n Labor is Over-Utilized.\n Calculating Equipment Utilization ...\n");
        // else
               // write_check( c1->varlocal, " Calculating Scrap Rates.\n Checking IBOM for Consistency.\n Calculating Production Requirements for Components.\n Calculating Labor Utilization.\n Calculating Equipment Utilization ...\n");
        /* ********* */

        c1->over_util_E = FALSE;
         for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                if(teq->num > 0)
                {  /*   --regular multiple server-- */
                        teq->uset  /= teq->num;
                        teq->urun  /= teq->num;
                        teq->udown  = (teq->uset+teq->urun) *
                            teq->mttr / (teq->mttf);

						//  added to account for multiple labor but overlay to cut absenteeism
						//  Aug 10 04 gwwd
						tlabor = c1->get_lab_num(teq->lab_ptr);
						teq->uwait *= effabs(tlabor) / (1-effabs(tlabor));

                        teq->uwait /= teq->num;
                        u1     = teq->uset + teq->urun + teq->uwait + teq->udown;
                        teq->u = u1;
						teq->num_av = teq->num * (teq->facovt+100)/(100.0+c1->get_lab_num(teq->lab_ptr)->max_eq_ot);
                        if( u1 > (c1->utlimit/100.0))  c1->over_util_E = TRUE;
                }
                else
                    {  /*   -- delay server-- */
                        teq->uwait = 0.0;
                        u1     = teq->udown;
                        teq->u = u1;
						teq->num_av = -1;
                        if( u1 > (c1->utlimit/100.0))  c1->over_util_E = TRUE;
                }

               
        }  // end equip loop 


        // sprintf_s(buf1, "at mpcc1f 3");
        // write_check( c1->varlocal, buf1);

        /* ************************************************* */
        /* ************************************************* */

	set_xbar_cs(c1);
	//Message( c1,"Hooraaayyy!!!");
	ret = set_cacalc(c1);
	//Message( c1,"Hooraaayyy!!!!!!!!!!!");
	if (ret == FALSE)
	{
		return(FALSE);
	}
	lextra(c1);
	//Message( c1,"Impossible!!");

	/*  ************************************************* */
	/*  ************************************************* */

        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) 
        {
                teq->xbarbar = 0.0;
                teq->xbard   = 0.0;
                teq->xsigbar = 0.0;

                if ( teq->num>0)
                        u1     = teq->uset + teq->urun + teq->uwait + teq->udown;
                else u1     = teq->udown;

                teq->u = u1;
                
                           // sprintf_s(buf1, "name , ut1,  uwait, \n %s \n %f %f\n ", teq->name, teq->u, teq->uwait);
                           // write_check( c1->varlocal, buf1);
                
                if ( u1 > (c1->utlimit/100.0) )   c1->over_util_E = TRUE;

               
        }


        if ( ( c1->over_util_E == TRUE ) || (c1->over_util_L == TRUE) )
        {
                if ( ( c1->over_util_E == TRUE ) && (c1->over_util_L == TRUE) )
                        sprintf_s(buf2, "LABOR and EQUIPMENT resources are over-utilized.");
                else if ( c1->over_util_E == TRUE )
                        sprintf_s(buf2, "EQUIPMENT resources are over-utilized.");
                else if ( c1->over_util_L == TRUE)
                        sprintf_s(buf2, "LABOR resources are over-utilized.");

                
                    sprintf_s(buf1, "WARNING:\n \nProduction level is not sustainable.\n %s \n Manufacturing Cycle Time and WIP cannot be calculated.\n", buf2);
                    write_check( c1->varlocal, buf1);
				
                return(TRUE);
        }


        /*  *************************** */

        sprintf_s(buf1, "at mpcc1f 4");
      //  MessageBox( NULL, buf1, "Into Running ? ", MB_OK | MB_TASKMODAL );

        set_xbar_cs(c1);
        ret = set_cacalc(c1);

        if (ret == FALSE) return(FALSE);

        //if (FULL == 0) return(TRUE);

		//MessageBox( NULL, buf1, "after full test ", MB_OK | MB_TASKMODAL );

         //write_check( c1->varlocal, "after full test ");

        /*  *************************** */

       // strcpy_s(buf1," Calculating Scrap Rates.\n Checking IBOM for Consistency.\n Calculating Production Requirements for Components.\n Calculating Labor Utilization.\n Calculating Equipment Utilization.\n");
      //  strcat_s(buf1," Calculating Operation WIP and Flowtime ...");
      //  write_check( c1->varlocal, buf1);

      //  MessageBox( NULL, buf1, "Into Running ? ", MB_OK | MB_TASKMODAL );

       	for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) 
        {
                /* getting get WAITING TIME */

                if (teq->num > 0)
                {
                      //sprintf_s(buf1, " name, ca2 cs2 \n %s %f %f %d", teq->name,
                      //    teq->ca2, teq->cs2, teq->num);
                      //  write_check( c1->varlocal, buf1); 
                        u1  = teq->u;
                        if (u1 < EPSILON) teq->wait = 0.0;
                        else teq->wait = teq->xbarbar *
                            ((teq->ca2 + teq->cs2)/2.0 )  *
                            NDI_POW(u1,(sqrt((double)(2.0*(teq->num+1.0))) -1.0))  /
                                (teq->num*(1.0-u1));

                          //sprintf_s(buf1, " name wait, xbar \n %s %f %f", teq->name,
                          //   teq->wait, teq->xbarbar);
                          //  write_check( c1->varlocal, buf1);

                        if (teq->wait < 0.0) {
                                sprintf_s(buf1, "MAJOR ERROR: #(103) CONTACT BUILD TO DEMAND.COM \n Equipment wait time < 0.0");
                                write_check( c1->varlocal, buf1);
                                sprintf_s(buf1, "Please record these numbers \n Wait %f  xbar %f ca %f c2 %f u %f n %f \n", teq->wait, teq->xbarbar, teq->ca2, teq->cs2, u1, teq->num);
                                Message( c1,buf1);
                                sprintf_s(buf1, "MAJOR ERROR: #(103) CONTACT CONTACT BUILD TO DEMAND.COM \n Equipment wait time < 0.0");
                              //  MessageBox( NULL, buf1, "out Running ? ", MB_OK | MB_TASKMODAL );

                                warn_err ( c1, buf1, 1,  " ", teq->name, " ", " ", " ", " ");
                                teq->wait = 0.0;

                        }
                          // sprintf_s(buf1, " Done getting waiting time %s %f \n %f %f \n %f %f \n %d %d ", teq->name, teq->wait, teq->ca2, teq->cs2, u1, teq->xbarbar, teq->num, teq->index);
                          // write_check( c1->varlocal, buf1);
                }
                else
                    {
                        teq->wait = teq->mttr * teq->mttr / (teq->mttf);
                        teq->qw   = (teq->uset + teq->urun) / (1.0 - teq->udown);
                }



              //  getx(teq); //use for VARIANCE ZZZ


           
        }  //  end eq	 loop 


        /*  *************************** */
        /* --calculate part/route measures-- */
		//MessageBox( NULL, buf1, "Into mct ", MB_OK | MB_TASKMODAL );


        //fileout = fopen ("data.1", "wt");
	//	MessageBox( NULL, buf1, "out boeing ??", MB_OK | MB_TASKMODAL );


        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {
                tbat_tot = 0.0;
                if ( (tpart->tbatch != -1) && (tpart->tbatch != tpart->lotsiz)) {

                        o_start = tpart->oper_start;
						o_count = tpart->oper_cnt;
						i = o_start +3;
				
                        for (i= o_start+3; i< o_start+o_count; i++ )                
                        {
					      toper = c1->get_oper_num(i);

                                /* getting SUM( S_i_tbatch) lotsize */
                                /* and allowing for lotsiz < 1 */
                                calc_op (c1, tpart, toper, &v1, &xbarsl, &xbarrl, LABOR_T_GATHER_3);
                                calc_op (c1, tpart, toper, &v1, &xbars,  &xbarr,  EQUIP_T_GATHER_3);

//xxZZ
                                /* times per tbatch &per piece !!! YES BREAKDOWNS !!! */
                                x1 = ( xbarrl + xbarsl) * MAX (1.0, tpart->tbatch * (toper->lsize/tpart->lotsiz));
                                x2 = ( xbarr + xbars)  * MAX (1.0, tpart->tbatch * (toper->lsize/tpart->lotsiz));
                                v1    *=  MIN(1.0, toper->lsize);

                                if ( (tpart->tbatch * (toper->lsize/tpart->lotsiz)) < 1)  /* ave tbatch size */
                                       no_batchs = toper->lsize;
                                else   no_batchs = tpart->lotsiz / tpart->tbatch;


                                x_t_1 = calc_xprime(c1,  tpart, toper, x1, x2);
                                toper->bc_lsize = x_t_1 * v1 * (no_batchs-1.0) * toper->vpergood;
                                tbat_tot += toper->bc_lsize;
                              
                        }
                } /* end of if tbatch ... */

				write_check(c1->varlocal, "about to gather");

                tpart->tgather   = get_gather(c1, tpart) / 2.0;

                tpart->tsgood  += tpart->tgather;
                tpart->w_lot   += tpart->tgather;
                tpart->qpart   += tpart->tgather * (1.0-tpart->scrap) * tpart->lotsiz * tpart->dlam;
                   // sprintf_s(buf1, " part %s  tbat_tot %f  gather %f ", tpart->name, tbat_tot, tpart->tgather);
                   // write_check( c1->varlocal, buf1); 

                        o_start = tpart->oper_start;
						o_count = tpart->oper_cnt;
						
				
                        for (i= o_start; i< o_start+o_count; i++ )                
                        {

                        teq = c1->get_eq_num(toper->eq_ptr);
                        tlabor = c1->get_lab_num(teq->lab_ptr);


//xxZZ
                        /* using TRANSFER BATCH  lotsize */
                        /* and allowing for lotsiz < 1 */
                        calc_op (c1, tpart, toper, &v1, &xbarsl, &xbarrl, LABOR_T);
                        calc_op (c1, tpart, toper, &v1, &xbars,  &xbarr,  EQUIP_T);


                        if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                        else xtrans = tpart->tbatch;

//xxZZ
                      calc_op (c1, tpart, toper, &v1, &xbarsl_t,  &xbarrl_t,  T_BATCH_TOTAL_LABOR);
                      calc_op (c1, tpart, toper, &v1, &xbars_t,   &xbarr_t,  T_BATCH_TOTAL_EQUIP);

                        xbar1 = xbarsl_t + xbarrl_t;
                        xbar2 = xbars_t  + xbarr_t;
                        v1    *=  MIN(1.0, toper->lsize);
                        xprime = calc_xprime(c1,  tpart, toper, xbar1, xbar2);


//xxZZ

                        if (( tbat_tot > EPSILON)  & ( toper->vpergood > EPSILON)) x1 = (toper->bc_lsize/tbat_tot)/toper->vpergood;
                        else x1 = 0.0;

						
                        rpv= xprime + teq->wait + tpart->tgather * x1;


                        toper->flowtime = toper->vpergood * rpv;
                        /* new flowtimes ... */

//xxZZ

                      calc_op (c1, tpart, toper, &v1, &xbars_t1,  &xbarr_t1,  T_BATCH_PIECE);

                          toper->w_run     = toper->vpergood * xbarr_t1;
                          toper->w_setup   = toper->vpergood * xbars_t1;

                      calc_op (c1, tpart, toper, &v1, &xbars_t2,  &xbarr_t2,  T_BATCH_WAIT_LOT);
                          toper->w_lot     = toper->vpergood *
                               ( (xbars_t2 + xbarr_t2) * MAX(0.0, xtrans*toper->lsize/tpart->lotsiz - 1.0)
                                 + tpart->tgather * x1 );


                          toper->w_equip  = (teq->u > SSEPSILON) ? (toper->vpergood * teq->wait * (teq->u - teq->uwait) / teq->u)  : 0.0;
                          toper->w_equip += toper->vpergood * (xbarr_t1+xbars_t1 +
                                                               ((xbars_t2 + xbarr_t2) * MAX(0.0, xtrans*toper->lsize/tpart->lotsiz - 1.0)) )
                                            * (teq->mttr / teq->mttf);

                           toper->w_labor = toper->flowtime -  (
                               toper->w_run + toper->w_setup + toper->w_lot + toper->w_equip );

                           if (toper->w_labor < -1.0*c1->t1/1000.0) {
                              sprintf_s (buf1, "INTERNAL ERROR: #(71)\n Contact Build to Demand at  BUILD TO DEMAND.COM \n Flow time details are questionable - \n Calculation will continue ");
                              warn_err ( c1, buf1, 1,  " ", teq->name, tpart->name, toper->name, " ", " ");
                           }

                           toper->w_labor = MAX(0.0, toper->w_labor);
                           if (toper->w_labor < (0.0001*toper->flowtime))
                                   toper->w_labor = 0.0;

                           ///  if NO DEMAND ...
                               if ((tpart->ndmnd * tpart->facdem + tpart->demd_ass + tpart->demd_scrap) < 0.0001) {
                                  toper->w_labor =0;
                                  toper->w_equip=0;
                                  toper->w_setup=0;
                                  toper->w_run=0;
                                  toper->w_lot=0  ;
                                  rpv   = 0;
                                }

                               tpart->w_labor += toper->w_labor;
                               tpart->w_equip += toper->w_equip;
                               tpart->w_setup += toper->w_setup;
                               tpart->w_run   += toper->w_run  ;
                               tpart->w_lot   += toper->w_lot  ;
                        /* end of new flowtimes ... */
                        tpart->tsgood  += toper->vpergood * rpv;



#if defined (BOEING)

                        calc_op (tpart, toper, &vlam1, &xbarsl, &xbarrl, LABOR_T);
                        xbarrl *= MAX(1.0, toper->lsize);

                        calc_op (tpart, toper, &vlam1, &xbars,  &xbarr,  EQUIP_T);
                        xbarr *=  MAX(1.0, toper->lsize);

                        vlam1    *= MIN(1.0, toper->lsize);


                        xbar1 = xbarsl+ xbarrl;
                        xbar2 = xbars + xbarr;


                        xprime = calc_xprime( tpart, toper, xbar1, xbar2);
                        xm_only = MAX(0.0,   xbar2-xbar1);
                        if (xbar2 > SSEPSILON) xl_only = MIN(xbar1, xbar2);
                        else             xl_only = xbar1;
                        xl_only *= (1.0 / ( 1.0 - effabs(tlabor)));

                        xprsqr =  (  2.0* NDI_POW(teq->mttr,2.0) * teq->imttf*xbar2
                            +  NDI_POW( (1.0 + teq->imttf*teq->mttr),2.0) *
                            NDI_POW( (c1->v_equip * teq->faccvs    * (xm_only)/100.0), 2.0)
                            +  NDI_POW( (c1->v_lab * tlabor->faccvs * (xl_only)*(1.0 + tlabor->fac_eq_lab) /100.0), 2.0));


                        xbarbar  = vlam1 * xprime;
                        xbard    = vlam1;
                        xsigbar  =
                            vlam1 * ( xprsqr + NDI_POW(xprime,2.0)  );


                        if (xbarbar  >  SSEPSILON)
                            cs2 = (xsigbar * xbard / NDI_POW(xbarbar,2.0)) - 1.0;
                        else
                            cs2 = 1.0;

                        xprime = rpv - teq->wait;

                        // using v_visit as cv2 of no. of visits
                        v_visit = fabs(toper->vpergood-1);
                        // now it is variance of no. of visits.
                        v_visit *= toper->vpergood *toper->vpergood;

                        x1  =   (teq->X + cs2*xprime*xprime ) * toper->vpergood*toper->vpergood // use for VARIANCE ZZZ
                                       + rpv*rpv * (v_visit);                  // use for VARIANCE ZZZ

                        tpart->tsvar +=   x1;

             fprintf(fileout, "part , oper %s %s,  v_visit %f  X:%f xprsqr:%f  xprime:%f  x1:%f rpv:%f \n",
                                    tpart->name, toper->name, v_visit, teq->X, xprsqr, xprime, x1, rpv);


#endif

                        toper->qpoper   = v1 * rpv * MAX(1.0,toper->lsize);
                        tpart->qpart   += v1 * rpv * MAX(1.0,toper->lsize);
                        tpart->wip_lots+= v1 * rpv;

                        /* we are using pieces not lots !!! */
                        teq->q += toper->qpoper;


                       
                }  // opers loop 

                


                /* --note the re-scale times to days-- */
                tpart->tsgood  /= c1->t1;

                tpart->w_labor /= c1->t1;
                tpart->w_equip /= c1->t1;
                tpart->w_setup /= c1->t1;
                tpart->w_run   /= c1->t1;
                tpart->w_lot   /= c1->t1;

                tpart->tgather /= c1->t1;

#if defined (BOEING)
             fprintf(fileout, "part %s  vartime:%f \n",  tpart->name, tpart->tsvar);

                tpart->tsvar =  sqrt(tpart->tsvar);   // use for VARIANCE ZZZ
                tpart->tsvar  /= c1->t1;

             fprintf(fileout, "part %s  vartime:%f \n",  tpart->name, tpart->tsvar);

#endif

               
        }  // end part loop 


        sprintf_s(buf1, "at mpcc1f 5");
      //  MessageBox( NULL, buf1, "Into Running ? ", MB_OK | MB_TASKMODAL );


  //#if defined (BOEING)
     //fclose(fileout);
  //  #endif
        /*  do_all_match (); */   /* do assembly times for all parts */

        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) 
        {
                if (teq->num > 0)
                {
                        teq->qp  = (teq->uset + teq->urun) * teq->num;
                        teq->qw  = teq->q - teq->qp;    /* W-I-P in process */
                } /* end of if */
                else {
                    teq->qp  = (teq->uset +teq->urun);
                    teq->qw  = teq->q - teq->qp;
                }

                if (teq->q < EPSILON)  teq->q = 0.0;
                if (teq->qp < EPSILON) teq->qp = 0.0;
                if (teq->qw < EPSILON) teq->qw = 0.0;
              
        }

        for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr())
        {
                tlabor->ul   = tlabor->uset + tlabor->urun + tlabor->absrate/100.0;
                if (tlabor->num > 0)
                        tlabor->qwl  = tlabor->ql - tlabor->qpl;    /* W-I-P in process */
                else
                        tlabor->ql  = tlabor->qpl + tlabor->qwl;

                if (tlabor->ql < EPSILON)  tlabor->ql = 0.0;
                if (tlabor->qpl < EPSILON) tlabor->qpl = 0.0;
                if (tlabor->qwl < EPSILON) tlabor->qwl = 0.0;
         
        }

        sprintf_s(buf1, "at mpcc1f 6");
      //  MessageBox( NULL, buf1, "Into Running ? ", MB_OK | MB_TASKMODAL );


        return(TRUE);

}

/*  ******************************************************************* */
float  calc_xprime(class_model * c1, class_part * tpart, class_oper  * toper,  float xbar1, float xbar2)
/*  ******************************************************************* */
{

        /*  time the machine is held:
               inlcudes:
                   setup
                   run
                   machine down
                   labor wait
                   labor setup
                   labor run
                   labor abseentee effects.
          */
        float xprime;
        float xtemp, x1, x2;
        class_eq * teq;
        class_lab * tlabor;
		char buf1 [MESSAGE_SIZE];

        float  xm_only, xl_only;

        xprime = 0.0;
        teq    = c1->get_eq_num(toper->eq_ptr);
        tlabor = c1->get_lab_num(teq->lab_ptr);

           // sprintf_s(buf1, " xprime  tpart:%s oper:%s lab:%s  lab %f eq %d ",
           //    tpart->name, toper->name, tlabor->name, tlabor->num_av, teq->num);
           // write_check( c1->varlocal, buf1);



        if ( xbar2 >= (xbar1-EPSILON)) {
                /* standard equipement time >= labor time */

                xm_only =  xbar2 - xbar1;
                xl_only =  xbar1 *  1.0 / ( 1.0 - effabs(tlabor));

                xprime = xm_only + (xbar2 * teq->mttr/(teq->mttf)) +
                    xl_only * ( 1.0 + tlabor->fac_eq_lab);
                return(xprime);
        }

        else if ( xbar2 > EPSILON) {

                /* labor time > equip time */
                //write_check( c1->varlocal, "Labor > equip");


                xprime = xbar2 * teq->mttr/(teq->mttf)  +
                       xbar2 * ( 1.0 / ( 1.0 - effabs(tlabor))) * (1.0 + tlabor->fac_eq_lab);
        }

        else  {
                /* equipment not used LABOR DELAY ONLY */
                //write_check( c1->varlocal, "Labor > equip == 0 ");


                xprime =  xbar1 * ( 1.0 / ( 1.0 - effabs(tlabor))) *
                        (1.0 + tlabor->fac_eq_lab);

                return(xprime);
        }

        return(xprime);
}

/* --------------------------------------- */

double effabs (class_lab * tlabor)
{
	double x;
	float n;
	char buf2 [ 255];



	//return (tlabor->absrate/100.0);

	n = tlabor->num - 1;
	if (n <0)  
		x = tlabor->absrate/100.0;
	else
		x = NDI_POW(tlabor->ul, (double) n) * tlabor->absrate/100;



	//sprintf_s(buf2 , " effabs %f, abs %f util %f\n", x, tlabor->absrate, tlabor->ul);
	//MessageBox( NULL, " got here", buf2, MB_OK | MB_TASKMODAL );
 
	 x = MIN (x, 0.999);  //  gwwd 


	return (x);
}


/* ********************************************************************* */
/* ********************************************************************* */
void calc_op (class_model * c1, class_part * tpart, class_oper  * oper, float *v1, float *xs, float *xr, int eq_lab)
/* ********************************************************************* */
{
        class_lab *lab;
        class_eq *eq;
        float batches;
        float tbatchsize, xtrans;
		char buf1 [MESSAGE_SIZE];


        *v1 = tpart->dlam * oper->lvisit;
        eq  = c1->get_eq_num(oper->eq_ptr);
        lab = c1->get_lab_num( eq->lab_ptr);

        if (tpart->tbatch > EPSILON)
		   if (c1->inOPT = TRUE) batches = (tpart->lotsiz / tpart->tbatch);
		   else batches = (int)((tpart->lotsiz / tpart->tbatch) +.90);
        else
           batches = 1;

            // sprintf_s(buf1, "into calc op %s %s %f  %f %f ", tpart->name, oper->name, *v1,  tpart->dlam, oper->lvisit);
            // write_check( c1->varlocal, buf1);  

        if (eq_lab == LABOR_T)
        {
                *xs = (oper->lsetup + oper->lsetbatch * batches + oper->lsetpiece * oper->lsize)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                /*  tested in read */

                *xr = (oper->lrlot + oper->lrbatch*batches + oper->lpiece*oper->lsize)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
                if (oper->lsize > EPSILON)
                     *xr /= oper->lsize;
                else
                     *xr = 0.0;
        }                         /*  tested in read */
       else if (eq_lab == EQUIP_T)
                {
                      *xs = (oper->esetup + oper->esetbatch * batches + oper->esetpiece * oper->lsize)
                              * eq->facset / (1.0+eq->facovt/100.0);
                        /*  tested in read */

                      *xr = (oper->erlot + oper->erbatch*batches + oper->epiece*oper->lsize)
                              * eq->facrun / (1.0+eq->facovt/100.0);
                      if (oper->lsize > EPSILON)
                          *xr /= oper->lsize;
                      else
                          *xr = 0.0;
        }                         /*  tested in read */
        else if (eq_lab == T_BATCH_TOTAL_LABOR)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->lsetup + oper->lsetbatch * 1 + oper->lsetpiece * tbatchsize)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                /*  tested in read */

                *xr = (oper->lrlot + oper->lrbatch * 1 + oper->lpiece * tbatchsize)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
                // return value is time for TBATCH pieces + 1 tbatch + 1 lot
        }
        else if (eq_lab == T_BATCH_TOTAL_EQUIP)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->esetup + oper->esetbatch * 1 + oper->esetpiece * tbatchsize)
                      * eq->facset / (1.0+eq->facovt/100.0);
                /*  tested in read */

                *xr = (oper->erlot + oper->erbatch * 1 + oper->epiece * tbatchsize)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
                // return value is time for TBATCH pieces + 1 tbatch + 1 lot
        }
        else if (eq_lab == T_BATCH_PIECE)
        {
                *xs = (oper->esetup + oper->esetbatch * 1 + oper->esetpiece * 1)
                      * eq->facset  / (1.0+eq->facovt/100.0);
                /*  tested in read */

                *xr = (oper->erlot + oper->erbatch * 1 + oper->epiece * 1)
                      * eq->facrun / (1.0+eq->facovt/100.0);
                // return value is time for 1 piece + 1 tbatch + 1 lot
        }
        else if (eq_lab == T_BATCH_WAIT_LOT) {

                *xs = oper->esetpiece
                      * eq->facset  / (1.0+eq->facovt/100.0);

                *xr =  oper->epiece
                      * eq->facrun  / (1.0+eq->facovt/100.0);
                // return value is time for 1 piece + 1 tbatch + 1 lot
        }
        else if (eq_lab == LABOR_T_GATHER_1)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                /////  real stuff
                *xs = oper->lsetup
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xr = 0.0;
                // return value is time for setup
        }
        else if (eq_lab == EQUIP_T_GATHER_1)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                ///  real stuff
                *xs = oper->esetup
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xr = 0.0;
        }
        else if (eq_lab == LABOR_T_GATHER_2)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->lsetbatch *1 + oper->lsetpiece * tbatchsize)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xs *= (tpart->tbatch == -1) ? 1 : ( tpart->lotsiz /tpart->tbatch -1);
                /*  tested in read */

                *xr = (oper->lrbatch * 1 + oper->lpiece * tbatchsize)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
                *xr *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch -1);

                // return value is time for TBATCH pieces + 1 tbatch
        }
        else if (eq_lab == EQUIP_T_GATHER_2)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->esetbatch*1 + oper->esetpiece * tbatchsize)
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xs *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch -1);
                /*  tested in read */

                *xr = (oper->erbatch * 1 + oper->epiece * tbatchsize)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
                *xr *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch -1);
        }
        else if (eq_lab == LABOR_T_GATHER_3)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->lsetbatch *1 + oper->lsetpiece * tbatchsize)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xs *= (tpart->tbatch == -1) ? 1 : ( tpart->lotsiz /tpart->tbatch);
                /*  tested in read */

                *xr = (oper->lrbatch * 1 + oper->lpiece * tbatchsize)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
                *xr *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch);

                // return value is time for TBATCH pieces + 1 tbatch
        }
        else if (eq_lab == EQUIP_T_GATHER_3)
        {
            if ( tpart->tbatch == -1.0) xtrans = tpart->lotsiz;
                else xtrans = tpart->tbatch;
            tbatchsize = MAX (1.0, xtrans * (oper->lsize/tpart->lotsiz));

                *xs = (oper->esetbatch*1 + oper->esetpiece * tbatchsize)
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xs *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch);
                /*  tested in read */

                *xr = (oper->erbatch * 1 + oper->epiece * tbatchsize)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
                *xr *= (tpart->tbatch == -1) ? 1 : (tpart->lotsiz /tpart->tbatch);
        }
		else if (eq_lab == LABOR_1P) {
                *xs = (oper->lsetpiece)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xr = (oper->lpiece)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
        }
        else if (eq_lab == EQUIP_1P) {
                *xs = (oper->esetpiece)
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xr = (oper->epiece)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
        }
        else if (eq_lab == LABOR_1_TB) {
                *xs = (oper->lsetbatch)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xr = (oper->lrbatch)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
        }
        else if (eq_lab == EQUIP_1_TB) {
                *xs = (oper->esetbatch)
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xr = (oper->erbatch)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
        }

		
        else if (eq_lab == LABOR_IJK) {
                *xs = (oper->lsetbatch +oper->lsetup)
                      * eq->facset * lab->facset / (1.0+lab->facovt/100.0);
                *xr = (oper->lrbatch)
                      * eq->facrun * lab->facrun / (1.0+lab->facovt/100.0);
        }
        else if (eq_lab == EQUIP_IJK) {
                *xs = (oper->esetbatch + oper->esetup)
                      * eq->facset / (1.0+eq->facovt/100.0);
                *xr = (oper->erbatch)
                      * eq->facrun  / (1.0+eq->facovt/100.0);
        }
        return;
}

/*************************************************/
/* added local variables */
  //  tot_weight
  //  tot_gather
  //  moved to c1 class_model

/*************************************************/
float  get_gather (class_model * c1, class_part * tpart)
/**********************************************************/
{
        class_oper *toper;
		int o_count;
		int o_start;
		int i;
		char buf1 [MESSAGE_SIZE];
      

        c1->tot_gather = 0.0;
        c1->tot_weight = 0.0;

          // sprintf_s(buf1, " part %s  lotsize %f  tbatch %f ", tpart->name, tpart->lotsiz, tpart->tbatch);
          // write_check( c1->varlocal, buf1); 


        if (tpart->lotsiz == tpart->tbatch) return (0.0);
        if (tpart->tbatch == -1.0)          return (0.0);


        /* loop through keep = 0   FLAG DONE */
	
       o_start = tpart->oper_start;
	   o_count = tpart->oper_cnt;
				
                for (i= o_start; i< o_start+o_count; i++ )                
                {
					toper = c1->get_oper_num(i);
					toper->keep = FALSE;
				}		



        toper = c1->get_oper_num(o_start); /* get pointer to "DOCK" */    

		write_check( c1->varlocal, "about to start  do_gather "); 

        do_gather (c1, tpart, toper, 0.0, 1.0);  // gwwd error 9-23

        if (c1->tot_weight > EPSILON) c1->tot_gather /= c1->tot_weight;
        else c1->tot_gather = 0.0;


        /* loop through keep  FLAG PERM */
       
                for (i= o_start; i< o_start+o_count; i++ )                
                {
					toper = c1->get_oper_num(i);
					  toper->keep = PERM;
                }


        return (c1->tot_gather);
}
/**********************************************************/
void   do_gather (class_model * c1, class_part * tpart, class_oper  * frm_oper, float gathn, float prob)
/**********************************************************/
{	
        class_oper *tooper;
        class_route*roper;
        float      new_gath;
        float      xbarsl, xbarrl, xbars, xbarr, v1;
        float      xbar1, xbar2, xbar3, xbar4;
        char       buf1[255];

          // sprintf_s(buf1, " do_gather op %s EQ: %s ", frm_oper->name, c1->get_eq_num(frm_oper->eq_ptr)->name);
          // write_check( c1->varlocal, buf1); 

        if (frm_oper->keep == TRUE) return;

        frm_oper->keep = TRUE;
        calc_op (c1, tpart, frm_oper, &v1, &xbarsl, &xbarrl, LABOR_T_GATHER_1);
        calc_op (c1, tpart, frm_oper, &v1, &xbars,  &xbarr,  EQUIP_T_GATHER_1);
        xbar1 = calc_xprime(c1, tpart, frm_oper, xbarsl, xbars);
        xbar2 = calc_xprime(c1, tpart, frm_oper, xbarrl, xbarr);
        // sprintf_s(buf1, "SETUP s, sl  r rl  %f %f %f %f bar1, bar2 %f %f \n", xbars, xbarsl, xbarr, xbarrl, xbar1, xbar2);
        // write_check( c1->varlocal, buf1);

        calc_op (c1, tpart, frm_oper, &v1, &xbarsl, &xbarrl, LABOR_T_GATHER_2);
        calc_op (c1, tpart, frm_oper, &v1, &xbars,  &xbarr,  EQUIP_T_GATHER_2);
        xbar3 = calc_xprime(c1, tpart, frm_oper, xbarsl, xbars);
        xbar4 = calc_xprime(c1, tpart, frm_oper, xbarrl, xbarr);
        // sprintf_s(buf1, "RUN  s, sl  r rl  %f %f %f %f bar3, bar4 %f %f \n", xbars, xbarsl, xbarr, xbarrl, xbar3, xbar4);
        // write_check( c1->varlocal, buf1);


//xxZZ
        new_gath = MAX (gathn - c1->get_eq_num(frm_oper->eq_ptr)->wait - xbar1 - xbar2, xbar3 + xbar4);


        roper = find_rt_from (c1, frm_oper->name, tpart);
        if (  (roper==NULL)&&(strcmp(frm_oper->name, "SCRAP") != 0)  ) {         
                  sprintf_s(buf1, "INTERNAL ERROR: #(84)\n Operation is a sink for pieces.\n There are no `FROM-TO' routings from this operation? ");
                  warn_err ( c1, buf1, 0,  " ", " ", tpart->name, frm_oper->name, " ", " ");
				  write_check(c1->varlocal, buf1);
                  return;
                  }

        while ( (roper != NULL) && (strcmp(frm_oper->name, c1->get_oper_num(roper->op_from_ptr)->name) == 0)) {


                if (strcmp(c1->get_oper_num(roper->op_to_ptr)->name, "STOCK") == 0) {    
                        c1->tot_gather += new_gath * roper->cmmpij * prob/100.0;
                        c1->tot_weight +=            roper->cmmpij * prob/100.0;
                          // sprintf_s(buf1, "gather adding to STOCK  %f %f ", new_gath, (roper->cmmpij*prob/100.0));
                          //  write_check( c1->varlocal, buf1); 
                }

                else { /* loop through all op eq assign pairs .. */

                        tooper = find_opas (c1, c1->get_oper_num(roper->op_to_ptr)->name, tpart);
                        if (tooper == NULL) {
                            sprintf_s(buf1, "INTERNAL ERROR: #(85)\n Operation data cannot be found.\n There is no `OPERATION ASSIGNMENT' record for this operation ");
                            warn_err (c1, buf1, 1,  " ",  " ", tpart->name, c1->get_oper_num(roper->op_to_ptr)->name, " ", " ");
							write_check(c1->varlocal, buf1);
                        }
                        while ( (tooper != NULL) && (strcmp(tooper->name, c1->get_oper_num(roper->op_to_ptr)->name) == 0)) {
                                do_gather (c1, tpart, tooper, new_gath, prob * roper->cmmpij * tooper->cmmprp/10000.0);
								
                                tooper = c1->get_oper_num(tooper->oper_ptr+1); ///  to do   Xxxgwd NEXT OPER AFTER ???
								if (tooper != NULL) { if (tpart != c1->get_part_num(tooper->part_ptr)) tooper = NULL; }
                        }
                }

               //  next oper oops !!!  
		//		error here !!!  same oper ? next op diff eq
				roper = c1->get_route_num(roper->me_ptr+1);
				if (roper != NULL) {  if (tpart != c1->get_part_num(roper->part_ptr)) ;  roper = NULL; }
				if (roper != NULL) {  if (strcmp(frm_oper->name, c1->get_oper_num(roper->op_from_ptr)->name) !=0); roper = NULL;}
        }


        frm_oper->keep = FALSE;


        return;
}



/////////////////////////////////////////////

float cdf ( float a);
float Pwm (int m,  float a);

float Pwm(int m, float u){  //  ??  gwwd fix ???
	return u;
}

/////////////////////////////////////////////
void  getx ( class_eq * teq) {
/////////////////////////////////////////////

  float c2, cd2, pw, d3, pwm;
  float gamma, z, pie, pie1, pie2, pie3, pie4, pie5, pie6;

  z =(teq->cs2+teq->ca2) / (1+teq->cs2) ;
  gamma = (teq->num - teq->num*teq->u - .5) / sqrt((double) (teq->num*teq->u * z));
  pwm = Pwm (teq->num, teq->u);

  pie4 = MIN(1,  1-cdf( (1+teq->cs2)*(1-teq->u)*sqrt((double) abs(teq->num))) )  ;

  pie5 = MIN(1,  1-cdf(            2*(1-teq->u)*sqrt((double) abs(teq->num)) / ( 1+teq->ca2)) * pwm /
                         ( 1-cdf( (1-teq->u)* sqrt((double) abs(teq->num))) ) );

  pie6 = 1.0 - cdf( (teq->num - teq->num*teq->u - 0.5) / sqrt( (double) (teq->num * teq->u * z)) );

  if ((teq->num <= 6) || (teq->ca2 >= 1) || (gamma < .5)) {
     pie1 = teq->u * pie4 + (1-teq->u)*(1-teq->u)* pie5;
     pie = pie1;
  }
  else if ((gamma > 1) && (teq->ca2<1)) {
     pie1 = teq->u * pie4 + (1-teq->u)*(1-teq->u)* pie5;
     pie2 = teq->ca2 * pie1 + (1-teq->ca2)*(1-teq->ca2) * pie6;
     pie = pie2;
  }
  else  {
     pie1 = teq->u * pie4 + (1-teq->u)*(1-teq->u)* pie5;
     pie2 = teq->ca2 * pie1 + (1-teq->ca2)*(1-teq->ca2) * pie6;
     pie3 = 2*(1-teq->cs2) * ( gamma-.5)* pie2 +
              (1-(2*(1-teq->ca2)*(gamma-0.5))) * pie1;
     pie = pie3;
  }
  pw = MIN(1.0, pie);


  if (teq->cs2 > 1) {
      d3 = 3 * NDI_POW(teq->cs2, 1.5) * (1+teq->cs2);
  }
  else {
      d3 = (2. * teq->cs2 +1) * (1+teq->cs2);
  }
  cd2 = 2.0 * teq->u -1 + 4*(1- teq->u) * d3 / ( 3 * (teq->cs2 + 1) * (teq->cs2+1));
  c2 = (cd2 + 1.0 - pw) / pw;
  teq->X = teq->wait * teq->wait * c2;


return;
}

