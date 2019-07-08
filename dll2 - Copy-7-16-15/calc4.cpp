

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
#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 4000


/* local prototypes */

void           get_lot_match (class_model * c1, class_part * tpart);
class_wused *   get_where_used (class_model * c1, class_part * tpart);
void            erase_w_used (class_model * c1);
void           get_cmpt_match (class_model * c1, class_part * tpart);
void            find_max(class_model * c1, char * name1, float *max,  class_part * tpart);


/* ********************************************************************* */
void   do_all_match (class_model * c1)
{
        class_part * tpart;
		class_oper * toper;

        /* *************** */

        /* GETTING TIMES */
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
                get_lot_match  (c1, tpart);
                get_cmpt_match (c1, tpart);

              
        } /* end of while */

        /* *************** */

		
        /* CHANGING TIME INTO Q PIECES */
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) {
			toper = c1->get_oper_num(tpart->oper_start+1);
                tpart->t_w_asm *= tpart->dlam * toper->lsize;
              
        } /* end of while */

        return;
}
/* *************** */

/* ********************************************************************* */
void   get_lot_match (class_model * c1, class_part * tpart)
{
        class_wused *  hd_wu;
        class_wused *  wu1;

        float ls_start, ls_exit, ls_exitx;
        float demon, numem;
        float delay1;
		class_oper * toper;

        /* *************** */
		c1->clean_wused();
        hd_wu = get_where_used (c1, tpart);

        /* *************** */

		int i;

        for(wu1=c1->all_wused[0], i=0; wu1->part_ptr != NULL; i++, wu1=c1->all_wused[i]) { 
			    toper = c1->get_oper_num(tpart->oper_start+1);
                ls_exitx = tpart->lotsiz * toper->lsize;
                ls_exit  = MAX(1.0, ls_exitx);
                ls_start = wu1->part_ptr->lotsiz;

                demon = ls_exit  * tpart->dlam * MIN(1.0, ls_exitx);
                numem = ls_start * wu1->part_ptr->dlam * wu1->cibom2_ptr->upa;
                /* numem is rate of need */
                /* demon is rate of supply */

                /* GATHER */
                if (ls_exit < ls_start)
                        delay1 = (ls_start/ls_exit) / ( tpart->dlam * MIN(1.0, ls_exitx) );

                else if (ls_exit == ls_start)
                        delay1 = 0.0;

                else  { /* ls_exit > ls_start :: ungather */
                        /* num/dem = fraction needed from each lot */

                        if ( ((numem/demon)*ls_exit) > ls_start)  /* UNGATHER */
                                delay1 = ((numem/demon)*ls_exit/ls_start) /  wu1->part_ptr->dlam;
                        else
                            delay1 = 0.0;
                }

                /* group delay times .. */
                tpart->t_w_asm += delay1 * numem/demon;  /* fraction ging this direction */

               
        }

        erase_w_used(c1);

        return;
}
/* ********************************************************************* */
class_wused *   get_where_used (class_model * c1, class_part * tpart)
{
      
        class_part  * mpart;
        class_ibom  * cibom2;

		class_wused * wu1;

		char buf1[MESSAGE_SIZE];

		int i_start;
		int i_count;
		int i;

       
        for(mpart  = c1->first_part_ptr2(); mpart != NULL; mpart = c1->next_part_ptr2()) {

                i_start = mpart->ibom_start;
				i_count = mpart->ibom_cnt;
				int i_ib;
		
				for ( i_ib = i_start; i_ib<i_count+i_start; i_ib++) {
                         cibom2 = c1->get_ibom_num(i_ib); 

                        if (strcmp(tpart->name, c1->get_part_num(cibom2->prnt_ptr)->name) == 0) {

                                /* this mpart is using tpart */

                              
								
								wu1 = c1->add_wused( );
                             
                                wu1->part_ptr = mpart;
                                wu1->cibom2_ptr   = cibom2;
                                

                        } /* end of if */

                       
                } /* end of cibom list */

                
        }  // end of mpart for 

        return( NULL );
}
/* ********************************************************************* */
void  erase_w_used (class_model * c1)
{
       int i;

       for ( i=0;i<c1->part_count; i++) {
		c1->all_wused[i]->me_ptr = i;
		c1->all_wused[i]->part_ptr = NULL;
		c1->all_wused[i]->cibom2_ptr = NULL;
		
	}

        return;
}
/* ********************************************************************* */
void   get_cmpt_match (class_model * c1, class_part * tpart)
{
        class_ibom  * cibom2;
        char big_name [MESSAGE_SIZE];
	    char trash [MESSAGE_SIZE];
   
        float max1;
        float max2;
        float demon, numem;

		class_part * tpart2;
		class_oper * toper;
		int i_count;
		int i_start;
		int i;


        /* *************** */

        find_max(c1, sNULL,    &max1,  tpart);
		strcpy(big_name,  c1->maxname);

        find_max(c1, big_name, &max2,  tpart);
		strcpy(trash, c1->maxname);
		

        /* *************** */
		

		i_start = tpart->ibom_start;
		i_count = tpart->ibom_cnt;

		int i_ib;
		
        for ( i_ib = i_start; i_ib<i_count+i_start; i_ib++) {
                cibom2 = c1->get_ibom_num(i_ib);
		
				tpart2 = c1->get_part_num(cibom2->prnt_ptr);
				toper = c1->get_oper_num(tpart2->oper_start+1);
                demon  = tpart2->dlam * toper->lsize;
                /* demon is rate of supply */

                numem = tpart->lotsiz * tpart->dlam * cibom2->upa;
                /* numem is rate of need */

                if (strcmp(tpart2->name, big_name) != 0)
                       tpart2->t_w_asm += (max1 / 2.0) * numem/demon;
                else
                    tpart2->t_w_asm += (max2 / 2.0) * numem/demon;

                
        }

        return;
}
/* ********************************************************************* */
void   find_max(class_model * c1, char * name1, float *max,  class_part * tpart)
{
        class_ibom  * cibom2;
        float   inter;

		class_part * tpart2;
		class_oper * toper;
		int i_count;
		int i_start;
		int i;

        /* excludes name1 from search for max interarrival time
             returns max as the max interarrival time and
             returns name2 as the name of that component */

        /* *************** */
        *max = 0.0;
        c1->maxname = "";

		
		i_start = tpart->ibom_start;
		i_count = tpart->ibom_cnt;
		int i_ib;
		
        for ( i_ib = i_start; i_ib<i_count+i_start; i_ib++) {
                cibom2 = c1->get_ibom_num(i_ib);
		

				tpart2 = c1->get_part_num(cibom2->prnt_ptr);
				toper = c1->get_oper_num(tpart2->oper_start+1);
  
                if ( strcmp(name1, tpart2->name) != 0) {
                        inter = 1.0 / (tpart->dlam * MIN(1.0, toper->lsize) );
                        if (*max < inter) {
                                *max = inter;
                                 c1->maxname = tpart2->name;
                        }
                }
                
        }
        return;
}
