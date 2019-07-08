#include "afx.h"
#include "StdAfx.h"

#include <iostream>
#include <fstream>
// #include <math.h>

using namespace std;

#include <direct.h>
#include <stdlib.h>
#include <stdio.h>
#pragma once


class  class_part
{
public:

  	class_part(void);
  	~class_part(void);
			

#define  BUFLEN    400
#define  NAME_SIZE  20
#define  MESSAGE_SIZE 400
	
        char  name[NAME_SIZE];
        double ndmnd;
        double lotsiz;
        double tbatch;
        double facdem;
        double faccva;

        int   tgatherbatch;

        double total;             /*  total production */
        double shipped;           /*  used for shipped production */
        double demd_ass;          /*  used in assembly */
        double demd_scrap;        /*  scrapped in assembly */
        double scrapped;          /*  scrapped production */

        double tsgood;
        double qpart;             /*  W-I-P in pieces */
        double wip_lots;          /*  used for W-I-P in LOTS */

        double tsvar;
        double bc_tsvar;

        double tgather;            /*  # time in gather step */
        double t_w_asm;            /*  # time in wait for assembly */

        double w_labor; /* time spent waiting for labor */
        double w_equip; /* time spent waiting for equip */
        double w_setup; /* time spent in setup */
        double w_run ; /* time spent in run */
        double w_lot ; /* time spent waiting for rest of lot */

        double bc_total;             /*  total production */
        double bc_shipped;           /*  used for shipped production */
        double bc_demd_ass;          /*  used in assembly */
        double bc_demd_scrap;        /*  scrapped in assembly */
        double bc_scrapped;          /*  scrapped production */

        double bc_tsgood;
        double bc_qpart;             /*  W-I-P in pieces */
        double bc_wip_lots;          /*  used for W-I-P in LOTS */


        double bc_tgather;            /*  # time in gather step */
        double bc_t_w_asm;            /*  # time in wait for assembly */

        double bc_wlabor; /* time spent waiting for labor */
        double bc_wequip; /* time spent waiting for equip */
        double bc_wsetup; /* time spent in setup */
        double bc_wrun ; /* time spent in run */
        double bc_wlot ; /* time spent waiting for rest of lot */

        int  index;
       

	int oper_cnt;
	int route_cnt;
	int ibom_cnt;
	
	int oper_start;
	int route_start;
	int ibom_start;

	 

        int  ibom_done;       /*  set to TRUE if this part is a component of an assembly */
        int  ibom_blink;      /*  flag for IBOM blinking */
        int  check_rout;      /*  do routing check and calc */

        double dlam;
        double scrap;             /*  scrapped rate in production */

                double weight;
                float FX;
                float a;
                float init_tb;

                float p1;
                float p2;
                float p3;
                float p4;


};

