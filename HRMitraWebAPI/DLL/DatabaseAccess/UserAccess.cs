using System;
using System.Data;
using NDatabaseHandler;
using NErrorHandler;

namespace NDatabaseAccess
{
    public class UserAccess
    {
        #region "Private Variable"
        private string strSQL;
        private DataAccess objDataAccess;
        private ErrorHandler objErrorLogger;
        #endregion

        #region "Constructor/Destructor"
        public UserAccess(ErrorHandler errorLogger, DataAccess dataAccess)
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

        ~UserAccess()
        {
            if (objDataAccess != null)
                objDataAccess = null;

            if (objErrorLogger != null)
                objErrorLogger = null;

            if (strSQL != null)
                strSQL = null;
        }
        #endregion

        #region "Public Methods"
        public DataTable GetRestictedMainMenu(string userName)
        {
            DataTable objDataTable = null;
            try
            {
                //strSQL = "SELECT MainMenu FROM MainMenu WHERE Id NOT IN (SELECT MR.MainMenuId FROM  MainMenuRoleMapper MR LEFT JOIN UserRolesMapping U ON U.RoleID = MR.RoleId WHERE MR.MainMenuRoleMapper = 1 AND U.ID = " + userId + ")";
                strSQL = "SELECT MainMenu FROM MainMenu WHERE IsActive = 0 OR Id NOT IN (SELECT MR.MainMenuId FROM  MainMenuRoleMapper MR LEFT JOIN UserRolesMapping UR ON UR.RoleID = MR.RoleId LEFT JOIN Users U ON U.ID = UR.UserID WHERE MR.IsActive = 1 AND U.UserName = '" + userName + "')";
                objDataTable = objDataAccess.GetDataTable(strSQL);
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile("Branch : GetBranchDetailsForList");
                objErrorLogger.WritetoLogFile(ex);
            }
            return objDataTable;
        }        

        public DataTable GetRestictedSubMainMenu(string userName)
        {
            DataTable objDataTable = null;
            try
            {
                //strSQL = "SELECT SubMenu FROM SubMenu WHERE Id NOT IN (SELECT MR.SubMenuId FROM SubMenuRoleMapper MR LEFT JOIN UserRolesMapping U ON U.RoleID = MR.RoleId WHERE MR.SubMenuRoleMapper = 1 AND U.ID = " + userId + ")";                
                strSQL = "SELECT MainMenuId, SubMenu FROM SubMenu WHERE IsActive = 0 OR Id NOT IN (SELECT MR.SubMenuId FROM SubMenuRoleMapper MR LEFT JOIN UserRolesMapping UR ON UR.RoleID = MR.RoleId LEFT JOIN Users U ON U.ID = UR.UserID WHERE MR.IsActive = 1 AND U.UserName = '" + userName + "')";
                objDataTable = objDataAccess.GetDataTable(strSQL);
            }
            catch (Exception ex)
            {
                objErrorLogger.WritetoLogFile("Branch : GetBranchDetailsForList");
                objErrorLogger.WritetoLogFile(ex);
            }
            return objDataTable;
        }

      
        #endregion
    }
}
