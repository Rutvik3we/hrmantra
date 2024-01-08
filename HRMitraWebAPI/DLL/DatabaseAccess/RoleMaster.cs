using NDataModel;
using NDatabaseHandler;
using NErrorHandler;
using System;
using System.Data;

namespace NDatabaseAccess
{
   public class RoleMaster
    {
        #region "Private Variable"
        private string strSQL;
        private DataAccess objDataAccess;
        private ErrorHandler objErrorLogger;
        //private DataAccess1 _objDataAccess;
        #endregion

        #region "Constructor/Destructor"
        public RoleMaster()
        {
            try
            {
                string connString = NEncryptDecrypt.EncryptDecrypt.GetConnString();
                objErrorLogger = new ErrorHandler("Department");
                objDataAccess = new DataAccess(connString, objErrorLogger);
                //_objDataAccess = new DataAccess1();
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile(ex);
            }
        }

        public RoleMaster(ErrorHandler errorLogger)
        {
            try
            {
                string connString = NEncryptDecrypt.EncryptDecrypt.GetConnString();
                objErrorLogger = errorLogger;
                objDataAccess = new DataAccess(connString, objErrorLogger);
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile(ex);
            }
        }

        public RoleMaster(ErrorHandler errorLogger, DataAccess dataAccess)
        {
            try
            {
                objDataAccess = dataAccess;
                objErrorLogger = errorLogger;
            }
            catch (Exception ex)
            {
                errorLogger.WritetoLogFile(ex);
            }
        }

        ~RoleMaster()
        {
            if (objDataAccess != null)
                objDataAccess = null;

            if (objErrorLogger != null)
                objErrorLogger = null;

            if (strSQL != null)
                strSQL = null;
        }

        #endregion

        #region "RoleMaster"
        public DataTable GetRoleData()
        {
            DataTable objDataTable = null;
            try
            {
                string strSql = "Select Id, ROW_NUMBER() OVER (ORDER BY Id DESC) [SrNo], RollName from RoleMaster  where  ID != 1 " +
                    "Order By Id DESC";
                objDataTable = objDataAccess.GetDataTable(strSql);
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile(ex);
            }
            return objDataTable;
        }

      

        public Boolean InsertRoleData(RoleMasterModel roleMaster, int userId, string MachineName)
        {
            bool retVal = false;
            try
            {
                string[] paramArray = new string[2];
                paramArray[0] = "@RoleName";
                paramArray[1] = "@UserId";

                object[] values = new object[2];
                values[0] = roleMaster.RollName;
                values[1] = userId;

                string strSQL = "InsertRoleDetails";
                retVal = Convert.ToBoolean(objDataAccess.ExecuteNonQuery(strSQL, paramArray, values, CommandType.StoredProcedure));
                //if (retVal)
                //{
                //  string  strSql = "SELECT MAX(Id) From RoleMaster";
                //    int id = Convert.ToInt32(objDataAccess.ExecuteScalar(strSql));

                 
                //    string MsgCode = "InsertRole";
                //    string uname = userId.ToString();
                //    string[] param = { Convert.ToString(id),
                //                       Convert.ToString(roleMaster.RollName)
                //    };
                //    _objDataAccess.SetAudit(MsgCode, uname, MachineName, param);
                //    _objDataAccess.WriteToFile(DateTime.Now + "Role Inserted With Id = " + id);
                //}
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile(ex);
                return false;
            }
            return true;
        }

        public Boolean UpdateRoleData(RoleMasterModel roleMaster, int userId, string MachineName)
        {
            bool retVal = false;

            try
            {
                string strSql = "UPDATE RoleMaster set " +
                    " RollName = '" + roleMaster.RollName + "' Where Id = " + roleMaster.Id + "";
                retVal = Convert.ToBoolean(objDataAccess.ExecuteNonQuery(strSql));
                //if (retVal)
                //{
                //    //strSql = "Select D.Id, ROW_NUMBER() OVER (ORDER BY D.Id DESC) [SrNo], D.Name , dbo.GetListText('Status', D.IsActive) [Status] " +
                //    //"from DesignationMaster D Where D.Id = " + dep.Id + " Order By D.Id DESC";
                //    //objDataTable = objDataAccess.GetDataTable(strSql);
                //    //if (objDataTable != null && objDataTable.Rows.Count > 0)
                //    //{
                //    //    company = Convert.ToString(objDataTable.Rows[0]["CompanyName"]);
                //    //    location = Convert.ToString(objDataTable.Rows[0]["Location"]);
                //    //}

                //    string MsgCode = "UpdateRole";
                //    string uname = userId.ToString();
                //    string[] param = { Convert.ToString(roleMaster.Id),
                //                       Convert.ToString(roleMaster.RollName),
                //                       };
                //    _objDataAccess.SetAudit(MsgCode, uname, MachineName, param);
                //    _objDataAccess.WriteToFile(DateTime.Now + "Department Updated With Id = " + roleMaster.Id);
                //}
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile(ex);
                return false;
            }
            return true;
        }
        #endregion
    }
}
