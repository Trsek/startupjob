using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using startupjob.mvc.Models;

namespace startupjob.mvc.Controllers
{
    [Route("webhook")]
    public class WebhooksController : Controller
    {
        private readonly SubscribersDbContext _db;

        public WebhooksController(SubscribersDbContext subscribersDb)
        {
            // Get instance for the DbContext from the DI
            _db = subscribersDb;
        }

        // POST: /
        [HttpPost]
        public async Task<IActionResult> ReceiveEventsAsync([FromBody] JObject payload)
        {
            // Check if the request paload could be parsed as JSON array
            if (!ModelState.IsValid)
                return BadRequest();
            
            JObject json = JObject.Parse(payload.ToString());

            // Check if the email already exist
            string email = (string)json["Email"];

            var subscriber = _db.Subscribers.FirstOrDefault(s => s.Email == email);
            if (subscriber != null)
            {
                // Unsubscribe
                subscriber.Subscribed = false;
            }

            // Save changes to the database
            await _db.SaveChangesAsync();
            return Ok();
        }

        // GET: /
        [Route("/")]
        [HttpGet]
        public IActionResult Home()
        {
            return Json(new { message = "Webhook for startupjob is up and running. Sending [POST] requests to: '/webhook'." });
        }
    }
}