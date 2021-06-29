using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using startupjob.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace startupjob.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;

        Dictionary<string, string> Appliciants_replace = new Dictionary<string, string>()
        {
            { "date", "" },
            { "candidateID", "" },
            { "offerID", "" },
            { "name", "LastName" },
            { "position","" },
            { "why","" },
            { "phone", "Phone" },
            { "email", "Email" },
            { "details", "Note" },
            { "linkedin", "UrlLinkedIn" },
            { "internalPositionName", "" },
            { "files", "" }
        };

        Dictionary<string, string> Jobresponse_replace = new Dictionary<string, string>()
        {
            { "date", "CreationTime" },
            { "candidateID", "JobAd_Id" },
            { "offerID", "" },
            { "name", "LastName" },
            { "position","" },
            { "why","" },
            { "phone", "Phone" },
            { "email", "Email" },
            { "details", "Web" },
            { "linkedin", "" },
            { "internalPositionName", "" },
            { "files", "Note" }
        };

        Dictionary<string, string> Doc_replace = new Dictionary<string, string>()
        {
            { "date", "CreationTime" },
            { "candidateID", "" },
            { "offerID", "" },
            { "name", "Name" },
            { "position","" },
            { "why","" },
            { "phone", "" },
            { "email", "" },
            { "details", "" },
            { "linkedin", "" },
            { "internalPositionName", "" },
            { "files", "" },
            //{ "", "Applicant_Id" },
            //{ "", "JobAdResponse_Id" },
        };

        public WebhookController(ILogger<WebhookController> logger)
        {
            _logger = logger;
        }

        // POST: webhook
        [HttpPost]
        public async Task<IActionResult> ReceiveEventsAsync([FromBody] object payload)
        {
            AppliciantsStore appliciants = new AppliciantsStore();
            JobAdResponsesStore jobresponse = new JobAdResponsesStore();
            DocumentsStore doc = new DocumentsStore();
            bool result = true;

            JObject json = JObject.Parse(payload.ToString());

            foreach (var element in json)
            {
                try
                {
                    appliciants.Update(Appliciants_replace[element.Key], element.Value.ToString());
                    jobresponse.Update(Jobresponse_replace[element.Key], element.Value.ToString());
                    doc.Update(Doc_replace[element.Key], element.Value.ToString());
                }
                catch { }
            }

            result &= await appliciants.Save();
            result &= await jobresponse.Save();
            result &= await doc.Save();

            return result
                ? Ok()
                : ValidationProblem();
        }

        [HttpGet]
        public string Get()
        {
            return "Webhook for startupjob is up and running. Sending [POST] requests to: '/webhook'.";
        }
    }
}
