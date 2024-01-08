using System;
using System.Data;
using NErrorHandler;
using NDatabaseHandler;
using NDataModel;
using NConverters;

namespace NDatabaseAccess
{
    public class APIMaster
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
        public APIMaster(string connString)
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
        ~APIMaster()
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

        #region "APIMaster"
        /// <summary>
        /// Get Record/Records
        /// </summary>
        /// <returns></returns>
        public DataTable GetAPIDetails(int id, int userId)
        {
            DataTable objDataTable = null;
            try
            {
                string[] param = new string[2] { "@Id", "@UserId" };
                object[] values = new object[2] { id, userId };

                _strSQL = "GetAPIMaster";
                objDataTable = _objDataAccess.GetDataTable(_strSQL, param, values, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
                return objDataTable;
            }
            return objDataTable;
        }

        /// <summary>
        /// Insert Record
        /// </summary>
        /// <param name="apiMaster"></param>
        /// <returns></returns>
        public bool SaveAPIMaster(APIMasterModel apiModel)
        {
            bool retFlag = false;
            try
            {
                string[] param = new string[5] { "@Id", "@UserId", "@APIKey", "@CreatedDate", "@ExpiryDate" };
                object[] values = new object[5] { apiModel.Id, apiModel.UserId, apiModel.ApiKey, apiModel.CreatedDate, apiModel.ExpiryDate };

                string _strSQL = "SaveAPIMaster";
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(_strSQL, param, values, CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
            }
            return retFlag;
        }

        #region "Old Commented Code"
        /// <summary>
        /// Get Record/Records By UserId
        /// </summary>
        /// <returns></returns>
        //public DataTable GetAPIDetailsByUserId(int userId)
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = string.Format("SELECT * FROM APIMaster WHERE UserId={0} ORDER BY Id DESC", userId);
        //        objDataTable = _objDataAccess.GetDataTable(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return objDataTable;
        //    }
        //    return objDataTable;
        //}

        //public bool InsertAPIDetails(APIMasterModel apiMaster)
        //{
        //    bool retFlag = false;
        //    try
        //    {
        //        object createdDate = TypeConverter.DateTimeNullCheck(apiMaster.CreatedDate);
        //        object expiryDate = TypeConverter.DateTimeNullCheck(apiMaster.ExpiryDate);
        //        string strSql = string.Format("INSERT INTO APIMaster(UserId, APIKey, CreatedDate, ExpiryDate) " +
        //                        "VALUES({0}, '{1}', {2}, {3})", apiMaster.UserId, apiMaster.ApiKey, createdDate, expiryDate);
        //        retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return retFlag;
        //    }
        //    return retFlag;
        //}

        /// <summary>
        /// Update Record
        /// </summary>
        /// <param name="apiMaster"></param>
        /// <returns></returns>
        //public bool UpdateAPIDetails(APIMasterModel apiMaster)
        //{
        //    bool retFlag = false;
        //    try
        //    {
        //        object createdDate = TypeConverter.DateTimeNullCheck(apiMaster.CreatedDate);
        //        object expiryDate = TypeConverter.DateTimeNullCheck(apiMaster.ExpiryDate);
        //        string strSql = string.Format("UPDATE APIMaster SET UserId={0}, APIKey='{1}', CreatedDate={2}, ExpiryDate={3} Where Id={4}",
        //                        apiMaster.UserId, apiMaster.ApiKey, createdDate, expiryDate, apiMaster.Id);
        //        retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return retFlag;
        //    }
        //    return retFlag;
        //}

        /// <summary>
        /// Get Record/Records
        /// </summary>
        /// <returns></returns>
        //public DataTable GetRPIAPIDetails(int id)
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = string.Format("SELECT * FROM APIMaster WHERE ({0}=0 OR (Id={0})) ORDER BY Id DESC", id);
        //        objDataTable = _objDataAccess.GetDataTable(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return objDataTable;
        //    }
        //    return objDataTable;
        //}
        #endregion

        #endregion
    }
}
