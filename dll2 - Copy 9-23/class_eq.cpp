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
class class_eq
{
public:  

	~class_eq(void);
	class_eq(void);
	
#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 400
	
        char  name[NAME_SIZE];
        long  num;
        double mttf;
        double mttr;
        double facovt;
        int lab_ptr;  //  int pointer => c_lab[ptr];
        double facset;
        double facrun;
        double faccvs;

        long  cellid;

        double uset;
        double urun;
        double uwait;
        double udown;
        double u;                 /*  total utilization */
        double qp;                /*  W-I-P in process */
        double qw;                /*  W-I-P in queue */
        double q;                 /*  W-I-P in process + in queue */
		double	num_av;           /* ave num active allowing for ot ... */
        double wait;

        double bc_uset;
        double bc_urun;
        double bc_uwait;
        double bc_udown;
        double bc_u;                 /*  total utilization */
        double bc_qp;                /*  W-I-P in process */
        double bc_qw;                /*  W-I-P in queue */
        double bc_q;                 /*  W-I-P in process + in queue */
        double bc_wait;

        long  index;
       
        double imttf;
        double xbarbar;
        double xbard;
        double xsigbar;
        double smbar;
        double smbard;
        double tpm;
        double ca2;
        double cs2;
        double A;
        double C0;
        double C2lam0;
        double lam0;
        double lam;
        double nV;
        double nW;
        double oV;
        double P0;
        double X;

        double fac_eq_lab;
	
		double lambda;
		double sum_a_g;
		double y1;
		double y2;
		double y3;

        float o1;
        float o2;
        float o3;
        float o4;

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

		class_eq::class_eq(void) { ;};
		class_eq::~class_eq(void) {;};