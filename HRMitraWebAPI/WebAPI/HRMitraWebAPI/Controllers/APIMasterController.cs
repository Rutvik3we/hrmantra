using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NDataModel;
using NDatabaseAccess;
using System.Data;
using NDataMapper.Mapping;
using System.Reflection;
using static NConstant.Constant;

namespace HRMitraWebAPI.Controllers
{
    public class APIMasterController : BaseController
    {
        private APIMaster _objAPIMaster = null;

        public APIMasterController()
        {
            _objAPIMaster = new APIMaster(ConnectionString);
        }

        private static List<APIMasterModel> GetListOfAPIs(DataTable dtRecords)
        {
            try
            {
                DataNamesMapper<APIMasterModel> mapper = new DataNamesMapper<APIMasterModel>();
                return mapper.Map(dtRecords).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region GetMethods
        [Route("api/APIMaster/GetAPIDetails")]
        [HttpGet]
        public HttpResponseMessage GetAPIDetails(int Id)
        {
            try
            {
                int userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
                DataTable dtRecords = _objAPIMaster.GetAPIDetails(Id, userId);
                List<APIMasterModel> modelColln = GetListOfAPIs(dtRecords);
                return modelColln != null && modelColln.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, modelColln)
                    : Request.CreateResponse(HttpStatusCode.NoContent, "No Data Found!");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                              string.Format("{0} : Failed to Fetch Data!", MethodBase.GetCurrentMethod().Name));
            }
        }
        #endregion

        #region PostMethods
        [Route("api/APIMaster/SaveAPIDetails")]
        [HttpPost]
        public HttpResponseMessage SaveAPIDetails(APIMasterModel apiMaster)
        {
            try
            {
                APIResponseModel _objAPIResponse = new APIResponseModel();

                bool recordInserted = _objAPIMaster.SaveAPIMaster(apiMaster);
                if (recordInserted == true)
                {
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Success);
                    if (apiMaster.Id == 0)
                    {
                        _objAPIResponse.Message = "API Master Data Inserted Successfully!";
                    }
                    else
                    {
                        _objAPIResponse.Message = "API Master Data Updated Successfully!";
                    }
                }
                else
                {
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
                    _objAPIResponse.Message = "Failed to Insert Data!";
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

    }
}
