using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using System.Collections.Generic;
using NDataModel;
using NDatabaseAccess;
using NDataMapper.Mapping;

namespace HRMitraWebAPI.Controllers
{
    public class UserController : BaseController
    {
        private Users _objUsers = null;
        //private General _objGeneral = null;
        private string _token = "";
        private long userId = 0;
        private int _roleId = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserController()
        {
            _objUsers = new Users(ConnectionString);
            //_objGeneral = new General(ConnectionString);
        }

        private static List<UserModel> GetListOfUsers(DataTable dtRecords)
        {
            try
            {
                DataNamesMapper<UserModel> usermapper = new DataNamesMapper<UserModel>();
                return usermapper.Map(dtRecords).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Destructor
        /// </summary>
        ~UserController()
        {
            if (_objUsers != null)
            {
                _objUsers = null;
            }
        }
    }
}