#include "StdAfx.h"

#include "class_lab.h"
#include "class_eq.h"
#include "class_part.h"
#include "class_oper.h"
#include "class_route.h"
#include "class_ibom.h"

#define EPSILON       1e-6     /* a small number */
#define SSEPSILON     1e-20    /* a VERY small number */
#define ROUT_ERR      0.1      /* size for ROUTING and ASSIGN ERRORs */


#define MAX(a,b)    ( (a>b) ? a : b  )
#define MIN(a,b)    ( (a<b) ? a : b  )
#define round(a)    ( (int) (a+0.5) )



#define arr(i,j,k)  (((i)*(k))+(j))
#define arr3(i,j,k,m,n)  (((i)*(m)*(n))+(j)*(n)+(k))

#define NDI_POW(a,b)   ( (a>EPSILON) ? (pow((double)(a), (double)(b))) : 0.0)



#include "wused.h"
#include "oplst.h"
#include <direct.h>
#include <stdlib.h>
#include <stdio.h>
#define GetCurrentDir _getcwd
#define SetCurrentDir _chdir


#include <iostream>
#include <fstream>
#include <math.h>

using namespace std;

void write_check(char * varlocal, char *  s0);



class class_model
{

//float fvalue_ci  (class_model * c1, float *lot1);
//void  dif1_ci     (class_model * c1, float * vector, float * diff);
	
#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 400

#define FALSE 0 
#define PERM -1

#define EQ_LIMIT 6000

public:  

	char varlocal [BUFLEN];

	int oplstcount;

	int CALC_CANCEL;
	int RESTART_CANCEL;
	int opas_rt_err;
	char  op_error_name [MESSAGE_SIZE];
	int over_util_L;
	int over_util_E;
	int inOPT ;

    int WID;
    float    total_pro;
    float    total_shi;
    float    total_scr;
    float    total_wip;
    float    total_ft ;


	char nam1 [6];
	char nam2 [6];
	char nam3 [6];

	float t1;
	float t2;
	float utlimit;

	float v_part;
	float v_lab;
	float v_equip;

	int lab_point;
	int eq_point;
	int eq_point2;
	int part_point;
	int part_point2;
	int oper_point;
	int route_point;
	int ibom_point;

	int wused_point;
	int oplst_point;

	int lab_count;
	int eq_count;
	int part_count;
	int oper_count;
	int route_count;
	int ibom_count;

	int oplst_count;

	float  tot_gather;
	float  tot_weight;

	//  opt parmeters

	int numberOfParameterAffected;

	float * valueOfParameterAffected;
	class_part ** partPointer;
	int * isLotOrTbatch;

	float tolerance;
	int iter;
	float resultValue;

	int INRUN;
	int FULL;

	class_lab **   all_lab;
	class_eq  **   all_eq;
	class_part**   all_part;
	class_oper**   all_oper;
	class_route**  all_route;
	class_ibom**   all_ibom;

	class_wused **  all_wused;

	class_oplst **  all_oplst;

	 char * maxname;
		


class_model::class_model(int l_size, int e_size, int p_size, int o_size, int r_size, int i_size, char * dir1)
{
		int i;
		char s0 [MESSAGE_SIZE];

	strcpy_s(nam1,"year");
	strcpy_s(nam2,"cday");
	strcpy_s(nam3,"hour");

	write_check(dir1, dir1);

	lab_count = l_size;

	write_check(dir1, "after  c1 start2aa\n");

	sprintf(s0, "lsize %i %i", l_size, lab_count);
	write_check(dir1, s0);

	write_check(dir1, "after  c1 start2b\n");


	all_lab = new class_lab* [l_size];

	for(i=0;i<l_size;i++) { 
		all_lab[i] = new class_lab();
	};
	lab_count = l_size;
	lab_point = 0;


	sprintf(s0, "step2 lsize %i %i", l_size, lab_count);
	write_check(dir1, s0);
	write_check(dir1, "after  c1 start2c\n");

	all_eq= new class_eq  * [e_size];
	for(i=0;i<e_size;i++) { 
		all_eq[i] = new class_eq();
		all_eq[i]->index = i;
		all_eq[i]->lab_ptr  = -1;
	}
	eq_count = e_size;
	eq_point = 0;

	
	all_part= new class_part  * [p_size];
	for(i=0;i<p_size;i++) { 
		all_part[i] = new class_part();
	}
	part_count = p_size;
	part_point = 0;
	for ( i=0;i<part_count; i++) {
		all_part[i]->oper_start = -1;
		all_part[i]->route_start = -1;
		all_part[i]->ibom_start = -1;
	}


	
	all_oper= new class_oper  * [o_size];
	for(i=0;i<o_size;i++) { 
		all_oper[i] = new class_oper;

		all_oper[i]->oper_ptr = i;
		
	    all_oper[i]->eq_ptr  = -1;
		all_oper[i]->part_ptr = -1;
		
	}
	oper_count = o_size;
	oper_point = 0;

	
	all_route= new class_route  * [r_size];
	for(i=0;i<r_size;i++) { 
		all_route[i] = new class_route();
		all_route[i]->part_ptr = -1;
		all_route[i]->op_from_ptr = -1;
		all_route[i]->op_to_ptr = -1;
		all_route[i]->me_ptr = i;
	}
	route_count = r_size;
	route_point = 0;

	
	all_ibom= new class_ibom  * [i_size];
	for(i=0;i<i_size;i++) { 
		all_ibom[i] = new class_ibom;
		all_ibom[i]->prnt_ptr = -1;
		all_ibom[i]->cmpt_ptr = -1;
		all_ibom[i]->me_ptr = i;
	}
	ibom_count = i_size;
	ibom_point = 0;

	
	all_wused= new class_wused  * [p_size];
	for(i=0;i<p_size;i++) { 
		all_wused[i] = new class_wused;
	}
	
	for ( i=0;i<part_count; i++) {
		all_wused[i]->me_ptr = i;
		all_wused[i]->part_ptr = NULL;
		all_wused[i]->cibom2_ptr = NULL;
	}

	wused_point = -1;


	//--------------------------------------------------------------
	all_oplst = new class_oplst  * [o_size];


	oplst_count = o_size;

	for(i=0;i<o_size;i++) { 
		all_oplst[i] = new class_oplst;
	}
	
	for ( i=0;i<o_size; i++) {
		all_oplst[i]->me_ptr = i;
		all_oplst[i]->oper = NULL;
		strcpy(all_oplst[i]->name, "");
	}

	oplst_point = -1;
	oplst_count = o_size;



}


class_model::~class_model(void)
{
		int i;

		for(i=0;i<lab_count; i++) {
		 //delete (all_lab[i]);
		}
		//delete(all_lab);
	
		for(i=0;i<eq_count; i++) {
		 //delete(all_eq[i]);
		}
		//delete(all_eq);

		for(i=0;i<part_count; i++) {
		 //delete(all_part[i]);
		}
		//delete(all_part);

		for(i=0;i<oper_count; i++) {
		 //delete(all_oper[i]);
		}
		//delete(all_oper);

		for(i=0;i<route_count; i++) {
		 //delete(all_route[i]);
		}
		//delete(all_route);

		for(i=0;i<ibom_count; i++) {
		 //delete(all_ibom[i]);
		}
		//delete(all_ibom);

		for(i=0;i<part_count; i++) {
		 //delete(all_wused[i]);
		}
		//delete(all_wused);

		for(i=0;i<oper_count; i++) {
		 //delete(all_oplst[i]);
		}
		//delete(all_oplst);

}

//    methods ....-----------------------------------------------------------------------------
// setups - utilities 




	class_lab *first_lab_ptr()  { 
		lab_point = 0;
			char s0 [MESSAGE_SIZE];

	sprintf(s0, "first lab ptr lab name %s %i",all_lab[0]->name, lab_count);
	write_check(varlocal, s0);

		return all_lab[0];
	};

	class_lab *next_lab_ptr()  { 
		lab_point +=1;
		if (lab_point >=  lab_count) return NULL;
		else {
			char s0 [MESSAGE_SIZE];

	sprintf(s0, "lab name %s  %d\n",all_lab[lab_point]->name, lab_point);
	write_check(varlocal, s0);

           return all_lab[lab_point];
		}
	};

	class_lab *xxadd_lab(char * name1, double size1)  { 
		
		lab_point +=1;
		//all_lab[lab_point]->name = name1;
		//all_lab[lab_point]->num = size1; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_lab[lab_point];
	};

	int find_lab_name( char *  lname1) { 
			int i;
			char s1 [MESSAGE_SIZE];	

			for (i=0;i<lab_count;i++) { 
				sprintf(s1, " checking  eq lab  name %s %s", lname1, all_lab[i]->name);
                write_check(varlocal, s1);

					if (strcmp(all_lab[i]->name, lname1) == 0) {
					    return(i);
					}
			}			
			return (-1);
	}

	class_lab * get_lab_num( int num) {
		if ((num <0 )  ||  (num >= lab_count)) return (NULL);
		return all_lab[num];
	}


//    methods ....--equipment--------------


	class_eq *first_eq_ptr()  { 
		eq_point = 0;
		return all_eq[0];
	};

	class_eq *next_eq_ptr()  { 
		eq_point +=1;
		if (eq_point >=  eq_count) return NULL;
		else return all_eq[eq_point];
	};


	class_eq *first_eq_ptr2()  { 
		eq_point2 = 0;
		return all_eq[0];
	};

	class_eq *next_eq_ptr2()  { 
		eq_point2 +=1;
		if (eq_point2 >=  eq_count) return NULL;
		else return all_eq[eq_point2];
	};

	class_eq *xxadd_eq(char * name1, double size1)  { 
		
		eq_point +=1;
		strcpy(all_eq[eq_point]->name, name1);
		all_eq[eq_point]->num = size1; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_eq[eq_point];
	};

	int find_eq_name( char * lname1) { 
			int i;
			for (i=0;i<eq_count;i++) { 
					if (strcmp(all_eq[i]->name,lname1) == 0) {
					    return(i);
					}
			}			
			return (-1);
	}

	class_eq * get_eq_num( int num) {
		if ((num <0 )  ||  (num >= eq_count)) return (NULL);
		return all_eq[num];
	}

	
//    methods ...products--


	class_part *first_part_ptr()  { 
		part_point = 0;
		return all_part[0];
	};

	class_part *next_part_ptr()  { 
		part_point +=1;
		if (part_point >=  part_count) return NULL;
		else return all_part[part_point];
	};

	

	class_part * first_part_ptr2 (void)  { 
		part_point2 = 0;
		return all_part[0];
	};

	class_part * next_part_ptr2 (void)  { 
		part_point2 +=1;
		if (part_point2 >=  part_count) return NULL;
		else return all_part[part_point2];
	};

	class_part *add_part(char * name1, double size1)  { 
		
		part_point +=1;
		strcpy(all_part[part_point]->name,name1);
		all_part[part_point]->ndmnd = size1; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_part[part_point];
	};

	int find_part_name( char * lname1) { 
			int i;
			for (i=0;i<part_count;i++) { 
					if (strcmp(all_part[i]->name, lname1)==0) {
					    return(i);
					}
			}			
			return (-1);
	}

	class_part * get_part_num( int num) {
		if ((num <0 )  ||  (num >= part_count)) return (NULL);
		return all_part[num];
	}


//    methods ....--operations--------------


	class_oper *first_oper_ptr()  { 
		oper_point = 0;
		return all_oper[0];
	};

	class_oper *next_oper_ptr()  { 
		oper_point +=1;
		if (oper_point >=  oper_count) return NULL;
		else return all_oper[oper_point];
	};

	class_oper *add_oper(char * name1, int opnum)  { 
		
		oper_point +=1;
		strcpy(all_oper[oper_point]->name, name1);
		all_oper[oper_point]->opnum = opnum; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_oper[oper_point];
	};

	int find_oper_name( char * lname1) { 
			int i;
			for (i=0;i<oper_count;i++) { 
					if ( strcmp(all_oper[i]->name, lname1)  == 0) {
					    return(i);
					}
			}			
			return (-1);
	}

	class_oper * get_oper_num( int num) {
		if ((num <0 )  ||  (num >= oper_count)) return (NULL);
		return all_oper[num];
	}
	
	
	
//    methods ...route--


	class_route *first_route_ptr()  { 
		route_point = 0;
		return all_route[0];
	};

	class_route *next_route_ptr()  { 
		route_point +=1;
		if (route_point >=  route_count) return NULL;
		else return all_route[route_point];
	};

	class_route *add_route( double cmmpij)  { 
		
		route_point +=1;
		all_route[route_point]->op_from_ptr = -1;
		all_route[route_point]->op_to_ptr = -1;
		all_route[route_point]->cmmpij = cmmpij; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_route[route_point];
	};

	
	class_route * get_route_num( int num) {
		if ((num <0 )  ||  (num >= route_count)) return (NULL);
		return all_route[num];
	}


//    methods ....--ibom--------------


	class_ibom *first_ibom_ptr()  { 
		ibom_point = 0;
		return all_ibom[0];
	};

	class_ibom *next_ibom_ptr()  { 
		ibom_point +=1;
		if (ibom_point >=  ibom_count) return NULL;
		else return all_ibom[ibom_point];
	};

	class_ibom *add_ibom(char * name1, double upa)  { 
		
		ibom_point +=1;
		all_ibom[ibom_point]->prnt_ptr = -1;
		all_ibom[ibom_point]->cmpt_ptr = -1;
		all_ibom[ibom_point]->upa = upa; 

		//err       l1->name = name1;
		// err  or  strcpy_s(l1->name = name1);
		

		return all_ibom[ibom_point];
	};



	class_ibom * get_ibom_num( int num) {
			char s0 [MESSAGE_SIZE];

			sprintf(s0," in get ibom structure %i, %i ", ibom_count, num);
			write_check(varlocal, s0);

		if ((num <0 )  ||  (num >= ibom_count)) {
				sprintf(s0,"error in get ibom structure %i, %i ", ibom_count, num);
				write_check(varlocal, s0);
			return (NULL);
		}
		return all_ibom[num];
	}

	

	
	void clean_wused() { 
		int i;
		for ( i=0; i<part_count; i++) {
		   all_wused[i]->me_ptr = i;
		   all_wused[i]->part_ptr = NULL;  
		   all_wused[i]->cibom2_ptr = NULL;
	    }
	}


	class_wused * add_wused(void) {
		wused_point++;
		return(all_wused[wused_point]);
	}
	

	//   work ------------------------------------------------------------------------


	  // void write_data (class_model * c1);
	  // void   mpcalc ( class_model * c1 );

	void clear_model_batch(char * dir1) {
		//  set dir  save dir;
		//  clear model ..  done at model setup (before this call)
		SetCurrentDir (dir1);

	}

	/////   for opt code !!!

   int ncom;  // move inside zsub ??

   float *pcom;
   float *xicom;  //(*nrfunc)(float []);  //  moved inside sub ??  to class_model

};  //  end of class //////////////////////////////////////////////////////////////////////////////////////////////////////////


 void  do_all_visits ( class_model * c1 );
 void  check_ops ( class_model * c1, class_part * tpart);
	 void  check_eq ( class_model * c1, class_part * tpart);

void   matinv(int *isol, int *idsol, int nr, int nc, float *a, int mca, int kwa[], float *det);

class_route * find_rt_from (class_model * c1, char * frm_oper_name, class_part *tpart);
class_oper * find_opas (class_model *c1, char * roper_to_op_name, class_part * tpart);

float  get_gather (class_model * c1,  class_part * tpart);
double effabs (class_lab * tlabor);
void Message (class_model * c1, char * str1);
void   calc_op ( class_model * c1, class_part *tpart, class_oper *  toper, float * v1, float * xbarsl, float *xbarrl, int abc);
float calc_xprime( class_model * c1, class_part * tpart, class_oper * toper, float x1,  float x2);

void  warn_err (class_model * c1, char * buf1, int level,  char *  str_l, char *  str_e, char *  str_p, char *  str_o, char *  str_r, char *  str_i);

  // void write_data (class_model * c1);

  // void   mpcalc ( class_model * c1 );

double ggc      (class_model * c1, class_lab * tlabor);
void do_balance (class_model * c1, class_lab * tlabor);

#define EPSILON       1e-6     /* a small number */
#define SSEPSILON     1e-20    /* a VERY small number */
#define ROUT_ERR      0.1      /* size for ROUTING and ASSIGN ERRORs */

#define MAX(a,b)    ( (a>b) ? a : b  )
#define MIN(a,b)    ( (a<b) ? a : b  )
#define round(a)    ( (int) (a+0.5) )

#define arr(i,j,k)  (((i)*(k))+(j))
#define arr3(i,j,k,m,n)  (((i)*(m)*(n))+(j)*(n)+(k))

#define NDI_POW(a,b)   ( (a>EPSILON) ? (pow((double)(a), (double)(b))) : 0.0)




#define  LABOR_T   1
#define  EQUIP_T   2
#define  LABOR_T_GATHER_3	6	
#define  EQUIP_T_GATHER_3	7


#define T_BATCH_TOTAL_LABOR  13
#define T_BATCH_TOTAL_EQUIP  14

#define T_BATCH_PIECE   23
#define T_BATCH_WAIT_LOT  24

#define LABOR_T_GATHER_1  27
#define LABOR_T_GATHER_2  28

#define EQUIP_T_GATHER_1  37
#define EQUIP_T_GATHER_2  38


#define LABOR_1P	47
#define EQUIP_1P	48
#define LABOR_1_TB	57
#define	EQUIP_1_TB	58

#define LABOR_IJK	67
#define EQUIP_IJK	68

#define LABOR_DIF  77
#define EQUIP_DIF  78

#define LABOR_DIF_T   71
#define EQUIP_DIF_T   72

#define LABOR_DIF_1  75
#define EQUIP_DIF_1  76

#define sNULL  "\0"
#define cNULL '\0'

#define  CANCEL          1007

#define mDO_ERR_LOCAL     1008 
#define mOPRT	1010
#define mOPASA  1011

#define EQ_LIMIT 6000
	

void write_check(char * varlocal, char * s0);
void read_check ( char * xx1);




