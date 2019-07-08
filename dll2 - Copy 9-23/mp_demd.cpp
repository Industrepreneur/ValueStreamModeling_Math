

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



void do_dmnd(class_model * c1,  char * pname, float cmp_dmd, float cmp_scrp);


/* ############################################################### */
void  set_demand ( class_model * c1)
/* ############################################################### */
{
        float   demd_g, demd_s;
        class_ibom  * cibom;
        class_part  * tpart;
		char name1 [NAME_SIZE];

        /* ********************** */

        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {
                tpart->demd_ass   = 0.0;
                tpart->demd_scrap = 0.0;
        }

        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {

                /* USE part as head in IBOM1 list */


                cibom  = c1->get_ibom_num(tpart->ibom_start);
                demd_g = tpart->ndmnd * tpart->facdem;
                if  (tpart->scrap < EPSILON) demd_s = 0.0;
                else     demd_s = demd_g * tpart->scrap;
                /* demnand for assembly */

                while (cibom != NULL)
                {                                 /* demand for components */
                      	strcpy(name1, c1->get_part_num(cibom->cmpt_ptr)->name);
						do_dmnd(c1, name1, demd_g*cibom->upa, demd_s*cibom->upa);
						cibom = c1->get_ibom_num(cibom->me_ptr+1);  
						if ( strcmp(name1,  c1->get_part_num(cibom->cmpt_ptr)->name) != 0) {cibom = NULL;}
                } /* end if ibom2 list */

            
        }

        return;
}
/* ############################################################### */
void  do_dmnd(class_model * c1,  char * pname, float c_dmd, float c_scrp)
/* ############################################################### */
{
        float   demd_g, demd_s;
        class_ibom * cibom;
        class_part * tpart;
		char buf1[MESSAGE_SIZE];
		char  name1[MESSAGE_SIZE];

        tpart = c1->get_part_num(c1->find_part_name(pname));

        tpart->demd_ass   += c_dmd;
        tpart->demd_scrap += c_scrp;

        sprintf_s(buf1," getting comp %s  \n good %f  scrp_ass %f ", pname, c_dmd, c_scrp);
        Message( c1,buf1);


        cibom = c1->get_ibom_num(tpart->ibom_start);  //   check on this!!!!  ok! 
        demd_g = c_dmd;
        if  (tpart->scrap <EPSILON) demd_s = c_scrp;
        else        demd_s = c_dmd * tpart->scrap + c_scrp + c_scrp*tpart->scrap;
        /* demand for assembly */

        while (cibom != NULL)
        {                                 /* demand for components */
				strcpy(name1, c1->get_part_num(cibom->cmpt_ptr)->name);
                do_dmnd(c1, name1, demd_g*cibom->upa, demd_s*cibom->upa);
                cibom = c1->get_ibom_num(cibom->me_ptr+1);  //  move to next ibom on list ...
				if ( strcmp(name1, c1->get_part_num(cibom->cmpt_ptr)->name) != 0) {cibom = NULL;}
				
        } /* end if ibom2 list */

        return;
}
