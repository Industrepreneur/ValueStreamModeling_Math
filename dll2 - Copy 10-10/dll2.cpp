	// dll2.cpp : Defines the initialization routines for the DLL.
//


//#include "streamwriter.h"

#include "stdafx.h"
#include "dll2.h"
#include <fstream>
#include <string>
#include "class_model.cpp"

 // using namespace String;


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
#define  MESSAGE_SIZE 400

 void warn_err_header ( class_model * c1 );

void write_check( char * varlocal, char *  s0);
 void   mpcalc ( class_model * c1 );
 void	write_data ( class_model * c1 );


//
//TODO: If this DLL is dynamically linked against the MFC DLLs,
//		any functions exported from this DLL which call into
//		MFC must have the AFX_MANAGE_STATE macro added at the
//		very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//

// Cdll2App

BEGIN_MESSAGE_MAP(Cdll2App, CWinApp)
END_MESSAGE_MAP()


// Cdll2App construction

Cdll2App::Cdll2App()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance

	//create all properties for this class = globals ?  then the call from the user does the start of the class (creating the globals)
	  //  ?  2nd and third calls get back to the instance for that user?   or all as 1 call ? 

	// getting help for dll in web environment  consultant 
	// ?dll=> 1 sub in the c# code ?   same stuff ?

}


// The one and only Cdll2App object

Cdll2App theApp;



  

// Cdll2App initialization

BOOL Cdll2App::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

   void load_data (class_model * c1,  
	   
	         //General_time (
			 float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

			  //Labor_data (
			  int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
              float *   x5, float *   x6, float *   x7, int * x8, 

			  //  Equip_data (
			  int eq_count, int *   e_x1, int *   e_x2, float *   e_x3, float *   e_x4, float *  e_x5,
              int *   e_x6, float *   e_x7, float *   e_x8, float *   e_x9, int * e_x10,

			  	
		   

		        

				 
		        //Parts_data (
				int p_count, int *   p_x1, float *   p_x2, float *   p_x3, float *   p_x4,
                 float *   p_x5, float *   p_x6, int * p_x7,

		          //Oper_data (
				  int op_count, int *  o_x1, int *   o_x2, int *   o_x3, int *   o_x4, float *   o_x5,
                  float *   o_x6, float *   o_x7, float *   o_x8, float *   o_x9,
                  float *   o_x10,float *   o_x11,float *   o_x12,float *   o_x13,
                  float *   o_x14,float *   o_x15,float *   o_x16,float *   o_x17,

				   // Route_data (
				   int r_count, int *   r_x1, int *   r_x2, int *   r_x3, float *   r_x4,

				   //Ibom_data (
				   int ib_count, int *   i_x1, int *   i_x2, float *   i_x3,

				   // run model 
				   int iWID ) 
   {

	   // write_check(c1->varlocal, "ain load data before gen1 \n");
	   // write_check(c1->varlocal, "ain load data before gen2 \n");
	 

	      c1->lab_count= l_count;
	      c1->eq_count = eq_count;
	      c1->part_count= p_count;
	      c1->oper_count=op_count;
	      c1->route_count=r_count;
	      c1->ibom_count=ib_count;

		 
		   c1->inOPT = false;

	c1->t1 = time1;
	c1->t2 = time2;
	c1->utlimit = u_limit;
	c1->v_lab = lab_var;
	c1->v_part = part_var;
	c1->v_equip = eq_var;
	   //aa;

 char s1 [MESSAGE_SIZE];

		// sprintf(s1, "  time 1 %10.2f ", time1);
        // write_check(c1->varlocal, s1);
		//  sprintf(s1, "  time 1  + c1  %9.2f ", c1->t1);
        //  write_check(c1->varlocal, s1);
		
	


	int i;
	class_lab * tlabor;
	class_eq * teq;
	class_part * tpart;
	class_oper * toper;
	class_route * troute;
	class_ibom * tibom;

	char s2 [400];

	sprintf(s2, "%s\\errors.txt", c1->varlocal);


    ofstream outfile ( s2 ); 
    if (outfile.is_open()) {
		outfile << "hello world! " ;

	    //gwwd  clear errors.txt!!
		outfile.close();
	}   


	//  load labor data 
	for (i=0; i<l_count; i++) { 
	  tlabor = c1-> all_lab[i];
	 
	   sprintf_s(s1, "%i", x1[i]);  //  change num into string...
	   strcpy(tlabor->name, s1);
	  tlabor->num = x2[i];

	  //sprintf(s1, " in lab load num %f ", x2[i]);
	  //write_check(c1->varlocal, s1);

	  tlabor->facovt = x3[i];
	  //sprintf(s1, " in lab load ovt %f ", x3[i]);
	  //write_check(c1->varlocal, s1);

	  tlabor->absrate = x4[i];
	  tlabor->facset = x5[i];
	  tlabor->facrun = x6[i];
	  tlabor->faccvs = x7[i];
	  tlabor->balance = x8[i];
	}

	

	sprintf(s1, "  time 1 %10.2f ", time1);
        //write_check(c1->varlocal, s1);
		// sprintf(s1, "  after labor + c1  %9.2f ", c1->t1);
        // write_check(c1->varlocal, s1);

	//  load eq data 
	for (i=0; i<eq_count; i++) { 
	  teq = c1-> all_eq[i];
	 
	  sprintf_s(s1, "%i", e_x1[i]);  //  change num into string...
	  strcpy(teq->name,s1);

	   // sprintf(s1, "  eq name %i %s", e_x1[i], teq->name);
       // write_check(c1->varlocal, s1);

	  teq->num = e_x2[i];
	  teq->mttf = e_x3[i];
	  teq->mttr = e_x4[i];
	  teq->facovt = e_x5[i];
	  sprintf(s1, "%i", e_x6[i]);
	  teq->lab_ptr = c1->find_lab_name(s1);
	    //sprintf(s1, "  eq lab  name %i %s", e_x6[i], c1->get_lab_num(teq->lab_ptr)->name); //gwwd  why garbage  in %s? ??
        //write_check(c1->varlocal, s1);
		//sprintf(s1, "  eq lab num %i  eqname %s ",  teq->lab_ptr, teq->name);
        //write_check(c1->varlocal, s1);
	  teq->facset = e_x7[i];
	  teq->facrun = e_x8[i];
	  teq->faccvs = e_x9[i];
	  teq->cellid = e_x10[i];
	}
	
	
	
    /*  sprintf(s1, "  time 1 %10.2f ", time1);
        write_check(c1->varlocal, s1);
		 sprintf(s1, " after equip  %9.2f ", c1->t1);
         write_check(c1->varlocal, s1);
		 */

		
	//  load part data 
	for (i=0; i<p_count; i++) { 
	  tpart = c1-> all_part[i];
	 
	  sprintf(s1, "%i", p_x1[i]);  //  change num into string...
	  strcpy(tpart->name, s1);

	  //write_check(c1->varlocal, s1);

	  tpart->ndmnd = p_x2[i];
	  tpart->lotsiz = p_x3[i];
	  tpart->tbatch = p_x4[i];
	  tpart->facdem = p_x5[i];
	  tpart->faccva = p_x6[i];
	  tpart->tgatherbatch = p_x7[i];

	  tpart->ibom_start = -1;  ///   gwwd add 9-18
	  tpart->ibom_cnt = 0;
	  tpart->oper_start = -1;
	  tpart->oper_cnt = 0;
	  tpart->route_start = -1;
	  tpart->route_cnt = 0;
	  tpart->check_rout =  TRUE; 

	  
	}

	sprintf(s1, "  time 1 %10.2f ", time1);
         // write_check(c1->varlocal, s1);
		 // sprintf(s1, "  after part data  %9.2f ", c1->t1);
         // write_check(c1->varlocal, s1);
	
	//  load oper data 
	for (i=0; i<op_count; i++) { 
	  toper = c1-> all_oper[i];
	 
	  sprintf_s(s1, "%i", o_x3[i]);  //  change num into string...
	  toper->part_ptr =  c1->find_part_name(s1);
	  
	  sprintf_s(s1, "%i-%i", o_x3[i], o_x1[i]);  //  change num into string... 
	  strcpy(toper->name, s1);

	  sprintf_s(s1, "%i", o_x1[i]);  //  change num into string...
	  strcpy(toper->oldname, s1);
	  
	  sprintf_s(s1, "%i", o_x4[i]);  //  change num into string...
	  toper->eq_ptr = c1->find_eq_name(s1);

	  toper->opnum = o_x2[i];

	  toper->cmmprp = o_x5[i];
	  toper->esetup = o_x6[i];
	  toper->esetbatch = o_x7[i];
	  toper->esetpiece = o_x8[i];
	  toper->erlot = o_x9[i];
	  toper->erbatch = o_x10[i];
	  toper->epiece = o_x11[i];
	  toper->lsetup = o_x12[i];
	  toper->lsetbatch = o_x13[i];
	  toper->lsetpiece = o_x14[i];
	  
	  toper->lrlot = o_x15[i];
	  toper->lrbatch = o_x16[i];
	  toper->lpiece = o_x17[i];
	}
	
	
  
		// sprintf(s1, "  time 1 %10.2f ", time1);
        // write_check(c1->varlocal, s1);
		// sprintf(s1, "  after oper data %9.2f ", c1->t1);
        //  write_check(c1->varlocal, s1);

	//  load route data 
	for (i=0; i<r_count; i++) { 
	  troute = c1-> all_route[i];
	 
	  sprintf_s(s1, "%i", r_x1[i]);  //  change num into string...
	  troute->part_ptr = c1->find_part_name(s1);

	    // sprintf(s1, "  route part  %i %i %i ", i, r_x1[i], troute->part_ptr);
        // write_check(c1->varlocal, s1);
	  
	  sprintf_s(s1, "%i-%i", r_x1[i], r_x2[i]);  //  change num into string...
	  troute->op_from_ptr = c1->find_oper_name (s1);
	    // sprintf(s1, "  route from op  %i %i %i ", i, r_x2[i], troute->op_from_ptr);
        // write_check(c1->varlocal, s1);
	  
	  sprintf_s(s1, "%i-%i", r_x1[i], r_x3[i]);  //  change num into string...
	  troute->op_to_ptr = c1->find_oper_name (s1);
	    // sprintf(s1, "  route to oper  %i %i %i ", i, r_x3[i], troute->op_to_ptr);
        // write_check(c1->varlocal, s1);

	 

	  troute->cmmpij = r_x4[i];
	}
	
       /*
	   sprintf(s1, "  time 1 %10.2f ", time1);
        write_check(c1->varlocal, s1);
		 sprintf(s1, "  after route data   %9.2f ", c1->t1);
         write_check(c1->varlocal, s1);
		 */
	
	//  load ibom data 
	for (i=0; i<ib_count; i++) { 
	  tibom = c1-> all_ibom[i];
	 
	  sprintf_s(s1, "%i", i_x1[i]);  //  change num into string...
				  // write_check(c1->varlocal, s1);
	  tibom->prnt_ptr = c1->find_part_name(s1);
				  // sprintf(s1, "ibom parent %i %i %i ", i, i_x1[i], tibom->prnt_ptr);
				  // write_check(c1->varlocal, s1);
	  sprintf_s(s1, "%i", i_x2[i]);  //  change num into string...
	  tibom->cmpt_ptr = c1->find_part_name(s1);  
			// sprintf(s1, "ibom component %i %i %i ", i, i_x2[i], tibom->cmpt_ptr);
			// write_check(c1->varlocal, s1);
	  tibom->upa = i_x3[i];

	}

	

	int indx1;
	int indx2;
	tpart = NULL;
	indx1 = -1;
	
	//  set part ptr for ibom..
	for (i=0; i<ib_count; i++) { 
	  tibom = c1-> all_ibom[i];
	  indx2 =   tibom->prnt_ptr;
	  tpart = c1->get_part_num(indx2);
	  // sprintf(s1, "in ibom ptr set %i ", indx2);
	  // write_check(c1->varlocal, s1);
	  if (indx1 != indx2) {
	       // sprintf(s1, "in ibom first set %i %s ", indx2, tpart->name);
	

		    indx1=indx2;
			tpart->ibom_start = i;
			tpart->ibom_cnt = 1;
	  }
	  else {
		  tpart->ibom_cnt ++;
	      //sprintf(s1, "in ibom ptr count %i ", tpart->name, tpart->ibom_cnt);
	  }
	}

		// write_check(c1->varlocal, "after set ibom ptrs ");

		indx1 = -1;
	//  set part ptr for oper..
	for (i=0; i<op_count; i++) { 
	  toper = c1-> all_oper[i];
	  indx2 =   toper->part_ptr;
	  tpart = c1->get_part_num(indx2);
	    // sprintf(s1, "in oper ptr set %i %i ",  i, indx2);
	    // write_check(c1->varlocal, s1);
	  if (indx1 != indx2) {
		    indx1=indx2;
			tpart->oper_start = i;
			tpart->oper_cnt = 1;
	  }
	  else tpart->oper_cnt ++;

	}


	//write_check(c1->varlocal, "after set oper ptrs ");


	 //   add DOCK STOCK and scrap NAMES !!!  LOOP PARTS  START +1, +2
	  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
		 toper = c1->get_oper_num(tpart->oper_start);
		 strcpy(toper->name, "DOCK");
		   // sprintf(s1, "in oper DOCK %i %s ",  tpart->oper_start, toper->name);
	       // write_check(c1->varlocal, s1);
		 toper = c1->get_oper_num(tpart->oper_start+1);
		 strcpy(toper->name, "STOCK");
		 toper = c1->get_oper_num(tpart->oper_start+2);
		 strcpy(toper->name, "SCRAP");
     } 



	//  set part count for route..
	indx1 = -1;
	for (i=0; i<r_count; i++) { 
	  troute = c1->all_route[i];
	  indx2 =   troute->part_ptr;
	  tpart = c1->get_part_num(indx2);
	  // sprintf(s1, "in route ptr set %i ", indx2);
	  // write_check(c1->varlocal, s1);
	  if (indx1 != indx2) {
			tpart->route_start = i;
			tpart->route_cnt = 1;
			indx1 = indx2;
	  }
	  else tpart->route_cnt ++;
	}
	  
	//write_check(c1->varlocal, "after set route ptrs ");

	c1->WID = iWID;

			/*
		sprintf(s1, "  time 1 %10.2f ", time1);
        write_check(c1->varlocal, s1);
		 sprintf(s1, "  time 1  + c1  %9.2f ", c1->t1);
         write_check(c1->varlocal, s1);
		 */

	return;
	
	}
		 
	
void read_check ( string xx1) {

  //  xx1 is file name   

    char s0[MESSAGE_SIZE];

         ifstream infile (xx1);
    if (infile.is_open()) {
            strcpy_s(s0, " ");
            infile.width(1); //read 1 char at a time
        while (s0 != sNULL) {
           infile >> s0;
        }
		infile.close();
    }
    
   
}

     int ii= 0;

void write_check(char * varlocal, char *  s0) {

     //string xx1;
	 char s2 [222];

	 ii ++;
     //xx1 = varlocal;
	 //xx1 += "\\";
	 sprintf(s2, "%s\\errors.txt", varlocal);
    // xx1 += s2;

         ofstream outfile ( s2, ios::app );  //   app =>  append 
		//ofstream outfile ( xx1, ios::app );  //   app =>  append 
    if (outfile.is_open()) {
       
        outfile << s0 << " \n" ;
        outfile.close();
    }
    //  read_check (xx1);
}

   void calc_all (class_model * c1) {

	   //aa;
	   return ;
   }

   void write_results (class_model * c1) {

	class_lab * tlab;
	class_eq * teq;
	class_part * tpart;
	class_oper * toper;
	

	string  xx1;
	char s0 [MESSAGE_SIZE];

	float x1;
	int i;

	int o_start;
	int o_count;

				write_check(c1->varlocal, "in write_results \n");

		//--------------------------------------

	 xx1 = c1->varlocal;
     xx1 += "\\";
     xx1 += "results.gen";

  	ofstream outfile (xx1);
	if (outfile.is_open()) {

		sprintf_s(s0 , " WID,TotalProd,TotalShipped,TotalStarted,Scrap,WIP,FlowTime  \n");
		outfile << s0;
		sprintf_s(s0,"%d,  %.2f, %.2f, %.2f, %.2f, %.2f, %.2f  \n",  c1->WID, c1->total_pro, c1->total_shi, c1->total_scr+c1->total_pro, c1->total_scr, c1->total_wip, c1->total_ft);
         outfile << s0;

		 outfile.close();
	}


		//--------------------------------------
	 xx1 = c1->varlocal;
     xx1 += "\\";
     xx1 += "results.lab";

  	ofstream outfile2 (xx1);
	if (outfile2.is_open()) {

		sprintf_s(s0, "WID,LaborID,SetupUtil,RunUtil,AbsUtil,Idle,Qprocess,QWait,Qtotal\n");
		outfile2 << s0;
		

	 for(tlab  = c1->first_lab_ptr(); tlab != NULL; tlab = c1->next_lab_ptr()) { 
		 sprintf_s(s0, "%d, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n", c1->WID, tlab->name, tlab->uset, tlab->urun, tlab->absrate,
                MAX(0.0, 100.0 - tlab->ul), tlab->qpl, tlab->qwl, tlab->ql);
         outfile2 << s0;

        
     }
	 
		 outfile2.close();
	}

	//--------------------------------------

	
	 xx1 = c1->varlocal;
     xx1 += "\\";
     xx1 +=  "results.eq";

  	ofstream outfile3 (xx1);
	if (outfile3.is_open()) {

		sprintf_s(s0,  "WID,EquipID,SetupUtil,RunUtil,RepUtil,LabWaitUtil,Idle,Qprocess,QWait,Qtotal\n");
		outfile3 << s0;
		
	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) {
		  
		  sprintf_s(s0, "%d, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                   c1->WID, teq->name, teq->uset, teq->urun, teq->udown, teq->uwait, MAX(0.0, 100.0-teq->u), teq->qp, teq->qw, teq->q);
         outfile3 << s0;   
      }
	  
	  outfile3.close();
	}

	//--------------------------------------

	
	 xx1 = c1->varlocal;
     xx1 += "\\";
     xx1 +=  "results.prt";

  	ofstream outfile4 (xx1);
	if (outfile4.is_open()) {

		sprintf_s(s0,   "WID,ProdID,TotalGoodProd,ShippedProd,GoodForAsmProd,ScrapInAsm,Scrap,FlowTime,WIP,LTEquip,LTLabor,LTSetup,LTRun,LTWaitLot,LTWaitAsm,LTGather\n");
		outfile4 << s0;

        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
	  
			  if (tpart->tgatherbatch != 0) {
                tpart->w_lot -= tpart->tgather;
                tpart->tsgood -= tpart->tgather;
                x1 = tpart->tgather * c1->t1 * tpart->dlam * tpart->lotsiz * (1.0 - tpart->scrap);
                tpart->tgather = 0.0;
                tpart->qpart -= x1;
            }

           sprintf_s(s0, "%d, %s, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f \n",
                        c1->WID, tpart->name, tpart->total, tpart->shipped, tpart->demd_ass, tpart->demd_scrap, tpart->scrapped, tpart->tsgood, tpart->qpart,
                        tpart->w_equip, tpart->w_labor, tpart->w_setup, tpart->w_run, tpart->w_lot, tpart->t_w_asm, tpart->tgather);
           outfile4 << s0;   
        }
	  
	  outfile4.close();
	}


				
	//--------------------------------------

	char buf1 [MESSAGE_SIZE];
	
	 xx1 = c1->varlocal;
     xx1 += "\\";
     xx1 +=  "results.opr";

  	ofstream outfile5 (xx1);
	if (outfile5.is_open()) {

		sprintf_s(s0,   "WID,ProdID,OpID,EqSetTime,EqRunTime,LabSetTime,LabRunTime,FlowTime,WIP,LTEquip,LTLabor,LTSetup,LTRun,LTWaitLot,VisitsPer100,VisitsPerGood,AverLotSize,NumSetups\n");

		outfile5 << s0;
     
        for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr())
        {

        
				int i_oper;

               o_start = tpart->oper_start;
			   o_count = tpart->oper_cnt;
			   i_oper = o_start +3;
			    //sprintf(buf1, " part name %s  icount %i %i ", tpart->name, o_start, o_count);
			    //write_check(c1->varlocal, buf1);
				
                for (i_oper= o_start+3; i_oper< o_start+o_count; i_oper++ )                
                {
					toper = c1->get_oper_num(i_oper);
					//sprintf(buf1, " part name %s  icount %i", tpart->name, i);
					//write_check(c1->varlocal, buf1);

                    // USING OLD NAME = OP_ID in the access code !!!
                    sprintf_s(s0, "%d, %s, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                        c1->WID, tpart->name,
                        toper->oldname,
                        toper->ueset,   toper->uerun,   toper->ulset, toper->ulrun, toper->flowtime,  toper->qpoper,
                        toper->w_equip, toper->w_labor, toper->w_setup, toper->w_run, toper->w_lot, toper->vper100, toper->vpergood, toper->lsize, toper->n_setups);
					outfile5 << s0;
                } // for toper

				//  gather at end!!!!
         if (tpart->tgather > EPSILON) {
                toper = c1->get_oper_num(tpart->oper_start+1);
                x1 = tpart->tgather * c1->t1 * tpart->dlam * tpart->lotsiz * (1.0 - tpart->scrap);
                sprintf_s(s0, "%d, %s, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                        c1->WID, tpart->name,
                        c1->get_oper_num(tpart->oper_start+1)->oldname,
                        0.0,                  0.0,             0.0,          0.0,         tpart->tgather*c1->t1,    x1,
                        0.0,                  0.0,              0.0,            0.0,       tpart->tgather*c1->t1, 100.0,            1.0,           tpart->lotsiz * (1.0 - tpart->scrap), 0.0);


				  outfile5 << s0; 
         } // if tgather

       }// for tpart
       
    }
	  
	  outfile5.close();


	//------------------------------------------------------


	   //aa;
	   return ;
   }

   int over_u (class_model * c1) {

	class_lab * tlabor;
	class_eq * teq;

	int ret = false;

	 for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr()) { 
		 if (tlabor->ul > c1->utlimit) { ret = true;}
   }

	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) { 
		    if (teq->u > c1->utlimit) { ret = true;}
   }

	   //aa;
	   return (ret) ;
   }

   void optimize (class_model * c1, int d1) { 


	  //  gwwd code here 
   }

   void  write_lotsizes (class_model * c1 ) { 

	   class_part * tpart;

	   	  for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
		   //  printf (tpart-> lotsize);
   }

   }

   void clear_c1 ( class_model * c1 ) { 

	class_lab * tlabor;
	class_eq * teq;
	class_part * tpart;
	class_oper * toper;
	class_route * troute;
	class_ibom * tibom;

	write_check(c1->varlocal, "in to clear c1");

	 for(tlabor  = c1->first_lab_ptr(); tlabor != NULL; tlabor = c1->next_lab_ptr()) { 
		   delete(tlabor);
   }

	  for(teq  = c1->first_eq_ptr(); teq != NULL; teq = c1->next_eq_ptr()) { 
		   delete(teq);
   }

   for(tpart  = c1->first_part_ptr(); tpart != NULL; tpart = c1->next_part_ptr()) { 
		   delete(tpart);
   }

   for(toper  = c1->first_oper_ptr(); toper != NULL; toper = c1->next_oper_ptr()) { 
		   delete(toper);
   }

    for(troute  = c1->first_route_ptr(); troute != NULL; troute = c1->next_route_ptr()) { 
		   delete(troute);
   }

	 for(tibom  = c1->first_ibom_ptr(); tibom != NULL; tibom = c1->next_ibom_ptr()) { 
		   delete(tibom);
   }

	 int i; 

	 for(i=0; i<c1->part_count; i++) {
		   delete(c1->all_wused[i]);
   }
	 
		 for(i=0; i<c1->oper_count; i++) {
		   delete(c1->all_oplst[i]);
   }
	 

		 delete(c1->all_lab);
		 delete(c1->all_eq);
		 delete(c1->all_part);
		 delete(c1->all_oper);
		 delete(c1->all_route);
		 delete(c1->all_ibom);
		 delete(c1->all_wused);
		 delete(c1->all_oplst);



	write_check(c1->varlocal, "out of clear c1");

	return;
 }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	int run_it	(class_model * c1, char * dir1, 
		 //General_time (
			 float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

			  //Labor_data (
			  int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
              float *   x5, float *   x6, float *   x7, int * x8, 

			  //  Equip_data (
			  int eq_count, int *   e_x1, int *   e_x2, float *   e_x3, float *   e_x4, float *  e_x5,
              int *   e_x6, float *   e_x7, float *   e_x8, float *   e_x9, int * e_x10,

		        //Parts_data (
				int p_count, int *   p_x1, float *   p_x2, float *   p_x3, float *   p_x4,
                 float *   p_x5, float *   p_x6, int * p_x7,

		          //Oper_data (
				  int op_count, int *  o_x1, int *   o_x2, int *   o_x3, int *   o_x4, float *   o_x5,
                  float *   o_x6, float *   o_x7, float *   o_x8, float *   o_x9,
                  float *   o_x10,float *   o_x11,float *   o_x12,float *   o_x13,
                  float *   o_x14,float *   o_x15,float *   o_x16,float *   o_x17,

				   // Route_data (
				   int r_count, int *   r_x1, int *   r_x2, int *   r_x3, float *   r_x4,

				   //Ibom_data (
				   int ib_count, int *   i_x1, int *   i_x2, float *   i_x3,



			    //Run_model (
				int iWID  //, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
				//ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS
				
				)  
{ 

		char s1 [MESSAGE_SIZE];

	write_check(dir1, "at start of run it \n");

	write_check(dir1, "after c1 start4\n");

	char s0 [MESSAGE_SIZE];
		/*
		sprintf(s0, "  time 1 %10.2f ", time1);
        write_check(dir1, s0);
		 sprintf(s0, "  time 2 %9.2f ", time2);
         write_check(dir1, s0);
		
	write_check(dir1, "at start of run it 2 \n");
	*/

	char s2[400];



		strcpy(c1->varlocal, dir1);  //  gwwd  critical !!!

		
		/*

		write_check(dir1, s2);
		write_check(dir1, "after c1 start 5\n");
		/*
		write_check(dir1, "after c1 start 5a\n");
		write_check(c1->varlocal, "after c1 start6\n");
	    write_check(c1->varlocal, "after c1 start6a\n");
		sprintf_s(s2, "%s ", c1->varlocal);
		write_check(dir1, "after c1 start 7\n");
		*/
	
	warn_err_header ( c1 );

	
	load_data (c1,  //General_time (
			 time1, time2, u_limit, lab_var, eq_var, part_var,

			  //Labor_data (
			 l_count,   x1 ,  x2,  x3,  x4,
               x5,  x6,  x7, x8, 

			  //  Equip_data (
			 eq_count,   e_x1,   e_x2,  e_x3,  e_x4, e_x5,
              e_x6,  e_x7,  e_x8,  e_x9, e_x10,

		        //Parts_data (
				p_count,   p_x1,  p_x2,  p_x3,  p_x4,
                  p_x5,  p_x6, p_x7,

		          //Oper_data (
				 op_count, o_x1,  o_x2,  o_x3,  o_x4,  o_x5,
                   o_x6,  o_x7,  o_x8,  o_x9,
                   o_x10,  o_x11,  o_x12,  o_x13,
                   o_x14,  o_x15,  o_x16,  o_x17,

				   // Route_data (
				  r_count,  r_x1,  r_x2,  r_x3,  r_x4,

				   //Ibom_data (
				  ib_count,  i_x1,  i_x2,  i_x3,
				  //  run model 
				  iWID);


	// write_check(dir1, "after load data 5\n");
		
	// write_check(c1->varlocal, "at start of run it \n");



		// sprintf(s1, "  time 1  + c1  %9.2f ", c1->t1);
        //  write_check(c1->varlocal, s1);

		// write_check(c1->varlocal, "after load in run it \n");

	write_data (c1);



		// sprintf(s1, "  time 1  + c1  %9.2f ", c1->t1);
        //  write_check(c1->varlocal, s1);

		 write_check(c1->varlocal, "about to start mpcalc in run it \n");

	   mpcalc (c1);


			write_check(c1->varlocal, "about write_results in run it \n");
      	write_results (c1);
	
		return(-1);
	
	
 }





				

int Optimization(class_model * c1,
										int             numberOfParts,          // the number of products available
                                        int*            partID,                         // pointer to the head of the array of product ID numbers
                                        float*          value,                          // the array of values
                                        int*            optimizeLotSize,        // boolean variable which decides if lot size is to be optimized
                                        int*            optimizeTbatch,         // boolean variable which decides if transfer batch is to be optimized
                                        float*          lotsizeValue,           // original value of lotsize for each part
                                        float*          tbatchValue);        
   

extern "C" int  iopt_all ( char * dir1,  

			 //General_time (
			 float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

			  //Labor_data (
			  int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
              float *   x5, float *   x6, float *   x7, int * x8, 

			  //  Equip_data (
			  int eq_count, int *   e_x1, int *   e_x2, float *   e_x3, float *   e_x4, float *  e_x5,
              int *   e_x6, float *   e_x7, float *   e_x8, float *   e_x9, int * e_x10,

		        //Parts_data (
				int p_count, int *   p_x1, float *   p_x2, float *   p_x3, float *   p_x4,
                 float *   p_x5, float *   p_x6, int * p_x7,

		          //Oper_data (
				  int op_count, int *  o_x1, int *   o_x2, int *   o_x3, int *   o_x4, float *   o_x5,
                  float *   o_x6, float *   o_x7, float *   o_x8, float *   o_x9,
                  float *   o_x10,float *   o_x11,float *   o_x12,float *   o_x13,
                  float *   o_x14,float *   o_x15,float *   o_x16,float *   o_x17,

				   // Route_data (
				   int r_count, int *   r_x1, int *   r_x2, int *   r_x3, float *   r_x4,

				   //Ibom_data (
				   int ib_count, int *   i_x1, int *   i_x2, float *   i_x3,



			    //Run_model (
				int iWID, 
							//, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
							//ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS 
							//	)  

				//  opt stuff 
               int numberOfParts, int * partid,
               float * weight, int * optimizeLotSize, int * OptimizeTbatch, float * lotsizeValue, float * tbatchValue)
{
    //************************************************************************************8

    int i;
	write_check(dir1, "before c1 start opt 1 \n");
	
	class_model * c1 = new class_model(l_count, eq_count, p_count, op_count, r_count, ib_count, dir1);    //  new class instance created here.

	strcpy(c1->varlocal, dir1);

	try {

	c1->clear_model_batch(dir1);  

	write_check(dir1, "starting run it opt...  1 \n");
	write_check(dir1, "before c1 start2 \n");


			i =   run_it(c1, dir1,  time1, time2, u_limit, lab_var, eq_var, part_var,

			  //Labor_data (
			 l_count,   x1 ,  x2,  x3,  x4,
               x5,  x6,  x7, x8, 

			  //  Equip_data (
			 eq_count,   e_x1,   e_x2,  e_x3,  e_x4, e_x5,
              e_x6,  e_x7,  e_x8,  e_x9, e_x10,

		        //Parts_data (
				p_count,   p_x1,  p_x2,  p_x3,  p_x4,
                  p_x5,  p_x6, p_x7,

		          //Oper_data (
				 op_count, o_x1,  o_x2,  o_x3,  o_x4,  o_x5,
                   o_x6,  o_x7,  o_x8,  o_x9,
                   o_x10,  o_x11,  o_x12,  o_x13,
                   o_x14,  o_x15,  o_x16,  o_x17,

				   // Route_data (
				  r_count,  r_x1,  r_x2,  r_x3,  r_x4,

				   //Ibom_data (
				  ib_count,  i_x1,  i_x2,  i_x3, iWID);

			write_check(dir1, "starting run it optimization.  1 \n");

	      Optimization(c1, numberOfParts, partid, weight, optimizeLotSize, OptimizeTbatch, lotsizeValue,  tbatchValue);




	 	clear_c1 (c1);  
		delete (c1);

	} catch (exception ex) {
		try {
			string filePath = "";
			filePath.append(c1->varlocal);
			filePath.append("\\dllerror.log");
			ofstream myfile (filePath);
			if (myfile.is_open()) {
				myfile << ex.what();
			  myfile << "\n";
			  myfile.close();
			}


		} catch (exception ex2) {

		}
	}
	  return(-1);
};



	  extern "C" int irun_it	(		char * dir1, 
		 //General_time (
			 float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

			  //Labor_data (
			  int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
              float *   x5, float *   x6, float *   x7, int * x8, 

			  //  Equip_data (
			  int eq_count, int *   e_x1, int *   e_x2, float *   e_x3, float *   e_x4, float *  e_x5,
              int *   e_x6, float *   e_x7, float *   e_x8, float *   e_x9, int * e_x10,

		        //Parts_data (
				int p_count, int *   p_x1, float *   p_x2, float *   p_x3, float *   p_x4,
                 float *   p_x5, float *   p_x6, int * p_x7,

		          //Oper_data (
				  int op_count, int *  o_x1, int *   o_x2, int *   o_x3, int *   o_x4, float *   o_x5,
                  float *   o_x6, float *   o_x7, float *   o_x8, float *   o_x9,
                  float *   o_x10,float *   o_x11,float *   o_x12,float *   o_x13,
                  float *   o_x14,float *   o_x15,float *   o_x16,float *   o_x17,

				   // Route_data (
				   int r_count, int *   r_x1, int *   r_x2, int *   r_x3, float *   r_x4,

				   //Ibom_data (
				   int ib_count, int *   i_x1, int *   i_x2, float *   i_x3,


			    //Run_model (
				int iWID) //, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
				          //ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS)  
{ 

		int i;


		char buf1 [240];

		sprintf(buf1, "%s\\errors.txt", dir1 );
		
	

		
		write_check(dir1, "before c1 start11\n");
		
		
		
	    class_model * c1 = new class_model(l_count, eq_count, p_count, op_count, r_count, ib_count, dir1);    //  new class instance created here.


		write_check(dir1, "after  c1 start21\n");
		


			i =   run_it(c1,  dir1,  time1, time2, u_limit, lab_var, eq_var, part_var,

			  //Labor_data (
			 l_count,   x1 ,  x2,  x3,  x4,
               x5,  x6,  x7, x8, 

			  //  Equip_data (
			 eq_count,   e_x1,   e_x2,  e_x3,  e_x4, e_x5,
              e_x6,  e_x7,  e_x8,  e_x9, e_x10,

		        //Parts_data (
				p_count,   p_x1,  p_x2,  p_x3,  p_x4,
                  p_x5,  p_x6, p_x7,

		          //Oper_data (
				 op_count, o_x1,  o_x2,  o_x3,  o_x4,  o_x5,
                   o_x6,  o_x7,  o_x8,  o_x9,
                   o_x10,  o_x11,  o_x12,  o_x13,
                   o_x14,  o_x15,  o_x16,  o_x17,

				   // Route_data (
				  r_count,  r_x1,  r_x2,  r_x3,  r_x4,

				   //Ibom_data (
				  ib_count,  i_x1,  i_x2,  i_x3, iWID);


				clear_c1 (c1);
				delete c1;

			  return(i);
   };
			  

			 