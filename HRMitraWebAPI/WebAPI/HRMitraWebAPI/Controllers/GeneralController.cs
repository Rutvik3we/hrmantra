using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static NConstant.Constant;
using NDataModel;
using NDatabaseAccess;

namespace HRMitraWebAPI.Controllers
{
    public class GeneralController : BaseController
    {
        private GeneralDB _objGeneralDB = null;
        private string _token = "";
        private long _userId = 0;
        private int _roleId = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralController()
        {
            _objGeneralDB = new GeneralDB(ConnectionString);
        }

        //#region "Post Method"
        //[Route("api/General/SetActiveInactiveStatus")]
        //[HttpPost]
        //public HttpResponseMessage SetActiveInactiveStatus(SetActiveInactiveModel mdlSetActiveInactive)
        //{
        //    try
        //    {
        //        APIResponseModel _objAPIResponse = new APIResponseModel();
        //        if (Request.Headers.Authorization.Parameter != null)
        //        {
        //            _token = Request.Headers.Authorization.Parameter;
        //            _userId = Convert.ToInt32(Request.Headers.GetValues("userid").FirstOrDefault());
        //            _roleId = Convert.ToInt32(Request.Headers.GetValues("RoleId").FirstOrDefault());

        //            if (_verifyJWTToken(_token))
        //            {

        //                bool resultFlag = _objGeneralDB.SetActiveInActiveStatus(mdlSetActiveInactive);
        //                if (resultFlag == true)
        //                {
        //                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Success);
        //                    if (mdlSetActiveInactive.StatusFlag == 1)
        //                    {
        //                        _objAPIResponse.Message = "Active Successfully!";
        //                    }
        //                    else
        //                    {
        //                        _objAPIResponse.Message = "Inactive Successfully!";
        //                    }
        //                }
        //                else
        //                {
        //                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
        //                    _objAPIResponse.Message = "Failed to Change Status!";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Unauthorized);
        //            _objAPIResponse.Message = "Invalid Token";
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
        //    }
        //    catch (Exception)
        //    {
        //        APIResponseModel _objAPIResponse = new APIResponseModel();
        //        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
        //        _objAPIResponse.Message = "Some Error Occurs";
        //        return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
        //    }
        //}
        //#endregion
    }
}
