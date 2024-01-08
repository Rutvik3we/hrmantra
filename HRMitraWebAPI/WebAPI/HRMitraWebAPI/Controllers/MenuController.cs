using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NDataModel;
using NDatabaseAccess;

namespace HRMitraWebAPI.Controllers
{
    public class MenuController : BaseController
    {
        private string _token = "";
        private long _userId = 0;
        private int _roleId = 0;


        private MainMenu _objMainMenu = null;

        public MenuController()
        {
            _objMainMenu = new MainMenu(ConnectionString);
        }

        // GET: Menu
        [Route("api/Menu/GetMenu")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<MainMenuMaster> menu = null;
            MainMenu objMenu = null;

            if (Request.Headers.Authorization.Parameter != null)
            {
                _token = Request.Headers.Authorization.Parameter;
                _userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _roleId = Convert.ToInt32(Request.Headers.GetValues("RoleId").FirstOrDefault());

                if (_verifyJWTToken(_token))
                {
                    DataTable table = new DataTable();
                    try
                    {
                        menu = _objMainMenu.GetMenulList(_roleId);
                        if (table == null)
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Data Not Found");
                        }
                    }
                    catch
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some Error is occur");
                    }
                    finally
                    {
                        if (objMenu != null)
                            objMenu = null;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, menu);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Token");
            }
            
        }
    }
}