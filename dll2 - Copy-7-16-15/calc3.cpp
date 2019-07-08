


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




/*  ############################################ */
int set_cacalc (class_model * c1 )
/*  ############################################ */
/*   local variables */
{
        int  eqi, eqj, i, j;
        float ivlamj, u1;
        float xtemp, ptemp, qtemp;
        float oW;

        int  isol, idsol;
        float det;

		char buf1[MESSAGE_SIZE];
		char buf2[MESSAGE_SIZE];

        class_oper  * aoper;
        class_oper  * oper2;
		class_oper  * oper3;
        class_route * r1;
        class_part  * tpart;
        class_eq * teq;
        class_eq * teq2;

		int oldoperfrom;


		//gwwd  9-20  move to c1 
        float  * lamij;
        int     * kwa;

		
		int ecnt;

        long  get_size;
        /* ________________________________________________ */

mem_again:
		//gwwd  9-20  move to c1 
		ecnt = c1->eq_count;
        get_size = (long) ecnt * (long)(ecnt+1);
        lamij   = (float   * ) calloc (get_size, sizeof(float));
        kwa     = (int     * ) calloc ( ecnt+1, sizeof(int));

        // sprintf_s(buf1, " trying to get mem %f", (float) ecnt*(ecnt+1));
        // Message( c1,buf1);

     

        for (i=0;i<ecnt;i++)
        {
                for (j=0;j<ecnt+1;j++)
                        lamij[ arr(i,j,ecnt+1) ] = 0.0;
        }

        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                teq->A     = 0.0;
                teq->C0    = 0.0;
                teq->C2lam0= 0.0;
                teq->lam0  = 0.0;
                teq->lam   = 0.0;
                teq->nV    = 0.0;
                teq->nW    = 0.0;
                teq->oV    = 0.0;
                teq->P0    = 0.0;
                teq->X     = 0.0;
         
        } /* end of teq equip  loop */


        /*   get Ca Cd....

              get Q2ij, Pij including  0 as   zz4mchgp
                 do lam(i,j), lam(i) (lam(j)=lam(i))
                         (run all operations summing as we go.
         */

        /* ________________________________________________ */
        /* ________________________________________________ */

	int o_count;
	int o_start;
	int i_oper;
	int ij;

	ij = 0;

	// Message( c1,"before part loop");
	for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
	{
		// sprintf(buf1, "i  part loop %s", tpart->name);
	    // write_check(c1->varlocal, buf1);
		o_start = tpart->oper_start;
		o_count = tpart->oper_cnt;


		for (i_oper= o_start; i_oper< o_start+o_count; i_oper++ )                
		{
			// sprintf(buf1, "i  oper loop %i", i);
			// write_check(c1->varlocal, buf1);
			aoper = c1->get_oper_num(i_oper);

			if (aoper == NULL) {
				Message( c1,"ERROR CACALC aoper null");
			}
		
			if (! ((strcmp(aoper->name, "DOCK") == 0) ||   
				(strcmp(aoper->name, "STOCK") == 0) ||
				(strcmp(aoper->name, "SCRAP") == 0)) )

			{
				// Message( c1,"not DOCK STOCK scrap");
				/* __________________________________ */
				/*   AOPER is in assign list
				(eqi is that machine assign to)
				R1->TO_OP is index for TO OPERATIONS in routing list
				OPER2  for ASSIGN TO MACHINE in assign list
				(eqj is that machine assign to for OPER2)
				*/
				teq = c1->get_eq_num(aoper->eq_ptr);
				eqi = teq->index;


                                teq->lam  +=  tpart->dlam * aoper->vper100/100.0;

                                // sprintf_s(buf1, "adding LAM at oper %s %f %s " , teq->name, teq->lam, aoper->name);
                                // write_check(c1->varlocal, buf1);



                                r1  = find_rt_from(c1, aoper->name, tpart );

                                while ((r1 != NULL) && (strcmp(c1->get_oper_num(r1->op_from_ptr)->name, aoper->name) == 0))
                                {
                                        // sprintf_s(buf1, " found from-to %s %s ", c1->get_oper_num(r1->op_from_ptr)->name, c1->get_oper_num(r1->op_to_ptr)->name);
                                        // write_check(c1->varlocal, buf1);



                                        if (! ((strcmp(c1->get_oper_num(r1->op_to_ptr)->name, "DOCK") == 0) ||  	 
                                            (strcmp(c1->get_oper_num(r1->op_to_ptr)->name,  "STOCK") == 0) ||
                                            (strcmp(c1->get_oper_num(r1->op_to_ptr)->name,  "SCRAP") == 0)) )

                                        {

													ij = 0;
                                                oper2 = c1->get_oper_num(r1->op_to_ptr);
												
												//sprintf_s(buf1, " found to %s %s", oper2->name,  c1->get_oper_num(r1->op_to_ptr)->name);
												//write_check(c1->varlocal, buf1);


                                                while ((oper2 != NULL) && (strcmp(c1->get_oper_num(r1->op_to_ptr)->name, oper2->name) == 0))
                                                {
                                                        teq2 = c1->get_eq_num(oper2->eq_ptr);
														eqj  = teq2->index;                     //  teq2  not teq !!!  gwwd error 9-19

														// sprintf_s(buf1, " eq from to %s %i %i", teq2->name, eqi, eqj);
												        // write_check(c1->varlocal, buf1);

                                                        lamij[ arr(eqj,eqi,ecnt+1) ] +=
                                                            tpart->dlam * aoper->vper100/100.0 * r1->cmmpij/100.0 * oper2->cmmprp/100.0;

													
                                                        // sprintf_s(buf1, "adding f-to LAM id %i  got %s %f teq2  \n eqj, eqi %i %i  per %f" , oper2->oper_ptr, teq2->name, teq2->lam, eqj, eqi,oper2->cmmprp );
                                                        // write_check(c1->varlocal, buf1);

														oper3 = c1->get_oper_num(oper2->oper_ptr+1); 
														if (oper3 != NULL) { if(oper3->part_ptr != oper2->part_ptr) oper3 = NULL;
														                         else if(strcmp(oper3->name, oper2->name) !=0) oper3 = NULL;  //???
														}

														if (oper3 == NULL)
															sprintf(buf1, "oper3 = null");
														else
															sprintf(buf1, "oper3 != NULL");

														//write_check(c1->varlocal, buf1);

														oper2 = oper3;    //  same name diff eq ?

                                                } /* end of oper2 while */
                                        } /* end of if */

										ij +=1;
										if (ij > 20) return(0);

										// sprintf(buf1, "need shift here %i ", i);
										// write_check(c1->varlocal, buf1);
										
										oldoperfrom = r1->op_from_ptr;
										r1 = c1->get_route_num(r1->me_ptr+1);
										if(r1 != NULL) {  if (c1->get_part_num(r1->part_ptr) != tpart) { r1 = NULL;}
										                       else if (r1->op_from_ptr != oldoperfrom) r1 = NULL;
										}

                                        
                                }; /* end of r1 while */

								 //write_check(c1->varlocal, "end of first  stuff");

                        }  /* end of NOT DOCK,STOCK,scrap if */



			          

                        /* ________________________________________________ */
                        /*      DOCK P0.... */

                        else if (strcmp(aoper->name, "DOCK") == 0) 
                        {

                                 // write_check(c1->varlocal, "into second  stuff");

								r1  = find_rt_from(c1, "DOCK", tpart );

                                while ((r1 != NULL) && (strcmp(c1->get_oper_num(r1->op_to_ptr)->name, "DOCK") == 0))
                                {


                                        if (! (( strcmp(c1->get_oper_num(r1->op_to_ptr)->name, "STOCK") == 0) ||    
                                            (strcmp(c1->get_oper_num(r1->op_to_ptr)->name,  "SCRAP") == 0)) )

                                        {
                                                oper2 = c1->get_oper_num(r1->op_to_ptr);

                                                while ((oper2 != NULL) && (strcmp(c1->get_oper_num(r1->op_to_ptr)->name, oper2->name) == 0))
                                                {
                                                        teq2 = c1->get_eq_num(oper2->eq_ptr);
                                                        eqj  = teq2->index;

                                                        xtemp =  tpart->dlam * (1.0 + tpart->scrap) * r1->cmmpij/100.0 * oper2->cmmprp/100.0;

                                                        teq2->P0  +=  xtemp;
                                                        teq2->lam0   += xtemp;
                                                        teq2->C2lam0 += xtemp * (tpart->faccva * c1->v_part/100.0) * (tpart->faccva * c1->v_part/100.0);
                                                        teq2->oV     += xtemp*xtemp;

                                                        // sprintf_s(buf1, "ADDING p0  got %s %f teq2  p0" , teq2->name, teq2->P0);
                                                        // Message( c1,buf1);

                                                        // sprintf_s(buf1, "ADDING C2 at 0 got %s %f teq2  c2lam0" , teq2->name, teq2->C2lam0);
                                                        // Message( c1,buf1);


                                                        
                                                } /* end of oper2 while */
                                        } /* end of if */


                                        oldoperfrom = r1->op_from_ptr;
										r1 = c1->get_route_num(r1->me_ptr+1);
										if (c1->get_part_num(r1->part_ptr) != tpart) { r1 = NULL;}
										else if (r1->op_from_ptr != oldoperfrom) r1 = NULL;   

                                } /* end of r1 while */
								// write_check(c1->varlocal, " between of r1 while end of dock  ");

                        } /* end of end of DOCK if */

						// write_check(c1->varlocal, " after dock if ");
                        
                } /* end of aoper for loop */

				// write_check(c1->varlocal, " end of aoper loop  ");
             
        } /* end of tpart loop */

        /* ___________________________________________ */


        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                eqi = teq->index;


		if ( teq->lam > SSEPSILON)
		{
			ivlamj = 1.0 / teq->lam;
			teq->P0  /= teq->lam;
			if ( teq->oV > SSEPSILON)
			{
				if ( teq->lam0 < SSEPSILON) teq->oV = 0.0;
				else teq->oV  =  NDI_POW(teq->lam0,2.0) / teq->oV;
				u1 = MIN(teq->u, c1->utlimit);
				oW     = 1.0 + 4.0*NDI_POW((1.0-u1),2.0) * (teq->oV - 1.0);
				if ( oW  > SSEPSILON)
					oW = 1.0 / oW;
				else
				{
					oW = 1.0;
					sprintf_s(buf1, "MAJOR ERROR: #(47a) \n CONTACT BUILD TO DEMAND.COM \n warning at line 113");
					warn_err ( c1, buf1, 1,  NULL, NULL, NULL, NULL, NULL, NULL);
				} /* end of if oW */
			} /* part of if oV */
			else
			{
				oW = 0.0;
				// sprintf_s(buf1, " Warning at line 114 %s ", teq->name);
				// Message( c1,buf1);
			} /* end of else oV */


                        /*  we need to add calculation of weights for
                              the Coefa * lambda(i)/sum(lambda) */

                        if ( teq->lam0 > SSEPSILON)
                                teq->C0  = (1.0 - oW) + oW * teq->C2lam0/teq->lam0;
                        else
                                teq->C0  = 1.0;

                        sprintf_s(buf1," using c2 %s %f %f %f\n %f ", teq->name, teq->lam0, teq->C2lam0, oW, teq->C0);
                        //Message( c1,buf1);
						sprintf_s (buf2, " set cacalc: %s \n", buf1);
						write_check(c1->varlocal, buf2);
                } /* end of True if lam */

                else
                    {
                        ivlamj = 0.0;
                        teq->P0  = 0.0;
                        teq->C0  = 1.0;
                } /* end of if lam */


                if ( teq->P0 < SSEPSILON) teq->nV = 0.0;
                else teq->nV = NDI_POW(teq->P0,2.0);

                sprintf (buf1, " start nV  %s \n teq->nV %f P0 %f ", teq->name, teq->nV, teq->P0 );
				sprintf(buf2, "setcacalc:: %s \n", buf1);
				write_check(c1->varlocal, buf2);
                //DB_Message (buf1);

                for (eqj=0; eqj<ecnt; eqj++)
                {
                        ptemp = lamij[ arr(eqi,eqj,ecnt+1) ] * ivlamj;
                        teq->nV += ptemp*ptemp;
                        // sprintf_s (buf1, " check nV  %s \n lamij %f ivlamj  %f ", teq->name, lamij[ arr(eqi,eqj,ecnt+1)], ivlamj );
                        // write_check(c1->varlocal, buf1);
                }

                if (teq->num > 0)
                        teq->X = 1.0 + NDI_POW(teq->num,-0.5)  * (MAX(0.2,teq->cs2) - 1.0);
                else
                        teq->X = teq->cs2;

                /*  vj and wj  = .... */
                if ( teq->nV > SSEPSILON)
                {
                        teq->nV = 1.0 / teq->nV;
                        u1 = MIN(teq->u, c1->utlimit);
                        teq->nW = 1.0 + 4.0 * NDI_POW((1.0 - u1),2.0) * (teq->nV-1.0);

                        if ( teq->nW > SSEPSILON)
                                teq->nW = 1.0 / teq->nW;
                        else
                        {
                                sprintf_s (buf1, "MAJOR ERROR: #(47b) \n CONTACT BUILD TO DEMAND.COM \n Inversion of arrival variances Equipment \n ");
                                write_check(c1->varlocal, buf1);

                                warn_err ( c1, buf1, 1,  NULL, teq->name, NULL, NULL, NULL, NULL);
                                /* return (FALSE); */
                                teq->nW = 1.0;
                        };
                } /* end of TRUE nV if */
                else
                    teq->nW = 0.0;
                /* end of nV if */

                
        } /* end of teq equip  loop */


				//write_check(c1->varlocal, "after eq load ...");
        /*   _____________________________________________ */


        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                eqi = teq->index;
                teq->A = 0.0;
                if ( teq->lam > SSEPSILON)
                        ivlamj = 1.0 / teq->lam;
                else
                    ivlamj = 0.0;

                for(teq2  = c1->first_eq_ptr2(); teq2 != NULL; teq2 = c1->next_eq_ptr2())
                {
                        eqj = teq2->index;

                        ptemp = lamij[ arr(eqi,eqj,ecnt+1)] * ivlamj;

                        if ( teq2->lam > SSEPSILON)
                                qtemp = lamij[arr(eqi,eqj,ecnt+1)] / teq2->lam;
                        else
                            qtemp = 0.0;

                        // sprintf_s(buf1, "MAKING LAMIJ  %f  \n P:%f  Q:%f IV:%f\n eqI eqJ %d %d" , lamij[ arr(eqi, eqj, ecnt+1)],  ptemp, qtemp, ivlamj,  eqi, eqj);
                        // Message( c1,buf1);

                        u1 = MIN(teq2->u, c1->utlimit);
                        lamij[ arr(eqi,eqj,ecnt+1)] = teq->nW * ptemp * qtemp * ( 1.0 - NDI_POW(u1,2.0) );

                        sprintf(buf1, "GOT lamij %f \n U:%f  W:%f X:%f", lamij[ arr(eqi, eqj, ecnt+1)],  u1, teq->nW, teq2->X);
						sprintf(buf2, "calc::: %s \n", buf1);
						write_check(c1->varlocal, buf2);
                        //DB_Message(buf1);

                        teq->A  +=  ptemp*
                            ((1.0 - qtemp) + qtemp * NDI_POW(u1,2.0) * teq2->X );


                       
                }  /* end of teq2 equip  loop */


                sprintf(buf1, "teqa %s %f %f %f \n w %f", teq->name, teq->A,teq->C0, teq->P0, teq->nW);
				sprintf(buf2, "calc:: %s \n", buf1);
				write_check(c1->varlocal, buf2);
                //DB_Message(buf1);

                teq->A = 1.0 + teq->nW * ( (teq->P0*teq->C0 -1.0) + teq->A);
                lamij[ arr(eqi,ecnt,ecnt+1)] = -1.0 * teq->A;

                sprintf(buf1, "AFTa %s %f A %d eqI %d ecnt", teq->name, teq->A, eqi, ecnt );
				sprintf(buf2, "calc:: %s \n", buf1);
				write_check(c1->varlocal, buf2);
                //DB_Message(buf1);

                
        } /* end of teq equip  loop */

		//write_check(c1->varlocal, "after  equip 2  load ...");




        for (eqi=0;eqi<ecnt;eqi++) lamij[arr(eqi,eqi,ecnt+1)] -= 1.0;

        /*   _____________________________________________ */
        
           for (i=0;i<ecnt;i++)
             for (j=0;j<ecnt;j++)
              {sprintf(buf2, "\n ij, value %d %d %f \n", i,j, lamij[arr(i,j,ecnt+1)]);
			   write_check(c1->varlocal, buf2);
		   }


           for (i=0;i<ecnt;i++)
             { sprintf(buf2, "\n last col value %d %f \n", i, lamij[arr(i,ecnt,ecnt+1)]);
			   write_check(c1->varlocal, buf2);
			}
        


        /*   _____________________________________________ */


        /* ********************** */
        /* matinv */

        matinv(&isol, &idsol, ecnt, ecnt+1, lamij, ecnt+1, kwa, &det);

        if ((isol != 1) || (idsol != 1)) {
                //Message("INTERNAL ERROR: #(48) \n Numerical Problems \n Equipment Varability Routing cannot be solved");

                sprintf_s(buf1, "INTERNAL ERROR: #(48) \n Numerical Problems \n Equipment Varability Routing cannot be solved");
                warn_err ( c1, buf1, 1,  NULL, NULL, NULL, NULL, NULL, NULL);

                //if (lamij   != NULL) ;//free( lamij);
                //if (kwa     != NULL) ;//free( kwa);
                return (FALSE);
        };
        /* ********************** */
        /* test and mult back visit values */

        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                eqi = teq->index;
                teq->ca2 = lamij [ arr(eqi,ecnt,ecnt+1) ];

                sprintf(buf1, " got %s %f ca2 %i  index  ", teq->name, teq->ca2, eqi);
				sprintf(buf2, "calc:: %s \n", buf1);
				write_check(c1->varlocal, buf2);
                //DB_Message(buf1);

               
        } /* end of teq equip  loop */


		
        //if (lamij != NULL)   ;//free( lamij);
        //if (kwa   != NULL)   ;//free( kwa);
        return(TRUE);
}

