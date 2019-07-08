

#include <stdio.h>
#include <string.h>
#include <stdlib.h>


#include "class_model.cpp"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


double ggc      (class_model * c1, class_lab * tlabor);
void do_balance (class_model * c1, class_lab * tlabor);

/*  ******************************************************************* */
void  set_xbar_cs (class_model * c1 )
/*  ******************************************************************* */
{
        class_lab * tlabor;
        class_eq * teq;
        class_part  * tpart;
        class_oper  * aoper;

        float   xbars, xbarr, xbarrl, xbarsl, xbar1, xbar2;
        float   xprime, vlam1, xprsig;
        float   xlabor, xm_only, xl_only, xprsigl;

        char buf1 [1024];

        /* ***************************** */

         for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr()) {
                tlabor->xbarbar = 0.0;
                tlabor->xbard = 0.0;
                tlabor->xsigbar = 0.0;
				if (tlabor->num > 0) {
                   tlabor->cs2  = NDI_POW( tlabor->faccvs*c1->v_lab/100.0, 2.0);
                }
                else {
                    tlabor->cs2  = tlabor->faccvs*c1->v_lab/100.0;
                    tlabor->cs2 *= tlabor->cs2;
                }
				tlabor->ct2 = tlabor->cs2;
              
        }

        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())     {
                teq->xbarbar = 0.0;
                teq->xbard = 0.0;
                teq->xsigbar = 0.0;
        }

		int i;
		int o_count;
		int o_start;
		int i_oper;

        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {
                  o_start = tpart->oper_start;
				  o_count = tpart->oper_cnt;
				
                for (i_oper= o_start; i_oper< o_start+o_count; i_oper++ )                
                {
					aoper = c1->get_oper_num(i_oper);
                        teq = c1->get_eq_num(aoper->eq_ptr);
                        tlabor = c1->get_lab_num( teq->lab_ptr);

                        calc_op (c1, tpart, aoper, &vlam1, &xbarsl, &xbarrl, LABOR_T);
                        xbarrl *= MAX(1.0, aoper->lsize);

                        calc_op (c1, tpart, aoper, &vlam1, &xbars,  &xbarr,  EQUIP_T);
                        xbarr *=  MAX(1.0, aoper->lsize);

                        vlam1    *= MIN(1.0, aoper->lsize);


                        xbar1 = (xbarsl+ xbarrl) * (1.0+tlabor->facovt/100.0);   //  check!!!! divide not multiply .!!!  gwwdcheck  already done?  in calc_op ? 
                        xbar2 = xbars + xbarr;  // xbar2 = ...s / (1.0+teq->facovt/100.0);		//  missing calc2 gwwd  10-6


                        xprime = calc_xprime(c1, tpart, aoper, xbar1, xbar2);
                        xm_only = MAX(0.0,   xbar2-xbar1);
                        if (xbar2 > SSEPSILON) xl_only = MIN(xbar1, xbar2);
                        else             xl_only = xbar1;
                        xl_only *= (1.0 / ( 1.0 - tlabor->absrate/100.0));

                        xprsig = sqrt (  2.0* NDI_POW(teq->mttr,2.0) * teq->imttf*xbar2
                            +  NDI_POW( (1.0 + teq->imttf*teq->mttr),2.0) *
                            NDI_POW( (c1->v_equip * teq->faccvs    * (xm_only)/100.0), 2.0)
                            +  tlabor->ct2 * NDI_POW( (xl_only)*(1.0 + teq->fac_eq_lab),2) );

                            // was
                            // + tlabor->cs2 * NDI_POW((xl_only)*(1.0 + tlabor->fac_eq_lab)/100.0,2) );

                        // sprintf_s(buf1, " eq cs2 %s %f %f %f\n xbar %f %f", teq->name, xm_only, xl_only, teq->fac_eq_lab, xbar1, xbar2);
                        // Message( c1,buf1);

                        teq->xbarbar  += vlam1 * xprime;
                        teq->xbard    += vlam1;
                        teq->xsigbar  +=
                            vlam1 * (  NDI_POW(xprsig,2.0) + NDI_POW(xprime,2.0)  );



                        /* labor stuff now */
                        /*   done these above ...
                                xbar1 /= MAX(1.0, aoper->lsize);
                                vlam1 *= MIN(1.0, aoper->lsize);
                            */


                        xlabor = xbar1 * (1.0 / (1.0 - tlabor->absrate/100.0));

                        xprsigl = (c1->v_lab * tlabor->faccvs * xlabor/100.0);


                        tlabor->xbarbar  += vlam1 * xlabor;
                        tlabor->xbard    += vlam1;
                        tlabor->xsigbar  +=
                            vlam1 * (  NDI_POW(xprsigl,2.0) + NDI_POW(xlabor,2.0)  );

                        // sprintf_s(buf1, " labor cs2 %s %f %f %f ", tlabor->name, tlabor->xbarbar, tlabor->xbard, tlabor->xsigbar);
                        // Message( c1,buf1);

                       
                }  //  end oper loop 

              
        }  //  end tpart loop

        /* ***************************** */

         for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr())
        {

                if (tlabor->num > 0) {
                       if ( tlabor->xbarbar  >  SSEPSILON)
                                tlabor->cs2  = NDI_POW( tlabor->faccvs*c1->v_lab/100.0, 0.9);
                                //tlabor->cs2 = (tlabor->xsigbar * tlabor->xbard / NDI_POW(teq->xbarbar,2.0)) - 1.0;
                        /*  effect of sum(l*v) / sum(l) / sum(l*x) / sum(l)**2 */
                        else
                            tlabor->cs2 = 1.0;
                }
                else {
                    tlabor->cs2  = tlabor->faccvs*c1->v_lab/100.0;
                    tlabor->cs2 *= tlabor->cs2;
                }

                //sprintf_s(buf1, "labor CS2 %s %f ", tlabor->name, tlabor->cs2);
                //Message(buf1);

                if (tlabor->xbard  >  SSEPSILON)
                        tlabor->xbarbar = tlabor->xbarbar / tlabor->xbard;
                else
                        tlabor->xbarbar = 0.0;


               
        }

        /* ***************************** */
        for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr())
        {
                /*  --test for usual server-- */
                if(teq->num >  0)
                {
                        if ( teq->xbarbar  >  SSEPSILON)
                                teq->cs2 = (teq->xsigbar * teq->xbard / NDI_POW(teq->xbarbar,2.0)) - 1.0;
                        /*  effect of sum(l*v) / sum(l) / sum(l*x) / sum(l)**2 */
                        else
                            teq->cs2 = 1.0;
                }
                else {
                    teq->cs2  = teq->faccvs*c1->v_equip/100.0;
                    teq->cs2 *= teq->cs2;
                }
                if (teq->xbard  >  SSEPSILON)
                        teq->xbarbar = teq->xbarbar / teq->xbard;
                else
                        teq->xbarbar = 0.0;

                // sprintf_s(buf1, " eq cs2 %s %f %f %f %f ", teq->name, teq->cs2, teq->xbarbar, teq->xbard, teq->xsigbar);
                // Message( c1,buf1);

				teq->fac_eq_lab = 0.0;

                
        }


        return;
}


double  phi (double u1, double n, double m, double nm_1 );

/* ##################################################### */
void lextra ( class_model * c1 )
/* ##################################################### */
{
        double u1, num, demon, WAIT, nm_1, ca_1, rho1, rho2, s1, s2;
        double tlab_nm, tlab_tpm, tlab_utl, tlab_ca;

        int i, j, k;
		char buf[256];
		char buf1[MESSAGE_SIZE];

        class_lab * tlabor;
        class_eq * teq;

		FILE * fout1;



        /*   ___________________________________________ */


         for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr())
        {
                tlabor->ql  = 0.0;
                tlabor->qwl = 0.0;
                tlabor->fac_eq_lab = 0.0;

				
				// in case ct2 is not set later !!!
				tlabor->ct2  = tlabor->faccvs*c1->v_lab/100.0;
                tlabor->ct2 *= tlabor->ct2;
				
				//fout1 = fopen("xx.1", "wt");
				//fprintf(fout1, "lab cv %s  %f %f \n", tlabor->name, tlabor->ct2, tlabor->cs2);
				//fclose(fout1); 
				tlabor->ct2 = min(4.0, tlabor->ct2);
				tlabor->cs2 = min(4.0, tlabor->cs2);

                if ( tlabor->eq_cover == 0) goto next_lab;

                // sprintf_s(buf1, "labor name :%s: %ld %f", tlabor->name, tlabor->num, tlabor->num_av);
                // Message( c1,buf1);

                if ( (tlabor->num <= 0) ||
                    ((tlabor->num_av >= (SSEPSILON+tlabor->eq_cover) )  && (tlabor->eq_cover > 0)) )
                {
                        	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
                                if (tlabor == c1->get_lab_num(teq->lab_ptr)) {
                                        tlabor->ql  += teq->smbard;
                                } /* end of if correct labor-equip match */
                               
                        } /* end of while teq equip list */
                        // sprintf_s(buf1," - 1 for labor :%s:", tlabor->name);
                        // Message( c1,buf1);
                        goto next_lab;

                }
                else if (tlabor->ul > (c1->utlimit/100.0)) {
                    //   LABOR UTIL > limit !!!
                        if (tlabor->eq_cover > 0)
                                WAIT = tlabor->eq_cover-1;
                        else
                                WAIT = 1000.0;

                        if ( tlabor->xbarbar > SSEPSILON)
                                tlabor->fac_eq_lab = WAIT;
                        else
                                tlabor->fac_eq_lab = 0.0;

                        	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
                                if (tlabor == c1->get_lab_num(teq->lab_ptr)) {

									if (teq->num > 0) {
                                      teq->uwait += ( tlabor->fac_eq_lab * teq->smbard ) / teq->num_av;
									  teq->fac_eq_lab = tlabor->fac_eq_lab;
									}
									else
                                      teq->uwait += 0.0;

                                tlabor->ql +=  (1.0 + tlabor->fac_eq_lab) * teq->smbard;

                                } /* end of if correct labor-equip match */
                              
                        } /* end of while teq equip list */
                }
                else   /* lab< eq_cover   or eq_cover = infinity */
                {

                        tlab_nm   = 0.0;
                        tlab_ca   = 0.0;
                        tlab_tpm  = 0.0;

                        u1 = MIN((c1->utlimit/100.0), tlabor->ul);
                        // sprintf_s(buf1, "u labor %s %f %ld %f", tlabor->name, u1, tlabor->num, tlabor->num_av);
                        // Message( c1,buf1);

                        tlab_tpm = 0.0;
                        tlab_utl = 0.0;
                        	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
                                if (tlabor == c1->get_lab_num(teq->lab_ptr)) {
                                        tlab_tpm += teq->tpm;
                                        tlab_utl += teq->smbard;
                                } /* end of if correct labor-equip match */
                                
                        } /* end of while teq equip list */


                        if (tlab_tpm < SSEPSILON) {
                                sprintf_s(buf1, "WARNING: #(89) \n Labor Group %s is not Used ", tlabor->name);
                                Message( c1,buf1);
                                goto next_lab;
                        }

                        	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
                                if (tlabor == c1->get_lab_num(teq->lab_ptr)) {

                                        if ( teq->num > 0)  {
                                                rho2 = u1;
                                                rho1 = MAX(0.0, (1.0 - (teq->smbard / teq->num_av)));
                                                s2   = tlabor->num_av;
                                                s1   = teq->num_av;

                                                if (s2 < SSEPSILON){
                                                        sprintf_s(buf1, "INTERNAL ERROR: #(XX)\n Number in Labor group is too small.\n Value is %f ??\n", s2);
                                                        warn_err(c1, buf1, 1,  tlabor->name, NULL, NULL, NULL, NULL, NULL);

                                                        sprintf_s(buf1, "INTERNAL ERROR: #(XX)\n Number in Labor group %s is too small.\n Value is %f ??\n", tlabor->name, s2);
                                                        Message( c1,buf1);
                                                        s2 = 1;
                                                }

                                                num  = 1.0 + (teq->cs2-1.0)* rho1*rho1/(NDI_POW(s1,0.5)) -
                                                    (1.0-rho1*rho1) * (1.0-rho2*rho2) +
                                                    (1.0-rho1*rho1) * (tlabor->cs2 - 1.0)* rho2*rho2 / NDI_POW(s2, 0.5);
                                                demon = 1.0 - (1.0 - rho1*rho1) * (1.0 - rho2*rho2);
                                                if ( demon < SSEPSILON) {
                                                        demon = 1.0;
                                                        num = 1.0;
                                                };

                                                if (tlab_utl > SSEPSILON)
                                                        tlab_nm    += teq->smbard * (1.0 - teq->smbard/(tlab_utl*s1));
                                                else
                                                        tlab_nm += teq->smbard * 1.0;
                                                tlab_ca    += num/demon * teq->tpm;

                                        }
                                        else  {
                                                tlab_nm    += teq->smbard * 1.0;
                                                tlab_ca    += 1.0 * teq->tpm;
                                        } /* end of else */

                                } /* end of if correct labor-equip match */
                                
                        } /* end of while teq equip list */

                        if (tlab_tpm < SSEPSILON) goto next_lab;

                        if (tlab_utl > SSEPSILON)
                                nm_1 = tlab_nm / tlab_utl;
                        else
                                nm_1 = 1.0;
                        ca_1 = tlab_ca / tlab_tpm;
                        tlabor->ca2 = ca_1;

                        // old format
                        //f = phi (u1, tlabor->num_av, tlabor->eq_cover, nm_1);
						//
						//
						//	fout1 = fopen("xx.1", "wt");
						//	fprintf(fout1, "lab A cv  %s, %f \n",tlabor->name,  tlabor->ca2);
						//	fclose(fout1); 
							tlabor->ca2 = min(4.0, tlabor->ca2);

						//
                        // new format
						//  added 4/16/04 nm_1
						if (tlabor->eq_cover >0) 
							nm_1 = (tlabor->eq_cover-1)/tlabor->eq_cover;

						
                        WAIT = ggc(c1, tlabor); // also sets lab->ct2
                        WAIT *= nm_1;     // closed population issue

						// sprintf_s(buf, " nm_1 old  %f,  eq_cover %f, num_av %f, WAIT %f", nm_1, tlabor->eq_cover, tlabor->num_av, WAIT);
						///MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  



                        // an old problem fixed now  // line # 392  calc2.cpp
                        if (tlabor->eq_cover > 0) {
                               // WAIT = MIN(WAIT, tlabor->eq_cover-1);  // gwwd 3/5/15  //  
							if (tlabor->num > 0 )  
								WAITgg = MIN(WAIT, (tlabor->eq_cover-1)/tlabor->num_av);
							    else WAIT = 0 ; 
						}

                        if ( tlabor->xbarbar > SSEPSILON)
                                tlabor->fac_eq_lab = WAIT;
                        else
                                tlabor->fac_eq_lab = 0.0;


						// Update to handle labor levelling !!!
						if (tlabor->balance != -1) {  
                       
								  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
									if (tlabor == c1->get_lab_num(teq->lab_ptr)) {
											teq->fac_eq_lab = tlabor->fac_eq_lab;

											if (teq->num > 0)
											    teq->uwait += ( tlabor->fac_eq_lab * teq->smbard ) / teq->num_av;
											else
												teq->uwait += 0.0;

											// sprintf_s(buf1, "teq fac_eq %s \n %f %f %d ", teq->name, teq->uwait, teq->smbard, teq->num);
											// Message( c1,buf1);

											tlabor->ql +=  (1.0 + tlabor->fac_eq_lab) * teq->smbard;
									} /* end of if correct labor-equip match */
									
							} /* end of while teq equip list */
						}
						else  {
						   // balance is set so that wait for labor is not equal for all equipment
							do_balance(c1, tlabor);

								  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
									if (tlabor == c1->get_lab_num(teq->lab_ptr)) {
											//teq->fac_eq_lab = tlabor->fac_eq_lab;

											//if (teq->num > 0)
											//	teq->uwait += ( teq->fac_eq_lab * teq->smbard ) / teq->num;
											//else
											//	teq->uwait += 0.0;

											//sprintf_s(buf1, "teq fac_eq %s \n %f %f %d ", teq->name, teq->uwait, teq->smbard, teq->num);
											//Message( c1,buf1);

											tlabor->ql +=  (1.0 + teq->fac_eq_lab) * teq->smbard;
									} /* end of if correct labor-equip match */
								
							} /* end of while teq equip list */

						}
                }  /* end of else not enough labor go to around */


next_lab:
		i =  0;
                
        }  //  next lab loop

        return;
}
//-----------------------------------------

/*  ------------------------------------------- */
double  phi (double u1, double n, double m, double nm_1 )
/*  ------------------------------------------- */
{
        double x_n, x_m;
        double ret;

        if ( n < 0.0)
                ret = 0.0;
        else if ( u1 < EPSILON )
                ret = 0.0;
        else if ( m < 0.0 )
                ret = NDI_POW(u1, (NDI_POW (2.0*(n+1.0),0.5) - 1.0)) / (n * (1.0 - u1));
        else if ( n >=  m )
                ret = 0.0;
        else
                ret = NDI_POW(u1, (NDI_POW (2.0*(n+1.0),0.5) - 1.0)) / (n  * (1.0 - u1));

        // Labor queue cannot exceed number of equipment to be served -1.
        if (m > 0) ret = MIN(ret, m-1);

        return (ret);
}


//-----------------------------------------
void do_balance(class_model * c1, class_lab * tlabor )
{  
	class_eq * teq;
	double t_wt_lab;
	double t_util;
	double net_util;
	double eq_cnt;
	int    done;

	// compute total wait for labor time
	// compute total util for equipment 

    t_wt_lab = 0.0; //tlabor->absrate * tlabor->num/100.0;
	for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) 

	{
		if ((tlabor == c1->get_lab_num(teq->lab_ptr)) && (teq->num > 0)) {
				
				t_wt_lab += teq->uwait * teq->num;
				t_wt_lab += (tlabor->fac_eq_lab * teq->smbard);
				teq->A = teq->uwait;
				teq->uwait = 0.0;
				teq->fac_eq_lab = -1.0;
		}
		
	}

	done = FALSE;
	while (done == FALSE) {
		eq_cnt = 0;
		t_util = 0;
	    for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
			if ((tlabor == c1->get_lab_num(teq->lab_ptr)) && (teq->num > 0) && (teq->fac_eq_lab ==-1.0) && (teq->smbard >=0)) {
			   eq_cnt += teq->num;
			   t_util += teq->num * (teq->uset +teq->urun +teq->udown);
			}
			
		}

		net_util = 1.0001 * (t_wt_lab + t_util) / eq_cnt;

		
		done = TRUE;
	    for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
			if ((tlabor == c1->get_lab_num(teq->lab_ptr)) && (teq->fac_eq_lab <0) && (teq->num > 0) && ((teq->uset+teq->urun+teq->udown)>net_util) && (teq->smbard >=0)) {
				teq->fac_eq_lab = 0.0;
				done = FALSE;
			}
			
		}
	}
 
	for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
		if ((tlabor == c1->get_lab_num(teq->lab_ptr)) && (teq->num > 0) && (teq->fac_eq_lab < 0.0) && (teq->smbard >=0)) {
			teq->uwait = net_util - (teq->uset+teq->urun+teq->udown);
			if (teq->smbard >=0) 
				teq->fac_eq_lab = (teq->uwait - teq->A) * teq->num / teq->smbard;
			else 
				teq->fac_eq_lab = 0.0;
			
		}
		if ((tlabor == c1->get_lab_num(teq->lab_ptr)) && (teq->fac_eq_lab <0)) {
			teq->fac_eq_lab = 0.0;
		}	
		
	}  //  for eq loop 



return;
}
