using System;
//using System.String;
//using System.IO.IsolatedStorage.IsolatedStorageFile;
//using System.IO.FileMode;

using System.Data;
using ADODB;
using System.IO;

using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.IO.IsolatedStorage;

//using System.Globalization.TextInfo;






public class ClassB : ClassA { 


  
    string page_title;

  


   
  
    public const bool ACADEMICMODE = false;
    public const bool STUDENTMODE = false;
    public const bool DEMOMODE = false;
    public const bool pcbdemomode = false;

    public short dead;


    public bool inOperRoutingPages = false;


    // GREG - common practice for programmer is to return named constants rather than hard coded numbers as a return value... Example:
    /*public const int EVERYTHING_OK = 0;
    public const int SOME_MINOR_ERROR = 1;
    public const int BIGGER_ERROR = 2;
    
    int check_size(short item, int prodid) {
        ...
        if (....) {
            retval = EVERYTHING_OK;
        } else if (...) {
            retval = SOME_MINOR_ERROR;
        } else {
            retval = BIGGER_ERROR;
        }
        return retval;
    }
    
    */

    short check_size(short item, int prodid)
    {
        //on error GoTo err_check;

        short retval;

        int LABOR_SIZE;
        int EQUIP_SIZE;
        int PROD_SIZE;
        int OPER_SIZE;
        int ROUTE_SIZE;
        int IBOM_SIZE;
        int count1;

        if (STUDENTMODE || DEMOMODE || pcbdemomode)
        {
            LABOR_SIZE = 7;
            EQUIP_SIZE = 13;
            PROD_SIZE = 13;
            OPER_SIZE = 16;
            ROUTE_SIZE = 13;
            IBOM_SIZE = 11;
        }
        else if (ACADEMICMODE)
        {
            LABOR_SIZE = 12;
            EQUIP_SIZE = 24;
            PROD_SIZE = 24;
            OPER_SIZE = 27;
            ROUTE_SIZE = 27;
            IBOM_SIZE = 21;
        }
        else
        {
            LABOR_SIZE = 6000000;
            EQUIP_SIZE = 6000000;
            PROD_SIZE = 6000000;
            OPER_SIZE = 6000000;
            ROUTE_SIZE = 6000000;
            IBOM_SIZE = 6000000;
        };

        retval = -1;

        switch (item)
        {

            case (Labor):
                {
                    count1 = get_count("SELECT Count(tbllabor.laborDesc) AS Count1 FROM tbllabor;", -1);
                    if ((count1 > LABOR_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
            case (equip):
                {
                    count1 = get_count("SELECT Count(tblequip.EquipDesc) AS Count1 FROM tblequip;", -1);
                    if ((count1 > EQUIP_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
            case (product):
                {
                    count1 = get_count("SELECT Count(tblprodfore.prodDesc) AS Count1 FROM tblprodfore;", -1);
                    if ((count1 > PROD_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
            case (oper):
                {
                    count1 = get_count("SELECT Count(tbloper.OpID) AS count1, tbloper.ProdFore From tbloper GROUP BY tbloper.ProdFore HAVING (((tbloper.ProdFore)=" + prodid + "));", -1);
                    if ((count1 > OPER_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
            case (route):
                {
                    count1 = get_count("SELECT Count(tbloperfrto.RecID) AS count1, tbloperfrto.PartFore From tbloperfrto GROUP BY tbloperfrto.PartFore HAVING (((tbloperfrto.PartFore)=" + prodid + "));", -1);
                    if ((count1 > ROUTE_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
            case (ibom):
                {
                    count1 = get_count("SELECT tblibom.ParentID, Count(tblibom.IbomID) AS count1 From tblibom GROUP BY tblibom.ParentID HAVING (((tblibom.ParentID)=" + prodid + "));", -1);
                    if ((count1 > IBOM_SIZE))
                    {
                        retval = 0;
                    };
                    break;
                }
        };  // end of switch;

        //exit_check:
        return retval; //exit  Function;

//err_check: 
        //;
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT
    } // end function;

    string trans_name2num(string str1, int prodid, string field1)
    {
        //on error GoTo err_n2num;

        string str2;
        string retval;

        str2 = str1;

        retval = str1;


        if (field1.Equals("labordesc")) retval = str1;
        else if (field1.Equals("labordept")) retval = str1;
        else if (field1.Equals("lretbcomment")) retval = str1;

        else if (field1.Equals("equipdesc")) retval = str1;
        else if (field1.Equals("equipdept")) retval = str1;
        else if (field1.Equals("eqcomment")) retval = str1;
        else if (field1.Equals("equiptype")) retval = str1;
        else if (field1.Equals("lretbor")) retval = str1;

        else if (field1.Equals("proddesc")) retval = str1;
        else if (field1.Equals("proddept")) retval = str1;
        else if (field1.Equals("prodcomment")) retval = str1;

        else if (field1.Equals("OpNretm")) retval = str1;
        else if (field1.Equals("EqID")) retval = str1;
        else if (field1.Equals("percentretssign")) retval = str1;

        else if (field1.Equals("eqsetuptime")) retval = str1;
        else if (field1.Equals("eqsetuptbrettch")) retval = str1;
        else if (field1.Equals("eqsetuppiece")) retval = str1;
        else if (field1.Equals("eqruntbrettch")) retval = str1;
        else if (field1.Equals("eqrunlot")) retval = str1;
        else if (field1.Equals("eqruntime")) retval = str1;
        else if (field1.Equals("lretbsetuptime")) retval = str1;
        else if (field1.Equals("lretbsetuptbrettch")) retval = str1;
        else if (field1.Equals("lretbsetuppiece")) retval = str1;
        else if (field1.Equals("lretbruntbrettch")) retval = str1;
        else if (field1.Equals("lretbrunlot")) retval = str1;
        else if (field1.Equals("lretbruntime")) retval = str1;

        else if (field1.Equals("PER")) retval = str1;

        else if (field1.Equals("OpNumF")) retval = str1;
        else if (field1.Equals("OpNumT")) retval = str1;

            //'======================================;
        //'       'operation times etc.;
        //'       } case ("perassign") : {
        //'            trans_name2num = str1;
        //'======================================;
        else
        {
            retval = CDbl_ndi(str1);
        };  // end of if ...

        //exit_n2num:
        return retval; //exit  Function;
        //err_n2num: ;
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_n2num;
    } // end function;



    float numberval(string str1) 
    {
        float retval;

        retval = 0.0F;
        retval = Convert.ToSingle(str1); 

        return retval;
    }


    public string find_item(int item_no, int item_no2, int item_type)
    {

        //on error goto Err_find_item;

        //Define variables;

        ADODB.Recordset reccust = null;
        string strtbl = "";
        string retval;

        retval = " ";

        switch (item_type)
        {
            case (Labor):
                {
                    strtbl = "SELECT DISTINCTROW tblLabor.laborDesc AS name FROM tblLabor WHERE ((tblLabor.LaborID=" + item_no + "));";
                }; break;
            case (equip):
                {
                    strtbl = "SELECT DISTINCTROW tblEquip.EquipDesc AS name FROM tblEquip WHERE ((tblEquip.EquipID=" + item_no + "));";
                }; break;
            case (product):
                {
                    strtbl = "SELECT DISTINCTROW tblProdFore.ProdDesc AS name FROM tblProdFore WHERE ((tblProdfore.ProdID=" + item_no + "));";
                }; break;
            case (oper):
                {
                    strtbl = "SELECT DISTINCTROW tblOper.opnam AS name FROM tblOper WHERE ((tbloper.opID=" + item_no + ") and (tbloper.prodfore =" + item_no2 + "));";
                }; break;
            case (route):
                {
                    strtbl = "SELECT DISTINCTROW tblOper.OpNam as name, tblOperFrTo.OpNumF FROM (tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.PartFore = tblProdFore.ProdID) INNER JOIN tblOper ON tblOperFrTo.OpNumF = tblOper.OpID WHERE ((tblOperFrTo.opnumF=" + item_no + ") and (tbloperfrto.partfore =" + item_no2 + "));";
                }; break;
            case (ibom):
                {
                    strtbl = "SELECT DISTINCTROW tblProdFore.ProdDesc as name FROM tblIbom INNER JOIN tblProdFore ON tblIbom.COMPID = tblProdFore.ProdID WHERE (([tblIBOM].[ParentID]=" + item_no + ") AND ([tblibom].[compid]=" + item_no2 + "));";
                }; break;
            case (Whatif):
                {
                    strtbl = "SELECT DISTINCTROW tblWhatif.Name AS name FROM tblwhatif WHERE ((tblwhatif.wID =" + item_no + "));";
                }; break;
            case (Eq_type):
                {
                    strtbl = "SELECT DISTINCTROW tblequiptype.equiptypeName AS name FROM tblequiptype WHERE ((tblequiptype.typeID=" + item_no + "));";
                }; break;
        };  //'' end of switch;

        DbUse.open_ado_rec(globaldb, ref reccust, strtbl);


        if ((reccust.EOF))
        {
            retval = " ";
        }
        else
        {
            retval = (string)reccust.Fields["Name"].Value;
        };


        //exit_find_item:

        if (null != reccust)
        {
            DbUse.CloseAdoRec(reccust);
            reccust = null;
        };
        return retval; //exit  Function;

 //err_find_item: ;
        //' msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;

    string trans_num2name(string str1, int prodid, string field1)  
    {
        //on error GoTo err_n2name;

        string str2;


        str2 = "ERROR";

        if (field1.Equals("labor"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, Labor);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)  
            {
                str2 = "None";
            }
        }
        else if (field1.Equals("laborid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, Labor);
            str2 = str2.Trim();
            if (str2.Trim().Length == 0)
            {
                str2 = "None";

            }
        }
        else if (field1.Equals("equiptype"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, Eq_type);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "None";
            }
        }

        else if (field1.Equals("equipid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, equip);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "None";
            }
        }
        else if (field1.Equals("eqid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, equip);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "None";
            }
        }
        else if (field1.Equals("partid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("prodid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("prodfore"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("partfore"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("parentid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            str2 = str2.Trim();
            if (str2.Length == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("compid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), 0, product);
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("operid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("opid"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("fromoper"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("opnumf"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            str2 = str2.Trim();
            if (str2.Length == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("tooper"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            str2 = str2.Trim();
            if (str2.Trim().Length  == 0)
            {
                str2 = "No Name";
            }
        }
        else if (field1.Equals("opnumt"))
        {
            str2 = find_item((int)numberval(CDbl_ndi(str1)), prodid, oper);
            str2 = str2.Trim();
            if (str2.Length == 0)
            {
                str2 = "No Name";
            }
        }
        else
        {
            str2 = str1;
        };  // end of if;

        //exit_n2name:
        return str2; //exit  Function;
        //err_n2name: ;
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_n2name;
    } // end function;

   public  void match_delay_equip(short useval)
    {
        //on error GoTo err_match;

        ADODB.Recordset rec1 = null;
        string str1;
        int delay_id;
        int stand_id;

        //useval = 2 force the match and only in basecase ...;
        if (glngwid != 0) return; //exit  Sub;
        //Open();
        str1 = "SELECT tblequip.* FROM tblequip;";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        delay_id = find_nameItem("Delay", 0, Eq_type, 0);
        stand_id = find_nameItem("standard", 0, Eq_type, 0);

        while (!rec1.EOF)
        {

            if ((useval == 0))
            {
                //'  using type to set grpsize;
                if (((int)rec1.Fields["EQUIPTYPE"].Value) == delay_id) // gerg - I'm not sure if this will work, if not try int.Parse(rec1.Fields["EQUIPTYPE"].Value.ToString()) == delay_id
                {
                    rec1.Fields["grpsiz"].Value = -1;
                }
                else if (((short) rec1.Fields["grpsiz"].Value) == -1)
                {
                    //' standard type but grpsiz = -1;
                    rec1.Fields["grpsiz"].Value = 1;
                   
                };
            }
            else if ((useval == 1))
            {
                if (((short)rec1.Fields["grpsiz"].Value) == -1)
                {
                    rec1.Fields["EQUIPTYPE"].Value = delay_id;
                }
                else if ((int) rec1.Fields["EQUIPTYPE"].Value != delay_id)
                {
                    //' grpsiz = 2, type = delay ??;
                    rec1.Fields["EQUIPTYPE"].Value = stand_id;
                };

            }
            else if ((useval == 2))
            {
                //'' force to -1 / delay if either is set.;
                if (short.Parse(rec1.Fields["grpsiz"].Value.ToString()) == -1)
                {
                    rec1.Fields["EQUIPTYPE"].Value = delay_id;
                }
                else if ((int)rec1.Fields["EQUIPTYPE"].Value == delay_id)
                {
                    rec1.Fields["grpsiz"].Value = -1;
                };
            };

            rec1.Update();
            rec1.MoveNext();
        }; // end of while ...

        //exit_match:
        DbUse.CloseAdoRec(rec1);
        rec1 = null;
        return; //exit  Sub;
        //err_match: ;
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_match;
    } // end sub;

    void getpages(short totpages, string TITLE, short startp, short endp)
    {
        //on error GoTo err_getPages;

        //Open Form to get new name of entity;
        page_title = TITLE;




        //exit_getpages:
        return; //exit  Sub;
        //err_getPages:
        //endp = 0;
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT
    } // end sub;

    short Read_File(string file_name)
    {

        string [] sBuffer ;

        //  needs translation - just reading the whole file to insure it has been written by other code ....

        if (File.Exists(file_name))
        {

            using (StreamReader fin = new StreamReader(file_name)) {

            sBuffer = File.ReadAllLines(file_name);

            fin.Close();
            }

            return (-1);
        }
        return 0;

    } // end function;

    // GREG - copy file example:
    // string oldPath = "C:\\OldData\\MyFile.txt";
    // string newPath = "C:\\NewData\\MyFile.txt";
    // File.Copy(oldPath, newPath, true); // make a copy of the file in the user directory; if it already exists under newPath name it gets rewritten
    //
    // rename file example:
    // string oldNamePath = "C:\\Data\\MyFile.txt";
    // string newNamePath = "C:\\Data\\YourFile.txt";
    // File.Move(oldNamePath, newNamePath); // rename the file
    public short Copy_File(string from_name, string to_name)
    {
        //on error GoTo err_copy;

        long size1;
        long size2;
        short ret;
        int i;
        int k;

        short retval;

        // ''k = VB.Timer() + 1;
        // ''Do;
        // ''    System.Windows.Forms.Application.DoEvents();
        // ''Loop   while ((VB.Timer() < k)) {

        retval = 0;

        from_name = from_name.ToUpper();
        to_name = to_name.ToUpper(); 
        if (from_name == to_name)
        {
            retval = -1;
            return retval; //exit  Function;
        };


        
        // GERG - example how to get file size in bytes
        FileInfo fileInfo = new FileInfo(from_name);
        size1 = (int) fileInfo.Length;

        if ((from_name.IndexOf("mpxmdb.mdb") >= 0) || (to_name.IndexOf("curr_mpx.xxx") >= 0))
        {

            //  GD?  // gd xxx  tomorrow  tomorrow  ERRORMSG   !!  got a match ?? raise an error ...

            if (null != globaldb)
            {
                DbUse.CloseAdo(globaldb);
                globaldb = null;
            };
        };

        ret = 0;
        // GREG GERG - I suggest having an directory for user models and keep mpxmdb.mdb as well as curr_mpx.xxx in the directory above - this way it doesn't matter what name the user models have
        // see my models.aspx page to get the idea
        if (!from_name.ToUpper().Equals(to_name.ToUpper()))
        {

            ret = testread(from_name);
            if ((ret == 0))
            {
                if ((from_name.IndexOf("curr_mpx.xxx") >= 0))
                {
                    // msgbox("INTERNAL ERROR: Data Table Locked, Please restart Program. ", 0, appl_name);

                    dead = -1;
                    //Stop;
                    return 0; //exit  Function;
                }
                else
                {
                    // msgbox("Copy File Error: MPX cannot read file " + from_name, 0, appl_name);

                };
                retval = 0;
                return retval;
            };
            ret = testwrite(to_name);
            if ((ret == 0))
            {
                if (to_name.IndexOf("curr_mpx.xxx") != -1)
                {
                    // msgbox("INTERNAL ERROR: Data Table Locked, Please restart Program. ", 0, appl_name);
                    //  ret = closeeverything();
                    dead = -1;
                    //Stop;
                    return 0; //exit  Function;
                }
                else
                {
                    // msgbox("Copy File Error: MPX cannot write to file " + to_name, 0, appl_name);
                };
                retval = 0;
                return 0;
            };

            
            File.Copy(from_name, to_name,true); 
        };


        FileInfo newFileInfo = new FileInfo(to_name);
        size2 = newFileInfo.Length;

        if ((size1 != size2))
        {    //tomorrow  ERRORMSG  xxx
            // msgbox("System error: " + appl_name + " could not copy file " + from_name + " to " + to_name + ".  Please retry or check on file name or disk space etc.", 48, appl_name);
            retval = 0;
        };

        retval = Read_File(to_name);

        retval = -1;

exit_copy:

        // do regardless of action !!!
        if (null == globaldb)
        {
            DbUse.open_ado(ref globaldb, varlocal + "\\mpxmdb.mdb");
        };

        if ((from_name.IndexOf("mpxmdb.mdb") != -1) || (to_name.IndexOf("curr_mpx.xxx") != -1))
        {
            if (null == globaldb)
            {
                DbUse.open_ado(ref globaldb, varlocal + "\\mpxmdb.mdb");
            };
        };
        return retval; //exit  Function;

    } // end function;

    public string Get_whatif_comment(int wid)
    {
        //on error GoTo Err_Get_whatif_name;

        ADODB.Recordset recset1 = null;
        string retval;

        if (wid == 0)
        {
            retval = "Base Case";
        }
        else
        {
            DbUse.open_ado_rec(globaldb, ref recset1, "tblwhatif");
            recset1.Filter = "[wid] =" + wid;
            retval = (string)recset1.Fields["Comm"].Value;
            DbUse.CloseAdoRec(recset1);
            recset1 = null;
        };

        // return; //exit _Get_whatif_name:
        return (retval); //exit  Function;

        //Err_Get_whatif_name:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;

    public int testresetwid(int wid)
    {
        ADODB.Recordset recset1 = null;
        int retval;

        if (wid == 0)
        {
            retval = -1;
            return retval;
        }
         DbUse.open_ado_rec(globaldb, ref recset1, "Select tblwhatif.* from tblwhatif where wid = "+ wid +";");
            if (!recset1.EOF)           
                   retval = -1;
             else {
                retval = 0;
                glngwid = 0; //  !!!  reset to 0 basecase !!
                saveWid();
                saveRecalcNeeded(0, -1);
                model_modified = -1;
                saveModel_modified();
            }

            DbUse.CloseAdoRec(recset1);
            recset1 = null;

            return retval;



    } 
   public string Get_whatif_name(int wid)
    {
        //on error GoTo Err_Get_whatif_name;

        ADODB.Recordset recset1 =null;
        string retval;

        if (wid == 0)
        {
            retval = "Basecase";
        }
        else
        {
            DbUse.open_ado_rec(globaldb, ref recset1, "Select tblwhatif.* from tblwhatif where wid = "+ wid +";");
            if (recset1.EOF)           
                   retval = "";
             else  retval = (string) recset1.Fields["Name"].Value;

            DbUse.CloseAdoRec(recset1);
            recset1 = null;
        };

        return (retval); //exit  Function;

        //Err_Get_whatif_name:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;


   public long Get_whatif_wid(string whatifname)
   {
       //on error GoTo Err_Get_whatif_name;

       ADODB.Recordset recset1 = null;
       long retval;

       if (whatifname == "Base Case")
           return(0);
      
       else
       {
           DbUse.open_ado_rec(globaldb, ref recset1, "Select tblwhatif.* from tblwhatif where name = " + whatifname + ";");
           if (recset1.EOF)
               retval = -1;
           else retval = (long)recset1.Fields["wid"].Value;

           DbUse.CloseAdoRec(recset1);
           recset1 = null;
       };

       return (retval); //exit  Function;

   

   } // end function;


   int findperiod(string Filename)
   {
       //on error GoTo Err_findperiod;

       int ilen;
       int pos;
       int oldpos;
       int retval;

       ilen = Filename.Length;
       if (ilen < 3)
       {
           retval = ilen;
       }
       else
       {


           pos = -1;
           
           pos = Filename.LastIndexOf("\\");
           pos = Filename.LastIndexOf(".", pos + 1);
           if ((pos >= 0))
            {
                retval = pos;
            }
            else
            {
               retval = ilen;
            };
        };


       // return; //exit _findperiod:
       return (retval); //exit  Function;

       //Err_findperiod:
       // msgbox(ErrorToString(), 0, appl_name);
       // resume   return; //exit _findperiod;
   } // end function;

    int lastInst(string Filename)
    {
        //on error GoTo Err_LastInst;

        int ilen;
        int pos;
        int oldpos;
        int retval;


        ilen = Filename.Length;
        if ((ilen < 3))
        {
            retval = ilen;
        }
        else
        {
            
            pos = -1;
            do
            {
                oldpos = pos;
                pos = Filename.IndexOf("\\", oldpos + 1);  
            } while (pos != -1);

            pos = Filename.IndexOf(".", oldpos);  
            if ((pos >= 0))
            {
                retval = pos;
            }
            else
            {
               retval = ilen;
            };
        };

        // return; //exit _LastInst:
        return (retval); //exit  Function;

        //Err_LastInst:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _LastInst;
    } // end function;

    int lastSlash(string Filename)
    {
        //on error GoTo Err_LastSlash;

        int ilen;
        int pos;
        int oldpos;
        int retval;

        ilen = Filename.Length;
        if ((ilen < 3))
        {
            retval = ilen;
        }
        else
        {
            pos = 1;
            do
            {
                oldpos = pos;
                pos = Filename.IndexOf("\\", oldpos + 1);   //  was InStr(oldpos + 1, Filename, "\\");
            } while (pos != -1);

            pos = Filename.IndexOf("\\", oldpos);  // wasInStr(oldpos, Filename, "\\");
            if ((pos >= 0))
            {
                retval = pos;
            }
            else
            {
                retval = ilen;
            };
        };

        // return; //exit _LastSlash:
        return (retval); //exit  Function;

        //Err_LastSlash:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _LastSlash;
    } // end function;


    //  globals - statics for a set of subroutines ???

    const short max_line = 1500;
    float max_x;

   public int[] parentX;
   public int[] parentY;
   public int[] compX;
   public int[] compY;
   public int depth;

// max nice round value
 public static  float FULL_SCALE(float X)
    {
        //on error GoTo err_max;
        int P1;
        int i1;
        int i2;
        float x1;
        float x2;
        float retval;

        if ((X == 0.0))
        {
            retval = 1.0F;
            return retval; //exit  Function;
        };

        x1 = (float) ( Math.Log(X) / Math.Log(10.0F));

        if ((x1 <= 0.0))
        {
            retval = 1.0f;
            return retval; //exit  Function;
        };

        P1 = (int)x1;

        x1 = (float) ( X * System.Math.Exp(System.Math.Log(10) * -1 * P1));
        x2 = (float) (X * System.Math.Exp(System.Math.Log(10) * (-1 * P1 + 1)));

        i1 = (int)x1;//'  /* between 1 and 9.9 truncated */;
        i2 = (int)x2; //   /* between 10 and 99 */;



        if ((i2 < 12))
        {
            retval = (float) (1.25 * System.Math.Exp(Math.Log(10) * P1));
            return retval; //exit  Function;
        };
        if ((i2 < 14))
        {
            retval = (float)(1.5 * System.Math.Exp(Math.Log(10) * P1));
            return retval; //exit  Function;
        }
        if ((i2 < 20))
        {
            retval = (float) (2.0 * System.Math.Exp(Math.Log(10) * P1));
            return retval; //exit  Function;
        };

        x1 = i1;
        retval = (float) ((x1 + 1.0) * (float) (System.Math.Exp(Math.Log(10) * P1)));

        //exit_max:
        return retval; //exit  Function;
        //err_max:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_max;

    } // end function;



    // GREG - LEFT OVER STUFF FROM VBASIC
    // Math.Min(a1, a2);
    // Math.Max(i, j);
    int minimum(int a1, int a2)
    {
        //on error GoTo err_min;

        
        /*if ((a1 > a2))
        {
            return a2;
        }
        else
        {
            return a1;
        };*/
        return Math.Min(a1, a2);

        //exit_min:
        //return a2; //exit  Function;
        //err_min:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_min;

    } // end function;

    int maximum1(int i, int j)
    {
        //on error  //RESUME NEXT

        /*if ((i > j))
        {
            return i;
        }
        else
        {
            return j;
        };*/
        return Math.Max(i, j);

    } // end function;

    void NotInList()
    {
        //on error GoTo Err_NotInList;

        //Display custom error message;
        // msgbox("Name Does Not Exist" + "\r\n" + "Please Click Add New Button To Add or" + "\r\n" + "Select From Drop List", , "Not In List");

        //Clear Drop box;
        System.Windows.Forms.SendKeys.Send("{ESC}");

        // return; //exit _NotInList:
        return; //exit  Sub;

        //Err_NotInList:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _NotInList;
    } // end sub;

    public int Count;  //  counts # times run then erases database --  nothing in it... a.mdb !!

  public   float RunSql2(string str1, short level1)
    {
        //on error GoTo err_RunSql2;

        ADODB.Recordset reccust = null;

        short ret;
        int i;
        float retval;
        string str2;

        retval = -1;

        if (globaldb_a == null) {
            try {
                if (!File.Exists("a.xxx")) {
                    File.Copy(DbUse.GetMainDirectory() + "App_Data\\" + "a.xxx", varlocal + "a.xxx"); 
                }
                if (!File.Exists("b.xxx")) {
                    File.Copy(DbUse.GetMainDirectory() + "App_Data\\" + "b.xxx", varlocal + "b.xxx");
                }
            } catch (Exception) { }
            globaldb_a = new ADODB.Connection();
            Count = 0;
        } else {
            Count++;
            DbUse.CloseAdo(globaldb_a);
        }
        if (Count > 2000) {
            try {
                File.Copy("b.xxx", "a.xxx", true);
                Count = 0;
            } catch (Exception) {
                
            }
        }
        DbUse.open_ado(ref globaldb_a, "a.xxx");

        //  DOES A CONVERSION OF AN EXPRESSION (6+7*4) TO A NUMBER...

        if ((str1.Trim() == "'") | (str1.Trim() == ".") | (str1.Trim() == ","))
        {
            retval = (float)0.0;
            return retval;
        }

        str2 = "SELECT tbldummy.GeneralID, " + str1 + " AS a1 FROM tbldummy;";
        DbUse.open_ado_rec(globaldb_a, ref reccust, str2);


        try
        {
            // xxx  tomorrow  on run times  += <> /()*.,   no  '"?:;[]{}|&!@#$%^&    NO: [0-31]  33-39  42 58,59 63 64 91-96  123-127
            retval = (float)(double.Parse(reccust.Fields["a1"].Value.ToString()));

                     
        }
        catch (Exception e)
        {    // ERRORMSG tomorrow 
            //MyUtilities.MsgBox("Error in solving Operation Time/Data Equation:  " + conn.ConnectionString + " (" + e.Message + ")");


            string str3;
            str3 = e.Message;
            //retval = (float) -777.0;
            retval = float.NaN;
            
        } finally {
            DbUse.CloseAdoRec(reccust);
            DbUse.CloseAdo(globaldb_a);

            //gwwdd  adding error here !!!
            if (float.IsNaN(retval) || retval < -100)
            {

                try {
                    DbUse.open_ado_rec(globaldb, ref reccust, "zstblerrors");
                    reccust.AddNew();

                    if (Oper_err_id > 0)
                    {
                        reccust.Fields["Level"].Value = 0; // Error;
                        calc_return |= ERR_FLAG;
                        reccust.Fields["Table"].Value = "Operations";
                        string errorMsg = " Product " + Part_err_name + " Operation " + Oper_err_name + " has an operation time which is not a number. ";
                        if (!inOperRoutingPages) {
                            errorMsg += "Go to Product Operations/Routing Page, hit the [Show Real Times] button and look for field value NaN.";
                        } else {
                            errorMsg += "Look for field value NaN.";
                        }
                        reccust.Fields["Text"].Value = errorMsg;
                        reccust.Fields["mpxFlag"].Value = -1;
                        reccust.Fields["partid"].Value = Part_err_id;
                        reccust.Fields["operid"].Value = Oper_err_id;
                        reccust.Fields["Wid"].Value = glngwid;
                        reccust.Update();

                    }
                    else
                    {  //  ROUTING DATA ERROR.
                   
                            reccust.Fields["Level"].Value = 0; // Error;
                            calc_return |= ERR_FLAG;
                            reccust.Fields["Table"].Value = "Routing";
                            string errorMsg = " Product " + Part_err_name + " FROM Operation " + Oper_err_name + " has an % routing value that is not a number. "; 
                            if (!inOperRoutingPages) {
                                errorMsg += "Go to Product Operations/Routing Page, Scroll down to the ROUTING AREA hit the [Show Real Percentage] button and Look for field value NaN. ";
                            } else {
                                errorMsg += "Look for field value NaN.";
                            }
                            reccust.Fields["Text"].Value = errorMsg;
                            reccust.Fields["mpxFlag"].Value = -1;
                            reccust.Fields["partid"].Value = Part_err_id;
                            reccust.Fields["operid"].Value = -1 * Oper_err_id;
                            reccust.Fields["Wid"].Value = glngwid;
                            reccust.Update();
                    
                    }
                } catch (Exception exp) {
                    LogFiles logFiles = new LogFiles(this.username);
                    logFiles.ErrorLog(exp);
                } finally {
                    DbUse.CloseAdoRec(reccust);
                }

                

            }
        }
        
        return retval; 

        //err_RunSql2:
        /*if (level1 == 1)
        {
            retval = -999;
            return retval; //exit  Function;
        };*/
             /*   if ((Err.Number == 3075))
                {
                    //parathesis problem;
                    // msgbox("Error in evaluating string " + str1 + " Parathesis Problem? " + ErrorToString() + " Using -999 as the value", 0, appl_name);

                    //unsolved parameter;
                    // msgbox("Error in evaluating string " + str1 + " Unresolved Parameter? " + ErrorToString() + " Using -999 as the value", 0, appl_name);

                    //divide by zero error;
                    // msgbox("Error in evaluating string " + str1 + " Divide by zero error? " + ErrorToString() + " Using -999 as the value", 0, appl_name);

                    //a parameter has a null value. Check 'switch' and 'iif';
                    // msgbox("Error in evaluating string " + str1 + " Null value! iif or Switch Problem? " + ErrorToString() + " Using -999 as the value", 0, appl_name);

                    // msgbox("Error in evaluating string " + str1 + " Unknown Problem? " + ErrorToString() + " Using -999 as the value", 0, appl_name);
                    retval = -999;
                    return retval; //exit  Function;
                };
                    */
        //retval = -999;
        //return retval;
        //RESUME NEXT

    } // end function;




    void xxxset_dir_at_start()
    {
        //on error GoTo err_set_dir;

        string varlocal_1;

        /* varlocal_1 = strleft(varlocal, 1);
         //ChDrive((varlocal_1));
        
         varlocal_1 = varlocal;
         if ((varlocal.Length > 3))
         {
             varlocal_1 = strleft(varlocal, varlocal.Length - 1);
         };
         ChDir((varlocal_1));
            * */

        ///Directory.SetCurrentDirectory(varlocal);
     
        //exit_set_dir:
        return; //exit  Sub;

        //err_set_dir:
      
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_set_dir;

    } // end sub;

    float availtime()
    {
        //on error GoTo err_availtime;

        ADODB.Recordset reccust = null;
        float X;

        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        X = ((float)reccust.Fields["RTU1b"].Value) * ((float)reccust.Fields["RTU1c"].Value);
        if ((X <= 0))
        {
            X = 1;
        };



        //exit_availtime:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return X; //exit  Function;

        //err_availtime:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_availtime;

    } // end function;

    public string get_Forecast_time_unit()
    {
        //on error GoTo err_forecast_time_name;

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = (string) reccust.Fields["TUFor"].Value;

        //exit_forecast_time_name:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_forecast_time_name:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_forecast_time_name;

    } // end function;

   public  string get_lead_time_unit()
    {
        //on error GoTo err_lead_time_name;

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = (string) reccust.Fields["TULT"].Value;

        //exit_lead_time_name:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_lead_time_name:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_lead_time_name;

    } // end function;

    void set_create_date()
    {
        //on error GoTo err_setdate;

        ADODB.Recordset reccust = null;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();
        //'reccust.edit;
        reccust.Fields["DateC"].Value = System.DateTime.Now;
        reccust.Update();

        DbUse.CloseAdoRec(reccust);
        reccust = null;


        //exit_setdate:
        return; //exit  Sub;
        //err_setdate:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    void set_modified_date()
    {
        //on error GoTo err_setdate;

        ADODB.Recordset reccust = null;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();
        //'reccust.edit;
        reccust.Fields["DateM"].Value = System.DateTime.Now;
        reccust.Update();



        //exit_setdate:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return; //exit  Sub;
        //err_setdate:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    void set_utlimit(float x1)
    {
        //on error GoTo err_setlimit;

        ADODB.Recordset reccust = null;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();
        //'reccust.edit;
        reccust.Fields["utlimit"].Value = x1;
        reccust.Update();


        //exit_setlimit:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return; //exit  Sub;
        //err_setlimit:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    public string get_op_time_unit() {
        //on error GoTo err_op_name;

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = (string) reccust.Fields["TUProd"].Value;

        //exit_op_name:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_op_name:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_op_name;

    } // end function;


    public string get_utlimit ()
    {
        //on error GoTo err_utlimit;

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = Convert.ToString ( (float)  reccust.Fields["utlimit"].Value);

        //exit_utlimit:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_utlimit:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_utlimit;

    } // end function;


    public string get_gen_t2()
    {
        

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = Convert.ToString((float)reccust.Fields["rtu1c"].Value);

     
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
       
    } // end function;

    int get_gen_id()
    {
        //on error GoTo err_genid;

        ADODB.Recordset reccust = null;
        int retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = (int) reccust.Fields["generalid"].Value;
       
        //exit_genid:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_genid:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_genid;

    } // end function;


    public short testread(string str1)
    {
        //on error GoTo err1;
        short retval;

        retval = 0;

        if (! File.Exists(str1) ) {
              return 0;
       }
        
        retval = -1;
        //exit1:
        return retval; //exit  Function;
        //err1:
        //  '' testread = 0;
        // resume  exit1;

    } // end function;

    short testwrite(string str1)
    {
        //on error GoTo err1;
        short retval;

        retval = 0;
        try {
            using (StreamWriter  fout = new  StreamWriter(str1)) {     
                fout.Close();
            }
            retval = -1;
        } catch (Exception) { }

        //exit1:
        return retval; //exit  Function;
        //err1:
        //testwrite = 0;
        // resume  exit1;

    } // end function;

    bool MPXMDB_squeeze()
    {
        //on error GoTo err1;

        short ret;



        globaldb.Close();



        //'xx  TBD TBD  ret = compactfile(varlocal + "\\mpxmdb.mdb");

        DbUse.open_ado(ref globaldb, varlocal + "\\mpxmdb.mdb");

        //exit1:
        return true; //exit  Function;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit1;
    } // end function;

    void put_to_clip(string str1)
    {
        //on error  //RESUME NEXT


    } // end sub;

    // Don't diaplay error message, just ignore it, if error number is 3426, 91, 5;
    // This function will be call for all form load, unload, active and deactive procedure.;
    // Now it is equil to resume next;

    short check_error_number(short error_number)
    {
        //on error  //RESUME NEXT

        if (((error_number != 3426) && (error_number != 91) && (error_number != 5) && (error_number != 3265) && (error_number != 438) && (error_number != 524)))
        {
            return -1;
        }
        else
        {
            return 0;
        };
    } // end function;


   public  string CDbl_ndi(string str1)
    {
        //on error GoTo err2;
        string ret;

       str1 = str1.Trim();
        if (str1.Length == 0)

            
            
        {
            ret = "0";
        }
        else if ((str1 == "."))
        {
            ret = "0";
        }
        else
        {
            ret = str1;  //''CDbl(str1);
        };//



        //exit2:
        return ret; //exit  Function;
        //err2:
                    /*if ((Err.Number == 13))
                    {
                        ret = "0";
                        return "0";
                    };
                    // msgbox(ErrorToString(), 0, appl_name);
                    //RESUME NEXT
                    */
    } // end function;



   public ClassB(string localdir):base(localdir){
       
   }
}  //End Class;  
