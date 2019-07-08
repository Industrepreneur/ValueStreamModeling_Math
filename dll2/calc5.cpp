#include <stdio.h>
#include <string.h>
#include <stdlib.h>
// #include <math.h>

#include "class_model.cpp"
#include "oplst.h"

void  do_visits(class_model * c1, class_part  * tpart);
 void write_results (class_model * c1);

class_oplst * count_op   (class_model * c1, class_part * tpart, int *num);
void          clean_oplst  (class_model * c1 );
class_oplst * find_oplst (class_model * c1, char * opname);

void load_pij   (class_model * c1, class_oplst * toplst, class_part * tpart, float  pij [], int *num );
void pij_prime  (class_model * c1, class_oplst * toplst, class_part * tpart, float  pij [], int *num );
void transpose  ( float  pij[], int *num );

void  cleanup (class_model * c1,  int * kwa,  float  * pij,  float  * s_pij,  float * mreturn,  class_oplst * toplst);



/* ################### */
void  do_all_visits ( class_model * c1 )
{
        class_part *tpart;
        class_oper *oper;
        int    i;
        int done;
        float  f1, f2;

		char buf1[MESSAGE_SIZE];
        int err_flg;

		class_oper * toper;

	
		 
 
        do {
                done = TRUE;
                for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
                        f1 = tpart->lotsiz;
                        f2 = tpart->tbatch;
                        if ((f1 != -1) && (f1 < f2) ) {

                               sprintf_s(buf1, "WARNING: #(56) \n   Transfer Batch Size is greater than lotsize.\n Transfer Batch Size should be -1 (meaning it is the same as the lotsize) or less than or equal to Lotsize.");
                               warn_err(c1, buf1, 1,  sNULL, sNULL, tpart->name, sNULL, sNULL, sNULL);
                                tpart->tbatch = -1;

                                //sprintf_s(buf1, "ERROR: #(56) \n Transfer Batch Size > Lotsize.\n Transfer Batch Size should be -1 or \n less than or equal to Lotsize.");
                                //Message( c1, c1,buf1);
                        } /* end of if */
                      
                } /* end of part loop while */
        }
        while (!done);

		write_check(c1->varlocal, "at start do_visits");
		

        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
        
                do_visits ( c1, tpart );
				write_check (c1->varlocal, "afte 1 do visits");
				write_check (c1->varlocal, "after 1 do visits");
			
				//return; //  gwwd 5-9-15  testing 
			          // all errrors !!  gwwd 5-8-15  error in do_visit ...

				write_check(c1->varlocal, "in do_visits");

                 if ( c1->CALC_CANCEL != FALSE) err_flg = TRUE;

    
                
        }  //  end of part for loop

		if (err_flg == TRUE)  {c1->CALC_CANCEL = TRUE;  write_check(c1->varlocal, "error in doall visits"); }

	
        return;
}

/* ################################################ */
void  do_visits(class_model * c1, class_part  * tpart)
/* ################################################ */
{
        int  isol, idsol;
        int   num;
        long  arr_size;
        int i, j, no_visit;
        float det;

		//gwwd  9-20  move to c1 ???   done 10-1   oops  no no no   10-2
        int   * kwa;
        float  * pij;
        float  * s_pij;
        float * mreturn;
          int err_flg;


		char buf1 [MESSAGE_SIZE];
		char buf2 [MESSAGE_SIZE];
		char buf3 [MESSAGE_SIZE];
		char s0 [MESSAGE_SIZE];
        class_oper * opas;

		//gwwd  9-20  move to c1 ???  done 10-1  oops undo 10-2
        class_oplst * toplst;
        class_oplst * f_lst;

        char bufl [MESSAGE_SIZE];
        char op_err_id [MESSAGE_SIZE];

		int oldpart;


        err_flg = FALSE;
        toplst = NULL;
        pij     = NULL;
        s_pij   = NULL;
        kwa     = NULL;
        mreturn = NULL;
		f_lst = NULL;

		write_check(c1->varlocal, "check_rout ?  do_visits-1 prod");  //gwwd go here ..  error later in rout

		if (tpart->check_rout == FALSE)  return; // gwwd 10-10

		write_check(c1->varlocal, "begin do visit a part");

restart1:
        c1->CALC_CANCEL = FALSE;
        c1->opas_rt_err = FALSE;   /* NO ERROR */


        check_eq (c1, tpart);

        if (c1->CALC_CANCEL != FALSE)  return;

		//write_check(c1->varlocal, "before check_ops");

        check_ops (c1, tpart); /* check for lab > eq ... */

		write_check(c1->varlocal, "after check_ops");
		


        // if (tpart->check_rout == FALSE)  /* DONE... */  return;  gwwd 10-2

		//write_check(c1->varlocal, "after check_route != false ");


        if (tpart->route_start == -1) { /* NO STEPS OR ROUTES ... */  //   gwwd error 9-18
                opas = c1->get_oper_num(tpart->oper_start); /* DOCK */
                opas->lvisit=1.0;
                opas->lsize=tpart->lotsiz;
                opas->vper100=100.0;
                opas->vpergood=1.0;

                opas = c1->get_oper_num(tpart->oper_start+1);  /* STOCK */
                opas->lvisit=1.0;
                opas->lsize=tpart->lotsiz;
                opas->vper100=100.0;
                opas->vpergood=1.0;

                opas = c1->get_oper_num(tpart->oper_start+2);  /* SCRAP */
                opas->lvisit=0.0;
                opas->lsize=tpart->lotsiz;
                opas->vper100=0.0;
                opas->vpergood=0.0;

                tpart->scrap = 0.0;

				write_check(c1->varlocal, "  no ops / routing ??? ");
                return;
        }



		write_check(c1->varlocal, "at start do_visits A");
//  gwwd error after 5-8-15

        cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
        pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
        /* clean up memory after retart if necessary */


        /* determine number of rows ... needed */
        toplst= count_op (c1, tpart, &num);
		

		write_check(c1->varlocal, "after count_op ");

		if (toplst== NULL){
			write_check(c1->varlocal, "after count_op   toplst returned == null ?");
			//   return;  //  error!!!!
			toplst= c1->all_oplst[0];
		}

		 
		sprintf(buf1, "after count_op A  %i ", num);
		write_check(c1->varlocal, buf1);

        if (c1->CALC_CANCEL == true) {
			            write_check(c1->varlocal, "  cancel calc after count_op A");
                        cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                         pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                        return;
        };

        if (toplst== NULL)  /* NO MEMORY while in run */
         {
			   write_check(c1->varlocal, "  cancel calc no toplst ? ");
				
                c1->CALC_CANCEL = true;
                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                return;
        };



        arr_size = num * (num+1);
//   gwwd error above 

		sprintf(buf3, "did some arrsize %i ", arr_size);
		write_check(c1->varlocal, buf3);

        if (num > EQ_LIMIT) {
                //sprintf_s(buf1, "ERROR: #(6)\n Number of operations for part %s exceeds limit of %d \n The routing equations cannot be solved due to lack of memory");
                //Message( c1, c1,buf1);

                sprintf_s(buf3, "ERROR: #(6)\n Number of operations for product exceeds limit of %d \n The routing equations cannot be solved due to lack of memory", EQ_LIMIT);
                warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, sNULL, sNULL, sNULL);
				write_check(c1->varlocal, buf3);

                c1->CALC_CANCEL = true;
                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                return;
        }

		sprintf(buf3, "about to mem !!! ");
		write_check(c1->varlocal, buf3);

mem_again:

		//gwwd  9-20  move to c1  need to move these to C1 gwwd 10-1   oops  10-2 undo !!

        pij   = (float * )   calloc ( arr_size,  sizeof(float));
        s_pij = (float * )   calloc (arr_size,  sizeof(float));
        mreturn = (float * ) calloc (num,  sizeof(float));
        kwa   = (int   * )   calloc ((num+1),   sizeof(int));


		


        if ((pij == NULL) || (s_pij == NULL) || (mreturn == NULL) || (kwa == NULL))
        {
					sprintf(buf3, "mem error  !!! ");
	    	        write_check(c1->varlocal, buf3);
                          cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                          pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                        //Message( c1, c1,"INSUFFICIENT MEMORY: #(12) \n Insufficient Memory Space. Cannot Solve Routing equations");

                        sprintf_s(buf3, "INSUFFICIENT MEMORY: #(12) \n Insufficient Memory Space. Cannot Solve Routing equations");
                        warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, sNULL, sNULL, sNULL);
						write_check(c1->varlocal, buf3);

                        c1->CALC_CANCEL = true;
                        return;
        };

	    //sprintf(buf3, "about to pij load ");
		//write_check(c1->varlocal, buf3);

        /* getting no. of lot visits */
        /* ********************** */
        load_pij (c1,  toplst, tpart, pij, &num );

		sprintf(buf3, "done pij load ");
		write_check(c1->varlocal, buf3);

        if (c1->CALC_CANCEL == true) {
				sprintf(buf3, "out at load pij ");
				write_check(c1->varlocal, buf3);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;

                return;
        }
				sprintf(buf3, "did load pij ");
				write_check(c1->varlocal, buf3);

        memcpy(s_pij, pij, arr_size*sizeof(float));
		        sprintf(buf3, "did memcpy  ");
				write_check(c1->varlocal, buf3);

        pij_prime(c1,  toplst, tpart, pij, &num );
        if (c1->CALC_CANCEL == true)  {
			    //sprintf(buf3, "out at load pij prime  ");
				//write_check(c1->varlocal, buf3);
                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                return;
        }
				//sprintf(buf3, "did at load pij prime ");
				//write_check(c1->varlocal, buf3);

        /* add changes for STOCK and scrap */
        i=0;j=1;
		pij[ arr (i,j,num+1) ] = 1.0; /* from stok(1) to DOCK(0) = 1, then transpose*/
        i=0;j=2;
        pij[ arr (i,j,num+1) ] = 1.0;

        /* subroutine minusi(a, lda, n) do 10 i = 1, n; { a(i,i) = a(i,i) - 1.0 }*/

        for (i=0; i<num;i++)
                pij [ arr(i,i,num+1)] +=  -1.0;

        /* subroutine makev1(a, lda, n, b) do 10 i = 1, n { a(i,n) = 0.0; b(i)   = 0.0; } */
		        //sprintf(buf3, "did some pij ");
				//write_check(c1->varlocal, buf3);
        for(i=0;i<num;i++)
        {
                j=1;
                pij[ arr(j,i,num+1) ] = 0.0;       /* remove row 1 */
                pij[ arr(i,num, num+1) ] = 0.0;    /* zero out last column */
        }

        /* {a(n,n) = 1.0; b(n)   = 1.0} */
        i=1;j=1;
        pij [ arr(i,j,num+1) ]   = 1.0;
        pij [ arr(i,num,num+1) ] = 1.0;

        //  for (i=0;i<num;i++)
        //       { for (j=0;j<num+1;j++)
        //          printf("pij values %i %i %f ", i,j, pij[ arr(i,j,num+1) ]);
        //         printf(" \n");
        //        }

        /* ********************** */
        /* matinv */

        write_check(c1->varlocal," att 2 ");

        matinv(&isol, &idsol, num, num+1, pij, num+1, kwa, &det);

		        sprintf(buf3, "did invert pij ");
				//return;  //  gwwd 5-9-15  error catch !!!
			    write_check(c1->varlocal, buf3);

        if ((isol != 1) || (idsol != 1)){
                //sprintf_s(buf1, "ERROR: #(69) \n Routing cannot be solved for Part %s. \n CHECK IF THERE IS A PATH FROM EVERY OPERATION TO 'STOCK'.", tpart->name);
                //Message( c1,buf1);

                sprintf_s(buf3, "ERROR: #(69) \n Routing cannot be solved for Product. \n CHECK IF THERE IS A PATH FROM EVERY OPERATION TO 'STOCK'.");
                warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, sNULL, sNULL);
				write_check(c1->varlocal, buf3);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;

                c1->opas_rt_err = mOPRT;
                strcpy(c1->op_error_name, "DOCK");

                c1->CALC_CANCEL = true;
                return;

        } /* end of if*/


        /* ********************** */
        /* test and mult back visit values */

        for (i=0;i<num;i++) mreturn[i] = pij[ arr(i,num,num+1)];

		  sprintf(buf3, "got visit numbers");
		  write_check(c1->varlocal, buf3);

        if (mreturn[1] < EPSILON) {
              sprintf_s(buf1, "Error: \n No pieces of Part %s are being routed to STOCK !\n Please change routing...", tpart->name);
              write_check(c1->varlocal, buf1);

                 sprintf_s(buf3, "Error: \n No pieces of product are being routed to STOCK !\n Please change routing...(1)");
                 warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, sNULL, sNULL);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;

              c1->opas_rt_err = mOPRT;
              strcpy(c1->op_error_name,"DOCK");    
                        c1->CALC_CANCEL = true;
                        return;
        } /* end of mreturn => no Stock */

        no_visit = FALSE;
        sprintf(buf2, "For PART  %s  The following operations are not visited \n", tpart->name);
     
        f_lst = toplst;                              /* go through OPLST */
        while (f_lst != NULL)

        {
                opas  = f_lst->oper;                     /* find op_as start */
                i    = f_lst->me_ptr;       // set me_ptr already     /* find from index */

                while ((opas != NULL) && (strcmp(opas->name, f_lst->name) == 0) )
                        /* go through op-as list */
                {
                        opas->lvisit = mreturn[i] * opas->cmmprp/100.0;

						
						sprintf(s0, "in do_visits no. of visits %i %f %f ", i, mreturn[i], opas->cmmprp);
						write_check(c1->varlocal, s0);

                        if ( (opas->lvisit < EPSILON) && (i != 2)   && (strcmp("SCRAP", opas->name) != 0) && (strcmp("STOCK", opas->name) != 0)  )  /* 2 is for scrap */
                        {																						//GGWWDD   5-5-15
                            if (i != 3) {

                                no_visit = TRUE;
								write_check(c1->varlocal, buf1);
								write_check(c1->varlocal, s0);
								write_check(c1->varlocal, opas->name);

                                if ((strlen(buf1)+64) < 400) {
                                   sprintf_s(bufl, " " );
                                   strcat (buf1, bufl);
                                   strcpy(op_err_id, opas->name);
		                         }; /* if space for message */

                            }

                        };
						oldpart = opas->part_ptr;
                        opas = c1->get_oper_num(opas->oper_ptr + 1);  //  gwwd next oper ?
						if (opas != NULL) {
							   if (oldpart != opas->part_ptr) { opas = NULL;} 
						}
                }                                      /* end of 'diff equip' list */

                // f_lst = c1->all_oplst[f_lst->me_ptr + 1];  
				// if (f_lst->oper == NULL) f_lst = NULL;
				//gwwd 3-23-15
				{
				 if ((f_lst->me_ptr+1) == c1->oplst_count) f_lst = NULL;
                 else  {f_lst = c1->all_oplst[f_lst->me_ptr + 1];
					    if (f_lst->oper == NULL) f_lst = NULL; 
					else {
						//sprintf(s0, "dovisits %s %i break loop next f_lst ",  f_lst->name, f_lst->me_ptr);
						//write_check(c1->varlocal, s0);
					}
				 }

				}       
		}
		/* end of 'all oper' list */

        if (no_visit == TRUE) {
              //  Message( c1,buf1); /* DO NOT ALLOW USER TO CHANGE NOW!! oprt_list(tpart);   */
			  sprintf(buf3, "%s  %s ", buf1, buf2);
              warn_err ( c1, buf3, 1,  sNULL, sNULL, tpart->name, op_err_id, sNULL, sNULL);
			  write_check(c1->varlocal, buf3);
        }

        /* ********************** */
        /* getting lot size */
        /* ********************** */
        memcpy(pij, s_pij, arr_size*sizeof(float));

        pij_prime(c1,  toplst, tpart, pij, &num );

        for (i=0; i<num; i++)
                for (j=0; j<num; j++)
                {
                        pij[ arr(j,i,num+1)] *= mreturn[i] * (1.0 - s_pij[ arr(2,i,num+1)]);
                        if ( mreturn[j] > SSEPSILON) pij[ arr(j,i,num+1)] /= mreturn[j];
                        else { /* leave pij as is .... */
                        }
                }

        /* add changes for STOCK and scrap */
        /* pij[ arr (0,1,num+1) ] = 1.0; */  /* from stok(1) to DOCK(0) = 1, then transpose*/
        i=0;j=2;
        pij[ arr (i,j,num+1) ] = 1.0;

        /* subroutine minusi(a, lda, n) do 10 i = 1, n; { a(i,i) = a(i,i) - 1.0 }*/

        for (i=0; i<num;i++)
                pij [ arr(i,i,num+1)] +=  -1.0;

        /* subroutine makev1(a, lda, n, b) do 10 i = 1, n { a(i,n) = 0.0; b(i)   = 0.0; } */

        for(i=0;i<num;i++)
        {
                pij[ arr(2,i,num+1) ] = 0.0;       /* remove row 2 */
                pij[ arr(i,num, num+1) ] = 0.0;    /* zero out last column */
        }

        /* {a(n,n) = 1.0; b(n)   = 1.0} */
        i=2;j=0;
        pij [ arr(i,j,num+1) ]   = 1.0;
        pij [ arr(i,num,num+1) ] = tpart->lotsiz;

        /* for (i=0;i<num;i++)
               { for (j=0;j<num+1;j++)
                  printf("pij values %i %i %f ", i,j, pij[ arr(i,j,num+1) ]);
                 printf(" \n");
                } */

        /* ********************** */
        /* matinv */

        //Message( c1," at 3 ");

        matinv(&isol, &idsol, num, num+1, pij, num+1, kwa, &det);

        if ((isol != 1) || (idsol != 1)) {
                //Message( c1,"INTERNAL ERROR: #(21b) \n Routing cannot be solved for Part");

                sprintf_s(buf3,"INTERNAL ERROR: #(21b) \n Routing cannot be solved for product");
                warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, sNULL, sNULL);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;
                c1->CALC_CANCEL = true;
                return;
        };

        //Message( c1," at 3a");
        /* ********************** */
        /* go number times run = lotisz * no. of visits */

        for (i=0;i<num;i++) mreturn[i] = pij[ arr(i,num,num+1)]; /* lotsiz */

        if (mreturn[1] < EPSILON) {
              sprintf_s(buf1, "Error: \n No pieces of Part %s are being routed to STOCK !\n Please change routing...", tpart->name);
              write_check(c1->varlocal,buf1);

                sprintf_s(buf3, "Error: \n No pieces of product are being routed to STOCK !\n Please change routing...(2)");
                warn_err ( c1, buf3, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, sNULL, sNULL);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;

              c1->opas_rt_err = mOPRT;
              strcpy(c1->op_error_name, "DOCK");
                        c1->CALC_CANCEL = true;
                        return;
        } /* end of mreturn => no Stock */

        f_lst = toplst;                                /* go through OPLST */
        while (f_lst != NULL)

        {
                opas  = f_lst->oper;                     /* find op_as start */
                i    = f_lst->me_ptr;                     /* find from index */
                //write_check(c1->varlocal, opas->name);

                while ((opas != NULL) && (strcmp(opas->name, f_lst->name) == 0) )
                        /* go through op-as list */
                {
                        //sprintf_s(buf1, "name%s %s return %f  retu1 %f i: %d", opas->name, f_lst->name, mreturn[i], mreturn[1], i);
                        //write_check(c1->varlocal, buf1);
                        opas->lsize = mreturn[i];
                        opas->vper100 = 100.0 * mreturn[i] * opas->lvisit / mreturn[1];

						//sprintf(s0, "in visits per 100 & lsize  %i %f %f %f ", i, mreturn[i], opas->lvisit, mreturn[1]);
						//write_check(c1->varlocal, s0);

                        oldpart = opas->part_ptr;
                        opas = c1->get_oper_num(opas->oper_ptr + 1);
						if (opas != NULL) {   
							 if (oldpart != opas->part_ptr) { opas = NULL;} 
						}
                }                                      /* end of 'diff equip' list */

                //f_lst = c1->all_oplst[f_lst->me_ptr + 1];
				//if (f_lst->oper == NULL) f_lst = NULL;
				//gwwd 3-23-15
				{
				 if ((f_lst->me_ptr+1) == c1->oplst_count) f_lst = NULL;
                 else  {f_lst = c1->all_oplst[f_lst->me_ptr + 1];
					    if (f_lst->oper == NULL) f_lst = NULL; 
					else {
						//sprintf(s0, "dovisits 2 %s %i break loop next f_lst ",  f_lst->name, f_lst->me_ptr);
						//write_check(c1->varlocal, s0);
					}
				 }

				}       
        }                                           /* end of 'all oper' list */

        tpart->scrap = (mreturn[0]-mreturn[1]) / mreturn[1];
		//sprintf(s0, "in visits scrap rate  %f %f %f ", tpart->scrap, mreturn[i],  mreturn[1]);
		//write_check(c1->varlocal, s0);


        //Message( c1," at 3b");
        /* ********************** */
        /* getting % of good pieces at operation */
        /* ********************** */


			   /*   load_pij (c1,  toplst, tpart, pij, &num );
				if (c1->RESTART_CANCEL == CANCEL) {
						cleanup (c1, kwa, pij, mreturn, mreturn, toplst);
						pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst = NULL;
						return;
				}
				*/  //  replaced by code below...

		
        memcpy(pij, s_pij, arr_size*sizeof(float));

        transpose ( pij, &num );


        /* subroutine minusi(a, lda, n) do 10 i = 1, n; { a(i,i) = a(i,i) - 1.0 }*/

        for (i=0; i<num;i++)
                pij [ arr(i,i,num+1)] +=  -1.0;

        /* add changes for STOCK and scrap */
        i=1;j=2;
        pij[ arr (i,i,num+1) ] = 1.0;
        pij[ arr (j,j,num+1) ] = 1.0;

        /* subroutine makev1(a, lda, n, b) do 10 i = 1, n { a(i,n) = 0.0; b(i)   = 0.0; } */

        for(i=0;i<num;i++)
                pij[ arr(i,num, num+1) ] = 0.0;    /* zero out last column */

        i=1;
        pij [ arr(i,num,num+1) ] = 1.0;

        /* for (i=0;i<num;i++)
               { for (j=0;j<num+1;j++)
                  printf("pij values %i %i %f ", i,j, pij[ arr(i,j,num+1) ]);
                 printf(" \n");
                } */


        /* ********************** */
        /* matinv */

      //  write_check(c1->varlocal," at 4 ");

        matinv(&isol, &idsol, num, num+1, pij, num+1, kwa, &det);

        if ((isol != 1) || (idsol != 1)) {
                //Message("INTERNAL ERROR: #(21c) \n Routing cannot be solved for Part");

                sprintf_s(buf1, "INTERNAL ERROR: #(21c) \n Routing cannot be solved for product");
                warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, sNULL, sNULL);

                cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
                pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;

                c1->CALC_CANCEL = true;
                return;
        };


        /* ********************** */
        /* go number times run by 1 good piece = % of good * vper100 */

        for (i=0;i<num;i++) mreturn[i] = pij[ arr(i,num,num+1)]; /* % eventually good */

        f_lst = toplst;                                /* go through OPLST */
        while (f_lst != NULL)

        {
                opas  = f_lst->oper;                     /* find op_as start */
                i    = f_lst->me_ptr;                       /* find from index */


                while ((opas != NULL) && (strcmp(opas->name, f_lst->name) == 0) )
                        /* go through op-as list */
                {
                        opas->vpergood =  mreturn[i] * opas->vper100 /100.0;

						//sprintf(s0, "in visits per good   %i %f %f ", i, mreturn[i], opas->vper100);
						// write_check(c1->varlocal, s0);

                        oldpart = opas->part_ptr;
                        opas = c1->get_oper_num(opas->oper_ptr + 1);
						if (opas != NULL) { 
							if (oldpart != opas->part_ptr) { opas = NULL;} 
						}
	            }                                      /* end of 'diff equip' list */

                //f_lst = c1->all_oplst[f_lst->me_ptr + 1];
				//if (f_lst->oper == NULL) f_lst = NULL;
				//gwwd 3-23-15
				{
				 if ((f_lst->me_ptr+1) == c1->oplst_count) f_lst = NULL;
                 else  {f_lst = c1->all_oplst[f_lst->me_ptr + 1];
					    if (f_lst->oper == NULL) f_lst = NULL; 
					else {
						//sprintf(s0, "dovisits 3 %s %i break loop next f_lst ",  f_lst->name, f_lst->me_ptr);
						//write_check(c1->varlocal, s0);
					}
				 }

				}       

        }                                           /* end of 'all oper' list */


        /* ********************** */

        cleanup (c1, kwa, pij, s_pij, mreturn, toplst);
        pij = NULL; s_pij = NULL; mreturn = NULL; kwa = NULL; toplst= NULL;


      //  write_check(c1->varlocal," at 5 ");

        //  gwwd  changed 9-25   changed back 10-11
		tpart->check_rout=FALSE;

        return;

}
/* ################################################ */
class_oplst * count_op (class_model * c1, class_part * tpart, int *num)
/* ################################################ */
{
        class_oplst * toplst;
        class_oplst * next_op;
        class_oplst * prev_op;

        class_oper  * poper;
        class_route  * oprt;

		int oldpart;


         char oldname  [NAME_SIZE];
		char buf1[MESSAGE_SIZE];
		char buf2[MESSAGE_SIZE];




        c1->CALC_CANCEL = FALSE;
        *num = 0;

        toplst = c1->all_oplst[0];

        if (toplst == NULL) {
                sprintf_s(buf1,"INSUFFICIENT MEMORY: #(22a) \n Insufficient Memory to calculate No. times operations done for product");
                warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, sNULL, sNULL, sNULL);
                //Message(buf1);
                return(NULL);
        }
        //  set already  toplst->me_ptr = 0;
        strcpy(toplst->name, "DOCK");
        toplst->optot = 0.0;
        toplst->rttot = 0.0;
		toplst->oper = find_opas (c1, "DOCK", tpart);

		int oldpart_ptr;

        /* do rttot */
        oprt = find_rt_from (c1, "DOCK",  tpart);
        while ((oprt != NULL) && (strcmp(c1->get_oper_num(oprt->op_from_ptr)->name, "DOCK") == 0))
        {
                toplst->rttot   += oprt->cmmpij;
				oldpart_ptr = oprt->part_ptr;
                oprt = c1->get_route_num(oprt->me_ptr+1);
				if (oprt != NULL) {			
					if (oprt->part_ptr != oldpart_ptr) oprt = NULL;
				}
        }
        if ( fabs(toplst->rttot-100.0) > ROUT_ERR)
        {
                sprintf_s(buf1,"ERROR: #(74) \n For part %s %% of pieces Routed out of DOCK does not equal  100.0%% ", tpart->name);
                write_check(c1->varlocal, buf1);//Message(buf1);

                sprintf_s(buf1,"ERROR: #(74) \n %% of pieces Routed out of DOCK does not equal 100.0%% ");
                warn_err ( c1, buf1, 0,  NULL, NULL, tpart->name, c1->get_oper_num(tpart->oper_start)->oldname, NULL, NULL);

                c1->opas_rt_err = mOPRT;
                strcpy(c1->op_error_name, "DOCK");    
                c1->CALC_CANCEL = true;
                return(NULL);
        } /* end of ROUT error */


        toplst->oper = c1->get_oper_num(tpart->oper_start); //  pointing to DOCK oper 
        
        next_op = c1->all_oplst[1];
        
        //   set me_ptr already next_op->index = 1;
        strcpy(next_op->name, "STOCK");    
        next_op->optot = 100.0;
        next_op->rttot = 100.0;
        next_op->oper = find_opas (c1, "STOCK", tpart);
        
   
        next_op = c1->all_oplst[2];  //  scrap oper
        if (next_op == NULL) {
              sprintf_s(buf1,"INSUFFICIENT MEMORY: #(22c) \n Insufficient Memory to calculate \n No. times operations done for product");
              warn_err( c1, buf1, 0,  NULL, NULL, tpart->name, NULL, NULL, NULL);
                return(NULL);
        }

        //  set me_ptr already  next_op->index = 2;
        strcpy(next_op->name, "SCRAP");
        next_op->optot = 100.0;
        next_op->rttot = 100.0;
        next_op->oper = find_opas (c1, "SCRAP", tpart);   
       

        poper =c1->get_oper_num( next_op->oper->oper_ptr +1);  //  gwwd ?   code here !!!
        strcpy(oldname, "SCRAP");
        *num = 2;


		  write_check(c1->varlocal, " at 6-1 ");
        while (poper != NULL)
        {

			 write_check(c1->varlocal, " at 6-1a ");

                //Message(poper->name);
                if (strcmp(oldname,poper->name) == 0) {
                        next_op->optot += poper->cmmprp;
				        sprintf_s(buf2, "%i", poper->opnum);
				}
                else
                {  /* checking prev op */
                        if ( fabs(next_op->optot-100.0) > ROUT_ERR)
							//////   error here !!!    missing addition and  op id/name for results.err
                        {
                               sprintf_s(buf1,"ERROR: #(75) \n For part %s Total Proportion \n Assigned to Operation %s is not equal to 100.0%% ", tpart->name, oldname);
                               write_check(c1->varlocal, buf1);

							   
                               sprintf_s(buf1,"ERROR: #(75) \n Total Proportion \n Assigned to Operation is not equal to 100.0%% ");
                               warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, buf2, sNULL, sNULL);
							   c1->CALC_CANCEL = true;


                                c1->opas_rt_err = mOPASA;
                                strcpy(c1->op_error_name, oldname);
                                c1->CALC_CANCEL = true;
                                return(NULL);
                        } /* end of assign error */

						//  should not be checking stock or scrap !!!
                        if (  (fabs(next_op->rttot-100.0) > ROUT_ERR) &&  (strcmp("STOCK", oldname) != 0)  && (strcmp("SCRAP", oldname) != 0) )
                        {
                               
                              sprintf_s(buf1,"ERROR: #(76c) \n %% of pieces routed \n out of operation does not equal  100.0%% ");
                              warn_err( c1, buf1, 0,  sNULL, sNULL, tpart->name, oldname, sNULL, sNULL);
							  write_check(c1->varlocal, buf1);

                                c1->opas_rt_err = mOPRT;
                                strcpy(c1->op_error_name,oldname);
                                c1->CALC_CANCEL = true;
                                return(NULL);
                        } /* end of rout err */

                        /* next operation */
                       

						
                        next_op= c1->all_oplst[next_op->me_ptr+1]; //  next free space
                        if (next_op == NULL) {
                                sprintf_s(buf1,"INSUFFICIENT MEMORY: #(22d) \n Insufficient Memory to calculate \n No. times operations done for product");
                                warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, sNULL, sNULL, sNULL);
								write_check(c1->varlocal, buf1);
                                return(NULL);
                        }
                         *num += 1;
                        // set me_ptr already  next_op->index = *num;

						//   gwwd 5-11-15  xxxx???
                        strcpy(next_op->name, poper->name);
						next_op->oper = poper;
						//sprintf(buf1, "in count ops %s %i", poper->name, next_op->me_ptr);
						// write_check(c1->varlocal, buf1);
                        next_op->optot   = poper->cmmprp;
						sprintf_s(buf2, "%i", poper->opnum);
                        next_op->rttot   = 0.0;
						
                        
                        strcpy(oldname, poper->name);
                        

                        /* do rttot */
                        oprt = find_rt_from (c1, oldname,  tpart);
                        while ((oprt != NULL) && (strcmp(oldname, c1->get_oper_num(oprt->op_from_ptr)->name) == 0))
                        {
                                next_op->rttot   += oprt->cmmpij;
								oldpart_ptr = oprt->part_ptr;
								oprt = c1->get_route_num(oprt->me_ptr+1);
				                if (oprt != NULL) {
									if (oprt->part_ptr != oldpart_ptr) oprt = NULL;
								}
				        }

                };


				//move to next oper record ......

				oldpart = poper->part_ptr;
                poper = c1->get_oper_num(poper->oper_ptr + 1);  //  xxgwwd next oper ...  9-20 check 
				if (poper != NULL) {
						if (oldpart != poper->part_ptr) { poper = NULL;} 
						// Message( c1,"poper is ok");
								
				} else {
						// write_check(c1->varlocal, "poper is null so it's going to crash");
				}
						
                            // poper = poper->n_opas;  old code 

        }  /* end of while loop */

		  write_check(c1->varlocal, " at 6-0 ");

        if ( fabs(next_op->optot-100.0) > ROUT_ERR)
        {
                sprintf_s(buf1,"ERROR: #(75b) \n For part %s Total Proportion \n Assigned to Operation %s is not equal to 100.0%% ", tpart->name, oldname);
                write_check(c1->varlocal, buf1);

				
                sprintf_s(buf1,"ERROR: #(75b) \n Total Proportion \n Assigned to operation is not equal to 100.0%% ");
                warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, buf2, sNULL, sNULL);
				write_check(c1->varlocal, buf1);
				c1->CALC_CANCEL = true;

                c1->opas_rt_err = mOPASA;
                strcpy(c1->op_error_name, oldname);
                c1->CALC_CANCEL = true;
                return(NULL);
        } /* end of assign error */

          //  gwwd 5-5-15    if ( fabs(next_op->rttot-100.0) > ROUT_ERR)
	    if (  (fabs(next_op->rttot-100.0) > ROUT_ERR) &&  (strcmp("STOCK", oldname) != 0)  && (strcmp("SCRAP", oldname) != 0) )
        {
                sprintf_s(buf1,"ERROR: #(76b) \n For part %s %% of pieces \n routed out of Operation %s does not equal 100.0%% ", tpart->name, oldname);
                write_check(c1->varlocal, buf1);

              sprintf_s(buf1,"ERROR: #(76b) \n %% of pieces \n routed out of operation does not equal 100.0%% ");
              warn_err( c1, buf1, 0,  sNULL, sNULL, tpart->name, oldname, sNULL, sNULL);

                c1->opas_rt_err = mOPRT;
                strcpy(c1->op_error_name, oldname);
                c1->CALC_CANCEL = true;
                return(NULL);
        } /* end of rout err */


        write_check(c1->varlocal, " at 6 end ");

        *num +=1;

		c1->oplstcount = *num;

        return(c1->all_oplst[0]);
}


/* ################################################ */
class_oplst * find_oplst(class_model * c1, char * opname)
/* ################################################ */
{
        class_oplst * next_op;
		int i;
		char s0[MESSAGE_SIZE];

		//sprintf(s0, "in find oplst %i  %s ", c1->oplstcount, opname);
		//write_check(c1->varlocal, s0);

		for (i=0;i<c1->oplstcount; i++) { 

			//sprintf(s0, "in find oplst %i  %s  %s", i, c1->all_oplst[i]->name, opname);
			//write_check(c1->varlocal, s0);

			if (strcmp(c1->all_oplst[i]->name, opname) ==0) {return c1->all_oplst[i];};
		}

		return(NULL);

}

/* ################################################ */
void  clean_oplst( class_model * c1 )
/* ################################################ */
{
        int i;

		for (i=0;i<c1->oplst_count; i++) { 
			strcpy(c1->all_oplst[i]->name,  " ");
			c1->all_oplst[i]->oper = NULL;
			c1->all_oplst[i]->me_ptr = i;
	    	strcpy(c1->all_oplst[i]->name, "");
		}
		c1->oplstcount = 0;


        return ;
}

/* ################################################ */
void load_pij (class_model * c1,  class_oplst * toplst, class_part * tpart, float  pij [], int *num )
/* ################################################ */
{
        int i, j, num1;
        class_route * rt;

		char buf1[MESSAGE_SIZE];

        class_oplst * f_lst;
        class_oplst * t_lst;

		int oldpart;
		char s0 [MESSAGE_SIZE];
		char buf3[MESSAGE_SIZE];


        num1 = *num;
        /* clear array values ? do we need ? */
        for (i=0;i<num1;i++)
		{	//sprintf(s0, "load pij %i ", i);
		    //write_check(c1->varlocal, s0);
                for (j=0;j<num1+1;j++) {
                        pij[ arr(i,j,num1+1) ] = 0.0;
				}
		}

		//sprintf(buf3, "MIDDLE load ");
		//write_check(c1->varlocal, buf3);


        /* assign in standard values
                                    from i to j */
        f_lst = toplst;                                /* go through OPLST */
        while (f_lst != NULL)  //  gwwd here  all from list  
        {
                rt = find_rt_from(c1, f_lst->name, tpart);   /* find from start */
                i    = f_lst->me_ptr;                        /* find from index */
				if (rt != NULL) sprintf(s0, "load pij %i  f_lst  rt %i %i   %s == %s  %i equal", i, rt->op_from_ptr, rt->op_to_ptr, c1->get_oper_num(rt->op_from_ptr)->name, f_lst->name, strcmp(c1->get_oper_num(rt->op_from_ptr)->name, f_lst->name));
		              else  sprintf(s0, "not into loop  %i", i);
              //  write_check(c1->varlocal, s0);

                while ((rt != NULL) && (strcmp(c1->get_oper_num(rt->op_from_ptr)->name, f_lst->name) == 0) )
                        /* go through to list */
                {
					    //sprintf(s0, "load pij  find to oper in oplst %s ", c1->get_oper_num(rt->op_to_ptr)->name);
						//write_check(c1->varlocal, s0);

                        t_lst = find_oplst(c1, c1->get_oper_num(rt->op_to_ptr)->name);

						//if (t_lst != NULL) sprintf(s0, "load pij  rtto op %i   %i %s to opername  ",rt->op_to_ptr, t_lst->me_ptr, t_lst->oper->name);
						//else sprintf(s0, "didn't find to op ...");
						//write_check(c1->varlocal, s0);
                        if ( t_lst == NULL) {
                               

                               sprintf_s(buf1,"MAJOR ERROR: #(26) \n CONTACT BUILD TO DEMAND.COM \n CANNOT FIND 'TO' OPERATION listed as a 'FROM' OPERATION");
                               warn_err( c1, buf1, 0,  sNULL, sNULL, tpart->name, c1->get_oper_num( rt->op_to_ptr)->name, sNULL, sNULL);
					    		write_check(c1->varlocal, buf1);

                                c1->CALC_CANCEL = true;
                                return;
                        }; /* end of if */
                        j     = t_lst->me_ptr;   // set me_ptr already gwwd               /* find to index */
						//sprintf(s0, "load pij  rtto op %i   %i %f  real val ",i, j, rt->cmmpij/100.0);
						//write_check(c1->varlocal, s0);
                        pij[ arr(j,i,num1+1) ] += rt->cmmpij/100.0;

						oldpart = rt->part_ptr;
                        rt = c1->get_route_num(rt->me_ptr+1);
						if (rt != NULL) { 
								if (rt->part_ptr != oldpart) { rt = NULL;}
								else if (strcmp(f_lst->oper->name, c1->get_oper_num(rt->op_from_ptr)->name) !=0) { rt = NULL;}  //   error!!!
						}
						//if (rt  != NULL)  sprintf(s0, "load pij %i  fromopname %s f_lst  rt %i   %s == %s", i ,f_lst->oper->name, rt->op_from_ptr, c1->get_oper_num(rt->op_from_ptr)->name, f_lst->name);
						//  else            sprintf(s0, " break loop ");
		                //write_check(c1->varlocal, s0);
                }            
				/* end of 'to' list */

				//sprintf(s0, "load pij %s %i break loop  f_lst ",  f_lst->name, f_lst->me_ptr);
				//write_check(c1->varlocal, s0);

				//gwwd 3-23-15
				if ((f_lst->me_ptr+1) == c1->oplst_count) f_lst = NULL;
                 else  {f_lst = c1->all_oplst[f_lst->me_ptr + 1];
					if (f_lst->oper == NULL) f_lst = NULL; 
					else {
						//sprintf(s0, "load pij %s %i break loop next f_lst ",  f_lst->name, f_lst->me_ptr);
						//write_check(c1->varlocal, s0);
					}
				 }

				
				if (f_lst != NULL)  sprintf(s0, "load pij %s next  f_lst ",  f_lst->name);
				   else            sprintf(s0, " break loop ");
		       //  write_check(c1->varlocal, s0);

        }    	/* end of 'from' list */
		
		//sprintf(buf1, "end load pij ");
		//write_check(c1->varlocal, buf1);
	
        return;
}
/* ################################################ */
void  pij_prime(class_model * c1, class_oplst * toplst, class_part * tpart, float  pij [], int *num )
/* ################################################ */
{
        int i,j,k;
        int num1;
        float x1;
		char buf1[MESSAGE_SIZE];
        class_oplst * opl;

        num1 = *num;
        /* add loops to move out scrap */
        i = 2;
        for (j=0;j<num1;j++)
        {
                if ((pij[ arr(i,j,num1+1) ] != 0.0)  &&
                    ((1.0 - pij[arr(i,j,num1+1)]) > EPSILON))

                {
                        x1 = 1.0 - pij[ arr(i,j,num1+1)];
                        for (k=0;k<num1;k++)
                                pij[ arr(k,j,num1+1) ] /= x1;
                        pij[ arr(i,j,num1+1)] = 0.0;
                }
                else
                        if (pij[ arr(i,j,num1+1) ] != 0.0)
                        {
                                c1->opas_rt_err = mOPRT;
                                opl = toplst;
                                while (opl != NULL) {
                                        if (opl->me_ptr == j) {    // set me_ptr already  gwwd
											
                                                strcpy(c1->op_error_name,  opl->name);
                                                //sprintf_s(buf1, "ERROR: #(77) \n FOR PART %s All pieces go to SCRAP from \n operation %s", tpart->name, opl->name);
                                                //Message( c1,buf1);

                                                sprintf_s(buf1, "ERROR: #(77) \n All pieces go to SCRAP from an operation.  This is not allowed.  (Hint: send 0.0001%% to STOCK)");
                                                warn_err ( c1, buf1, 0,  sNULL, sNULL, tpart->name, opl->name, sNULL, sNULL);

                                                opl = NULL;
                                        }
                                        else   {//write_check(c1->varlocal, "in pij prime test 3-23-15");
												opl = c1->all_oplst[opl->me_ptr + 1];
										        if (opl->oper == NULL) opl = NULL; 
												//write_check(c1->varlocal, "after pij prime test 3-23-15");
										       } //   was code  opl = opl->n_oplst;
                                } /* end of WHILE */
                                c1->CALC_CANCEL = true;
                                return;
                        }/* end of else \n if */
        } /* end of for */

        return;
}
/* ********************** */
void  transpose ( float  pij[], int *num )
/* ********************** */
{
        float t1, t2;
        int   num1, i,j;

        num1 = *num;

        for (i=0;i<num1;i++)
                for (j=i+1;j<num1;j++)
                {
                        t1 = pij[ arr(i,j,num1+1) ];
                        t2 = pij[ arr(j,i,num1+1) ];
                        pij[ arr(i,j,num1+1) ]= t2;
                        pij[ arr(j,i,num1+1) ]= t1;
                };

        return;
}

/* ********************** */
void  cleanup (class_model * c1,  int   * kwa,  float  * pij,  float * s_pij,  float * mreturn,  class_oplst * toplst)

/* ********************** */
{

	//write_check(c1->varlocal," ïn to clenup");

        if (pij     != NULL) free( pij);
        if (s_pij   != NULL) free( s_pij);
        if (mreturn != NULL) free( mreturn);
        if (kwa     != NULL) free( kwa);
          clean_oplst(c1);

		
	//write_check(c1->varlocal," after to clenup");

        return;
}

