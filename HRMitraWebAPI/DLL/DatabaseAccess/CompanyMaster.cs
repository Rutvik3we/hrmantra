using System;
using System.Data;
using NErrorHandler;
using NDatabaseHandler;
using NDataModel;

namespace NDatabaseAccess
{
    public class CompanyMaster
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
        public CompanyMaster(string connString)
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
        ~CompanyMaster()
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

        #region "Public Method"
        public DataTable GetCompanyMaster(long Companyid, int Isactive)
        {
            DataTable objDataTable = null;
            try
            {
                string[] param = new string[2] { "@CompanyId", "@IsActive" };
                object[] values = new object[2] { Companyid, Isactive };

                _strSQL = "GetCompanyMaster";
                objDataTable = _objDataAccess.GetDataTable(_strSQL, param, values, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
                return objDataTable;
            }
            return objDataTable;
        }

        public bool SaveCompanyMaster(CompanyMasterModel Companymastermodel, long userId)
        {
            bool retFlag = false;
            try
            {
                string[] param = new string[21] { "@CompanyId", "@CompanyCode", "@CompanyName", "@Address1", "@Address2", "@Country", "@State", "@City", "@PinCode", "@CompanyEmail",
                    "@ContactNumber", "@WhatsAppNumber", "@IsGSTRegister", "@GSTNumber", "@CINNumber", "@WebSite", "@LinkedInPage", "@InstagramPage", "@FaceBookPage", "@Logo", "@UpdatedBy" };
                object[] values = new object[21] { Companymastermodel.Id, Companymastermodel.CompanyCode, Companymastermodel.CompanyName, Companymastermodel.Address1, Companymastermodel.Address2, Companymastermodel.Country,
                Companymastermodel.State, Companymastermodel.City, Companymastermodel.PinCode, Companymastermodel.CompanyEmail, Companymastermodel.ContactNumber, Companymastermodel.WhatsAppNumber, Companymastermodel.IsGSTRegister,
                Companymastermodel.GSTNumber, Companymastermodel.CINNumber, Companymastermodel.WebSite, Companymastermodel.LinkedInPage, Companymastermodel.InstagramPage, Companymastermodel.FaceBookPage, Companymastermodel.Logo, userId};

                string _strSQL = "SaveCompanyMaster";
                retFlag = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(_strSQL, param, values, CommandType.StoredProcedure));
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
            }
            return retFlag;
        }
        #endregion
    }
}
