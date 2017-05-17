using Newtonsoft.Json;
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

        // GET api/values
        /// <summary>This method returns all the Users.</summary>
        /// <returns>List of all the Users in the database</returns>
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage result = null;

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
        public HttpResponseMessage Get(string id)
        {
            HttpResponseMessage result = null;

            #region InputCheck
            if (!IsGuid(id))
            {
                var message = JsonConvert.SerializeObject("This ID is not a GUID");
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
                result.Content = new StringContent(message, Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                var user = usersService.GetById(id);
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
        public HttpResponseMessage Post([FromBody] Users value)
        {
            HttpResponseMessage result = null;

            #region InputCheck

            try
            {
                if (usersService.GetById(value.Id) != null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Found);
                    result.Content = new StringContent(JsonConvert.SerializeObject("User already exist"), Encoding.UTF8, "application/json");
                    return result;
                }
                if (companyService.GetById(value.CompanyId) == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Found);
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

            #endregion

            try
            {
                //value.Id = Guid.NewGuid().ToString();
                usersService.Add(value);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject("Insert operation is a success"),
                    Encoding.UTF8, "application/json");
            }
            catch (System.Exception tEx)
            {
                System.Exception raisedException = tEx;
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                result.Content = new StringContent(JsonConvert.SerializeObject(raisedException.ToString()), Encoding.UTF8, "application/json");
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
        public HttpResponseMessage Put([FromBody] Users value)
        {
            HttpResponseMessage result = null;

            #region InputCheck

            if (!IsGuid(value.Id))
            {
                var message = JsonConvert.SerializeObject("This ID is not a GUID");
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
                result.Content = new StringContent(message, Encoding.UTF8, "application/json");
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
        public HttpResponseMessage Delete(string id)
        {
            HttpResponseMessage result = null;

            #region InputCheck
            if (!IsGuid(id))
            {
                var message = JsonConvert.SerializeObject("This ID is not a GUID");
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
                result.Content = new StringContent(message, Encoding.UTF8, "application/json");
                return result;
            }
            #endregion

            try
            {
                Users userToDelete = usersService.GetById(id);
                if (userToDelete == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.NotFound);
                    result.Content = new StringContent(JsonConvert.SerializeObject("User id doesn't exist in database"), Encoding.UTF8, "application/json");
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