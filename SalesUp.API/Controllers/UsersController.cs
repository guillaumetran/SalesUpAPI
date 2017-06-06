using Newtonsoft.Json;
using SalesUp.API.Models;
using SalesUp.BLL;
using SalesUp.DAL.Entity;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SalesUp.API.Controllers
{
    /// <summary>
    /// This class is used in an api ressources
    /// </summary>
    public class UsersController : ApiController
    {
        private IUsersService usersService = new UsersService();
        private ICompanyService companyService = new CompanyService();
        private Utils utils = new Utils();

        // GET api/values
        /// <summary>This method returns all the Users.</summary>
        /// <returns>List of all the Users in the database</returns>
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.Unauthorized)]
        public HttpResponseMessage Get(HttpRequestMessage req)
        {
            HttpResponseMessage result = null;
            string userId = null;

            #region InputCheck
            try
            {
                string token = req.Headers.Authorization.ToString();
                userId = utils.checkToken(token);
            }
            catch (Exception tEx)
            {
                result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                result.Content = new StringContent(JsonConvert.SerializeObject("Unauthorized access"), Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                var user = usersService.GetAll();

                if (user != null)
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.NotFound);
                }

                result.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException.ToString()), Encoding.UTF8, "application/json");
            }
            return result;
        }

        // GET api/values/5
        /// <summary>This method returns an Users by his Id.</summary>
        /// <param name="id">The identity of the user.</param>
        /// <returns>an user from database</returns>
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Get(HttpRequestMessage req, string id)
        {
            HttpResponseMessage result = null;
            string userId = null;

            #region InputCheck
            try
            {
                string token = req.Headers.Authorization.ToString();
                userId = utils.checkToken(token);
            }
            catch (Exception tEx)
            {
                result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                result.Content = new StringContent(JsonConvert.SerializeObject("Unauthorized access"), Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                var user = usersService.GetById(id);
                if (user != null)
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.NotFound);
                    result.Content = new StringContent(JsonConvert.SerializeObject("The user doesn't exist"), Encoding.UTF8, "application/json");
                }
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException.ToString()), Encoding.UTF8, "application/json");
            }
            return result;
        }

        // POST api/values
        /// <summary>
        /// This method add a new user to database.
        /// </summary>
        /// <param name="value">An user object</param>
        /// <remarks>
        /// - User first name is a string of 30 characters max
        /// - User last name is a string of 30 characters max
        /// - User fonction is a string of 50 characters max
        /// - User service is a string of 50 characters max
        /// - User mail is a string of 50 characters max
        /// </remarks>
        [SwaggerOperation("Post")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.Found)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Post(HttpRequestMessage req, [FromBody] Users value)
        {
            HttpResponseMessage result = null;
            string userId = null;

            #region InputCheck
            try
            {
                if (value == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                    result.Content = new StringContent(JsonConvert.SerializeObject("Invalid Parameters"), Encoding.UTF8, "application/json");
                    return result;
                }
                if (usersService.GetById(value.Id) != null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Found);
                    result.Content = new StringContent(JsonConvert.SerializeObject("User already exist"), Encoding.UTF8, "application/json");
                    return result;
                }
                if (companyService.GetById(value.CompanyId) == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.NotFound);
                    result.Content = new StringContent(JsonConvert.SerializeObject("The company doesn't exist"), Encoding.UTF8, "application/json");
                    return result;
                }
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException), Encoding.UTF8, "application/json");
            }
            try
            {
                string token = req.Headers.Authorization.ToString();
                userId = utils.checkToken(token);
            }
            catch (Exception tEx)
            {
                result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                result.Content = new StringContent(JsonConvert.SerializeObject("Unauthorized access"), Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                usersService.Add(value);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject("Insert operation is a success"),
                    Encoding.UTF8, "application/json");
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject("Insert operation failed"), Encoding.UTF8, "application/json");
            }
            return result;
        }

        // PUT api/values/5
        /// <summary>
        /// This method update the user object.
        /// </summary>
        /// <param name="value">An user object</param>
        /// <remarks>
        /// - User first name is a string of 30 characters max
        /// - User last name is a string of 30 characters max
        /// - User fonction is a string of 50 characters max
        /// - User service is a string of 50 characters max
        /// - User mail is a string of 50 characters max
        /// </remarks>
        [SwaggerOperation("Put")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Put(HttpRequestMessage req, [FromBody] Users value)
        {
            HttpResponseMessage result = null;
            string userId = null;

            #region InputCheck
            try
            {
                string token = req.Headers.Authorization.ToString();
                userId = utils.checkToken(token);
            }
            catch (Exception tEx)
            {
                result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                result.Content = new StringContent(JsonConvert.SerializeObject("Unauthorized access"), Encoding.UTF8, "application/json");
                return result;
            }
            if (companyService.GetById(value.CompanyId) == null)
            {
                result = Request.CreateResponse(HttpStatusCode.Found);
                result.Content = new StringContent(JsonConvert.SerializeObject("The company doesn't exist"), Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                usersService.Update(value);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject("Update operation is a success"), Encoding.UTF8, "application/json");
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException.ToString()), Encoding.UTF8, "application/json");
            }
            return result;
        }

        // DELETE api/values/5
        /// <summary>
        /// This method delete the user object.
        /// </summary>
        /// <param name="id">The identity of the user.</param>
        /// <returns></returns>
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Delete(HttpRequestMessage req, string id)
        {
            HttpResponseMessage result = null;
            string userId = null;

            #region InputCheck
            try
            {
                string token = req.Headers.Authorization.ToString();
                userId = utils.checkToken(token);
            }
            catch (Exception tEx)
            {
                result = Request.CreateResponse(HttpStatusCode.Unauthorized);
                result.Content = new StringContent(JsonConvert.SerializeObject("Unauthorized access"), Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                Users userToDelete = usersService.GetById(id);
                if (userToDelete == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.NotFound);
                    result.Content = new StringContent(JsonConvert.SerializeObject("The user doesn't exist"), Encoding.UTF8, "application/json");
                }
                else
                {
                    usersService.Delete(userToDelete);
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StringContent(JsonConvert.SerializeObject("Delete operation is a success"), Encoding.UTF8, "application/json");
                }
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException.ToString()), Encoding.UTF8, "application/json");
            }
            return result;
        }
    }
}