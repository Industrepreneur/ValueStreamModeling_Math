using System;
using System.Data;
using System.Data.OleDb;
using ADODB;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

public partial class ClassF : Class2 {
    public ClassF(string localdir)
        : base(localdir) {

    }

    long inUPDATEMPX;
    long instartup;




    public long UpdateMPX() {
        //on error GoTo err_update_mpx   ' Enable error trapping.;



        // DAO.Database mydb;
        DAO.Recordset Mytable;

        string myfile;
        string strsql;
        string strfld;

        DAO.Database dat;
        DAO.Recordset rec;


        DAO.TableDef tdf;


        DAO.Field fld;
        long errorcondition;


        //DAO.Database dbcust;
        DAO.Recordset recTable;

        string strtable;
        DAO.Recordset recTableList;

        long newtable = 0;
        int retValue = 0;

        int err;

        string str1;


        inUPDATEMPX = -1;




        //Need to close globaldb!!!
        /*DbUse.CloseAdo(globaldb);

        myfile = varlocal + "curr_mpx.mdb";
        myfile = varlocal + "mpxmdb.mdb";

        //on error GoTo dberrorHandler   ' Enable error trapping.;
        DAO.DBEngine daoEngine = new DAO.DBEngine();
        dat = daoEngine.OpenDatabase(myfile, true, false, "");
        //rec = dat.OpenRecordset(TABLE_NAMES, DAO.RecordsetTypeEnum.dbOpenDynaset);


        // myfile = varlocal + "\\curr_mpx.xxx";
        //DAO.DBEngine daoEngine = new DAO.DBEngine();
        // mydb = daoEngine.OpenDatabase(myfile, 0, false, "");
       


      // add loop to test for all required tables.;
      //  DAO.DBEngine daoEngine2 = new DAO.DBEngine();
      //  dbcust = daoEngine2.OpenDatabase(varlocal + "\\mpxmdb.mdb", -1, 0);
        strsql = "SELECT DISTINCTROW zstblfieldnames.* FROM zstblfieldnames ORDER BY zstblfieldnames.tablename, zstblfieldnames.fieldname ;";
        recTable = dat.OpenRecordset(strsql, DAO.RecordsetTypeEnum.dbOpenDynaset);


        recTable.MoveFirst();
        while (!recTable.EOF)
        {

            //on error GoTo TableErrorHandler ' Enable error trapping.;
           // mydb.Close();
           // mydb = daoEngine.OpenDatabase(myfile, 0, false, "");
           //test Mytable = dat.OpenRecordset(Convert.ToString(recTable.Fields["tablename"].Value), DAO.RecordsetTypeEnum.dbOpenDynaset);  //' Open table.;
            strtable = Convert.ToString(recTable.Fields["tablename"].Value);

          

            tdf = dat.TableDefs[strtable];
            //on error GoTo err_update_mpx;

            while (!recTable.EOF)
            {
                if (Convert.ToString(recTable.Fields["tablename"].Value) != strtable)  goto nextrec;
                //on error GoTo FieldErrorHandler ' Enable error trapping.;
               

                strfld = Convert.ToString(recTable.Fields["fieldname"].Value);
                fld = tdf.Fields[strfld];

               
                //on error GoTo err_update_mpx;

                if (Convert.ToInt32(recTable.Fields["typenumber"].Value) != 0)
                {


                    if (Convert.ToBoolean(recTable.Fields["allow_no_null"].Value) == true)
                    {
                        fld.AllowZeroLength = false;
                    }
                    else
                    {
                      //test   fld.AllowZeroLength = true;
                    };

                    //  if field types don't match !!!;
                    if (Convert.ToInt16(recTable.Fields["typenumber"].Value) != fld.Type)
                    {
                      //test  changefieldtype(dat, Convert.ToString(recTable.Fields["tablename"].Value), Convert.ToString(recTable.Fields["fieldname"].Value), Convert.ToString(recTable.Fields["fieldtype"].Value));
                    };


                    if (Convert.ToBoolean(recTable.Fields["Index"].Value) == true)
                    {
                       //test  dat.Execute("CREATE INDEX " + Convert.ToString(recTable.Fields["fieldname"].Value) + " ON " + Convert.ToString(recTable.Fields["tablename"].Value) + " (" + Convert.ToString(recTable.Fields["fieldname"].Value) + ");");
                    };

                    //' Set ValidationRule and ValidationText.;
                    if (!(null == Convert.ToString(recTable.Fields["Validaterule"].Value)))
                    {
                        fld.ValidationRule = Convert.ToString(recTable.Fields["Validaterule"].Value);
                    };

                    //' done here blocked error in runsql;
                    if (!(null == Convert.ToString(recTable.Fields["DefaultValue"].Value)))
                    {
                        fld.DefaultValue = Convert.ToString(recTable.Fields["DefaultValue"].Value);
                        //set null -> default value;
                        runsql("UPDATE " + Convert.ToString(recTable.Fields["tablename"].Value) + " SET " + Convert.ToString(recTable.Fields["tablename"].Value) + ".[" + Convert.ToString(recTable.Fields["fieldname"].Value) + "] = " + Convert.ToString(recTable.Fields["DefaultValue"].Value) + " WHERE ((([" + Convert.ToString(recTable.Fields["tablename"].Value) + "." + Convert.ToString(recTable.Fields["Fieldname"].Value) + "] ) Is Null));");
                    };

                    fld.ValidationText = "";

                };  // end if 

         
                //on error GoTo err_update_mpx;
                recTable.MoveNext();
            

            }  //  end while loop;

            nextrec:
            if ((newtable == -1))
            {
                //remove extra item;
                strsql = "ALTER TABLE " + strtable + " DROP COLUMN xxxxx;";
                dat.Execute(strsql);
                newtable = 0;
            };

        }  //  end while loop;


        // added for this version;
        recTable = dat.OpenRecordset("ZSTBLKEYS", DAO.RecordsetTypeEnum.dbOpenDynaset);

        while (!recTable.EOF)
        {
            // debug   see1(recTable!table1, mydb);
            //on error  //RESUME NEXT
            tdf = dat.TableDefs[recTable.Fields["table1"].Value];
            //test tdf.Indexes.Delete("PrimaryKey");
            //test tdf.Indexes.Delete(Convert.ToString(recTable.Fields["keyname"].Value));
            // debug  see1(recTable!table1, mydb);
            //on error GoTo err_update_mpx;

            str1 = Convert.ToString(recTable.Fields["key1"].Value);
            str1 = str1.Trim();
            if (str1.Length > 0)
            {

                //on error  //RESUME NEXT
                strsql = "Alter TABLE [" + Convert.ToString(recTable.Fields["table1"].Value) + "] Drop CONSTRAINT " + Convert.ToString(recTable.Fields["keyname"].Value) + ";";
                dat.Execute(strsql);
                strsql = "Alter TABLE [" + Convert.ToString(recTable.Fields["table1"].Value) + "] ADD CONSTRAINT " + Convert.ToString(recTable.Fields["keyname"].Value) + " PRIMARY KEY (" + Convert.ToString(recTable.Fields["key1"].Value) + ");";
                dat.Execute(strsql);
                //on error GoTo err_update_mpx;
            };
           

            recTable.MoveNext();

        };   // end while


     

        //  update zstblTables AS well ...;
        runsql("Delete [zstbltableNames].* from [zstbltableNames];");

        runsql( "INSERT INTO zstblTableNames ( tableName ) SELECT  distinct  zstblfieldnames.[tableName] FROM zstblfieldnames;");
       

        //mydb.Close();    // Close database.;

        retValue = -1;
        dat.Close();


  /*exit_update_mpx:


        //LinkTables();
        inUPDATEMPX = 0;

        //mydb.Close();    // Close database.;

        setGlobalVar();
        return retValue; //exit  Function;

err_update_mpx:
        if ((err == 3375) | (err == 3219) | (err == 3420))
        {
            //RESUME NEXT
        };
        // // zzzz msgbox Error$ + " Continuing to update datafile.", 0, appl_name;
        retValue = 0;
        //RESUME NEXT

dberrorHandler:
        errorcondition = -1;
        retValue = 0;
        // zz msgbox "Can't open " + myfile + " database.", 0, appl_name;
        // zz resume  exit_update_mpx;

TableErrorHandler:
        //add table;
        //test addtable1(Convert.ToString(recTable.Fields["tablename"].Value), dat);

        Mytable = dat.OpenRecordset(Convert.ToString(recTable.Fields["tablename"].Value));
        newtable = -1;
    //RESUME NEXT

   FieldErrorHandler:
        if ((err != 3380) & (err != 3090) & (err != 3265))
        {
            // zz msgbox Error$, 0, appl_name;
        };

    //test    addfield(Convert.ToString(recTable.Fields["tablename"].Value), Convert.ToString(recTable.Fields["fieldname"].Value), Convert.ToString(recTable.Fields["fieldtype"].Value), Convert.ToInt32(recTable.Fields["typenumber"].Value), fld, tdf, dat);

        //RESUME NEXT


       */
        return retValue;
    }  //End Function;


    public void changefieldtype(DAO.Database mydb, string table1, string field1, string type1) {
        //on error GoTo err1;

        mydb.Execute("ALTER TABLE " + table1 + " add  column temp " + type1 + ";");
        mydb.Execute("UPDATE " + table1 + " SET " + table1 + ".temp = [" + table1 + "].[" + field1 + "];");

        //on error  //RESUME NEXT
        mydb.Execute("drop index " + field1 + " on " + table1 + ";");

        //on error GoTo err1;
        mydb.Execute("alter table " + table1 + " drop column " + field1 + ";");
        mydb.Execute("ALTER TABLE " + table1 + " add  column  " + field1 + " " + type1 + ";");
        mydb.Execute("UPDATE " + table1 + " SET " + table1 + "." + field1 + " = [" + table1 + "].[temp];");
        mydb.Execute("alter table " + table1 + " drop column temp;");

        //exit1:
        return; //exit  Sub;
        //err1:
        // zz msgbox Error$, 0, appl_name;
        //RESUME NEXT
    } // end sub;

    public void xxaddtable1(string tablename, DAO.Database mydb) {
        //on error GoTo err1;

        DAO.TableDef tdfnew;

        // Create a new TableDef object.;
        tdfnew = mydb.CreateTableDef(tablename);

        tdfnew.Fields.Append(tdfnew.CreateField("xxxxx", TEXT_TYPE));

        mydb.TableDefs.Append(tdfnew);

        //exit1:
        return; //exit  Sub;
        //err1:
        // msgbox Error$, 0, appl_name;
        //zz resume  exit1;

    } // end sub;

    void addfield(string tablename, string fieldname, string fieldtype, long typenumber, DAO.Field fld, DAO.TableDef tdf, DAO.Database mydb) {
        //on error GoTo err1;

        string strsql;


        if ((typenumber != 0)) {
            if (typenumber != 10) {
                tdf.Fields.Append(tdf.CreateField(fieldname, typenumber));
            } else {
                tdf.Fields.Append(tdf.CreateField(fieldname, typenumber, 255));
            };
            fld = tdf.Fields[fieldname];
        } else {
            //.Fields.Append .CreateField(recTable!Fieldname, dbLong);
            //Set fld = tdf.fieldCreateField(recTable!fieldname, dbLong);
            //fld.Attributes = fld.Attributes + dbAutoIncrField;
            if (fieldname == "index") {
                fieldname = "[index]";
            };
            strsql = "ALTER TABLE " + tablename + " add COLUMN " + fieldname + " " + fieldtype + ";";
            mydb.Execute(strsql);
            if (fieldname == "[index]") {
                strsql = "CREATE UNIQUE INDEX index1 ON " + tablename + " ([index]);";
                mydb.Execute(strsql);
            } else {
                strsql = "CREATE UNIQUE INDEX " + fieldname + " ON " + tablename + " (" + fieldname + ");";
                mydb.Execute(strsql);
            };
        };


        //exit1:
        return; //exit  Sub;
        //err1:
        //z msgbox Error$, 0, appl_name;
        //RESUME NEXT

    } // end sub;


    public bool set_ids2names() {
        //on error GoTo err_setid2;
        bool idsOk = true;

        idsOk = idsOk && runsqlado("UPDATE tblEquip SET tblEquip.LaborDesc = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblOper SET tblOper.ProdDesc = ' ', tblOper.EquipDesc = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblOperFrTo SET tblOperFrTo.prodDesc = ' ', tblOperFrTo.fromopname = ' ', tblOperFrTo.toopname = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblIbom  SET tblIbom.ParentName = ' ', tblIbom.CompName = ' ';");

        idsOk = idsOk && runsqlado("UPDATE tblWhatIfaudit SET tblWhatIfAudit.Whatif  = ' ';");

        idsOk = idsOk && runsqlado("UPDATE tblRsSummary SET tblRsSummary.Whatif  = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblRsLabor  SET tblRsLabor.Whatif  = ' ', tblRsLabor.LaborDesc = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblRsEquip  SET tblRsEquip.EquipDesc = ' ', tblRsEquip.Whatif  = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblrsprod SET tblRsProd.ProdDesc = ' ', tblRsProd.Whatif  = ' ';");
        idsOk = idsOk && runsqlado("UPDATE tblrsoper SET tblRsOper.ProdDesc = ' ', tblRsOper.OperName = ' ', tblRsOper.Whatif  = ' ';");

        idsOk = idsOk && runsqlado("UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.Labor = tblLabor.LaborID SET tblEquip.LaborDesc = [tbllabor].[laborDesc];");
        idsOk = idsOk && runsqlado("UPDATE (tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID) INNER JOIN tblEquip ON tblOper.EqID = tblEquip.EquipID SET tblOper.ProdDesc = [TBLPRODFORE].[PRODDESC], tblOper.EquipDesc = [TBLEQUIP].[EQUIPDESC];");
        idsOk = idsOk && runsqlado("UPDATE (tblOperFrTo INNER JOIN tblOper AS tblOper_1 ON (tblOperFrTo.OpNumT = tblOper_1.OpID) AND (tblOperFrTo.OpNumT = tblOper_1.OpID)) INNER JOIN (tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID) ON (tblOperFrTo.OpNumF = tblOper.OpID) AND (tblOper_1.ProdFore = tblProdFore.ProdID) SET tblOperFrTo.prodDesc = [tblprodFore].[proddesc], tblOperFrTo.fromopname = [tbloper].[opnam], tblOperFrTo.toopname = [tbloper_1].[opnam];");
        idsOk = idsOk && runsqlado("UPDATE (tblIbom INNER JOIN tblProdFore ON tblIbom.ParentID = tblProdFore.ProdID) INNER JOIN tblProdFore AS tblProdFore_1 ON tblIbom.CompID = tblProdFore_1.ProdID SET tblIbom.ParentName = [TBLPRODFORE].[PRODDESC], tblIbom.CompName = [TBLPRODFORE_1].[PRODDESC];");



        idsOk = idsOk && runsqlado("UPDATE tblwhatif INNER JOIN tblWhatIfAudit ON tblWhatIf.WID  = tblWhatIfAudit.WID SET tblWhatIfAudit.Whatif = [tblwhatif].[name];");

        idsOk = idsOk && runsqlado("UPDATE tblRsSummary INNER JOIN tblwhatif ON tblRsSummary.WID  = tblWhatIf.WID SET tblRsSummary.Whatif = [tblwhatif].[name];");
        idsOk = idsOk && runsqlado("UPDATE (tblRsLabor INNER JOIN tblLabor ON tblRsLabor.LaborID = tblLabor.LaborID) INNER JOIN tblwhatif ON tblRsLabor.WID  = tblWhatIf.WID SET tblRsLabor.Whatif = [tblwhatif].[name], tblRsLabor.LaborDesc = [tbllabor].[labordesc];");
        idsOk = idsOk && runsqlado("UPDATE (tblRsEquip INNER JOIN tblEquip ON tblRsEquip.EquipID = tblEquip.EquipID) INNER JOIN tblwhatif ON tblRsEquip.WID  = tblWhatIf.WID SET tblRsEquip.EquipDesc = [tblequip].[equipdesc], tblRsEquip.Whatif = [tblwhatif].[name];");
        idsOk = idsOk && runsqlado("UPDATE (tblProdFore INNER JOIN tblRsProd ON tblProdFore.ProdID = tblRsProd.ProdID) INNER JOIN tblwhatif ON tblRsProd.WID  = tblWhatIf.WID SET tblRsProd.ProdDesc = [tblprodfore].[proddesc], tblRsProd.Whatif = [tblwhatif].[name];");


        idsOk = idsOk && runsqlado("UPDATE ((tblProdFore INNER JOIN tblRsOper ON tblProdFore.ProdID = tblRsOper.ProdID) INNER JOIN tblOper ON (tblRsOper.OpID = tblOper.OpID) AND (tblRsOper.OpID = tblOper.OpID)) INNER JOIN tblwhatif ON tblRsOper.WID  = tblWhatIf.WID SET tblRsOper.ProdDesc = [TBLPRODFORE].[proddesc], tblRsOper.OperName = [TBLOPER].[OPNAM], tblRsOper.Whatif = [tblwhatif].[NAME];");

        idsOk = idsOk && runsqlado("UPDATE (tblRsLabor INNER JOIN tblLabor ON tblRsLabor.LaborID = tblLabor.LaborID) SET tblRsLabor.Whatif  = 'Base Case', tblRsLabor.LaborDesc = [tbllabor].[labordesc] WHere tblrslabor.wid = 0;");
        idsOk = idsOk && runsqlado("UPDATE (tblRsEquip INNER JOIN tblEquip ON tblRsEquip.EquipID = tblEquip.EquipID) SET tblRsEquip.EquipDesc = [tblequip].[equipdesc], tblRsEquip.Whatif  = 'Base Case' where tblrsequip.wid = 0;");
        idsOk = idsOk && runsqlado("UPDATE (tblProdFore INNER JOIN tblRsProd ON tblProdFore.ProdID = tblRsProd.ProdID) SET tblRsProd.ProdDesc = [tblprodfore].[proddesc], tblRsProd.Whatif  = 'Base Case' where tblrsprod.wid = 0;");
        idsOk = idsOk && runsqlado("UPDATE ((tblProdFore INNER JOIN tblRsOper ON tblProdFore.ProdID = tblRsOper.ProdID) INNER JOIN tblOper ON (tblRsOper.OpID = tblOper.OpID) AND (tblRsOper.OpID = tblOper.OpID)) SET tblRsOper.ProdDesc = [TBLPRODFORE].[proddesc], tblRsOper.OperName = [TBLOPER].[OPNAM], tblRsOper.Whatif = 'Base Case'   Where tblrsoper.wid = 0;");

        //EXIT_setid2:
        return idsOk; //exit  Sub;
        //err_setid2:
        //zz msgbox Error$, 0, appl_name;
        //zz resume  EXIT_setid2;
    } // end sub;

    public bool set_names2ids() {
        //on error GoTo err_set_names;
        bool namesOk = true;
        namesOk = namesOk && runsqlado("UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.LaborDesc = tblLabor.LaborDesc SET tblEquip.Labor = [tbllabor].[laborid] WHERE (((tblLabor.LaborID)<>[TBLequip].[labor]));");
        namesOk = namesOk && runsqlado("UPDATE (tblOper INNER JOIN tblProdFore ON tblOper.ProdDesc = tblProdFore.ProdDesc) INNER JOIN tblEquip ON tblOper.EquipDesc = tblEquip.EquipDesc SET tblOper.ProdFore = [tblprodfore].[prodid], tblOper.EqID = [tblequip].[equipid];");
        namesOk = namesOk && runsqlado("UPDATE ((tblOperFrTo INNER JOIN tblOper AS tblOper_1 ON (tblOperFrTo.prodDesc = tblOper_1.ProdDesc) AND (tblOperFrTo.toopname = tblOper_1.OpNam)) INNER JOIN tblOper ON (tblOperFrTo.prodDesc = tblOper.ProdDesc) AND (tblOperFrTo.fromopname = tblOper.OpNam)) INNER JOIN tblProdFore ON tblOperFrTo.prodDesc = tblProdFore.ProdDesc SET tblOperFrTo.PartFore = [tblprodfore].[prodid], tblOperFrTo.OpNumF = [tbloper].[opid], tblOperFrTo.OpNumT = [tbloper_1].[opid];");
        namesOk = namesOk && runsqlado("UPDATE (tblIbom INNER JOIN tblProdFore ON tblIbom.ParentName = tblProdFore.ProdDesc) INNER JOIN tblProdFore AS tblProdFore_1 ON tblIbom.CompName = tblProdFore_1.ProdDesc SET tblIbom.ParentID = [tblprodfore].[prodid], tblIbom.CompID = [tblprodfore_1].[prodid];");

        namesOk = namesOk && runsqlado("UPDATE tblWhatIfAudit INNER JOIN tblwhatif ON tblWhatIfAudit.Whatif  = tblWhatIf.Name SET tblWhatIfAudit.WID = [tblwhatif].[WID];");

        namesOk = namesOk && runsqlado("UPDATE (tblRsLabor INNER JOIN tblwhatif ON tblRsLabor.Whatif  = tblWhatIf.name) INNER JOIN tblLabor ON tblRsLabor.LaborDesc = tblLabor.LaborDesc SET tblRsLabor.WID = [tblwhatif].[wid], tblRsLabor.LaborID = [tbllabor].[laborid];");
        namesOk = namesOk && runsqlado("UPDATE (tblRsEquip INNER JOIN tblwhatif ON tblRsEquip.Whatif  = tblWhatIf.Name) INNER JOIN tblEquip ON tblRsEquip.EquipDesc = tblEquip.EquipDesc SET tblRsEquip.WID = [tblwhatif].[wid], tblRsEquip.EquipID = [tblequip].[equipid];");
        namesOk = namesOk && runsqlado("UPDATE (tblRsProd INNER JOIN tblProdFore ON tblRsProd.ProdDesc = tblProdFore.ProdDesc) INNER JOIN tblwhatif ON tblRsProd.Whatif  = tblWhatIf.Name SET tblRsProd.WID = [tblwhatif].[wid], tblRsProd.ProdID = [tblprodfore].[prodid];");
        namesOk = namesOk && runsqlado("UPDATE ((tblRsOper INNER JOIN tblProdFore ON tblRsOper.ProdDesc = tblProdFore.ProdDesc) INNER JOIN tblwhatif ON tblRsOper.Whatif  = tblWhatIf.Name) INNER JOIN tblOper ON tblRsOper.OperName = tblOper.OpNam SET tblRsOper.WID = [tblwhatif].[wid], tblRsOper.ProdID = [tblprodfore].[prodid], tblRsOper.OpID = [tbloper].[opid];");
        namesOk = namesOk && runsqlado("UPDATE tblRsSummary INNER JOIN tblwhatif ON tblRsSummary.Whatif  = tblWhatIf.Name SET tblRsSummary.WID = [tblwhatif].[wid];");

        set_ids2names();

        return namesOk;
        //exit_set_names:
        //err_set_names:
        //zz msgbox Error$, 0, appl_name;
        //zz resume  exit_set_names;

    } // end sub;

    public void test_names_ids() {
        //on error GoTo err_test_names;

        long err1;
        string str1;
        long ret;

        ADODB.Recordset rec1 = null;
        err1 = 0;

        //test equip;
        str1 = "SELECT tblLabor.LaborID FROM tblEquip INNER JOIN tblLabor ON tblEquip.LaborDesc = tblLabor.LaborDesc WHERE (((tblLabor.LaborID) <> [tblequip].[labor]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF) {
            err1 = -1;
        };

        //test oper;
        str1 = "SELECT tblOper.ProdDesc, tblOper.EquipDesc FROM (tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID) INNER JOIN tblEquip ON tblOper.EqID = tblEquip.EquipID WHERE (((tblOper.ProdDesc)<>[TBLPRODFORE].[proddesc])) OR (((tblOper.EquipDesc)<>[tblequip].[equipdesc]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };

        //test ibom;
        str1 = "SELECT tblIbom.ParentName, tblIbom.CompName FROM tblProdFore AS tblProdFore_1 INNER JOIN (tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.ParentID) ON tblProdFore_1.ProdID = tblIbom.CompID WHERE (((tblIbom.CompName)<>[TBLPRODFORE_1].[PRODDESC])) OR (((tblIbom.ParentName)<>[TBLPRODFORE].[PRODDESC]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };

        //test audit;
        str1 = "SELECT tblWhatIf.Name FROM tblwhatif INNER JOIN tblWhatIfAudit ON tblWhatIf.WID  = tblWhatIfAudit.WID WHERE (((tblWhatIf.Name)<>[tblwhatifaudit].[whatif]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };

        //test rsSummary;
        str1 = "SELECT tblRsSummary.Whatif FROM tblRsSummary INNER JOIN tblwhatif ON tblRsSummary.WID  = tblWhatIf.WID WHERE (((tblRsSummary.Whatif)<>[tblwhatif].[name]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };

        //test rsLabor;
        str1 = "SELECT tblRsLabor.Whatif, tblRsLabor.LaborDesc FROM (tblRsLabor INNER JOIN tblLabor ON tblRsLabor.LaborID = tblLabor.LaborID) INNER JOIN tblwhatif ON tblRsLabor.WID  = tblWhatIf.WID WHERE (((tblRsLabor.Whatif)<>[tblwhatif].[name])) OR (((tblRsLabor.LaborDesc)<>[tbllabor].[labordesc]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };
        //test rsEquip;
        str1 = "SELECT tblRsequip.Whatif, tblRsequip.equipDesc FROM (tblRsequip INNER JOIN tblequip ON tblRsequip.equipID = tblequip.equipID) INNER JOIN tblwhatif ON tblRsequip.WID  = tblWhatIf.WID WHERE (((tblRsequip.Whatif)<>[tblwhatif].[name])) OR (((tblRsequip.equipDesc)<>[tblequip].[equipdesc]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };
        //test rsProd;
        str1 = "SELECT tblRsProd.Whatif, tblRsProd.ProdDesc FROM (tblRsProd INNER JOIN tblprodfore ON tblRsProd.ProdID = tblprodfore.ProdID) INNER JOIN tblwhatif ON tblRsProd.WID  = tblWhatIf.WID WHERE (((tblRsProd.Whatif)<>[tblwhatif].[name])) OR (((tblRsProd.ProdDesc)<>[tblprodfore].[Proddesc]));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };
        //test rsOper;
        str1 = "SELECT tblRsOper.OperName, tblRsOper.ProdDesc, tblRsOper.Whatif FROM (tblOper INNER JOIN (tblprodfore INNER JOIN tblRsOper ON tblprodfore.ProdID  = tblRsOper.ProdID) ON tblOper.OpID = tblRsOper.OpID) INNER JOIN tblwhatif ON tblRsOper.WID = tblWhatIf.WID WHERE (((tblRsOper.OperName)<>[tbloper].[opnam])) OR (((tblRsOper.ProdDesc)<>[tblprodfore].[proddesc])) OR (((tblRsOper.Whatif)<>[tblwhatif].[name])); ";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.BOF) {
            err1 = -1;
        };


        if (err1 == -1) {
            if (inUPDATEMPX == -1) {
                set_ids2names();
            } else {
                //ret = zz msgbox("The indexes and descriptions in cross-referenced fields are inconsistent. Should MPX use the descriptions as the correct data ?", 4, appl_name);
                ret = -1;
                if ((ret == -1)) {
                    set_names2ids();
                } else {
                    set_ids2names();
                };
            };
        };

        check_dock_stock();
        DbUse.CloseAdoRec(rec1);
        //exit_test_names:
        return; //exit  Sub;
        //err_test_names:
        // zz msgbox Error$, 0, appl_name;
        // zz resume  exit_test_names;
    } // end sub;

    public int check_dock_stock() {
        //on error GoTo err1;

        ADODB.Recordset rec1 = null;
        string str1;
        int ret = 0;

        runsqlado("UPDATE tblProdFore SET tblProdFore.Flag = 0;");
        runsqlado("UPDATE tblProdFore INNER JOIN tbloper ON tblProdFore.ProdID = tbloper.ProdFore SET tblProdFore.Flag = tblprodfore.[flag]+1 WHERE (((tbloper.OpNam)='dock')) OR (((tbloper.OpNam)='stock')) OR (((tbloper.OpNam)='SCRAP'));");

        str1 = "SELECT tblProdFore.ProdID From tblProdFore WHERE (((tblProdFore.Flag)<3));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF) {
            ret |= DOCK_STOCK_MISSING;//ret = zz msgbox("One or more products are missing DOCK, STOCK and SCRAP.  Do you want these to be added for all appropriate products ?", vbYesNo, appl_name);
            
            flag_run();
            //displayMessage ("Adding dock/stock/scrap ");
            runsqlado("delete tbloper_dup.* from tbloper_dup");
            runsqlado("INSERT INTO tbloper_dup ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper.ProdDesc, tbloper.O1, tbloper.O3, tbloper.LabSetupTime, tbloper.PercentAssign, tbloper.O2, tbloper.ProdFore, tbloper.OpNum, tbloper.O4, tbloper.labSetupTbatch, tbloper.eqRunLot, tbloper.eqRunTbatch, tbloper.eqSetupPiece, tbloper.EqID, tbloper.EquipDesc, tbloper.Flag, tbloper.labRunLot, tbloper.labRunTbatch, tbloper.LabRunTime, tbloper.labSetupPiece, tbloper.EqRunTime, tbloper.OpNam, tbloper.EqSetupTime, tbloper.eqSetupTbatch FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.ProdID where ((tblprodfore.flag <3)); "); // move opers
            //runsqlado("INSERT INTO tbloper_dup ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper.ProdDesc, tbloper.O1, tbloper.O3, tbloper.LabSetupTime, tbloper.PercentAssign, tbloper.O2, tbloper.ProdFore, tbloper.OpNum, tbloper.O4, tbloper.labSetupTbatch, tbloper.eqRunLot, tbloper.eqRunTbatch, tbloper.eqSetupPiece, tbloper.EqID, tbloper.EquipDesc, tbloper.Flag, tbloper.labRunLot, tbloper.labRunTbatch, tbloper.LabRunTime, tbloper.labSetupPiece, tbloper.EqRunTime, tbloper.OpNam, tbloper.EqSetupTime, tbloper.eqSetupTbatch FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.ProdID; " // move opers
            runsqlado("DELETE  tbloper.* FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            runsqlado("DELETE  tbloper.* FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            while (!rec1.EOF) {
                addoper_1(Convert.ToInt32(rec1.Fields["prodid"].Value));
                rec1.MoveNext();
            };

            runsqlado("INSERT INTO tbloper ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper_dup.ProdDesc, tbloper_dup.O1, tbloper_dup.O3, tbloper_dup.LabSetupTime, tbloper_dup.PercentAssign, tbloper_dup.O2, tbloper_dup.ProdFore, tbloper_dup.OpNum, tbloper_dup.O4, tbloper_dup.labSetupTbatch, tbloper_dup.eqRunLot, tbloper_dup.eqRunTbatch, tbloper_dup.eqSetupPiece, tbloper_dup.EqID, tbloper_dup.EquipDesc, tbloper_dup.Flag, tbloper_dup.labRunLot, tbloper_dup.labRunTbatch, tbloper_dup.LabRunTime, tbloper_dup.labSetupPiece, tbloper_dup.EqRunTime, tbloper_dup.OpNam, tbloper_dup.EqSetupTime, tbloper_dup.eqSetupTbatch FROM tbloper_dup; "); // move opers
            runsqlado("delete tbloper_dup.* from tbloper_dup");
            runsqlado("DELETE  tbloperfrto.* FROM tbloperfrto INNER JOIN tblProdFore ON tbloperfrto.PartFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            set_ids2names();
            

        };

        runsqlado("UPDATE tblProdFore SET tblProdFore.Flag = 0;");
        runsqlado("UPDATE tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.PartFore = tblProdFore.ProdID SET tblProdFore.Flag = tblProdFore.[flag]+1;");

        str1 = "SELECT tblProdFore.ProdID From tblProdFore WHERE (((tblProdFore.Flag)<1));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF) {
            ret |= NO_ROUTING; //ret = zz msgbox("One or more products does not have a routing.  Should a default routing be added for these products ?", vbYesNo, appl_name);
            flag_run();

            //displayMessage ("Adding default routings");
            while (!rec1.EOF) {
                addfromto_part(Convert.ToInt32(rec1.Fields["prodid"].Value));
                rec1.MoveNext();
            };
            set_ids2names();
        };
        DbUse.CloseAdoRec(rec1);
        do_dock_stock_2_none(); // set equipment to 'NONE' for dock, stock and scrap
        
        rec1 = null;

        //hidedisplaymessage;
        //exit1:
        return ret; //exit  Sub;
        //err1:
        //zz msgbox Error$, 0, appl_name;
        //RESUME NEXT

    } // end sub;




    public void addfromto() {
        //On Error GoTo err_addfromto

        ADODB.Recordset reccust = null;

        runsqlado("delete tbloperfrto.* from tbloperfrto;");

        DbUse.open_ado_rec(globaldb, ref reccust, "tblprodfore");


        reccust.MoveFirst();
        while (!reccust.EOF) {
            addfromto_part(Convert.ToInt32(reccust.Fields["prodid"].Value));
            reccust.MoveNext();
        }

        DbUse.CloseAdoRec(reccust);
        reccust = null;

        //exit_addfromto:
        return; //Exit Sub

        //err_addfromto:
        //    MsgBox Error$, 0, appl_name
        //    Resume exit_addfromto
    }

    public void addfromto_part(int prod) {
        //On Error GoTo Err_addFromto_part

        ADODB.Recordset recoper = null;
        ADODB.Recordset recRoute = null;
        string strSQLoper;
        int dock_id;
        int stock_id;
        int scrap_id;
        int prev_id;
        string prev_name;
        string op_name;

        //'Run the Delete Queries to empty tables
        runsqlado("DELETE tblOperFrTo.* From tblOperFrTo where tbloperfrto.partfore = " + prod + ";");

        //"""""""""""""""
        runsqlado("UPDATE DISTINCTROW tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam) SET tbloper.OpNum = [tbloper_1].[opnum] WHERE ((tbloper.OpID<>[tbloper_1].[opid]) AND (tbloper_1.ProdFore=" + prod + "));");

        strSQLoper = "SELECT DISTINCTROW tblOper.OpID, tblOper.ProdFore FROM tblOper WHERE ((tblOper.ProdFore = " + prod + ")) ORDER BY tblOper.OpID;";


        DbUse.open_ado_rec(globaldb, ref recoper, strSQLoper);
        DbUse.open_ado_rec(globaldb, ref recRoute, "tblOperFrTo");


        recoper.MoveFirst();
        dock_id = Convert.ToInt32(recoper.Fields["opid"].Value);
        recoper.MoveNext();
        stock_id = Convert.ToInt32(recoper.Fields["opid"].Value);
        recoper.MoveNext();
        scrap_id = Convert.ToInt32(recoper.Fields["opid"].Value);
        DbUse.CloseAdoRec(recoper);
        strSQLoper = "SELECT DISTINCTROW tblOper.OpID, tbloper.opnam, tblOper.ProdFore FROM tblOper WHERE ((tblOper.ProdFore = " + prod + ")  and (((tblOper.OpNam)<>'dock' And (tblOper.OpNam)<>'stock' And (tblOper.OpNam)<>'scrap'))) ORDER BY tblOper.OpNum, tbloper.opid;";
        DbUse.open_ado_rec(globaldb, ref recoper, strSQLoper);


        prev_id = dock_id;
        prev_name = "";

        while (!recoper.EOF) {
            op_name = recoper.Fields["OpNam"].Value.ToString();  //  gd
            op_name = op_name.ToUpper();                //  gd 
            if ((!op_name.Equals(prev_name)) & (!op_name.Equals("DOCK")) & (!op_name.Equals("STOCK")) & (!op_name.Equals("SCRAP"))) {
                recRoute.AddNew();
                recRoute.Fields["PartFore"].Value = prod;
                recRoute.Fields["OpNumF"].Value = prev_id;
                recRoute.Fields["OpNumT"].Value = recoper.Fields["opid"].Value;
                recRoute.Fields["Per"].Value = 100;
                recRoute.Update();
                prev_id = Convert.ToInt32(recoper.Fields["opid"].Value);
                prev_name = Convert.ToString(recoper.Fields["opnam"].Value).ToUpper();
            }

            recoper.MoveNext();
        }

        //' add route to stock and scrap
        recRoute.AddNew();
        recRoute.Fields["PartFore"].Value = prod;
        recRoute.Fields["OpNumF"].Value = prev_id;
        recRoute.Fields["OpNumT"].Value = stock_id;
        recRoute.Fields["Per"].Value = 100;
        recRoute.Update();

        recRoute.AddNew();
        recRoute.Fields["PartFore"].Value = prod;
        recRoute.Fields["OpNumF"].Value = prev_id;
        recRoute.Fields["OpNumT"].Value = scrap_id;
        recRoute.Fields["Per"].Value = 0;
        recRoute.Update();

        DbUse.CloseAdoRec(recoper);
        recoper = null;
        DbUse.CloseAdoRec(recRoute);
        recRoute = null;

        //Exit_addFromto_part:
        return;

        //Err_addFromto_part:
        //    MsgBox Error$
        //    Resume Next 'Exit_addFromto_part

    }





    public void do_dock_stock_2_none() {
        //On Error GoTo err_1

        string str1;
        int num1;

        num1 = find_nameItem("none", 0, equip, 0);
        if (num1 != 0) {
            str1 = "UPDATE DISTINCTROW tbloper SET tbloper.EqID = " + num1 + ", tblOper.EquipDesc = 'NONE' WHERE (((tbloper.OpNam)='dock' Or (tbloper.OpNam)='stock' Or (tbloper.OpNam)='scrap'));";
            runsqlado(str1);
        }

        //exit_1:
        return; //exit Sub

        //err_1:
        //    MsgBox Error$, 0, appl_name
        //    Resume Next
    }


    // 
    // / /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 

    

    public void del_eq_ref(long equipid) {
        long num1;
        string str1;


        num1 = find_nameItem("none", 0, equip, 0);
        if (num1 != 0) {
            str1 = "UPDATE DISTINCTROW tbloper SET tbloper.EqID = " + num1 + ", tbloper.EquipDesc = 'None' WHERE (tbloper.eqid=" + equipid + ");";
            runsqlado(str1);
        }
    }

    public void del_lab_ref(long labid) {
        long num1;
        string str1;


        num1 = find_nameItem("none", 0, equip, 0);
        if (num1 != 0) {
            str1 = "UPDATE DISTINCTROW tblequip SET tblequip.labor = " + num1 + ", tblequip.LaborDesc='None' WHERE (tblequip.labor=" + labid + ");";
            runsqlado(str1);
        }
    }

    public void del_op_ref(long opid, int prodid) {
        del_op_ref(opid, prodid, -1);
        
    }

    public bool del_op_ref(long opid, int prodid, long otheropid) {
        bool issueWarning = false;
        if (otheropid > 0) {
            runsqlado("UPDATE tblOperFrTo SET tblOperFrTo.OpNumT = " + otheropid + " WHERE tblOperFrTo.OpNumT = " + opid + ";");
            runsqlado("UPDATE tblOperFrTo SET tblOperFrTo.OpNumF = " + otheropid + " WHERE tblOperFrTo.OpNumF = " + opid + ";");
        } else {
            ADODB.Recordset rec = new ADODB.Recordset();
            DbUse.OpenAdoRec(globaldb, rec, "SELECT tblOperFrTo.* FROM tblOperFrTo WHERE tblOperFrTo.OpNumT = " + opid + " OR tblOperFrTo.OpNumF = " + opid + ";");
            try {
                issueWarning = !rec.EOF;
            } catch (Exception) {
            } finally {
                DbUse.CloseAdoRec(rec);
            }
            runsqlado("DELETE * FROM tblOperFrTo WHERE tblOperFrTo.OpNumT = " + opid + ";");
            runsqlado("DELETE * FROM tblOperFrTo WHERE tblOperFrTo.OpNumF = " + opid + ";");
        }
        return issueWarning;
    }

    public bool repair_routings(long opid, string opname, int prodid) {
        // test for more opers with the same name but different id

        int otherOpId = -1;
        ADODB.Recordset rec = new ADODB.Recordset();
        string sqlQuery = "SELECT tbloper.ProdDesc, tbloper.OpNam, Max(tbloper.OpID) AS MaxOfOpID, Count(1) AS Expr1, Min(tblOper_1.OpID) AS MinOfOpID FROM tbloper INNER JOIN tblOper AS tblOper_1 ON (tbloper.ProdFore = tblOper_1.ProdFore) AND (tbloper.OpNam = tblOper_1.OpNam) GROUP BY tbloper.ProdDesc, tblOper.Prodfore, tbloper.OpNam HAVING (((tbloper.Prodfore)=" + prodid + ") AND ((tbloper.OpNam)='" + opname + "'));";
        DbUse.OpenAdoRec(globaldb, rec, sqlQuery);
        try {
            if (!rec.EOF) {
                int minOpId = int.Parse(rec.Fields["MinOfOpID"].Value.ToString());
                int maxOpId = int.Parse(rec.Fields["MaxOfOpID"].Value.ToString());
                if (minOpId != maxOpId) {
                    if (minOpId == opid) {
                        otherOpId = maxOpId;
                    } else {
                        otherOpId = minOpId;
                    }
                }
            }
        } catch (Exception exp) {
            LogFiles logFiles = new LogFiles(username);
            logFiles.ErrorLog(exp);
        } finally {
            DbUse.CloseAdoRec(rec);
        }
        // delete or reassign routings
        return del_op_ref(opid, prodid, otherOpId);
    }

    public void add_dock_stock_scrap() {

        string str1;
        int prodid;
        ADODB.Recordset recprod = null;

        str1 = "SELECT distinct tblProdFore.ProdID FROM tblProdFore LEFT JOIN tblOper ON tblProdFore.[ProdID] = tblOper.[ProdFore] WHERE (((tblOper.ProdFore) Is Null)); ";
        DbUse.open_ado_rec(globaldb, ref recprod, str1);

        while (recprod.EOF != true) {
            prodid = Convert.ToInt32(recprod.Fields["prodid"].Value);

            addoper_1(prodid);
            recprod.MoveNext();
        }

    }


    //------------------------------------------------------------------------------------------------
    //  state  added new part at opers    need to add dock/stock/scrap   but opers records with names are there ...
    //displayMessage ("Adding dock/stock/scrap ");

    void prep_dock_stock_scrap() {

        string str1;
        int prodid;
        ADODB.Recordset rec1 = null;

        runsqlado("UPDATE tblProdFore SET tblProdFore.Flag = 0;");
        runsqlado("UPDATE tblProdFore INNER JOIN tbloper ON tblProdFore.ProdID = tbloper.ProdFore SET tblProdFore.Flag = tblprodfore.[flag]+1 WHERE (((tbloper.OpNam)='dock')) OR (((tbloper.OpNam)='stock')) OR (((tbloper.OpNam)='SCRAP'));");

        str1 = "SELECT tblProdFore.ProdID From tblProdFore WHERE (((tblProdFore.Flag)<3));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF) {

            runsqlado("delete tbloper_dup.* from tbloper_dup");
            runsqlado("INSERT INTO tbloper_dup ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper.ProdDesc, tbloper.O1, tbloper.O3, tbloper.LabSetupTime, tbloper.PercentAssign, tbloper.O2, tbloper.ProdFore, tbloper.OpNum, tbloper.O4, tbloper.labSetupTbatch, tbloper.eqRunLot, tbloper.eqRunTbatch, tbloper.eqSetupPiece, tbloper.EqID, tbloper.EquipDesc, tbloper.Flag, tbloper.labRunLot, tbloper.labRunTbatch, tbloper.LabRunTime, tbloper.labSetupPiece, tbloper.EqRunTime, tbloper.OpNam, tbloper.EqSetupTime, tbloper.eqSetupTbatch FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.ProdID where ((tblprodfore.flag <3)); "); // move opers
            //runsqlado("INSERT INTO tbloper_dup ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper.ProdDesc, tbloper.O1, tbloper.O3, tbloper.LabSetupTime, tbloper.PercentAssign, tbloper.O2, tbloper.ProdFore, tbloper.OpNum, tbloper.O4, tbloper.labSetupTbatch, tbloper.eqRunLot, tbloper.eqRunTbatch, tbloper.eqSetupPiece, tbloper.EqID, tbloper.EquipDesc, tbloper.Flag, tbloper.labRunLot, tbloper.labRunTbatch, tbloper.LabRunTime, tbloper.labSetupPiece, tbloper.EqRunTime, tbloper.OpNam, tbloper.EqSetupTime, tbloper.eqSetupTbatch FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.ProdID; " // move opers
            runsqlado("DELETE  tbloper.* FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            runsqlado("DELETE  tbloper.* FROM tbloper INNER JOIN tblProdFore ON tbloper.ProdFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            while (!rec1.EOF) {
                addoper_1(Convert.ToInt32(rec1.Fields["prodid"].Value));
                rec1.MoveNext();
            };

            runsqlado("INSERT INTO tbloper ( ProdDesc, O1, O3, LabSetupTime, PercentAssign, O2, ProdFore, OpNum, O4, labSetupTbatch, eqRunLot, eqRunTbatch, eqSetupPiece, EqID, EquipDesc, Flag, labRunLot, labRunTbatch, LabRunTime, labSetupPiece, EqRunTime, OpNam, EqSetupTime, eqSetupTbatch ) SELECT tbloper_dup.ProdDesc, tbloper_dup.O1, tbloper_dup.O3, tbloper_dup.LabSetupTime, tbloper_dup.PercentAssign, tbloper_dup.O2, tbloper_dup.ProdFore, tbloper_dup.OpNum, tbloper_dup.O4, tbloper_dup.labSetupTbatch, tbloper_dup.eqRunLot, tbloper_dup.eqRunTbatch, tbloper_dup.eqSetupPiece, tbloper_dup.EqID, tbloper_dup.EquipDesc, tbloper_dup.Flag, tbloper_dup.labRunLot, tbloper_dup.labRunTbatch, tbloper_dup.LabRunTime, tbloper_dup.labSetupPiece, tbloper_dup.EqRunTime, tbloper_dup.OpNam, tbloper_dup.EqSetupTime, tbloper_dup.eqSetupTbatch FROM tbloper_dup; "); // move opers
            runsqlado("delete tbloper_dup.* from tbloper_dup");
            runsqlado("DELETE  tbloperfrto.* FROM tbloperfrto INNER JOIN tblProdFore ON tbloperfrto.PartFore = tblProdFore.Prodid WHERE (((tblProdFore.Flag)<3));");

            set_ids2names(); //  in class F
        };
        return;
    }

    //--------------------------------------------------------------------


    public void do_all_id_name_flags() {
        int val1;
        string flag;
        string str1;

        ADODB.Recordset recdata = null;


        str1 = "SELECT zstblname_id_flags.Fvalue, zstblname_id_flags.Flag FROM zstblname_id_flags order by id; ";
        DbUse.open_ado_rec(globaldb, ref recdata, str1);

        val1 = (int)recdata.Fields["fvalue"].Value;
        flag = (string)recdata.Fields["flag"].Value;
        while (recdata.EOF != true) {
            switch (flag) {
                case ("Lflag"): {
                        if (val1 == 0) {  //  using lab names in equipment  
                            //  first insert new names from equipment table into labor table...
                            str1 = "INSERT INTO tblLabor ( OT, GrpSiz, Setup, Abst, Run, Varbility, PriorityShare, LaborDesc ) SELECT distinct 0 AS Expr8, 1 AS Expr1, 1 AS Expr2, 0 AS Expr3, 1 AS Expr4, 1 AS Expr5, True AS Expr7, tblEquip.LaborDesc FROM tblEquip LEFT JOIN tblLabor ON tblEquip.[LaborDesc] = tblLabor.[LaborDesc] WHERE (((tblLabor.LaborDesc) Is Null)); ";
                            runsqlado(str1);
                            str1 = "UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.LaborDesc = tblLabor.LaborDesc SET tblEquip.Labor = [tbllabor].[laborid];";
                            runsqlado(str1);

                        } else if (val1 == 1) {  //  using laborids in equipment 

                            //deleteing references to labor records deleted from tbllabor 
                            int lab_none;
                            lab_none = find_nameItem("none", 0, Labor, 0);
                            runsqlado(" UPDATE tblEquip LEFT JOIN tblLabor ON tblEquip.[Labor] = tblLabor.[LaborID] SET tblEquip.Labor = " + lab_none + " , tblEquip.LaborDesc = 'None'  WHERE (((tblLabor.LaborID) Is Null));  ");

                            runsqlado("DELETE  tblRsLabor.* FROM tblRsLabor LEFT JOIN tblLabor ON tblRsLabor.[LaborID] = tblLabor.[LaborID] WHERE (((tblLabor.LaborID) Is Null)); ");
                            runsqlado("DELETE  tblWhatIfAudit.*  FROM tblWhatIfAudit LEFT JOIN tblLabor ON tblWhatIfAudit.[RecordID] = tblLabor.[LaborID] WHERE (((tblLabor.LaborID) Is Null) AND ((tblWhatIfAudit.TableE)= 'tbllabor' )); ");


                            //updatewidname eq table
                            str1 = "UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.Labor = tblLabor.LaborID SET tblEquip.LaborDesc = [tblLabor].[LaborDesc];";
                            runsqlado(str1);
                        }
                    }
                    break;
                case ("Eflag"): {
                        if (val1 == 0) {
                            //  insert names from opers into eqquipment table
                            str1 = "INSERT INTO tblEquip ( EquipDesc, GrpSiz, MTF, mtr, setup, run, Varbility, LaborDesc, OT, EquipType ) SELECT distinct tblOper.EquipDesc, 1 AS Expr1, 1 AS Expr7, 0 AS Expr6, 1 AS Expr5, 1 AS Expr4, 1 AS Expr3, 'none' AS Expr2, 0 AS Expr8, 0 AS Expr9 FROM tblOper LEFT JOIN tblEquip ON tblOper.[EquipDesc] = tblEquip.[EquipDesc] WHERE (((tblEquip.EquipDesc) Is Null)); ";
                            runsqlado(str1);

                            //  using eq names in opers  eqids in opers table update 
                            str1 = "UPDATE tblEquip INNER JOIN tblOper ON tblEquip.EquipDesc = tblOper.EquipDesc SET tblOper.EqID = [tblEquip].[equipid];";
                            runsqlado(str1);
                        } else if (val1 == 1) {  //  using eqids in opers 

                            //deleteing references to equip records deleted from tblequip 
                            int eq_none;
                            eq_none = find_nameItem("none", 0, equip, 0);
                            runsqlado(" UPDATE tblOpers LEFT JOIN tblEquip ON tblOpers.[Eqid] = tblEquip.[EquipID] SET tblOpers.Equip = " + eq_none + " , tblOpers.EquipDesc = 'None'  WHERE (((tblEquip.EquipID) Is Null));  ");

                            runsqlado("DELETE  tblRsEquip.* FROM tblRsEquip LEFT JOIN tblEquip ON tblRsEquip.[EquipID] = tblEquip.[EquipID] WHERE (((tblEquip.EquipID) Is Null)); ");
                            runsqlado("DELETE  tblWhatIfAudit.*  FROM tblWhatIfAudit LEFT JOIN tblEquip ON tblWhatIfAudit.[RecordID] = tblEquip.[EquipID] WHERE (((tblEquip.EquipID) Is Null) AND ((tblWhatIfAudit.TableE)= 'tblequip' )); ");


                            str1 = "UPDATE tblEquip INNER JOIN tblOper ON tblEquip.Equipid = tblOper.EqID SET tblOper.Equipdesc = [tblEquip].[equipdesc]; ";
                            runsqlado(str1);
                        }
                    }
                    break;
                case ("P1flag"): {
                        if (val1 == 0) {
                            //  add new product names from oers table 
                            str1 = "INSERT INTO tblProdFore ( ProdDesc, EndDemd, DemandFac, lotsiz, Variability, makestock, TBatchGather, ProdComment, TransferBatch, [Value], LotSizeFac ) SELECT distinct tblOper.ProdDesc, 0 AS Expr1, 1 AS Expr2, 1 AS Expr3, 1 AS Expr4, False AS Expr5, True AS Expr6, tblProdFore.ProdComment, -1 AS Expr7, 0 AS Expr8, 1 AS Expr9 FROM tblOper LEFT JOIN tblProdFore ON tblOper.[ProdDesc] = tblProdFore.[ProdDesc] WHERE (((tblProdFore.ProdDesc) Is Null)); ";
                            prep_dock_stock_scrap();  //  adding opers for new prduct.

                            //using prod names in opers  
                            str1 = "UPDATE tblOper INNER JOIN tblProdFore ON tblOper.ProdDesc = tblProdFore.ProdDesc SET tblOper.ProdFore = [tblProdFore].[prodid];";
                            runsqlado(str1);
                        } else if (val1 == 1) {  //  using prodids in opers 
                            str1 = "UPDATE tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID SET tblOper.ProdDesc = [tblProdFore].[proddesc];";
                            runsqlado(str1);

                            runsqlado("DELETE tblWhatIfAudit.* FROM tblWhatIfAudit LEFT JOIN tblprodfore ON tblWhatIfAudit.[RecordID] = tblprodfore.[prodID] WHERE (((tblLabor.LaborID) Is Null)); ");
                            runsqlado("DELETE  tblrsprod .*  FROM tblRsprod LEFT JOIN tblProdfore ON tblRsprod.[prodID] = tblprodfore.[prodID] WHERE (((tblprodfore.prodID) Is Null)); ");


                            //add dock, stock and scrap for new products !!!
                            add_dock_stock_scrap();
                        }
                    }
                    break;
                case ("P2flag"): {
                        if (val1 == 0) {  //add new prods from routing table 
                            str1 = "INSERT INTO tblProdFore ( ProdDesc, EndDemd, DemandFac, lotsiz, Variability, makestock, TBatchGather, ProdComment, TransferBatch, [Value], LotSizeFac ) SELECT distinct tblOperfrto.ProdDesc, 0 AS Expr1, 1 AS Expr2, 1 AS Expr3, 1 AS Expr4, False AS Expr5, True AS Expr6, tblProdFore.ProdComment, -1 AS Expr7, 0 AS Expr8, 1 AS Expr9 FROM tblOperfrto LEFT JOIN tblProdFore ON tblOperfrto.[ProdDesc] = tblProdFore.[ProdDesc] WHERE (((tblProdFore.ProdDesc) Is Null)); ";
                            runsqlado(str1);
                            add_dock_stock_scrap();

                            //  using prod names in routing  
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.ProdDesc = tblProdFore.ProdDesc SET tblOperFrTo.PartFore = [tblProdFore].[prodid];";
                            runsqlado(str1);
                        } else if (val1 == 1) {  //  using prodids in routing 
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.PartFore = tblProdFore.ProdID SET tblOperFrTo.ProdDesc = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                        }
                    }
                    break;
                case ("P3flag"): {
                        if (val1 == 0) {  //adding parent names from ibom to prodfore 
                            str1 = "INSERT INTO tblProdFore ( ProdDesc, EndDemd, DemandFac, lotsiz, Variability, makestock, TBatchGather, ProdComment, TransferBatch, [Value], LotSizeFac ) SELECT distinct tblIbom.parentname, 0 AS Expr1, 1 AS Expr2, 1 AS Expr3, 1 AS Expr4, False AS Expr5, True AS Expr6, tblProdFore.ProdComment, -1 AS Expr7, 0 AS Expr8, 1 AS Expr9 FROM tblibom LEFT JOIN tblProdFore ON tblIbom.[Parentname] = tblProdFore.[ProdDesc] WHERE (((tblProdFore.ProdDesc) Is Null)); ";
                            runsqlado(str1);
                            add_dock_stock_scrap();
                            str1 = "INSERT INTO tblProdFore ( ProdDesc, EndDemd, DemandFac, lotsiz, Variability, makestock, TBatchGather, ProdComment, TransferBatch, [Value], LotSizeFac ) SELECT distinct tblIbom.compname, 0 AS Expr1, 1 AS Expr2, 1 AS Expr3, 1 AS Expr4, False AS Expr5, True AS Expr6, tblProdFore.ProdComment, -1 AS Expr7, 0 AS Expr8, 1 AS Expr9 FROM tblibom LEFT JOIN tblProdFore ON tblIbom.[Compname] = tblProdFore.[ProdDesc] WHERE (((tblProdFore.ProdDesc) Is Null)); ";
                            runsqlado(str1);
                            add_dock_stock_scrap();

                            //  using prod names in ibom  
                            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdDesc = tblIbom.ParentName SET tblIbom.ParentID = [tblProdFore].[prodid];";
                            runsqlado(str1);
                            str1 = "UPDATE tblIbom INNER JOIN tblProdFore ON tblIbom.compName = tblProdFore.ProdDesc SET tblIbom.CompID = [tblProdFore].[prodid];";
                            runsqlado(str1);

                        } else if (val1 == 1) {  //  using prodids in ibom 
                            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.ParentID SET tblIbom.ParentName = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                            str1 = "UPDATE tblIbom INNER JOIN tblProdFore ON tblIbom.CompID = tblProdFore.ProdID SET tblIbom.compName = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                        }
                    }
                    break;
                case ("Oflag"): {
                        if (val1 == 0) {  //adding new opers from routing into opers table

                            //  new products ?  JUST IN CASE !!!
                            str1 = "INSERT INTO tblProdFore ( ProdDesc, EndDemd, DemandFac, lotsiz, Variability, makestock, TBatchGather, ProdComment, TransferBatch, [Value], LotSizeFac ) SELECT distinct tblIbom.parentname, 0 AS Expr1, 1 AS Expr2, 1 AS Expr3, 1 AS Expr4, False AS Expr5, True AS Expr6, tblProdFore.ProdComment, -1 AS Expr7, 0 AS Expr8, 1 AS Expr9 FROM tblibom LEFT JOIN tblProdFore ON tblIbom.[Parentname] = tblProdFore.[ProdDesc] WHERE (((tblProdFore.ProdDesc) Is Null)); ";
                            runsqlado(str1);
                            add_dock_stock_scrap();  //  adding opers for new prduct.

                            //  new oper names  from 
                            str1 = "INSERT INTO tblOper ( OpNam, ProdDesc, EquipDesc, PercentAssign, EqSetupTime, EqRunTime, LabSetupTime, LabRunTime, labRunLot, labSetupTbatch, labSetupPiece, labRunTbatch, eqSetupTbatch, eqSetupPiece, eqRunTbatch, eqRunLot ) SELECT distinct tblOperFrTo.fromopname, tblOper.ProdDesc, 'none' AS Expr13, 100 AS Expr14, 0 AS Expr1, 0 AS Expr2, 0 AS Expr3, 0 AS Expr4, 0 AS Expr5, 0 AS Expr6, 0 AS Expr7, 0 AS Expr8, 0 AS Expr9, 0 AS Expr10, 0 AS Expr11, 0 AS Expr12 FROM tblOperFrTo LEFT JOIN tblOper ON tblOperFrTo.[fromopname] = tblOper.[OpNam] WHERE (((tblOper.OpNam) Is Null)); ";
                            runsqlado(str1);
                            //  new oper names  to
                            str1 = "INSERT INTO tblOper ( OpNam, ProdDesc, EquipDesc, PercentAssign, EqSetupTime, EqRunTime, LabSetupTime, LabRunTime, labRunLot, labSetupTbatch, labSetupPiece, labRunTbatch, eqSetupTbatch, eqSetupPiece, eqRunTbatch, eqRunLot ) SELECT distinct tblOperFrTo.toopname, tblOper.ProdDesc, 'none' AS Expr13, 100 AS Expr14, 0 AS Expr1, 0 AS Expr2, 0 AS Expr3, 0 AS Expr4, 0 AS Expr5, 0 AS Expr6, 0 AS Expr7, 0 AS Expr8, 0 AS Expr9, 0 AS Expr10, 0 AS Expr11, 0 AS Expr12 FROM tblOperFrTo LEFT JOIN tblOper ON tblOperFrTo.[toopname] = tblOper.[OpNam] WHERE (((tblOper.OpNam) Is Null)); ";
                            runsqlado(str1);

                            //  using oper names in routing  
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON (tblOperFrTo.PartFore = tblOper.ProdFore) AND (tblOperFrTo.fromopname = tblOper.OpNam) SET tblOperFrTo.OpNumF = [tbloper].[OpID];";
                            runsqlado(str1);
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON (tblOperFrTo.ToOpName = tblOper.OpNam) AND (tblOperFrTo.PartFore = tblOper.ProdFore) SET tblOperFrTo.OpNumT = [tbloper].[OpID];";
                            runsqlado(str1);
                        } else if (val1 == 1) {  //  using operids in routing 
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON tblOperFrTo.OpNumF = tblOper.OpID SET tblOperFrTo.fromopname = [tbloper].[opnam];";
                            runsqlado(str1);
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON tblOperFrTo.OpNumT = tblOper.OpID SET tblOperFrTo.ToOpName = [tbloper].[opnam];";
                            runsqlado(str1);
                        }
                    }
                    break;

            }

            recdata.MoveNext();
        }

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }

        clear_id_name_flags();
    }

    public void eliminate_nulls0() {
        //on error goto err_nulls0;


        string CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + varlocal + "\\mpxmdb.mdb";


        using (OleDbConnection cnnNW = new OleDbConnection(CONNECTION_STRING)) {
        cnnNW.Open();


        OleDbCommand Dosqltext = new OleDbCommand("Update tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam) SET tbloper.OpNum = [tbloper_1].[opnum] WHERE ((tbloper.OpID<>[tbloper_1].[opid]));", cnnNW);
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g1 = 0 WHERE (((IIf(IsNull([g1]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g2 = 0 WHERE (((IIf(IsNull([g2]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g3 = 0 WHERE (((IIf(IsNull([g3]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g4 = 0 WHERE (((IIf(IsNull([g4]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l1 = 0 WHERE (((IIf(IsNull([l1]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l2 = 0 WHERE (((IIf(IsNull([l2]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l3 = 0 WHERE (((IIf(IsNull([l3]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l4 = 0 WHERE (((IIf(IsNull([l4]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbllabor SET tbllabor.laborDept = ' ' WHERE (((IIf(IsNull([labordept]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbllabor SET tbllabor.labcomment = ' ' WHERE (((IIf(IsNull([labcomment]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e1 = 0 WHERE (((IIf(IsNull([e1]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e2 = 0 WHERE (((IIf(IsNull([e2]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e3 = 0 WHERE (((IIf(IsNull([e3]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e4 = 0 WHERE (((IIf(IsNull([e4]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.equipDept = ' ' WHERE (((IIf(IsNull([equipdept]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.eqcomment = ' ' WHERE (((IIf(IsNull([eqcomment]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();


        Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p1 = 0 WHERE (((IIf(IsNull([p1]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p2 = 0 WHERE (((IIf(IsNull([p2]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p3 = 0 WHERE (((IIf(IsNull([p3]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p4 = 0 WHERE (((IIf(IsNull([p4]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.prodDept = ' ' WHERE (((IIf(IsNull([proddept]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.prodcomment = ' ' WHERE (((IIf(IsNull([prodcomment]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.makestock = -1 WHERE (((IIf(IsNull([makestock]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.tbatchgather = -1 WHERE (((IIf(IsNull([tbatchgather]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();


        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o1 = 0 WHERE (((IIf(IsNull([o1]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o2 = 0 WHERE (((IIf(IsNull([o2]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o3 = 0 WHERE (((IIf(IsNull([o3]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o4 = 0 WHERE (((IIf(IsNull([o4]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqsetuptbatch = 0 WHERE (((IIf(IsNull([eqsetuptbatch]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqsetuppiece = 0 WHERE (((IIf(IsNull([eqsetuppiece]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqruntbatch = 0 WHERE (((IIf(IsNull([eqruntbatch]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqrunlot = 0 WHERE (((IIf(IsNull([eqrunlot]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();

        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labsetuptbatch = 0 WHERE (((IIf(IsNull([labsetuptbatch]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labsetuppiece = 0 WHERE (((IIf(IsNull([labsetuppiece]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labruntbatch = 0 WHERE (((IIf(IsNull([labruntbatch]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();
        Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labrunlot = 0 WHERE (((IIf(IsNull([labrunlot]),0,-1))=0));";
        Dosqltext.ExecuteNonQuery();



        cnnNW.Close();
        }

        //exit_nulls0:
        return; //exit  Sub;
        //err_nulls0:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    public bool clean_model()    {  //  MyUtilities.cleans an uploaded model …
    // return true if problem with units in general table else false
       bool unitProblem = false;
     ADODB.Recordset rec1 = null;   
 
     string S1;
     string S2;
     string S3;
     string S4;
     string S5;
     string S6;
     string S7;
     string S8;
     string S9;
     string S10;
     string S11;
     string S12;
     string S13;
     string S14;
     string S15;
     string S16;
    string S17;   //new
 
     int id1;
     string strsql1 ;


     strsql1 = "SELECT tblgeneral.generalid, tblgeneral.Title,  tblgeneral.tufor,  tblgeneral.tult, tblgeneral.tuprod  FROM tblgeneral  ;";

     DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
     while (!rec1.EOF)
     {
         id1 = (int)rec1.Fields["generalid"].Value;
         if (rec1.Fields["title"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["title"].Value).ToUpper();
         if (rec1.Fields["tufor"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["tufor"].Value).ToUpper();
         if (!S2.Equals("YEAR") && !S2.Equals("QUARTER") && !S2.Equals("MONTH") && !S2.Equals("WEEK"))
         {
             S2 = "YEAR";
             unitProblem = true;
         }

         if (rec1.Fields["tult"].Value == null) S3 = " "; else S3 = MyUtilities.clean((string)rec1.Fields["tult"].Value).ToUpper();
         if (!S3.Equals("CALENDAR DAY"))
         {
             S3 = "CALENDAR DAY";
             unitProblem = true;
         }
         if (rec1.Fields["tuprod"].Value == null) S4 = " "; else S4 = MyUtilities.clean((string)rec1.Fields["tuprod"].Value).ToUpper();
         if (!S4.Equals("HOUR") && !S4.Equals("MIN") && !S4.Equals("SEC") && !S4.Equals("HOUR * 100") && !S4.Equals("HOUR * 1000"))
         {
             S2 = "HOUR";
             unitProblem = true;
         }
         strsql1 = "UPDATE tblgeneral  SET tblgeneral.title = '" + S1 + "'," + "tblgeneral.tufor = '" + S2 + "'," + " tblgeneral.tult = '" + S3 + "'," + " tblgeneral.tuprod = '" + S4 + "'" + "  WHERE (((tblgeneral.generalid)=" + id1 + "));";

         runsqlado(strsql1);

         rec1.MoveNext();
     }
     DbUse.CloseAdoRec(rec1);


     strsql1 = "SELECT tblLabor.Laborid, tblLabor.LaborDesc,  tblLabor.LaborDept, tblLabor.labComment  FROM tblLabor  ;";

     DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
     while (!rec1.EOF)
     {
         id1 = (int)rec1.Fields["Laborid"].Value;
         if (rec1.Fields["LaborDesc"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["LaborDesc"].Value).ToUpper();
         if (rec1.Fields["LaborDept"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["LaborDept"].Value).ToUpper();

         if (rec1.Fields["LabComment"].Value == null) S3 = " "; else S3 = MyUtilities.clean((string)rec1.Fields["LabComment"].Value).ToUpper();

         strsql1 = "UPDATE tblLabor  SET tblLabor.Labordesc = '" + S1 + "'," + "tblLabor.Labordept = '" + S2 + "'," + " tblLabor.Labcomment = '" + S3 + "'" + " WHERE (((tblLabor.Laborid)=" + id1 + "));";

         runsqlado(strsql1);

         rec1.MoveNext();
     }

     DbUse.CloseAdoRec(rec1);
    
 

    strsql1 =   "SELECT tblequip.equipid, tblEquip.EquipDesc,  tblequip.labordesc,  tblEquip.EquipDept, tblEquip.Eqcomment  FROM tblEquip  ;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["Equipid"].Value;

    if (rec1.Fields["Equipdesc"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["Equipdesc"].Value).ToUpper();

    if (rec1.Fields["Equipdept"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["Equipdept"].Value).ToUpper();

    if (rec1.Fields["Eqcomment"].Value == null) S3 = " "; else S3 = MyUtilities.clean((string)rec1.Fields["Eqcomment"].Value).ToUpper();  //fixed  null Field 
    if (rec1.Fields["Labordesc"].Value == null) S4 = " "; else S4 = MyUtilities.clean((string)rec1.Fields["Labordesc"].Value).ToUpper();
 
    strsql1 = "UPDATE tblequip  SET tblequip.equipdesc = '" + S1 + "'," + "tblequip.equipdept = '" + S2 + "'," +   " tblequip.Eqcomment = '" + S3 + "'," +   " tblequip.labordesc = '" + S4 + "'" +   "  WHERE (((tblequip.equipid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }


    DbUse.CloseAdoRec(rec1);


 
    strsql1 =   "SELECT tblProdfore.Prodid, tblProdfore.ProdDesc,  tblProdfore.ProdDept, tblProdfore.prodComment  FROM tblProdFore  ;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["Prodid"].Value;
    if (rec1.Fields["Proddesc"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["Proddesc"].Value).ToUpper();
    if (rec1.Fields["Proddept"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["Proddept"].Value).ToUpper();

    if (rec1.Fields["Prodcomment"].Value == null) S3 = " "; else S3 = MyUtilities.clean((string)rec1.Fields["Prodcomment"].Value).ToUpper();
 
    strsql1 = "UPDATE tblProdfore  SET tblProdfore.Proddesc = '" + S1 + "'," + "tblProdfore.Proddept = '" + S2 + "'," +   " tblProdfore.Prodcomment = '" + S3 + "'" +   "  WHERE (((tblProdfore.Prodid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }
    DbUse.CloseAdoRec(rec1);
 
 
    strsql1 =   "SELECT tblIbom.Ibomid, tblIbom.ParentName,  tblIbom.Compname FROM tblIbom  ;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["Ibomid"].Value;
    if (rec1.Fields["Parentname"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["Parentname"].Value).ToUpper();
    if (rec1.Fields["Compname"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["Compname"].Value).ToUpper();
 
 
    strsql1 = "UPDATE tblIbom  SET tblIbom.parentname = '" + S1 + "'," + "tblIbom.compname = '" + S2 + "'" + "  WHERE (((tblIbom.Ibomid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }
    DbUse.CloseAdoRec(rec1);
 
   /* DUPLICATE LATER !!!
    * strsql1 =   "SELECT tbloperfrto.recid, tbloperfrto.prodDesc,  tbloperfrto.fromopname,  tbloperfrto.toopname, tbloperfrto.per  FROM tbloperfrto;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["Recid"].Value;
    if (rec1.Fields["Fromopname"].Value == null) S1 = " "; else S1 = MyUtilities.clean((string)rec1.Fields["Fromopname"].Value).ToUpper();
    if (rec1.Fields["toopname"].Value == null) S2 = " "; else S2 = MyUtilities.clean((string)rec1.Fields["toopname"].Value).ToUpper();

    if (rec1.Fields["proddesc"].Value == null) S3 = " "; else S3 = MyUtilities.clean((string)rec1.Fields["proddesc"].Value).ToUpper();
    if (rec1.Fields["per"].Value == null) S4 = " "; else S4 = MyUtilities.clean((string)rec1.Fields["per"].Value).ToUpper();
 
 
    strsql1 = "UPDATE tbloperfrto  SET tbloperfrto.proddesc = '" + S3 + "'," + "tbloperfrto.fromopname = '" + S1 + "'," +   " tbloperfrto.toopname = '" + S2 + "'," + " tbloperfrto.per = '" + S4 + "'" +     " WHERE (((tbloperfrto.recid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }
    DbUse.CloseAdoRec(rec1);
 
 */
 
 
 
 
    strsql1 =   "SELECT tbloper.opid, tblOper.prodDesc,  tbloper.Opnam,  tblOper.equipdesc, " +
    "tbloper.PercentAssign,  tblOper.EqSetupTime,  tblOper.EqRunTime,  tblOper.LabSetupTime,  tblOper.LabRunTime,  tblOper.labRunLot,  tblOper.labSetupTbatch,  tblOper.labSetupPiece,  tblOper.labRunTbatch,  tblOper.eqSetupPiece,  tblOper.eqRunTbatch, tblOper.eqSetupTbatch, tblOper.eqRunLot  FROM tblOper;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["opid"].Value;
    if (rec1.Fields["proddesc"].Value == null) S1 = " "; S1 = MyUtilities.clean((string)rec1.Fields["proddesc"].Value).ToUpper();
    if (rec1.Fields["Opnam"].Value == null) S2 = " "; S2 = MyUtilities.clean((string)rec1.Fields["Opnam"].Value).ToUpper();

    if (rec1.Fields["equipdesc"].Value == null) S3 = " "; S3 = MyUtilities.clean((string)rec1.Fields["equipdesc"].Value).ToUpper();

    if (rec1.Fields["PercentAssign"].Value == null) S5 = " "; S5 = MyUtilities.clean((string)rec1.Fields["PercentAssign"].Value).ToUpper();
    if (rec1.Fields["EqSetupTime"].Value == null) S6 = " "; S6 = MyUtilities.clean((string)rec1.Fields["EqSetupTime"].Value).ToUpper();
    if (rec1.Fields["EqRunTime"].Value == null) S7 = " "; S7 = MyUtilities.clean((string)rec1.Fields["EqRunTime"].Value).ToUpper();
    if (rec1.Fields["LabSetupTime"].Value == null) S8 = " "; S8 = MyUtilities.clean((string)rec1.Fields["LabSetupTime"].Value).ToUpper();
    if (rec1.Fields["LabRunTime"].Value == null) S9 = " "; S9 = MyUtilities.clean((string)rec1.Fields["LabRunTime"].Value).ToUpper();
    if (rec1.Fields["labRunLot"].Value == null) S10 = " "; S10 = MyUtilities.clean((string)rec1.Fields["labRunLot"].Value).ToUpper();
    if (rec1.Fields["labSetupTbatch"].Value == null) S11 = " "; S11 = MyUtilities.clean((string)rec1.Fields["labSetupTbatch"].Value).ToUpper();
    if (rec1.Fields["labSetupPiece"].Value == null) S12 = " "; S12 = MyUtilities.clean((string)rec1.Fields["labSetupPiece"].Value).ToUpper();
    if (rec1.Fields["labRunTbatch"].Value == null) S13 = " ";  S13 = MyUtilities.clean((string)rec1.Fields["labRunTbatch"].Value).ToUpper();
    if (rec1.Fields["eqSetupPiece"].Value == null) S14 = " "; S14 = MyUtilities.clean((string)rec1.Fields["eqSetupPiece"].Value).ToUpper();
    if (rec1.Fields["eqRunTbatch"].Value == null) S15 = " "; S15 = MyUtilities.clean((string)rec1.Fields["eqRunTbatch"].Value).ToUpper();
    if (rec1.Fields["eqRunLot"].Value == null) S16 = " "; S16 = MyUtilities.clean((string)rec1.Fields["eqRunLot"].Value).ToUpper();
    if (rec1.Fields["eqSetupTbatch"].Value == null) S17 = " "; S17 = MyUtilities.clean((string)rec1.Fields["eqSetupTbatch"].Value).ToUpper();  // new
 
    strsql1 = "UPDATE tbloper  SET tbloper.proddesc = '" + S1 + "'," + " tbloper.opnam = '" + S2 + "'," +   " tbloper.equipdesc = '" + S3 + "',"
        + "tbloper.PercentAssign= '" + S5 + "',"
       + "tbloper.EqSetupTime= '" + S6 + "',"
       + "tbloper.EqRunTime= '" + S7 + "',"
       + "tbloper.LabSetupTime= '" + S8 + "',"
       + "tbloper.LabRunTime= '" + S9 + "',"
       + "tbloper.labRunLot= '" + S10 + "',"
       + "tbloper.labSetupTbatch= '" + S11 + "',"
       + "tbloper.labSetupPiece= '" + S12 + "',"
       + "tbloper.labRunTbatch= '" + S13 + "',"
       + "tbloper.eqSetupPiece= '" + S14 + "',"
       + "tbloper.eqRunTbatch= '" + S15 + "',"
       + "tbloper.eqSetupTbatch= '" + S17 + "',"    //new
       + "tbloper.eqRunLot= '" + S16 + "'"  +   "  WHERE (((tbloper.opid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }
    DbUse.CloseAdoRec(rec1);
 
 
    strsql1 =   "SELECT tbloperfrto.recid, tbloperfrto.prodDesc,  tbloperfrto.fromopname,  tbloperfrto.toopname, tbloperfrto.Per FROM tbloperfrto;";
 
    DbUse.open_ado_rec(globaldb, ref rec1, strsql1);
    while (!rec1.EOF) {
    id1 = (int) rec1.Fields["recid"].Value;
    if (rec1.Fields["proddesc"].Value == null) S1 = " "; S1 = MyUtilities.clean((string)rec1.Fields["proddesc"].Value).ToUpper();
    if (rec1.Fields["FromOpname"].Value == null) S2 = " "; S2 = MyUtilities.clean((string)rec1.Fields["fromOpname"].Value).ToUpper();
    if (rec1.Fields["toopname"].Value == null) S3 = " "; S3 = MyUtilities.clean((string) rec1.Fields["toopname"].Value).ToUpper();

    if (rec1.Fields["Per"].Value == null) S4 = " "; S4 = MyUtilities.clean((string)rec1.Fields["Per"].Value).ToUpper();
 
    strsql1 = "UPDATE tbloperfrto  SET tbloperfrto.proddesc = '" + S1 + "'," + "tbloperfrto.fromopname = '" + S2 + "',"
    +   " tbloperfrto.toopname = '" + S3 + "',"
        + "tbloperfrto.Per= '" + S4 + "'"
      +   "  WHERE (((tbloperfrto.recid)=" + id1 + "));";
 
            runsqlado(strsql1);
 
           rec1.MoveNext();
    }
    DbUse.CloseAdoRec(rec1);
    return unitProblem;

}




}  // end class
