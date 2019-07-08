// dll2.cpp : Defines the initialization routines for the DLL.
//



#include "stdafx.h"
#include "class_model.h"
include "dll2.h"



#ifdef _DEBUG
#define new DEBUG_NEW
#endif

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

   extern "C" int  iclear_model(int collect, char * dir1) { 

	 classmpx_c1 *  c1 = new classmpx_c1;
	 c1 = new classmpx_c1;    //  new class instance created here.

	c1->clear_model_batch(collect, dir1);

	//	~cl;  //  dispose of c1 

};


   
/*
int VBENTRY  irun_all ( int collect, char * dir1,  

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
				int iWID, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
				ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS)  
{
    //************************************************************************************8

      run_all (  collect, dir1,  

			 //General_time (
			  time1,  time2,  u_limit,  lab_var,  eq_var,  part_var,

			  //Labor_data (
			   l_count,    x1 ,    x2,    x3,    x4,
                 x5,    x6,    x7,  x8, 

			  //  Equip_data (
			   eq_count,    e_x1,    e_x2,    e_x3,    e_x4,   e_x5,
                 e_x6,    e_x7,    e_x8,    e_x9,  e_x10,

		        //Parts_data (
				 p_count,    p_x1,    p_x2,    p_x3,    p_x4,
                    p_x5,    p_x6,  p_x7,

		          //Oper_data (
				   op_count,   o_x1,    o_x2,    o_x3,    o_x4,    o_x5,
                     o_x6,    o_x7,    o_x8,    o_x9,
                     o_x10,   o_x11,   o_x12,   o_x13,
                     o_x14,   o_x15,   o_x16,   o_x17,

				   // Route_data (
				    r_count,    r_x1,    r_x2,    r_x3,    r_x4,

				   //Ibom_data (
				    ib_count,    i_x1,    i_x2,    i_x3,



			    //Run_model (
				 iWID,   ierrcode,   ilab_err,   ieq_err, 
                 ipart_err,   ioper_err,   iroute_err,   iibom_err,  full,  MESS);
    

	  return(-1);
}
*/

/* later we add  cppentry  int runall (.......) { 

class_mpx  c1   = new class_mpx;    //  new class instance created here.

	c1.clear_model(collect, dir1);
	c1.general_time(g1, g2, g3, g4...);
	c1.labor_data (l1, l2, ...);

	...

	c1.run(r1, r2, ...);
*/

   //~cl;  //  dispose of c1 



