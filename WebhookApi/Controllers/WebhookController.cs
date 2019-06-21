using Google.Cloud.Dialogflow.V2;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebhookApi.Controllers
{
    [Route("webhook")]
	public class WebhookController : Controller
	{

		[HttpPost]
		public async Task<JsonResult> GetWebhookResponse([FromBody] WebhookRequest request)
		{
			
			var pas = request.QueryResult.Parameters;
			var askingName = pas.Fields.ContainsKey("name") && pas.Fields["name"].ToString().Replace('\"', ' ').Trim().Length > 0;
			var askingAddress = pas.Fields.ContainsKey("address") && pas.Fields["address"].ToString().Replace('\"', ' ').Trim().Length > 0;
			var askingBusinessHour = pas.Fields.ContainsKey("business-hours") && pas.Fields["business-hours"].ToString().Replace('\"', ' ').Trim().Length > 0; 
			var response = new WebhookResponse();

			string name = "Jeffson Library", address="1234 Brentwood Lane, Dallas, TX 12345", businessHour="8:00 am to 8:00 pm";

			StringBuilder sb = new StringBuilder();

			if (askingName) {
				sb.Append("The name of library is: "+name+"; ");
			}

			if (askingAddress) {
				sb.Append("The Address of library is: "+address+"; ");
			}

			if (askingBusinessHour) {
				sb.Append("The Business Hour of library is: "+businessHour+"; ");
			}

			if (sb.Length == 0) {
				sb.Append("Greetings from our Webhook API!");
			}

			response.FulfillmentText = sb.ToString();

			return Json(response);
		}
	}
}
