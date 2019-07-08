
using System;


public partial class ClassC : ClassB
{


   // void clean_ops_route_quick(int prodid);
   // int find_nameItem(string name1, int item2, short item_type, int Count);


   
    string msgLoop;  //  global...


    public void del_nulls() {


        string str1;
        str1 = "UPDATE tblOper SET tblOper.PercentAssign = '100'  WHERE (((tblOper.PercentAssign) Is Null) OR tblOper.PercentAssign = ''); ";
        runsqlado(str1);

        str1 = "UPDATE tblOper SET tblOper.labsetuptime = '0'  WHERE (((tblOper.labsetuptime) Is Null) OR tblOper.labsetuptime = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.labruntime = '0'  WHERE (((tblOper.labruntime) Is Null) OR tblOper.labruntime = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.labrunlot = '0'  WHERE (((tblOper.labrunlot) Is Null) OR tblOper.labrunlot = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.labsetuptbatch = '0'  WHERE (((tblOper.labsetuptbatch) Is Null) OR tblOper.labsetuptbatch = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.labsetuppiece = '0'  WHERE (((tblOper.labsetuppiece) Is Null) OR tblOper.labsetuppiece = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.labruntbatch = '0'  WHERE (((tblOper.labruntbatch) Is Null) OR tblOper.labruntbatch = ''); ";
        runsqlado(str1);

        str1 = "UPDATE tblOper SET tblOper.eqsetuptime = '0'  WHERE (((tblOper.eqsetuptime) Is Null) OR tblOper.eqsetuptime = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.eqruntime = '0'  WHERE (((tblOper.eqruntime) Is Null) OR tblOper.eqruntime = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.eqrunlot = '0'  WHERE (((tblOper.eqrunlot) Is Null) OR tblOper.eqrunlot = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.eqsetuptbatch = '0'  WHERE (((tblOper.eqsetuptbatch) Is Null) OR tblOper.eqsetuptbatch = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.eqsetuppiece = '0'  WHERE (((tblOper.eqsetuppiece) Is Null) OR tblOper.eqsetuppiece = ''); ";
        runsqlado(str1);
        str1 = "UPDATE tblOper SET tblOper.eqruntbatch = '0'  WHERE (((tblOper.eqruntbatch) Is Null) OR tblOper.eqruntbatch = ''); ";
        runsqlado(str1);

        str1 = "UPDATE tblOperFrTo SET tblOperFrTo.Per = '100'  WHERE (((tblOperFrTo.Per) Is Null) OR tblOperFrTo.Per = ''); ";
        runsqlado(str1);


    }

    public short check_for_null(string str1, string str2, ADODB.Recordset recerror) {


        string strtable1;
        string strmsg;
        short fError;


        ADODB.Recordset recoper = null;


        strtable1 = "SELECT tblOper.OpID, tblOper.ProdFore, tblOper.OpNam, tblOper.OpNum,  tblProdFore.ProdDesc AS aa1 FROM tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID WHERE (((tblOper." + str1 + " ) Is Null) OR tblOper." + str1 + "  = ''); ";
        DbUse.open_ado_rec(globaldb, ref recoper, strtable1);

        fError = 0;

        while (!recoper.EOF) {
            strmsg = "Operation Data Error -- ";
            strmsg = strmsg + "Operation Must have a " + str2;
            strmsg = strmsg + "Part: " + recoper.Fields["aa1"].Value + "  ";
            strmsg = strmsg + "Operation: " + recoper.Fields["opnam"].Value + "  ";
            strmsg = strmsg + "does not have a percent assigned value.";
            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Operation Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;
            recoper.MoveNext();
        }; // end while
        DbUse.CloseAdoRec(recoper);

        return (fError);
    } 


    void CheckEquip()
    {

        //on error goto Err_CheckEquip;

        //Define variables;

        ADODB.Recordset recEquip = null;
        ADODB.Recordset recLabor = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        int l_none_id;

        short fexit;

        //Default varaibles;
        strtable1 = "tblEquip";
        strtable2 = "tbllabor";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable1);
        DbUse.open_ado_rec(globaldb, ref recLabor, strtable2);

        strCriteria = "[LaborDesc] = '" + "NONE" + "'";
        recLabor.Filter = strCriteria;                         
        l_none_id = (int)recLabor.Fields["LaborID"].Value;

        recEquip.MoveFirst();

        //Loop through all the Equip Records to Validate them.;
        //First make sure each record has a valid labor Name or NULL, If the record does;
        //not have a valid labor name then assign NONE as LABOR ID.;


        while (!recEquip.EOF)
        {


            //Search the Labor Table for a matching;
            //Labor ID;
            strCriteria = "[LaborDesc] = '" + recEquip.Fields["labordesc"].Value + "'";
            recLabor.Filter = strCriteria;                                                

            //Test search results;
            if (recLabor.EOF)
            {
                recEquip.Fields["Labor"].Value = l_none_id;
            }
            else
            {
                recEquip.Fields["Labor"].Value = recLabor.Fields["LaborID"].Value;
            };

            recEquip.Update();
            recEquip.MoveNext();

        };  //end while



        DbUse.CloseAdoRec(recEquip);
        recEquip = null;
        DbUse.CloseAdoRec(recLabor);
        recLabor = null;

        // return; //exit _CheckEquip:
        return; //exit  Sub;

//err_CheckEquip: ;

        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume   return; //exit _CheckEquip;

    } // end sub;

    void CheckIbom()
    {

        //on error goto Err_CheckIbom;

        //Define variables;

        ADODB.Recordset recIbom = null;
        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recIbomreal = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        int prodid;
        int compid;
        string strmsg;
        short fexit;

        //Default varaibles;
        strtable1 = "tblIbomImport";
        strtable2 = "tblProdFore";
        strTAble3 = "tblIbom";

        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recIbom, strtable1);
        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable2);
        DbUse.open_ado_rec(globaldb, ref recIbomreal, strTAble3);

        if (recIbom.BOF) { goto exit_checkIbom; }

        recIbom.MoveFirst();

        //Loop through all the Ibom Records to Validate them.;
        //First make sure each record has a valid Product IDs, If the record does;
        //not have a valid Product ID then nodo not add IBOM real record.;
        //Also insure that UPA >= 0.;


        while (!recIbom.EOF)
        {
            //Search the Product Forecast Table for a matching;
            //Product ID;
            strCriteria = "[ProdDesc] = '" + recIbom.Fields["[ParentID]"].Value + "'";
            recProdFore.Filter = strCriteria;                                          


            //Test search results;
            if (recProdFore.EOF)
            {
                recIbom.Delete();
            }
            else
            {
                prodid = (int)recProdFore.Fields["prodid"].Value;
                strCriteria = "[ProdDesc] = '" + (string)recIbom.Fields["[compid]"].Value + "'";
                recProdFore.Filter = strCriteria;                                                    
                if (recProdFore.EOF)
                {
                    recIbom.Delete();
                }
                else
                {
                    compid = (int)recProdFore.Fields["prodid"].Value;
                    //Test UPA value;

                    if (((float)recIbom.Fields["UPA"].Value) < 0)
                    {
                        recIbom.Fields["UPA"].Value = 0;
                    };

                    // add the record to the real table;
                    recIbomreal.AddNew();
                    recIbomreal.Fields["ParentID"].Value = prodid;
                    recIbomreal.Fields["compid"].Value = compid;
                    recIbomreal.Fields["UPA"].Value = recIbom.Fields["UPA"].Value;
                    recIbomreal.Update();

                };
            };

            recIbom.MoveNext();

        }; // end while

exit_checkIbom:

        DbUse.CloseAdoRec(recIbom);
        recIbom = null;
        DbUse.CloseAdoRec(recProdFore);
        recProdFore = null;
        DbUse.CloseAdoRec(recIbomreal);
        recIbomreal = null;



        return; //exit  Sub;

//err_CheckIbom: ;
        /*   if ((Err.Number  == 3420))   {
                return; //exit  Sub;
           } else {
              //' msgbox(ErrorToString(), 0, appl_name);
              //' resume  exit_checkIbom;
               };
         * */


    } // end sub;

    void CheckOper()
    {

        //on error goto Err_CheckOper;

        //Define variables;

        ADODB.Recordset recoper = null;
        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recEquip = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        int ProdForeId;
        int l_none_id;
        int oldprod;
        int opnumber;

        short fexit;

        //Default varaibles;

        strtable1 = "SELECT DISTINCTROW tblOper.* FROM tblOper ORDER BY tblOper.OpID;";
        strtable2 = "tblProdFore";
        strTAble3 = "tblEquip";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recoper, strtable1);
        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable2);
        DbUse.open_ado_rec(globaldb, ref recEquip, strTAble3);


        recoper.MoveFirst();

        //Loop through all the Oper Records to Validate them.;
        //First make sure each record has a valid ProdFore Name, If the record does;
        //not have a valid ProdFore name then delete it.;

        strCriteria = "[EquipDesc] = '" + "NONE" + "'";
        recEquip.Filter = strCriteria;
        l_none_id = (int)recEquip.Fields["equipid"].Value;        


        oldprod = 0;
        opnumber = 10;


        while (!recoper.EOF)
        {
            //Search the ProdFore Table for a matching;
            //ProdFore ID;
            strCriteria = "[ProdDesc] = '" + recoper.Fields["proddesc"].Value + "'";
            recProdFore.Filter = strCriteria;                                             

            if (((int)recProdFore.Fields["prodid"].Value) != oldprod)
            {
                oldprod = (int)recProdFore.Fields["prodid"].Value;
                opnumber = 10;
            };



            recoper.Update();


            recoper.MoveNext();


        }; // end while




        DbUse.CloseAdoRec(recoper);
        recoper = null;
        DbUse.CloseAdoRec(recProdFore);
        recProdFore = null;
        DbUse.CloseAdoRec(recEquip);
        recEquip = null;

        //exit_checkOper:
        return; //exit  Sub;

//err_CheckOper: ;

        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume  exit_checkOper;

    } // end sub;


    void CheckOpFrTo()
    {

        //on error goto Err_CheckOpFrTo;

        //Define variables;

        ADODB.Recordset recOpFrTo = null;
        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recoper = null;
        ADODB.Recordset recOpFrToReal = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        string strTAble4;
        int prodid;
        int OperFID;
        int OperTId;




        //Default variables;
        strtable1 = "tblOpFrToImport";
        strtable2 = "tblProdFore";
        strTAble3 = "tblOper";
        strTAble4 = "tblOperFrTo";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recOpFrTo, strtable1);
        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable2);
        DbUse.open_ado_rec(globaldb, ref recoper, strTAble3);
        DbUse.open_ado_rec(globaldb, ref recOpFrToReal, strTAble4);


        recOpFrTo.MoveFirst();

        //Loop through all the OpFrTo Records to Validate them.;
        //First make sure each record has a valid ProdFore Name, If the record does;
        //not have a valid ProdFore name then delete it.;
        //  { check if From oper and to opers exist. If not delete record;
        // else {


        while (!recOpFrTo.EOF)
        {
            //Search the ProdFore Table for a matching;
            //ProdFore ID;
            strCriteria = "[ProdDesc] = '" + recOpFrTo.Fields["proddesc"].Value + "'";
            recProdFore.Filter = strCriteria;                                             


            //Test search results;
            if (recProdFore.EOF)
            {
                //recOpFrTo.Delete() 'don't felete ???;
            }
            else
            {
                prodid = (int)recProdFore.Fields["prodid"].Value;
                strCriteria = "[OpNam] = '"  + recOpFrTo.Fields["fromoper"].Value + "'";
                strCriteria = strCriteria + " and [prodFore] = " + prodid;
                recoper.Filter = strCriteria;                                  
                if (recoper.EOF)
                {
                    recOpFrTo.Delete();
                }
                else
                {
                    OperFID = (int)recoper.Fields["opid"].Value;
                    strCriteria = "[OpNam] = '" + recOpFrTo.Fields["tooper"].Value + "'";                    
                    strCriteria = strCriteria + " and [prodFore] = " + prodid;
                    recoper.Filter = strCriteria;                                           
                    if (recoper.EOF)
                    {
                        recOpFrTo.Delete();
                    }
                    else
                    {
                        OperTId = (int)recoper.Fields["opid"].Value;
                        recOpFrToReal.AddNew();
                        recOpFrToReal.Fields["PartFore"].Value = prodid;
                        recOpFrToReal.Fields["OpNumF"].Value = OperFID;
                        recOpFrToReal.Fields["OpNumT"].Value = OperTId;
                        recOpFrToReal.Fields["per"].Value = recOpFrTo.Fields["Percent"].Value;
                        recOpFrToReal.Update();
                    };
                };
            };

            recOpFrTo.MoveNext();


        }; // end while



        DbUse.CloseAdoRec(recOpFrTo);
        recOpFrTo = null;
        DbUse.CloseAdoRec(recProdFore);
        recProdFore = null;
        DbUse.CloseAdoRec(recoper);
        recoper = null;
        DbUse.CloseAdoRec(recOpFrToReal);
        recOpFrToReal = null;

        // return; //exit _CheckOpFrTo:
        return; //exit  Sub;

//err_CheckOpFrTo: ;
        /* if ((Err.Number  == 91))   {
              return; //exit  Sub;
         } else {
            //' msgbox(ErrorToString(), 0, appl_name);
              //RESUME NEXT
             };
     */

    } // end sub;

    void xxCheckWhatif()  //check whatif on load from external table ???
    {

        //on error goto Err_CheckWhatif;

        //Define variables;

        ADODB.Recordset recwhatif = null;
        ADODB.Recordset recWhatifrecs = null;
        ADODB.Recordset recwhatifImport = null;
        ADODB.Recordset rec1 = null;

        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        string rec;
        string part;
        short mtype;
        int recid;
        int partid;
        short icount;



        //Default varaibles;
        strtable1 = "tblWhatif";
        strtable2 = "tblWhatifaudit";
        strTAble3 = "tblwhatifImport";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recwhatifImport, strTAble3);
        DbUse.open_ado_rec(globaldb, ref recWhatifrecs, strtable2);
        DbUse.open_ado_rec(globaldb, ref recwhatif, strtable1);


        if ((recwhatifImport.BOF))
        {
            goto exit_checkWhatif;
        };

        recwhatifImport.MoveFirst();

        //Loop through all the audit Records to Validate them.;
        //First make sure each record has a valid whatif Name or NULL, If the record does;
        //not have a valid whatif name then delete it.;


        while (!recwhatifImport.EOF)
        {

            recWhatifrecs.AddNew();

            //Search the Whatif table for a matching name;
            strCriteria = "[Name] = '" + recwhatifImport.Fields["Whatif_name"].Value + "'";
            recwhatif.Filter = strCriteria;                                                 

            //Test search results;
            if (!(recwhatif.EOF))
            {
                recWhatifrecs.Fields["wid"].Value = recwhatif.Fields["wid"].Value;

                recWhatifrecs.Fields["TABLEE"].Value = recwhatifImport.Fields["TABLEE"].Value;
                recWhatifrecs.Fields["tablea"].Value = recwhatifImport.Fields["tablea"].Value;
                recWhatifrecs.Fields["FIELDe"].Value = recwhatifImport.Fields["FIELDe"].Value;
                recWhatifrecs.Fields["fielda"].Value = recwhatifImport.Fields["fielda"].Value;
                recWhatifrecs.Fields["newval"].Value = recwhatifImport.Fields["newval"].Value;
                recWhatifrecs.Fields["OLDval"].Value = recwhatifImport.Fields["OLDval"].Value;
                recWhatifrecs.Fields["DataType"].Value = recwhatifImport.Fields["DataType"].Value;

                recWhatifrecs.Fields["newshow"].Value = recwhatifImport.Fields["newval"].Value;
                recWhatifrecs.Fields["oldshow"].Value = recwhatifImport.Fields["OLDval"].Value;

                mtype = (short)recwhatifImport.Fields["mtype"].Value;
                rec = (string)recwhatifImport.Fields["I_name"].Value;
                part = (string)recwhatifImport.Fields["p_name"].Value;

                switch (mtype)
                {

                    case (0):
                        {  //labor
                            recid = find_nameItem(rec, 0, mtype, 0);
                            recWhatifrecs.Fields["recORDid"].Value = recid;

                        } break;
                    case (1):
                        {   //equip 
                            recid = find_nameItem(rec, 0, mtype, 0);
                            recWhatifrecs.Fields["recORDid"].Value = recid;
                            if (((string)recWhatifrecs.Fields["fielda"].Value == "labor"))
                            {

                                recWhatifrecs.Fields["OLDval"].Value = find_nameItem((string)recWhatifrecs.Fields["OLDval"].Value, 0, Labor, 0);
                                recWhatifrecs.Fields["newval"].Value = find_nameItem((string)recWhatifrecs.Fields["newval"].Value, 0, Labor, 0);
                            };
                        } break;
                    case (2):
                        {   //product
                            recid = find_nameItem(rec, 0, mtype, 0);
                            recWhatifrecs.Fields["recORDid"].Value = recid;
                        } break;
                    case (3):
                        {  //  oper
                            partid = find_nameItem(part, 0, product, 0);
                            icount = (short)recwhatifImport.Fields["Count"].Value;

                            recid = find_nameItem(rec, partid, oper, icount);
                            recWhatifrecs.Fields["recORDid"].Value = recid;
                            if (((string)recWhatifrecs.Fields["fielda"].Value == "eqid"))
                            {
                                recWhatifrecs.Fields["OLDval"].Value = find_nameItem((string)recWhatifrecs.Fields["OLDval"].Value, 0, equip, 0);
                                recWhatifrecs.Fields["newval"].Value = find_nameItem((string)recWhatifrecs.Fields["newval"].Value, 0, equip, 0);
                            };
                            //;

                        } break;
                    case (4):
                        { //route

                            partid = find_nameItem(part, 0, product, 0);
                            icount = (short)recwhatifImport.Fields["Count"].Value;
                        } break;
                    case (5):
                        {  //ibom
                            partid = find_nameItem(part, 0, product, 0);
                            recid = find_nameItem(rec, partid, ibom, 0);
                            recWhatifrecs.Fields["recORDid"].Value = recid;
                            //;
                        } break;
                    case (6):
                        {  //general
                            DbUse.open_ado_rec(globaldb, ref rec1, "tblgeneral");
                            recid = (int)rec1.Fields["generalid"].Value;
                            recWhatifrecs.Fields["recORDid"].Value = recid;
                        } break;
                };  //'' end of switch;

                recWhatifrecs.Update();
            };  //  end if

            recwhatifImport.MoveNext();

        }; // end while

        if (null != recwhatif)
        {
            DbUse.CloseAdoRec(recwhatif);
            recwhatif = null;
        };
        if (null != recWhatifrecs)
        {
            DbUse.CloseAdoRec(recWhatifrecs);
            recWhatifrecs = null;
        };
        if (null != recwhatifImport)
        {
            DbUse.CloseAdoRec(recwhatifImport);
            recwhatifImport = null;
        };

        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };

exit_checkWhatif:

        return; //exit  Sub;

err_CheckWhatif: ;
        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume  exit_checkWhatif;

    } // end sub;

    

    protected short ValidateEquipGroup(ref short fError, ref short fWarn)
    {
        //on error goto Err_ValidateEquipGroup;

        //Define variables;

        ADODB.Recordset recEquip = null;
        ADODB.Recordset recLab = null;
        ADODB.Recordset recerror = null;

        int labid;
        string strtable;
        string strtable2;
        string strmsg;
        short fexit;
        short retval;

        short i;


        //Default variables;
        fexit = 0;
        fError = 0;
        fWarn = 0;
        strtable = "tblEquip";
        strtable2 = "zstblErrors";

        //Open();
        //Set variables;
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable);
        DbUse.open_ado_rec(globaldb, ref recerror, strtable2);


        recEquip.Filter = "[equipdesc] = 'NONE'";     
        if ((recEquip.EOF))
        {
            recEquip.AddNew();
            recEquip.Fields["equipdesc"].Value = "NONE";
            recEquip.Fields["grpsiz"].Value = "-1";
            recEquip.Fields["EquipType"].Value = 1;
            recEquip.Fields["MTF"].Value = 1;
            recEquip.Fields["Labor"].Value = find_nameItem("none", 0, Labor, 0);

            recEquip.Update();
        };

      

        strtable = "SELECT DISTINCTROW tblequip.* FROM tblequip WHERE ((tblequip.GrpSiz<>-1) and (tblequip.GrpSiz<=0)) order by tblequip.equipid;";
        
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable);

  
        //Loop through all records and test for number of;
        //people in the equip group, if less than one generate;
        //an error.;
        while (!recEquip.EOF)
        {
            strmsg = "Equipment Group Size Less Than 1 -- ";
            strmsg = strmsg + "Equipment Group: " + recEquip.Fields["equipdesc"].Value + "  ";
            strmsg = strmsg + "Group Size Must Be -1, or Greater Than 0";
            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Equipment Group Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;
            recEquip.MoveNext();
        }; // end while


     
        strtable = "SELECT tblequip.* FROM tblequip  ;";
        
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable);

        //Loop through all records and test for assigned labor;
        //if no assignement generate an error.;

        while (!recEquip.EOF) {
            labid = Convert.ToInt32(recEquip.Fields["labor"].Value);
            strtable = "SELECT tbllabor.* FROM tbllabor WHERE tbllabor.laborid = "  + labid + " ;";

            DbUse.open_ado_rec(globaldb, ref recLab, strtable);


            if (recLab.EOF) {
                strmsg = "No assigned Labor for an Equipment Group ";
                strmsg = strmsg + "Equipment Group: " + recEquip.Fields["equipdesc"].Value + "  ";
                recerror.AddNew();
                recerror.Fields["Level"].Value = ERROR_FLAG;
                calc_return |= ERR_FLAG;
                recerror.Fields["Table"].Value = "Equipment Group Table";
                recerror.Fields["Text"].Value = strmsg;
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
                fError = -1;
            }
            recEquip.MoveNext();
        }; // end while





        if ((fError == -1) || (fWarn == -1))
        {
            retval = 0;
        }
        else
        {
            retval = -1;
        };


        fexit = -1;
        DbUse.CloseAdoRec(recEquip);
        DbUse.CloseAdoRec(recerror);
        DbUse.CloseAdoRec(recLab);
        

        // return; //exit _ValidateEquipGroup:
        return retval; //exit  Function;

//err_ValidateEquipGroup: ;
        // if (fexit == )   {
        //      //RESUME NEXT
        //     };

        /*   if (Err.Number  == 3021)   {
              //' resume   return; //exit _ValidateEquipGroup;
           } else {
              //' msgbox(ErrorToString(), 0, appl_name);
                //RESUME NEXT
               };
         * 
         * */
    } // end function;

    protected short ValidateIbom(short fError, short fWarn)
    {
        //on error goto Err_ValidateIbom;

        //Define variables;

        ADODB.Recordset recIbom = null;
        ADODB.Recordset recerror = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        string strProdName;
        string strmsg;
        short fexit;
        short retval;

        //Default varaibles;
        fexit = 0;
        fError = 0;
        fWarn = 0;
        strProdName = "";
        strtable1 = "tblIbom";
        strtable2 = "tblProdFore";
        strTAble3 = "zstblErrors";


        
        
        runsqlado("Update tblibom SET tblibom.flag = 0;");
        runsqlado("Update (tblibom INNER JOIN tblprodfore ON tblibom.ParentID = tblprodfore.ProdID) INNER JOIN tblprodfore AS tblprodfore_1 ON tblibom.CompID = tblprodfore_1.ProdID SET tblibom.flag = 1;");


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recerror, strTAble3);

        strtable1 = "SELECT DISTINCTROW tblibom.* FROM tblibom WHERE (tblibom.flag=0);";
        DbUse.open_ado_rec(globaldb, ref recIbom, strtable1);

        while (!recIbom.EOF)
        {
            recIbom.Delete();
            recIbom.MoveNext();
        }; // end while



        runsqlado("Update tblibom SET tblibom.flag = 0;");
        runsqlado("Update tblibom SET tblibom.flag = 1 WHERE (tblibom.UPA>=0);");

        strtable1 = "SELECT DISTINCTROW tblibom.flag, tblprodfore.ProdDesc AS PARENTDESC, tblprodfore_1.ProdDesc AS COMPDESC, tblibom.UPA FROM (tblibom INNER JOIN tblprodfore ON tblibom.ParentID = tblprodfore.ProdID) INNER JOIN tblprodfore AS tblprodfore_1 ON tblibom.CompID = tblprodfore_1.ProdID WHERE (tblibom.flag=0);";
        DbUse.open_ado_rec(globaldb, ref recIbom, strtable1); 

        //Loop through all the Ibom Records to Validate them.;
        //First make sure each record has a valid Product IDs, If the record does;
        //not have a valid Product ID then delete the Product Assignment record.;
        //Also insure that UPA >= 0.;

        while (!recIbom.EOF)
        {
            strmsg = "Invalid Units Per Assembly in IBOM record -- ";
            strmsg = strmsg + "Product: " + recIbom.Fields["PARENTDESC"].Value + "  ";
            strmsg = strmsg + "Component: " + recIbom.Fields["COMPDESC"].Value + "  ";
            strmsg = strmsg + "Has less than 0 units per assembly";

            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Indented Bill Of Materials Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;
            recIbom.MoveNext();
        }; // end while

        if (testLoop() != 0)
        {
            fError = -1;
        };


        //Test the Error and Warning Flags;
        if ((fError == -1) || (fWarn == -1))
        {
            retval = 0;
        }
        else
        {
            retval = -1;
        };

        fexit = -1;
        DbUse.CloseAdoRec(recIbom);
        DbUse.CloseAdoRec(recerror);

        // return; //exit _ValidateIbom:  ;
        return retval; //exit  Function;

err_ValidateIbom: ;

    } // end function;

    protected short ValidateLaborGroup(ref short fError, ref short fWarn)
    {
        //on error goto Err_ValidateLaborGroup;

        //Define variables;

        ADODB.Recordset recLabor = null;
        ADODB.Recordset recerror = null;
        string strtable;
        string strtable2;
        string strmsg;
        short fexit;
        short retval;

        int i;


        //Default variables;
        fexit = 0;
        fError = 0;
        fWarn = 0;
        strtable = "tblLabor";
        strtable2 = "zstblErrors";

        //Open();
        DbUse.open_ado_rec(globaldb, ref recLabor, "tblLabor");

        recLabor.Filter = "[labordesc] = 'none'";                    
        if ((recLabor.EOF))
        {
            recLabor.AddNew();
            recLabor.Fields["labordesc"].Value = "NONE";
            recLabor.Fields["grpsiz"].Value = "-1";
            recLabor.Update();
        };
        DbUse.CloseAdoRec(recLabor);
        

        runsqlado("Update tblLABOR SET tblLABOR.Flag = 0;");

        runsqlado("Update tblLABOR SET tblLABOR.Flag = 1 WHERE (tblLABOR.GrpSiz=-1 Or tblLABOR.GrpSiz>0);");

        strtable = "SELECT DISTINCTROW tblLABOR.* FROM tblLABOR WHERE (tblLABOR.Flag=0);";
        DbUse.open_ado_rec(globaldb, ref recLabor, strtable);
        //Set variables;
        //DbUse.open_ado_rec(globaldb, ref recerror, strtable2);

        //Loop through all records and test for number of;
        //people in the LABOR group, if less than one generate;
        //an error.;

        while (!recLabor.EOF)
        {

            strmsg = "Labor Group Size Less Than 1 -- ";
            strmsg = strmsg + "Labor Group: " + recLabor.Fields["labordesc"].Value + "  ";
            strmsg = strmsg + "Group Size Must Be -1, or Greater Than 0";
            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Labor Group Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;


            recLabor.MoveNext();
        }; // end while

        if ((fError == -1) || (fWarn == -1))
        {
            retval = 0;
        }
        else
        {
            retval = -1;
        };

        fexit = -1;
        DbUse.CloseAdoRec(recLabor);
        DbUse.CloseAdoRec(recerror);
        

        // return; //exit _ValidateLaborGroup:
        return retval; //exit  Function;

//err_ValidateLaborGroup:
        if (fexit == -1)
        {
            //RESUME NEXT
        };

        /*     if (Err.Number  == 3021)   {
                //' resume   return; //exit _ValidateLaborGroup;
             } else {
                //' msgbox(ErrorToString(), 0, appl_name);
                //' resume   return; //exit _ValidateLaborGroup;
                 };
         * 
         * */

    } // end function;

    protected short ValidateOperation(short fError, short fWarn)
    {
        //on error goto Err_ValidateOperation;

        //Define variables;

        ADODB.Recordset recoper = null;
        ADODB.Recordset recOpera = null;

        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recLabor = null;
        ADODB.Recordset recerror = null;
        ADODB.Recordset recOperFrTo = null;
        ADODB.Recordset recOperFT = null;
        string strCriteria;
        string strtable1;
        string strTAble1a;
        string strtable2;
        string strTAble3;
        string strTAble4;


        string strsql;
        int partid;

        string strmsg;
        short fexit;
        float flttotal;
        short retval;

        //Default varaibles;
        fexit = 0;
        fError = 0;
        fWarn = 0;

        strtable1 = "tblOper";
        strTAble1a = "tblOper";
        strtable2 = "tblProdFore";
        strTAble3 = "tblLABOR";
        strTAble4 = "zstblErrors";

        //  done else where ...  checkDOCKSTOCK();

        //Set variables and open tables;
        //DbUse.open_ado_rec(globaldb, ref recOpera, strTAble1a);
        //DbUse.open_ado_rec(globaldb, ref recProdFore, strtable2);
        //DbUse.open_ado_rec(globaldb, ref recLabor, strTAble3);
        

        // delete opers with no product.;
        runsqlado("Update tbloper SET tbloper.Flag = 0;");
        runsqlado("Update tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID SET tbloper.Flag = 1;");
        runsqlado("delete tbloper.* FROM tbloper WHERE (tbloper.Flag=0);");

        // mark opers with missing eq ???;
        runsqlado("Update tbloper SET tbloper.Flag = 0;");
        runsqlado("Update tbloper, tblequip SET tbloper.eqid = [tblequip].[EQUIPid] WHERE ((tbloper.OpNam='DOCK' Or tbloper.OpNam='STOCK' Or tbloper.OpNam='SCRAP') AND (tblequip.EquipDesc='NONE'));");
        runsqlado("Update tbloper SET tbloper.Flag = 1 WHERE (tbloper.OpNam='DOCK' Or tbloper.OpNam='STOCK' Or tbloper.OpNam='SCRAP');");
        runsqlado("Update tbloper INNER JOIN tblequip ON tbloper.EqID = tblequip.EquipID SET tbloper.Flag = 1;");

        strtable1 = "SELECT DISTINCTROW tbloper.Flag, tblprodfore.ProdDesc, tbloper.OpNam frOM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID WHERE (tbloper.Flag=0);";
        DbUse.open_ado_rec(globaldb, ref recoper, strtable1);
        DbUse.open_ado_rec(globaldb, ref recerror, strTAble4);
        // NO EQUIPMENT;

        //Make sure all operations other than Dock, Stock + scrap use at least;
        //one piece of equipment. If an Operation does not use a piece of;
        //equipment, generate an error.;

        while (!recoper.EOF)
        {
            strmsg = "No Equipment Assigned -- "; //  gwwd error  ths is where the verify error is generated ... 
            strmsg = strmsg + "Product: " + recoper.Fields["proddesc"].Value + " ";
            strmsg = strmsg + "Operation: " + recoper.Fields["opnam"].Value + " ";
            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Operation Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;
            recoper.MoveNext();
        }; // end while
        DbUse.CloseAdoRec(recoper);
        DbUse.CloseAdoRec(recerror);


        //Make sure all Operations other than Stock + SCRAP appear in the;
        //From Side of the To/From table.;

        runsqlado("Update tbloper SET tbloper.Flag = 0;");
        runsqlado("Update tbloper SET tbloper.Flag = 1 WHERE (Tbloper.OpNam='STOCK' Or tbloper.OpNam='SCRAP');");
        runsqlado("Update tbloperfrto INNER JOIN tbloper ON tbloperfrto.OpNumF = tbloper.OpID SET tbloperfrto.fromopname = tbloper.opnam;");
        runsqlado("Update tbloperfrto INNER JOIN tbloper ON tbloperfrto.fromopname = tbloper.Opnam SET tbloper.Flag = 1;");


        strtable1 = "SELECT DISTINCTROW tbloper.*, tblprodfore.ProdDesc as proddesc, tbloper.Flag FROM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID WHERE (tbloper.Flag=0);";

        DbUse.open_ado_rec(globaldb, ref recoper, strtable1);
        DbUse.open_ado_rec(globaldb, ref recerror, strTAble4);
        // NO FROM operation in routing;

        while (!recoper.EOF)
        {
            strmsg = "Operation Assignment Error -- ";
            strmsg = strmsg + "All operations must have a From - To assignment.  ";
            strmsg = strmsg + "Part: " + recoper.Fields["proddesc"].Value + "  ";
            strmsg = strmsg + "Operation: " + recoper.Fields["opnam"].Value + "  ";
            strmsg = strmsg + "does not have an assignment."; //  gwwd error  ths is where the verify error is generated ...  error
            recerror.AddNew();
            recerror.Fields["Level"].Value = ERROR_FLAG;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Operation Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            fError = -1;
            recoper.MoveNext();
        }; // end while
        DbUse.CloseAdoRec(recoper);

        int ret1;
        //  check for null values

        ret1 = check_for_null("PercentAssign", "Percent Assigned = an Expression or a number between 0 and 100. ", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("labsetuptime", "Labor Setup Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("labruntime", "Labor Run Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqsetuptime", "Equipment Setup Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqruntime", "Equipment Run Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;


        ret1 = check_for_null("labrunlot", "Labor Run Lot Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("labsetuptbatch", "Labor Setup Transfer Batch Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("labsetuppiece", "Labor Setup Time per piece =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("labruntbatch", "Labor Run Time per transfer batch=  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqrunlot", "Equipment Run Lot Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqsetuptbatch", "Equipment Setup Transfer Batch Time =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqsetuppiece", "Equipment Setup Time per piece =  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        ret1 = check_for_null("eqruntbatch", "Equipment Run Time per transfer batch=  an Expression or a number greater or equal to 0.", recerror);
        if (ret1 != 0)
            fError = -1;

        DbUse.CloseAdoRec(recerror);

        if ((fError == -1) || (fWarn == -1))
        {
            retval = 0;
        }
        else
        {
            retval = -1;
        };




        //if (null != recoper)
        //{
        //    recoper.Close();
        //    recoper = null;
        //};

        if (null != recOpera)
        {
            DbUse.CloseAdoRec(recOpera);
            recOpera = null;
        };

        if (null != recProdFore)
        {
            DbUse.CloseAdoRec(recProdFore);
            recProdFore = null;
        };

        if (null != recLabor)
        {
            DbUse.CloseAdoRec(recLabor);
            recLabor = null;
        };

        //if (null != recerror)
        //{
        //    recerror.Close();
        //    recerror = null;
        //};

        if (null != recOperFrTo)
        {
            DbUse.CloseAdoRec(recOperFrTo);
            recOperFrTo = null;
        };

        if (null != recOperFT)
        {
            DbUse.CloseAdoRec(recOperFT);
            recOperFT = null;
        };


        // return; //exit _ValidateOperation:
        fexit = -1;
        return retval; //exit  Function;

err_ValidateOperation:
        if (fexit == -1)
        {
            //RESUME NEXT
        }
        else
        {
            //' msgbox(ErrorToString() + "   Going on to next operation.", 0, appl_name);
            //' resume   return; //exit _ValidateOperation;
        };
        //' resume;
    } // end function;






    protected short ValidateOperFrTo(short fError, short fWarn)
    {
        //on error goto Err_ValidateOperFrTo;

        //Define variables;

        ADODB.Recordset recToFr = null;
        ADODB.Recordset recToFr2 = null;
        ADODB.Recordset recoper = null;
        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recerror = null;
        int prodid;

        string strCriteria;
        string strtable1;
        string strtable2;
        string strTAble3;
        string strTAble4;
        string strTable5;
        string strmsg;
        short fexit;
        short fDel;
        short intTotal;
        int partid;
        short retval;

        //Default varaibles;
        fexit = 0;
        fError = 0;

        fWarn = 0;
        fDel = 0;
        intTotal = 0;
        strtable1 = "tblOperFrTo";
        strtable2 = "zstblErrors";

        //Set variables and open all tables;

        
        runsqlado("Update tblOperFrTo SET tblOperFrTo.Flag = 0;");
        runsqlado("Update (tbloperfrto INNER JOIN tbloper ON tbloperfrto.OpNumF = tbloper.OpID) INNER JOIN tbloper AS tbloper_1 ON tbloperfrto.OpNumT = tbloper_1.OpID SET tbloperfrto.Flag = 1;");
        runsqlado("delete tbloperFRTO.* FROM tbloperFRTO WHERE (tbloperFRTO.Flag=0);");

        DbUse.open_ado_rec(globaldb, ref recerror, strtable2);
        //Loop through all the records and validate them.;
        //Check to see if all the Operations are valid.;
        //If an Operation is not valid delete the record;

        //count # of routing and see if > 0 for all parts.; 
         /* runsql("UPDATE tblprodfore SET tblprodfore.Flag = 0;");  // today !!  race race race 
        runsql("UPDATE tblprodfore INNER JOIN tbloperfrto ON tblprodfore.ProdID = tbloperfrto.PartFore SET tblprodfore.Flag = 1;");
        runsql("UPDATE tblprodfore INNER JOIN tbloperfrto ON tblprodfore.ProdID = tbloperfrto.PartFore SET tblprodfore.Flag = 1;");
        runsql("UPDATE tblprodfore INNER JOIN tbloperfrto ON tblprodfore.ProdID = tbloperfrto.PartFore SET tblprodfore.Flag = 1;");
        runsql("UPDATE tblprodfore INNER JOIN tbloperfrto ON tblprodfore.ProdID = tbloperfrto.PartFore SET tblprodfore.Flag = 1;");
          orroder quries 

        .Threading.CountdownEvent.Wait(100);
       */ 


        strtable1 = "SELECT tblprodfore.* FROM tblprodfore  order by tblprodfore.prodid;";
        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable1);

        recProdFore.MoveFirst();
        while (!recProdFore.EOF) {
            prodid = Convert.ToInt32(recProdFore.Fields["Prodid"].Value);
            strtable1 = "SELECT tbloperfrto.* FROM tbloperfrto where tbloperfrto.partfore = "+ prodid + ";";
            DbUse.open_ado_rec(globaldb, ref recToFr, strtable1);

            if (recToFr.EOF)
            {
                strmsg = "Product Routing Error -- ";
                strmsg = strmsg + "All products must have at least 1 From - To assignment.  ";
                strmsg = strmsg + "Part: " + recProdFore.Fields["proddesc"].Value + "  ";
                strmsg = strmsg + "does not have any routing records.";
                recerror.AddNew();
                recerror.Fields["Level"].Value = ERROR_FLAG;
                calc_return |= ERR_FLAG;
                recerror.Fields["Table"].Value = "Routing Table";
                recerror.Fields["Text"].Value = strmsg;
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
                fError = -1;
            }
            recProdFore.MoveNext();
        }; // end while
        DbUse.CloseAdoRec(recProdFore);
        DbUse.CloseAdoRec(recerror);

        del_nulls();

        if ((fError == -1) || (fWarn == -1))
        {

            retval = 0;
        }
        else
        {

            retval = -1;
        };

        fexit = -1;
        if (recToFr != null)
        {
            DbUse.CloseAdoRec(recToFr);
            recToFr = null;
        }
        if (recToFr2 != null)
        {
            DbUse.CloseAdoRec(recToFr2);
            recToFr2 = null;
        }
        if (recoper != null)
        {
            DbUse.CloseAdoRec(recoper);
            recoper = null;
        }
        //if (recProdFore != null)
        //{
        //    recProdFore.Close();
        //    recProdFore = null;
        //}
        //if (recerror != null)
        //{
        //    recerror.Close();
        //    recerror = null;
        //}
       
        // return; //exit _ValidateOperFrTo:
        return retval; //exit  Function;

//err_ValidateOperFrTo:
        if (fexit == -1)
        {
            //RESUME NEXT
        }
        else
        {

            //' msgbox(ErrorToString(), 0, appl_name);
            //' resume   return; //exit _ValidateOperFrTo;
        };
    } // end function;

    protected short ValidateProdFore(ref short fError, ref short fWarn)
    {

        //on error goto Err_ValidateProdFore;


        //Define variables;

        ADODB.Recordset recProdFore = null;
        ADODB.Recordset recoper = null;
        ADODB.Recordset recoper2 = null;
        ADODB.Recordset recerror = null;

        string strCriteria;
        string str1;
        string strtable1;
        string strtable2;
        string strTAble3;
        string strmsg;
        short intAssigned;
        short fexit;
        short intcount;
        short retval = 0;


        //Default variables;
        fexit = 0;
        fError = 0;
        fWarn = 0;
        strtable1 = "tblProdFore";
        strtable2 = "tbloper";
        strTAble3 = "zstblErrors";

        //Open(); LucieFix
        //Set variables and opentables;
        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable1);
        DbUse.open_ado_rec(globaldb, ref recoper, strtable2);
        DbUse.open_ado_rec(globaldb, ref recerror, strTAble3);


        str1 = "SELECT tblprodfore.ProdDesc, tblprodfore.Prodid, tbloper.opid, tbloper.OpNam, tbloper.OpNum, tbloper.OpId FROM tbloper AS tbloper_1 INNER JOIN (tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID) ON (tbloper_1.OpNum = tbloper.OpNum) AND (tbloper_1.ProdFore = tbloper.ProdFore) WHERE (((tbloper.OpNam)<>[tbloper_1].[opnam]) AND (tbloper.OpId)<>[tbloper_1].[opid]);";
        DbUse.open_ado_rec(globaldb, ref recoper2, str1);
        if (!recoper2.EOF)
        {

            //ret = '' msgbox("Warning: One or more products have operations with the same number but different names.  Should these operations be listed in the Warnings/Error screen ?", MsgBoxStyle.YesNo, appl_name);
            fWarn = -1;
            while (!recoper2.EOF)
            {
                recerror.AddNew();
                recerror.Fields["Level"].Value = 1;
                calc_return |= WARN_FLAG;
                recerror.Fields["Text"].Value = "For product: " + recoper2.Fields["proddesc"].Value + " Operation: " + recoper2.Fields["opnam"].Value + " Number " + recoper2.Fields["opnum"].Value + ".  There are two or more operations with the same number and different names.";
                recerror.Fields["Table"].Value = "Operations Table";
                recerror.Fields["partid"].Value = recoper2.Fields["prodid"].Value;
                recerror.Fields["operid"].Value = recoper2.Fields["opid"].Value;
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
                recoper2.MoveNext();
            }; // end while
        } //  end if 

        
        while (!recProdFore.EOF)
        {
            clean_ops_route_quick((int)recProdFore.Fields["ProdID"].Value);
            recProdFore.MoveNext();
        }; // end while



        checkDOCKSTOCK();
      
        //'RUNSQL "UPDATE tblprodfore SET tblprodfore.Flag = 0;";
        strtable1 = "SELECT Count(tbloper.OpNam) AS CountOfOpNam, tblprodfore.ProdDesc, tblprodfore.EndDemd FROM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID GROUP BY tblprodfore.ProdDesc, tblprodfore.EndDemd;";

        DbUse.open_ado_rec(globaldb, ref recProdFore, strtable1);


        //Loop through all the records to validate them;
        //Also make sure each Product has at least three;
        //operations else 

        while (!recProdFore.EOF)
        {


            //If it is make sure that the Product appears in a;
            //Production Assignment, Make sure 100% of the Product is;
            //Assigned, 

            if (((int)recProdFore.Fields["countofopnam"].Value < 3))
            {
                strmsg = "Invalid Operations -- Product: " + recProdFore.Fields["proddesc"].Value + "  ";
                strmsg = strmsg + "Does not Contain Dock, Stock + Scrap Operations";
                recerror.AddNew();
                recerror.Fields["Level"].Value = ERROR_FLAG;
                calc_return |= ERR_FLAG;
                recerror.Fields["Table"].Value = "Operations Table";
                recerror.Fields["Text"].Value = strmsg;
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
                fError = -1;
            };
            if (((float)recProdFore.Fields["enddemd"].Value < 0))
            {
                strmsg = "Invalid Demand -- Product: " + recProdFore.Fields["proddesc"].Value + "  ";
                strmsg = strmsg + "requires less than 0 production ?";
                recerror.AddNew();
                recerror.Fields["Level"].Value = ERROR_FLAG;
                calc_return |= ERR_FLAG;
                recerror.Fields["Table"].Value = "Product Table";
                recerror.Fields["Text"].Value = strmsg;
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
                fError = -1;
            };
            recProdFore.MoveNext();
        }; // end while

        if ((fError == -1) || (fWarn == -1))
        {
            retval = 0;
        }
        else
        {
            retval = -1;
        };


        fexit = -1;
        DbUse.CloseAdoRec(recProdFore);
        DbUse.CloseAdoRec(recerror);
        DbUse.CloseAdoRec(recoper);
        DbUse.CloseAdoRec(recoper2);
        

        // return; //exit _ValidateProdFore:
        return retval; //exit  Function;

//err_ValidateProdFore: ;
        /* if (Err.Number == 3021)
         {
             strmsg = "You Do Not Have Any Products In Model -- ";
             strmsg = "You Must Define At Least One Product";
             recerror.AddNew();
             recerror.Fields["Level"].Value = ERROR_FLAG;
             calc_return |= ERR_FLAG;
             recerror.Fields["Table"].Value = "Production Forecast Table";
             recerror.Fields["Text"].Value = strmsg;
             recerror.Update();
             fError = -1;
             retval = 0;
             //' resume   return; //exit _ValidateProdFore;
         };  */

        //if (fexit == -1)
        //{
            //RESUME NEXT
        //}
        //else
        //{

            //' msgbox(ErrorToString(), 0, appl_name);
            //RESUME NEXT
        //};
    } // end function;



   public short testLoop()  {                
        //on error goto err1;

        ADODB.Recordset rec1 = null;
        ADODB.Recordset rec2 = null;
         short retval = 0;

         int  err_Renamed;
            int errtot;
        runsqlado("UPDATE TBLPRODFORE SET TBLPRODFORE.FLAG = 0");
        runsqlado("DELETE zstblErrors.* From zstblErrors WHERE (((zstblErrors.Table)='IBOM Table - LOOP'));");

        DbUse.open_ado_rec(globaldb, ref rec1, "tblprodFORE");
        DbUse.open_ado_rec(globaldb, ref rec2, "zstblerrors");

        msgLoop = "";

        retval = 0;

        errtot = 0;

        while (!rec1.EOF) {
            if ( short.Parse(rec1.Fields["Flag"].Value.ToString())  == 0)   {
                msgLoop = "";
                err_Renamed = markloop(( (int) rec1.Fields["prodid"].Value));
                msgLoop = msgLoop + rec1.Fields["proddesc"].Value;
                errtot = errtot + err_Renamed;
                if ((errtot  == 1))   {
                   //' msgbox(msgLoop, 0, appl_name);  //ERROR MSG  
                    xxx(); //  add check at places in PAGES  for error table not empty & | error was added to table ???
                    retval = -1;
                    };

                if ((err_Renamed != 0))   {
                   //' add error item;
                    rec2.AddNew();
                    rec2.Fields["Level"].Value = ERROR_FLAG;
                    calc_return |= ERR_FLAG;
                    rec2.Fields["Table"].Value = "IBOM Table - LOOP";
                    rec2.Fields["Text"].Value = msgLoop;
                    rec2.Update(); 
                    };

                };
            rec1.MoveNext();
        }; // end while



        DbUse.CloseAdoRec(rec1);
        rec1 =  null;
        DbUse.CloseAdoRec(rec2);
        rec2 =  null;

// return; //exit 1:
         return retval; //exit  Function;
     //err1: ;
       //' msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT

    } // end function;


    int markloop(int prodid) {
        //on error goto err1;

        ADODB.Recordset rec1 = null;
        ADODB.Recordset rec2 = null;
        string str1;
        int err_Renamed; 
        int retval;


        str1 = "SELECT tblibom.*, tblibom.ParentID From tblibom WHERE (((tblibom.ParentID)=" + prodid + "));";
        err_Renamed = 0;
         DbUse.open_ado_rec(globaldb, ref rec1, str1);
         DbUse.open_ado_rec(globaldb, ref rec2, "tblprodFORE");

        while (!rec1.EOF) {
            rec2.Filter = "[prodid] = " + rec1.Fields["compid"].Value;  
            if (! (rec2.EOF))   {
                if ((  short.Parse(rec2.Fields["Flag"].Value.ToString())  == 1))   {
                    msgLoop = "IBOM ERROR.  Product " + rec2.Fields["proddesc"].Value + " contains itself ?!?  IBOM Loop:";
                    retval = 1;
                     return retval; //exit  Function;
                    };
                if (short.Parse(rec2.Fields["Flag"].Value.ToString())  == 0)   {

                    rec2.Fields["Flag"].Value = 1;
                    rec2.Update();
                    err_Renamed = markloop(( (int)rec1.Fields["compid"].Value));
                    if (err_Renamed != 0)   {
                        msgLoop = msgLoop + rec2.Fields["proddesc"].Value + " :: ";

                        retval = err_Renamed;
                         return retval; //exit  Function;
                        };

                    rec2.Fields["Flag"].Value = 3;
                    rec2.Update();
                    };
                };
            rec1.MoveNext();
        }; // end while

        retval = 0;


        DbUse.CloseAdoRec(rec1);        
        rec1 =  null;
        DbUse.CloseAdoRec(rec2);
        rec2 =  null;

//exit1:
         return retval; //exit  Function;

//err1: ;
       //' msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT
    } // end function;


    void CheckEquip_NONE()
    {
        //on error goto Err_CHECKeQUIPnONE;

        //Define variables;

        ADODB.Recordset recEquip = null;
        ADODB.Recordset recLabor = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        int l_none_id;

        short fexit;

        //Default varaibles;
        strtable1 = "tblEquip";
        strtable2 = "tblLabor";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable1);
        DbUse.open_ado_rec(globaldb, ref recLabor, strtable2);

        strCriteria = "[LaborDesc] = '" + "NONE" + "'";
        recLabor.Filter = strCriteria;                                   
        l_none_id = (int) recLabor.Fields["LaborID"].Value;            

        recEquip.MoveFirst();

        //Loop through all the Equip Records to Validate them.;
        //First make sure each record has a valid labor Name or NULL, If the record does;
        //not have a valid labor name then assign NONE as LABOR ID.;


        while (!recEquip.EOF)
        {

            //Search the Labor Table for a matching;
            //Labor ID;
            strCriteria = "[LaborID] = " + recEquip.Fields["Labor"].Value;
            recLabor.Filter = strCriteria;                                         

            //Test search results;
            if (recLabor.EOF)
            {

                recEquip.Fields["Labor"].Value = l_none_id;
                recEquip.Update();
            };


            recEquip.MoveNext();

        }; // end while


        DbUse.CloseAdoRec(recEquip);
        recEquip = null;
        DbUse.CloseAdoRec(recLabor);
        recLabor = null;

        // return; //exit _CHECKeQUIPnONE:
        return; //exit  Sub;

//err_CHECKeQUIPnONE: ;
        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume   return; //exit _CHECKeQUIPnONE;

    } // end sub;

    void Checkoper_NONE()
    {
        //on error goto Err_CHECKOperNone;

        //Define variables;

        ADODB.Recordset recEquip = null;
        ADODB.Recordset recoper = null;
        string strCriteria;
        string strtable1;
        string strtable2;
        int e_none_id;

        //Default varaibles;
        strtable1 = "tbloper";
        strtable2 = "tblEquip";


        //Set variables and open tables;
        DbUse.open_ado_rec(globaldb, ref recoper, strtable1);
        DbUse.open_ado_rec(globaldb, ref recEquip, strtable2);

        strCriteria = "[Equipdesc] = '"  + "NONE" + "'";
        recEquip.Filter = strCriteria;                           
        e_none_id = (int)  recEquip.Fields["equipid"].Value;

        recoper.MoveFirst();

        //Loop through all the opers Records to Validate them.;
        //First make sure each record has a valid equip Name or NULL, If the record does;
        //not have a valid labor name then assign NONE as equip ID.;


        while (!recoper.EOF)
        {



            //Search the equip Table for a matching;
            //equip ID;
            strCriteria = "[equipID] = " + recoper.Fields["EqID"].Value;
            recEquip.Filter = strCriteria;                                   

            //Test search results;
            if (recEquip.EOF)
            {
                recoper.Fields["EqID"].Value = e_none_id;
                recoper.Update();
            };

            recoper.MoveNext();

        }; // end while


        DbUse.CloseAdoRec(recEquip);
        DbUse.CloseAdoRec(recoper);
        recEquip = null;
        recoper = null;

        // return; //exit _CHECKOperNone:
        return; //exit  Sub;

//err_CHECKOperNone: ;

                 /*   if (Err.Number != 3021)
                    {
                        //' msgbox(ErrorToString(), 0, appl_name);
                    };
                    //' resume   return; //exit _CHECKOperNone;
              */

    } // end sub;



    public string getname(string strname)
    {
        //on error goto err_getname;

        ADODB.Recordset recDatapath = null;
        string str1;
        string retval;



        str1 = "select tblPrmtrname.* from tblPrmtrname where [label] = '"  + strname  + "';";
        //Open system table to store path to database;
        DbUse.open_ado_rec(globaldb, ref recDatapath, str1);

        if ((recDatapath.EOF))
        {
            retval = "No Name";
        }
        else
        {
            retval = (string)  recDatapath.Fields["Name"].Value;
        };

        if (null != recDatapath)
        {
            DbUse.CloseAdoRec(recDatapath);
            recDatapath = null;
        };

        //exit_getname:
        return retval; //exit  Function;
    //
    //err_getname: ;
        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume  exit_getname;

    } // end function;




    public void FixDuplicateOperNameRoutings() {
        // Lucie - NEW - fix routings for opers with the same name (assigned to one opid)
        if (!runsqlado("SELECT tblOper.ProdFore, tblOper.OpNam, Min(tblOper.OpID) AS MinOfOpID INTO zstblopid FROM tblOper GROUP BY tblOper.ProdFore, tblOper.OpNam;")) {
            runsqlado("DELETE zstblopid.* FROM zstblopid;");
            runsqlado("INSERT INTO zstblopid (ProdFore, OpNam, MinOfOpID) SELECT tblOper.ProdFore, tblOper.OpNam, Min(tblOper.OpID) AS MinOfOpID FROM tblOper GROUP BY tblOper.ProdFore, tblOper.OpNam;");
        }
        runsqlado("UPDATE zstblopid INNER JOIN tblOperFrTo ON (zstblopid.OpNam = tblOperFrTo.fromopname) AND (zstblopid.ProdFore = tblOperFrTo.PartFore) SET tblOperFrTo.OpNumF = [zstblopid].[MinOfOpID];");
        runsqlado("UPDATE zstblopid INNER JOIN tblOperFrTo ON (zstblopid.OpNam = tblOperFrTo.ToOpName) AND (zstblopid.ProdFore = tblOperFrTo.PartFore) SET tblOperFrTo.OpNumT = [zstblopid].[MinOfOpID];");
        // now routings for multiple opers with the same name are assigned to 1 opid only 
    }

    public ClassC(string localdir):base(localdir) {
        
    }
} // End Class;

