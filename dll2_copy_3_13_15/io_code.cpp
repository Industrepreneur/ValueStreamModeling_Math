


//#include "streamwriter.h"

#include <iostream>
#include <fstream>
// #include <math.h>




using namespace std;

#include "stdafx.h"
#include "dll2.h"

// #include <math.h>
#include <io.h>

#include "class_model.cpp"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define EPSILON       1e-6     /* a small number */
#define SSEPSILON     1e-20    /* a VERY small number */
#define ROUT_ERR      0.1      /* size for ROUTING and ASSIGN ERRORs */

#define MAX(a,b)    ( (a>b) ? a : b  )
#define MIN(a,b)    ( (a<b) ? a : b  )


void  warn_err_header ( class_model * c1 )
{ int i;
 char s2 [222];
 char buf1 [500];


 
     write_check(c1->varlocal, "At header results.err");

	 sprintf(s2, "%s\\results.err", c1->varlocal);


    ofstream outfile ( s2 ); 
    if (outfile.is_open()) {

		sprintf( buf1, "Level,Table,Text,mpxFlag,laborid,equipid,partid,operID,routeId,ibomID\n");
		outfile  <<  buf1;
		outfile.close();
	}
	return;
}
       

void  warn_err (class_model *c1,  char * buf1, int level,  char *  str_l, char *  str_e, char *  str_p, char *  str_o, char *  str_r, char *  str_i)
{ int i;

  string xx1; //char xx1 [MESSAGE_SIZE];
  char buf [MESSAGE_SIZE];
  char buf2 [400];

  for(i=0;i<strlen(buf1); i++) {
    if (buf1[i] == '\n') buf1[i] = '-';
  }

  xx1 = c1->varlocal; //al;  //strcpy_s (xx1,c1->varlocal);
  xx1 	  += "\\";  //strcat_s(xx1, "\\");
  xx1 	  +=  "results.err"; //strcat_s(xx1, "results.err");


  write_check(c1->varlocal, "At prob warn err results.err");

  sprintf(buf2, "warning/error!!! %d   %s   %s %s %s   %s %s %s", level, buf1, str_l, str_e, str_p, str_o, str_r, str_i);
  write_check(c1->varlocal, buf2);
  write_check(c1->varlocal, buf2);

 ofstream errfile (xx1);
  if (errfile.is_open()) {

     sprintf(buf1, "%d,In Calculations,%s, 1 ,%s,%s,%s,%s,%s,%s\n", level, buf1, str_l, str_e, str_p, str_o, str_r, str_i);
     errfile << buf1;

     errfile.close();
 }
}


char * x_strleft(char * s0, int num)
{    s0[num] = '\0';
     return(s0);
}


/* adding local strupr2 change to upper case */
/* ---------------------------------------- */
char * x_strupr2 ( char * s)
/* ---------------------------------------- */
{
        int i;

		

        i = 0;
        while (s[i] != cNULL)
        {
                s[i] = toupper(s[i]);
                i++;
        };

        return(s);
}

/* ################### */
char * x_xspace( char * str1 )
{
        int i,j;
		
        char str2 [5*MESSAGE_SIZE];

        i = 0;
        while ((str1[i] != cNULL) && (str1 [i] == ' ')) i++;

        j = strlen(str1);
        if (j == 0) return(str1);
        while ((j>0) && (str1 [j-1] == ' ')) j--;


        if ((j-i) > 0)
        {
                strncpy(str2, &str1[i], j-i);
                str2[j-i] = cNULL;
                  strcpy (str1,str2);
        }
        else
            {
                str1[0] = cNULL;
                str1[1] = cNULL;
        }

        x_strupr2(str1);

          return(str1);
}

/* ************************** */
/*
          fndecpt.c                        6/10/90

        % strfixdp

        Routines for using fixed decimal points in numeric fields.

        C-scape 3.2
        Copyright (c) 1990, by Oakland Group, Inc.
        ALL RIGHTS RESERVED.

        Revision History:
        -----------------
         6/10/90 mla        rewrote from scratch
         8/23/90 gwwd       add boolean fix for absolute fixed...
*/





boolean        x_is_sn(char *num, char dpc, char **dp, char **end)
/*
 *        Determines if num is in scientific notation by searching for 'E' or 'e'.
 *        only if num is not in scientific notation does it report end of string
 *        and location of decimal point thru extra parameters end and dp.
 */
{
        for (; *num != '\0'; num++) {

                if (*num == dpc) {
                        *end = num + 1;
                        while (**end != '\0') {

                                if (toupper(**end) == 'E') {

                                        return (TRUE);
                                }
                                (*end)++;
                        }
                        *dp = num;
                        return (FALSE);
                }

                if (toupper(*num) == 'E') {

                        return (TRUE);
                }
        }

        *end = num;
        *dp = NULL;

        return (FALSE);
}


int  x_strround(char *num, char *dp, int fixdec, int len)
/* round num at dp + fixdec */
{
        char *dec, *i; /* points to active decimal digit */

        if  ( *(dp+fixdec+1) < '5') return(0); /* no need to round */


        /* MUST round*/
        dec  = (dp+fixdec);

        while (dec >= num) /* stop if dec=0 (set) or dec=num (beginning of string) */
        {
                if (dec == dp)  dec --;

                if ( *(dec) == '9') {
                        *(dec) = '0';
                        dec--;
                        {
                                if (dec == (num-1) )
                                        /* number too large for field */
                                        /* shift it down... */
                                {
                                        *num = '1';
                                        for (i=(num+1);i<(num+len);i++) *(i) = '0';
                                        *(dp+1)='.';
                                        return(1);
                                };
                        };
                }
                else
                    {
                        *(dec) +=1;
                        dec = 0;
                }; /* done */
        }



        return(0);
}

char        * x_strfixgd(char *num, int fixdec, int len, boolean fix)
/*
 *        fixes fixdec decimal places in num.  assumes num is a string of digits
 *        with no leading spaces.  maximum amount of storage for num is len.
 *        if fixdec is zero, num is fixed to a whole number.
 */
{
        char        *dp, *end;
        int                curdec, i;
        char        *iz;
        boolean     allz;
        double      f1;
        int         shift;
        /*
         *        if num is in scientific notation, do nothing.
         *  else find the decimal point and end.
         */


        if (x_is_sn(num, '.', &dp, &end)) {
                return (num);
        }

        f1 = atof (num); /* get the actual value */


        if (dp == NULL) {

                /*  num has no decimal point  */

                /*  if storage space is inadequate, clip least significant digits  */
                if (len < (i = strspn(num, "0123456789")) + 1 + fixdec) {
                        fixdec = len - i - 1;
                }

                if (fixdec <= 0) {
                        return(num);
                }

                /* add enough trailing zeroes */
                *end = '.';
                for (end++; fixdec > 0; fixdec--, end++) {
                        *end = '0';
                }
                *end = '\0';

                return(num);
        }

        /*  else, num has a decimal point  */


        /* if storage space is inadequate, clip least significant digits */
        if (len < (dp - num) + 1 + fixdec) {
                fixdec = len - (dp - num) - 1;
        }


        /*       if (fixdec == 0) {
         *                                *dp = '\0';
         *                                return(num);
         *               }
         */  /* HANDLE 0 AS A STANDARD TRUNCATE */


        curdec = end - dp - 1;

        if (curdec < fixdec) {
                /*  add some decimal places  */

                for (i = (fixdec - curdec); i > 0; i--, end++) {
                        *end = '0';
                }
                *end = '\0';
        }

        if (curdec > fixdec) {
                /*  remove some decimal places  */

                /*                cheesy, broken round off code which has been ousted, for now ...
                 *
                 *                if (*(dp + fixdec + 1) > '4') {
                 *                        *(dp + fixdec) += 1;
                 *                }
                 */

                /*  truncate  */
                /*  from input  side  KEEP all non-zero digits */
                if (fix == FALSE) {
                        /*  ZEROS ONLY !!! FROM RIGHT END */
                        for (iz=(end-1);iz>(dp+fixdec);iz--) {
                                if ( *(iz) == '0' ) *(iz) = '\0';
                                else  break;
                        }
                        if ( *(dp+1) == '\0') *(dp) ='\0';  /* erase dp if at end of number */
                } /* END OF IF FIX == FALSE */

                /*  from output side  truncate if number is large */
                if (fix == TRUE) {
                        if (fabs(f1) > 0.1)  {
                                shift = 0;
                                if ( *(dp+fixdec+1) > '4')  /* round number before truncate */
                                        shift = x_strround(num, dp, fixdec, len);
                                if (fixdec == 0) *(dp+shift) = '\0';
                                else *(dp + fixdec + shift +1) = '\0';
                        }
                        else   {
                                /* f1 <.1 and keep 2 digits if there are 2 digits */
                                /* check if last two digits are zeros */
                                allz = TRUE;
                                for (iz=dp+1;iz<end;iz++) {
                                        shift = 0;
                                        if ( *(iz) != '0') {
                                                allz = FALSE;
                                                if ( (iz+2)<end ) {
                                                        shift = x_strround (num, dp, (iz+1-dp), len);
                                                        *(iz+2+shift) = '\0';
                                                };
                                                break;
                                        }
                                } /* end of for loop */
                                if (allz == TRUE)   {
                                        if (fixdec == 0) *(dp) = '\0';
                                        else *(dp + fixdec + 1) = '\0';
                                } /* end of if allz  == true */
                        }/* end of else */
                }/* end of if fix */

        }/* end of if truncate */

        if ((curdec == fixdec) && (dp+1 == end)) *dp= '\0';
        /* if correct no. and dp is last char then truncate dp */


        return(num);
}
/* _______________________ */
string  pf_format (float f1, int declen, int fieldlen, boolean fixed)
/* _______________________ */
{
        float f2;
        int nlen, i, dlength, len_max;
        char str1[NAME_SIZE];
		char buf1[MESSAGE_SIZE];

		string s0;
		char str2 [MESSAGE_SIZE];
		


        if ( fabs(f1) > SSEPSILON) {
                f2 = log10(fabs(f1));
                if (f2 < 0) f2 = -1*f2 + 2;
                nlen = ceil(f2) + ((f1<0) ? 1:0);  /*number of digits + 1 if<0*/
                if ( nlen > fieldlen) {
                        f2 = log10(fabs(f1));
                        if (fieldlen > 6) {
                                sprintf_s(buf1, "%%.%dfE%%d", fieldlen-6);
                                sprintf(str2, buf1, f1/NDI_POW(10,(double)f2), (int)f2);
                                s0 = str2;
                                // strright(str2, fieldlen);
                                return (s0);
                        }
                        else  {  /* num is too large and field len is too short ??? */
                                sprintf_s(buf1, "%%%ddE%%3d", fieldlen-4);
                                sprintf(str2, buf1, round( (double) f1/ NDI_POW(10,(double)f2)), (int) f2 );
									//round((double)f1/NDI_POW(10,(double)f2)), (int)f2);
                                
                                for (i=0;i<strlen(str2);i++) {
                                        if (str2[i] == ' ') str2[i] = '0';
                                };
                                s0 = str2;
                                return(s0);
                        }

                } /* end if nlen ... end of num too large for field */
                else{
                        sprintf_s(str1, "%%%df", fieldlen);
                        sprintf(str2, str1, f1);
                        x_xspace(str2);
                        dlength = MAX(0, strpbrk(str2, ".")-str2);  /* find length to . point, if none then 0 */
                        len_max = MAX(dlength, fieldlen+1);
                        if (strlen(str2) > len_max) str2[len_max+1] = cNULL;
                        x_strfixgd(str2, declen, fieldlen, fixed);
                        // strright(str2, fieldlen);
                        str2[fieldlen] = '\0';
						s0 = str2;
                        return(s0);
                }
        }
        else {   /* number is very very small */
                strcpy(str2, "0.0");
                //xspace(str2);
                x_strfixgd(str2, declen, fieldlen, fixed);
                // strright(str2, fieldlen);
        }
        str2[fieldlen] = '\0';
		s0 = str2;
        return(s0);
}

/* _______________________ */
void  x_pl_format (long i1, int declen, int fieldlen, boolean fixed, char * str2)
/* _______________________ */
{
        float f2;
		char buf1 [MESSAGE_SIZE];

        sprintf(str2, "%ld", i1);

        if ( fabs((float) i1) > EPSILON) {
                f2 = log10(fabs((float) i1));
                if ( (int) fabs(f2) > (fieldlen-2)) {
                        sprintf(buf1, "%%.%dfE%%d", fieldlen-6);
                        sprintf(str2, buf1, ((float) i1)/NDI_POW( 10.0, (double) f2));
                        x_xspace(str2); 
                        // strright(str2, fieldlen);
						return;
                };
        }
        else
            sprintf(str2, "%ld", i1);
        x_xspace(str2);
        x_strfixgd(str2, declen, fieldlen, fixed);
        // strright(str2, fieldlen);

        return;
}
/* _______________________ */
void  x_strunc (char * str1, int size, char * str2)
/* _______________________ */
{
        int i;

        for (i=0;i<size;i++) str2[i] = ' ';

        for (i=0;i<size;i++)
        {
                if (str1[i] == '\0') break;
                str2[i] = str1[i];
        }

        str2[size] = NULL;

        return;
}


///////////////////////////////////////////////////////////////
void write_mem_check  (class_model * c1) 
///////////////////////////////////////////////////////////
{
char buf1 [ 4000];

	class_oper * toper;
		toper = c1->get_oper_num(4);
		sprintf_s(buf1, "a checking where lpiece  4 %s %f \n", toper->name, toper->lpiece);
		write_check(c1->varlocal, buf1);
		toper = c1->get_oper_num(8);
		write_check(c1->varlocal, buf1);
		write_check(c1->varlocal, buf1);
		if ( toper != NULL) {

			sprintf_s(buf1, "checking where lpiece 8 %s \n", toper->name);   //  !!!!????
			write_check(c1->varlocal, buf1);

			sprintf_s(buf1, "checking where lpiece 8 %f \n", toper->lpiece);
			write_check(c1->varlocal, buf1);
		} else 
			write_check(c1->varlocal, " toper 8 null  !!");
}
/**************************************************************/
   void write_data (class_model * c1) {
/**************************************************************/
	   
	   
	class_lab * tlab;
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

	
	     char d2 = '"';
		 char s0 [BUFLEN*6];
		 char s1 [MESSAGE_SIZE];  //string s1;

		 

	ofstream outfile ("mpx.mdl");
if (outfile.is_open()) {

	write_check( c1->varlocal, "start write check");

sprintf(s0, "GENERAL \n");
         outfile << s0;

        sprintf(s0, "* \n" );
         outfile << s0;
        sprintf(s0, "* TITLE LINE  \n %s\n",    " no title " );
         outfile << s0;

		 
		
        sprintf(s0, "* \n" );
         outfile << s0;
       

        sprintf(s0, "* \n" );
         outfile << s0;
        sprintf(s0, "*                                    FOR ENTIRE PLANT      For 1 LABOR SHIFT     \n");
         outfile << s0;
        sprintf(s0, "* OPERATION  FLOW TIME    DEMAND  ");
         outfile << s0;
        sprintf(s0, " %s ",c1->nam1); //strunc(  c1->nam1, 4, s0);
         outfile << s0;
       
       sprintf(s0, " %s ",c1->nam2); //strunc(  c1->nam1, 4, s0);
         outfile << s0;
    
       sprintf(s0, " %s \n",c1->nam3); //strunc(  c1->nam1, 4, s0);
         outfile << s0;

		
		 //	 sprintf(s0, "  time 1  + c1  %9.2f ", c1->t1);
         // write_check(c1->varlocal, s0);

		sprintf(s0, "  time 1 %10.2f ", c1->t1);
         outfile << s0;
		 	sprintf(s0, "  time 2 %9.2f ", c1->t2);
         outfile << s0;
	
	
        
        sprintf(s0, "* \n" );
         outfile << s0;

		
        sprintf(s0, "*                PART       LABOR    EQUIPMENT \n"     );
         outfile << s0;
        sprintf(s0, "* UTILIZATION   ARRIVAL    SERVICE    SERVICE  \n");
         outfile << s0;
        sprintf(s0, "*   LIMIT     VARIABILITYVARIABILITYVARIABILITY\n");
         outfile << s0;

      
        sprintf(s0, " %9f", c1->utlimit);
         outfile << s0;

         sprintf(s0, "      %9f", c1->v_part);
		 outfile << s0;
        sprintf(s0, "      %9f", c1->v_lab);
		 outfile << s0;
        sprintf(s0, "      %9f", c1->v_equip);
		 outfile << s0;

        sprintf(s0, "\nDONE\n" );
         outfile << s0;

		 // sprintf(s0, "  write data time 1  + c1  %9.2f ", c1->t1);
         // write_check(c1->varlocal, s0);

		
		 
        // ################# 

		 	

        sprintf(s0, "* \n" );
         outfile << s0;
        sprintf(s0, "LABOR   \n" );
         outfile << s0;


        sprintf(s0, "* \n" );
         outfile << s0;
        sprintf(s0, "* \n" );
         outfile << s0;
        sprintf(s0, "* LABOR            ");
         outfile << s0;
		sprintf(s0, " NO. OF                          SETUP        RUN      SERVICE     ");
         outfile << s0;
        sprintf(s0, "\n");
         outfile << s0;

        sprintf(s0, "* GROUP            ");
         outfile << s0;
		sprintf(s0, " PEOPLE    ABSENTEE  OVERTIME    TIME         TIME     VARIABILITY ");
         outfile << s0;
        sprintf(s0, "\n");
         outfile << s0;

        sprintf(s0, "* NAME             ");
         outfile << s0;
        sprintf(s0, " IN GROUP  RATE (%%)  RATE (%%)  MULTIPLIER  MULTIPLIER  MULTIPLIER \n");
         outfile << s0;
        

		  // write_check(c1->varlocal, s0);

		 //  sprintf(s0, "lab start %i %s %f", c1->lab_count, c1->all_lab[0]->name, c1->all_lab[0]->num);
		 //  write_check(c1->varlocal, s0);

	
       for (tlab=c1->first_lab_ptr(); tlab != NULL; tlab = c1->next_lab_ptr()  ) {

            
                sprintf(s0, "%s", tlab->name);
         outfile << s0;
        //write_check(c1->varlocal, s0);


                        sprintf(s0, "      %9f", tlab->num);
		outfile << s0;
                        sprintf(s0, "      %9f",  tlab->absrate);
         outfile << s0;
                        sprintf(s0, "      %9f",  tlab->facovt);
         outfile << s0;
                        sprintf(s0, "      %9f", tlab->facset);
         outfile << s0;
                        sprintf(s0, "      %9f",  tlab->facrun);
         outfile << s0;
                        sprintf(s0, "      %9f", tlab->faccvs);
         outfile << s0;
                         
                sprintf(s0, "\n");
         	 outfile << s0;
        }

        sprintf(s0, "DONE\n" );
         outfile << s0;
	
        // ################# 
        // ################# 

        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "EQUIP   \n" );
         outfile << s0;
   


        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "* EQUIPMENT        " );
         outfile << s0;
        sprintf_s(s0, " NO. OF  ASSOCIATED             MEAN    MEAN              SETUP         RUN       SERVICE    ");
         outfile << s0;
        
        sprintf_s(s0, "\n");
         outfile << s0;

        sprintf_s(s0, "* GROUP            " );
         outfile << s0;
        sprintf_s(s0, " PIECES  LABOR                TIME TO  TIME TO  OVERTIME  TIME          TIME      VARIABILITY ");
         outfile << s0;
        sprintf_s(s0, "\n");
         outfile << s0;

        sprintf_s(s0, "* NAME           " );
         outfile << s0;
        sprintf_s(s0, " IN GROUP  GROUP                FAILURE  REPAIR   RATE (%%)  MULTIPLIER  MULTIPLIER  MULTIPLIER ");
         outfile << s0;
        sprintf_s(s0, "\n");
         outfile << s0;

       for(teq=c1->first_eq_ptr(); teq!= NULL; teq=c1->next_eq_ptr() ) {

						sprintf(s0, " %s", teq->name);
         outfile << s0;
         // write_check(c1->varlocal, s0);

                        sprintf(s0, " %i",teq->num);
         outfile << s0;
                        sprintf(s0, " %s",  c1->get_lab_num(teq->lab_ptr)->name);
         outfile << s0;
                        sprintf(s0, " %f", teq->mttf);
         outfile << s0;
                        sprintf(s0, " %f", teq->mttr);
         outfile << s0;
                        sprintf(s0, " %f", teq->facovt);
         outfile << s0;
                        sprintf(s0, " %f", teq->facset);
         outfile << s0;
                        sprintf(s0, " %f", teq->facrun);
         outfile << s0;
                        sprintf(s0, " %f", teq->faccvs);
         outfile << s0;
         
                sprintf_s(s0, "\n");
         outfile << s0;
        
        }
        sprintf_s(s0, "DONE\n" );
         outfile << s0;

		  write_mem_check(c1);


		 // write_check( c1->varlocal, "in  write check");
		 write_check( c1->varlocal, "in  write check2");

		
		 write_mem_check(c1);

		
        // ################# 

        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "PART    \n" );
         outfile << s0;
        


        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "* PART            " );
         outfile << s0;
        sprintf_s(s0, "                      TRANSFER               ARRIVAL     ");
         outfile << s0;
        sprintf_s(s0, "\n");
         outfile << s0;

        sprintf_s(s0, "* NAME            " );
         outfile << s0;
        sprintf_s(s0, " EXTERNAL   PART       LOT       DEMAND      VARIABILITY ");
         outfile << s0;
        sprintf_s(s0, "\n");
         outfile << s0;


        sprintf_s(s0, "* NUMBER          " );
         outfile << s0;
		sprintf_s(s0, "  DEMAND    LOTSIZE    SIZE      MULTIPLIER  MULTIPLIER  ");
         outfile << s0;
         sprintf(s0, " %s " , c1->nam2);
         outfile << s0;
        sprintf_s(s0, "\n");
         outfile << s0;

       for (tpart=c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()  ) {


		   		 write_mem_check(c1);
                
                sprintf_s(s0, "%s  ", tpart->name );
         outfile << s0;
		// write_check(c1->varlocal, s0);

                        sprintf(s0, "%f " , tpart->ndmnd);
           outfile << s0;
                        sprintf(s0, "%f " ,tpart->lotsiz);
         outfile << s0;
                        sprintf(s0, "%f " , tpart->tbatch);
         outfile << s0;
                        sprintf(s0, "%f " , tpart->facdem);
         outfile << s0;
                        sprintf(s0, "%f " , tpart->faccva);
         outfile << s0;
                        sprintf(s0, " %i ", tpart->tgatherbatch);
         outfile << s0;
                
                sprintf_s(s0, "\n");
         outfile << s0;

        }
        sprintf_s(s0, "DONE\n" );
         outfile << s0;
	
        // ################# 

       for (tpart=c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()  ) {



                sprintf_s(s0, "* \n" );
         outfile << s0;
                sprintf_s(s0, "OPERATION\n" );
         outfile << s0;
                sprintf_s(s0, "* PART    \n" );
         outfile << s0;
                sprintf_s(s0, "* NAME    \n" );
         outfile << s0;
                sprintf_s(s0, "%s \n",  tpart->name);
         outfile << s0;


                sprintf_s(s0, "* OPERATION         EQUIPMENT          PROPORTION  EQUIPMENT   EQUIPMENT     LABOR      LABOR   ");
         outfile << s0;
                sprintf_s(s0, "\n");
         outfile << s0;

                sprintf_s(s0, "* NAME                NAME              ASSIGNED   SETUP TIME  RUN TIME    SETUP TIME  RUN TIME ");
         outfile << s0;
                
                
                sprintf_s(s0, "\n");
         outfile << s0;

		 int i_oper;
			
			o_start = tpart->oper_start;
			o_count = tpart->oper_cnt;
			i_oper = o_start +3;
				
                for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ ) 

               
                {
					toper = c1->get_oper_num(i_oper);

                     sprintf(s0, "%s", toper->name);
         outfile << s0;
                     sprintf(s0, " %s ", c1->get_eq_num( toper->eq_ptr)->name);

         outfile << s0;
                     sprintf(s0, " %f ", toper->cmmprp);
         outfile << s0;

                    sprintf(s0, " %f ", toper->esetup);
         outfile << s0;
                     sprintf(s0, " %f ",  toper->esetbatch);
         outfile << s0;
                     sprintf(s0, " %f ", toper->esetpiece);
         outfile << s0;

                     sprintf(s0, " %f ", toper->erlot);
         outfile << s0;
                     sprintf(s0, " %f ",  toper->erbatch);
         outfile << s0;
                     sprintf(s0, " %f ", toper->epiece);
         outfile << s0;

                      sprintf(s0, " %f ", toper->lsetup);
         outfile << s0;
                      sprintf(s0, " %f ", toper->lsetbatch);
         outfile << s0;
                      sprintf(s0, " %f ", toper->lsetpiece);
         outfile << s0;

                     sprintf(s0, " %f ", toper->lrlot);
         outfile << s0;
                     sprintf(s0, " %f ",  toper->lrbatch);
         outfile << s0;
                     sprintf(s0, " %f ",  toper->lpiece);
         outfile << s0;

                        sprintf_s(s0, "\n");
         outfile << s0;
                      
                } //  end oper list 

                sprintf_s(s0, "DONE\n" );
         outfile << s0;

}  //  end part list 

  // sprintf(s0, "  after opers part count  %i ", c1->part_count);
  // write_check(c1->varlocal, s0);
	

        // ################# 

        for (tpart=c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()  ) {

			 sprintf(s0, "  in routepart count  %s ",  tpart->name);
           // write_check(c1->varlocal, s0);

                sprintf_s(s0, "* \n" );
         outfile << s0;
                sprintf_s(s0, "ROUTING\n" );
         outfile << s0;
                sprintf_s(s0, "* PART    \n" );
         outfile << s0;
                sprintf_s(s0, "* NAME    \n" );
         outfile << s0;
                sprintf_s(s0, "%s \n",  tpart->name);
         outfile << s0;
		// write_check(c1->varlocal, s0);


                sprintf_s(s0, "* FROM              TO                              \n");
         outfile << s0;
                sprintf_s(s0, "* OPERATION         OPERATION            PROPORTION \n");
         outfile << s0;
                sprintf_s(s0, "* NAME              NAME                  ASSIGNED  \n");
         outfile << s0;
		// write_check(c1->varlocal, s0);


		 int i_r;
		 r_count = tpart->route_cnt;
		 r_start = tpart->route_start;
         for(i_r=r_start; i_r< r_start +r_count; i_r++) {
			  troute = c1->get_route_num(i_r);

                        sprintf_s(s0,"%s ",  c1->get_oper_num(troute->op_from_ptr)->name);
         outfile << s0;
		// write_check(c1->varlocal, s0);

                        sprintf_s(s0, "%s ",  c1->get_oper_num(troute->op_to_ptr)->name);
         outfile << s0;
                        sprintf_s(s0, "%f  \n", troute->cmmpij);
         outfile << s0;
		// write_check(c1->varlocal, s0);

		 // sprintf(s0, "  in routepart count 2 %s %s %f \n ",  c1->get_oper_num(troute->op_from_ptr)->name, c1->get_oper_num(troute->op_to_ptr)->name, troute->cmmpij );
         // write_check(c1->varlocal, s0);

                }  //  route
                sprintf_s(s0, "DONE\n" );
         outfile << s0;
}			//  end parts


	
        // ################# 



        sprintf_s(s0, "* \n" );
         outfile << s0;
        sprintf_s(s0, "IBOM     \n" );
         outfile << s0;

		 
		  

		 
     
        for (tpart=c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()  ) {

			// sprintf(s0, "  in ibom  part count  %s  %i %i ",  tpart->name, tpart->ibom_cnt, tpart->ibom_start);
           // write_check(c1->varlocal, s0);
			// write_check(c1->varlocal, s0);
  
                if (tpart->ibom_cnt > 0)  //  gwwd change 9-18
                {
                        sprintf_s(s0, "********************\n" );
       
		

		 outfile << s0;
		// write_check(c1->varlocal, s0);

                        sprintf_s(s0, "*             PART   \n" );
         outfile << s0;
                        sprintf_s(s0, "*             NAME   \n" );
         outfile << s0;

                        sprintf_s(s0, "ASSEMBLY       %s \n",   tpart->name);
         outfile << s0;
		// write_check(c1->varlocal, s0);
                        sprintf_s(s0, "*                              PART                UNITS PER SALVAGE  \n");
         outfile << s0;
                        sprintf_s(s0, "*                              NAME                ASSEMBLY  RATE (%%) \n");
         outfile << s0;

		 i_start = tpart->ibom_start;
		 i_count = tpart->ibom_cnt;

		 int i_ib;

		// write_check(c1->varlocal, s0);
		
        for ( i_ib = i_start; i_ib<i_count+i_start; i_ib++) {	

			//sprintf(s0, " about to start loop  %i ", i);

			// write_check(c1->varlocal, s0);

            tibom = c1->get_ibom_num(i_ib);

			//write_check(c1->varlocal, s0);
				

			sprintf(s0, " %s    component  %s %f \n", c1->get_part_num(tibom->prnt_ptr)->name,  c1->get_part_num(tibom->cmpt_ptr)->name,tibom->upa);
           outfile << s0;
		 // write_check(c1->varlocal, s0);
          
	
           // write_check(c1->varlocal, s0);
			
                                
		}   // for loop              

         sprintf_s(s0, "********************\n" );
         outfile << s0;
                }  //  end if cnt >0
			
                // sprintf(s0, "  end ibom  part count  %s ",  tpart->name);
				// write_check(c1->varlocal, s0);

        }  //  end part loop 

        sprintf_s(s0, "DONE\n" );
         outfile << s0;


        sprintf_s(s0, "DONE\n" );
         outfile << s0;
       
		 outfile.close();

}   //  if openfile 



 return;
}
