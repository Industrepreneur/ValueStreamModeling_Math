#include "afx.h"
#include "StdAfx.h"

#include <iostream>
#include <fstream>
#include <math.h>

using namespace std;

#include <direct.h>
#include <stdlib.h>
#include <stdio.h>

#pragma once
#include "class_eq.h"
class class_oper
{
public:
	class_oper(void);
	~class_oper(void);

#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 400

	 int oper_ptr;

	 char name[NAME_SIZE];
	 char oldname[NAME_SIZE];

	 int opnum;
	    int part_ptr;  //  index for part 
        int eq_ptr; //class_eq   * eq_ptr;

		//int route_ptr; //  first from 
		//int route_cnt; //  # of from routes

        double cmmprp;

        double esetup;
        double esetbatch;
        double esetpiece;

        double erlot;
        double erbatch;
        double epiece;

        double lsetup;
        double lsetbatch;
        double lsetpiece;

        double lrlot;
        double lrbatch;
        double lpiece;

        double lvisit;
        double lsize;
        double vper100;
        double vpergood;
        double qpoper;

        double ueset;
        double uerun;
        double ulset;
        double ulrun;

        double flowtime;

        double w_labor; /* time spent waiting for labor */
        double w_equip; /* time spent waiting for equip */
        double w_setup; /* time spent in setup */
        double w_run ; /* time spent in run */
        double w_lot ; /* time spent waiting for rest of lot */

        double n_setups;

        double bc_lvisit;
        double bc_lsize;
        double bc_vper100;
        double bc_vpergood;
        double bc_qpoper;
        double bc_ueset;
        double bc_uerun;
        double bc_ulset;
        double bc_ulrun;
        double bc_flowtime;

        double bc_wlabor; /* time spent waiting for labor */
        double bc_wequip; /* time spent waiting for equip */
        double bc_wsetup; /* time spent in setup */
        double bc_wrun ; /* time spent in run */
        double bc_wlot ; /* time spent waiting for rest of lot */

        double bc_n_setups;

        int keep;
           

        float o1;
        float o2;
        float o3;
        float o4;

        int index;
        float * percent_to;
        float weight;

        float i1;
        float i2;
        float i3;
        float i4;
        float i5;
        float i6;
        float i7;
        float i8;
        float i9;
        float i10;
        float i11;
        float i12;

        float pcass;
};

