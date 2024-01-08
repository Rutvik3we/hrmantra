using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace HRMitraWebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public string ConnectionString;
        private readonly string _secret;
        public BaseController()
        {
            string constr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            int index1 = constr.IndexOf("Password=") + 9;
            int index2 = constr.IndexOf(";", index1);
            string pwd = constr.Substring(index1, (index2 - index1));

            DecryptHelper.Decrypt objDecrypt = new DecryptHelper.Decrypt();
            string dencryptPwd = objDecrypt.DecryptPWD(pwd);
            ConnectionString = constr.Replace(pwd, dencryptPwd);

            _secret = "YOUR_VERY_CONFIDENTIAL_SECRET_FOR_SIGNING_JWT_TOKENS!!!";
        }

        /**
         * Verify the given token
         *
         * @param token
         * @private
         */
        public Boolean _verifyJWTToken(string token)
        {
            // Split the token into parts
            string[] obj = token.Split('.');
            string header = obj[0];
            string payload = obj[1];
            string signature = obj[2];

            // Re-sign and encode the header and payload using the secret
            string str = Encoding.UTF8.GetString(HashHMACHex(header + "." + payload, this._secret));
            string signatureCheck = this._base64url(str);

            // Verify that the resulting signature is valid
            return (signature == signatureCheck);
        }

        private static byte[] HashHMACHex(string keyHex, string messageHex)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyHex);
            byte[] message = Encoding.UTF8.GetBytes(messageHex);
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        /**
         * Return base64 encoded version of the given string
         *
         * @param source
         * @private
         */
        private string _base64url(string source)
        {

            // Encode in classical base64
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(source);
            string encodedSource = Convert.ToBase64String(encodedBytes);

            // Remove padding equal characters
            //encodedSource = encodedSource.Replace("=", "").Replace("+", "").Replace("$", "");
            encodedSource = Regex.Replace(encodedSource, @"=+$", "");

            // Replace characters according to base64url specifications
            encodedSource = Regex.Replace(encodedSource, @"\+", "-");
            encodedSource = Regex.Replace(encodedSource, @"\/", "_");

            // Return the base64 encoded string
            return encodedSource;
        }

        //Generate JWTToken at the Time of Login
        public string GenerateJWTToken()
        {
            // Define token header
            Dictionary<string, string> header = new Dictionary<string, string>
            {
                { "alg", "HS256" },
                { "typ", "JWT" }
            };

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            DateTime now = DateTime.Now;

            TimeSpan ts = now.Subtract(epoch);
            double unixTimeMilliseconds = ts.TotalMilliseconds;

            //DateTime date = new DateTime();
            //now = DateTime.Now;
            double iTime = unixTimeMilliseconds / 1000;
            double iat = Math.Floor(iTime);

            TimeSpan ts1 = now.AddDays(1).Subtract(epoch);
            double unixTimeMilliseconds1 = ts1.TotalMilliseconds;

            double eTime = unixTimeMilliseconds1 / 1000;
            double exp = Math.Floor(eTime);

            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                { "iat", iat },
                { "iss", "Bspl" },
                { "exp", exp }
            };

            // Stringify and encode the header
            UTF8Encoding utf8 = new UTF8Encoding();

            string unicodeStringHeader = JsonConvert.SerializeObject(header);
            byte[] encodedBytesHeader = utf8.GetBytes(unicodeStringHeader);
            string stringifiedHeader = utf8.GetString(encodedBytesHeader);
            string encodedHeader = this._base64url(stringifiedHeader);

            // Stringify and encode the payload
            string unicodeStringPayload = JsonConvert.SerializeObject(payload);
            byte[] encodedBytesPayload = utf8.GetBytes(unicodeStringPayload);
            string stringifiedPayload = utf8.GetString(encodedBytesPayload);
            string encodedPayload = this._base64url(stringifiedPayload);

            // Sign the encoded header and mock-api
            string signature = encodedHeader + '.' + encodedPayload;

            signature = Encoding.UTF8.GetString(HashHMACHex(signature, this._secret));
            signature = this._base64url(signature);

            // Build and return the token
            return encodedHeader + '.' + encodedPayload + '.' + signature;
        }

        //private static byte[] HashHMACHex(string keyHex, string messageHex)
        //{
        //    byte[] key = Encoding.UTF8.GetBytes(keyHex);
        //    byte[] message = Encoding.UTF8.GetBytes(messageHex);
        //    var hash = new HMACSHA256(key);
        //    return hash.ComputeHash(message);
        //}
    }
}
