using System;
using System.Data;
using NDataModel;
using NErrorHandler;
using NDatabaseHandler;


namespace NDatabaseAccess
{
    public class SubMenuRoleMapper
    {
        #region "Private Variable"
        private string _strSQL;
        private DataAccess _objDataAccess;
        private ErrorHandler _objErrorLogger;
        //private DataAccess1 _objDataAccess;
        #endregion

        #region "Constructor/Destructor"
        public SubMenuRoleMapper()
        {
            try
            {
                string connString = NEncryptDecrypt.EncryptDecrypt.GetConnString();
                _objErrorLogger = new ErrorHandler("Department");
                _objDataAccess = new DataAccess(connString, _objErrorLogger);
                //_objDataAccess = new DataAccess1();
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(ex);
            }
        }

        public SubMenuRoleMapper(ErrorHandler errorLogger)
        {
            try
            {
                string connString = NEncryptDecrypt.EncryptDecrypt.GetConnString();
                _objErrorLogger = errorLogger;
                _objDataAccess = new DataAccess(connString, _objErrorLogger);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(ex);
            }
        }

        public SubMenuRoleMapper(ErrorHandler errorLogger, DataAccess dataAccess)
        {
            try
            {
                _objDataAccess = dataAccess;
                _objErrorLogger = errorLogger;
            }
            catch (Exception ex)
            {
                errorLogger.WritetoLogFile(ex);
            }
        }

        ~SubMenuRoleMapper()
        {
            if (_objDataAccess != null)
                _objDataAccess = null;

            if (_objErrorLogger != null)
                _objErrorLogger = null;

            if (_strSQL != null)
                _strSQL = null;
        }
        #endregion

       

        #region "SubMenuRoleMapper"
        /// <summary>
        /// Get Record/Records
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubMenuRoleMapperDetails(int id, int roleId)
        {
            DataTable objDataTable = null;
            try
            {
                string[] paramArray = new string[2];
                paramArray[0] = "@Id";
                paramArray[1] = "@RoleId";

                object[] values = new object[2];
                values[0] = id;
                values[1] = roleId;

                string strSQL = "GetSubMenuRoleMapperDetails";
                objDataTable = _objDataAccess.GetDataTable(strSQL, paramArray, values, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                
                return objDataTable;
            }
            return objDataTable;
        }


        /// <summary>
        /// Update Active Status By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateActiveStatus(int id)
        {
            bool retFlag = false;
            try
            {
                string strSql = "UPDATE SubMenuRoleMapper SET IsActive = Case When IsActive = 0 then 1 else 0 end Where Id = " + id;
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception)
            {
              
                return retFlag;
            }
            return retFlag;
        }

        /// <summary>
        /// Can Add By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCanAddById(int id)
        {
            bool retFlag = false;
            try
            {
                string strSql = "UPDATE SubMenuRoleMapper SET CanAdd = Case When CanAdd = 0 then 1 else 0 end Where Id = " + id;
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception)
            {
              
                return retFlag;
            }
            return retFlag;
        }

        /// <summary>
        /// Can Edit By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCanEditById(int id)
        {
            bool retFlag = false;
            try
            {
                string strSql = "UPDATE SubMenuRoleMapper SET CanEdit = Case When CanEdit = 0 then 1 else 0 end Where Id = " + id;
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception)
            {
               
                return retFlag;
            }
            return retFlag;
        }

        /// <summary>
        /// Can Add By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCanDeleteById(int id)
        {
            bool retFlag = false;
            try
            {
                string strSql = "UPDATE SubMenuRoleMapper SET CanDelete = Case When CanDelete = 0 then 1 else 0 end Where Id = " + id;
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception)
            {
               
                return retFlag;
            }
            return retFlag;
        }

        /// <summary>
        /// Can Add By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCanViewById(int id)
        {
            bool retFlag = false;
            try
            {
                string strSql = "UPDATE SubMenuRoleMapper SET CanView = Case When CanView = 0 then 1 else 0 end Where Id = " + id;
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception)
            {
                return retFlag;
            }
            return retFlag;
        }

        ///// <summary>
        ///// Get Record/Records
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetSubMenuRoleMappingDetails(int id)
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = string.Format("SELECT * FROM SubMenuRoleMapper WHERE ({0}=0 OR (Id={0})) ORDER BY Id DESC", id);
        //        objDataTable = _objDataAccess.GetDataTable(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return objDataTable;
        //    }
        //    return objDataTable;
        //}

        ///// <summary>
        ///// Get Record/Records
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetSubMenuRoleMappingDetailsBySubMenuId(int subMenuId)
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = string.Format("SELECT * FROM SubMenuRoleMapper WHERE SubMenuId={0} ORDER BY Id DESC", subMenuId);
        //        objDataTable = _objDataAccess.GetDataTable(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return objDataTable;
        //    }
        //    return objDataTable;
        //}

        ///// <summary>
        ///// Get Record/Records
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetSubMenuRoleMappingDetailsByRoleId(int roleId)
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = string.Format("SELECT * FROM SubMenuRoleMapper WHERE RoleId={0} ORDER BY Id DESC", roleId);
        //        objDataTable = _objDataAccess.GetDataTable(strSql);
        //    }
        //    catch (Exception ex)
        //    {
        //        _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
        //        return objDataTable;
        //    }
        //    return objDataTable;
        //}

        ///// <summary>
        ///// Get Active Records
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetActiveSubMenuRoleMappingDetails()
        //{
        //    DataTable objDataTable = null;
        //    try
        //    {
        //        string strSql = "SELECT * FROM SubMenuRoleMapper Where IsActive = 1 ORDER BY Id DESC";
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
    }
}
