
#pragma once
class classmpx_c1
{
public:  
	 classmpx_c1(void){};
	~classmpx_c1(void){};
	
#define __cplusplus

	// include getdef.h 
	




//#define boolean int

#if defined (BATCH)
#include <windows.h>
#endif


#if defined BATCH
#define omalloc(a,b)  malloc(b)
#define ofree(a,b)    free(b)
#define ocalloc(a,b,c) calloc(b,c)
#define ACCESS_TRUE   -1
#endif

#define bBLUE_WHITE 0x97
#define bCYAN_RED   0xB4
#define bRED_WHITE  0xC7
#define bWHITE_BLUE 0xF1
#define bGREEN_WHITE 0xA7

#define BLACK_BLACK 0x00
#define BLACK_RED   0x04
#define BLACK_WHITE 0x07

#define BLUE_BLACK  0x10
#define BLUE_BLUE   0x11
#define BLUE_CYAN   0x13
#define BLUE_RED    0x14
#define BLUE_WHITE  0x17

#define GREEN_GREEN 0x22

#define CYAN_BLACK  0x30
#define CYAN_BLUE   0x31
#define CYAN_CYAN   0x33
#define CYAN_RED    0x34
#define CYAN_WHITE  0x37

#define RED_RED     0x44
#define RED_WHITE   0x47
#define RED_LCYAN   0x4B

#define MEGEN_WHITE 0x57
#define MEGEN_BLACK 0x50
#define MEGEN_MEGEN 0x55

#define BROWN_BROWN  0x66

#define WHITE_BLACK 0x70
#define WHITE_BLUE  0x71
#define WHITE_GREEN 0x72
#define WHITE_CYAN  0x73
#define WHITE_RED   0x74
#define WHITE_BROWN 0x76
#define WHITE_WHITE 0x77
#define WHITE_YELLOW 0x7E

#define WHITE_BWHITE 0x7F


#define BLUE_LBLUE   0x19
#define GREEN_LGREEN 0x2A
#define CYAN_LCYAN   0x3B
#define RED_LRED     0x4C
#define MEGEN_LMEGEN 0x5D



#define  STOP             999
#define    QSTOP        "999"


#define  CANCELEDITS     1001
#define    QCANCELEDITS "1001"

#define  ADD             1002
#define    QADD         "1002"

#undef   DELETE
#define  DELETE          1003
#define    QDELETE      "1003"

#define  COPY            1004
#define    QCOPY        "1004"

#define  RENAME          1005
#define    QRENAME      "1005"

#define  CANCEL          1007
#define    QCANCEL      "1007"

#define  TABLE           1008
#define    QTABLE       "1008"

#define  GRAPH           1009
#define    QGRAPH       "1009"

#define  GRAPHFT           1010
#define    QGRAPHFT       "1010"

#define  GRAPHWIP           1011
#define    QGRAPHWIP       "1011"

#define  GRAPHEU           1012
#define    QGRAPHEU       "1012"

#define  GRAPHLU           1013
#define    QGRAPHLU       "1013"

#define  GRAPHUTIL           1014
#define    QGRAPHUTIL       "1014"

#define  GRAPHSET           1015
#define    QGRAPHSET       "1015"

#define  OPERS           1016
#define    QOPERS       "1016"

#define  BOTH               1017
#define    QBOTH           "1017"

#define  GRAPHICS           1018
#define    QGRAPHICS       "1018"

#define  CHARACTER          1019
#define    QCHARACTER      "1019"

#define  TOFILE               1020
#define    QTOFILE           "1020"


#define  PRINTER            1021
#define    QPRINTER        "1021"

#define  TEST               1022
#define    QTEST           "1022"

#define  GO                 1023
#define    QGO             "1023"

#define  REPAINT            1024
#define    QREPAINT        "1024"

#define  COMPARE            1025
#define    QCOMPARE        "1025"

#define  NOCOMPARE            1026
#define    QNOCOMPARE        "1026"

#define  REPORTWHATIF            1027
#define    QREPORTWHATIF        "1027"

#define  REPORTCOMPARE            1028
#define    QREPORTCOMPARE        "1028"

#define  DATA                   1029
#define    QDATA                "1029"

#define  CLOSE           1030
#define    QCLOSE       "1030"

#define  SHOWNAME        1031
#define  QSHOWNAME      "1031"

#define  NDI_MPGUP       1032
#define  QNDI_MPGUP     "1032"

#define  NDI_MPGDN       1033
#define  QNDI_MPGDN     "1033"

#define  NDI_MUP         1034
#define  QNDI_MUP       "1034"

#define  NDI_MDN         1035
#define  QNDI_MDN       "1035"

#define  RESTART         1036
#define    QRESTART     "1036"

#define  ADD_P           1037
#define    QADD_P       "1037"

#define MPX_MAIN        9
#define FILE_MAIN      21
#define EDIT_MAIN      10
#define RUN_MAIN       11
#define REPORT_MAIN    40
#define GRAPH_MAIN     60
#define WHAT_IF_MAIN   79
#define PRINT_MAIN     80



#define LOAD_NEW         2
#define LOAD_OLD         3
#define LOAD_SAVE        4
#define FILE_IMPT        5
#define FILE_EXPT        6
#define EXIT_DOS         7
#define QUIT             8

#define TITLE_INPUT    11
#define SYSTEM_INPUT   12
#define LABOR_INPUT    13
#define EQUIP_INPUT    14
#define PRODUCT_INPUT  15
#define OPRT_INPUT     16
#define IBOM_INPUT     17
#define ADVANCED_INPUT 18

#define OPAS_INPUT     19

#define RUN             21
#define RUN_MPX         22
#define RUN_MACRO       23

#define REPORT_SUMMARY  31
#define R_P_SUM         32
#define R_P_FLOW        33
#define R_P_WIP         34
#define R_P_PROD        35
#define R_PDET_DIA      36
#define R_E_SUM         37
#define R_E_UTIL        38
#define R_E_WIP         39
#define R_EDET_DIA      40
#define R_L_SUM         41
#define R_L_UTIL        42
#define R_L_WIP         43
#define R_LDET_DIA      44

#define R_I_TIME        45
#define G_I_TIME        46

#define P_FT_GRAPH      51
#define P_WIP_GRAPH     52
#define P_P_GRAPH       53
#define G_PDET_DIA      54
#define E_U_GRAPH       55
#define E_WIP_GRAPH     56
#define G_EDET_DIA      57
#define L_U_GRAPH       58
#define L_WIP_GRAPH     59
#define G_LDET_DIA      60

#define WHATIF_NEW      71
#define WHATIF_OLD      72
#define WHATIF_SAVE     73
#define WHATIF_DEL      74
#define WHATIF_CLSE     75
#define WHATIF_VIEW     76
#define WHATIF_RBC      77
#define WHATIF_MDL      78

#define PRINT_INPUT     81
#define PRINT_WHATIF    82
#define PRINT_ALL_R     83
#define PRINT_PART_R    84
#define PRINT_EQUIP_R   85
#define PRINT_LABOR_R   86
#define PRINT_IBOM_R    87

#define PRINT_I_DET     89
#define PRINT_R_P_D     90
#define PRINT_R_E_D     91
#define PRINT_R_L_D     92


#define ARGCNT        30       /* Number of character arguments in a line of input data */
#define MAXLEVEL      5       /* Max number os levels to build ibom show */
#define NUM_ARGCNT    30       /* Number of number arguements in a line of input data */
#define BAR_CNT       20       /* Max Number of veritcal bars on screen */
#define MESSAGE_SIZE  80       /* length of message strings */
#define EPSILON       1e-6     /* a small number */
#define SSEPSILON     1e-20    /* a VERY small number */
#define ROUT_ERR      0.1      /* size for ROUTING and ASSIGN ERRORs */

#define MAX(a,b)    ( (a>b) ? a : b  )
#define MIN(a,b)    ( (a<b) ? a : b  )
#define mprintf(file, str, var, cnt, k )  { for(k=0;k<cnt;k++) fprintf(file,  str, var);}


#if defined (DOS)
#define msl_fprn(str, var, cnt, k )  { for(k=0;k<cnt;k++) sl_fprn(str, var);}
#endif

//#if !defined (DOS)
//  c/ msl_frpn( / mprintf(fileprint, /
//  c/ sl_frpn( / fprintf(fileprint, /
//#define mprintf(str, var, cnt, k )  { for(k=0;k<cnt;k++) fprintf(fileprint, str, var);}
//#endif


#define arr(i,j,k)  (((i)*(k))+(j))
#define arr3(i,j,k,m,n)  (((i)*(m)*(n))+(j)*(n)+(k))

#define NDI_POW(a,b)   ( (a>EPSILON) ? (pow((a), (b))) : 0.0)

#define   NEWLINE  10   /* newline   ascii */
#define   FORMFEED 12   /* form feed ascii */
#define   CARG_RETN 13  /* carriage return ascii */

#define   READ_NO_ERR 0     /* used for READERR flags */
#define   READERR     1     /* used for READERR flags */
#define   READWARN    2
#define   READSHORT   4
#define   READEND     8

#define   READIO   1     /* error types for mperr */
#define   MATCH    2


#define MY_MEM_TAG 11111

#if defined (DOS)


#if defined (HUGE)
#define EQ_LIMIT 32000
#endif

#if !defined (HUGE)
#define EQ_LIMIT 125
#undef  _huge
#define _huge
#undef   huge
#define  huge
#define halloc(a,b)  calloc (a,b)
#define hfree(a)     free (a)
#endif

#endif

#if defined (MPX386)
#undef   huge
#define  huge
#undef   HUGE
#define  HUGE

#undef  EQ_LIMIT
#define EQ_LIMIT 32000
#endif

#if defined (OS2)
#define EQ_LIMIT 32000
#define _huge
#define halloc(a,b)  calloc (a,b)
#define hfree(a)     free (a)
#endif
#if defined (VAX)
#define EQ_LIMIT 32000
#define _huge
#define  huge
#define halloc(a,b)  calloc (a,b)
#define hfree(a)     free (a)
#endif


#define TRUE_ACCESS -1
#define FALSE     0
#define TRUE      1
#define NOTIFY    2

#define   BAD_FILE_NAME 4

#define SKIP_REC  1
#define ADD_NEW   2
#define OVERWRITE 3

#define Q_SKIP_REC  "1"
#define Q_ADD_NEW   "2"
#define Q_OVERWRITE "3"



#define TEMP  -1
#define PERM   0
#define TADD   1
#define TDEL   2
#define ALL    4
#define PADD   8
#define PDEL   16

#define LABOR_T 0
#define EQUIP_T 1
#define T_BATCH_TOTAL_LABOR 2
#define T_BATCH_TOTAL_EQUIP 3
#define T_BATCH_PIECE       4
#define T_BATCH_WAIT_LOT    5
#define LABOR_T_GATHER_1    6
#define EQUIP_T_GATHER_1    7
#define LABOR_T_GATHER_2    8
#define EQUIP_T_GATHER_2    9
#define LABOR_T_GATHER_3    10
#define EQUIP_T_GATHER_3    11
#define LABOR_1P			12
#define EQUIP_1P			13
#define LABOR_1_TB			21
#define EQUIP_1_TB			22
#define LABOR_IJK			23
#define EQUIP_IJK			24
#define LABOR_DIF			25
#define EQUIP_DIF			26
#define LABOR_DIF_T			27
#define EQUIP_DIF_T			28
#define LABOR_DIF_1			29
#define EQUIP_DIF_1			30


typedef  int   flag_acdr;
typedef  int   flag_menu;
typedef  int   flag_rg_type;
typedef  int   flag;



#define  mLABOR  0
#define  mEQUIP  1
#define  mPART   2
#define  mOPASA  3
#define  mOPRT   4
#define  mIBOM   5
#define  mOPAS1  6
#define  mOPRTA  7
#define  mTIME   8
#define  mTITLE  9

#define  COMMON  11 /* used only in what if report */
#define  mUNLOCK 12 /* used to unlock reports */
#define  mLOCK   13 /* used to unlock reports */




#define  NAME   1
#define  FLOAT  2
#define  INTEG  3
#define  PLABOR 4
#define  PEQUIP 5
#define  PPART  6
#define  POPAS  7
#define  DOUBLE 8


#define  fMPX    10
#define  fMP2    11
#define  fMC     12
#define  fBINARY 20
#define  fASCII  21
#define  fLOTUS  22
#define  fREAD   23
#define  fINPUT  31
#define  fOUTPUT 32
#define  fIN_OUT 33

#define  mDO_ERR_LOCAL 35
#define  mDO_ERR_RETN  36


#define  rl_row(i)  strt_r+((i)*blk_r)
#define  rl_col(i)  strt_c+((i)*blk_c)
#define  iINFIN     -32000

#if defined(VAX)
#define  labs(i)   abs(i)
#endif

#if defined(DOS)
#define  sCHECK     "û"
#define  cCHECK     'û'
#endif

#if defined(OS2)
#define  sCHECK     "û"
#define  cCHECK     'û'
#endif

#if defined(VAX)
#define  sCHECK     "*"
#define  cCHECK     '*'
#endif


#define ROUTE_A   0
#define ROUTE_AR  1
#define ROUTE_B   2
#define ROUTE_C   3
#define ROUTE_D   4

#define ROUTE_RD  11
#define ROUTE_ED  12
#define ROUTE_LD  13

#define ROUTE_RE  21
#define ROUTE_EE  22
#define ROUTE_LE  23

#define ROUTE_RU  31
#define ROUTE_EU  32
#define ROUTE_LU  33

#define ROUTE_NONE  40
#define ROUTE_NAME  41


#define ROUTE_START 45
#define ROUTE_CONT  46
#define ROUTE_STOP  47


//  include mp_strct.h 

#define  BUFLEN    100
#define  NAME_SIZE  20

typedef         struct LABOR   p_LABOR;
typedef         struct EQUIP   p_EQUIP;
typedef         struct PART    p_PART;
typedef         struct OPAS    p_OPAS;
typedef         struct OPRT    p_OPRT;
typedef         struct IBOM    p_IBOM;

typedef         struct OPLST   p_OPLST;
typedef         struct WHATIF  p_WHATIF;
typedef         struct WH_REC  p_WH_REC;
typedef         struct ARROW   p_ARROW;

typedef         struct WUSED   p_WUSED;


/********************************************************************/
struct LABOR {

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
        flag keep;
        char   *  oldname;
        p_LABOR   * next_lb;


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
 /* *******************************************************************/
struct EQUIP {

        char  name[NAME_SIZE];
        long  num;
        double mttf;
        double mttr;
        double facovt;
        p_LABOR   * lab_ptr;
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
        flag  keep;
        char   *  oldname;
        p_EQUIP   * next_eq;

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

 /* ***********
      real facovt      { factor for overtime (usually 1.0) }
      real facmts      { factor for speed up of SETUP on machine}
      real facmtp      { factor for speed up of Piece on machine}
      real facCVS      { factor for variability of service}
 * **************/


 /* *******************************************************************/
struct PART {

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
        flag keep;
        char   *  oldname;

        p_PART    * next_pt;
        p_OPAS    * n_opas;
        p_OPRT    * n_oprt;
        p_IBOM    * n_cmpt;


        boolean  ibom_done;       /*  set to TRUE if this part is a component of an assembly */
        boolean  ibom_blink;      /*  flag for IBOM blinking */
        boolean  check_rout;      /*  do routing check and calc */

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

 /* **************
      real   facdem     { factor for extra demand (0.0 => no PARTs)}
      real   facset     { factor for speed up of setups on this PART}
      real   facrun     { factor for speed up of Pieces for this PART}
      real   faccva     { factor for variability of arrival}
 * ****************************/


 /* *******************************************************************/

struct OPAS {

        char name[NAME_SIZE];
        p_EQUIP   * eq_ptr;
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

        flag keep;
        char   *  oldname;

        p_OPAS    * n_opas;

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

 /* *******************************************************************/
struct OPRT{

        struct OPAS    * from_op;
        struct OPAS    * to_op;
        double cmmpij;

        flag keep;
        struct OPRT    * n_oprt;
};

 /* *******************************************************************/

 /*  for components of part their are hanging from */
struct IBOM {

        p_PART    * name_ptr;
        double  upa;
        double  salv;


        flag keep;
        p_IBOM   * n_cmpt;
};

 /* *******************************************************************/
struct OPLST {
        long index;
        char name[NAME_SIZE];
        double optot;
        double rttot;
        p_OPAS    * n_opas;
        p_OPLST   * n_oplst;
};

 /* *******************************************************************/
 /* *******************************************************************/
 /* *******************************************************************/

struct WHATIF {
        char  name[NAME_SIZE];
        char fname[NAME_SIZE];
        p_WHATIF    *  n_whi;
        char comment[BUFLEN];
        p_WH_REC    *  n_whrec;
};

struct WH_REC {
        char n0[NAME_SIZE];
        char n1[NAME_SIZE];
        char n2[NAME_SIZE];
        char n3[NAME_SIZE];
        char n4[NAME_SIZE];
        flag_menu  n5;
        char n6[NAME_SIZE];
        int  n7;
        char n8;
        p_WH_REC    *  n_whrec;
};

 /* *******************************************************************/
 /* *******************************************************************/



struct ARROW {
        int from_r;
        int from_c;
        int to_r;
        int to_c;
        int row_num;
        int col_num;
        int route;
        int start_stop;
        p_ARROW   *  n_arr;
};


 /* *******************************************************************/
 /* *******************************************************************/

 /*  for components of WHERE USED LIST */
struct WUSED {

        p_PART    * part_ptr;
        p_IBOM    * cibom2;
        p_WUSED   * n_wused;
};


 /* *******************************************************************/
typedef         struct CRITICAL   p_CRIT;

struct CRITICAL {

        int line_num;
        p_CRIT   *  n_crit;
};


//  include  add_g.hg and  commonh 

//  add_g.h 
  int TRACE2;

    int WID;

    int far *    err_code;
   int far *    lab_err;
   int far *    eq_err;
   int far *    part_err;
   int far *    oper_err;
   int far *    route_err;
   int far *    ibom_err;

#if defined (DEBUG)
#define ocalloc(a,b,c) calloc(b,c)
#define omalloc(a,b)   malloc(b)
#define ofree(a,b)     free(b)
#undef halloc
#undef hfree
#endif


#if defined (ACADEMIC)
#define   lab_limit  14
#define   eq_limit   25
#define   part_limit 26
#define   opas_limit 31
#define   oprt_limit 33
#define   ibom_limit 12

#elif defined (STUDENT)
#define   lab_limit   8
#define   eq_limit   14
#define   part_limit 14
#define   opas_limit 17
#define   oprt_limit 16
#define   ibom_limit 12

#else
#define   lab_limit  6000000
#define   eq_limit   6000000
#define   part_limit 6000000
#define   opas_limit 6000000
#define   oprt_limit 6000000
#define   ibom_limit 6000000

#endif



 char varlocaldir [MESSAGE_SIZE];



#if !defined (BATCH)
 sed_type calc_sed, smem_sed;
 sed_type back, back2;
 sed_type menubar;
 sed_type  mess_sed;

 menu_type menu;
#endif



 int barlist[BAR_CNT];
 int bar2   [BAR_CNT];
 int ibomslt[BAR_CNT];
 char  char_gra [ 129 ];
 int inOPT;


/********************************************************************/
/*  input/output file pointers */
/********************************************************************/

 int FULL;
 int mess;

 int ICOMBINE;

  FILE *filein;
  FILE *filein_b;
  FILE *fileecho;
  FILE *fileout;
  FILE *fileout_b;
  FILE *fileerr;
  FILE *filemsg;
  FILE *fileprint;

  int      def_disk;
  char     def_dir  [MESSAGE_SIZE];
  unsigned drive, rest_drive;
  char     MPX_DRIVE_DIR[MESSAGE_SIZE];

 int   INT_FLAG; /* SET IF IN INTERRUPT */
 int   readerr, rstatus;
 char   reada [ARGCNT][NAME_SIZE];
 double  readn [NUM_ARGCNT];


/********************************************************************/
/*  input variables */
/********************************************************************/


  char dir_loc[5*MESSAGE_SIZE];

  char title[MESSAGE_SIZE];
  char version[MESSAGE_SIZE];
  int  ver_form;
  char act_version[MESSAGE_SIZE];
  char nam1[NAME_SIZE], nam2[NAME_SIZE], nam3[NAME_SIZE];
  double t_tot1, t_tot2;
  double t_shft1, t_shft2;
  double coefta,coeftl, coefte, utlimit;

  double       total_pro,    total_shi,    total_scr,    total_wip,    total_ft;
  double       wi_total_pro, wi_total_shi, wi_total_scr, wi_total_wip, wi_total_ft;

  int ecnt, lcnt;     /* count of number of equip and labor structures */

   p_LABOR *hd_labor;
   p_EQUIP *hd_equip;
   p_PART  *hd_part;

   p_PART  *ppart_global;
   int          DELETE_ALL;

   int  SHOW_LIST_FLAG;
   char SHOW_NAME  [NAME_SIZE];
   char SHOW_FIELD [NAME_SIZE];


 int WARN_MESS_ALL;
 int READING;

#if !defined (BATCH)
 /* MACRO STUFF */
     int MACRO_STATE;
     sed_type macro_info_win;
     FILE * file_mac;
#endif

 char model_name[MESSAGE_SIZE];
 char whatif_name[MESSAGE_SIZE];
 char result_form[MESSAGE_SIZE];
 char result_data[MESSAGE_SIZE];
 char model_whatif_name[NAME_SIZE];
 char whatif_msg[NAME_SIZE];
 boolean whatif_modified;
 p_WHATIF * hd_whi;          /* carries what-if model cases */
 p_WHATIF * cur_whi;         /* current what-if model */
 char textbuffer[BUFLEN];        /* carries the comments in what-if scrren */

 char cdates    [3][NAME_SIZE];  /* created, modified, run */

/********************************************************************/
/*  current state pointers */
/********************************************************************/
 char      T_DATE [MESSAGE_SIZE];
 int       RESULTS;
 char      SAVE_WHATIF_AS_NEWMODEL[NAME_SIZE];
 int       RUN_BC_WHI;
 int       MODIFY;
 int       COLOR;
 int       Warn_mess;
 int       LOADED;
 long      MEMORY_TOP;
 int       MEMORY_LFT;
 int       mem_avail;

 int       LABOR_CHG;
 int       EQUIP_CHG;
 int       PART_CHG;

/********************************************************************/
/*  ERROR message buffers */
/********************************************************************/
 char   buf1 [15*MESSAGE_SIZE];
 char   buf2 [15*MESSAGE_SIZE];

/********************************************************************/
/* FIELD NAME FLAG */
/********************************************************************/
  int in_collect;

  boolean ADVANCED;
  boolean WHATIF;
  int     whatif_tem_flag;


  boolean  IBOM_CHG;

  boolean INSIDE_FEXIT_OPRT_FLAG;

  char  m_names[9][NAME_SIZE];
  char  a_names[4][NAME_SIZE];
  char  chartype[25];

  char filename_out[MESSAGE_SIZE];

 int  FIELD_CNT[6];

 char FIELD_NAM[6][9][NAME_SIZE];

 int  FIELD_TYP[6][9];

 char RESULTS_F_NAME[4][15][NAME_SIZE];

/* display parameters * -------------------------------- */
 int row, col, maxrow, maxcol;
 int strt_r, strt_c, blk_r, blk_c;
 int stockx, stocky, stockb, scrapx, scrapy, scrapb;
 int DMAXROW, DMAXCOL, ROWS;
 int sled_rows;
 int PGLEN;

/*    CONSTANTS  */
 char  cNULL;
 char  sNULL [NAME_SIZE];


/*   over utilization */
  int   over_util_L;
  int   over_util_E;

/* whatif calculation flag */
  int   SWITCH_TO_WHATIF;

/*   flags for op - rt check */
  int         do_err_type;
  flag_menu   opas_rt_err;
  char        op_error_name[NAME_SIZE];
  int         INRUN;
  int         RESTART_CANCEL;

/*  flags for print options */
  int   result_type;
  int   printer_type;
  int   out_type;
  char  det_name [NAME_SIZE];
  //   inclde link1_1a.c
  

/*
 *  Alcoa link_acc.C
 *
 *  C Compile:      wcc386 link_acc /bd /bt=windows /d2
 */



/* -------- EXTERNAL CONNECTION Functions  -----------------*/
//
// DLL calls



/* ************************************************************** */
int Clear_model_batch ( int collect,  char * dir1  )
/* ************************************************************** */
{
  char buf [100];
  int i;


    strcpy(varlocaldir, dir1);
	i = strlen(varlocaldir);
	
	if (varlocaldir[i-1] == '\\') { 
		varlocaldir[i-1] = cNULL;  }
     SetCurrentDirectory(dir1);
  

   sprintf(buf, " setlocal dir %s :: %s ", dir1, varlocaldir);
  Message(buf);



  initial_model();
  Clear_Model();
  LOADED = FALSE;

  unlink("stop.1");
  unlink("results.ass");
  unlink("results.gen");
  unlink("results.lab");
  unlink("results.eq");
  unlink("results.prt");
  unlink("results.opr");
  unlink("results.err");
  unlink("results.whi");
  unlink("results.w_r");
    
  unlink("results.pol");



  warn_err_header();

  in_collect = collect;
  ICOMBINE = FALSE;

    warn_err_header();

return(0);
};

/* ************************************************************** */
int  Run_model (int iWID, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
        ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS)
        /* ************************************************************** */
{ int  ret;  // return value
  char buf [100];
  char buf1[100];
  char buf2[100];
  p_LABOR * tlabor;
   strcpy(buf, " starting do work ");
    // Message(buf);

	unlink("stop.1");
	ICOMBINE = FALSE;

	 sprintf(buf, "Can open yuiyiu \n %s %f \n", varlocaldir, t_tot1);
     Message(buf);


	TRACE2 = 1;

    WID = iWID;


    FULL = full;
    mess = MESS;

	inOPT= FALSE;

     //sprintf(buf2, "FULL ? %d ", FULL);
     //Message (buf2);

#if defined (LOCAL)
    err_code  = ierrcode;
    lab_err   = ilab_err;
    eq_err    = ieq_err;
    part_err  = ipart_err;
    oper_err  = ioper_err;
    route_err = iroute_err;
    ibom_err  = iibom_err;
#else
    err_code  = (int *) ierrcode;
    lab_err   = (int *) ilab_err;
    eq_err    = (int *) ieq_err;
    part_err  = (int *) ipart_err;
    oper_err  = (int *) ioper_err;
    route_err = (int *) iroute_err;
    ibom_err  = (int *) iibom_err;
#endif
  TRACE2 = 1;
  //if (TRACE2) {
  //   strcpy(buf, " check_loaded ");
  //   MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  //}

        sprintf(buf1, "at link 0");
        //Message(buf1);

  ret = check_loaded ();
  if (ret == FALSE) return(ACCESS_TRUE);

  //if (TRACE2) {
  //   strcpy(buf, " starting do work ");
  //   MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  //}


  unlink("mpx.mdl");
  strcpy(model_name, "mpx.mdl");
  write_all (fMPX, fASCII, fINPUT);

        sprintf(buf1, "at link 1  %f ", t_tot1);
        Message(buf1);

  mpcalc();

        sprintf(buf1, "at link 2");
        //Message(buf1);


  //if (TRACE2) {
  //   strcpy(buf, " after calc ");
  //   MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  //}

  // strcpy(model_name, "results.txt");
  // write_all (fMPX, fASCII, fIN_OUT);

  //if (TRACE2) {
  //   strcpy(buf, " after writing results.txt ");
  //   MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  //}


      	 sprintf(buf, "Can open yl;'l'uiyiu \n %s %f \n", varlocaldir, t_tot1);
         Message(buf);


  ret = write_flat_file_results ();


	 sprintf(buf, "Can open yuiihhkjyiu \n %s %f \n", varlocaldir, t_tot1);
     Message(buf);


sprintf(buf, " done. writing.." );
 // Message(buf);


  //if (TRACE2) {
  //   strcpy(buf, " after writing flat files ");
  //   MessageBox( NULL, buf, "Into Running ? ", MB_OK | MB_TASKMODAL );
  //}

  //Message(" about to return");

return(0);
}

/* ************************************************************** */
int    General_time (float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var)
/* ************************************************************** */
{
	char buf [MESSAGE_SIZE];

	unlink("stop.1");

  t_tot1 = t_shft1 = time1;
  t_tot2 = t_shft2 = time2;

  coeftl = lab_var;
  coefte = eq_var;
  coefta = part_var;
  utlimit = u_limit;

   
   sprintf(buf, "Into Running jkljkll %f time1? ", t_tot1, time1 );
   Message( buf);

  return(0);
}


/* ************************************************************** */
int    Labor_data (int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
        float *   x5, float *   x6, float *   x7, int * x8)
/* ************************************************************** */
{int i;
 p_LABOR  * lab;

 int ix;

 int *lab_id;
 float *lab_size;
 float *lab_ot;
 float *lab_abs;
 float *lab_s_fact;
 float *lab_r_fact;
 float *lab_v_fact;

 int *lab_balance;

  char buf[128];
  char buf2[128];


  unlink("stop.1");

#if defined (LOCAL)


   lab_id      = (int   * ) x1;
   lab_size    = (float * ) x2;
   lab_ot      = (float * ) x3;
   lab_abs     = (float * ) x4;
   lab_s_fact  = (float * ) x5;
   lab_r_fact  = (float * ) x6;
   lab_v_fact  = (float * ) x7;
   lab_balance = (int * ) x8;
#else


   lab_id      = (int   *) x1;
         //sprintf(buf, "lab id : %ld %ld %ld  ", lab_id[0], lab_id[1], lab_id[2]);
         //MessageBox( NULL, buf, "after mapaliastoflat", MB_OK | MB_TASKMODAL );
   lab_size    = (float *) x2;
   lab_ot      = (float *) x3;
   lab_abs     = (float *) x4;
   lab_s_fact  = (float *) x5;
   lab_r_fact  = (float *) x6;
         //sprintf(buf, "lab id : %f %f %f  ", lab_r_fact[0], lab_r_fact[1], lab_r_fact[2]);
         //MessageBox( NULL,  buf, "after mapaliastoflat", MB_OK | MB_TASKMODAL );
   lab_v_fact  = (float *) x7;
   lab_balance = (int * ) x8;

#endif


  for (i=0; i<l_count; i++) {
     ix = lab_id[i];
     sprintf(buf, "%d", ix);
     buf[20] = '\0';
     xspace(buf);
     lab =  hd_labor; // gwwd  add_labor(buf, PERM, "");
     if (lab == NULL) {
           sprintf(buf2, "Could Not add Labor Group");
           Message (buf2);
           warn_err (buf2, 0,  buf, " ", " ", " ", " ", " ");
           return(ACCESS_TRUE);
     }

     lab->num = lab_size[i];
     lab->absrate = lab_abs[i];
     lab->facovt  = lab_ot[i];
     lab->facset  = lab_s_fact[i];
     lab->facrun  = lab_r_fact[i];
     lab->faccvs  = lab_v_fact[i];
	 lab->balance = lab_balance[i];
  } // loop


return(0);
}

/* ************************************************************** */
int    Equip_data (int eq_count, int *   x1, int *   x2, float *   x3, float *   x4, float *  x5,
         int *   x6, float *   x7, float *   x8, float *   x9, int * x10)
/* ************************************************************** */
{int i;
 p_EQUIP * eq;

 int ix;

 int * eq_id;
 int * eq_size;
 float * mttf;
 float * mttr;
 float * eq_ot;
 int   * lab_id;
 float * eq_s_fact;
 float * eq_r_fact;
 float * eq_v_fact;

 int * eq_cell;

  char buf1[128];
  char buf2[128];
  char buf3[128];

	unlink("stop.1");

#if defined (LOCAL)
   eq_id      = (int   * ) x1;
   eq_size    = (int   * ) x2;
   mttf       = (float * ) x3;
   mttr       = (float * ) x4;
   eq_ot      = (float * ) x5;
   lab_id     = (float * ) x6;
   eq_s_fact  = (float * ) x7;
   eq_r_fact  = (float * ) x8;
   eq_v_fact  = (float * ) x9;
   eq_cell    = (int * ) x10;
#else
   eq_id      = (int   *) x1;
   eq_size    = (int   *) x2;
   mttf       = (float *) x3;
   mttr       = (float *) x4;
   eq_ot      = (float *) x5;
   lab_id     = (int   *) x6;
   eq_s_fact  = (float *) x7;
   eq_r_fact  = (float *) x8;
   eq_v_fact  = (float *) x9;
   eq_cell    = (int * ) x10;
#endif


  for (i=0; i<eq_count; i++) {
     ix = eq_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     eq = add_equip(buf1, "", PERM, " ");
     if (eq == NULL) {
            sprintf(buf2, "Could Not add Equipment Group");
            Message (buf2);
            warn_err (buf2, 0,  " ", buf1, " ", " ", " ", " ");
            return(ACCESS_TRUE);
     }
     eq->num     = eq_size[i];
     eq->mttf    = mttf[i];
     eq->mttr    = mttr[i];
     eq->facovt  = eq_ot[i];


     ix = lab_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     eq->lab_ptr = find_labor(buf1);
     if (eq->lab_ptr == NULL) {
       eq->lab_ptr = hd_labor;
       sprintf(buf1, "Did not find labor group for equipment");
       Message (buf1);

       sprintf(buf2, "%d", eq_id[i]);
       sprintf(buf3, "%d", lab_id[i]);
       warn_err (buf1, 0,  buf3, buf2, " ", " ", " ", " ");
       return(ACCESS_TRUE);
     }

     eq->facset  = eq_s_fact[i];
     eq->facrun  = eq_r_fact[i];
     eq->faccvs  = eq_v_fact[i];
	 eq->cellid  = eq_cell[i];
  } // loop

return(0);
}

/* ************************************************************** */
int    Parts_data (int p_count, int *   x1, float *   x2, float *   x3, float *   x4,
        float *   x5, float *   x6, int * x7)
/* ************************************************************** */
{int i;
 p_PART * part;

 int ix;

 int * part_id;
 float * ndmnd;
 float * lotsiz;
 float * tbatch;
 float * facdem;
 float * faccvs;
 int   * tgather;

  char buf[128];
  char buf1[128];
  char buf2[128];
  char buf3[128];

	unlink("stop.1");

#if defined (LOCAL)
   part_id      = (int   * ) x1;
   ndmnd        = (float * ) x2;
   lotsiz       = (float * ) x3;
   tbatch       = (float * ) x4;
   facdem       = (float * ) x5;
   faccvs       = (float * ) x6;
   tgather      = (int   * ) x7;
#else
   part_id      = (int   *) x1;
   ndmnd        = (float *) x2;
   lotsiz       = (float *) x3;
   tbatch       = (float *) x4;
   facdem       = (float *) x5;
   faccvs       = (float *) x6;
   tgather      = (int   *) x7;
#endif


  for (i=0; i<p_count; i++) {

     ix = part_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);

     part = add_part(buf1, PERM, " ");
     if (part == NULL) {
       sprintf(buf2, "Could not add Product");
       Message (buf2);
       warn_err (buf2, 0,  " ", " ", buf1, " ", " ", " ");
       return(ACCESS_TRUE);
     }

     part->ndmnd  = ndmnd[i];
     part->lotsiz = lotsiz[i];
     part->tbatch = tbatch[i];
     part->facdem = facdem[i];
     part->faccva = faccvs[i];
     part->tgatherbatch = tgather[i];


  } // loop


return(0);
}

/* ************************************************************** */
int    Oper_data (int op_count, int *  x1, int *   x2, int *   x3, int *   x4, float *   x5,
                  float *   x6, float *   x7, float *   x8, float *   x9,
                  float *   x10,float *   x11,float *   x12,float *   x13,
                  float *   x14,float *   x15,float *   x16,float *   x17)
/* ************************************************************** */
{int i;
 p_PART  * part;
 p_EQUIP * eq;
 p_OPAS  * oper;

 int ix;

 int *  oper_name;
 int *  oper_id;
 int *  part_id;
 int *  eq_id;
 float *  assgn;

 float * esetup;
 float * esetbatch;
 float * esetpiece;

 float * erlot;
 float * erbatch;
 float * epiece;

 float * lsetup;
 float * lsetbatch;
 float * lsetpiece;

 float * lrlot;
 float * lrbatch;
 float * lpiece;


 int  old_part;

  char buf1[255];
  char buf2[255];
  char buf3[255];
  char buf4[255];


  unlink("stop.1");

#if defined (LOCAL)
   oper_name    = (int   * ) x1;
   oper_id      = (int   * ) x2;
   part_id      = (int   * ) x3;
   equip_id     = (int   * ) x4;
   assgn        = (float * ) x5;

   esetup       = (float * ) x6;
   esetbatch    = (float * ) x7;
   esetpiece    = (float * ) x8;
   erlot        = (float * ) x9;
   erbatch      = (float * ) x10;
   epiece       = (float * ) x11;
   lsetup       = (float * ) x12;
   lsetbatch    = (float * ) x13;
   lsetpiece    = (float * ) x14;
   lrlot        = (float * ) x15;
   lrbatch      = (float * ) x16;
   lpiece       = (float * ) x17;
#else

   oper_name    = (int   *) x1;
   oper_id      = (int   *) x2;
   part_id      = (int   *) x3;
   eq_id        = (int   *) x4;

   assgn        = (float *) x5;
   esetup       = (float * ) x6;
   esetbatch    = (float * ) x7;
   esetpiece    = (float * ) x8;
   erlot        = (float * ) x9;
   erbatch      = (float * ) x10;
   epiece       = (float * ) x11;
   lsetup       = (float * ) x12;
   lsetbatch    = (float * ) x13;
   lsetpiece    = (float * ) x14;
   lrlot        = (float * ) x15;
   lrbatch      = (float * ) x16;
   lpiece       = (float * ) x17;

#endif

  old_part = 0;
  for (i=0; i<op_count; i++) {

   //fileout = fopen("test.tst", "at");
   //fprintf(fileout, "oper %d %d %d %d \n", part_id[i], oper_id[i], oper_name[i], eq_id[i]);
   //fclose(fileout);


     ix = part_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     part = find_part(buf1);
     if (part == NULL) {

        if (in_collect == TRUE) continue;

        sprintf(buf1, "Cannot find Product for an operation");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, " ", " ", " ");

        return(ACCESS_TRUE);
     }

     ix = eq_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     eq = find_equip(buf1);
     if (eq == NULL) {
        if (in_collect == TRUE) {
           eq = add_equip(buf1, "None", PERM, "");
        }
        else {
           sprintf(buf1, "Cannot find equipment for an operation");
           Message(buf1);

           sprintf(buf2, "%d", part_id[i]);
           sprintf(buf3, "%d", eq_id[i]);
           sprintf(buf4, "%d", oper_id[i]);
           warn_err (buf1, 0,  " ", buf3, buf2, buf4, " ", " ");

          return(ACCESS_TRUE);
        }
     }

     // do not add dock, stock, scrap
     ix = part_id[i];
     if (old_part != ix) {
         old_part = ix;

         oper = find_opas("DOCK", part);
         if (oper == NULL) {
             sprintf(buf1, "Cannot find DOCK for a product ??");
             Message(buf1);
             sprintf(buf2, "%d", part_id[i]);
             sprintf(buf3, "%d", eq_id[i]);
             sprintf(buf4, "%d", oper_id[i]);
             warn_err (buf1, 0,  " ", buf3, buf2, buf4, " ", " ");
             return(ACCESS_TRUE);
          }

         ix = oper_id[i];
         sprintf(buf2, "%d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         strcpy(oper->name, buf2);
         oper->oldname = (char * ) malloc (sizeof(char) * (strlen(buf2)+2));
         strcpy(oper->oldname, buf2);
         sprintf(buf2, "Dock,  %d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         i++;

         oper = find_opas("STOCK", part);
         if (oper == NULL) {
             sprintf(buf1, "Cannot find STOCK for a product ??");
             Message(buf1);
             sprintf(buf2, "%d", part_id[i]);
             sprintf(buf3, "%d", eq_id[i]);
             sprintf(buf4, "%d", oper_id[i]);
             warn_err (buf1, 0,  " ", buf3, buf2, buf4, " ", " ");
             return(ACCESS_TRUE);
          }
         ix = oper_id[i];
         sprintf(buf2, "%d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         strcpy(oper->name,    buf2);
         oper->oldname = (char * ) malloc (sizeof(char) * (strlen(buf2)+2));
         strcpy(oper->oldname, buf2);
         sprintf(buf2, "Stock, %d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         i++;

         oper = find_opas("SCRAP", part);
         if (oper == NULL) {
             sprintf(buf1, "Cannot find STOCK for a product ??");
             Message(buf1);
             sprintf(buf2, "%d", part_id[i]);
             sprintf(buf3, "%d", eq_id[i]);
             sprintf(buf4, "%d", oper_id[i]);
             warn_err (buf1, 0,  " ", buf3, buf2, buf4, " ", " ");
             return(ACCESS_TRUE);
          }
         ix=oper_id[i];
         sprintf(buf2, "%d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         strcpy(oper->name,    buf2);
         oper->oldname = (char * ) malloc (sizeof(char) * (strlen(buf2)+2));
         strcpy(oper->oldname, buf2);
         sprintf(buf2, "Scrap, %d", ix);
         buf2[20] = '\0';
         xspace(buf2);

         continue;
     }

     // else standard op....
     ix = oper_name[i];
     sprintf(buf2, "%d", ix);
     buf2[20] = '\0';
     xspace(buf2);
     oper = add_opas(buf2, buf1, part, PERM, buf2);
     if (oper == NULL) {
         sprintf(buf1, "Cannot add operation");
         Message(buf1);

         sprintf(buf2, "%d", part_id[i]);
         sprintf(buf4, "%d", oper_id[i]);
         warn_err (buf1, 0,  " ", " ", buf2, buf4, " ", " ");

         return(ACCESS_TRUE);
     }
         ix = oper_id[i];
         sprintf(buf2, "%d", ix);
         buf2[20] = '\0';
         xspace(buf2);
         oper->oldname = (char * ) malloc (sizeof(char) * (strlen(buf2)+2));
         strcpy(oper->oldname, buf2);
         // USING OLD NAME = OP_ID in the access code !!!
         //Message(oper->name);

     oper->cmmprp = assgn[i];

        oper->esetup =esetup[i];
        oper->esetbatch =esetbatch[i];
        oper->esetpiece =esetpiece[i];

        oper->erlot =    erlot[i];
        oper->erbatch =  erbatch[i];
        oper->epiece =   epiece[i];

        oper->lsetup =   lsetup[i];
        oper->lsetbatch =lsetbatch[i];
        oper->lsetpiece =lsetpiece[i];

        oper->lrlot =    lrlot[i];
        oper->lrbatch =  lrbatch[i];
        oper->lpiece =   lpiece[i];

  } // loop


return(0);
}


/* ************************************************************** */
int    Route_data (int r_count, int *   x1, int *   x2, int *   x3, float *   x4)
/* ************************************************************** */
{int i;
 p_PART * part;
 p_OPAS * oper1;
 p_OPAS * oper2;
 p_OPRT * route;

 int ix;

 int * part_id;
 int * oper1_id;
 int * oper2_id;
 float * assgn;

  char buf[128];
  char buf1[128];
  char buf2[128];
  char buf3[128];
  char buf4[128];


  unlink("stop.1");

#if defined (LOCAL)
   part_id      = (int   * ) x1;
   oper1_id     = (int   * ) x2;
   oper2_id     = (int   * ) x3;
   assgn        = (float * ) x4;
#else
   part_id      = (int   *) x1;
   oper1_id     = (int   *) x2;
   oper2_id     = (int   *) x3;
   assgn        = (float *) x4;
#endif




  for (i=0; i<r_count; i++) {

    //fileout = fopen("test", "at");
    //fprintf(fileout, "rout %d %d %d \n", part_id[i], oper1_id[i], oper2_id[i]);
    //fclose(fileout);



     ix = part_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     part = find_part(buf1);
     if (part == NULL) {

        if (in_collect == TRUE) continue;

        sprintf(buf1, " IN routing missing product");
        Message(buf1);

         sprintf(buf2, "%d", part_id[i]);
         warn_err (buf1, 0,  " ", " ", buf2, " ", " ", " ");

        return(ACCESS_TRUE);
     }
     ix = oper1_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     ix = oper2_id[i];
     sprintf(buf2, "%d", ix);
     buf2[20] = '\0';
     xspace(buf2);
     oper1 = find_opas(buf1, part);
     oper2 = find_opas(buf2, part);
     if (oper1 == NULL) {
        sprintf(buf1, "Routing table is missing 'From operation'  for a product");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        sprintf(buf3, "%d", oper1_id[i]);
        sprintf(buf4, "%d", oper2_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, buf3, buf4, " ");

        return(ACCESS_TRUE);
     }
     if (oper2 == NULL) {

        sprintf(buf1, "Routing table is missing 'To operation'  for a product");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        sprintf(buf3, "%d", oper1_id[i]);
        sprintf(buf4, "%d", oper2_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, buf3, buf4, " ");

        Message(buf1);

        return(ACCESS_TRUE);
     }

     route = add_oprt(buf1, buf2, part, PERM, buf1);
     if (route == NULL) {
        sprintf(buf1, "Routing could not be added ... ");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        sprintf(buf3, "%d", oper1_id[i]);
        sprintf(buf4, "%d", oper2_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, buf3, buf4, " ");

        return(ACCESS_TRUE);
     }

     route->cmmpij = assgn[i];

  } // loop

  for (part = hd_part->next_pt; part != NULL; part=part->next_pt) {
       strcpy(part->n_opas->name, "DOCK");
       strcpy(part->n_opas->n_opas->name, "STOCK");
       strcpy(part->n_opas->n_opas->n_opas->name, "SCRAP");
  }



return(0);
}

/* ************************************************************** */
int    Ibom_data (int ib_count, int *   x1, int *   x2, float *   x3)
/* ************************************************************** */
{int i;

 p_PART * part;
 p_PART * cpart;
 p_IBOM * ibom1;

 int ix;

 int * part_id;
 int * comp_id;
 float * upa;

  char buf1[128];
  char buf2[128];
  char buf3[128];
  char buf4[128];

  unlink("stop.1");

#if defined (LOCAL)
   part_id      = (int   * ) x1;
   comp_id      = (int   * ) x2;
   upa          = (float * ) x3;
#else
   part_id      = (int   *) x1;
   comp_id      = (int   *) x2;
   upa          = (float *) x3;
#endif


  for (i=0; i<ib_count; i++) {
     ix = part_id[i];
     sprintf(buf1, "%d", ix);
     buf1[20] = '\0';
     xspace(buf1);
     part = find_part(buf1);
     if (part == NULL)  {
        sprintf(buf1, "Could not find parent product in IBOM");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, " ", " ", " ");

        return(ACCESS_TRUE);
     }

     ix = comp_id[i];
     sprintf(buf2, "%d", ix);
     buf2[20] = '\0';
     xspace(buf2);
     cpart = find_part(buf2);
     if (cpart == NULL) {
        sprintf(buf1, "Did not find component product in IBOM");
        Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        sprintf(buf3, "%d", comp_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, " ", " ", buf3);
        return(ACCESS_TRUE);
     }
     ibom1 = add_ibom2(buf2, part, PERM, "");
     if (ibom1 == NULL) {
       sprintf(buf1, "Did not add record in IBOM ");
       Message(buf1);

        sprintf(buf2, "%d", part_id[i]);
        sprintf(buf3, "%d", comp_id[i]);
        warn_err (buf1, 0,  " ", " ", buf2, " ", " ", buf3);
        return(ACCESS_TRUE);
     }

     ibom1->upa = upa[i];

  } // loop


return(0);
}




int run_all ( int collect, char * dir1,  

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


		char buf1 [MESSAGE_SIZE];

      Clear_model_batch(collect, dir1);  

	    sprintf(buf1, "at gen0 %f  %f ", time1, t_tot1);
        Message(buf1);


			 General_time (time1,  time2,  u_limit,  lab_var,  eq_var,  part_var);


			  sprintf(buf1, "at gen1 %f  %f ", time1, t_tot1);
              Message(buf1);

			 Labor_data (l_count,    x1 ,    x2,    x3,    x4,
                 x5,    x6,    x7,  x8);

			 Equip_data (eq_count,    e_x1,    e_x2,    e_x3,    e_x4,   e_x5,
                 e_x6,    e_x7,    e_x8,    e_x9,  e_x10);

		     Parts_data (p_count,    p_x1,    p_x2,    p_x3,    p_x4,
                    p_x5,    p_x6,  p_x7);

		     Oper_data (op_count,   o_x1,    o_x2,    o_x3,    o_x4,    o_x5,
                     o_x6,    o_x7,    o_x8,    o_x9,
                     o_x10,   o_x11,   o_x12,   o_x13,
                     o_x14,   o_x15,   o_x16,   o_x17);

			 Route_data (r_count,    r_x1,    r_x2,    r_x3,    r_x4);

			 Ibom_data (ib_count,    i_x1,    i_x2,    i_x3);


        sprintf(buf1, "at run 1  %f %f ", t_tot1, time1);
        Message(buf1);

		sprintf(buf1, "Can open \n %s %f \n", varlocaldir, t_tot1);
        Message(buf1);



			  Run_model (iWID,   ierrcode,   ilab_err,   ieq_err, 
                 ipart_err,   ioper_err,   iroute_err,   iibom_err,  full,  MESS);
    
		sprintf(buf1, "at run 1  %f %f ", t_tot1, time1);
        Message(buf1);

		sprintf(buf1, "Can open \n %s %f \n", varlocaldir, t_tot1);
        Message(buf1);

			return(-1);
}





int run_opt5 ( int collect, char * dir1,  

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
				ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS,
				
				
				
				//optimize
				int			numberOfParts,		// the number of products available
							int*		partID,				// pointer to the head of the array of product ID numbers
							float*		value,				// the array of values
							int*		optimizeLotSize,	// boolean variable which decides if lot size is to be optimized
							int*		optimizeTbatch,		// boolean variable which decides if transfer batch is to be optimized
							float*		lotsizeValue,		// original value of lotsize for each part
							float*		tbatchValue

				
				)  
{
    //************************************************************************************8

      Clear_model_batch(collect, dir1);  

			 General_time (time1,  time2,  u_limit,  lab_var,  eq_var,  part_var);

			 sprintf(buf1, "Can open  ssfds\n %s %f \n", varlocaldir, t_tot1);
             Message(buf1);


			 Labor_data (l_count,    x1 ,    x2,    x3,    x4,
                 x5,    x6,    x7,  x8);

			 Equip_data (eq_count,    e_x1,    e_x2,    e_x3,    e_x4,   e_x5,
                 e_x6,    e_x7,    e_x8,    e_x9,  e_x10);

		     Parts_data (p_count,    p_x1,    p_x2,    p_x3,    p_x4,
                    p_x5,    p_x6,  p_x7);

		     Oper_data (op_count,   o_x1,    o_x2,    o_x3,    o_x4,    o_x5,
                     o_x6,    o_x7,    o_x8,    o_x9,
                     o_x10,   o_x11,   o_x12,   o_x13,
                     o_x14,   o_x15,   o_x16,   o_x17);

			 Route_data (r_count,    r_x1,    r_x2,    r_x3,    r_x4);

			 Ibom_data (ib_count,    i_x1,    i_x2,    i_x3);

			sprintf(buf1, "Can open ff \n %s %f %f\n", varlocaldir, t_tot1, time1);
			Message(buf1);


			  Run_model (iWID,   ierrcode,   ilab_err,   ieq_err, 
                 ipart_err,   ioper_err,   iroute_err,   iibom_err,  full,  MESS);
    

			  sprintf(buf1, "Can open ffjhk \n %s %f %f\n", varlocaldir, t_tot1, time1);
		      Message(buf1);


		/*	check_util();


			Optimize(			numberOfParts,		// the number of products available
									partID,				// pointer to the head of the array of product ID numbers
									value,				// the array of values
									optimizeLotSize,	// boolean variable which decides if lot size is to be optimized
									optimizeTbatch,		// boolean variable which decides if transfer batch is to be optimized
									lotsizeValue,		// original value of lotsize for each part
									tbatchValue);


				*/

			return(-1);
}



//
//
// Output files are named
//
// xxx.gen
// xxx.lab
// xxx.part
// xxx.prt
// xxx.opr
//

/* ************************************************************** */
int  check_loaded ( void )
/* ************************************************************** */
{

  LOADED = TRUE;

  // Message(" Warning: I have not really checked the data yet!!!");

return(TRUE);
}


/**************************************************************** */
int  gdtestread(char  * fileout) {


	FILE * fid;
	char  c1;
	fid = fopen(fileout, "rt");
	c1 = 'a';
	while (c1 != EOF) {
		  c1 = getc(fid);    
	}
	fclose(fid);
	return(-1);
}
/* ************************************************************** */
int   write_flat_file_results ( void )
/* ************************************************************** */
{ int i, j;
  p_LABOR * tlab;
  p_EQUIP * teq;
  p_PART  * tpart;
  p_OPAS  * toper;


  float * eq_cellfromto;

  char name[80];
  char buf[80];

  FILE * fileout;


  int eqsize;
  double * eq_scrap;
  double * eq_time;
  double * eq_X;
  int      cell_count;
  double x0, x1, x2, x3, x31, x32;
  
  char xx1 [MESSAGE_SIZE];

  strcpy(name, "RESULTS");
  strcat(name, ".gen");

 
  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);

   fileout = fopen(xx1, "wt");
    sprintf(buf, " about to put results into file %s :: %s", varlocaldir, xx1);
  Message(buf);

   if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
   }

#if defined (HLIM)
   fprintf(fileout, "WID,Product Family,TotalProd,TotalShipped,TotalStarted,Scrap,WIP,FlowTime\n");
   fprintf(fileout, "%d, TOTAL, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n", WID, total_pro, total_shi, total_pro+total_scr, total_scr, total_wip, total_ft);
#else
   fprintf(fileout, "WID,TotalProd,TotalShipped,TotalStarted,Scrap,WIP,FlowTime\n");
   fprintf(fileout, "%d, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n", WID, total_pro, total_shi, total_pro+total_scr, total_scr, total_wip, total_ft);
#endif


   fclose (fileout);

   gdtestread(name);

  /********************************/
  strcpy(name, "RESULTS");
  strcat(name, ".lab");


  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);

  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }

 sprintf(buf, " about to put results into file %s ", name);
 // Message(buf);

  fprintf(fileout, "WID,LaborID,SetupUtil,RunUtil,AbsUtil,Idle,Qprocess,QWait,Qtotal\n");

  for(tlab=hd_labor->next_lb; tlab!=NULL; tlab=tlab->next_lb) {
            fprintf(fileout, "%d, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n", WID, tlab->name, tlab->uset, tlab->urun, tlab->absrate,
                MAX(0.0, 100.0 - tlab->ul), tlab->qpl, tlab->qwl, tlab->ql);
  }


   fclose (fileout);
   gdtestread(name);

  /********************************/

  strcpy(name, "RESULTS");
  strcat(name, ".eq");
  cell_count = 1;

  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);

  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }

   sprintf(buf, " about to put results into file %s ", name);
   //Message(buf);

  fprintf(fileout, "WID,EquipID,SetupUtil,RunUtil,RepUtil,LabWaitUtil,Idle,Qprocess,QWait,Qtotal\n");

        for(teq=hd_equip->next_eq; teq!=NULL; teq=teq->next_eq) {
			if (cell_count < teq->cellid) cell_count = teq->cellid;
            fprintf(fileout, "%d, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                   WID, teq->name, teq->uset, teq->urun, teq->udown, teq->uwait, MAX(0.0, 100.0-teq->u), teq->qp, teq->qw, teq->q);
        }

   fclose (fileout);
   gdtestread(name);
  /********************************/

  strcpy(name, "RESULTS");
  strcat(name, ".prt");

  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);

  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }
  
   sprintf(buf, " about to put results into file %s ", name);
   //Message(buf);
  
  fprintf(fileout, "WID,ProdID,TotalGoodProd,ShippedProd,GoodForAsmProd,ScrapInAsm,Scrap,FlowTime,WIP,LTEquip,LTLabor,LTSetup,LTRun,LTWaitLot,LTWaitAsm,LTGather\n");

        for(tpart=hd_part->next_pt; tpart!=NULL; tpart=tpart->next_pt) {
            if (tpart->tgatherbatch != 0) {
                tpart->w_lot -= tpart->tgather;
                tpart->tsgood -= tpart->tgather;
                x1 = tpart->tgather * t_tot1 * tpart->dlam * tpart->lotsiz * (1.0 - tpart->scrap);
                tpart->tgather = 0.0;
                tpart->qpart -= x1;
            }

            fprintf(fileout, "%d, %s, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f \n",
                        WID, tpart->name, tpart->total, tpart->shipped, tpart->demd_ass, tpart->demd_scrap, tpart->scrapped, tpart->tsgood, tpart->qpart,
                        tpart->w_equip, tpart->w_labor, tpart->w_setup, tpart->w_run, tpart->w_lot, tpart->t_w_asm, tpart->tgather);
        }
   fclose (fileout);
   gdtestread(name);
  /********************************/

  strcpy(name, "RESULTS");
  strcat(name, ".opr");


  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);
  
  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }

   sprintf(buf, " about to put results into file %s ", name);
   //Message(buf);

  fprintf(fileout, "WID,ProdID,OpID,EqSetTime,EqRunTime,LabSetTime,LabRunTime,FlowTime,WIP,LTEquip,LTLabor,LTSetup,LTRun,LTWaitLot,VisitsPer100,VisitsPerGood,AverLotSize,NumSetups\n");

        for(tpart=hd_part->next_pt; tpart!=NULL; tpart=tpart->next_pt) {
          for(toper=tpart->n_opas->n_opas->n_opas->n_opas; toper!=NULL; toper=toper->n_opas) {


             // USING OLD NAME = OP_ID in the access code !!!
             fprintf(fileout, "%d, %s, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                        WID, tpart->name,
                        toper->oldname,
                        toper->ueset,   toper->uerun,   toper->ulset, toper->ulrun, toper->flowtime,  toper->qpoper,
                        toper->w_equip, toper->w_labor, toper->w_setup, toper->w_run, toper->w_lot, toper->vper100, toper->vpergood, toper->lsize, toper->n_setups);
         } // for toper

         if (tpart->tgather > EPSILON) {
                toper = tpart->n_opas->n_opas;
                x1 = tpart->tgather * t_tot1 * tpart->dlam * tpart->lotsiz * (1.0 - tpart->scrap);
                fprintf(fileout, "%d, %s, %s, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f \n",
                        WID, tpart->name,
                        toper->oldname,
                        0.0,                  0.0,             0.0,          0.0,         tpart->tgather*t_tot1,    x1,
                        0.0,                  0.0,              0.0,            0.0,       tpart->tgather*t_tot1, 100.0,            1.0,           tpart->lotsiz * (1.0 - tpart->scrap), 1.0);
         } // if tgather

       }// for tpart
  fclose(fileout);
  gdtestread(name);

  /********************************/


  strcpy(name, "RESULTS");
  strcat(name, ".ass");


  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);
  
  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }

 sprintf(buf, " about to put results into file %s ", name);
 // Message(buf);

  fprintf(fileout, "WID,ReportType,Name,AssetUtil,Throughput,LeadTime,AvailTime,UnSchedDT,Setup,LabWtTim,ScrapTim,BlckTim,BusyWithOther,IdleTim,DutyCycle,CapacityUsed,NoValAdd,RunSpd,RatSpd,ActSpd,QualTim,ScrapPcnt,ScrapAssmb\n");

  // need asset util for equipment first.

  for(teq=hd_equip->next_eq,i=0; teq!=NULL; teq=teq->next_eq) {
    i++;
    teq->index = i;
  }

  eqsize = i+2;


  eq_scrap = (double *) malloc (sizeof(double) * eqsize);
  eq_time  = (double *) malloc (sizeof(double) * eqsize);
  eq_X     = (double *) malloc (sizeof(double) * eqsize);
  for(i=0;i<eqsize;i++) {
      eq_scrap[i]= 0.0;
      eq_time [i]= 0.0;
      eq_X    [i]= 0.0;
  }


  for(tpart=hd_part->next_pt; tpart!=NULL; tpart=tpart->next_pt) {
       for(toper=tpart->n_opas->n_opas->n_opas->n_opas; toper!=NULL; toper=toper->n_opas) {
          i = toper->eq_ptr->index;
          if (toper->vper100 > 0) {
               eq_scrap[i] += (toper->vpergood*100 / toper->vper100) * toper->uerun;
               eq_X    [i] += toper->lvisit * toper->lsize;
               eq_time [i] += toper->lvisit * toper->lsize * toper->flowtime/t_tot1;
          }
       }
  }



  for(teq=hd_equip->next_eq; teq!=NULL; teq=teq->next_eq) {
     i = teq->index;

     if ((teq->num > 0) && (teq->urun > EPSILON)) {
          eq_scrap[i] = 100.0 * eq_scrap[i] / (teq->urun * teq->num);
     }
     else eq_scrap[i] = 0.0;


     //sprintf(buf1, "asset %d %f %f \n", i, eq_time[i], eq_X[i]);
     //Message(buf1);


     eq_X[i] /= (t_tot1*t_tot2);  //  total pieces / hour


     x0 = teq->urun * (1.0/teq->facrun) * eq_scrap[i] / 100.0;
     x1 = teq->urun;


     x2 = (100.0 / teq->facrun);
     x3 = eq_scrap[i];


     fprintf(fileout,  "%d, %d, %s, %.2f, %.3f, %.2f,  %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f \n",
     // fprintf(fileout,
     // WID type name AssetUtil Throughput LeadTime
     // AvailTime UnSchedDT Setup LabWtTim ScrapTim BlckTim BusyWithOther IdleTim
     // DutyCycle CapacityUsed NoValAdd
     // RunSpd RatSpd ActSpd
     // QualTim ScrapPcnt\n");

           WID, 0, teq->name, x0, eq_X[i], eq_time[i],
           x1, teq->udown, teq->uset, teq->uwait, 0.0, 0.0, 0.0, MAX(0.0, (100.0 - teq->u)),
           100.0, 0.0, 0.0,
           x2, 1.0, 1.0/teq->facrun,
           x3, 100.0-eq_scrap[i], 0.0);


  }



  for(tpart=hd_part->next_pt; tpart!=NULL; tpart=tpart->next_pt) {

     x0 = x3 = x31 = x32 = 0.0;
     if ((tpart->total+tpart->scrapped) > EPSILON) {
           x31 = 100*tpart->scrapped / (tpart->total+tpart->scrapped);
           x32 = 100*tpart->demd_scrap / (tpart->total+tpart->scrapped);
           x3 = (tpart->scrapped + tpart->demd_scrap) / (tpart->total+tpart->scrapped);
           x3 = 100 * (1.0 - x3);
           x0 = x3;
     }
     else x3 = 100.0;

     fprintf(fileout,  "%d, %d, %s, %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f,  %.2f, %.2f, %.2f \n",
     // fprintf(fileout,
     // WID type name AssetUtil Throughput LeadTime
     // AvailTime UnSchedDT Setup LabWtTim ScrapTim BlckTim BusyWithOther IdleTim
     // DutyCycle CapacityUsed NoValAdd
     // RunSpd RatSpd ActSpd
     // QualTim ScrapPcnt\n");

           WID, 2, tpart->name, x0, tpart->total, tpart->tsgood,
           100., 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0,
           100.0, 0.0, 0.0,
           100.0, 1.0, 1.0,
           x3, x31, x32);
  }

  free(eq_scrap);
  free(eq_time);
  free(eq_X);
  fclose(filein);

  gdtestread(name);

    /********************************/
  strcpy(name, "RESULTS");
  strcat(name, ".pol");


  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, name);
  
  fileout = fopen(xx1, "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for results data ", xx1);
        Message(buf);
        return(ACCESS_TRUE);
  }

   sprintf(buf, " about to put results into file %s ", name);
  //Message(buf);

  // fprintf(fileout, "Fromcell,Tocell,WIPlots, WIPpieces, WIPtransferbatches\n");

	
  eq_cellfromto = ( float * ) malloc ((cell_count+1) * (cell_count+1) * 3 * sizeof(float)); 
  memset (eq_cellfromto, '\0', (cell_count+1) * (cell_count+1) * 3 * sizeof(float));
  
  //do_Polca (eq_cellfromto, cell_count);

  for (i=0; i<=cell_count; i++) {
 	  for(j=0; j<=cell_count; j++) {
            fprintf(fileout, "%d, %d, %.2f, %.2f, %.2f \n", i, j, eq_cellfromto[arr3(i,j,0,cell_count,3)], eq_cellfromto[arr3(i,j,0,cell_count,2)], eq_cellfromto[arr3(i,j,1,cell_count,3)], eq_cellfromto[arr3(i,j,0,cell_count,2)], eq_cellfromto[arr3(i,j,1,cell_count,3)]);
   }
  }

   fclose (fileout);  
   gdtestread(name);

   free(eq_cellfromto);

  /********************************/


  sprintf(buf, " done..." );
  //Message(buf);


return(0);
}



/*--------------------*/
void  Message( char * s)
{

  MessageBox( NULL, s, "In MPX Calculations ", MB_OK | MB_ICONEXCLAMATION | MB_TASKMODAL );

}
void DB_Message(char  *s)
{    return; }
void Mess_UP(char  *s)
{    return; }
void Mess_DOWN(void)
{    return; }
void disp_Close(void)
{    return; }
void screen_Crt (char  *s, int height, int width)
{  /*  Message(s); */ }
void screen_Upd (char  *s)
{  /*   Message(s);  */ }
void screen_Close ()
{    return; }
/*--------------------*/


void  warn_err ( char * buf1, int level,  char * str_l, char * str_e, char * str_p, char * str_o, char * str_r, char * str_i)
{ int i;
  FILE * file_err;
  char xx1 [MESSAGE_SIZE];
  char buf [MESSAGE_SIZE];

  for(i=0;i<strlen(buf1); i++) {
    if (buf1[i] == '\n') buf1[i] = '-';
  }

  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, "results.err");
  file_err = fopen (xx1, "at");

  if (file_err == NULL)  {
     sprintf(buf1,"Cannot open error file to write message !1!?\n %s \n", xx1);
	 Message(buf1);
     return;
  }

  fprintf( file_err, "%d,In Calculations,%s, 1 ,%s,%s,%s,%s,%s,%s\n", level, buf1, str_l, str_e, str_p, str_o, str_r, str_i);

  fclose(file_err);

  if (level == 0) {
     strcpy (xx1, varlocaldir);
     strcat(xx1, "\\");
     strcat(xx1, "STOP.1");
     file_err = fopen(xx1, "wt");
     if (file_err == NULL)  {
        sprintf(buf, "Cannot open error file to write message !2!??\n %s \n", xx1);
        Message(buf);
		return;
     }
     fprintf( file_err, "%d,In Calculations,%s, 1 ,%s,%s,%s,%s,%s,%s\n", level, buf1, str_l, str_e, str_p, str_o, str_r, str_i);
     fclose(file_err);
	 gdtestread("results.err");
  }

return;
}

void  warn_err_header ( void )
{ int i;
  FILE * file_err;
  char xx1[MESSAGE_SIZE];
  char buf [MESSAGE_SIZE];

  strcpy (xx1, varlocaldir);
  strcat(xx1, "\\");
  strcat(xx1, "results.err");
  file_err = fopen (xx1, "wt");

  if (file_err == NULL)  {
     sprintf(buf, "Cannot open error file to write message %s !3!??\n %s \n", xx1, varlocaldir);
     Message(buf);
	 return;
  }

     sprintf(buf, "Can open error file to write message %s !3!??\n %s %f \n", xx1, varlocaldir, t_tot1);
     Message(buf);

  fprintf( file_err, "Level,Table,Text,mpxFlag,laborid,equipid,partid,operID,routeId,ibomID\n");

  fclose(file_err);

  gdtestread("results.err");

return;
}
//*******************************************************
int   oper_read ( char * filename )
{
  int fld_size[] = {15, 1, 3, 3, 3, 10, 6, 2, 2, 10};
                   // size of fields for fixed record sizes.
  int field_count;

  char buf[128];

  char fname[255];

  char ppart[128];
  char eq_name [NAME_SIZE*3];
  char op_name [NAME_SIZE*3];
  double esetup;
  double epiece;

  int i, done;

  // count plants
  // get 2 ops per plant - manufacture, que,
  // split transport into interplant stuff.

 //essage("about to change pointer ");

  strcpy(fname, filename);

 //essage("after to change pointer ");
 //essage(fname);

  filein = fopen(fname, "rt");
  if (filein == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for download data", filename);
        Message(buf);
        return(ACCESS_TRUE);
  }

  fileout = fopen("data.txt", "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for reformatted MRP data ", "data.txt");
        Message(buf);
        return(ACCESS_TRUE);
  }

  fprintf(fileout, "ProdDesc, Opnam, OpNum, EquipDesc, PercentAssign, eqsetuptime, eqruntime, labsetuptime, labruntime\n");



  field_count = sizeof(fld_size)/sizeof(int);
  done = FALSE;

  while  (!done) {

          //read_f_str_n(sizeof(fld_size)/sizeof(int));
          read_comma_str_n(sizeof(fld_size)/sizeof(int));
          if (readerr == READEND) goto end;
          if ((readerr != READ_NO_ERR) && (readerr != READSHORT)) {
                         Message(" Read error Infile 4 ?? \n stopping at line  ... \n");
                         for (i=0; i<6; i++)
                             printf ("%s", reada[i]);
                         printf("\n");
                         return(ACCESS_TRUE);
          }

          for (i=0; i<field_count; i++) xspace(reada[i]);

          strcpy(ppart, reada[0]);
          sprintf(op_name, "%s_%s", reada[4], reada[9]);
          sprintf(eq_name, "%s%s", reada[2], reada[3]);


          esetup = atof(reada[6]) / 1.5;
          epiece = atof(reada[5]) / 1.5;

          fprintf(fileout, "%s, %s,%s,  %s, 100.0,  %.3f, %.3f, %.3f, Luse*%.3f\n",
               ppart, op_name, reada[4], eq_name, esetup, epiece, esetup, epiece);

        } // end while read

end:

  fclose (filein);
  fclose (fileout);

 return(0);
}

#define MAX_LEVELS 20

//*******************************************************
int  ibom_read ( char * filename)
{ int done, i, j, dup;
  char fname[255];

  // old
  //int fld_size[] = {1, 4, 19, 18, 11, 8, 2, 2, 8, 7, 10, 4, 2, 2, 6};
     //                5  24  42  53 61 63 65 73 80  90 94 96 98 104
     //                1   2   3   4  5  6  7  8  9  10 11 12 13  14
  // new
  int fld_size[] = {1, 4, 19, 18,  3,  9, 9, 8, 4, 4, 2, 2, 2};
     //                5  24  42  45  54 63 71 75 79 81 83 85
     //                1   2   3   4   5  6  7  8  9 10 11 12
     // old field   0  1   2   3   6   8  6 10 11 12 13 14 14
     //                                x  x  x     x  x
                   // size of fields for fixed record sizes.
  int field_count;
  p_PART * tpart;

  p_PART * p_levels [MAX_LEVELS];  // points to names of assemblies
                                   // through the levels to 0

  p_PART * par_part;               // parent part for component.


  char DPNAME[MAX_LEVELS][50];
  char DPDept[MAX_LEVELS][50];
  char Pdept[50];
  char buf[256];
  char root_part[256];


  char buf1[256];
  char p_name[256];
  char * c1;
  char eq_name[256];
  int level;
  p_IBOM * cibom_1;
  int root_level;

    // 0 - line printer
    // 1 - level
    // 2 - name
    // 3 - name
    // 4 -
    // 5 -
    // 6 - ar  as required ... gone
    // 7 -
    // 8 -
    // 9 -
    // 10 - part desc
    // 11 -
    // 12 - plant 2  - plt
    // 13 - purchase, manufacture  - sc  src- source
    // 14 -

  int action = FALSE;  // deciding which parts / iboms to read....

  int skip_level;     // if duplicate subassemblies are used we
                      // do not want to count them twice in the IBOM
                      // so that once we hit a dup part, skip all parts
                      // with a level less that the level of the dup
                      // part.
                      // Also if we skip parts due to as_req, purchase, edetail
                      //  skip their components as well.



  strcpy(fname, filename);
  filein = fopen (fname, "rt");
  if (filein == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for download data", fname);
        Message(buf);
        return(ACCESS_TRUE);
  }


  fileout = fopen("data.txt", "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for reformatted MRP data ", "data.txt");
        Message(buf);
        return(ACCESS_TRUE);
  }

  initial_model();
  Clear_Model();


  fprintf(fileout, "ParentID, ParentDept, CompId, CompDept, UPA\n");


    for (i=0; i<12; i++) {
       p_levels [i] = NULL;
       strcpy(DPDept [i], sNULL);
    }



    done = FALSE;
    skip_level = MAX_LEVELS;
    field_count = sizeof(fld_size)/sizeof(int);

    while  (!done) {

          read_f_str_n(field_count, fld_size);
          if (readerr == READEND) goto next;
          if ((readerr != READ_NO_ERR) && (readerr != READSHORT)) {
                         Message(" Read error Infile1 ?? \n stopping at line  ... \n");
                         for (i=0; i<sizeof(fld_size)/sizeof(int); i++)
                             printf ("%s", reada[i]);
                         printf("\n");
                         goto next;
          }

          for (i=0; i<field_count; i++) xspace(reada[i]);


          if (reada[0][0] != '0') continue;
          if ( (reada[1][0] < '0') || (reada[1][0] > '9')) continue;

          if (strlen(reada[1]) < 1) continue;  // message line ....


          // skip parts with level below current skip level - duplicates
          if (skip_level < atoi(reada[1])) {
                 continue;
          }


          // change singapore parts to manufactured !!!
          if (strcmp("66", reada[9]) == 0) reada[10][0] = 'M';


          strcpy(p_name, reada[2]);
          strcat(p_name, reada[3]);

             c1 = strstr(p_name, " ");
             if ( c1 != NULL) {
               strcpy(buf1,c1+1);
               xspace(buf1);
               *c1 = '\0';
               //strcat(p_name, "__");
               //strcat(p_name, buf1);
               xspace(p_name);
             }
          xspace(p_name);

          // header part!!!
          if (atoi(reada[1]) == 0)  { // not reading yet

                   strcpy(root_part, p_name);
                   action = TRUE;
                   root_level = atof(reada[1]);
          } // if FALSE not reading yet


          if (strlen(p_name) >= NAME_SIZE) {
              sprintf(buf1, "Part %s has too long a name \n truncating name... \n", p_name);
              Message(buf1);
              p_name[19] = cNULL;
          }



          tpart = find_part(p_name);
          dup = FALSE;
          if (tpart != NULL) {
              sprintf(buf1, "Duplicate part %s \n Not adding part record \n", p_name);
              MessageBox( NULL, buf1, "dup part", MB_OK | MB_TASKMODAL);
              dup = TRUE;
              skip_level = atoi(reada[1]);
          }
          else {
              tpart = add_part(p_name, PERM, "");

              tpart->lotsiz = -1;

              strcpy(buf1, reada[9]);
              strcat(buf1, "_");
              strcat(buf1, "ZZ");

                   if (reada[10][0]== 'S') strcat(buf1, ":S/C");
              else if (strcmp(reada[9], "66") == 0)  strcat(buf1, ":M-SNG");
              else if (reada[10][0]== 'P') strcat(buf1, ":P");
              else                         strcat(buf1, ":M");

              if (reada[4][0] == '*')      strcat(buf1, ":AR");


              buf1[50-1] = '\0';


              strcpy(Pdept, buf1);
              level = atoi (reada[1]);
              strcpy(DPDept[level], buf1);
              skip_level = MAX_LEVELS;

              strcpy(DPNAME[level], p_name);


          }
          level = atoi (reada[1]);
          p_levels [level] = tpart;
          for (i=level+1; i<MAX_LEVELS; i++) p_levels[i] = NULL;


          if (level == root_level) tpart->ndmnd = 1.0;

          if (level != root_level) {
            par_part = p_levels [level-1];
            if (par_part == NULL) {
                       sprintf(buf1, "Jumping levels at part %s level %d\n exiting... \n ", p_name, level);
                       Message(buf1);
                       return(1);
            }


            cibom_1 = find_ibom2(p_name, par_part);
            if (cibom_1 != NULL) { // add necessary components...
              cibom_1->upa += atof (reada[5]);
            }
            else {
              cibom_1 = add_ibom2(p_name, par_part, PERM, "");
              cibom_1->upa = atof (reada[5]);
            }
          } // done adding as a IBOM


      if (level != 0) fprintf(fileout, "%s,%s,%s,%s,%f \n",  par_part->name, DPDept[level-1],  p_name, Pdept,  cibom_1->upa);


    }  // end of while


next:
  fclose(filein);
  fclose(fileout);



return (0);
}



//*******************************************************
int  part_read ( char * filename )
{ int done, i;
  p_PART * tpart;
  char p_name [250];
  char buf    [250];
  char Pdept  [250];
  char fname[255];
  double lotsiz;

  strcpy(fname, filename);
  filein = fopen (fname, "rt");
  if (filein == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for download data", fname);
        Message(buf);
        return(ACCESS_TRUE);
  }

  fileout = fopen("data.txt", "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for reformatted MRP data ", "data.txt");
        Message(buf);
        return(ACCESS_TRUE);
  }

  fprintf(fileout, "ProdDesc, ProdDept, Lotsiz \n");


    done = FALSE;

    while  (!done) {

nextrec:
          read_comma_str_n(12);
          if (readerr == READEND) goto end;
          if ((readerr != READ_NO_ERR) && (readerr != READSHORT)) {
                         Message(" Read error Infile 2 ?? \n stopping at line  ... \n");
                         printf("\n");
                         for (i=0; i<6; i++)
                             printf ("%s | ", reada[i]);
                         printf("\n");
                         return (1);
          }

          tpart = find_part(reada[0]);
          if (tpart != NULL) goto nextrec;
          tpart = add_part(reada[0], PERM, "");

          lotsiz = MAX(1.0, atof(reada[4]));

          strcpy(Pdept, reada[10]);



          fprintf(fileout, "%s,%s, %s \n", reada[0], reada[10], reada[4]);

    }  // end of while

end:

 fclose(filein);
 fclose(fileout);
return (0);
}
//*******************************************************
int  wctr_read ( char * filename )
{
//* fixed format
//*3    3       6          6  20      3    2          1      7        2         5         7      7         7        1       5        7        7      1      7         7          7       7       4         2        4        1    50
//*dept wctr act_date Keyword lcname mach dispatch schdshft scdeffpc rlfacqty sht_hrs plnquedy plniqued fxdlyhrs cnstrflg tolrnpct fcrwklab fcrwmch linflo sumchpct sumlabpct rnmchpct rnlabpct process plt_site plt_func status text
//*
//*  2     5       5     5        5    1    5     5   5    5    5       5
//* clnup ratedpt estpt ratedsu estsu lock lrpt lept lrsu lesu unperc lunper
//*
//*
//*
//* 0   1       2      3        4     5     6         7       8       9        10       11        12     13        14       15      16       17       18     19       20        21      22      23        24      25        26    27,28
//*dept wctr act_date Keyword lcname mach dispatch schdshft scdeffpc rlfacqty sht_hrs plnquedy plniqued fxdlyhrs cnstrflg tolrnpct fcrwklab fcrwmch linflo sumchpct sumlabpct rnmchpct rnlabpct process plt_site plt_func status text
//*  29     30     31     32     33    34   35   36   37   38    39    40
//* clnup ratedpt estpt ratedsu estsu lock lrpt lept lrsu lesu unperc lunper

  int fld_size[] = {3, 3, 6, 6, 20, 3, 2, 1, 7, 2, 5, 7, 7, 7, 1, 5, 7, 7, 1, 7, 7, 7, 7, 4, 2, 4, 1, 25, 25, 2, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5};
                   // size of fields for fixed record sizes.
  int field_count;

  p_EQUIP * eq;
  int done, i;
  double x1, x2;
  char eq_name [NAME_SIZE];
  char buf1    [MESSAGE_SIZE];
  char dept    [MESSAGE_SIZE];
  char buf     [MESSAGE_SIZE];

  char fname[255];

  strcpy(fname, filename);
  filein = fopen (fname, "rt");
  if (filein == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for download data", fname);
        Message(buf);
        return(ACCESS_TRUE);
  }

  fileout = fopen("data.txt", "wt");
  if (fileout == NULL) {
        sprintf(buf, " ERROR : \n Cannot open file %s for reformatted MRP data ", "data.txt");
        Message(buf);
        return(ACCESS_TRUE);
  }

  fprintf(fileout, "EquipDesc, EquipDept \n");



  field_count = sizeof(fld_size)/sizeof(int);

  done = FALSE;

  while  (!done) {

          //read_f_str_n(sizeof(fld_size)/sizeof(int), fld_size);
          read_comma_str_n( 25 );  // not get all....
          if (readerr == READEND) goto end;
          if ((readerr != READ_NO_ERR) && (readerr != READSHORT)) {
                         Message(" Read error Infile 3 ?? \n stopping at line  ... \n");
                         for (i=0; i<6; i++)
                             printf ("%s", reada[i]);
                         printf("\n");
                         return (1);
          }

          for (i=0; i<field_count; i++) xspace(reada[i]);


       //   if (ship) add
       //     add_equip()
       //   else
       //     check if added already
       //       yes continue
       //     else add temp & save delay
       //     check if plant is added
       //       yes continue
       //     else add perm

           if ((strstr(reada[3], "SHIP") != NULL) || (strstr(reada[4], "SHIP") != NULL)) {
              strcpy(eq_name, reada[0]);
              strcat(eq_name, reada[1]);

              strcpy(buf1, reada[24]);
              strcat(buf1, "_SHIP_");
              strcat(buf1, reada[4]);
              strcat(buf1, "_");
              strcat(buf1, reada[3]);
              buf1[NAME_SIZE-1] = '\0';

              strcpy(dept, buf1);
            }
           else {

              strcpy(eq_name, reada[0]);
              strcat(eq_name, reada[1]);
              strcpy(dept, reada[24]);


            } // else

        fprintf(fileout, "%s,%s \n", eq_name, dept);

      } // read done


end:

  fclose (filein);
  fclose (fileout);
return(0);
}

void do_Polca (float * eq_cellfromto, int cell_count )
{   

  p_PART  * tpart;
  p_OPAS  * toper;
  p_OPAS  * toper2;
  p_OPAS  * toper3;
  p_OPRT  * troute;
 


  char toname [NAME_SIZE];
  int from_cell; 
  int to_cell;
	float f1;
	float f2;
	float intotal;
	float outtotal;
	float in1;
	
	float * eq_wip;


  return;
  


  eq_wip = (float * )  malloc (3*cell_count *sizeof(float));
  memset (eq_wip, '\0', (cell_count) * 3 * sizeof(float));
  

  // loop on parts 
  //   step all opers
  //      total wip,lots,tbatch for each cell 
  //   step all opers / routing
  //      if route to oper leaves cell then
  //        determine cell1wip *fraction1
  //        look at to oper and get fraction2*wip(neew cell)
  //        add values to eqcellfromto

   
  for (tpart=hd_part->next_pt; tpart!= NULL; tpart=tpart->next_pt) {
      from_cell = 0;
	  to_cell = 0;
	  
	  toper=tpart->n_opas; // start at dock
	
    memset (eq_wip, '\0', (cell_count) * 3 * sizeof(float));
  	for(toper=tpart->n_opas->n_opas->n_opas->n_opas; toper!=NULL; toper=toper->n_opas) {
	  from_cell = toper->eq_ptr->cellid;
	  eq_wip[arr(from_cell, 0, cell_count)] += toper->qpoper / toper->lsize;
	  eq_wip[arr(from_cell, 1, cell_count)] += toper->qpoper / ((toper->lsize/tpart->lotsiz)*tpart->tbatch);
	  eq_wip[arr(from_cell, 2, cell_count)] += toper->qpoper;
    }; // totalled all wip for this part into cells


  	for(toper=tpart->n_opas->n_opas->n_opas->n_opas; toper!=NULL; toper=toper->n_opas) {
	  from_cell = toper->eq_ptr->cellid;
	      troute = find_rt_from(toper->name, tpart);
		  strcpy(toname, troute->to_op->name);

		  //do all from ops with that from name
		  for(toper2=toper; strcpy(toper2->name,toper->name)!= 0; toper2=toper2->n_opas) {
			from_cell = toper2->eq_ptr->cellid;
				outtotal = get_total_percent_from(tpart, toper2);

				//do all from ops with that to name
				for(toper3=toper; strcmp(toper3->name,toname)!= 0; toper3=toper3->n_opas) {
					to_cell = toper2->eq_ptr->cellid;
					if ((from_cell != to_cell)  &&  (strcmp(toper3->name, "STOCK")==0)  && (strcmp(toper3->name, "SCRAP")==0)) {
						intotal  = get_total_percent_to(tpart, toper2); 
						if (intotal != 0) 
						   in1 = fraction_to(tpart, troute, toper2, toper3);
						else { intotal = 1; in1 = 0;};
						f1 = toper2->cmmprp *troute->cmmpij/outtotal/100;
						f2 =  in1/intotal;

		 
					  eq_cellfromto[arr3(from_cell,to_cell, 0, cell_count, 3)] += f1 * eq_wip[arr(from_cell, 0, cell_count)] + f2 * eq_wip[arr(to_cell, 0, cell_count)];
					  eq_cellfromto[arr3(from_cell,to_cell, 1, cell_count, 3)] += f1 * eq_wip[arr(from_cell, 1, cell_count)] + f2 * eq_wip[arr(to_cell, 1, cell_count)];              
					  eq_cellfromto[arr3(from_cell,to_cell, 2, cell_count, 3)] += f1 * eq_wip[arr(from_cell, 2, cell_count)] + f2 * eq_wip[arr(to_cell, 2, cell_count)];
					}; // endif
				}; // for all to opers 3
				}; // for all opers 2
		}; // for all opers
	}  // for all parts 


  free(eq_wip);

return;

};

float get_total_percent_from(p_PART * tpart, p_OPAS * toper)
{

   p_OPAS * toop;
   p_OPAS * toper3;
   p_OPRT * troute;
   p_OPRT * trout2;

   int cellid;
   int to_cell;
   char toname [NAME_SIZE];

   float tot;


   cellid = toper->eq_ptr->cellid;

   troute = find_rt_from(toper->name, tpart);
   tot = 0;

      strcpy(toname, troute->to_op->name);
	  for(trout2=troute; strcmp(troute->from_op->name,toper->name)!= 0; trout2=trout2->n_oprt) {
	
 
      toop = trout2->to_op;

	  //do all from ops with that to name
	  for(toper3=toop; strcmp(toper3->name,toname)!= 0; toper3=toper3->n_opas) {
		to_cell = toper3->eq_ptr->cellid;
		if (cellid != to_cell) 
			tot += troute->cmmpij * toper3->cmmprp / 100.0;
	  }   // all to  ops
   } //loop on routing

	return(tot);

}

float fraction_to(p_PART * tpart, p_OPRT * troute, p_OPAS * toper2, p_OPAS * toper3)
{

   
	float f1;


	
    f1 = toper2->vper100 * toper2->lsize * troute->cmmpij;



	return(f1);

};

float get_total_percent_to(p_PART * tpart, p_OPAS * toper2)
{

	p_OPAS * from_op;
	p_OPRT * from_rt;

	int cellid;
    int from_id;

	char toname [NAME_SIZE];
	char fromname[NAME_SIZE];

	float tot;


	cellid = toper2->eq_ptr->cellid;
	from_rt = find_rt_to(toper2->name, tpart);
	if (from_rt == NULL) return(0);
	strcpy(toname, toper2->name);


	tot = 0;
	for(from_rt=from_rt;strcmp(from_rt->to_op->name, toname)==0; from_rt=from_rt->n_oprt) {
		strcpy(fromname, from_rt->from_op->name);

		for (from_op = from_rt->to_op; strcmp(fromname,from_op->name) ==0; from_op=from_op->n_opas) {
			if (cellid != from_op->eq_ptr->cellid) {

			  tot += from_op->vper100 * from_op->lsize * from_rt->cmmpij;


			}// end if different cells
		} // all names matching from op
	} // all possible routes to toper

	return(tot);

};


// # include init_m


/* ******************************************************* */
void initial_model ( void )
/* ******************************************************* */
{

  if (hd_labor == NULL) {
        hd_labor = (p_LABOR *) omalloc(MY_MEM_TAG, sizeof(struct LABOR) );
        if (hd_labor == NULL) {
                sprintf(buf1,"INSUFFICIENT MEMORY: #(18a) \n Insufficient Memory to add ANY LABOR GROUP");
                warn_err ( buf1, 0,  " ",  " ", " ", " ", " ", " ");
                return;
        }
        memset(hd_labor, '\0', sizeof (p_LABOR));

        strcpy(hd_labor->name, " ");
        hd_labor->keep= PERM;
        hd_labor->num= -1;
        hd_labor->absrate=0.0;
        hd_labor->facovt=0.0;
        hd_labor->facset=1.0;
        hd_labor->facrun=1.0;
        hd_labor->faccvs=1.0;
        hd_labor->next_lb=NULL;
        hd_labor->oldname=NULL;
  }

  if (hd_equip == NULL) {
        hd_equip = (p_EQUIP *) omalloc(MY_MEM_TAG, sizeof(struct EQUIP) );
        if (hd_equip == NULL) {
                sprintf(buf1,"INSUFFICIENT MEMORY: #(18b) \n Insufficient Memory to add ANY EQUIPMENT GROUP");
                warn_err ( buf1, 0,  " ",  " ", " ", " ", " ", " ");
                return;
        }
        memset(hd_equip, '\0', sizeof (p_EQUIP));

        strcpy(hd_equip->name, " ");
        hd_equip->keep= PERM;
        hd_equip->num= -1;
        hd_equip->lab_ptr=hd_labor;
        hd_equip->mttf=1.0;
        hd_equip->mttr=0.0;
        hd_equip->facovt=0.0;
        hd_equip->facset=1.0;
        hd_equip->facrun=1.0;
        hd_equip->faccvs=1.0;
        hd_equip->next_eq=NULL;
        hd_equip->oldname=NULL;
  }

  if (hd_part == NULL) {
        hd_part = (p_PART *) omalloc(MY_MEM_TAG, sizeof(struct PART) );
        if (hd_part == NULL) {
                sprintf(buf1,"INSUFFICIENT MEMORY: #(18c) \n Insufficient Memory to add ANY PART DATA");
                warn_err ( buf1, 0,  " ",  " ", " ", " ", " ", " ");
                return;
        }
        memset(hd_part, '\0', sizeof (p_PART));

        strcpy(hd_part->name, " ");
        hd_part->keep= PERM;
        hd_part->ndmnd=0.0;
        hd_part->lotsiz=1.0;
        hd_part->tbatch= (-1.0);
        hd_part->facdem=1.0;
        hd_part->faccva=1.0;
        hd_part->next_pt=NULL;
        hd_part->oldname=NULL;
        hd_part->n_opas = NULL;
        add_opas("DOCK",  " ", hd_part, PERM, "");
        add_opas("STOCK", " ", hd_part, PERM, "");
        add_opas("SCRAP", " ", hd_part, PERM, "");
        hd_part->n_oprt = NULL;
        add_oprt("DOCK",  "STOCK", hd_part, PERM, "");
        hd_part->check_rout = TRUE;

        hd_part->n_cmpt = NULL;

  }

  if (hd_whi == NULL) {
        hd_whi = (p_WHATIF *) omalloc(MY_MEM_TAG, sizeof(p_WHATIF) );
        if (hd_whi == NULL) {
                sprintf(buf1,"INSUFFICIENT MEMORY: #(18d) \n Insufficient Memory to add ANY WHAT-IF CASES");
                warn_err ( buf1, 0,  " ",  " ", " ", " ", " ", " ");
                return;
        }
        memset(hd_whi, '\0', sizeof (p_WHATIF));

        strcpy(hd_whi->name,  sNULL);
        strcpy(hd_whi->fname, sNULL);
        hd_whi->n_whi=NULL;
        strcpy(hd_whi->comment, sNULL);
        hd_whi->n_whrec=NULL;
    }


return;
}


  //include  "c:\users\c_code\web_dll\link1_1b.C"
	/*
#include  "c:\users\c_code\web_dll\READ1B.C"

#include  "c:\users\c_code\web_dll\BFNVAL2.C"
//#include  "c:\users\c_code\web_dll\Brack2.c"
//#include  "c:\users\c_code\web_dll\Brent.c"
#include  "c:\users\c_code\web_dll\CHECK_4.C"
#include  "c:\users\c_code\web_dll\Combine.c"
#include  "c:\users\c_code\web_dll\Combine2.c"
#include  "c:\users\c_code\web_dll\CPY.C"
#include  "c:\users\c_code\web_dll\DATA_1.C"
#include  "c:\users\c_code\web_dll\DEL1.C"
#include  "c:\users\c_code\web_dll\DEL_STR.C"
#include  "c:\users\c_code\web_dll\GD_PAR0.C"
#include  "c:\users\c_code\web_dll\GDDECPT.C"
#include  "c:\users\c_code\web_dll\Getdef.h"
//#include  "c:\users\c_code\web_dll\GWRITE4A.C"
//#include  "c:\users\c_code\web_dll\HWRITE4.C"
#include  "c:\users\c_code\web_dll\IB_CHECK.C"
#include  "c:\users\c_code\web_dll\INIT_M.C"
#include  "c:\users\c_code\web_dll\LEUNG.C"
#include  "c:\users\c_code\web_dll\Link_1a.c"
#include  "c:\users\c_code\web_dll\MATX2.C"
// #include  "c:\users\c_code\web_dll\Mem3.c"
#include  "c:\users\c_code\web_dll\MPC_SWI.C"
#include  "c:\users\c_code\web_dll\Mpcc0.c"
#include  "c:\users\c_code\web_dll\Mpcc1f.c"
#include  "c:\users\c_code\web_dll\Mpcc2f.c"
#include  "c:\users\c_code\web_dll\MPCC3E.C"
#include  "c:\users\c_code\web_dll\MPCC4.C"
#include  "c:\users\c_code\web_dll\MPDEMD.C"
#include  "c:\users\c_code\web_dll\MPX95.C"
#include  "c:\users\c_code\web_dll\MPX95A.C"
#include  "c:\users\c_code\web_dll\mpx95i_i.c"
//#include  "c:\users\c_code\web_dll\OPT2.C"
//#include  "c:\users\c_code\web_dll\OPTIMI~1.C"

#include  "c:\users\c_code\web_dll\READ2A.C"
#include  "c:\users\c_code\web_dll\READ2B.C"
#include  "c:\users\c_code\web_dll\READ3.C"
#include  "c:\users\c_code\web_dll\READ44.C"
#include  "c:\users\c_code\web_dll\RENAM.C"
#include  "c:\users\c_code\web_dll\SETUP.C"

#include  "c:\users\c_code\web_dll\WG1.C"
#include  "c:\users\c_code\web_dll\WG1B.C"
#include  "c:\users\c_code\web_dll\WG2B.C"
#include  "c:\users\c_code\web_dll\WH3D.C"
#include  "c:\users\c_code\web_dll\WRITE3.C"
#include  "c:\users\c_code\web_dll\WRITE4B.C"
#include  "c:\users\c_code\web_dll\WRITE4C.C"     
*/


};  

 /* sean Larson 
	  c/o Paul ericksen
	  95 marvista way 


	port angles  wa 98362

	*/