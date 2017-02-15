using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Shipbob.Data;
using Shipbob.Repositories;
using Shipbob.Models;

namespace Shipbob.API.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private IShipbobRepository _SbRepo;

        //Injecting shipbobrepository via dependency injection container.
        public OrderController(IShipbobRepository SbRepo)
        {
            _SbRepo = SbRepo;
        }

        /// <summary>
        /// Passes in trackingID of the order and retrieves the order information from the repo
        /// </summary>
        /// <param name="trackingID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{trackingID}")]
        public IHttpActionResult Get(string trackingID)
        {

            OrderInfo ret = new OrderInfo();

            try
            {                 
                ret = _SbRepo.SearchForOrder(trackingID);
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

            return Ok(ret);
        }

        /// <summary>
        /// Pulls the existing item and bundle names for the UI to display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IHttpActionResult ListNames()
        {
            List<string> Names = new List<string>();

            try
            {
                Names = _SbRepo.GetItemBundleNames();
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

            return Ok(Names);
        }

        /// <summary>
        /// Adds order to the Orders table via the repo service call.
        /// Initial check to make sure the input values are valid.
        /// </summary>
        /// <param name="orderInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]OrderInfo orderInput)
        {
            try
            {
                if (orderInput == null
                    || (orderInput.BundlesQty == null && orderInput.ItemsQty == null))
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid Order Submission");
                }

                _SbRepo.CreateNewOrder(orderInput);
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

            return Ok("Your order has been created!");
        }
    }
}
