using System;
using System.Data;
using NErrorHandler;
using NDatabaseHandler;
using NDataModel;

namespace NDatabaseAccess
{
    public class GeneralDB
    {
        #region "Private Variable"
        private string _strSQL;
        private DataAccess _objDataAccess;
        private ErrorHandler _objErrorLogger;
        #endregion

        #region "Constructor/Destructor"
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralDB(string connString)
        {
            try
            {
                _objErrorLogger = new ErrorHandler(this.GetType().Name);
                _objDataAccess = new DataAccess(connString, _objErrorLogger);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~GeneralDB()
        {
            if (_objDataAccess != null)
            {
                _objDataAccess = null;
            }
            if (_objErrorLogger != null)
            {
                _objErrorLogger = null;
            }
            if (_strSQL != null)
            {
                _strSQL = null;
            }
        }
        #endregion

        //#region "Public Method"
        //public bool SetActiveInActiveStatus(SetActiveInactiveModel mdlSetActiveInactive)
        //{
        //    bool retFlag = false;
        //    try
        //    {
        //        string _strSql = "SetActiveInActive";
        //        string[] param = new string[4] { "@TableName", "@Id", "@StatusFlag", "@UpdatedBy" };
        //        object[] value = new object[4] { mdlSetActiveInactive.TableName, mdlSetActiveInactive.Id, mdlSetActiveInactive.StatusFlag, mdlSetActiveInactive.UpdatedBy };
        //        //retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(_strSql, param, value, System.Data.CommandType.StoredProcedure));
        //        retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(_strSql, param, value, CommandType.StoredProcedure));
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(ex.ToString());
        //    }
        //    return retFlag;
        //}
        //#endregion
    }
}
