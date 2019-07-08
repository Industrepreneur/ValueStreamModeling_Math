

using System;


public partial class ClassC0 : ClassC {

   
            


    public short DeleteWhatIfAudit(int lngid )  {
        //on error GoTo Err_DeleteWhatIfAudit;

        //Define variables;
         string strsql;
         short retval;

        //Default Variables;
        retval = 0;
        //Create the SQL statement to delete records;
        runsqlado("delete tblWhatIf.* FROM tblWhatIf WHERE ((tblWhatIf.WID=" + lngid + "));");

        //Delete all the records with a matching WID from the;
        //permanent What-if Audit table;
        runsqlado("delete tblWhatIfAudit.* FROM tblWhatIfAudit WHERE ((tblWhatIfAudit.WID=" + lngid + "));");
        runsqlado("DELETE tblAnalysisPoints.* FROM tblAnalysisPoints WHERE WID = " + lngid + " OR InitWID = " + lngid + ";");

        runsqlado("DELETE tblRsSummary.* From tblRsSummary WHERE ((tblRsSummary.WID=" + lngid + "));");
        runsqlado("delete tblRsEquip.* From tblRsEquip WHERE ((tblRsEquip.WID=" + lngid + "));");
        runsqlado("delete tblRsProd.* From tblRsProd WHERE ((tblRsProd.WID=" + lngid + "));");
        runsqlado("delete tblRsLabor.* From tblRsLabor WHERE ((tblRsLabor.WID=" + lngid + "));");
        runsqlado("delete tblRSOper.* FROM tblRSOper WHERE ((tblRSOper.WID=" + lngid + "));");

        //Delete from tblwhatif the recently deleted what if;
        runsqlado("delete tblWhatIf.* from tblwhatif WHERE((tblWhatIf.WID =" + lngid + "));");
        retval = -1;

// return; //exit _DeleteWhatIfAudit:
         return retval; //exit  Function;

//Err_DeleteWhatIfAudit:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _DeleteWhatIfAudit;

    } // end function;


    public short InsertAudit()  {
        //on error GoTo Err_InsertAudit;

        //'Define varaibles;

          ADODB.Recordset recAudit = null;
          ADODB.Recordset tblaudit = null;
             string strCriteria;
             string strtable;
         short  retval; //exit;

             string str1;
             string str2;
            double num1;
            double num2;

            int k_rec1;
            int k_rec2;
             string k_rec3;
             string k_rec4;
            int justadded;
             string oldval;
           

        //'Default variables;
        retval = 0;
        strtable = "ZstblwhatifAudit";

        globTNameA = globTNameA.ToLower();
        globFNameA = globFNameA.ToLower();


        if ((globDType  == YES_NO_TYPE))  { 
            if (globNVal  == "0")   {
                globOVal = "1";
            } else {
                globOVal = "0";
                };
            };

        if ((null == globNVal))   {
            if ((globDType  == (int)TEXT_TYPE)||(globDType == (int) MEMO_TYPE))   {
                globNVal = " ";
            } else {
                globNVal = "0";
            };
         };

        if ((null == globOVal))   {
            if ((globDType  == (int) TEXT_TYPE) || (globDType == (int) MEMO_TYPE))   {
                globOVal = " ";
            } else {
                globOVal = "0";
            };
        };


        if ((globDType  == TEXT_TYPE) || (globDType == MEMO_TYPE))   {
            if (null==globNVal)   {
                globNVal = " ";
                };

          
             str1 = globNVal.Trim();
             str2 = globOVal.Trim();
          
            if ((str1  == str2))   {
                retval = -1;
                 return retval; //exit  Function;
                };
        }
        else if ((globDType == (int)SINGLE_TYPE) || (globDType == (int)int_TYPE) || (globDType == (int)DOUBLE_TYPE))
        {
            num1 = str2d(globNVal);
            num2 = str2d(globOVal);
            if ((num1  == num2))   {
                retval = -1;
                 return retval; //exit  Function;
                };

         } else if   ((globNVal  == globOVal))   {
            retval = -1;
             return retval; //exit  Function;
            };

          if   ((globNVal  == globOVal))   {
            retval = -1;
             return retval; //exit  Function;
          };


        if (null == globOVal)   {
            if ((globDType == (int)TEXT_TYPE) || (globDType == (int)MEMO_TYPE))
            {
                globOVal = " ";
            } else {
                globOVal = "0";
                };
        };



        if ((globTNameA  == "tblEquip") && (globFNameA == "labor"))   {
            str2 = find_item(str2i(globNVal), 0, Labor);
            if (str2.Trim().Length   == 0)   {
                 return retval; //exit  Function;
                };
            };

        if ((globTNameA  == "tbloper") && (globFNameA == "eqid"))   {
            str2 = find_item(str2i(globNVal), 0, equip);
            if (str2.Trim().Length  == 0)   {
                 return retval; //exit  Function;
                };
            };


        //'Set varaibles;

        
        strCriteria = "select * from "+ strtable + " where [WID] = " + glngwid + " And [TableA] = '" + globTNameA + "' And [FieldA] = '" + globFNameA + "' And [RecordID] = '" + globrecid + "'";
        //''        recAudit.Filter = strCriteria;

         DbUse.open_ado_rec(globaldb, ref recAudit, strCriteria);  //' added where !!!;

        justadded = 0;
        if (recAudit.EOF)   {
            justadded = -1;
            recAudit.AddNew();
            recAudit.Fields["wid"].Value = glngwid;
            recAudit.Fields["tablea"].Value = globTNameA;
            recAudit.Fields["TABLEE"].Value = globTNameE;
            recAudit.Fields["fielda"].Value = globFNameA;
            recAudit.Fields["FIELDe"].Value = globFNameE;
            recAudit.Fields["recORDid"].Value = globrecid;
            if ((globDType  == YES_NO_TYPE))   {
                recAudit.Fields["newval"].Value = Cyesno(globNVal);
                recAudit.Fields["OLDval"].Value = Cyesno(globOVal);
            } else {
                recAudit.Fields["newval"].Value = globNVal;
                recAudit.Fields["OLDval"].Value = globOVal;

            };

            if ((((string) recAudit.Fields["tablea"].Value).Equals("tblEquip")) && ( ((string)recAudit.Fields["fielda"].Value).Equals("labor")))   {
                recAudit.Fields["newshow"].Value = recAudit.Fields["newval"].Value;
                recAudit.Fields["oldshow"].Value = recAudit.Fields["OLDval"].Value;
                recAudit.Fields["newshow"].Value = find_item(Convert.ToInt32((string) recAudit.Fields["newshow"].Value), 0, Labor);  

                recAudit.Fields["oldshow"].Value = find_item(Convert.ToInt32((string) recAudit.Fields["oldshow"].Value), 0, Labor);  
            }
            else if (((string)recAudit.Fields["tablea"].Value == "tbloper") && ((string)recAudit.Fields["fielda"].Value == "eqid"))
            {
                recAudit.Fields["newshow"].Value = recAudit.Fields["newval"].Value;
                recAudit.Fields["oldshow"].Value = recAudit.Fields["OLDval"].Value;
                recAudit.Fields["newshow"].Value = find_item(Convert.ToInt32((string) recAudit.Fields["newshow"].Value), 0, equip);   

                recAudit.Fields["oldshow"].Value = find_item(Convert.ToInt32((string) recAudit.Fields["oldshow"].Value), 0, equip);  
            } else {
                recAudit.Fields["newshow"].Value = recAudit.Fields["newval"].Value;
                recAudit.Fields["oldshow"].Value = recAudit.Fields["OLDval"].Value;
                };

            recAudit.Fields["DataType"].Value = globDType;
            recAudit.Update();

            if (Gfamilywid > 0)   {
                //gd gwd  code check!!
                //'add copy to others in family .Fields["!!"].Value;
                 DbUse.open_ado_rec(globaldb, ref tblaudit, "ZStblwhatifaudit");

                recAudit.MoveLast();
                k_rec1 = Convert.ToInt32( recAudit.Fields["recORDid"].Value);
                k_rec2 = Convert.ToInt32( recAudit.Fields["wid"].Value);
                k_rec3 = Convert.ToString( recAudit.Fields["tablea"].Value);
                k_rec4 = Convert.ToString(  recAudit.Fields["fielda"].Value);
                str1 = "INSERT INTO ZStblwhatifaudit ( WID, RecordID, TableA, FieldA, TableE, FieldE, NewVal, OldVal, DataType, Descripton, newshow, oldshow )" + " SELECT tblWhatif.WID, ZstblwhatifAudit.RecordID, ZstblwhatifAudit.TableA, ZstblwhatifAudit.FieldA, ZstblwhatifAudit.TableE, ZstblwhatifAudit.FieldE, ZstblwhatifAudit.OldVal, ZstblwhatifAudit.OldVal, ZstblwhatifAudit.DataType, ZstblwhatifAudit.Descripton, ZstblwhatifAudit.oldshow, ZstblwhatifAudit.oldshow " + " From tblWhatif, ZstblwhatifAudit" + 
                              "WHERE (((tblWhatif.Familyid)=" + Gfamilywid + ") AND ((ZstblwhatifAudit.RecordID)='" + k_rec1 + "') AND ((ZstblwhatifAudit.TableA)= '" + k_rec3 + "') AND ((ZstblwhatifAudit.FieldA)= '" + k_rec4 + "'));";
                runsqlado(str1); // '' adding records to std audit table;

                 DbUse.open_ado_rec(globaldb, ref tblaudit, "Zstblwhatifaudit");
                //' get audit id for the current wid  edit back into zsaudittrail table;
                str1 = "(([WID]=" + k_rec2 + ") AND ([TableA]='" + k_rec3 + "') AND (([FieldA])= '" + k_rec4 + "') AND ([RecordID]= " + k_rec1 + "))";
                tblaudit.Filter = str1;
                k_rec1 = Convert.ToInt32( tblaudit.Fields["auditid"].Value);
                recAudit.Fields["auditid"].Value = k_rec1;
                recAudit.Update();
                };

        }
        else
        {

            if ((globDType  == YES_NO_TYPE))  {
                recAudit.Fields["newval"].Value = Cyesno(globNVal);
            } else {
                recAudit.Fields["newval"].Value = (string) (globNVal);
            };

            if (( ((string) recAudit.Fields["tablea"].Value)  == "tblequip") && ((string) recAudit.Fields["fielda"].Value == "labor"))   
                {
                recAudit.Fields["newshow"].Value = find_item( Convert.ToInt32((string) recAudit.Fields["newval"].Value), 0, Labor);
                recAudit.Fields["oldshow"].Value = find_item(Convert.ToInt32((string)recAudit.Fields["oldval"].Value), 0, Labor);
            }
            else if (((string)recAudit.Fields["tablea"].Value == "tbloper") && ((string)recAudit.Fields["fielda"].Value == "eqid"))
            {
                recAudit.Fields["newshow"].Value = find_item( Convert.ToInt32((string)recAudit.Fields["newval"].Value), 0, equip);
                recAudit.Fields["oldshow"].Value = find_item(Convert.ToInt32((string)recAudit.Fields["oldval"].Value), 0, equip);
            } else {
                recAudit.Fields["newshow"].Value = recAudit.Fields["newval"].Value;
                recAudit.Fields["oldshow"].Value = recAudit.Fields["oldval"].Value;

            };

            recAudit.Update();
        
            if (Gfamilywid  == 0)   {
                if (  Convert.IsDBNull(recAudit.Fields["OLDval"].Value))   { 
                    oldval = "";
                } else {
                    oldval = (string) recAudit.Fields["OLDval"].Value;
                };
                if ((globDType  == YES_NO_TYPE))   {

                    if ((Cyesno(oldval)  == Cyesno(globNVal)))  {
                        recAudit.Delete();
                        };
                 } else if   (justadded  == 0)   {
                    if (! (oldval  == ""))   {
                        if ((string)(oldval)  == (string)(globNVal))   {

                            recAudit.Delete();
                            };
                        };
                    };
                };

            };

        retval = -1;



// return; //exit _InsertAudit:
        if (null != recAudit)   {
            DbUse.CloseAdoRec(recAudit);
            recAudit =  null;
            };
        if (null != tblaudit)   {
            DbUse.CloseAdoRec(tblaudit);
            tblaudit =  null;
            };
       retval = -1; //exit  = -1;
       return retval; //exit  Function;

//Err_InsertAudit:
      
    } // end function;


      // GREG - you could return bool type instead of string "TRUE" or "FALSE"
      string Cyesno(string val1) {
        //on error GoTo err2;
    string retval;

        if (!val1.Equals("0"))   {
            retval = "TRUE";
        } else {
            retval  = "FALSE";
        };

//exit2:
         return retval; //exit  Function;
//err2:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT

    } // end function;


     public short LoadBaseCase() {
        //on error GoTo Err_LoadBaseCase;

        //Define varaibles;
            short retval;

         ADODB.Recordset recAudit = null;
         ADODB.Recordset RECWHATIFID = null;
             string strtable;
             string strsql;
             short  fstatus;
             string strTableA;
             string strField;
             string strID;
         short  intType;
         short  intlen;
             string strNVal;
             string strOVal;
             string pstrcaption;  //'Use to retrieve current Window Caption;
             string strNewCaption;  //'Used to set new Window Caption;
             string strBaseCase;  //  'Used to Store Base case (Caption) 

             string oldval;

            
        if (glngwid == 0)  return -1;  //  in basecase already ...



        DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id"); 
        RECWHATIFID.MoveFirst();

        RECWHATIFID.Fields["whatifload"].Value = -1;
        RECWHATIFID.Update();



        //Default variables;
        retval   = 0;
        strBaseCase = "MPX - Model: ";
        strtable = "ZstblwhatifAudit";
        retval = 0;
        strsql = "delete ZstblwhatifAudit.* FROM ZstblwhatifAudit;";

        //change display field for basecase of what-if's;
        runsqlado("UPDATE [tblwhatif] set [tblwhatif].[display]=-1 WHERE([tblwhatif].[wid]=0);");
        //Set object variables;
         DbUse.open_ado_rec(globaldb, ref recAudit, strtable);

        

     
        while (!recAudit.EOF) {
            strTableA = Convert.ToString( recAudit.Fields["tablea"].Value);
            strField = Convert.ToString(recAudit.Fields["fielda"].Value);
            strID = Convert.ToString(  recAudit.Fields["recORDid"].Value);
            intType = Convert.ToInt16( recAudit.Fields["DataType"].Value);
            if ((recAudit.Fields["OLDval"].Value) == null  )   {
                oldval = "";
            } else {
                oldval = Convert.ToString( recAudit.Fields["OLDval"].Value);
            };
            strNVal = oldval; //'OldVal Stores Base } case (Value) : {
            strOVal = "0";     //'strOVal is not used for the base case;

            fstatus = WriteChanges2(strTableA, strField, strID, intType, strNVal, strOVal); // note 2 lines back strN = oldval writechanges2 uses strN  not show  yep (show is for whatif audit editing!!!   
            if (fstatus  == 0)   {
                // msgbox("Warning An Error was found while reloading basecase", 0, appl_name);
                recAudit.Delete();
                DbUse.CloseAdoRec(recAudit);
                recAudit = null;
                DbUse.CloseAdoRec(RECWHATIFID);
                RECWHATIFID = null;
                retval = 0;
                return retval;//'GoTo  return; //exit _LoadBaseCase;
                };

            recAudit.MoveNext();
        };

        DbUse.CloseAdoRec(recAudit);
         //  done ...
        runsqlado(strsql);   //'Delete all records from the Temporary Audit Table;
        glngwid = 0;
        saveWid();
        Gfamilywid = 0;
         //  add check for basecase run needed ? 

    
        DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");     //  at web restart?  here and others ....        phase for restart  
                                                                              // (-1 basecase load, 0 nothing  1 reading basecase values, 2 loading whatif records ...
        RECWHATIFID.MoveFirst();

        RECWHATIFID.Fields["globalwhatifId"].Value = 0;
        RECWHATIFID.Fields["whatifload"].Value = 0;
        RECWHATIFID.Update();


        retval = -1;

       
// return; //exit _LoadBaseCase:
        retval   = -1;
        
        DbUse.CloseAdoRec(RECWHATIFID);
        MatchEquipTypeToGroupSize();

       return retval; //exit  Function;

Err_LoadBaseCase: ; 
          //ERROR PAGE
                   /* if (Err.Number  == 3021)  {   //'No records in What-if Audit Temp Table;
                        glngwid = 0;
                    * saveWid();
                        Gfamilywid = 0;

                         DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");
                        RECWHATIFID.MoveFirst();

                        RECWHATIFID.Fields["globalwhatifId"].Value = 0;
                        RECWHATIFID.Update();

                        retval = -1;
                        // resume   return; //exit _LoadBaseCase;
                     };

                    if (retval  == -1)   {
                         //RESUME NEXT
                    } else {
                        // msgbox(ErrorToString(), 0, appl_name);
                         //RESUME NEXT
                    };
                    // resume;
                    * 
                    * */
    } // end function;

    public void MatchEquipTypeToGroupSize() {
         runsqlado("UPDATE tblequip SET EquipType = 0, equiptypename = 'Standard' WHERE GrpSiz > 0;");
         runsqlado("UPDATE tblequip SET EquipType = 1, equiptypename = 'Delay'  WHERE GrpSiz = -1;");
    }

     public short LoadWhatIf(int plngWID)  {
        //on error GoTo Err_LoadWhatIf;

        //Define varaibles;

         ADODB.Recordset recAudit = null;
         ADODB.Recordset RECWHATIFID = null;
             string strtable;
             string strsql;
             string strsql2;
             string strTableA;
             string strField;
             string strID;
         short  intType;
             string strNVal;
             string strOVal;
             string strOshow;
             string strNshow;
             string pstrcaption;  //'Use to retrieve current Window Caption;
             string strNewCaption; //'Used to set new Window Caption;
             string strwhatif;  //  'Used to Sore What-if Caption;
         short  fstatus;
         short  ferr;
         short  retval ; //exit;
         short  intlen;
         string str1;

         ADODB.Recordset reccust = null;

        if (plngWID ==0){ 
            str1 = "error loading Base Case as whatif ????";
        }


        DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");
        RECWHATIFID.MoveFirst();
        RECWHATIFID.Fields["whatifload"].Value = 1;
        RECWHATIFID.Fields["globalwhatifId"].Value = plngWID;
        RECWHATIFID.Update();
        DbUse.CloseAdoRec(RECWHATIFID);

        //Default variables;
        retval  = 0;
        ferr = 0;
        
        strtable = "ZstblwhatifAudit";
        strsql = "DELETE [ZstblwhatifAudit].* FROM [ZstblwhatifAudit];";
        strsql2 = "INSERT INTO [ZstblwhatifAudit] ( AuditID, WID, TableA, TableE, FieldA, FieldE, RecordID, NewVal, OldVal, DataType, Descripton, newshow, oldshow ) SELECT DISTINCTROW tblWhatIfAudit.AuditID, tblWhatIfAudit.WID, tblWhatIfAudit.TableA, tblWhatIfAudit.TableE, tblWhatIfAudit.FieldA, tblWhatIfAudit.FieldE, tblWhatIfAudit.RecordID, tblWhatIfAudit.NewVal, tblWhatIfAudit.OldVal, tblWhatIfAudit.DataType, tblWhatIfAudit.Descripton, tblWhatIfAudit.newshow, tblWhatIfAudit.oldshow FROM [tblWhatIfAudit]"
                + " WHERE (([tblWhatIfAudit].[WID]=" + plngWID + "));";

     
        runsqlado("UPDATE [tblwhatif] SET [tblwhatif].[display] = -1 WHERE (([tblwhatif].[wid]=" + plngWID + "));");

        //Delete all current records from the Temporary What-if Audit Table;
        runsqlado(strsql);

        //Add All What-if Changes From Permenant Audit Table To Temporary;
        //Audit Table for selected What-if ID;
        runsqlado(strsql2);
        do_whatif_audit(1); //  does this load whatif ??  yes reading basecase values...  two phase locking ... read then write ...  know which phase you are in and prevents overwrite - loss of data, loss of state.
        MatchEquipTypeToGroupSize();
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''';
        //'  
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''';

        glngwid = plngWID;
        saveWid();
        Gfamilywid = findfamilywid(glngwid);

         DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");
        RECWHATIFID.MoveFirst();
        RECWHATIFID.Fields["globalwhatifId"].Value = plngWID;
        RECWHATIFID.Fields["whatifload"].Value = 0;
        RECWHATIFID.Update();

        retval = -1;


// return; //exit _LoadWhatIf:
        if ( null != recAudit)   {
            DbUse.CloseAdoRec(recAudit);
            recAudit =  null;
            };
        if ( null != RECWHATIFID)    {
            DbUse.CloseAdoRec(RECWHATIFID);
            RECWHATIFID =  null;
            };
        retval   = -1;
         return retval ; //exit  Function;

//Err_LoadWhatIf:
                 /*   if (Err.Number  == 3021)   {
                        ferr = -1;
                         //RESUME NEXT
                        };
                   */
    } // end function;

    public void do_whatif_audit(short phase) { // xxx()  tracing back
        //on error GoTo err1;

         ADODB.Recordset recAudit =null;
         ADODB.Recordset RECWHATIFID = null;

             string strtable;
             string strsql;
             string strTableA;
             string strField;
             string strID;
         short  intType;
             string strNVal;
             string strOVal;
             string strOshow;
             string strNshow;
         short  fstatus;

             string oldval;



        strtable = "select ZstblwhatifAudit.* from ZstblwhatifAudit order by auditid";
        //Set the Object variables;
         DbUse.open_ado_rec(globaldb, ref recAudit, strtable);

        //Step through all records in temporary Audit table;
        //and make changes to data tables;
        if (recAudit.EOF)   {
             return; //exit  Sub;
            };


        //  GETTING OLD VALUE FROM BASECASE;

        recAudit.MoveFirst();

        if (phase  == 1)   {
            while  (!recAudit.EOF) {
                strTableA = Convert.ToString(recAudit.Fields["tablea"].Value);
                strField = Convert.ToString(  recAudit.Fields["fielda"].Value);
                strID = Convert.ToString(recAudit.Fields["recORDid"].Value);
                intType = Convert.ToInt16( recAudit.Fields["DataType"].Value);
                strNVal = Convert.ToString(  recAudit.Fields["newval"].Value);
                strNshow = Convert.ToString(  recAudit.Fields["newshow"].Value);
                if  (Convert.IsDBNull(recAudit.Fields["OLDval"].Value))   {
                    oldval = "";
                } else {
                    oldval =  Convert.ToString( recAudit.Fields["OLDval"].Value);
                };
                strOVal = oldval;
                strOshow = Convert.ToString(recAudit.Fields["oldshow"].Value);


                // function to load data;
                fstatus = WriteChanges1(strTableA, strField, strID, intType, strNVal, ref strOVal);

                //'   update field name ...;
                 update_field_desc(recAudit);

                //Test if WriteChanges had an Error;

                
               if (fstatus != 0)   {

                    recAudit.Fields["OLDval"].Value = strOVal;
                    if (((string)recAudit.Fields["tablea"].Value == "tblEquip") && ((string)recAudit.Fields["fielda"].Value == "labor"))
                    {
                        recAudit.Fields["oldshow"].Value = find_item((int)recAudit.Fields["OLDval"].Value, 0, Labor);
                        recAudit.Update();
                    }
                    else if (((string)recAudit.Fields["tablea"].Value == "tbloper") && ((string)recAudit.Fields["fielda"].Value == "eqid"))
                    {

                        recAudit.Fields["oldshow"].Value = find_item(int.Parse(recAudit.Fields["OLDval"].Value.ToString()), 0, equip);
                        recAudit.Update();
                    }
                    else
                    {
                        recAudit.Fields["oldshow"].Value = recAudit.Fields["OLDval"].Value;
                        recAudit.Update();

                    };

              } else {
                    //' had an error loading whatif;
                    // msgbox("Error loading a What-if record (e.g. missing matching BaseCase record etc).  Changing whatif value - Table: '" + recAudit.Fields["TABLEE"].Value + "'  Field: '" + recAudit.Fields["FIELDe"].Value + "' to the value: " + recAudit.Fields["newshow"].Value, 0, appl_name)
                    //ERROR PAGE ERROR ON WHATIF LOAD ...  MISSING RECORD - DELETED ??  OK.
                    recAudit.Delete();

             }; 



                recAudit.MoveNext();
            };  // end of while

        }; // end ifphase 1


         DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");
        RECWHATIFID.MoveFirst();
        RECWHATIFID.Fields["whatifload"].Value = 2;
        RECWHATIFID.Update();



        recAudit.MoveFirst();

        while  (!recAudit.EOF) {
            strTableA = Convert.ToString(  recAudit.Fields["tablea"].Value);
            strField = Convert.ToString(  recAudit.Fields["fielda"].Value);
            strID = Convert.ToString(  recAudit.Fields["recordid"].Value);
            intType = Convert.ToInt16(recAudit.Fields["DataType"].Value);
            strNVal = Convert.ToString(  recAudit.Fields["newval"].Value);
            strNshow = Convert.ToString(  recAudit.Fields["newshow"].Value);
            if (  (recAudit.Fields["OLDval"].Value) == null)   {
                oldval = "";
            } else {
                oldval = Convert.ToString(  recAudit.Fields["OLDval"].Value);
            };
            strOVal = oldval;
            strOshow = Convert.ToString(  recAudit.Fields["oldshow"].Value);


            // function to load data;
            fstatus = WriteChanges2(strTableA, strField, strID, intType, strNVal, strOVal);

            //Test if WriteChanges had an Error;
            if (fstatus  == 0)   {
                // msgbox("Error loading a What-if record (e.g. missing matching BaseCase record etc).  Trying to changing whatif value - Table: '" + recAudit.Fields["TABLEE"].Value + "'  Field: '" + recAudit.Fields["FIELDe"].Value + "' to the;value: " + recAudit.Fields["newshow"].Value, 0, appl_name)
                //  error page ?  missing record from basecase ...?
                recAudit.Delete();
            };

            recAudit.MoveNext();
        };


         DbUse.open_ado_rec(globaldb, ref RECWHATIFID, "zstbl_g_whatif_id");
        RECWHATIFID.MoveFirst();

        RECWHATIFID.Fields["globalwhatifId"].Value = glngwid;
        RECWHATIFID.Fields["whatifload"].Value = 0;
        RECWHATIFID.Update();


//end1:
        if ( null != recAudit)   {
            DbUse.CloseAdoRec(recAudit);
            recAudit =  null;
            };
        if (null != RECWHATIFID)    {
            DbUse.CloseAdoRec(RECWHATIFID);
            RECWHATIFID =  null;
            };

         return; //exit  Sub;
//err1:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT
    } // end sub;

    public string copy_eq_name(string name) {

        string str1;
        ADODB.Recordset reccust = null;
        bool done;

        name = name.ToUpper();

        //check for duplicate name!! 
        done = false;
        while (!done) {
            str1 = "select tblequip.* from tblequip where equipdesc = '" + name + "';";
            DbUse.open_ado_rec(globaldb, ref reccust, str1);

            if (!reccust.EOF) {
                name = "COPY_OF_" + name;
            } else { done = true; };

        }  // end while


        //xxx-------------------------------------------------------
        if (reccust != null) {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
        }

        return name;
    }
    
    public int addnewwhatif(string widname, string widcomment) {
        
        int wid2; 
        string str1;
        ADODB.Recordset reccust = null;
        bool done;

        widname = widname.ToUpper();

        //check for duplicate name!! 
        done = false;
        while (!done)
        {
            str1 = "select tblwhatif.* from tblwhatif where name = '" + widname + "';";
            DbUse.open_ado_rec(globaldb, ref reccust, str1);

            if (!reccust.EOF)
            {
                widname = "ANOTHER_" + widname;
            }
            else { done = true; };

        }  // end while


        str1 = "select tblwhatif.* from tblwhatif;";

        DbUse.open_ado_rec(globaldb, ref reccust, str1);
        reccust.AddNew();
        wid2 = (int)reccust.Fields["wid"].Value; 
        reccust.Fields["name"].Value = widname.ToUpper(); 
        reccust.Fields["comm"].Value = widcomment;
        reccust.Update();

        if (reccust != null)
        {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
        };

        return wid2;
    }

    public void updatewidname(int lngid, string widname, string widcomment) {


        string str1; 
        ADODB.Recordset reccust = null;
        bool done;
        string widname2;

        done = false;
        widname2 = widname.ToUpper();
        while (!done)
        {
            str1 = "select tblwhatif.* from tblwhatif where name = '" + widname2 + "' and Wid <> "+ lngid + ";";
            DbUse.open_ado_rec(globaldb, ref reccust, str1);

            if (!reccust.EOF)
            {
               widname2 = "A_" + widname2;
            }
            else { done = true; };

        }  // end while 



        str1 = "select tblwhatif.* from tblwhatif where tblwhatif.wid = " + lngid + ";";

        DbUse.open_ado_rec(globaldb, ref reccust, str1);
        if (!reccust.EOF)
        {
            reccust.Fields["name"].Value = widname2.ToUpper();
            reccust.Fields["comm"].Value = widcomment;
            reccust.Update();
        }

        if (reccust != null) {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
        };

    }

    public string get_widcomment(int wid)
    {
        string widcomm;
        string str1;
        ADODB.Recordset reccust = null;

        str1 = "select tblwhatif.* from tblwhatif where tblwhatif.wid = " + wid + ";";

        DbUse.open_ado_rec(globaldb, ref reccust, str1);
        if (reccust.EOF == false)
        {
            widcomm = (string)reccust.Fields["comm"].Value;
            return widcomm;
        }
        else { return ""; }
    }

    public string get_widname(int wid) {
        string widname = "";
        string query;
        ADODB.Recordset reccust =null;
        
        query = "select tblwhatif.* from tblwhatif where tblwhatif.wid = " + wid + ";";

        DbUse.open_ado_rec(globaldb, ref reccust, query);
        if (!reccust.EOF) {
            widname = (string)reccust.Fields["name"].Value;
            
        }
        return widname;
    }

    public string GetAnalysisName(int analysisId) {
        string analysisName = "";
        string query;
        try {
            ADODB.Recordset reccust = null;

            query = "SELECT tblAnalysis.* from tblAnalysis where tblAnalysis.AnalysisID = " + analysisId + ";";

            DbUse.open_ado_rec(globaldb, ref reccust, query);
            if (!reccust.EOF) {
                analysisName = reccust.Fields["Name"].Value.ToString();

            }
        } catch (Exception) { }
        return analysisName;
    }


    public int get_wid(string widname)
    {
        int wid; 
        string str1;
         ADODB.Recordset reccust =null;


        if (widname.ToUpper().Equals("BASECASE") || widname.ToUpper().Equals("BASE CASE")) { return 0; }

        widname = widname.ToUpper();

        str1 = "select tblwhatif.* from tblwhatif where tblwhatif.name = '" + widname + "';";

         DbUse.open_ado_rec(globaldb, ref reccust, str1);
         if (!reccust.EOF)
         {
             wid = (int)reccust.Fields["wid"].Value;
             DbUse.CloseAdoRec(reccust);
             return wid;
         }
         else
         {
             DbUse.CloseAdoRec(reccust);
             return -1;
         }
    }

   public short SaveWhatIfAudit (int lngid ) {
        //on error GoTo Err_SaveWhatIfAudit;

        //Define Variables;
             string strsql;
             string strsql2;
         short retval;

        //Default variables;
        retval = 0;
        runsqlado("UPDATE ZstblwhatifAudit SET ZstblwhatifAudit.WID = " + lngid + ";");

        //Create the SQL statement to delete records;
        strsql = "delete tblWhatIfAudit.* FROM tblWhatIfAudit WHERE ((tblWhatIfAudit.WID=" + lngid + "));";

        //Delete all the records with a matching WID from the;
        //permanent What-if Audit table;

        runsqlado(strsql);

        //Create the SQL statement to append the temporary table;
        //to permanent What-if Audit Table;
        strsql2 = " INSERT INTO tblwhatifaudit ( WID, TableA, TableE, FieldA, FieldE, RecordID, NewVal, OldVal, DataType, Descripton, newshow, oldshow ) SELECT ZstblwhatifAudit.WID, ZstblwhatifAudit.TableA, ZstblwhatifAudit.TableE, ZstblwhatifAudit.FieldA, ZstblwhatifAudit.FieldE, ZstblwhatifAudit.RecordID, ZstblwhatifAudit.NewVal, ZstblwhatifAudit.OldVal, ZstblwhatifAudit.DataType, ZstblwhatifAudit.Descripton, ZstblwhatifAudit.Newshow, ZstblwhatifAudit.Oldshow FROM ZstblwhatifAudit;";


        //Append all the records from the Temporary What-if Table;
        //To the permanent What-if Audit Table;

        runsqlado(strsql2);
        retval = -1;

// return; //exit _SaveWhatIfAudit:
         return retval; //exit  Function;

//Err_SaveWhatIfAudit:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _SaveWhatIfAudit;
    } // end function;


    public void whatif_startup()
    {
        xxx(); //  not used anywhere ...  at initoal start up !!!  check  
        //  checking out  "zstbl_g_whatif_id"
        

        //on error GoTo Err_WhatIf_startup;

         ADODB.Recordset recwhatif = null;
         ADODB.Recordset recAudit = null;

             string strCriteria;
             string pstrcaption;
             string strNewCaption;
             string strwhatif;

            int wid;
            int phase;
            int fstatus;

         DbUse.open_ado_rec(globaldb, ref recwhatif, "zstbl_g_Whatif_id");
        recwhatif.MoveFirst();

        if (((int) recwhatif.Fields["globalwhatifId"].Value != 0))   {
            glngwid = (int) recwhatif.Fields["globalwhatifId"].Value;
            saveWid();
            Gfamilywid = findfamilywid(glngwid);


            runsqlado("UPDATE [zstblWhatIf] SET [zstblWhatIf].[display] = -1 WHERE (([zstblWhatIf].[wid]=" + glngwid + "));");

            DbUse.open_ado_rec(globaldb, ref recAudit, "tblWhatif"); 
            strCriteria = "[WID] = " + (string)(recwhatif.Fields["globalwhatifId"].Value);
            recAudit.Filter = strCriteria;
            if (! (recAudit.BOF))   {
                xxx(); //''  
              
                phase = (int) recwhatif.Fields["whatifload"].Value;
                if (phase  == -1)   {
                    // msgbox("Program stopping in the midst of loading the basecase.");
                    LoadBaseCase();
                    glngwid = 0;
                    saveWid();
                 } else if   (phase  == 1)   {
                    //?  msgbox("Program stopping in phase 1 of loading a whatif.");
                    do_whatif_audit((1));
                 } else if   (phase  == 2)   {
                    //?  msgbox("Program stopping in phase 2 of loading a whatif.");
                    do_whatif_audit((2));
                    };

            } else {
                glngwid = 0;
                saveWid();
                Gfamilywid = 0;
                };
        } else {
            glngwid = 0;
            saveWid();
            Gfamilywid = 0;
            };

// return; //exit _WhatIf_startup:
        if (null !=recAudit)   {
            DbUse.CloseAdoRec(recAudit);
            recAudit =  null;
            };
        if (null != recwhatif)  {
            DbUse.CloseAdoRec(recwhatif);
            recwhatif  = null;
            };
         return; //exit  Sub;

//Err_WhatIf_startup:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _WhatIf_startup;

    } // end sub;

  public    short WriteChanges1(string strtable, string strField, string strID , short intType, string strNVal , ref string strOVal )  {   // vs writechanges2

         //  writechanges1 reads & saves basecase, writechanges2 writes new whatif value  2 phase locking ..  tracks phase of whatif load ...
        //on error GoTo Err_WriteChanges;

        //Define Variables;

         ADODB.Recordset recTable = null;
             string strCriteria;
         short  retval;

        //Default variables;
        retval = 0;

        strtable = strtable.ToLower();

        switch (strtable) {
            case ("tblequip") : {
                strCriteria = "[EquipID] = " + str2i(strID);
            } break;
            case ("tbllabor") : {
                strCriteria = "[LaborID] = " + str2i(strID);
            } break;
            case ("tbloper") : {
                strCriteria = "[OpID] = " + str2i(strID);
            } break;
            case ("tblprodfore") : {
                strCriteria = "[ProdID] = " + str2i(strID);
            } break;
            case ("tbloperfrto") : {
                strCriteria = "[RecID] = " + str2i(strID);
            } break;
            case ("tblgeneral") : {
                strCriteria = "[GeneralID] = " + str2i(strID);
            } break;
            case ("tblibom") : {
                strCriteria = "[ibomID] = " + str2i(strID);
            }break;
                        
           default:  {
                retval = 0;
                return retval; //exit _WriteChanges;
           }
        };  // end of switch;

        //Set varaibales and open table;
         DbUse.open_ado_rec(globaldb, ref recTable, strtable);

        recTable.Filter = strCriteria;

        if (recTable.EOF)   {
            retval = 0;
            return retval; //exit _WriteChanges;
        } else {
            switch (intType) {
                case (YES_NO_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);

                 } break;
                case (BYTE_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                case (INTEGER_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                case (int_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                case (CURRENCY_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                case (SINGLE_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                 case (DOUBLE_TYPE) : {
                     strOVal = Convert.ToString(recTable.Fields[strField].Value);
                 } break;
                 case (DATE_TIME_TYPE) : {
                     strOVal = Convert.ToString(recTable.Fields[strField].Value);
                } break;
                case (TEXT_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                } break;
                case (MEMO_TYPE) : {
                    strOVal = Convert.ToString(recTable.Fields[strField].Value);
                }    break;
                default : {
                    // msgbox("Internal Error: Field type unknown in Whatif records.  Please save model and contact BTD", 0, appl_name);
                    retval = 0;
                    return  retval; //exit _WriteChanges;
                } break;
            };  // end of switch;

            retval = -1;

        };  //end if

// return; //exit _WriteChanges:
        retval  = -1;
        DbUse.CloseAdoRec(recTable);
        recTable =  null;
         return retval; //exit  Function;

//Err_WriteChanges:
    
    } // end function;
     

     public short WriteChanges2 (string strtable, string strField, string strID, short intType, string strNVal, string strOVal)  {
        //on error GoTo Err_WriteChanges;

        //Define Variables;
         short retval;

         ADODB.Recordset recTable = null;
             string strCriteria;
         

        //Default variables;
        retval  = 0;
       
        strtable = strtable.ToLower();
        switch (strtable) {
             case ("tblequip") : {
                strCriteria = "[EquipID] = " + str2i(strID);
            } break;
            case ("tbllabor") : {
                strCriteria = "[LaborID] = " + str2i(strID);
            } break;
            case ("tbloper") : {
                strCriteria = "[OpID] = " + str2i(strID);
            } break;
            case ("tblprodfore") : {
                strCriteria = "[ProdID] = " + str2i(strID);
            } break;
            case ("tbloperfrto") : {
                strCriteria = "[RecID] = " + str2i(strID);
            } break;
            case ("tblgeneral") : {
                strCriteria = "[GeneralID] = " + str2i(strID);
            } break;
            case ("tblibom") : {
                strCriteria = "[ibomID] = " + str2i(strID);
            } break;
            default : {
                retval = 0;
                //ERROR PAGE --  CAN'T FIND TABLE FOR WHATIF RECORD 
                return retval; //exit _WriteChanges;
            }
        };  // end of switch;



         //Set varaibales and open table;
         DbUse.open_ado_rec(globaldb, ref recTable, strtable);

        recTable.Filter = strCriteria;
     
     
        if (recTable.EOF)   {
            retval = 0;
            return retval; //exit _WriteChanges;
        } else {
            switch (intType) {
                 case (YES_NO_TYPE) : {
                    strOVal = strOVal = Convert.ToString((bool)(recTable.Fields[strField].Value));

                    if ((strNVal  == "TRUE"))   {
                        recTable.Fields[strField].Value = (-1);
                    } else {
                        recTable.Fields[strField].Value = (0);
                    };

                    recTable.Update();

                } break;
                case (BYTE_TYPE) : {
                    strOVal =  Convert.ToString((byte)(recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = str2i(strNVal);
                    recTable.Update();

                } break;
                case (INTEGER_TYPE) : {
                    strOVal = Convert.ToString((int)(recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = str2i(strNVal);
                    recTable.Update();

                } break;
                case (int_TYPE) : {
                    strOVal = recTable.Fields[strField].Value.ToString();

                    recTable.Fields[strField].Value = str2i(strNVal);
                    recTable.Update();

                } break;
                case (CURRENCY_TYPE) : {
                    strOVal = Convert.ToString((Decimal)(recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = Convert.ToDecimal(strNVal);  // str2Cur(strNVal);
                    recTable.Update();

                } break;
                case (SINGLE_TYPE) : {
                    strOVal = Convert.ToString((recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = str2f(strNVal);
                    try {
                    recTable.Update();
                    } catch (Exception ex) {
                        string bla = "";
                    }

                } break;
                case (DOUBLE_TYPE) : {
                    strOVal = Convert.ToString((double)(recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = CDbl_ndi(strNVal);
                    recTable.Update();

                } break;
                case (DATE_TIME_TYPE) : {
                    strOVal = Convert.ToString((DateTime)(recTable.Fields[strField].Value));

                    recTable.Fields[strField].Value = strNVal;  //  date ..  time
                    recTable.Update();

                } break;
                case (TEXT_TYPE) : {
                    strOVal = recTable.Fields[strField].Value.ToString();

                    recTable.Fields[strField].Value = strNVal;
                    recTable.Update();

                } break;
                case (MEMO_TYPE) : {
                    strOVal = recTable.Fields[strField].Value.ToString();

                    recTable.Fields[strField].Value = strNVal;
                    recTable.Update();

                } break ;
                default:  {
                    // msgbox("Internal Error: Field type unknown in Whatif records.  Please save model and contact NDI", 0, appl_name);
                    //ERROR PAGE  
                    retval  = 0;
                    return retval; //exit _WriteChanges;
                }
            };  // end of switch;

            retval = -1;
            
            }; // end else if
     

     
// return; //exit _WriteChanges:
        retval  = -1;
        try {
            DbUse.CloseAdoRec(recTable);
        } catch (Exception) { }
        recTable =  null;
         return retval; //exit  Function;
     
//Err_WriteChanges:
        if (retval   == -1)   {
             //RESUME NEXT
        } ; /*else {
            if (Err.Number  == 3316)   {
                //'  // msgbox "Data Error in Whatif field: Invalid entry for Field (e.g. Num machines  == 0), using original value", 0, appl_name;
                WriteChanges2 = 0;
                // resume   return; //exit _WriteChanges;
            } else {
                // msgbox(ErrorToString(), 0, appl_name);
                 //RESUME NEXT
            };  
        }; */
    } // end function;

    public  void flag_run() {
        //on error GoTo err_flag;

      
        model_modified = -1; 
        saveModel_modified();
        

//exit_flag:
         return; //exit  Sub;
//err_flag:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_flag;

    } // end sub;

   

  
    public  void status_flag() {
        //on error GoTo err_flag;

             string Text1;
         int  val1;
         int  val0;

         ADODB.Recordset rec1 = null;

         DbUse.open_ado_rec(globaldb, ref rec1, "SELECT tblwhatif.WID, tblWhatIf.recalc From tblwhatif;");

        rec1.Filter = "[wid] = 0";
        val0 =  ((bool) rec1.Fields["recalc"].Value) ? -1:0 ;
        rec1.Filter = "[wid] = " + glngwid;
        val1 = ((bool)rec1.Fields["recalc"].Value) ? -1 : 0;

            if ((glngwid  == 0))   {
            //'' flag  if run necessary;
            if ((val0 != 0 ))   {
                Text1 = "ReCalc necessary for Base Case";
                };
        } else { 
            if (((val0 != 0) && (val1 == 0)))   {
                Text1 = "ReCalc necessary for Base Case";
             } else if   ((! (val0 == 0)) && (val1!=0))   {
                Text1 = "ReCalc necessary for What-If Scenario" + Get_whatif_name(glngwid);
             } else if   ((val0!=0) && (val1!=0))   {
                Text1 = "ReCalc necessary for Base Case + What-If Scenario" + Get_whatif_name(glngwid);
            } else {
                Text1 = "";
                };

            };



//exit_flag:
        if (!(rec1 == null ))   {
            DbUse.CloseAdoRec(rec1);
            rec1 =  null;
            };
         return; //exit  Sub;
//err_flag:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_flag;

    } // end sub;

     void update_field_desc( ADODB.Recordset auditval) {
        //on error GoTo err1;

             string desc;
            int prodid;
            int opid;
             string prodname;
             string prodname2;
             string opname;
             string opname2;
             ADODB.Recordset rec1 = null;

         string str1;
         string str2;



        str1 = (string) auditval.Fields["tablea"].Value;

             if (str1 == "tbllabor")  {
                 desc = find_item(Convert.ToInt32(auditval.Fields["recORDid"].Value), 0, Labor);

                str2 = Convert.ToString( auditval.Fields["fielda"].Value);
                    if (str2 =="Abst")  {
                        auditval.Fields["FIELDe"].Value = desc + " - % Time Unavailable";
                    } if (str2 == "GrpSiz") {
                        auditval.Fields["FIELDe"].Value = desc + " - Group Size";
                    } if (str2 =="Labcomment") {
                        auditval.Fields["FIELDe"].Value = desc + " - Comment";
                    } if (str2 =="LaborDept") {
                        auditval.Fields["FIELDe"].Value = desc + " - Department/Group";
                    } if (str2 =="OT") {
                        auditval.Fields["FIELDe"].Value = desc + " - Overtime %";
                    } if (str2 =="Run") {
                        auditval.Fields["FIELDe"].Value = desc + " - Run Muktiplier";
                    } if (str2 =="Setup") {
                        auditval.Fields["FIELDe"].Value = desc + " - Setup Multiplier";
                    } if (str2 =="Varbility") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variability Multiplier";
                    } if (str2 =="L1") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("L1");

                    } if (str2 =="L2") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("L2");

                    } if (str2 =="L3") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("L3");

                    } if (str2 =="L4") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("L4");
                    }
             };  // end of if labor;


             if (str1 =="tblEquip") {
                 desc = find_item(Convert.ToInt32(auditval.Fields["recORDid"].Value), 0, equip);
                str2 = Convert.ToString(  auditval.Fields["fielda"].Value);
                    if (str2 =="E1") {
                        auditval.Fields["FIELDe"].Value = desc + "-Variable " + getname("e1");
                    } if (str2 =="E2") {
                        auditval.Fields["FIELDe"].Value = desc + "-Variable " + getname("e2");
                    } if (str2 =="E3") {
                        auditval.Fields["FIELDe"].Value = desc + "-Variable " + getname("e3");
                    } if (str2 =="E4") {
                        auditval.Fields["FIELDe"].Value = desc + "-Variable " + getname("e4");
                    } if (str2 =="Labor") {
                        auditval.Fields["FIELDe"].Value = desc + " - AssignedLabor";
                    } if (str2 =="EqComment") {
                        auditval.Fields["FIELDe"].Value = desc + " - Comment";
                    } if (str2 =="EquipDept") {
                        auditval.Fields["FIELDe"].Value = desc + " - Department";
                    } if (str2 =="GrpSiz") {
                        auditval.Fields["FIELDe"].Value = desc + " - GroupSize";
                    } if (str2 =="MTF") {
                        auditval.Fields["FIELDe"].Value = desc + " - Mean Time to Failure";
                    } if (str2 =="MTR") {
                        auditval.Fields["FIELDe"].Value = desc + " - Mean Time for Repair";
                    } if (str2 =="OT") {
                        auditval.Fields["FIELDe"].Value = desc + " - Overtime%";
                    } if (str2 =="Run") {
                        auditval.Fields["FIELDe"].Value = desc + " - Run Multiplier";
                    } if (str2 =="Setup") {
                        auditval.Fields["FIELDe"].Value = desc + " - Set Multiplier";
                    } if (str2 =="Varbility") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variability Multiplier";
                    }; //end if
                };  // end of if equip;


            if (str1 =="tblprodfore") {
                desc = find_item(Convert.ToInt32(auditval.Fields["recordID"].Value), 0, product);
                str2 = Convert.ToString( auditval.Fields["fielda"].Value);
                    if (str2 =="DemandFac") {
                        auditval.Fields["FIELDe"].Value = desc + " - Demand Multiplier";
                    } if (str2 =="EndDemd") {
                        auditval.Fields["FIELDe"].Value = desc + " - End Demand";
                    } if (str2 =="LotSiz") {
                        auditval.Fields["FIELDe"].Value = desc + " - Lot size";
                    } if (str2 =="Makestock") {
                        auditval.Fields["FIELDe"].Value = desc + " - Make to stock";
                    } if (str2 =="Tbatchgather") {
                        auditval.Fields["FIELDe"].Value = desc + " - Gather TBatches at Stock";
                    } if (str2 =="ProdComment") {
                        auditval.Fields["FIELDe"].Value = desc + " - Comment";
                    } if (str2 =="ProdDept") {
                        auditval.Fields["FIELDe"].Value = desc + " - Group/Dept/Area";
                    } if (str2 =="TransferBatch") {
                        auditval.Fields["FIELDe"].Value = desc + " - Transfer Batch Size";
                    } if (str2 =="Variability") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variability Multiplier";
                    } if (str2 =="P1") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("P1");
                    } if (str2 =="P2") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("P2");
                    } if (str2 =="P3") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable" + getname("P3");
                    } if (str2 =="P4") {
                        auditval.Fields["FIELDe"].Value = desc + " - Variable " + getname("P4");
                    };  // end of switch;
            }// end if product   
            


            if (str1 =="tbloper") {
                 DbUse.open_ado_rec(globaldb, ref rec1, "tbloper");
                 rec1.Filter = "[opid] =" + Convert.ToInt32(auditval.Fields["recORDid"].Value);
                prodid = Convert.ToInt32(  rec1.Fields["ProdFore"].Value);
                prodname = find_item(prodid, 0, product);
                desc = Convert.ToString(  rec1.Fields["opnam"].Value);
                str2 = (string) auditval.Fields["fielda"].Value;
                    if (str2 =="O1") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - " + getname("o1");
                    } if (str2 =="O2") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - " + getname("o2");
                    } if (str2 =="O3") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - " + getname("o3");
                    } if (str2 =="O4") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - " + getname("o4");
                    } if (str2 =="OpNum") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Number";
                    } if (str2 =="PercentAssign") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Percent Assigned";
                    } if (str2 =="Eqid") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Assigned Equipment";
                    } if (str2 =="Labrunlot") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Run time per Lot ";
                    } if (str2 =="Labruntbatch") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Run time per Tbatch";
                    } if (str2 =="LabRunTime") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Run time per Piece ";
                    } if (str2 =="LabSetupPiece") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Setup time per Piece";
                    } if (str2 =="LabSetupTbatch") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Setup time per Tbatch";
                    } if (str2 =="LabSetupTime") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Labor Setup time per Lot";
                    } if (str2 =="Eqrunlot") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Run time per Lot ";
                    } if (str2 =="Eqruntbatch") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Run time per Tbatch";
                    } if (str2 =="EqRunTime") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Run time per Piece";
                    } if (str2 =="EqSetupPiece") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Setup time per Piece";
                    } if (str2 =="EqSetupTbatch") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Setup time per Tbatch";
                    } if (str2 =="EqSetupTime") {
                        auditval.Fields["FIELDe"].Value = prodname + " - " + desc + "  - Equip Setup time per Lot";
                };  // end of if
            } //end if oper


            if (str1 =="tbloperfrto") {
                 DbUse.open_ado_rec(globaldb, ref rec1, "tbloperfrto");
                 rec1.Filter = "[recid] =" + Convert.ToInt32(auditval.Fields["recORDid"].Value);
                prodid = Convert.ToInt32(  rec1.Fields["PartFore"].Value);
                prodname = find_item(prodid, 0, product);
                opname = find_item((int)(rec1.Fields["OpNumF"].Value), prodid, oper);
                opname2 = find_item((int)(rec1.Fields["OpNumT"].Value), prodid, oper);
                auditval.Fields["FIELDe"].Value = "Product " + prodname + " From " + opname + " To " + opname2 + " - Routing percent";
            }  //  end fromto 
 
            if (str1 =="tblibom") {
                 DbUse.open_ado_rec(globaldb, ref rec1, "tblibom");
                 rec1.Filter = "[ibomid] =" + Convert.ToInt32(auditval.Fields["recORDid"].Value);

                prodname = find_item(Convert.ToInt32(rec1.Fields["ParentID"].Value), 0, product);
                prodname2 = find_item(Convert.ToInt32(rec1.Fields["compid"].Value), 0, product);
                auditval.Fields["FIELDe"].Value = "Parent " + prodname + " Component " + prodname2 + " - Units per Assembly";
            }  //  end if ibom

            if (str1 =="General Table") {
                str2 = (string)auditval.Fields["fielda"].Value;

                    if (str2 =="G1") {
                        auditval.Fields["FIELDe"].Value = getname("g1");
                    } if (str2 =="G2") {
                        auditval.Fields["FIELDe"].Value = getname("g2");
                    } if (str2 =="G3") {
                        auditval.Fields["FIELDe"].Value = getname("g3");
                    } if (str2 =="G4") {
                        auditval.Fields["FIELDe"].Value = getname("g4");
                    };  // end of if;
        };  // end of if general;
        auditval.Update();
//exit1:
        if (null != rec1)   {
            DbUse.CloseAdoRec(rec1);
            rec1 =  null;
            };

         return; //exit  Sub;
//err1:
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT
    } // end sub;

    public int  findfamilywid(int wid )  {
        //on error GoTo err1;

        ADODB.Recordset recset1 = null;
        int retval;

        if (wid == 0)   {
            retval = 0;
             return retval; //exit  Function;
            };

         DbUse.open_ado_rec(globaldb, ref recset1, "tblwhatif");
        recset1.Filter = "[wid]= " + wid;
        if (recset1.EOF  == false)   {
            retval = (int) recset1.Fields["Familyid"].Value;
        } else {
            retval = 0;
            };

//exit1:
        if (null != recset1)   {
            DbUse.CloseAdoRec(recset1);
            recset1 =  null;
            };
         return retval ; //exit  Function;
//err1:
       /*  if (Err.Number  == 94)   {
            retval = 0;
             return retval; //exit  Function;
            };
        // msgbox(ErrorToString(), 0, appl_name);
         //RESUME NEXT
       */
    } // end function;



    public ClassC0(string localdir): base(localdir) {

    }
}
   //  end class

