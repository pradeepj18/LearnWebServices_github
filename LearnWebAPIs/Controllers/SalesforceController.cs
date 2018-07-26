using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LearnWebAPIs.Controllers
{
    
    [Produces("application/json")]
    [Route("api/Salesforce")]
    public class SalesforceController : Controller
    {
        private const string LOGIN_ENDPOINT = "https://login.salesforce.com/services/oauth2/token";
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; }
        public string InstanceUrl { get; set; }

        [HttpGet("login")]
        [Produces("application/json")]
        public string Login()
        {
            String jsonResponse;
            using (var client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", "3MVG9d8..z.hDcPLMwnhCIndM36TjVbIGMyNbyFU8obBz_IVqYsDe9NuFbCmp3M98YHhfjra2ibpqmlB1.m2d"},
                    {"client_secret", "3364958416727039608"},
                    {"username", "info.mca2016@gmail.com"},
                    {"password", "info.salesforce2020fJO7xUW9mFoHoW7NZ55024iAa"}
                }
                );
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
                jsonResponse = response.Content.ReadAsStringAsync().Result;
            }
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
             AuthToken = values["access_token"];
             InstanceUrl = values["instance_url"];
            using (var client = new HttpClient())
            {
                string restRequest = InstanceUrl + "/services/data/v42.0/" + "query/?q=select Name from Account limit 1";
                var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
                request.Headers.Add("Authorization", "Bearer " + AuthToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.SendAsync(request).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
 
        }
        
    }
}