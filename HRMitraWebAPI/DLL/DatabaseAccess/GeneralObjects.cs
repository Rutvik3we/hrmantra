using System;
using NDatabaseHandler;
using NErrorHandler;
using NDataModel;
using System.Data;


namespace NDatabaseAccess
{
    public static class GeneralObjects
    {
        private static DataAccess _objDataAccess;
        private static ErrorHandler _objErrorLogger;

        public static string DateAsPerSQL(string date)
        {
            string modifiedDate;
            if (date == null)
            {
                modifiedDate = "";
            }
            else
            {
                modifiedDate = DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm");
                //modifiedDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            }
            return modifiedDate;
        }

        public static object DateTimeNullCheck(DateTime? date)
        {
            return !string.IsNullOrEmpty(date.ToString())
            ? string.Format("'{0}'", Convert.ToDateTime(date).ToString("yyyy-MM-dd"))
            : (object)System.Data.SqlTypes.SqlDateTime.Null;
        }

        public static void SetAudit(string msgCode, string addedby, string MachineName, string[] param)
        {
            try
            {
                string strSql = "SELECT MessageText FROM Messages WHERE MessageCode = '" + msgCode + "'";
                string msgText = Convert.ToString(_objDataAccess.ExecuteScalar(strSql));

                strSql = "SELECT UserName FROM Users WHERE ID = '" + addedby + "'";
                string userName = Convert.ToString(_objDataAccess.ExecuteScalar(strSql));

                bool retVal = false;
                msgText = string.Format(msgText, param);
                strSql = "INSERT INTO AuditRecords (AuditText,ComputerName,MessageCode,UserName,TimeStamp) " +
                         "VALUES ('" + msgText + "', '" + MachineName + "', '" + msgCode + "', '" + userName + "', DATEADD(minute, 330, GETUTCDATE()))";
                retVal = Convert.ToBoolean(_objDataAccess.ExecuteNonQuery(strSql));
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(ex.ToString());
            }
        }

       
    }
}
