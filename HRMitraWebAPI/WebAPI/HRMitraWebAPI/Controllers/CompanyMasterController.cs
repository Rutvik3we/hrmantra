using NDatabaseAccess;
using NDataMapper.Mapping;
using NDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Controllers;
using static NConstant.Constant;

namespace HRMitraWebAPI.Controllers
{
    public class CompanyMasterController : BaseController
    {
        private CompanyMaster _objCompanyMaster = null;
        private string _token = "";
        private long _userId = 0;
        private int _roleId = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyMasterController()
        {
            _objCompanyMaster = new CompanyMaster(ConnectionString);
        }

        private static List<CompanyMasterModel> GetListOfCompanyMaster(DataTable objDataTable)
        {
            try
            {
                DataNamesMapper<CompanyMasterModel> cMapper = new DataNamesMapper<CompanyMasterModel>();
                return cMapper.Map(objDataTable).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region "Get Method"
        [Route("api/CompanyMaster/GetCompanyMaster")]
        [HttpGet]
        public HttpResponseMessage GetCompanyMaster(long companyId, int Isactive)
        {
            try
            {
                APIResponseModel _objAPIResponse = new APIResponseModel();
                if (Request.Headers.Authorization.Parameter != null)
                {
                    _token = Request.Headers.Authorization.Parameter;
                    _userId = Convert.ToInt32(Request.Headers.GetValues("userId").FirstOrDefault());
                    _roleId = Convert.ToInt32(Request.Headers.GetValues("RoleId").FirstOrDefault());
                }
                else
                {
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Unauthorized);
                    _objAPIResponse.Message = "Invalid Token";
                    //return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
                }
                if (_verifyJWTToken(_token))
                {
                    DataTable objDataTable = _objCompanyMaster.GetCompanyMaster(companyId, Isactive);
                    List<CompanyMasterModel> cModel = GetListOfCompanyMaster(objDataTable);

                    if (cModel != null && cModel.Count > 0)
                    {
                        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Success);
                        _objAPIResponse.Message = "Success";
                        _objAPIResponse.Data = cModel;
                    }
                    else
                    {
                        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.NoDataFound);
                        _objAPIResponse.Message = "No Data Found";
                    }
                }
                else
                {
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Unauthorized);
                    _objAPIResponse.Message = "Invalid Token";
                }
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
            catch (Exception)
            {
                APIResponseModel _objAPIResponse = new APIResponseModel();
                _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
                _objAPIResponse.Message = "Some Error Occurs";
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
        }
        #endregion

        #region "Post Method"
        [Route("api/CompanyMaster/SaveCompanyMaster")]
        [HttpPost]
        public HttpResponseMessage SaveCompanyMaster(CompanyMasterModel companyMasterModel)
        {
            try
            {
                APIResponseModel _objAPIResponse = new APIResponseModel();
                if (Request.Headers.Authorization.Parameter != null)
                {
                    _token = Request.Headers.Authorization.Parameter;
                    _userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                    _roleId = Convert.ToInt32(Request.Headers.GetValues("RoleId").FirstOrDefault());

                    if (_verifyJWTToken(_token))
                    {
                        DataTable objDataTable = _objCompanyMaster.GetCompanyMaster(0, -1);
                        List<CompanyMasterModel> cModel = GetListOfCompanyMaster(objDataTable);
                        bool recordExist = cModel.Where(x => x.Id != companyMasterModel.Id).Any(x => Equals(x.CompanyCode, companyMasterModel.CompanyCode));

                        if (recordExist)
                        {
                            _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Conflict);
                            _objAPIResponse.Message = "Company Code Already exists!";
                        }
                        else
                        {
                            bool recordInserted = _objCompanyMaster.SaveCompanyMaster(companyMasterModel, _userId);
                            if (recordInserted == true)
                            {
                                _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Success);
                                if (companyMasterModel.Id == 0)
                                {
                                    _objAPIResponse.Message = "Company Inserted Successfully!";
                                }
                                else
                                {
                                    _objAPIResponse.Message = "Company Updated Successfully!";
                                }
                            }
                            else
                            {
                                _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
                                _objAPIResponse.Message = "Failed to Insert Data!";
                            }
                        }
                    }
                    else
                    {
                        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Unauthorized);
                        _objAPIResponse.Message = "Invalid Token";
                    }
                }
                else
                {
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Unauthorized);
                    _objAPIResponse.Message = "Invalid Token";
                }
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
            catch (Exception)
            {
                APIResponseModel _objAPIResponse = new APIResponseModel();
                _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
                _objAPIResponse.Message = "Some Error Occurs";
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
        }
        #endregion


        /// <summary>
        /// Destructor
        /// </summary>
        ~CompanyMasterController()
        {
            if (_objCompanyMaster != null)
            {
                _objCompanyMaster = null;
            }
        }
    }
}
