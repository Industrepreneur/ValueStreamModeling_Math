
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;




/* <summary>
*   edit mode  vs.  add mode 
*   
*  add mode  - basecase + if new  product, equipment, or labor shows up add to lists 
*    see basadd.bas  for code  /addoper/  then /insert/ 
 *    
 * also get add dock/stock/scrap/   and add default routing ...
 * 
 * addOPer_1(partid) to add dock, stock, scrap ...
 * 
 * basdupdate.bas
 *    Sub check_dock_stock()  & 
 *             routing ...
 *             
 * 
* */



public partial class ClassE : ClassC1 {


    string[] pnames;
    int[] partids;
    float[] ptimes0;
    float[] ptimes1;
    float[] ptimes2;
    float[] ptimes3;
    float[] ptimes4;
    float[] ptimes5;
    float[] ptimes6;
    float[] ptimes7;
    float[] ptimes8;
    float[] ptimes9;
    float[] ptimes10;
    float[] ptimes11;
    float[] ptimes12;
    float[] ptimes13;

    int Pole;
    int Listsize;
    float maxpole;

    float treelen;
    float polelen;

    public int gbl_p_range;
    public int gbl_t_range;

    short STOP_LOOP;
    private string p;

    const float minlen = 0.005f;  // gwwd

    public ClassE(string localdir)
        : base(localdir) {

    }

    public class Colors {
        public static Color Cyan = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[0]);
        public static Color Red = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[1]);
        public static Color Orange = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[2]);
        public static Color Yellow = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[3]);
        public static Color Green = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[4]);
        public static Color Magenta = ColorTranslator.FromHtml(TableGraph.COLORS_MCT[5]);
    }


    // //////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void index_poles() {
        //on error goto err_index_poles;

        ADODB.Recordset recPoles = null;
        int i;
        int newp;
        int oldp;
        string strrec;

        strrec = "SELECT DISTINCTROW tblRsIbomPoles.*, tblRsIbomPoles.maxendtime FROM tblRsIbomPoles ORDER BY tblRsIbomPoles.maxendtime, tblRsIbomPoles.pole, tblRsIbomPoles.level;";

        DbUse.open_ado_rec(globaldb, ref recPoles, strrec);

        i = 0;
        newp = 0;
        oldp = -1;
        while (!recPoles.EOF) {
            i = i + 1;
            if ((oldp != (int)recPoles.Fields["Pole"].Value)) {
                oldp = (int)recPoles.Fields["Pole"].Value;
                newp = newp + 1;
            };

            recPoles.Fields["Count"].Value = i;
            recPoles.Fields["Pole"].Value = newp;
            recPoles.Update();
            recPoles.MoveNext();
        }; // end while

        //exit_index_poles:
        DbUse.CloseAdoRec(recPoles);
        recPoles = null;
        return; //exit  Sub;
        //err_index_poles:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT


    } // end sub;


    void index_trees() {
        //on error goto err_index_trees;

        ADODB.Recordset rectrees = null;
        int i;
        string strrec;

        strrec = "SELECT DISTINCTROW tblRsIbomTree.* FROM tblRsIbomTree ORDER BY tblRsIbomTree.maxendtime;";


        DbUse.open_ado_rec(globaldb, ref rectrees, strrec);

        i = 0;
        while (!rectrees.EOF) {
            i = i + 1;

            rectrees.Fields["Flag"].Value = i;
            rectrees.Update();
            rectrees.MoveNext();
        };
        ;  //  end while

        //exit_index_trees:
        DbUse.CloseAdoRec(rectrees);
        rectrees = null;
        return; //exit  Sub;
        //err_index_trees:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT
    } // end sub;

    int polerange(int Flag) {
        //on error goto err_p_range;

        int range;
        int retval;

        range = gbl_p_range;

        if (((range - 1) * 10 <= (Flag - 1)) && ((Flag - 1) <= 10 * (range + 1))) {
            retval = Flag;
        } else {
            retval = 0;
        };

        //exit_p_range:
        return retval; //exit  Function;
        //err_p_range:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_p_range;

    } // end function;

    int size_table(string strtable) {
        //on error goto err_size;


        ADODB.Recordset recdata = null;

        int int1;
        int retval;

        retval = 0;


        DbUse.open_ado_rec(globaldb, ref recdata, strtable);

        int1 = 0;
        while (!recdata.EOF) {
            int1 = int1 + 1;
            recdata.MoveNext();
        };
        retval = int1;

        //exit_size:
        DbUse.CloseAdoRec(recdata);
        recdata = null;
        return retval; //exit  Function;
        //err_size:
        //'  no message size = 0... // msgbox errortostring, 0, appl_name;
        //retval = 0;
        //return retval; //exit  Function;

    } // end function;

    int treerange(int Flag) {
        //on error goto err_t_range;

        int range;
        int retval;

        range = gbl_t_range;

        if (((range - 1) * 10 <= (Flag - 1)) && ((Flag - 1) <= 10 * (range + 1))) {
            retval = Flag;
        } else {
            retval = 0;
        };

        //exit_t_range:
        return retval; //exit  Function;
        //err_t_range:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_t_range;

    } // end function;

    void addparttolist(int prodid, int Level, int wid, ADODB.Recordset recprod, short MTS) {
        //on error goto Err_AddParttoList;

        string disp_name;
        short i;
        float Slack;

        if ((Level >= Listsize)) {
            resizelist((Level));
        };

        disp_name = "";
        for (i = 0; i <= (Level - 1); i++) {
            disp_name = disp_name + "   ";
        }; // end for
        disp_name = disp_name + recprod.Fields["prodname"].Value;
        if ((MTS == 1)) {
            disp_name = disp_name + "-From Stock";
        } else if ((MTS == 2)) {
            disp_name = disp_name + "-ASM Stock";
        };

        //'add to list....;
        pnames[Level] = disp_name;
        partids[Level] = prodid;
        if ((MTS == 0)) {
            ptimes0[Level] = ((float)recprod.Fields["flowtime"].Value);
            ptimes1[Level] = ((float)recprod.Fields["LTEquip"].Value);
            ptimes2[Level] = ((float)recprod.Fields["LTLabor"].Value);
            ptimes3[Level] = ((float)recprod.Fields["LTSetup"].Value);
            ptimes4[Level] = ((float)recprod.Fields["LTRun"].Value);
            ptimes5[Level] = ((float)recprod.Fields["LTWaitLot"].Value);
            ptimes6[Level] = ((float)recprod.Fields["LTWaitasm"].Value);
            ptimes7[Level] = 0;
        } else {
            ptimes0[Level] = 0;
            ptimes1[Level] = 0;
            ptimes2[Level] = 0;
            ptimes3[Level] = 0;
            ptimes4[Level] = 0;
            ptimes5[Level] = 0;
            ptimes6[Level] = 0;
            ptimes7[Level] = 0;
        };

        // return; //exit _AddParttoList:
        return; //exit  Sub;

        //Err_AddParttoList:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;


    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    void AddPole(int prodid, int Level, int wid) {
        //on error goto Err_AddPole;

        ADODB.Recordset recPoles = null;
        short i;
        short j;
        double endtime;
        double starttime;
        double Slack;
        int p_start;
        string strCriteria;
        double maxendtime;


        DbUse.open_ado_rec(globaldb, ref recPoles, "tblRsIbomPoles");

        float[] myvalues;
        myvalues = new float[15];

        //'add poles for list to table;

        for (i = 0; i <= Level; i++) {

            myvalues[0] = myvalues[0] + ptimes0[i];
            myvalues[1] = myvalues[1] + ptimes1[i];
            myvalues[2] = myvalues[2] + ptimes2[i];
            myvalues[3] = myvalues[3] + ptimes3[i];
            myvalues[4] = myvalues[4] + ptimes4[i];
            myvalues[5] = myvalues[5] + ptimes5[i];
            myvalues[6] = myvalues[6] + ptimes6[i];
            myvalues[7] = myvalues[7] + ptimes7[i];
            myvalues[8] = myvalues[8] + ptimes8[i];
            myvalues[9] = myvalues[9] + ptimes9[i];
            myvalues[10] = myvalues[10] + ptimes10[i];
            myvalues[11] = myvalues[11] + ptimes11[i];
            myvalues[12] = myvalues[12] + ptimes12[i];
            myvalues[13] = myvalues[13] + ptimes13[i];


        }; // end for

        if ((maxpole == 0)) {
            maxpole = myvalues[0];
        } else if ((maxpole < myvalues[0])) {
            //  // msgbox "Internal error - Max lead time for assembly is wrong -> Slack numbers are wrong", 0, APPL_NAME;
            maxpole = myvalues[0];
        };

        if ((polelen < maxpole)) {
            polelen = maxpole;
        };

        Pole = Pole + 1;

        endtime = maxpole;
        starttime = endtime;

        maxendtime = endtime;
        for (i = 0; i <= Level; i++) {
            maxendtime = maxendtime - ptimes0[i];
        }; // end for

        for (i = 0; i <= Level; i++) {
            // add record;
            recPoles.AddNew();
            recPoles.Fields["wid"].Value = wid;
            recPoles.Fields["partdesc"].Value = pnames[i];
            recPoles.Fields["prodid"].Value = partids[i];
            recPoles.Fields["Pole"].Value = Pole;
            recPoles.Fields["Level"].Value = i;
            recPoles.Fields["Count"].Value = Count + 1;
            Count = Count + 1;
            recPoles.Fields["endtime"].Value = dig_round(starttime, 4);
            recPoles.Fields["starttime"].Value = dig_round(starttime - ptimes0[i], 4);

            if (((double)recPoles.Fields["starttime"].Value) < 0.000001) {
                recPoles.Fields["starttime"].Value = 0;
            };

            recPoles.Fields["maxendtime"].Value = dig_round(maxendtime, 4);

            recPoles.Fields["flowtime"].Value = dig_round(ptimes0[i], 4);

            recPoles.Fields["LTEquip"].Value = dig_round(ptimes1[i], 4);
            recPoles.Fields["LTLabor"].Value = dig_round(ptimes2[i], 4);
            recPoles.Fields["LTSetup"].Value = dig_round(ptimes3[i], 4);
            recPoles.Fields["LTRun"].Value = dig_round(ptimes4[i], 4);
            recPoles.Fields["LTWaitLot"].Value = dig_round(ptimes5[i], 4);
            recPoles.Fields["LTWaitasm"].Value = dig_round(ptimes6[i], 4);

            recPoles.Fields["Slack"].Value = dig_round(polelen - ((double)recPoles.Fields["endtime"].Value), 4);

            starttime = starttime - ptimes0[i];

            recPoles.Update();
        }; // end for

        p_start = Count;

        // add Total record;
        recPoles.AddNew();
        recPoles.Fields["wid"].Value = wid;
        recPoles.Fields["partdesc"].Value = "--- TOTAL ---";
        recPoles.Fields["prodid"].Value = 0;
        recPoles.Fields["Pole"].Value = Pole;
        recPoles.Fields["Level"].Value = Level + 1;
        recPoles.Fields["Count"].Value = Count + 1;
        Count = Count + 1;
        recPoles.Fields["endtime"].Value = dig_round(maxpole, 4);
        recPoles.Fields["starttime"].Value = dig_round(starttime, 4);
        recPoles.Fields["maxendtime"].Value = dig_round(maxendtime, 4);
        recPoles.Fields["flowtime"].Value = dig_round(myvalues[0], 4);


        recPoles.Fields["LTEquip"].Value = dig_round(myvalues[1], 4);
        recPoles.Fields["LTLabor"].Value = dig_round(myvalues[2], 4);
        recPoles.Fields["LTSetup"].Value = dig_round(myvalues[3], 4);
        recPoles.Fields["LTRun"].Value = dig_round(myvalues[4], 4);
        recPoles.Fields["LTWaitLot"].Value = dig_round(myvalues[5], 4);
        recPoles.Fields["LTWaitasm"].Value = dig_round(myvalues[6], 4);

        Slack = maxpole - myvalues[0];
        recPoles.Update();

        // add BLANK;
        recPoles.AddNew();
        recPoles.Fields["wid"].Value = wid;
        recPoles.Fields["partdesc"].Value = "  ";
        recPoles.Fields["prodid"].Value = 0;
        recPoles.Fields["Pole"].Value = Pole;
        recPoles.Fields["Level"].Value = Level + 2;
        recPoles.Fields["Count"].Value = Count + 1;
        Count = Count + 1;
        recPoles.Fields["maxendtime"].Value = dig_round(maxendtime, 4);
        recPoles.Fields["endtime"].Value = 0;
        recPoles.Fields["starttime"].Value = 0;
        recPoles.Fields["flowtime"].Value = 0;
        recPoles.Fields["Slack"].Value = 0;
        recPoles.Update();

        // free myvalues ?  // tbd  or on the stack ??

        // return; //exit _AddPole:
        DbUse.CloseAdoRec(recPoles);
        recPoles = null;
        return; //exit  Sub;

        //Err_AddPole:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;


    public short m_makeResultsPoles(int ParentID) {
        //on error goto err_m_makeRsPoles;

        string strCriteria;
        int count1;
        short retval;

        runsqlado("DELETE tblRsIbomPoles.* from tblRsIbomPoles;");



        resizelist((15));
        Count = 0;
        Pole = 0;
        maxpole = 0.0F;

        count1 = get_count("tblrsprod", glngwid);
        if ((count1 <= 0)) {
            retval = 0;
            return retval; //exit  Function;
        };

        if ((glngwid != 0)) {
            set_max_pole(ParentID, 0, glngwid, 0);
            makeResultsPoles(ParentID, 0, glngwid, 0);
        };

        maxpole = 0.0F;
        Pole = 0;

        set_max_pole(ParentID, 0, 0, 0);
        makeResultsPoles(ParentID, 0, 0, 0);

        index_poles();

        //exit_m_makeRsPoles:
        retval = -1;
        return retval; //exit  Function;

        //err_m_makeRsPoles:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_m_makeRsPoles;

    } // end function;

    public short m_makeResultsTree(int ParentID) {
        //on error goto err_m_makeRsTree;

        string strCriteria;
        float X;
        short retval;

        runsqlado("DELETE tblRsIbomTree.* from tblRsIbomTree;");

        Count = 0;


        Count = get_count("tblrsprod", glngwid);
        if ((Count <= 0)) {
            retval = 0;
            return retval; //exit  Function;

        };

        if ((glngwid != 0)) {
            maxpole = 0;
            set_max_pole(ParentID, 0, glngwid, 0);
            X = makeResultsTree_count(ParentID, 0, glngwid, maxpole, 0);
            X = makeResultsTree_add(ParentID, 0, glngwid, maxpole, 0);
        };




        maxpole = 0;
        set_max_pole(ParentID, 0, 0, 0);
        X = makeResultsTree_count(ParentID, 0, 0, maxpole, 0);
        X = makeResultsTree_add(ParentID, 0, 0, maxpole, 0);



        //index_trees(); // LUCIE commented out - getting weird exception

        //exit_m_makeRsTree:
        retval = -1;
        return retval; //exit  Function;

        //err_m_makeRsTree:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_m_makeRsTree;

    } // end function;




    public void MakeIbomTree(int ParentID, int prodid, int myLevel) {
        //on error goto Err_MakeIbomTree;

        // Define variables;
        ADODB.Recordset recprod = null;
        ADODB.Recordset recIbomtree = null;
        ADODB.Recordset recIbomtree2 = null;
        ADODB.Recordset recIbom = null;
        string strname;
        string strCriteria;
        int i;
        int intcompid;
        float upa1;

        if ((myLevel == 0)) {
            runsqlado("UPDATE tblprodfore SET tblprodfore.Flag = 0;");
        };

        DbUse.open_ado_rec(globaldb, ref recprod, "tblProdFore");
        DbUse.open_ado_rec(globaldb, ref recIbom, "tblIbom");
        DbUse.open_ado_rec(globaldb, ref recIbomtree, "tblbomTree");
        DbUse.open_ado_rec(globaldb, ref recIbomtree2, "tblbomTree");

        strCriteria = "[prodid] = " + prodid;
        recprod.Filter = strCriteria;

        if ((short.Parse(recprod.Fields["Flag"].Value.ToString()) == 1)) {
            // msgbox("Loop in IBOM.  Part '" + recprod.Fields["proddesc"].Value + "' is a component of itself!", 0, appl_name);
            STOP_LOOP = -1;
            // recIbomtree.AddNew;
            // recIbomtree!Level = Level;
            // recIbomtree!Count = Count;
            // recIbomtree!prodid = prodid;
            // recIbomtree!PartDesc = recprod!ProdDesc;
            // Count = Count + 1;
            // recIbomtree!showName = recprod!ProdDesc;
            // recIbomtree!Show = -1;
            // recIbomtree.Update;
            goto exitMakeIbomTree;
        };

        // xxx;
        strCriteria = "[prodid] = " + ParentID;
        recIbomtree2.Filter = strCriteria;
        if (recIbomtree2.EOF) {
            upa1 = 1;
        } else {
            upa1 = ((int)recIbomtree2.Fields["UPf"].Value);
        };

        //   Adding new tree record;
        recIbomtree.AddNew();
        recIbomtree.Fields["Level"].Value = myLevel;
        recIbomtree.Fields["prodid"].Value = prodid;
        recIbomtree.Fields["partdesc"].Value = recprod.Fields["proddesc"].Value;
        recIbomtree.Fields["Count"].Value = Count;
        recIbomtree.Fields["UPf"].Value = 1;
        Count = Count + 1;

        if ((ParentID != 0)) {
            strCriteria = "[parentId] = " + ParentID + " and [compid] = " + prodid;
            recIbom.Filter = strCriteria;
            recIbomtree.Fields["UPA"].Value = recIbom.Fields["UPA"].Value;
            recIbomtree.Fields["UPf"].Value = upa1 * (Double.Parse(recIbom.Fields["UPA"].Value.ToString()));
        } else {
            recIbomtree.Fields["UPA"].Value = 1.0;
        };

        if ((myLevel > 1)) {
            recIbomtree.Fields["Show"].Value = 0;
        } else {
            STOP_LOOP = 0;
            recIbomtree.Fields["Show"].Value = -1;
        };

        //strname = "";
        //for (i = 0; i <= (myLevel - 1); i++) {
        //    strname = strname + "   ";
        //}; // end for

        strCriteria = "[parentId] = " + prodid;
        recIbom.Filter = strCriteria;

        //  if part has No components ??;
        if (recIbom.EOF) {
            strname = recprod.Fields["proddesc"].Value.ToString();
            recIbomtree.Fields["Parent"].Value = 0;
            recIbomtree.Fields["showName"].Value = strname;
            recIbomtree.Update();
        } else {
            //  Part does have components.;

            recprod.Fields["Flag"].Value = 1;
            recprod.Update();
            if ((myLevel == 0)) {
                strname = recprod.Fields["proddesc"].Value.ToString();
            } else {
                strname = recprod.Fields["proddesc"].Value.ToString();
            };

            recIbomtree.Fields["Parent"].Value = -1;
            recIbomtree.Fields["showName"].Value = strname;
            recIbomtree.Update();
            while (!recIbom.EOF) {
                intcompid = (int)recIbom.Fields["compid"].Value;
                if ((intcompid != 0)) {
                    MakeIbomTree(prodid, intcompid, myLevel + 1);
                    if (STOP_LOOP == -1) {
                        goto exitMakeIbomTree;
                    };
                };


                recIbom.MoveNext(); // need to move to another record
            }; // end while
        };  //  end else


    exitMakeIbomTree:

        recprod.Fields["Flag"].Value = 0;
        recprod.Update();

        DbUse.CloseAdoRec(recprod);
        DbUse.CloseAdoRec(recIbom);
        DbUse.CloseAdoRec(recIbomtree);
        DbUse.CloseAdoRec(recIbomtree2);

        recprod = null;
        recIbom = null;
        recIbomtree = null;
        recIbomtree2 = null;
        return; //exit  Sub;

        //Err_MakeIbomTree:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;




    public void MakeNotPossibleTable_int(int prodid) {

        //  add prodid to not possible table
        //   find all parents of prodid not in not possible list  


        string strsql1;
        int prod2;
        ADODB.Recordset recNotPoss = null;



        //  add part to the not possible;



        // find parents of prodid  not in recnotPoss   
        //    do it by ....
        //  mark all prod = 0 
        //  mark       prod =1 if in notposs already 
        //  find parent prods with prodflag = 0 
        //  add those prod to not poss list 



        //  add prod to not poss list 
        strsql1 = "INSERT INTO tblNotPossible ( ProdID )  SELECT " + prodid + " AS Expr1;";
        runsqlado(strsql1);

        //  mark all prod = 0 
        strsql1 = "Update tblProdFore set [flag] = 0;";
        runsqlado(strsql1);

        //  mark   prod =1 if in notposs already and is parent to prodid 
        strsql1 = "UPDATE (tblNotPossible INNER JOIN tblIbom ON tblNotPossible.ProdID = tblIbom.ParentID) INNER JOIN tblProdFore ON tblNotPossible.ProdID = tblProdFore.ProdID SET tblProdFore.flag = 1  WHERE (((tblIbom.CompID)=" + prodid + "));";
        runsqlado(strsql1);

        //  find parent prods with prodflag = 0 

        strsql1 = "SELECT tblProdFore.Flag, tblIbom.CompID, tblProdFore.ProdID FROM tblIbom INNER JOIN tblProdFore ON tblIbom.ParentID = tblProdFore.ProdID WHERE (((tblProdFore.Flag)=0) AND ((tblIbom.CompID)= " + prodid + "));";

        //  add those prod to not poss list 
        DbUse.open_ado_rec(globaldb, ref recNotPoss, strsql1);

        while (recNotPoss.EOF == false) {
            prod2 = (int)recNotPoss.Fields["prodid"].Value;
            //    add    parent to not possible list 
            MakeNotPossibleTable_int(prod2);
            recNotPoss.MoveNext();
        }


        return;

    }
    public void MakePossibleTable(int prodid) {

        string strsql1;

        runsqlado("DELETE tblNotPossible.* from tblNotPossible;");
        runsqlado("DELETE tblPossibleComp.* from tblPossibleComp;");



        //  get all parents onto the nort possible list 
        MakeNotPossibleTable_int(prodid);


        //  put all current components on not possible list too 

        strsql1 = "INSERT INTO tblNotPossible ( ProdID ) SELECT tblIbom.CompID FROM tblIbom WHERE (((tblIbom.ParentID)=" + prodid + "));";
        runsqlado(strsql1);


        //  now  find all product not in not possible list to create possible list;


        runsqlado("Update tblProdFore set [flag] = 0;");

        //  mark prodfore = 1 for products in NOT Possible;
        runsqlado(" UPDATE tblNotPossible INNER JOIN tblProdFore ON tblNotPossible.ProdID = tblProdFore.ProdID SET tblProdFore.Flag = 1;");

        //  move into possible all prodfore = 0
        strsql1 = "INSERT INTO tblPossibleComp ( ProdID, proddesc ) SELECT tblProdFore.ProdID, tblprodfore.proddesc FROM tblProdFore WHERE (((tblProdFore.Flag)=0));";
        runsqlado(strsql1);



    }


    void makeResultsPoles(int prodid, int Level, int wid, short PMTS) {
        //on error goto Err_MakeResultsPoles;

        // Define variables;

        ADODB.Recordset recprod = null;
        ADODB.Recordset recIbomPole = null;
        ADODB.Recordset recResults = null;

        string strname;
        string strCriteria;
        short i;
        int intcompid;
        short makestock;


        //  get data for part;
        //  add pole when a leaf is reached.;
        //    find records for pole by looking to static list;
        //    include a total record as well.;
        //  else {


        DbUse.open_ado_rec(globaldb, ref recIbomPole, "tblRsIbomPoles");

        strCriteria = "SELECT DISTINCTROW tblProdFore.ProdDesc as prodname, tblProdFore.ProdID, tblProdFore.makestock, tblRsProd.* FROM tblRsProd INNER JOIN tblProdFore ON tblRsProd.ProdID = tblProdFore.ProdID WHERE ((tblProdFore.ProdID=" + prodid + ") and (tblrsProd.WID=" + wid + "));";
        DbUse.open_ado_rec(globaldb, ref recprod, strCriteria);
        recprod.MoveFirst();

        strCriteria = "SELECT DISTINCTROW tblIbom.CompID, tblRsProd.FlowTime FROM tblIbom INNER JOIN tblRsProd ON tblIbom.CompID = tblRsProd.ProdID WHERE ((tblIbom.ParentID=" + prodid + ") AND (tblRsProd.WID=" + wid + ")) ORDER BY tblRsProd.FlowTime DESC;";
        DbUse.open_ado_rec(globaldb, ref recResults, strCriteria);

        polelen = 0;

        if (PMTS > 0) {
            makestock = 2;
        } else if ((bool)recprod.Fields["makestock"].Value == true && Level > 0) {
            makestock = 1;
        } else {
            makestock = 0;
        };

        //  if part has No components ??;
        if (recResults.BOF) {
            addparttolist(prodid, Level, wid, recprod, makestock);
            AddPole(prodid, Level, wid);
        } else {
            recResults.MoveFirst();
            // Part does have components.;
            //  add part to list.;

            addparttolist(prodid, Level, wid, recprod, makestock);

            while (!recResults.EOF) {
                intcompid = (int)recResults.Fields["compid"].Value;
                makeResultsPoles(intcompid, Level + 1, wid, makestock);
                recResults.MoveNext();
            };

        };

    exit_MakeResultsPoles:
        DbUse.CloseAdoRec(recprod);
        DbUse.CloseAdoRec(recIbomPole);
        DbUse.CloseAdoRec(recResults);
        recprod = null;
        recIbomPole = null;
        recResults = null;
        return; //exit  Sub;

        //Err_MakeResultsPoles:

        /*     if (Err.Number  == 3021)   {
                return; //exit  Sub;
               };
       */
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;



    void resizelist(int Level) {
        //on error goto Err_Resizelist;


        Array.Resize(ref pnames, (Level + 5));
        Array.Resize(ref  partids, (Level + 5));

        Array.Resize(ref ptimes0, (Level + 5));
        Array.Resize(ref ptimes1, (Level + 5));
        Array.Resize(ref ptimes2, (Level + 5));
        Array.Resize(ref ptimes3, (Level + 5));
        Array.Resize(ref ptimes4, (Level + 5));
        Array.Resize(ref ptimes5, (Level + 5));
        Array.Resize(ref ptimes6, (Level + 5));
        Array.Resize(ref ptimes7, (Level + 5));
        Array.Resize(ref ptimes8, (Level + 5));
        Array.Resize(ref ptimes9, (Level + 5));
        Array.Resize(ref ptimes10, (Level + 5));
        Array.Resize(ref ptimes11, (Level + 5));
        Array.Resize(ref ptimes12, (Level + 5));
        Array.Resize(ref ptimes13, (Level + 5));

        Listsize = Level + 4;

        // return; //exit _Resizelist:
        return; //exit  Sub;

        //Err_Resizelist:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _Resizelist;

    } // end sub;

    void set_max_pole(int prodid, float ptime, int wid, short Level) {
        //on error goto Err_set_max_pole;

        //  if end item check ptime+flowtime vs. maxpole;
        //  for all components.;
        //    setmaxpole(ptime+flowtime, partid);

        ADODB.Recordset recprod = null;
        ADODB.Recordset recRsProd = null;
        ADODB.Recordset recComp = null;

        float X;
        int i;
        string strCriteria;
        float minslack;


        DbUse.open_ado_rec(globaldb, ref recprod, "tblProdfore");
        DbUse.open_ado_rec(globaldb, ref recRsProd, "tblRsProd");
        DbUse.open_ado_rec(globaldb, ref recComp, "tblIbom");

        strCriteria = "[prodid] = " + prodid;
        strCriteria = strCriteria + " and [WID] = " + wid;
        recRsProd.Filter = strCriteria;
        recRsProd.MoveFirst();


        strCriteria = "[prodid] = " + prodid;
        recprod.Filter = strCriteria;
        recprod.MoveFirst();

        if ((bool)recprod.Fields["makestock"].Value == true && Level > 0)   //  todaay makestock bool type cast
        {
            goto exit_set_max_pole;
        };

        X = ptime + (float)recRsProd.Fields["flowtime"].Value;
        if ((X > maxpole)) {
            maxpole = X;
        };

        strCriteria = "[parentid] = " + prodid;
        recComp.Filter = strCriteria;
        if (recComp.EOF) {
            goto exit_set_max_pole;
        };
        recComp.MoveFirst();





        //  if part has components;
        if (!recComp.EOF) {

            while (!recComp.EOF) {
                //  Part does have components.;
                i = (int)recComp.Fields["compid"].Value;
                set_max_pole(i, X, wid, 1);
                recComp.MoveNext();
            };

        };

    exit_set_max_pole:

        DbUse.CloseAdoRec(recprod);
        DbUse.CloseAdoRec(recRsProd);
        DbUse.CloseAdoRec(recComp);
        recprod = null;
        recRsProd = null;
        recComp = null;
        return; //exit  Sub;

        //Err_set_max_pole:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;




    float makeResultsTree_add(int prodid, int Level, int wid, float endt, short PMTS) {
        //on error goto Err_MakeResultsTree;

        // Define variables;

        ADODB.Recordset recprod = null;
        ADODB.Recordset recIbomtree = null;
        ADODB.Recordset recResults = null;

        string strname;
        string strCriteria;
        short i;
        int intcompid;
        float sngx;
        float maxendtime;
        float endtime;
        short maketostock;
        string str1;

        float retval;


        strCriteria = "SELECT DISTINCTROW tblProdFore.ProdDesc as prodname, tblprodfore.MAKESTOCK, tblProdFore.ProdID, tblRsProd.* FROM tblRsProd INNER JOIN tblProdFore ON tblRsProd.ProdID = tblProdFore.ProdID WHERE ((tblProdFore.ProdID=" + prodid + ") and (tblrsProd.WID= " + wid + "));";

        DbUse.open_ado_rec(globaldb, ref recprod, strCriteria);

        DbUse.open_ado_rec(globaldb, ref recIbomtree, "tblRsIbomTree");

        if (Level == 0) {
            treelen = endt;
        };

        recprod.MoveFirst();

        if (((bool)recprod.Fields["makestock"].Value == true && Level > 0) || PMTS == -1) {
            maketostock = -1;
        } else {
            maketostock = 0;
        };

        strname = "";
        for (i = 0; i <= (Level - 1); i++) {
            strname = strname + "   ";
        }; // end for

        strname = strname + recprod.Fields["prodname"].Value;

        // Adding new tree record;
        recIbomtree.AddNew();
        recIbomtree.Fields["wid"].Value = wid;
        recIbomtree.Fields["Level"].Value = Level;
        recIbomtree.Fields["prodid"].Value = prodid;
        recIbomtree.Fields["partdesc"].Value = strname;
        recIbomtree.Fields["endtime"].Value = endt;

        if (PMTS == -1) {
            recIbomtree.Fields["partdesc"].Value = strname + "-Asm Stock";
        } else if ((maketostock == -1)) {
            recIbomtree.Fields["partdesc"].Value = strname + "-In Stock";
        };


        if ((maketostock == 0)) {
            recIbomtree.Fields["starttime"].Value = ((double)recIbomtree.Fields["endtime"].Value) - ((float)recprod.Fields["flowtime"].Value);
        } else {
            recIbomtree.Fields["starttime"].Value = recIbomtree.Fields["endtime"].Value;
        };

        if ((((double)recIbomtree.Fields["starttime"].Value) < 0.000001)) {
            recIbomtree.Fields["starttime"].Value = 0.0;
        };
        maxendtime = (float)((double)recIbomtree.Fields["starttime"].Value);
        sngx = (float)((double)recIbomtree.Fields["starttime"].Value);

        recIbomtree.Fields["Slack"].Value = treelen - endt;

        if ((maketostock == 0)) {
            recIbomtree.Fields["flowtime"].Value = recprod.Fields["flowtime"].Value;
            recIbomtree.Fields["LTEquip"].Value = recprod.Fields["LTEquip"].Value;
            recIbomtree.Fields["LTLabor"].Value = recprod.Fields["LTLabor"].Value;
            recIbomtree.Fields["LTSetup"].Value = recprod.Fields["LTSetup"].Value;
            recIbomtree.Fields["LTRun"].Value = recprod.Fields["LTRun"].Value;
            recIbomtree.Fields["LTWaitLot"].Value = recprod.Fields["LTWaitLot"].Value;
            recIbomtree.Fields["LTWaitasm"].Value = recprod.Fields["LTWaitasm"].Value;
        } else {
            recIbomtree.Fields["flowtime"].Value = 0;
            recIbomtree.Fields["LTEquip"].Value = 0;
            recIbomtree.Fields["LTLabor"].Value = 0;
            recIbomtree.Fields["LTSetup"].Value = 0;
            recIbomtree.Fields["LTRun"].Value = 0;
            recIbomtree.Fields["LTWaitLot"].Value = 0;
            recIbomtree.Fields["LTWaitasm"].Value = 0;
        };

        Count = Count + 1;
        recIbomtree.Fields["Count"].Value = Count;
        recIbomtree.Update();


        strCriteria = "SELECT DISTINCTROW tblIbom.CompID, tblIbom.flag, tblRsProd.FlowTime FROM tblIbom INNER JOIN tblRsProd ON tblIbom.CompID = tblRsProd.ProdID WHERE ((tblIbom.ParentID=" + prodid + ") AND (tblRsProd.WID=" + wid + ")) ORDER BY tblibom.flag";
        DbUse.open_ado_rec(globaldb, ref recResults, strCriteria);


        //  if part has components;
        if ((!recResults.BOF)) {
            recResults.MoveFirst();
            while (!recResults.EOF) {
                intcompid = (int)recResults.Fields["compid"].Value;

                endtime = makeResultsTree_add(intcompid, Level + 1, wid, sngx, maketostock);
                if ((endtime < maxendtime)) {
                    maxendtime = endtime;
                };
                recResults.MoveNext();
            }; // end while
        };



        str1 = " UPDATE tblRsIbomTree SET tblRsIbomTree.maxendtime = " + Convert.ToString(maxendtime) + "  WHERE (((tblRsIbomTree.Count)=" + Count + " ));";
        runsqlado(str1);

        /* 
         * recibomtree.filter = [count] = " + count;
        recIbomtree.Fields["maxendtime"].Value = maxendtime;
        recIbomtree.Update();
        */

exit_MakeResultsTree:
        DbUse.CloseAdoRec(recprod);
        DbUse.CloseAdoRec(recIbomtree);
        DbUse.CloseAdoRec(recResults);
        
        recprod = null;
        recIbomtree = null;
        recResults = null;
        retval = maxendtime;
        return retval; //exit  Function;

        //Err_MakeResultsTree:
        /* if ((Err.Number  == 3021))   {
              return retval; //exit  Function;
             };  */
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;




    float makeResultsTree_count(int prodid, int Level, int wid, float endt, short PMTS) {
        //on error goto Err_MakeResultsTree;

        // Define variables;

        ADODB.Recordset recprod = null;
        ADODB.Recordset recIbom = null;

        int size1;
        string str1;
        string strname;
        string strCriteria;
        int i;
        int j;
        int int1;

        int intcompid;
        float sngx;
        float maxendtime;
        float endtime;

        float[] max_comp = null;
        int[] max_compid = null;
        float tcomp;
        int tcompID;

        short maketostock;
        float starttime;

        float retval;



        strCriteria = "SELECT DISTINCTROW tblProdFore.ProdDesc as prodname, tblprodfore.MAKESTOCK, tblProdFore.ProdID, tblRsProd.* FROM tblRsProd INNER JOIN tblProdFore ON tblRsProd.ProdID = tblProdFore.ProdID WHERE ((tblProdFore.ProdID=" + prodid + ") and (tblrsProd.WID=" + wid + "));";
        DbUse.open_ado_rec(globaldb, ref recprod, strCriteria);

        recprod.MoveFirst();

        if (((bool)recprod.Fields["makestock"].Value == true && Level > 0) || PMTS == -1) {
            maketostock = -1;
        } else {
            maketostock = 0;
        };
        if ((maketostock == 0)) {
            starttime = endt + ((float)recprod.Fields["flowtime"].Value);
        } else {
            starttime = endt;
        };



        str1 = "SELECT DISTINCTROW tblibom.* from tblibom where (tblibom.parentid = " + prodid + ");";
        DbUse.open_ado_rec(globaldb, ref recIbom, str1);


        if ((recIbom.EOF)) {
            maxendtime = starttime;
            goto exit_MakeResultsTree;
        };

        recIbom.MoveFirst();
        int1 = 0;
        while (!recIbom.EOF) {
            int1 = int1 + 1;
            recIbom.MoveNext();
        }; // end while
        size1 = int1;

        /* xxxxxxxxxxxx  */
        //  redim  resize !!
        Array.Resize(ref max_comp, size1);
        Array.Resize(ref max_compid, size1);


        i = 0;
        maxendtime = 0;
        //  if part has components;
        if ((!recIbom.BOF)) {
            recIbom.MoveFirst();
            while (!recIbom.EOF) {
                intcompid = (int)recIbom.Fields["compid"].Value;

                endtime = makeResultsTree_count(intcompid, Level + 1, wid, starttime, maketostock);

                max_comp[i] = endtime;
                max_compid[i] = intcompid;
                i = i + 1;

                if ((endtime > maxendtime)) {
                    maxendtime = endtime;
                };
                recIbom.MoveNext();
            }; // end while
        };


        for (i = 0; i <= (size1 - 2); i++) {
            for (j = i + 1; j <= (size1 - 1); j++) {
                if ((max_comp[i] < max_comp[j])) {
                    tcomp = max_comp[i];
                    max_comp[i] = max_comp[j];
                    max_comp[j] = tcomp;
                    tcompID = (int)max_compid[i];
                    max_compid[i] = max_compid[j];
                    max_compid[j] = tcompID;
                };
            }; // end for
        }; // end for



        for (i = 0; i <= (size1 - 1); i++) {
            recIbom.Filter = "[compid] = " + max_compid[i];
            recIbom.Fields["Flag"].Value = i;
            recIbom.Update();
        }; // end for


exit_MakeResultsTree:
        DbUse.CloseAdoRec(recprod);
        DbUse.CloseAdoRec(recIbom);
        
        recprod = null;
        recIbom = null;

        retval = maxendtime;
        return retval; //exit  Function;


    } // end function;





    public void place_poles(int part_id) {

        int height;
        int width = 1500;

        ADODB.Recordset recpole = new ADODB.Recordset();
        int Count;
        double max_d;
        float max_x;
        string str1;
        string str2;
        int X;
        int Y;
        int max_line;
        string strflow;
        int c_level = 0;
        int previous_level = 0;

        int sizex;
        int sizey;
        string curdir;
        int par_x;
        int par_y;
        double sizex_f;

        double val1; // gwwd


        //---------------------------------------------------------------------

        str1 = "SELECT DISTINCTROW IIf([zstblwhatif].[display],[zstblwhatif].[name],'skip') AS description, tblRsIbomPoles.PartDesc, tblRsIbomPoles.Pole, tblRsIbomPoles.Level, tblRsIbomPoles.EndTime, tblRsIbomPoles.StartTime, tblRsIbomPoles.Slack, tblRsIbomPoles.FlowTime, tblRsIbomPoles.LTEquip, tblRsIbomPoles.LTLabor, tblRsIbomPoles.LTSetup, tblRsIbomPoles.LTRun, tblRsIbomPoles.LTWaitLot, tblRsIbomPoles.LTWaitAsm, tblRsIbomPoles.Wid FROM tblRsIbomPoles INNER JOIN zstblwhatif ON tblRsIbomPoles.Wid = zstblwhatif.WID "
             + " WHERE (((IIf([zstblwhatif].[display],[zstblwhatif].[name], '_skip'))<> '_skip')) ORDER BY tblRsIbomPoles.Wid DESC , tblRsIbomPoles.Count;";

        DbUse.open_ado_rec(globaldb, ref recpole, str1);


        if ((recpole.EOF)) {
            return; //exit  Sub;
        };

        recpole.MoveFirst();

        max_d = (double)recpole.Fields["EndTime"].Value;
        max_x = (float)max_d;
        if ((max_x < 0.00001)) {
            max_x = 1.0F;
        } else {
            max_x = FULL_SCALE(max_x);
        };



        Count = 0;
        recpole.MoveFirst();
        while (!recpole.EOF) {
            Count = Count + 1;
            recpole.MoveNext();
        };  //  end while Loop;


        //height = 300 + 150 * Count;
        height = 300 + 75 * Count;

        System.Drawing.Pen pen_r = new System.Drawing.Pen(Colors.Red, 15);
        System.Drawing.Pen pen_w = new System.Drawing.Pen(Color.Black, 3);
        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);

        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.Clear(Color.White);

        recpole.MoveFirst();
        str1 = (string)recpole.Fields["partdesc"].Value;
        str1 = str1.Trim();
        g.DrawString("Product: " + str1, new Font("Arial", 16, FontStyle.Regular), SystemBrushes.WindowText, new PointF(10, 10));

        X = 120;
        Y = 30;


        Y = Y + 30;
        g.DrawString("Time spent waiting for: ", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));


        Y = Y + 30;
        X = 150;
        g.DrawString("Equipment", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Labor", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Orange;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Setup", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Yellow;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Run", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Green;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Wait for Lot", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Magenta;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Out of Area", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Cyan;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        Y = 170;
        g.DrawLine(pen_w, 120, Y, 1350, Y);

        max_line = 1200 - 120;

        g.DrawLine(pen_w, 1350 - 20, Y - 10, 1350 + 1, Y);
        g.DrawLine(pen_w, 1350 - 20, Y + 10, 1350 + 1, Y);


        X = 120;
        g.DrawString("0.0", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 4;
        str1 = Convert.ToString(max_x / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 2;
        str1 = Convert.ToString(max_x / 2);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line * 3 / 4;
        str1 = Convert.ToString(max_x * 3 / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line;
        str1 = Convert.ToString(max_x);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);



        //-------------------------------------------------

        X = 120;
        par_x = X; //start of component;
        par_y = Y;

        recpole.MoveFirst();
        while (!recpole.EOF) {
            if (glngwid == (int)recpole.Fields["wid"].Value) {

                str1 = (string)recpole.Fields["partdesc"].Value;
                str1 = str1.Trim();
                if (str1.Length == 0) {
                    Y = Y + 45;
                    g.DrawLine(pen_w, 100, Y, max_line, Y);
                } else {
                    str2 = (string)recpole.Fields["partdesc"].Value;
                    if (str2.IndexOf("--- TOTAL ---") >= 0) {
                        Y = Y + 15;
                    } else {

                        // 'add product name;
                        Y = Y + 40;  // was 20;
                        X = 120 + (int)(((double)recpole.Fields["Slack"].Value / max_x) * max_line);
                        strflow = dig_round((double)recpole.Fields["flowtime"].Value, 4);
                        str1 = (string)recpole.Fields["partdesc"].Value;
                        str2 = str1.Trim() + " - " + strflow + " " + get_lead_time_unit() + "(s)";
                        g.DrawString(str2, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X + 50, Y));



                        //' add color bars;
                        Y = Y + 40;

                        //'in place of current add arrows;
                        str1 = (string)recpole.Fields["partdesc"].Value;
                        c_level = (int)((short)recpole.Fields["Level"].Value);

                        if (c_level > 0) {
                            g.DrawLine(pen_w, par_x, par_y, par_x, Y);
                        }


                        //'ADD ENTITIES++++++++++++++;
                        sizey = 1;
                        val1 = (double)recpole.Fields["LTEquip"].Value / max_x;  // gwwd
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);  // gwwd
                        sizex = (int)(val1 * max_line);  // gwwd
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Red;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };
                        val1 = (double)recpole.Fields["LTLabor"].Value / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Orange;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };

                        val1 = (double)recpole.Fields["LTSetup"].Value / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Yellow;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };
                        val1 = (double)recpole.Fields["LTRun"].Value / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Green;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };
                        val1 = (double)recpole.Fields["LTWaitLot"].Value / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Magenta;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };

                        val1 = (double)recpole.Fields["LTWaitasm"].Value / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Cyan;
                            g.DrawRectangle(pen_r, X, Y, sizex, 15);
                            X = X + sizex;
                        };

                        val1 = ((double)recpole.Fields["flowtime"].Value) / max_x;
                        if (val1 > 0.0)
                            val1 = Math.Max(minlen, val1);
                        sizex = (int)(val1 * max_line);
                        if ((sizex > 0.01)) {
                            pen_r.Color = Colors.Magenta;
                            g.DrawRectangle(pen_r, X, Y, 1, 15);
                            X = X + 1;
                        };

                        par_x = X; //start of component;
                        par_y = Y;


                    };  //end else
                };  //end else
            };  //end if glngwid

            recpole.MoveNext();

        };  //  end while loop 




        //-------------------------------------------------

        Y = Y + 50;
        g.DrawLine(pen_w, 120, Y, 1350, Y);

        max_line = 1200 - 120;


        g.DrawLine(pen_w, 1350 - 20, Y - 10, 1350 + 1, Y);
        g.DrawLine(pen_w, 1350 - 20, Y + 10, 1350 + 1, Y);


        X = 120;
        g.DrawString("0.0", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 30 + max_line / 4;
        str1 = Convert.ToString(max_x / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 30 + max_line / 2;
        str1 = Convert.ToString(max_x / 2);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 30 + max_line * 3 / 4;
        str1 = Convert.ToString(max_x * 3 / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 30 + max_line;
        str1 = Convert.ToString(max_x);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);



        X = 150;
        Y = Y + 40;
        g.DrawString("Time spent waiting for: ", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));


        Y = Y + 20;
        pen_r.Color = Colors.Red;
        g.DrawString("Equipment", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Labor", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Orange;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Setup", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Yellow;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Run", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Green;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Wait for Lot", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Magenta;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Out of Area", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Cyan;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;




        str1 = Directory.GetCurrentDirectory();

        bmp.Save(localdir + "Graphs//" + MyUtilities.POLES_IMG_NAME, ImageFormat.Png);

        g.Dispose();
        bmp.Dispose();

        return;

    }




    public void place_tree(int partid) {
        //on error GoTo err_place_all_trees;

        ADODB.Recordset rectree = null;
        int X;
        int Y;
        float max_y;
        string name1;
        string id0;
        string id_bar;
        string id1;
        string id2;
        int sizey;
        int sizex;
        float lenx;
        int Count;
        int extraspace;
        string str1;
        int i;
        int max_line;
        float max_x;
        int Y1;

        double val1; // gwwd



        // 'xxxxxxxxxxxxxxxxxxxxxxxxxxxx;
        int c_level;
        int previous_level;
        string id_left;
        string id_left2;
        string strflow;
        string curdir;

        int X1;
        int Y2;



        //----------------------------------------------


        if ((partid <= 0)) {
            return; //exit  Sub;
        };


        Count = 0;

        DbUse.open_ado_rec(globaldb, ref rectree, "qfltrsIbomTree_g");

        if ((rectree.EOF)) {
            return; //exit  Sub;
        };


        rectree.MoveFirst();
        Count = 0;
        while (!rectree.EOF) {
            Count = Count + 1;
            rectree.MoveNext();
        }; //  while end


        rectree.MoveFirst();
        max_x = (float)((double)rectree.Fields["endtime"].Value);
        if ((max_x < 0.00001)) {
            max_x = 1.0F;
        } else {
            max_x = FULL_SCALE(max_x);
        };


        depth = Count;

        Array.Resize(ref parentX, depth);
        Array.Resize(ref parentY, depth);
        Array.Resize(ref compX, depth);
        Array.Resize(ref compY, depth);


        //int width = 2000;
        int width = 1500;
        //int height = 300 + 150 * Count;
        int height = 350 + 120 * Count;


        System.Drawing.Pen pen_r = new System.Drawing.Pen(Colors.Red, 15);
        System.Drawing.Pen pen_w = new System.Drawing.Pen(Color.Black, 3);
        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(bmp);

        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.Clear(Color.White);

        rectree.MoveFirst();
        str1 = (string)rectree.Fields["partdesc"].Value;
        str1 = str1.Trim();
        g.DrawString("Product: " + str1, new Font("Arial", 16, FontStyle.Regular), SystemBrushes.WindowText, new PointF(10, 10));

        X = 120;
        Y = 30;


        Y = Y + 30;
        g.DrawString("Time spent waiting for: ", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));


        Y = Y + 30;
        X = 150;
        g.DrawString("Equipment", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Labor", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Orange;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Setup", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Yellow;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Run", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Green;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;

        g.DrawString("Wait for Lot", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Magenta;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;


        g.DrawString("Out of Area", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Cyan;
        g.DrawRectangle(pen_r, X + 10, Y + 25, 50, 15);
        X = X + 150;



        Y = 170;
        g.DrawLine(pen_w, 120, Y, 1350, Y);

        max_line = 1200 - 120;

        g.DrawLine(pen_w, 1350 - 20, Y - 10, 1350 + 1, Y);
        g.DrawLine(pen_w, 1350 - 20, Y + 10, 1350 + 1, Y);


        X = 120;
        g.DrawString("0.0", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 4;
        str1 = Convert.ToString(max_x / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 2;
        str1 = Convert.ToString(max_x / 2);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line * 3 / 4;
        str1 = Convert.ToString(max_x * 3 / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line;
        str1 = Convert.ToString(max_x);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);



        //-------------------------------------------------


        X = 120;
        parentX[0] = X; //start of component;
        parentY[0] = Y;



        while (!(rectree.EOF)) {

            if ((glngwid == (int)rectree.Fields["wid"].Value)) {

                Count = Count + 1;

                // 'add product name;
                Y = Y + 50;

                X = (int)(120 + ((double)rectree.Fields["Slack"].Value / max_x) * max_line);
                strflow = dig_round((double)rectree.Fields["flowtime"].Value, 4);
                str1 = (string)rectree.Fields["partdesc"].Value;
                name1 = str1.Trim() + " - " + strflow + " " + get_lead_time_unit() + "(s)";
                g.DrawString(name1, new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X + 15, Y));

                //' add color bars;
                Y = Y + 30;




                c_level = (int)rectree.Fields["Level"].Value;


                sizey = 1;
                //' add vertical lines;
                if (c_level >= 1) {
                    compY[c_level - 1] = Y;
                    X = parentX[c_level - 1];
                } else {
                    X = 120;
                };


                Y = Y + 10;
                //'ADD ENTITIES++++++++++++++;
                val1 = (double)rectree.Fields["LTEquip"].Value / max_x;  // gwwd
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);                // gwwd
                sizex = (int)(val1 * max_line);                          // gwwd
                if ((sizex > 0.01)) {
                    //' ADDING EQUIP BOX;

                    pen_r.Color = Colors.Red;
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                val1 = (double)rectree.Fields["LTLabor"].Value / max_x;
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);
                sizex = (int)(val1 * max_line);

                if ((sizex > 0.01)) {
                    // ' ADDING labor BOX;
                    pen_r.Color = Colors.Orange;
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                val1 = (double)rectree.Fields["LTSetup"].Value / max_x;
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);
                sizex = (int)(val1 * max_line);
                if ((sizex > 0.01)) {
                    pen_r.Color = Colors.Yellow;
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                val1 = (double)rectree.Fields["LTRun"].Value / max_x;
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);
                sizex = (int)(val1 * max_line);
                if ((sizex > 0.01)) {
                    pen_r.Color = Colors.Green;
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                val1 = (double)rectree.Fields["LTWaitLot"].Value / max_x;
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);
                sizex = (int)(val1 * max_line);
                if ((sizex > 0.01)) {
                    pen_r.Color = Colors.Magenta;
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                val1 = (double)rectree.Fields["LTWaitasm"].Value / max_x;
                if (val1 > 0.0)
                    val1 = Math.Max(minlen, val1);
                sizex = (int)(val1 * max_line);
                if ((sizex > 0.01)) {
                    g.DrawRectangle(pen_r, X, Y, sizex, 15);
                    X = X + sizex;
                };

                if ((double)rectree.Fields["flowtime"].Value == 0) {
                    g.DrawRectangle(pen_w, X, Y, 3, 15);
                };

                //--------------------------------------------------------------------------



                //' add vertical lines;
                if (c_level >= 1) {
                    X1 = parentX[c_level - 1];
                    Y1 = parentY[c_level - 1];
                    Y2 = compY[c_level - 1];
                    g.DrawLine(pen_w, X1 - 5, Y1, X1, Y1);
                    g.DrawLine(pen_w, X1, Y1, X1, Y2 + 15);
                    g.DrawLine(pen_w, X1, Y2 + 15, X1 + 5, Y2 + 15);
                };

                parentX[c_level] = X; //start of component;
                parentY[c_level] = Y + 5;




            };  //end if glngwid



            rectree.MoveNext();

        };  //  end while loop 




        //-------------------------------------------------

        Y = Y + 50;
        g.DrawLine(pen_w, 120, Y, 1350, Y);

        max_line = 1200 - 120;


        g.DrawLine(pen_w, 1350 - 20, Y - 10, 1350 + 1, Y);
        g.DrawLine(pen_w, 1350 - 20, Y + 10, 1350 + 1, Y);


        X = 120;
        g.DrawString("0.0", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 4;
        str1 = Convert.ToString(max_x / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line / 2;
        str1 = Convert.ToString(max_x / 2);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line * 3 / 4;
        str1 = Convert.ToString(max_x * 3 / 4);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);

        X = 120 + max_line;
        str1 = Convert.ToString(max_x);
        g.DrawString(str1, new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X - 10, Y + 15));
        g.DrawLine(pen_w, X, Y - 15, X, Y + 15);



        X = 150;
        Y = Y + 40;
        g.DrawString("Time spent waiting for: ", new Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));


        Y = Y + 20;
        pen_r.Color = Colors.Red;
        g.DrawString("Equipment", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;

        g.DrawString("Labor", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Orange;
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;

        g.DrawString("Setup", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Yellow;
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;


        g.DrawString("Run", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Green;
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;

        g.DrawString("Wait for Lot", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Magenta;
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;


        g.DrawString("Out of Area", new Font("Arial", 14, FontStyle.Regular), SystemBrushes.WindowText, new PointF(X, Y));
        pen_r.Color = Colors.Cyan;
        g.DrawRectangle(pen_r, X + 10, Y + 35, 50, 15);
        X = X + 150;




        str1 = Directory.GetCurrentDirectory();
        bmp.Save(localdir + "Graphs//" + MyUtilities.TREES_IMG_NAME, ImageFormat.Png);

        g.Dispose();
        bmp.Dispose();

        return;

    }


    void testtreespoles() {

        //----------------------------------------------
        m_makeResultsPoles(1);
        place_poles(1);
        m_makeResultsTree(2);
        place_tree(2);

        return;
    }



}  //End Class;
