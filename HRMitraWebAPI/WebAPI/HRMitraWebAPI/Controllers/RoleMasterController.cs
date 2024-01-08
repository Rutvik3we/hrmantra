using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NDataModel;
using NDatabaseAccess;
using System;
using System.Linq;

namespace WebApi.Controllers
{
    public class RoleMasterController : ApiController
    {
        [Route("api/GetRoleData")]
        [HttpGet]
        public HttpResponseMessage GetRoleData()
        {

            DataTable table = new DataTable();
            RoleMaster objRoleMaster = null;
            try
            {
                objRoleMaster = new RoleMaster();
                table = objRoleMaster.GetRoleData();
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
                if (objRoleMaster != null)
                    objRoleMaster = null;
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/InsertRoleMaster")]
        [HttpPost]
        public HttpResponseMessage InsertRoleMaster(RoleMasterModel roleMaster)
        {
            RoleMaster objRoleMaster = null;
            try
            {
                objRoleMaster = new RoleMaster();
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                string MachineName = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                bool IsInserted = objRoleMaster.InsertRoleData(roleMaster, userId, MachineName);

                if (IsInserted == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Role Inserted Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Role Not Inserted");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Exception While Insert");
            }
            finally
            {
                if (objRoleMaster != null)
                    objRoleMaster = null;
            }
        }

        [Route("api/UpdateRoleMaster")]
        [HttpPut]
        public HttpResponseMessage UpdateRoleMaster(RoleMasterModel roleMaster)
        {
            RoleMaster objRoleMaster = null;
            try
            {
                objRoleMaster = new RoleMaster();
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                string MachineName = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                bool IsUpdated = objRoleMaster.UpdateRoleData(roleMaster, userId, MachineName);
                if (IsUpdated == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Role Updated Successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Role Not Updated");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Exception While Update");
            }
            finally
            {
                if (objRoleMaster != null)
                    objRoleMaster = null;
            }
        }
    }
}