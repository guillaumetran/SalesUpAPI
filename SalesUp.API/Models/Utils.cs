using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace SalesUp.API.Models
{
    public class Utils
    {
        public string checkToken(string token)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://salesupauthservice.scalingo.io/api/checkToken");
                request.Method = "GET";
                request.Headers.Add("Authorization", token);
                StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
                var responseData = responseReader.ReadToEnd();
                JToken jtoken = JObject.Parse(responseData);
                var userId = (string)jtoken.SelectToken("_id");
                return userId;
            }
            catch (Exception tEx)
            {
                throw tEx;
            }
        }

        public bool IsGuid(string id)
        {
            try
            {
                Guid testGuid = new Guid(id);

                if (id == testGuid.ToString())
                    return true;
            }
            catch
            {
            }
            return false;
        }
    }
}