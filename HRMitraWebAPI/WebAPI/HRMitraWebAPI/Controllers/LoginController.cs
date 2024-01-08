using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using NDatabaseAccess;
using NDataMapper.Mapping;
using NDataModel;
using static NConstant.Constant;

namespace HRMitraWebAPI.Controllers
{
    public class LoginController : BaseController
    {
        private Users _objUsers = null;

        public LoginController()
        {
            _objUsers = new Users(ConnectionString);
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

        #region "Validate User"
        [Route("api/Login/ValidateUser")]
        [HttpPost]
        public HttpResponseMessage ValidateUser([FromBody] LoginModel loginModel)
        {
            try
            {
                DataTable dtRecords = _objUsers.ValidateUser(loginModel.LoginName);
                //UserModel userModelColln = GetListOfUsers(dtRecords).FirstOrDefault();
                List<UserModel> userModelColln = GetListOfUsers(dtRecords);
                APIResponseModel _objAPIResponse = new APIResponseModel();

                if (userModelColln != null && userModelColln.Count > 0)
                {
                    string pwd = NEncryptDecrypt.EncryptDecrypt.Encrypt(loginModel.Password);
                    bool validPassword = string.Compare(userModelColln.FirstOrDefault().Password, pwd) == 0;
                    if (validPassword)
                    {
                        string jwtToken = GenerateJWTToken();
                        Dictionary<string, object> header = new Dictionary<string, object>
                        {
                        { "accessToken", jwtToken },
                        { "user",  userModelColln},
                        { "tokenType", "bearer" }
                        };

                        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.Success);
                        _objAPIResponse.Message = "Success";
                        _objAPIResponse.Data = header;
                    }
                    else
                    {
                        _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.NoDataFound);
                        _objAPIResponse.Message = "Password invalid, please check your password!";
                    }
                }
                else
                {
                    //return Request.CreateResponse(HttpStatusCode.NotFound, "Wrong Email Address Or Password");
                    _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.NoDataFound);
                    _objAPIResponse.Message = "Invalid User!";
                }
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
            catch (Exception ex)
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError,
                //                              string.Format("{0} : Failed to Fetch Data!", MethodBase.GetCurrentMethod().Name));
                APIResponseModel _objAPIResponse = new APIResponseModel();
                _objAPIResponse.ResponseStatus = Convert.ToInt32(APIResponseStatus.ExceptionOrFailed);
                _objAPIResponse.Message = "Some Error Occurs";
                return Request.CreateResponse(HttpStatusCode.OK, _objAPIResponse);
            }
        }
        #endregion
    }
}
