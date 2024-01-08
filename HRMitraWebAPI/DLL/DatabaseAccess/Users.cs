using System;
using System.Data;
using NDatabaseHandler;
using NErrorHandler;
using NDataModel;
using System.Globalization;

namespace NDatabaseAccess
{
    public class Users
    {
        #region "Private Variable"
        private string _strSQL;
        private DataAccess _objDataAccess;
        private ErrorHandler _objErrorLogger;
        #endregion

        #region "Constructor/Destructor"
        public Users(string connString)
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

        ~Users()
        {
            if (_objDataAccess != null)
                _objDataAccess = null;

            if (_objErrorLogger != null)
                _objErrorLogger = null;

            if (_strSQL != null)
                _strSQL = null;
        }
        #endregion

        #region "Public Methods"

        public DataTable ValidateUser(string loginName)
        {
            DataTable objDataTable = null;
            try
            {
                _strSQL = "ValidateUser";
                string[] param = new string[1] { "@LoginName" };
                object[] values = new object[1] { loginName };

                objDataTable = _objDataAccess.GetDataTable(_strSQL, param, values, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
                return objDataTable;
            }
            return objDataTable;
        }
        #endregion
    }
}
