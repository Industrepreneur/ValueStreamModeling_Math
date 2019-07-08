#include "StdAfx.h"


#include <direct.h>
#include <stdlib.h>
#include <stdio.h>



#include <iostream>
#include <fstream>
#include <math.h>

using namespace std;



#pragma once
class class_lab
{
public:
	class_lab(void);
	~class_lab(void);

	
#define NAME_SIZE 20
       

        //string name; //
	    char name[NAME_SIZE];
		double num;
        double absrate;
        double facovt;
        double facset;
        double facrun;
        double faccvs;

		int    balance;         // tell MPX to balance wait for labor to match util.

        double uset;
        double urun;
        double ul;                /* total utilization */
        double qpl;               /* W-I-P in process */
        double qwl;               /* W-I-P in queue */
        double ql;                /* W-I-P in process + in queue */

        double bc_uset;
        double bc_urun;
        double bc_absrate;           /* saved from input */
        double bc_ul;                 /*  total utilization */
        double bc_qpl;                /*  W-I-P in process */
        double bc_qwl;                /*  W-I-P in queue */
        double bc_ql;                 /*  W-I-P in process + in queue */

        long index;
        int keep;
        


        double num_av;
        double fac_eq_lab;
        double ca2;
        double cs2;
        double ct2;
        double xbarbar;
        double xbard;
        double xsigbar;
        double eq_cover;

		double max_eq_ot;


};

