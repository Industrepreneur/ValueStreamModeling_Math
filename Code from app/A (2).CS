using System;
using System.Data;
using System.Data.OleDb;
using ADODB;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
public class ClassA : CalcClass {

    public string localdir;
    public string username;

    public ClassA(string userdir):base(userdir + "\\mpxmdb.mdb") {
        this.varlocal = userdir;
        setGlobalVar();
    }

    public void CreateRunFile() {
        DbUse.CreateRunFile(varlocal, username);
    }

    public string errorMessageGlobal = "";

 //  master page line 2   <%@ Register Assembly="Infragistics2.Web.v12.1, Version=12.1.20121.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
 //   Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
    
//
// DLL calls
    /*
int  ichange_labor ( int idno, float nvalue);

int  iset_demand (int pcount, int * idno, float * nvalue);
int  iset_lotsize(int pcount, int * idno, float * nvalue);

int      iClear_model ( int collect , string dir1);
int      iRun_model (int iWID, int *  errcode, int *  lab_err, int *  eq_err, int *
                   part_err, int *  oper_err, int *  route_err, int *  ibom_err, int full, int mess);
int      iGeneral_time (float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var);
int      iLabor_data (int l_count, int *  lab_id , float *  lab_size, float *  lab_ot, float * lab_abs,
                   float *  lab_s_fact, float *  lab_r_fact, float *  lab_v_fact, int * balance);
int      iEquip_data (int eq_count, int *  eq_id , int *  eq_size, float *  mttf, float *  mttr, float *  eq_ot,
                   int *  lab_id, float *  eq_s_fact, float *  eq_r_fact, float *  eq_v_fact, int * cellid);
int      iParts_data (int p_count, int *  part_id , float *  demand, float *  lotsize, float * tbatch,
                   float *  part_d_fact, float *  part_v_fact, int * tgather);

int      iOper_data (int op_count, int *  op_name, int *  op_id , int *  part_id, int *  eq_id, float *  pct_assgn,
                   float *  eq_setup1, float *  eq_run1, float *  lab_setup1, float *  lab_run1,
                   float *  eq_setup2, float *  eq_run2, float *  lab_setup2, float *  lab_run2,
                   float *  eq_setup3, float *  eq_run3, float *  lab_setup3, float *  lab_run3);

int      iRoute_data (int r_count, int *  part_id3, int *  from_op_id, int *  to_op_id, float * pct_route);
int      iIbom_data (int ib_count, int *  prt_part_id , int *  comp_part_id, float *  upa);

int      pic2text (int rec_id);
int      text2pic (int rec_id);
    */

       

 //  int  iOptimization (int numberOfParts, int [] partid, float  []weight, int []optimizeLotSize, int []optimizeTbatch, float []lotsizeValue, float []tbatchValue  );


    //public const string APPL_NAME = "mpx-web"; - gerg - this property (global variable) is already in parent class CalcClass


   public ADODB.Connection globaldb = null; // GREG   //  need to setup after coming from page  --  complete next instances of all variables !!!- all variables need to be initialized before using them!!! - I initialized some of them to null 
   public ADODB.Connection globaldb_a = null;

   public bool adoOpened;

    public bool global_runalldone;
    public int  global_initwid;
    
    public string cookienum;
    public string varlocal;

    public int glngwid;
    public int model_modified;
    public int verify_needed;
    public int Gfamilywid;

    public int Part_err_id;
    public int Oper_err_id;
    public string Part_err_name;
    public string Oper_err_name;

   


    public int calc_return;     //0 - none, 1 labor, 2 eq over util, 4 warnings 8 errors 
    public string calc_msg; //  error_msg;    //  strng of ok. ....


  
    public const short CLEAR_FLAG = 0;
    public const short LAB_OVER_U = 1;
    public const short EQ_OVER_U = 2;
    public const short WARN_FLAG = 4;
    public const short ERR_FLAG = 8;
    public const short NO_OPT_FLAG = 16;
    public const short NO_EQUIP_FLAG = 32;
    public const short NO_PROD_FLAG = 64;
    public const short ERR_MSG_FLAG = 128;


    public const short YACC = 0;

    public const short Labor  = 0;
    public const short equip= 1;
    public const short product  = 2;
    public const short oper  = 3;
    public const short route  = 4;
    public const short ibom  = 5;
    public const short Whatif   = 6;
    public const short Eq_type  = 7;
    public const short General = 8;



    public const short YES_NO_TYPE  = 1;
    public const short BYTE_TYPE  = 2;
    public const short INTEGER_TYPE  = 3;
    public const short int_TYPE  = 4;
    public const short CURRENCY_TYPE  = 5;
    public const short SINGLE_TYPE  = 6;
    public const short DOUBLE_TYPE  = 7;
    public const short DATE_TIME_TYPE  = 8;
    public const short TEXT_TYPE  = 10;
    public const short MEMO_TYPE  = 12;
    public const short name_type  = 13;

   // public const short true  = -1;
   // public const short false  = 0;

    public const short INLABOR2  = 112;
    public const short INEQUIP2  = 122;
    public const short INprod2  = 132;
    public const short OUTLABOR1  = 212;
    public const short OUTEQUIP1  = 222;
    public const short OUTprod1  = 232;


    public string globTNameA;
    public string globTNameE;
    public string globFNameA;
    public string globFNameE;
    public int   globrecid;
    public string globNVal;
    public string globOVal;
    public int   globDType;


    public const short ERROR_FLAG = 0;




   
    public bool batch_run;
    public bool recalcall;

    public int  inOPT;
    public int calcerr;
    public bool INTERNATIONAL;
    public int fexit;

  
    public bool in_calculate;
    public bool recalc_needed;

    

    //Define Global Error Flag for DLL Calls;
    public int fDLLError;



    //Define Global Arrays for (Input Information;) {
    public float time1;
    public float time2;
    public float utlimit;
    public float cv_l;
    public float cv_e;
    public float cv_p;

    //Define Global Arrays for Labor Input;
    public int Lab_count = 0;
    public int[] Lab_id;
    public float[]  Lab_size;
    public float[] Lab_ot;
    public float[] lab_abs;
    public float[]  Lab_s_fact;
    public float[] Lab_r_fact;
    public float[] Lab_v_fact;
   
    public int[] Lab_share;

    //Define Global Arrays for Equipment Input;
    public int Eq_count = 0;
    public int[] Eq_id;
    public int[] eq_size;
    public int[] Lab_id2;
    public  float[] mttf;
    public  float[] MTTR;
    public  float[] eq_ot;
    public float[] Lab_ptr;
    public  float[] eq_s_fact;
    public  float[] eq_r_fact;
    public  float[] eq_v_fact;
    public int[] eq_cell;
    public int cell_count;

    //Define global arrays for production input;
    
    public int Prod_count = 0;
    public int[] Part_id;
    public float[] demand;
    public  float[] lotsize;
    public  float[] tbatch;
    public float[] part_d_fact;
    public  float[] part_v_fact;
    public int[] tbg;

    //define global arrays for operations input;
    public int Op_count = 0;
    public int[] OP_name;
    public int[] op_id;
    public int[] Part_id2;
    public int[] Eq_id2;
    public int[] Eq_ptr;
    public  float[] Pct_Assgn;
    public  float[] eq_setup;
    public  float[] eq_run;
     public float[] Lab_setup;
    public float[] Lab_run;

   
    public  float[] eq_SetupPiece;
    public float[] eq_SetupTbatch;
    public float[] eq_RunLot;
    public  float[] eq_RunTbatch;
    public  float[] lab_SetupPiece;
    public  float[] lab_SetupTbatch;
    public  float[] lab_RunLot;
    public  float[] lab_RunTbatch;

    //define global arrays for operation from - to;
    public int Route_count = 0;
    public int[] Part_r_ptr;
    public int[] Part_id3;
    public int[] from_op_id;
    public int[] to_op_id;
    public float[] Pct_route;

    //define global arrays for production IBOM input;
    public int Ibom_count = 0;
    public int[] Part_id4;
    public int[] Comp_part_id;
    public float[] UPA;

  public string[] qstrArry = new string[40];

    public int STOPMPX;


    
   public void  setGlobalVar() {
      
       ADODB.Recordset reccust = null;
       string cookieid;
       DateTime t1;

       //  also get cookieid from user
       //cookieid = GetCookieId();
       t1 = DateTime.UtcNow;

       // cross check cookie to database
       // cross check time in database 
       
       //  if errors => logout !!!  no return 


       //  remove from code ---  goes to cookie !!!
       //varlocal = "c:\\users\\websites\\webmpxnewa\\aa"; xxx();// Directory.GetCurrentDirectory();
       Directory.SetCurrentDirectory(varlocal);
       

       //  replace with correct stuff ....
      adoOpened = DbUse.open_ado(ref globaldb, varlocal + "\\mpxmdb.mdb");


        string connecString = "SELECT * FROM ";
       //  get gngwid;
        DbUse.open_ado_rec(globaldb, ref reccust, connecString + "zstblstate");
       glngwid = (int)  reccust.Fields["WID"].Value;
       model_modified = (int)reccust.Fields["model_modified"].Value;
       verify_needed = (int) reccust.Fields["verify_needed"].Value;
       DbUse.CloseAdoRec(reccust);
       reccust = null;
    }

    public void SetBasicModelInfo() {
        ADODB.Recordset reccust = null;

       string connecString = "SELECT * FROM ";
       //  get gngwid;
       DbUse.open_ado_rec(globaldb, ref reccust, connecString + "zstblstate");
       glngwid = (int)reccust.Fields["WID"].Value;
       model_modified = (int)reccust.Fields["model_modified"].Value;
       verify_needed = (int)reccust.Fields["verify_needed"].Value;
       DbUse.CloseAdoRec(reccust);
       reccust = null;
    }


    public string get_sort_command(string sortedTableName)
    {
        string strvalue = "";
        ADODB.Recordset rec1 = null;
        string gettab = "select ...";
        DbUse.open_ado_rec(globaldb, ref rec1, gettab);
        if (rec1.EOF != null) strvalue = ((string)rec1.Fields["sortsring"].Value);
        DbUse.CloseAdoRec(rec1);
        return strvalue;

    }


   private string[] whatifnames;
   private int whatifcnt;
   private int basecaserecalc;

  private  void getwhatifrecalc(){ 
        
        ADODB.Recordset reccust = null;
        string str1;
        int i;

       whatifcnt =0;
       basecaserecalc = 0;

       str1 = " select zs0tblWhatIf.* from zs0tblWhatIf WHERE (((zs0tblWhatIf.WID)=0));";
       DbUse.open_ado_rec(globaldb, ref reccust, str1);
       basecaserecalc = Convert.ToInt32( reccust.Fields["recalc"].Value);

       str1 = " select tblWhatIf.* from tblWhatIf WHERE (tblWhatIf.recalc <> 0);";
       DbUse.open_ado_rec(globaldb, ref reccust, str1);
       while (!reccust.EOF)
       {   whatifcnt +=1;
           reccust.MoveNext();
       }
       if (whatifcnt > 0)
       {
           reccust.MoveFirst();
           i = 0;
           whatifnames = new string[whatifcnt];
           while (!reccust.EOF)
           {
               whatifnames[i] = Convert.ToString(reccust.Fields["name"].Value);

               reccust.MoveNext();
               i += 1;
           }
       }
      

    return; 

    }

   public string recalcmsg()
   {
       string str1;
       int i;

       getwhatifrecalc();
       
       if ((basecaserecalc == 0) & (whatifcnt == 0))
       {
           str1 = "No Recalc is needed.";
           return str1;
       }

       str1 = "Recalc needed for ";
       if ( basecaserecalc !=0) {
           str1 += "Base Case ";
           if (whatifcnt == 0)
           {
               return str1;
           }
           if (whatifcnt == 1)
           {
               str1 += "and What-If Scenario: " + whatifnames[0];
               return str1;
           }
           else if (whatifcnt > 1) str1 += "and for What-If Scenarios: ";
       }
       else if (whatifcnt == 1)
       {
           str1 += "What-If: " + whatifnames[0];
           return str1;
       }
       else if (whatifcnt > 1) str1 += "What-Ifs: "; 
           
          

        for(i=0;i<(whatifcnt-2); i++) { 
            str1 += whatifnames[i] + ", ";
        }
      
            str1 += whatifnames[whatifcnt-2];
            str1 += " and " + whatifnames[whatifcnt-1];
        
       

       return str1;
   }

   

   public void xxx() {    //xxx();
   }

   
  
   public void saveGlobals()
   {
       saveWid();
       saveModel_modified();
       saveVerify_needed();
   }
   public void saveWid()
   {
      string str1;
      //Close();
       str1 = " Update zstblstate set wid = " + glngwid +";";
       // save  gngwid;

        runsqlado(str1);
        //Open();
      

   }

   public int readRecalcNeeded(int wid)
   {
       string str1;
       int val;
       ADODB.Recordset reccust = null;



       if (wid == 0)
       {
          str1 = " select zs0tblWhatIf.* from zs0tblWhatIf WHERE (((zs0tblWhatIf.WID)=" + wid + "));";
       }
       else
       {
           str1 = " select tblWhatIf.* from tblWhatIf WHERE (((tblWhatIf.WID)=" + wid + "));";
       };

       DbUse.open_ado_rec( globaldb, ref reccust, str1);

       val =  ((bool) reccust.Fields["recalc"].Value ? -1: 0);

       return val;

   }
    public void saveRecalcNeeded( int wid, int Val) { 
        string str1;
       
        if (wid == 0) {
           if (Val == 0)
            {
                str1 = " UPDATE zs0tblWhatIf SET zs0tblWhatIf.recalc = 0  WHERE (((zs0tblWhatIf.WID)=" + wid + "));";
            }
            else
            {
                runsqlado("UPDATE tblWhatIf SET tblWhatIf.recalc = -1");
                str1 = " UPDATE zs0tblWhatIf SET zs0tblWhatIf.recalc = -1  WHERE (((zs0tblWhatIf.WID)=" + wid + "));";
            }
        } else { 
            if (Val == 0) 
                { str1 = " UPDATE tblWhatIf SET tblWhatIf.recalc = 0  WHERE (((tblWhatIf.WID)=" + wid +"));"; }
            else  str1 = " UPDATE tblWhatIf SET tblWhatIf.recalc = -1  WHERE (((tblWhatIf.WID)=" + wid +"));";
        }

        runsqlado(str1);
        return;

    }
   public void saveModel_modified( )
   {
       string str1;
       str1 = " Update zstblstate set model_modified = " + model_modified + ";";
       // save  model-modified;

         runsqlado(str1);
       
   }


   public void saveVerify_needed()
   {
       string str1;

       str1 = " Update zstblstate set verify_needed = " + verify_needed + ";";
       // save  model-modified;
       runsqlado(str1);


   }

                      


   public string dig_round(double x, int num)
   {
       string retval = "0";
       int size;
       int size2;
       int r1;
       int r2;
       double x1;
       double x2;


       x1 = Math.Abs(x);
       if ( x1 == 0)  return "0";

       size = (int)(0.999 + (float)(Math.Log(x1) / Math.Log(10.0F)));
       size2 = num - size;
       r1 = (int)(0.5111 + x1 * System.Math.Exp(System.Math.Log(10) * size2));
       x2 = r1 / System.Math.Exp(System.Math.Log(10) * size2);
       if (x < 0) x2 *= -1;
       retval = Convert.ToString(x2);
       //retval = round_out2(retval, num);
       retval = x2.ToString("G" + num); 
       return retval;
   } 

  public  short checkDOCKSTOCK()   {
        //on error goto err_check;
         short check;


          ADODB.Recordset rec1 = null;
          ADODB.Recordset rec2 = null;
         int prodid;
         int xprod;
         string sqlstr1;
         int eqid_none;
         int op1;

        eqid_none =  find_nameItem("none", 0, equip, 0);


        sqlstr1 = "SELECT PRODID   From tblPRODFORE;";
        DbUse.open_ado_rec(globaldb, ref rec2, sqlstr1);

       
        while (!rec2.EOF) {

            xprod = (int) rec2.Fields["prodid"].Value;
            sqlstr1 = "SELECT tblOper.*  From tblOper where prodfore = "+ Convert.ToString(xprod) + "  ORDER BY tblOper.ProdFore, tblOper.OpID;";
            if (rec1 != null)
            {
                DbUse.CloseAdoRec(rec1);
                rec1 = null;
            };
            DbUse.open_ado_rec(globaldb, ref rec1, sqlstr1);
            op1 = Convert.ToInt32(rec1.Fields["opid"].Value);

                //   dock   

                rec1.Fields["opnum"].Value = 0;
                rec1.Fields["opnam"].Value = "DOCK";
                rec1.Fields["ProdFore"].Value = xprod;
                rec1.Fields["Percentassign"].Value = "100";
                rec1.Fields["EqID"].Value = eqid_none;


                rec1.Fields["EqSetupTime"].Value = "0";
                rec1.Fields["EqRunTime"].Value = "0";
                rec1.Fields["eqSetupTbatch"].Value = "0";
                rec1.Fields["eqRunTbatch"].Value = "0";
                rec1.Fields["eqSetupPiece"].Value = "0";
                rec1.Fields["eqrunlot"].Value = "0";

                rec1.Fields["LabSetupTime"].Value = "0";
                rec1.Fields["LabRunTime"].Value = "0";
                rec1.Fields["labSetupTbatch"].Value = "0";
                rec1.Fields["labRunTbatch"].Value = "0";
                rec1.Fields["labSetupPiece"].Value = "0";
                rec1.Fields["labrunlot"].Value = "0";


                rec1.Update();

                rec1.MoveNext();
                // if ((string)rec1.Fields["opnam"].Value != "STOCK")
                rec1.Fields["opnum"].Value = 9999;
                rec1.Fields["opnam"].Value = "STOCK";
                rec1.Fields["ProdFore"].Value = xprod;
                rec1.Fields["Percentassign"].Value = "100";
                rec1.Fields["EqID"].Value = eqid_none;


                rec1.Fields["EqSetupTime"].Value = "0";
                rec1.Fields["EqRunTime"].Value = "0";
                rec1.Fields["eqSetupTbatch"].Value = "0";
                rec1.Fields["eqRunTbatch"].Value = "0";
                rec1.Fields["eqSetupPiece"].Value = "0";
                rec1.Fields["eqrunlot"].Value = "0";

                rec1.Fields["LabSetupTime"].Value = "0";
                rec1.Fields["LabRunTime"].Value = "0";
                rec1.Fields["labSetupTbatch"].Value = "0";
                rec1.Fields["labRunTbatch"].Value = "0";
                rec1.Fields["labSetupPiece"].Value = "0";
                rec1.Fields["labrunlot"].Value = "0";


                rec1.Update();

                rec1.MoveNext();
                // "SCRAP")
                rec1.Fields["opnum"].Value = 10000;
                rec1.Fields["opnam"].Value = "SCRAP";
                rec1.Fields["ProdFore"].Value = xprod;
                rec1.Fields["Percentassign"].Value = "100";
                rec1.Fields["EqID"].Value = eqid_none;


                rec1.Fields["EqSetupTime"].Value = "0";
                rec1.Fields["EqRunTime"].Value = "0";
                rec1.Fields["eqSetupTbatch"].Value = "0";
                rec1.Fields["eqRunTbatch"].Value = "0";
                rec1.Fields["eqSetupPiece"].Value = "0";
                rec1.Fields["eqrunlot"].Value = "0";

                rec1.Fields["LabSetupTime"].Value = "0";
                rec1.Fields["LabRunTime"].Value = "0";
                rec1.Fields["labSetupTbatch"].Value = "0";
                rec1.Fields["labRunTbatch"].Value = "0";
                rec1.Fields["labSetupPiece"].Value = "0";
                rec1.Fields["labrunlot"].Value = "0";

                rec1.Update();

                op1 = Convert.ToInt32(rec1.Fields["opid"].Value);

               fixopers(xprod, "DOCK",  op1);
               fixopers(xprod, "STOCK", op1);
               fixopers(xprod, "SCRAP", op1);
         
           

          
            rec2.MoveNext();
        };

        if (null != rec1)   {
            DbUse.CloseAdoRec(rec1);
            rec1 =  null;
            };
        ;
       if (null != rec2)   {
            DbUse.CloseAdoRec(rec2);
            rec2 =  null;
            };
        ;
        


     //  breaking rule of dock, stock, scrap as123  ?  knowing when user is f**ing with the data ???


      //  

//exit_check:
         return -1; //exit  Function;

    } // end function;

  public int testfind(int prodid, string opnam, int opid)
  {
      ADODB.Recordset rec1 = null;
      string sqlstr1;
      int ret1;


      sqlstr1 = "SELECT TBLOPER.* from tbloper where prodfore = " + Convert.ToString(prodid) + " and tbloper.opnam = '" + Convert.ToString(opnam) + "' and tbloper.opid > " + opid + " ;";
      DbUse.open_ado_rec(globaldb, ref rec1, sqlstr1);
      if (rec1.EOF == true)
      {
          DbUse.CloseAdoRec(rec1);
          return -1;
      }
      else
      {
          if (null != rec1)
          {
              DbUse.CloseAdoRec(rec1);
          };

          return 0;

      }
  }

    public void fixopers(int prodid, string opnam, int opid){
        ADODB.Recordset rec1 = null;
        string sqlstr1;
        int count;
        string opnam2;
        int ret1;


        sqlstr1 = "SELECT TBLOPER.* from tbloper where prodfore = " + Convert.ToString(prodid) + " and tbloper.opnam = '" + Convert.ToString(opnam) + "' and tbloper.opid > " + opid + " ;";
        DbUse.open_ado_rec(globaldb, ref rec1, sqlstr1);

       
        count = 1;
        while (!rec1.EOF)
        {
            opnam2 = opnam + "-" + count;
            ret1 = testfind ( prodid, opnam2, opid);
            while (ret1 == 0) {
                count += 1;
                opnam2 = opnam + "-" + count;
                ret1 = testfind ( prodid,  opnam2, opid);
            }
            rec1.Fields["opnam"].Value = opnam2;
            rec1.Update();

            rec1.MoveNext();
        }


        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };
        return;
    }


    public  void xxxadd_initial( string pstrtable, short Index) {
        xxx(); //  not used !!!//  check where used !!
        //on error goto err_add_init;

            int prodid;
         short  Count;
         ADODB.Recordset reccust = null;
             string strsql;

        //Set variables and open table;
             Count = 0;

        DbUse.open_ado_rec(globaldb, ref reccust, pstrtable); // pstrtable is short - must be string!!

             if (pstrtable =="tblLabor") {
                strsql = "((LaborDesc) <> '" +  "NONE"  + "')";  //  needs   ((labordesc) !='none')   in the string  34 = " but '  works as well
                reccust.Filter = strsql;
                if ((reccust.EOF))   {
                    Count = 0;
                } else {
                    Count = 1;
                };
            }

             if (pstrtable =="tblEquip") {
                if ((Index  == 0))   {
                    strsql = "(((EquipDesc) <> '" + "NONE" + "'))";
                } else {
                    strsql = "(((EquipDesc) <> '" + "NONE" +  "'))";
                };
                reccust.Filter = strsql;
                if ((reccust.EOF))   {
                    Count = 0;
                } else {
                    Count = 1;
                };
            } 
            
            if (pstrtable =="tblProdFore") {
                if (reccust.EOF)   {
                    Count = 0;
                } else {
                    Count = 1;
                };
            }


            if (Count == 0)
            {

                if (pstrtable == "tblLabor")
                {
                    reccust.AddNew();
                    reccust.Fields["labordesc"].Value = "Initial Labor";
                    reccust.Fields["grpsiz"].Value = 1;
                    reccust.Update();
                    // msgbox("No Labor defined.  Adding 1 group", 0, appl_name);
                }
                if (pstrtable == "tblEquip")
                {
                    if ((Index == 0))
                    {
                        //' std equip;
                        reccust.AddNew();
                        reccust.Fields["equipdesc"].Value = "Initial Equipment";
                        reccust.Fields["grpsiz"].Value = 1;
                        reccust.Fields["Labor"].Value = find_nameItem("none", 0, Labor, 0);
                        reccust.Update();
                    };
                    // msgbox("No Equipment Defined.  Adding 1 group", 0, appl_name);
                }
                if (pstrtable == "tblProdFore")
                {
                    reccust.AddNew();
                    reccust.Fields["proddesc"].Value = "Initial Product";
                    prodid = (int)reccust.Fields["prodid"].Value;
                    reccust.Update();
                    addoper_1(prodid);
                    // msgbox("No Products defined.  Adding 1 Product", 0, appl_name);
                }

            }; // end if 
            

        if ( null != reccust)   {
            DbUse.CloseAdoRec(reccust);
            reccust =  null;
            };

//exit_add_init:
         return; //exit  Sub;
//err_add_init:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_add_init;

    } // end sub;

    public  void addoper_1( int prodid) {
        //on error goto Err_AddOper_1;

        //Define variables;
            ADODB.Recordset reccust = null;
            ADODB.Recordset recRoute = null;
             string strCriteria;
             string strtable;
            int dock;
            int stock;
            int Scrap;
            int scrapall;
            int none_id;
            int dockid;

        //Set variables and open table;
       DbUse.open_ado_rec(globaldb, ref reccust, "tblOper");
       DbUse.open_ado_rec(globaldb, ref recRoute, "tbloperFrTo");

        dockid = find_nameItem("DOCK", prodid, oper, 0);
        if ((dockid != 0))   {
             return; //exit  Sub;
            };

        none_id = find_nameItem("NONE", 0, equip, 0);

        //Add two new operation for new Process;
        //Operation 1 - DOCK Operation 2 - STOCK  3 - Scrap 4 -whole lot scrap;
        reccust.AddNew();
        reccust.Fields["opnum"].Value = 0;
        reccust.Fields["opnam"].Value = "DOCK";
        reccust.Fields["ProdFore"].Value = prodid;
        reccust.Fields["EqID"].Value = none_id;
        reccust.Fields["Percentassign"].Value = "100";
        dock = (int) reccust.Fields["opid"].Value;
        reccust.Update();

        // STOCK;
        reccust.AddNew();
        reccust.Fields["ProdFore"].Value = prodid;
        reccust.Fields["opnum"].Value = 9999;
        reccust.Fields["opnam"].Value = "STOCK";
        reccust.Fields["EqID"].Value = none_id;
        reccust.Fields["Percentassign"].Value = "100";
        stock = (int)  reccust.Fields["opid"].Value;
        reccust.Update();

        // SCRAP;
        reccust.AddNew();
        reccust.Fields["ProdFore"].Value = prodid;
        reccust.Fields["opnum"].Value = 10000;
        //recCust.Fields["OpNam"].Value = "Partial Lot Scrap";
        reccust.Fields["opnam"].Value = "SCRAP";
        reccust.Fields["EqID"].Value = none_id;
        reccust.Fields["Percentassign"].Value = "100";
        Scrap = (int) reccust.Fields["opid"].Value;
        reccust.Update();

        //ROUTING  SCRAP entire lot;
        recRoute.AddNew();
        recRoute.Fields["PartFore"].Value = prodid;
        recRoute.Fields["OpNumF"].Value = dock;
        recRoute.Fields["OpNumT"].Value = stock;
        recRoute.Fields["per"].Value = 100.0;
        recRoute.Update();

        if (null !=reccust )   {
            DbUse.CloseAdoRec(reccust);
            reccust =  null;
            };
        if ( null != recRoute)   {
            DbUse.CloseAdoRec(recRoute);
            recRoute =  null;
            };

// return; //exit _AddOper_1:
         return; //exit  Sub;

//Err_AddOper_1:
        // msgbox(ErrorToString(), 0, "Appl_name");
         //RESUME NEXT
    } // end sub;

    public  void CopyEquip(string oldname, string pNewName) {
        //on error goto Err_CopyEquip;

        //Define variables;
         ADODB.Recordset recEquip = null;
         ADODB.Recordset recEquipID = null;

         int  intGrpSiz;
            float sngMTF;
            float sngMTR;
            float sngOT;
            int intLabor;
            float sngsetup;
            float sngrun;
            float sngvarbility;
             string sE1;
             string sE2;
             string sE3;
             string sE4;
             string fltdept;
             string fltcomment;
                   
         short  intStand;

             string strmsg;

        
        //Set Message box error message;
             strmsg = "There is invalid data in the Equipment file." + "<br/>" + "<br/>" + "Some information may not be copied";

        //Open tables;
        DbUse.open_ado_rec(globaldb, ref recEquip, "tblequip");

        //Find first match of search criteria;
        recEquip.Filter = "[equipdesc] = '" + pNewName + "'";

          while (!(recEquip.EOF)) {
            // msgbox("Equipment group " + pNewName + " already exists.  A copy with the name 'Another_" + pNewName + "' will be created.", 0, appl_name);
            pNewName = "Another_" + pNewName;
            recEquip.Filter = "[equipdesc] = '" + pNewName + "'";
        }; // end while


        //Begin search for match;
        recEquip.Filter = "[equipdesc] ='" + oldname + "'";

        //Test for match and then loop through all matches;
        if ((!recEquip.EOF))   {


            intGrpSiz = (short)  recEquip.Fields["grpsiz"].Value;

            sngMTF = ((float)recEquip.Fields["MTF"].Value);
            sngMTR = ((float)recEquip.Fields["MTR"].Value);
            sngOT = ((float)recEquip.Fields["OT"].Value);
            intLabor = ((int)recEquip.Fields["Labor"].Value);
            sngsetup = ((float)recEquip.Fields["SETUP"].Value);
            sngrun = ((float)recEquip.Fields["RUN"].Value);
            sngvarbility = str2f( (string) recEquip.Fields["Varbility"].Value);

            sE1 = (string)  recEquip.Fields["e1"].Value;
            sE2 = (string) recEquip.Fields["e2"].Value;
            sE3 = (string) recEquip.Fields["e3"].Value;
            sE4 = (string) recEquip.Fields["e4"].Value;
             if(null==recEquip.Fields["EqComment"].Value)  { 
                fltcomment =  " "; }
            else fltcomment = (string) recEquip.Fields["EqComment"].Value;

             if(null==recEquip.Fields["EquipDept"].Value) { 
                 fltdept =     " ";}
            else fltdept =  (string) recEquip.Fields["EquipDept"].Value;



             
             //check for duplicate name 
            bool done;
            string str1;
            done = false;
            while (!done)
            {
                str1 = "select tblequip.* from tblequip where name = '" + pNewName + "';";
                DbUse.open_ado_rec(globaldb, ref recEquip, str1);

                if (!recEquip.EOF)
                {
                    pNewName = "A_" + pNewName;
                }
                else { done = true; };

            }  // end while

            //Add new record to table and paste back all fields;
            recEquip.AddNew();
            recEquip.Fields["equipdesc"].Value = pNewName;  //'' left(pNewName), 19);
            recEquip.Fields["grpsiz"].Value = intGrpSiz;
            recEquip.Fields["MTF"].Value = sngMTF;
            recEquip.Fields["MTR"].Value = sngMTR;
            recEquip.Fields["OT"].Value = sngOT;
            recEquip.Fields["Labor"].Value = intLabor;
            recEquip.Fields["SETUP"].Value = sngsetup;
            recEquip.Fields["RUN"].Value = sngrun;
            recEquip.Fields["Varbility"].Value = sngvarbility;
            recEquip.Fields["e1"].Value = sE1;
            recEquip.Fields["e2"].Value = sE2;
            recEquip.Fields["e3"].Value = sE3;
            recEquip.Fields["e4"].Value = sE4;
            recEquip.Fields["EqComment"].Value = fltcomment;
            recEquip.Fields["EquipDept"].Value = fltdept;

            recEquip.Update();

            //Find next match on Complex ID;
        } else {
            DbUse.CloseAdoRec(recEquip);
            recEquip = null;
            // msgbox("Internal error : cannot find equipment " + oldname + ". Aborting copy ", 0, appl_name);
            //tomorrow  ERROR MSG   at copy 
             return; //exit  Sub;
            };

        DbUse.CloseAdoRec(recEquip);
        recEquip =  null;

        if ( null != recEquip)    {
            DbUse.CloseAdoRec(recEquip);
            recEquip =  null;
            };
        if (null != recEquipID)     {
            DbUse.CloseAdoRec(recEquipID);
            recEquipID =  null;
            };


// return; //exit _CopyEquip:
         return; //exit  Sub;

//Err_CopyEquip:
        // msgbox(strmsg, 48, "Copy Equipment Data");
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT
    } // end sub;

    public  void CopyIbom(int lngOldID, int lngNewID) {
        //on error goto Err_CopyIbom;

        //Define variables;
             string strCriteria;
        strCriteria = "INSERT INTO tblibom ( CompID, UPA, ParentID ) SELECT tblibom.CompID, tblibom.UPA, " + lngNewID + " AS Expr1 From tblibom WHERE (((tblibom.ParentID)=" + lngOldID + "));";
        runsqlado(strCriteria);


// return; //exit _CopyIbom:
         return; //exit  Sub;

//Err_CopyIbom:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _CopyIbom;
    } // end sub;

    public  void CopyLabor(string oldname, string pstrnewname) {
        //on error goto Err_CopyLabor;

        //Define variables;
        ADODB.Recordset reccust = null;
             int lngLaborID;
             string strLaborDesc;
            float grpsiz;
            float fltOT;
            float fltAbst;
            float fltsetup;
            float fltrun;
            float fltvarbility;
            bool  fltprioritize;

             string fltL1;
             string fltL2;
             string fltL3;
             string fltL4;
             string fltdept;
             string fltcomment;

        //Open Labor table and add new record;
        DbUse.open_ado_rec(globaldb, ref reccust, "tbllabor"); 
        //Find first match of search criteria;
        reccust.Filter = "[labordesc] = '" + pstrnewname + "'";

          while ((!reccust.EOF)) {
            // msgbox("Labor group  " + pstrnewname + " already exists.  A copy with the name 'Another_" + pstrnewname + "' will be created.", 0, appl_name);
            pstrnewname = "Another_" + pstrnewname;
            reccust.Filter = "[labordesc] = '" + pstrnewname + "'";
        }; // end while

        reccust.Filter = "[labordesc] ='" + oldname + "'";
        if (!reccust.EOF)   {

            //Get value of all fields in record to be copied;
            strLaborDesc = (string) reccust.Fields["labordesc"].Value;
            grpsiz = ((float)  reccust.Fields["grpsiz"].Value);
            fltOT = ((float)reccust.Fields["OT"].Value);
            fltAbst =str2f( (string) reccust.Fields["Abst"].Value);
            fltsetup =str2f( (string) reccust.Fields["SETUP"].Value);
            fltrun =str2f( (string) reccust.Fields["RUN"].Value);
            fltvarbility =((float)  reccust.Fields["Varbility"].Value);
            fltprioritize =((bool) reccust.Fields["PriorityShare"].Value);
            fltL1 = (string) reccust.Fields["l1"].Value;
            fltL2 = (string)  reccust.Fields["l2"].Value;
            fltL3 = (string)  reccust.Fields["l3"].Value;
            fltL4 = (string)  reccust.Fields["l4"].Value;
            if(null== reccust.Fields["LabComment"].Value) {  fltcomment = " ";} else  fltcomment = (string) reccust.Fields["LabComment"].Value;
            if(null== reccust.Fields["labordept"].Value) { fltdept= " ";} else fltdept = (string) reccust.Fields["labordept"].Value;


            //check for duplicate name 
             bool done;
            string str1;
            done = false;
            while (!done)
            {
                str1 = "select tbllabor.* from tbllabor where name = '" + pstrnewname + "';";
                DbUse.open_ado_rec(globaldb, ref reccust, str1);

                if (!reccust.EOF)
                {
                    pstrnewname = "A_" + pstrnewname;
                }
                else { done = true; };

            }  // end while
            //Add new record;
             reccust.AddNew();
            lngLaborID = str2i((string)  reccust.Fields["LaborID"].Value);  //  not used!!!
            reccust.Fields["labordesc"].Value = pstrnewname;
            reccust.Fields["grpsiz"].Value = grpsiz;
            reccust.Fields["OT"].Value = fltOT;
            reccust.Fields["Abst"].Value = fltAbst;
            reccust.Fields["SETUP"].Value = fltsetup;
            reccust.Fields["RUN"].Value = fltrun;
            reccust.Fields["Varbility"].Value = fltvarbility;
            reccust.Fields["PriorityShare"].Value = (bool)fltprioritize;
            reccust.Fields["l1"].Value = fltL1;
            reccust.Fields["l2"].Value = fltL2;
            reccust.Fields["l3"].Value = fltL3;
            reccust.Fields["l4"].Value = fltL4;
            reccust.Fields["labordept"].Value = fltdept;
            reccust.Fields["LabComment"].Value = fltcomment;
            reccust.Update();
        } else {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
            // msgbox("Internal error : cannot find labor group " + oldname + ". Aborting copy ", 0, appl_name);
            //tomorrow  ERROR MSG   at copy 
             return; //exit  Sub;
            };

        if (null != reccust)   {
            DbUse.CloseAdoRec(reccust);
            reccust =  null;
            };

// return; //exit _CopyLabor:
         return; //exit  Sub;

//Err_CopyLabor:
        // msgbox(ErrorToString());
         //RESUME NEXT
    } // end sub;

    // GREG - way to handle errors is to use try/catch block - see below


    public string strleft(string str1, int l1)
    {
        int len;
        if (l1 == 0) { return "";};
        if (l1 == str1.Length) { return str1; };
        len = str1.Length;
        l1 = Math.Min(l1, len);
        return str1.Substring(0, l1);

    } // end function;

    public string strright(string str1, int l1)
    {
        int l2;
        if (l1 == 0) { return ""; };
        if (l1 == str1.Length) { return  str1; };
        l2 = Math.Max(0, str1.Length - l1);
        l1 = Math.Min(str1.Length, l1);

        return str1.Substring(l2, l1);

    } // end function;

    public bool IsNumeric(string str1)
    {
       
        int i;
        int j;
        char j1;
        string str2;

        str1 = str1.Trim();
        str2 = "";

        if ((str1[0] == Convert.ToChar(43)) | (str1[0] == Convert.ToChar(45)))   //43 +-
        {
            str1 = strright(str1, str1.Length - 1);
        }
        i = str1.IndexOf(Convert.ToChar(46));  //  split at .
        if (i >= 0)
        {
            str2 = strright(str1, str1.Length - i -1); 
            str1 = strleft(str1, i);
              
            i = str2.IndexOf(Convert.ToChar(46));  // find another .
            if (i >= 0) return false;
        }

        for (i=0; i<str1.Length; i++) {
            for (j=48; j<= 57; j++) { 
               j1 = Convert.ToChar(j);  
               if (str1[i] == j1) goto nexti;
            }
            //error no jump no match 
            return false;
        nexti:  ; 
        }

        for (i = 0; i < str2.Length; i++)
        {
            for (j = 48; j<=57; j++)
            {
                j1 = Convert.ToChar(j);
                if (str2[i] == j1) goto nexti2;
            }
            //error no jump no match 
            return false;
        nexti2: ;
        }
        return true;
        
      /*  try {
            x1 = Convert.ToDouble(str1);
            isNum = true;
          // if error return false;
          //  else return true;
        } catch (Exception) {
            isNum = false;
        }
        return isNum;
        */

    }

    public int Calc_count()
    {
        int ret = -1;

        ADODB.Recordset reccust = null;


        DbUse.open_ado_rec(globaldb, ref reccust, "select tblprodfore.* from tblprodfore");
        if (reccust.EOF)
        {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
            return 0;
        }


        return ret;
    }
    public int str2i(string str1)
    {
        int retval;
        retval = 0;

        str1 = str1.Trim();
        if (str1.Length == 0) { return (0); };

        if (IsNumeric(str1) == false)
        {
            return (0);
        }

        retval = Convert.ToInt32(str1);

        return retval;
    }

   

    public float str2f(string str1)
    {
        float retval;
        retval = 0;

        if (IsNumeric(str1) == false)
        {
            return (0);
        }
        retval = (float)Convert.ToDouble(str1);

        return retval;
    }


    public int get_count(string recs, int localwid)
    {
        //on error GoTo err_get_count;

        ADODB.Recordset reccust = null;
        string strcrit;
        int i;
        int retval;
        int count;

        retval = 0;

        if ((localwid >= 0))  //   use for input  always localwid = -1 !!!
        {
            strcrit = "SELECT Count(" + recs + ".WID) AS Count1, " + recs + ".WID FROM " + recs + " GROUP BY " + recs + ".WID HAVING ((" + recs + ".WID=" + localwid + "));";
            count = -1;
        }
        else
        {
            strcrit = recs;
            count = 0;
        };

        DbUse.open_ado_rec(globaldb, ref reccust, strcrit);


        if ((reccust.EOF))
        {
            retval = 0;
            DbUse.CloseAdoRec(reccust);
            reccust = null;
            return retval;
        }
        else if (count == -1) {
        
            reccust.MoveFirst();
            retval = (int)reccust.Fields["count1"].Value;
            DbUse.CloseAdoRec(reccust);
            reccust = null;
            return retval;
        };

        
        reccust.MoveFirst();
            i = 0;
            while (!reccust.EOF)
            {
                i = i + 1;
                reccust.MoveNext();
            };
        retval = i;
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;

        //err_get_count:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_get_count;

    } // end function;


   public  double str2d(string str1)
    {
        double retval;
        retval = 0;

        retval = Convert.ToDouble(str1);

        return retval;
    }

    public  void CopyOper(int plngProdOld, int plngProdNew) {
        CopyOper(plngProdOld, plngProdNew, false);
    }

    public  void CopyOper(int plngProdOld, int plngProdNew, Boolean GI  ) {
        //on error goto Err_CopyOper;

        //Define variables;
             ADODB.Recordset recoper = null;
             ADODB.Recordset recNxtOp = null;
             string strCriteria;
             string strCriteria2;
        
             int [] lngOldID = null;
           
             int [] lngNewID = null;
        int arraysize;

         int  X;
         short  i;
         short  j;

         string str1;

             string strOpNam;
         short  intOpNum;
             string strpctAssgn;
            int EqID;

             string strEqsetupTime;
             string strEqrunTime;
             string strEqsetupPiece;
             string strEqrunLot;
             string strEqsetupTBatch;
             string strEqrunTBatch;

             string strLabsetupTime;
             string strlabrunTime;
             string strLabsetupPiece;
             string strlabrunLot;
             string strLabsetupTBatch;
             string strlabrunTBatch;

             string fltO1;
             string fltO2;
             string fltO3;
             string fltO4;

            int lngOpNumT;
            int lngProdFore;
            int oldrecid;
            int newrecid;
             string fltPer;

             string strmsg;

        //Set Message box error message;
             strmsg = "There is invalid data in the Operations file." + "<br/>" + "Some information may not be copied"; // What does numbers in () represent??
        if (GI) {
            runsqlado("DELETE * FROM picbackoper;");
            runsqlado("DELETE * FROM picbackprodfore;");
        }
        //Default variables;
        X = 1;
        i = 1;
        j = 1;

        //tbd GD add code to get arraysize;  
        str1 = "select tblOper.* from tbloper where ( tbloper.prodfore = "+ plngProdOld + ");";
        arraysize = 5 + get_count(str1, -1);
        //Open tables;
       DbUse.open_ado_rec(globaldb, ref recoper, str1 );
       DbUse.open_ado_rec(globaldb, ref recNxtOp, "tblOperFrTo");

        //Create search ctriteria;
        strCriteria = "[ProdFore] = " + plngProdOld;

        //Find first occurrance of search criteria;
        recoper.Filter = strCriteria;


       

         Array.Resize(ref lngOldID, (int) arraysize);
         Array.Resize(ref lngNewID, (int) arraysize);  

        //Test for Match and then copy and update all associated records;
        while (!recoper.EOF) {
            //Copy all fields in operation record;
           
            lngOldID[X] = (int)  recoper.Fields["opid"].Value;
            strOpNam = (string) recoper.Fields["opnam"].Value;
            intOpNum = short.Parse(recoper.Fields["opnum"].Value.ToString());
            strpctAssgn = (string) recoper.Fields["Percentassign"].Value;
            EqID =(int) recoper.Fields["EqID"].Value;

            strEqsetupTime = (string) recoper.Fields["EqSetupTime"].Value;
            strEqrunTime = (string) recoper.Fields["EqRunTime"].Value;
            strEqsetupTBatch = (string)recoper.Fields["eqSetupTbatch"].Value;
            strEqrunTBatch = (string) recoper.Fields["eqRunTbatch"].Value;
            strEqsetupPiece = (string) recoper.Fields["eqSetupPiece"].Value;
            strEqrunLot = (string)recoper.Fields["eqrunlot"].Value;

            strLabsetupTime = (string)recoper.Fields["LabSetupTime"].Value;
            strlabrunTime =(string) recoper.Fields["LabRunTime"].Value;
            strLabsetupTBatch = (string)recoper.Fields["labSetupTbatch"].Value;
            strlabrunTBatch =(string) recoper.Fields["labRunTbatch"].Value;
            strLabsetupPiece = (string)recoper.Fields["labSetupPiece"].Value;
            strlabrunLot =(string) recoper.Fields["labrunlot"].Value;

           if (Convert.IsDBNull(recoper.Fields["o1"].Value)) fltO1 = " "; else fltO1 = (string) recoper.Fields["o1"].Value;
           if (Convert.IsDBNull(recoper.Fields["o2"].Value)) fltO2 = " "; else fltO2 = (string) recoper.Fields["o2"].Value;
           if (Convert.IsDBNull(recoper.Fields["o3"].Value)) fltO3 = " "; else fltO3 = (string) recoper.Fields["o3"].Value;
           if (Convert.IsDBNull(recoper.Fields["o4"].Value)) fltO4 = " "; else fltO4 = (string)recoper.Fields["o4"].Value;

                    
            
            //Add new record to operation table;
            
            recoper.AddNew();
            //Paste all field into new record and update FK fields;
            recoper.Fields["opnam"].Value = strOpNam;
            recoper.Fields["opnum"].Value = intOpNum;
            recoper.Fields["Percentassign"].Value = strpctAssgn;
            recoper.Fields["EqID"].Value = EqID;
            recoper.Fields["EqSetupTime"].Value = strEqsetupTime;
            recoper.Fields["EqRunTime"].Value = strEqrunTime;
            recoper.Fields["eqSetupTbatch"].Value = strEqsetupTBatch;
            recoper.Fields["eqRunTbatch"].Value = strEqrunTBatch;
            recoper.Fields["eqSetupPiece"].Value = strEqsetupPiece;
            recoper.Fields["eqrunlot"].Value = strEqrunLot;

            recoper.Fields["LabSetupTime"].Value = strLabsetupTime;
            recoper.Fields["LabRunTime"].Value = strlabrunTime;
            recoper.Fields["labSetupTbatch"].Value = strLabsetupTBatch;
            recoper.Fields["labRunTbatch"].Value = strlabrunTBatch;
            recoper.Fields["labSetupPiece"].Value = strLabsetupPiece;
            recoper.Fields["labrunlot"].Value = strlabrunLot;

            recoper.Fields["o1"].Value = fltO1;
            recoper.Fields["o2"].Value = fltO2;
            recoper.Fields["o3"].Value = fltO3;
            recoper.Fields["o4"].Value = fltO4;
            recoper.Fields["ProdFore"].Value = plngProdNew;
            lngNewID[X] = (int) recoper.Fields["opid"].Value;


            recoper.Update();

            if (GI)
            {
                runsql("INSERT INTO picbackoper ( OpID, Flag ) VALUES (" + lngNewID[X] + ", " + lngOldID[X] + "); ");
            }

            recoper.MoveNext(); 

            X = X + 1;

           
        };

        //Copy records in From/To Operation table and change Operation ID;
        for (i = 1; i<=X; i++) {
            //Set search criteria, search on old operation ID;
            strCriteria2 = "[OpNumF] = " + lngOldID[i];

            //Find first occurrance of search;
            recNxtOp.Filter = strCriteria2;

            //Copy and update From/To records;
            while (!recNxtOp.EOF) {
                //Copy all fields;
                oldrecid = (int)recNxtOp.Fields["recid"].Value;  //gwwd
                lngOpNumT =(int) recNxtOp.Fields["OpNumT"].Value;
                fltPer = (string) recNxtOp.Fields["per"].Value;

                //Add new record to Form/To Table;
                recNxtOp.AddNew();
                recNxtOp.Fields["PartFore"].Value = plngProdNew;
                recNxtOp.Fields["OpNumF"].Value = lngNewID[i];
                recNxtOp.Fields["per"].Value = fltPer;
                //Loop through all new operation numbers to find Operation to;
                for (j = 1; j<=X; j++) {
                    if (lngOpNumT  == lngOldID[j])   {
                        recNxtOp.Fields["OpNumT"].Value = lngNewID[j];
                          break; //exit  For;
                        };
                  }; // end for
                newrecid = (int)recNxtOp.Fields["recid"].Value; //gwwd
                recNxtOp.Update();

                if (GI)  //  gwwd  gwwd
                {
                    runsql("INSERT INTO picbackoperfrto ( RecID, Flag ) VALUES (" + newrecid + ", " + oldrecid + "); ");
                }


                //Find next occurrance of search criteria;
                //x';
                recNxtOp.MoveNext(); //  tbf GD 'x'(strCriteria2);
            };  // end while
          }; // end for

        
        DbUse.CloseAdoRec(recoper);
        recoper = null;
        DbUse.CloseAdoRec(recNxtOp);
        recNxtOp  = null;
        

// return; //exit _CopyOper:
         return; //exit  Sub;

//Err_CopyOper:
        // msgbox(strmsg, 48, "Copy Product Operations");
         //RESUME NEXT
    } // end sub;


    public  string CopyProdFore(string oldname, string pstrnewname) {
        return CopyProdFore(oldname, pstrnewname, false);
    }

    public int CopyProdforeFromGUI(int idToCopy, string nameNew) {
        int idNew = -1;

        // Product variables
        int lngdemd;
        int lngLotsiz;
        int lngTbatch;

        float fltdemdfact;
        float fltvarbility;
        float fltlotsizedfact;
        string fltP1;
        string fltP2;
        string fltP3;
        string fltP4;
        string fltdept;
        string fltcomment;
        bool lngMTS;
        bool lngTBG;

        try {
            ADODB.Recordset recProdFore = new ADODB.Recordset();
            DbUse.open_ado_rec(globaldb, ref  recProdFore, "tblProdFore");
            //Find first match of search criteria;
            recProdFore.Filter = "[prodid] = " + idToCopy;

            if (!recProdFore.EOF) {

                //Copy all fields;

                lngdemd = int.Parse(recProdFore.Fields["enddemd"].Value.ToString());
                lngLotsiz = int.Parse(recProdFore.Fields["lotsiz"].Value.ToString());
                lngTbatch = int.Parse(recProdFore.Fields["transferbatch"].Value.ToString());
                fltdemdfact = ((float)recProdFore.Fields["demandfac"].Value);
                fltvarbility = ((float)recProdFore.Fields["Variability"].Value);
                fltlotsizedfact = ((float)recProdFore.Fields["lotsizefac"].Value);
                if (Convert.IsDBNull(recProdFore.Fields["P1"].Value))
                    fltP1 = " ";
                else
                    fltP1 = (string)recProdFore.Fields["P1"].Value; // gerg - fix logic!! space cannot be converted to float!!!!
                if (Convert.IsDBNull(recProdFore.Fields["P2"].Value))
                    fltP2 = " ";
                else
                    fltP2 = (string)recProdFore.Fields["P2"].Value;  //  fltPx is a string ...
                if (Convert.IsDBNull(recProdFore.Fields["P3"].Value))
                    fltP3 = " ";
                else
                    fltP3 = (string)recProdFore.Fields["P3"].Value;
                if (Convert.IsDBNull(recProdFore.Fields["P4"].Value))
                    fltP4 = " ";
                else
                    fltP4 = (string)recProdFore.Fields["P4"].Value;

                if (Convert.IsDBNull(recProdFore.Fields["proddept"].Value))
                    fltdept = " ";
                else
                    fltdept = (string)recProdFore.Fields["Proddept"].Value;
                if (Convert.IsDBNull(recProdFore.Fields["prodcomment"].Value))
                    fltcomment = " ";
                else
                    fltcomment = (string)recProdFore.Fields["Prodcomment"].Value;

                lngMTS = (bool)recProdFore.Fields["makestock"].Value;
                lngTBG = (bool)recProdFore.Fields["TBatchGather"].Value;

            
                //Add new record;
                recProdFore.AddNew();

                //Paste all fields;
                recProdFore.Fields["proddesc"].Value = nameNew;
                recProdFore.Fields["enddemd"].Value = lngdemd;
                recProdFore.Fields["lotsiz"].Value = lngLotsiz;
                recProdFore.Fields["transferbatch"].Value = lngTbatch;
                recProdFore.Fields["demandfac"].Value = fltdemdfact;
                recProdFore.Fields["Variability"].Value = fltvarbility;
                recProdFore.Fields["lotsizefac"].Value = fltlotsizedfact;
                recProdFore.Fields["P1"].Value = fltP1;
                recProdFore.Fields["P2"].Value = fltP2;
                recProdFore.Fields["P3"].Value = fltP3;
                recProdFore.Fields["P4"].Value = fltP4;
                recProdFore.Fields["ProdDept"].Value = fltdept;
                recProdFore.Fields["ProdComment"].Value = fltcomment;
                recProdFore.Fields["makestock"].Value = lngMTS;
                recProdFore.Fields["TBatchGather"].Value = lngTBG;

                //Copy new Product ID;
                idNew = (int)recProdFore.Fields["prodid"].Value;

                //Save new record;
                recProdFore.Update();
                recProdFore.Close();
                //  void to copy all matching Product Assignments {
                CopyOper(idToCopy, idNew, true);   //gwwd change
                CopyIbom(idToCopy, idNew);

                
            }
        } catch (Exception ex) {
            LogFiles logFiles = new LogFiles();
            logFiles.ErrorLog(ex);
        }

        return idNew;
    }


    public  string CopyProdFore(string oldname, string pstrnewname, Boolean GI ) {
        //on error goto Err_CopyProdFore;

        //Define variables;
        ADODB.Recordset recProdFore = null;
         short  fstatus;
             string varFlag;
            int lngOldID;
            int lngNewID;
             string strmsg;
             string strProdDesc;

            int lngdemd;
            float lngLotsiz;
            int lngTbatch;

            float fltdemdfact;
            float fltvarbility;
            float fltlotsizedfact;
             string fltP1;
             string fltP2;
             string fltP3;
             string fltP4;
             string fltdept;
             string fltcomment;
         bool  lngMTS;
         bool  lngTBG;

        //Set Message box error message;
         strmsg = "There is invalid data in the Product file." + "<br/>" + "Some information may not be copied"; // fix numbers in string

        //Set varaibles;
       DbUse.open_ado_rec(globaldb, ref  recProdFore, "tblProdFore");


        //Find first match of search criteria;
        recProdFore.Filter = "[proddesc] = '" + pstrnewname + "'";

          while (!(recProdFore.EOF)) {
            // msgbox("Product " + pstrnewname + " already exists.  A copy with the name 'Another_" + pstrnewname + "' will be created.", 0, appl_name);
            pstrnewname = "Another_" + pstrnewname;
            recProdFore.Filter = "[proddesc] = '" + pstrnewname + "'";
        }; // end while

        //Find first match of search criteria;
        recProdFore.Filter = "[proddesc] = '" + oldname + "'";

        //Test for match;
        if (! recProdFore.EOF)   {

            //Copy all fields;
            strProdDesc = pstrnewname;

            lngdemd = int.Parse(recProdFore.Fields["enddemd"].Value.ToString());
            lngLotsiz = int.Parse(recProdFore.Fields["lotsiz"].Value.ToString());
            lngTbatch = int.Parse(recProdFore.Fields["transferbatch"].Value.ToString());
            fltdemdfact = ((float)recProdFore.Fields["demandfac"].Value);
            fltvarbility = ((float)recProdFore.Fields["Variability"].Value);
            fltlotsizedfact = ((float)recProdFore.Fields["lotsizefac"].Value);
            if ( Convert.IsDBNull(recProdFore.Fields["P1"].Value)) fltP1 = " "; else fltP1 = (string)recProdFore.Fields["P1"].Value; // gerg - fix logic!! space cannot be converted to float!!!!
            if (Convert.IsDBNull(recProdFore.Fields["P2"].Value)) fltP2 = " "; else fltP2 = (string)recProdFore.Fields["P2"].Value;  //  fltPx is a string ...
            if (Convert.IsDBNull(recProdFore.Fields["P3"].Value)) fltP3 = " "; else fltP3 = (string)recProdFore.Fields["P3"].Value;
            if (Convert.IsDBNull(recProdFore.Fields["P4"].Value)) fltP4 = " "; else fltP4 = (string)recProdFore.Fields["P4"].Value;

            if (Convert.IsDBNull(recProdFore.Fields["proddept"].Value)) fltdept = " "; else fltdept = (string)recProdFore.Fields["Proddept"].Value;
            if (Convert.IsDBNull(recProdFore.Fields["prodcomment"].Value)) fltcomment = " "; else fltcomment = (string)recProdFore.Fields["Prodcomment"].Value;
          
            lngOldID =(int)  recProdFore.Fields["prodid"].Value;
            lngMTS = (bool) recProdFore.Fields["makestock"].Value;
            lngTBG = (bool)  recProdFore.Fields["TBatchGather"].Value;

            //check for duplicate name 
            bool done;
            string str1;
            done = false;
            while (!done)
            {
                str1 = "select tblprodfore.* from tblprodfore where ProdDesc = '" + strProdDesc + "';";
                DbUse.open_ado_rec(globaldb, ref recProdFore, str1);

                if (!recProdFore.EOF)
                {
                    strProdDesc = "A_" + strProdDesc;
                }
                else { done = true; };

            }  // end while
            //Add new record;
            recProdFore.AddNew();

            //Paste all fields;
            recProdFore.Fields["proddesc"].Value = strProdDesc;
            recProdFore.Fields["enddemd"].Value = lngdemd;
            recProdFore.Fields["lotsiz"].Value = lngLotsiz;
            recProdFore.Fields["transferbatch"].Value = lngTbatch;
            recProdFore.Fields["demandfac"].Value = fltdemdfact;
            recProdFore.Fields["Variability"].Value = fltvarbility;
            recProdFore.Fields["lotsizefac"].Value = fltlotsizedfact;
            recProdFore.Fields["P1"].Value = fltP1;
            recProdFore.Fields["P2"].Value = fltP2;
            recProdFore.Fields["P3"].Value = fltP3;
            recProdFore.Fields["P4"].Value = fltP4;
            recProdFore.Fields["ProdDept"].Value = fltdept;
            recProdFore.Fields["ProdComment"].Value = fltcomment;
            recProdFore.Fields["makestock"].Value = lngMTS;
            recProdFore.Fields["TBatchGather"].Value = lngTBG;

            //Copy new Product ID;
            lngNewID = (int) recProdFore.Fields["prodid"].Value;

            //Save new record;
            recProdFore.Update();


            if (GI)
            {
                runsql("DELETE picbackprodfore.* FROM picbackprodfore;");
                runsql("INSERT INTO picbackprodfore ( ProdID, Flag ) SELECT " + lngNewID + " AS Expr1, " + lngOldID + " AS Expr2 FROM picbackprodfore;");

                runsql("DELETE picbackoper.* FROM picbackoper;");
                runsql("DELETE picbackoperfrto.* FROM picbackoperfrto;");
            }

            //  void to copy all matching Product Assignments {
             CopyOper(lngOldID, lngNewID, GI);   //gwwd change
             CopyIbom(lngOldID, lngNewID);  
        } else {
            // msgbox("Internal error : cannot find product " + oldname + ". Aborting copy ", 0, appl_name);
             throw new Exception("Internal error: cannot find product " + oldname + ".Aborting copy. "); //exit  Sub;
            };


        if (null != recProdFore)    {
            DbUse.CloseAdoRec(recProdFore);
            recProdFore =  null;
            };
// return; //exit _CopyProdFore:
         return pstrnewname; //exit  Sub;

//Err_CopyProdFore:
        // msgbox(strmsg, 48, "Copy Product");
         //RESUME NEXT
    } // end sub;

    public  void del_op_res(int operid) {
        //on error goto err_dor;
        //Close();
        //delete all references to operid from tblrsoper;
        runsqlado("delete tblrsoper.* from tblrsoper where (tblrsoper.opid = " + operid + ");");

        //delete all references to operid from tblrsoper_select;
        runsqlado("delete tblrsoper_select.* from tblrsoper_select where (tblrsoper_select.opid = " + operid + ");");

        //delete all references to operid from tblwhatifaudit;
        runsqlado("delete tblwhatifaudit.* FROM tblwhatifaudit WHERE ((tblwhatifaudit.tablea= 'tbloper') And (tblwhatifaudit.recordid=" + operid + "));");
        //Open();


//exit_dor:
         return; //exit  Sub;
//err_dor:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_dor;

    } // end sub;

    public bool runsqlado(string commandString) {
        if (globaldb == null) {
            globaldb = new ADODB.Connection();
            DbUse.OpenAdo(globaldb, connectionString);
        }
        return DbUse.RunSqlAdo(globaldb, commandString);
    }

    public  void del_route_res(int routeid ) {
        //on error goto err_droute;
        //Close();
        //delete all references to routeid from tbloperfrto;

        runsqlado("delete tbloperfrto.* from tbloperfrto where (tbloperfrto.recid = " + routeid + ");");


        //delete all references to routeid from tblwhatifaudit;
        runsqlado("delete tblwhatifaudit.* FROM tblwhatifaudit WHERE ((tblwhatifaudit.tablea= 'tbloperfrto') And (tblwhatifaudit.recordid=" + routeid + "));");
        //Open();

//exit_droute:
         return; //exit  Sub;
//err_droute:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_droute;

    } // end sub;
    public  void del_pt_res(int prid) {
        //on error GoTo err_dpr;
        //Close();
        //delete all references to prid from tblrsprod;
        runsqlado("delete tblrsprod.* from tblrsprod where (tblrsprod.prodid = " + prid + ");");

        //delete all references to prid from tblrsibomtree;
         runsqlado("delete tblrsibomtree.* from tblrsibomtree where (tblrsibomtree.prodid = " + prid + ");");

        //delete all references to prid from tblwhatifaudit;
         runsqlado("delete tblwhatifaudit.* FROM tblwhatifaudit WHERE ((tblwhatifaudit.tablea= 'tblprodfore') And (tblwhatifaudit.recordid=" + prid + "));");
         runsqlado("DELETE tblwhatifaudit.*, tbloper.ProdFore, tblwhatifaudit.TableA FROM tbloper INNER JOIN tblwhatifaudit ON tbloper.OpID = tblwhatifaudit.RecordID WHERE (((tbloper.ProdFore)=" + prid + ") AND ((tblwhatifaudit.TableA)='tbloper'));");


        //delete all references to prid from tblrsoper;
         runsqlado("DELETE tblrsoper.* FROM tblrsoper WHERE ((tblrsoper.prodid = " + prid + "));");
        

        // Cleaning input side;
        //delete all references to prid from tblibom;
       runsqlado("DELETE tblibom.* FROM tblibom WHERE ((tblibom.parentid = " + prid + "));");
       runsqlado("DELETE tblibom.* FROM tblibom WHERE ((tblibom.compid = " + prid + "));");

        //delete all references to prid from tbloper;
        runsqlado("DELETE tbloper.* From tbloper wHERE (((tbloper.ProdFore)=" + prid + "));");

        //delete all references to prid from tbloper;
        runsqlado("DELETE tbloperfrto.* From tbloperfrto WHERE (((tbloperfrto.PartFore)= " + prid + "));");
        
        clean_ops_route_quick((prid));

//exit_dpr:
         return; //exit  Sub;
//err_dpr:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT

    } // end sub;

 public     void clean_ops_route_quick(int prodid) {
        //on error goto err_clean;

             string str1;
            int num1;

             string strcrit;
            int routeid;
             string opname;
            int opnum;
            int operid;
            int eqnone;

        //Close();
        str1 = "UPDATE tbloper SET tbloper.ProdFore = " + prodid + " WHERE (((tbloper.ProdFore)=0));";
        runsqlado(str1);
        str1 = "UPDATE tbloperfrto SET tbloperfrto.PartFore = " + prodid + " WHERE (((tbloperfrto.PartFore)=0));";
        runsqlado(str1);


        str1 = "UPDATE tbloperfrto INNER JOIN (tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID) ON tbloperfrto.PartFore = tblprodfore.ProdID SET tbloperfrto.OpNumF = [tbloper].[opid]" + " WHERE (((tbloper.OpNam)='dock') AND ((tbloperfrto.OpNumF)=0) and (prodfore = " + prodid + "));" ;
        runsqlado(str1);
        str1 = "UPDATE tbloperfrto INNER JOIN (tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID) ON tbloperfrto.PartFore = tblprodfore.ProdID SET tbloperfrto.OpNumT = [tbloper].[opid] " + " WHERE (((tbloper.OpNam)='stock') AND ((tbloperfrto.OpNumT)=0) and (tbloperfrto.partfore = " + prodid + "));" ;
        runsqlado(str1);
        //Open();


//exit_clean:
         return; //exit  Sub;
//err_clean:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_clean;
    } // end sub;


    
public void clean_all_tables() { 

  runsql(" DELETE tblRsEquip.*, tblRsEquip.WID, tblEquip.EquipDescFROM tblEquip RIGHT JOIN tblRsEquip ON tblEquip.EquipID = tblRsEquip.EquipID WHERE (((tblRsEquip.WID)<>0) AND ((tblEquip.EquipDesc)=Null));" );
  runsql(" DELETE tblRsLabor.*, tblRsLabor.WID, tblLabor.LaborDesc FROM tblLabor RIGHT JOIN tblRsLabor ON tblLabor.LaborID = tblRsLabor.LaborID WHERE (((tblRsLabor.WID)<>0) AND ((tblLabor.LaborDesc)=Null));" );
  runsql(" DELETE tblRsProd.*, tblRsProd.WID, tblProd.ProdDescFROM tblProdfore RIGHT JOIN tblRsProd ON tblProdfore.ProdID = tblRsProd.ProdID WHERE (((tblRsProd.WID)<>0) AND ((tblProd.ProdDesc)=Null));" );
  runsql(" DELETE tblRsOper.*, tblRsOper.WID, tblOper.OperDesc FROM tblOper RIGHT JOIN tblRsOper ON tblOper.OperID = tblRsOper.OperID WHERE (((tblRsOper.WID)<>0) AND ((tblOper.OperDesc)=Null));" );
  runsql(" DELETE tblOper.*, tblProdFore.ProdDesc FROM tblOper LEFT JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdIDWHERE (((tblProdFore.ProdDesc)=Null));" );
  runsql(" DELETE tblProdFore.ProdDesc, tblOperFrTo.* FROM tblProdFore RIGHT JOIN tblOperFrTo ON tblProdFore.ProdID = tblOperFrTo.PartFore WHERE (((tblProdFore.ProdDesc) Is Null));" );
  runsql(" DELETE tblOperFrTo.*, tblOper.ProdFore FROM tblOper RIGHT JOIN tblOperFrTo ON tblOper.OpID = tblOperFrTo.OpNumF WHERE (((tblOper.ProdFore)=Null));" );
  runsql(" DELETE tblOperFrTo.*, tblOper.ProdFore FROM tblOperFrTo LEFT JOIN tblOper ON tblOperFrTo.OpNumT = tblOper.OpID WHERE (((tblOper.ProdFore)=Null));" );
  runsql(" DELETE tblProdFore.ProdDesc, tblIbom.*FROM tblIbom LEFT JOIN tblProdFore ON tblIbom.CompID = tblProdFore.ProdID WHERE (((tblProdFore.ProdDesc)=Null));" );
  runsql(" DELETE tblProdFore.ProdDesc, tblIbom.* FROM tblIbom LEFT JOIN tblProdFore ON tblIbom.Parentid = tblProdFore.ProdID WHERE (((tblProdFore.ProdDesc)=Null));" );
  runsql(" DELETE tblWhatIf.Name, tblRsEquip.*, tblRsEquip.WID FROM tblRsEquip LEFT JOIN tblWhatIf ON tblRsEquip.WID = tblWhatIf.WID WHERE (((tblWhatIf.Name)=Null) AND ((tblRsEquip.WID)<>0));" );
  runsql(" DELETE tblWhatIf.Name, tblRsLabor.*, tblRsLabor.WID FROM tblRsLabor LEFT JOIN tblWhatIf ON tblRsLabor.WID = tblWhatIf.WID WHERE (((tblWhatIf.Name)=Null) AND ((tblRsLabor.WID)<>0));" );
  runsql(" DELETE tblWhatIf.Name, tblRsProd.*, tblRsProd.WID FROM tblRsProd LEFT JOIN tblWhatIf ON tblRsProd.WID = tblWhatIf.WID WHERE (((tblWhatIf.Name)=Null) AND ((tblRsProd.WID)<>0));" );
  runsql(" DELETE tblWhatIf.Name, tblRsOper.*, tblRsOper.WID FROM tblRsOper LEFT JOIN tblWhatIf ON tblRsOper.WID = tblWhatIf.WID WHERE (((tblWhatIf.Name)=Null) AND ((tblRsOper.WID)<>0));" );
  runsql(" DELETE tblWhatIf.Name, tblpictures.* FROM tblpictures INNER JOIN tblWhatIf ON tblpictures.seqno = tblWhatIf.WID WHERE (((tblWhatIf.Name)=Null));");

      return;

}


    public  void del_eq_res(int prid) {
        //on error goto err_der;
       
        //delete all references to prid from tblrsequip;
        runsqlado("delete tblrsequip.* from tblrsequip where (tblrsequip.equipid = " + prid + ");");

        //delete all references to prid from tblwhatifaudit;
        runsqlado("delete tblwhatifaudit.* FROM tblwhatifaudit WHERE ((tblwhatifaudit.tablea= 'tblequip') And (tblwhatifaudit.recordid=" + prid + "));");
        
//exit_der:
         return; //exit  Sub;
//err_der:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT

    } // end sub;

    public  void del_labor_res(int prid ) {
        //on error goto err_dlr;
        //Close();
        //delete all references to prid from tblrslabor;
        runsqlado("delete tblrslabor.* from tblrslabor where (tblrslabor.laborid = " + prid + ");");

        //delete all references to prid from tblwhatifaudit;
        runsqlado("delete tblwhatifaudit.* FROM tblwhatifaudit WHERE ((tblwhatifaudit.tablea= 'tbllabor') And (tblwhatifaudit.recordid=" + prid + "));");
        //Open();
//exit_dlr:
         return; //exit  Sub;
//err_dlr:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_dlr;

    } // end sub;

   int  xxxdup_oper(int Oldopid, int plngProdNew) {
        //  where is dup_oper  used and why deleting routing ???

        //on error goto Err_CopyOper;
       int ret;  // retirn value

        //Define variables;
        
        ADODB.Recordset  recoper = null;
             string strCriteria; 


         short  X;
         short  i;
         short  j;

             string strOpNam;
             int  intOpNum;
             string strpctAssgn;
            int EqID;

             string strEqsetupTime;
             string strEqrunTime;
             string strEqsetupPiece;
             string strEqrunLot;
             string strEqsetupTBatch;
             string strEqrunTBatch;

             string strLabsetupTime;
             string strlabrunTime;
             string strLabsetupPiece;
             string strlabrunLot;
             string strLabsetupTBatch;
             string strlabrunTBatch;

             string fltO1;
             string fltO2;
             string fltO3;
             string fltO4;

            int lngOpNumT;
            int lngProdFore;
             string fltPer;

             string strmsg;

        //Set Message box error message;
             strmsg = "There is invalid data in the Operations file." + "<br/>" + "Some information may not be copied";

        //Open table;
        DbUse.open_ado_rec(globaldb, ref recoper, "tblOper");

        //Create search ctriteria;
        strCriteria = "[opid] = " + Oldopid;

        //Find first occurrance of search criteria;
        recoper.Filter = strCriteria;

        //Test for Match and then copy and update all associated records;

        if (recoper.EOF)   {
            // msgbox("Internal error :???  Can't find old operation ?", 0, appl_name);
            //tomorrow  ERROR PAGE
             return (0); //exit  Function;
            };

        //copy and update all associated records;

        //Copy all fields in operation record;
        strOpNam = (string) recoper.Fields["opnam"].Value;
        intOpNum = (int) recoper.Fields["opnum"].Value;
        strpctAssgn = (string) recoper.Fields["Percentassign"].Value;
        EqID = (int)  recoper.Fields["EqID"].Value;

        strEqsetupTime = (string) recoper.Fields["EqSetupTime"].Value;
        strEqrunTime = (string) recoper.Fields["EqRunTime"].Value;
        strEqsetupTBatch =(string)  recoper.Fields["eqSetupTbatch"].Value;
        strEqrunTBatch =(string)  recoper.Fields["eqRunTbatch"].Value;
        strEqsetupPiece =(string)  recoper.Fields["eqSetupPiece"].Value;
        strEqrunLot = (string) recoper.Fields["eqrunlot"].Value;

        strLabsetupTime = (string) recoper.Fields["LabSetupTime"].Value;
        strlabrunTime =(string)  recoper.Fields["LabRunTime"].Value;
        strLabsetupTBatch = (string) recoper.Fields["labSetupTbatch"].Value;
        strlabrunTBatch = (string) recoper.Fields["labRunTbatch"].Value;
        strLabsetupPiece = (string)  recoper.Fields["labSetupPiece"].Value;
        strlabrunLot = (string) recoper.Fields["labrunlot"].Value;

        fltO1 = (string) (Convert.IsDBNull(recoper.Fields["o1"].Value)? " ": (string) recoper.Fields["o1"].Value);
        fltO2 = (string) (Convert.IsDBNull(recoper.Fields["o2"].Value)? " ": (string) recoper.Fields["o2"].Value);
        fltO3 = (string) (Convert.IsDBNull(recoper.Fields["o3"].Value)? " ": (string) recoper.Fields["o3"].Value);
        fltO4 = (string) (Convert.IsDBNull(recoper.Fields["o4"].Value)? " ": (string) recoper.Fields["o4"].Value);


        //check for duplicate name 
        bool done;
        string str1;
        done = false;
        while (!done)
        {
            str1 = "select tbloper.* from tbloper where name = '" + strOpNam + "';";
            DbUse.open_ado_rec(globaldb, ref recoper, str1);

            if (!recoper.EOF)
            {
                strOpNam = "A_" + strOpNam;
            }
            else { done = true; };

        }  // end while

       //Add new record to operation table;
        recoper.AddNew();
        //Paste all field into new record and update FK fields;
        recoper.Fields["opnam"].Value = strOpNam;
        recoper.Fields["opnum"].Value = intOpNum;
        recoper.Fields["Percentassign"].Value = strpctAssgn;
        recoper.Fields["EqID"].Value = EqID;
        recoper.Fields["EqSetupTime"].Value = strEqsetupTime;
        recoper.Fields["EqRunTime"].Value = strEqrunTime;
        recoper.Fields["eqSetupTbatch"].Value = strEqsetupTBatch;
        recoper.Fields["eqRunTbatch"].Value = strEqrunTBatch;
        recoper.Fields["eqSetupPiece"].Value = strEqsetupPiece;
        recoper.Fields["eqrunlot"].Value = strEqrunLot;

        recoper.Fields["LabSetupTime"].Value = strLabsetupTime;
        recoper.Fields["LabRunTime"].Value = strlabrunTime;
        recoper.Fields["labSetupTbatch"].Value = strLabsetupTBatch;
        recoper.Fields["labRunTbatch"].Value = strlabrunTBatch;
        recoper.Fields["labSetupPiece"].Value = strLabsetupPiece;
        recoper.Fields["labrunlot"].Value = strlabrunLot;

        recoper.Fields["o1"].Value = fltO1;
        recoper.Fields["o2"].Value = fltO2;
        recoper.Fields["o3"].Value = fltO3;
        recoper.Fields["o4"].Value = fltO4;
        recoper.Fields["ProdFore"].Value = plngProdNew;
        ret = (int) recoper.Fields["opid"].Value;


        recoper.Update();
        DbUse.CloseAdoRec(recoper);

        //  where is dup_oper  used and why deleting routing ???
        strCriteria = "DELETE tbloperfrto.* From tbloperfrto WHERE (((tbloperfrto.OpNumF)=" + Oldopid + " ));";
        runsqlado(strCriteria);

        strCriteria = "DELETE tbloperfrto.* From tbloperfrto WHERE (((tbloperfrto.OpNumT)=" + Oldopid + " ));";
        runsqlado(strCriteria);

        if (null != recoper)   {
            DbUse.CloseAdoRec(recoper);
            recoper =  null;
            };

// return; //exit _CopyOper:
         return(ret); //exit  Function;

//Err_CopyOper:
        // msgbox(strmsg, 48, "Copy Product Operations");
         //RESUME NEXT
    } // end function;


    /* not needed ???

     short ifnull(Object varValue)  {
        //on error goto Err_IfNull;
         short ifnull;

    ifnull = -1;
        if (IsDBNull(varValue))   {
            ifnull = -1;
        } else {
            ifnull = -1;
        } else {
            ifnull = 0;
            };

// return; //exit _IfNull:
         return (ifnull) ; //exit  Function;

//Err_IfNull:

        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _IfNull;
    } // end function;
    */  


    public int   find_nameItem(string name1 , int item2, short item_type, int Count ) {

        //on error goto Err_find_nameItem;
        int retval;

        //Define variables;
            ADODB.Recordset reccust = null;
            string strtbl;
            int index1;
            int i;

        retval = 0;

        strtbl = "";

        switch (item_type) {
            case (General):
                {
                    strtbl = "SELECT DISTINCTROW tblgeneral.generalid AS index1 FROM tblGeneral;";
                } break;
             case (Labor) : {
                strtbl = "SELECT DISTINCTROW tblLabor.laborid AS index1 FROM tblLabor WHERE ((tblLabor.Labordesc='"  + name1 + "'));";
            } break;
            case (equip) : {
                strtbl = "SELECT DISTINCTROW tblEquip.Equipid AS index1 FROM tblEquip WHERE ((tblEquip.Equipdesc='" + name1  + "'));";
            } break;
             case (product) : {
                strtbl = "SELECT DISTINCTROW tblProdFore.Prodid AS index1 FROM tblProdFore WHERE ((tblProdfore.Proddesc='"  + name1  + "'));";

            }break;
            case (oper) : {
                strtbl = "SELECT DISTINCTROW tblOper.opid AS index1 FROM tblOper WHERE ((tbloper.opnam='"  + name1  + "') and (tbloper.prodfore =" + item2 + "));";
            }break;
            
            case (route) : {
                strtbl = "SELECT DISTINCTROW tblOperFrTo.RecID AS index1, tblOperFrTo.OpNumF FROM tblOperFrTo INNER JOIN tblOper ON tblOperFrTo.OpNumF = tblOper.OpID WHERE (((tblOper.OpNam)='" + name1 + "') AND ((tblOperFrTo.PartFore)=" + item2 + ")) ORDER BY tblOper.OpID;";
            } break;
            case (ibom) : {
                strtbl = "SELECT DISTINCTROW tblibom.ibomid as index1 FROM tblibom INNER JOIN tblprodfore ON tblibom.ParentID = tblprodfore.ProdID WHERE (((tblibom.CompID)=" + item2 + ") AND ((tblprodfore.ProdDesc)='" + name1 + "'));";

            } break;
            case (Whatif) : {
                strtbl = "SELECT DISTINCTROW zstblwhatif.wid AS index1 FROM tblwhatif WHERE ((tblwhatif.name = '"  + name1 + "'));";
            }break;
            case (Eq_type) : {
                strtbl = "SELECT DISTINCTROW tblequiptype.typeid AS index1 FROM tblequiptype WHERE ((tblequiptype.equiptypename= '"  + name1 + "'));";
            }break;
        };  // end of switch;

        DbUse.open_ado_rec(globaldb, ref reccust, strtbl); 

        if ((reccust.EOF))   {
            retval = 0;
        } else {
            reccust.MoveFirst();
            for (i = 0;i<=Count;i++ ){ 
                if (item_type != Eq_type) {
                    retval =(int) reccust.Fields["index1"].Value;
                }
                else
                {
                    retval = (short) reccust.Fields["index1"].Value;
                }
                reccust.MoveNext();
              }; // end for
            };

//exit_retval:

        if (null != reccust)    {
            DbUse.CloseAdoRec(reccust);
            reccust =  null;
            };
         return(retval); //exit  Function;

//Err_retval:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT

    } // end function;

    public void Close() {
        if (globaldb != null) {
            try {
                globaldb.Close();
            } catch (Exception) { }
            globaldb = null;
        }
        if (globaldb_a != null) {
            try {
                globaldb_a.Close();
            } catch (Exception) { }
        }
    }

    public void Open() {
        if (globaldb == null) {
            globaldb = new ADODB.Connection();
        } else {
            try {
                globaldb.Close();
            } catch (Exception) { }
        }
        DbUse.OpenAdo(globaldb, connectionString);
    }


    public static string dig_round_deci(string str1, Boolean keep)
        //  removing decimal point then adding back ....
    {
        int i;
        int deci_loc;
        string sign;
        string str2;

        if (str1[0] == '-') {
            sign = "-";
            str1 = str1.Substring(1);
        } else
            sign = "";



        for (i = 0; i < str1.Length; i++) {
            if (str1[i] == '.') {
                deci_loc = i;
                str2 = str1.Substring(0, i);
                str2 += str1.Substring(i + 1);
                str1 = str2;
                if (str1[0] == '9') { str1 = "0" + str1; deci_loc += 1; }
                str1 = dig_round_last(str1, true);

                str2 = str1.Substring(0, deci_loc) + "." + str1.Substring(deci_loc);   // "   617 868 0847
                if (str2[0] == '0')
                    str2 = str2.Substring(1);
                str1 = sign + str2;
                return (str1);
            }

        }  //  no decimal point found ...

        deci_loc = -1; //  infinity 
        if (str1[0] == '9') { str1 = "0" + str1; }
        str1 = dig_round_last(str1, true);

        if (str1[0] == '0')
            str1 = str1.Substring(1);
        str1 = sign + str1;
        return (str1);
    }


    public static string dig_round_last(string str1, Boolean keep)
        //  rounding of the string last digit
    {
        int len;
        int val;
        int i;
        int j;
        string str2;
        Boolean done;
        string dig1;
        string sign;


        if (str1[0] == '-') {
            sign = "-";
            str1 = str1.Substring(1);
        } else
            sign = "";

        len = str1.Length;

        if (str1[len - 1] == '.') {
            //  deasd code
            str2 = str1.Remove(len - 1);
            str1 = dig_round_last(str2, true);
            str1 += ".";
            return (sign + str1);
        };

        
        dig1 = Convert.ToString(str1[len - 1]);
        try {
            int num = Convert.ToInt16(dig1);
        } catch (Exception) {
            string prob = "problem";
        }
        if (Convert.ToInt16(dig1) < 5) {
            if (keep == false)
                str1 = str1.Remove(len - 1);
            else
                str1 = str1.Substring(0, len - 1) + "0";
            return (sign + str1);


        } else {//  round up
            i = len - 2;
            done = false;
            while (done == false)


            // .9   5.9  9.9   -0.9  -.9
            {
                if (Convert.ToString(str1[i]) == ".") {  //dead code 
                    i -= 1;
                    keep = true;
                    if (i == -1) { return (sign + "1." + str1.Substring(i + 1)); }
                }  //  keep going ...
                else if (Convert.ToString(str1[i]) == "-") { return ("-1" + str1.Substring(i + 1)); }  //  dead code 
                else if (i == -1) { return (sign + "1" + str1.Substring(i + 1)); }  //   dead code ??
                else if (Convert.ToString(str1[i]) == "9")  //  need to add 1 at prev digit
                {
                    if (keep == false)
                        str1 = str1.Remove(i);  //  9.  dead code 
                    else {
                        if (i > 0) { str1 = str1.Substring(0, i) + "0" + str1.Substring(i + 1); } else { str1 = "10" + str1.Substring(i + 1); return (sign + str1); }  // dead code i == 0
                    }
                    i -= 1;

                } else {  //  not 9

                    dig1 = Convert.ToString(str1[i]);
                    val = Convert.ToInt16(dig1) + 1;  //  must be <9 due to if above   +1  val<=9
                    str2 = str1.Substring(0, i) + Convert.ToString(val);
                    if ((keep == true) & ((i - 1) < str1.Length)) {
                        for (j = 0; j < (str1.Length - i - 1); j++) {
                            if (Convert.ToString(str1[j + i + 1]) == ".")
                                return (sign + str2);  // dead code 
                            else
                                str2 += "0";
                        }
                        return (sign + str2);
                    }
                    return sign + str2;

                }

            } // end while
            return sign + str1;
        } // end round up
    }


   

    public static string round_out2(string str1, int dig) {

        int i;
        int j = 0;
        string str2;
        Boolean neg;
        Boolean keep;
        int c;

        if (dig <= 0) { dig = 1; }


        str1 = str1.Trim();
        c = 0;
        str2 = "";

        if (str1.StartsWith("0.")) {
            str2 = "0.";
            c = 2;
        } else if (str1.StartsWith(".")) {
            str2 = ".";
            c = 1;
        } else if (str1.StartsWith("-.")) {
            str2 = "-.";
            c = 2;
        } else if (str1.StartsWith("-0.")) {
            str2 = "-0.";
            c = 3;
        }

        if (c > 0) {
            //  got a small number - do 0s  then count
            for (i = c; i < str1.Length; i++) {
                if (str1[i] == '0') {
                    str2 += "0";
                } else {
                    if ((str1.Length - str2.Length) <= dig) {
                        return str1;
                    } else if ((str1.Length - str2.Length) == (dig + 1)) {
                        return (dig_round_deci(str1, false));
                    }
                    //  now count
                    for (j = 0; j <= dig; j++) {
                        str2 += str1[i + j];
                    }
                    str2 = dig_round_deci(str2, false);
                    return str2;
                }

            }  // end for

        }  // end if c

        //  bigger than .999
        str2 = "";

        if (str1.StartsWith("-")) {
            str1 = str1.Substring(1, str1.Length - 1);
            str2 = "-";
        }

        keep = true;
        c = 0;  //  count of real digits

        for (i = 0; i < str1.Length; i++) {
            if (str1[i] == '.') {
                str2 += ".";
                keep = false;
            } else  //  must be digit!!!
            {
                str2 += str1[i];
                if ((str1[i] != '0') | (c > 0))
                    c += 1;
            }

            if (c > dig) {
                str2 = dig_round_deci(str2, keep);


                if (keep == false)
                    return str2;
                else {
                    for (j = i + 1; j < str1.Length; j++) {
                        if (str1[j] == '.') { str2 += "."; return str2; }
                        str2 += "0";
                    }
                    return str2;
                }
            }  //  end if c>dig
        } // end for

        return str2;

    }

    public string GetErrorMessage() {
        ADODB.Recordset reccust = null;
        string errorMsg;

        DbUse.open_ado_rec(globaldb, ref reccust, "zstblerrors");

        errorMsg = "";
        while (!reccust.EOF) {
            errorMsg = errorMsg + (string)reccust.Fields["Text"].Value + "<br/>";
            reccust.MoveNext();

        }
        DbUse.CloseAdoRec(reccust);


        return errorMsg;
    }



} // end class 
