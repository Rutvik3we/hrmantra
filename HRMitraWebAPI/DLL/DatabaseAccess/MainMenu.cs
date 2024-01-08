using NDataModel;
using NDatabaseHandler;
using NErrorHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NDatabaseAccess
{
    public class MainMenu
    {
        #region "Private Variable"
        private string _strSQL;
        private DataAccess _objDataAccess;
        private ErrorHandler _objErrorLogger;
        #endregion

        #region "Constructor/Destructor"
        public MainMenu(string connString)
        {
            try
            {
                //string connString = NEncryptDecrypt.EncryptDecrypt.GetConnString();
                _objErrorLogger = new ErrorHandler(this.GetType().Name);
                _objDataAccess = new DataAccess(connString, _objErrorLogger);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
            }
        }

        public MainMenu(ErrorHandler errorLogger, DataAccess dataAccess)
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

        ~MainMenu()
        {
            if (_objDataAccess != null)
                _objDataAccess = null;

            if (_objErrorLogger != null)
                _objErrorLogger = null;

            if (_strSQL != null)
                _strSQL = null;
        }

        #endregion

        #region "MainMenu"
        public List<MainMenuMaster> GetMenulList(int roleId)
        {
            List<MainMenuMaster> menu = new List<MainMenuMaster>();
            List<NDataModel.SubMenu> sMenu = new List<NDataModel.SubMenu>();

            MainMenuMaster objMainMenu = null;
            NDataModel.SubMenu objSubMenu = null;
            int menuId;
            string icon, submenuicon;
            DataTable objSubDataTable;

            _strSQL = "GetMainMenu";
            string[] param = new string[1] { "@RoleId" };
            object[] values = new object[1] { roleId };

            DataTable objMainDataTable = _objDataAccess.GetDataTable(_strSQL, param, values, CommandType.StoredProcedure);
            foreach (DataRow dr in objMainDataTable.Rows)
            {
                objMainMenu = new MainMenuMaster();
                menuId = Convert.ToInt32(dr["Id"]);
                icon = Convert.ToString(dr["MenuIcon"]);
                objMainMenu.text = Convert.ToString(dr["TitleName"]);
                objMainMenu.routerLink = Convert.ToString(dr["Controller"]);
                objMainMenu.icon = icon;

                sMenu = new List<NDataModel.SubMenu>();
                string strSql = "GetSubMenu";
                string[] paramSubmenu = new string[2] { "@RoleId", "@MainMenuId" };
                object[] valuesSubMenu = new object[2] { roleId, menuId };

                objSubDataTable = _objDataAccess.GetDataTable(strSql, paramSubmenu, valuesSubMenu, CommandType.StoredProcedure);
                foreach (DataRow drSub in objSubDataTable.Rows)
                {
                    objSubMenu = new NDataModel.SubMenu();

                    objSubMenu.text = Convert.ToString(drSub["TitleName"]);
                    objSubMenu.routerLink = Convert.ToString(drSub["Controller"]);
                    submenuicon = Convert.ToString(drSub["SubMenuIcon"]);
                    objSubMenu.icon = submenuicon;

                    sMenu.Add(objSubMenu);
                }
                if (sMenu.Count > 0)
                    objMainMenu.Child = sMenu;
                menu.Add(objMainMenu);
            }

            return menu;
        }

        public DataTable GetMainMenuDetails(long id)
        {
            DataTable objDataTable = null;
            try
            {
                _strSQL = string.Format("Select * From ViewMainMenu" +
                            " Where EmpId = " + id + " And IsActive = 1 Order By OrderNo ASC", id);
                objDataTable = _objDataAccess.GetDataTable(_strSQL);
            }
            catch (Exception ex)
            {
                _objErrorLogger.WritetoLogFile(string.Format("Message:{0}, Exception:{1}", ex.Message, ex));
                return objDataTable;
            }
            return objDataTable;
        }

        public DataTable GetActiveSubMenuDetails(long id)
        {
            DataTable objDataTable = null;
            try
            {
                _strSQL = string.Format("Select * From ViewSubMenu" +
                         " Where EmpId = " + id + " And IsActive = 1 Order By OrderNo ASC ", id);
                objDataTable = _objDataAccess.GetDataTable(_strSQL);
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
