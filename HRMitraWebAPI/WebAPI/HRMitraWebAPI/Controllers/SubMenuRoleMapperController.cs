using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using System.Collections.Generic;
using NDataModel;
using NDatabaseAccess;
using NDataMapper.Mapping;


namespace WebApi.Controllers
{
    public class SubMenuRoleMapperController : ApiController
    {
        private static List<SubMenuRoleMapperModel> GetListOfSubMenuRoleMapping(DataTable dtRecords)
        {
            try
            {
                DataNamesMapper<SubMenuRoleMapperModel> mapper = new DataNamesMapper<SubMenuRoleMapperModel>();
                return mapper.Map(dtRecords).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: EmployeeDetails
        [Route("api/GetSubMenuRoleMapperDetails")]
        [HttpGet]
        public HttpResponseMessage GetSubMenuRoleMapperDetails(int Id, int roleid)
        {

            DataTable table = new DataTable();
            SubMenuRoleMapper _objSubMenuRoleMapper = null;

            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                table = _objSubMenuRoleMapper.GetSubMenuRoleMapperDetails(Id, roleid);
                List<SubMenuRoleMapperModel> modelColln = GetListOfSubMenuRoleMapping(table);
                //DecryptPassword(modelColln);
                return modelColln != null && modelColln.Count > 0
                      ? Request.CreateResponse(HttpStatusCode.OK, modelColln)
                      : Request.CreateResponse(HttpStatusCode.NoContent, "No Data Found!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some Error is occur");
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }
        [Route("api/UpdateActiveStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateActiveStatus(SubMenuRoleMapperModel submenuroleModel)
        {
            SubMenuRoleMapper _objSubMenuRoleMapper = null;
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                bool recordUpdated = _objSubMenuRoleMapper.UpdateActiveStatus(submenuroleModel.Id);
                if (recordUpdated == true)
                {
                    //string auditCode = submenuroleModel.CanAdd ? "AddPermissionActivated" : "AddPermissionDeactivated";
                    //string strText = string.Format("Screen: {0}, Role:{1} ", submenuroleModel.TitleName, submenuroleModel.RoleName);
                    //SetAudit(auditCode, userId, strText);
                    return Request.CreateResponse(HttpStatusCode.OK, "Record Updated Successfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                                  string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }



        [Route("api/UpdateCanAdd")]
        [HttpPut]
        public HttpResponseMessage UpdateCanAddById(SubMenuRoleMapperModel submenuroleModel)
        {
            SubMenuRoleMapper _objSubMenuRoleMapper = null;
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                bool recordUpdated = _objSubMenuRoleMapper.UpdateCanAddById(submenuroleModel.Id);
                if (recordUpdated == true)
                {
                    //string auditCode = submenuroleModel.CanAdd ? "AddPermissionActivated" : "AddPermissionDeactivated";
                    //string strText = string.Format("Screen: {0}, Role:{1} ", submenuroleModel.TitleName, submenuroleModel.RoleName);
                    //SetAudit(auditCode, userId, strText);
                    return Request.CreateResponse(HttpStatusCode.OK, "Record Updated Successfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                                  string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }

        [Route("api/UpdateCanEdit")]
        [HttpPut]
        public HttpResponseMessage UpdateCanEdit(SubMenuRoleMapperModel submenuroleModel)
        {
            SubMenuRoleMapper _objSubMenuRoleMapper = null;
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                bool recordUpdated = _objSubMenuRoleMapper.UpdateCanEditById(submenuroleModel.Id);
                if (recordUpdated == true)
                {
                    //string auditCode = submenuroleModel.CanAdd ? "AddPermissionActivated" : "AddPermissionDeactivated";
                    //string strText = string.Format("Screen: {0}, Role:{1} ", submenuroleModel.TitleName, submenuroleModel.RoleName);
                    //SetAudit(auditCode, userId, strText);
                    return Request.CreateResponse(HttpStatusCode.OK, "Record Updated Successfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                                  string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }


        [Route("api/UpdateCanViewById")]
        [HttpPut]
        public HttpResponseMessage UpdateCanViewById(SubMenuRoleMapperModel submenuroleModel)
        {
            SubMenuRoleMapper _objSubMenuRoleMapper = null;
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                bool recordUpdated = _objSubMenuRoleMapper.UpdateCanViewById(submenuroleModel.Id);
                if (recordUpdated == true)
                {
                    //string auditCode = submenuroleModel.CanAdd ? "AddPermissionActivated" : "AddPermissionDeactivated";
                    //string strText = string.Format("Screen: {0}, Role:{1} ", submenuroleModel.TitleName, submenuroleModel.RoleName);
                    //SetAudit(auditCode, userId, strText);
                    return Request.CreateResponse(HttpStatusCode.OK, "Record Updated Successfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                                  string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }

        [Route("api/UpdateCanDeleteById")]
        [HttpPut]
        public HttpResponseMessage UpdateCanDeleteById(SubMenuRoleMapperModel submenuroleModel)
        {
            SubMenuRoleMapper _objSubMenuRoleMapper = null;
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                _objSubMenuRoleMapper = new SubMenuRoleMapper();
                bool recordUpdated = _objSubMenuRoleMapper.UpdateCanDeleteById(submenuroleModel.Id);
                if (recordUpdated == true)
                {
                    //string auditCode = submenuroleModel.CanAdd ? "AddPermissionActivated" : "AddPermissionDeactivated";
                    //string strText = string.Format("Screen: {0}, Role:{1} ", submenuroleModel.TitleName, submenuroleModel.RoleName);
                    //SetAudit(auditCode, userId, strText);
                    return Request.CreateResponse(HttpStatusCode.OK, "Record Updated Successfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                                  string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Update Data!", MethodBase.GetCurrentMethod().Name));
            }
            finally
            {
                if (_objSubMenuRoleMapper != null)
                    _objSubMenuRoleMapper = null;
            }
        }
    }
}