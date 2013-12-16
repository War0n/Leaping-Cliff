using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barometer_ASP_NET.Database;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.Messaging;
using System.IO;

namespace Barometer_ASP_NET.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/
        LDAPConnector connr = LDAPConnector.GetInstance();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult StartOAuth()
        {

            var serviceProvider = connr.GetServiceDescription();
            var consumer = new WebConsumer(serviceProvider, connr.tokenManager);

            // Url to redirect to TODO
            
            var authUrl = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/OAuthCallBack");

            // request access
            consumer.Channel.Send(consumer.PrepareRequestUserAuthorization(authUrl, null, null));

            // This will not get hit!
            return null;
        }

        public ActionResult OAuthCallback()
        {
            // Process result from the service provider
            var serviceProvider = connr.GetServiceDescription();
            var consumer = new WebConsumer(serviceProvider, connr.tokenManager);
            var accessTokenResponse = consumer.ProcessUserAuthorization();

            // If we didn't have an access token response, this wasn't called by the service provider
            if (accessTokenResponse == null)
                return RedirectToAction("Index");

            // Extract the access token
            string accessToken = accessTokenResponse.AccessToken;

            ViewBag.Token = accessToken;
            ViewBag.Secret = connr.tokenManager.GetTokenSecret(accessToken);
            return View();
        }
        /*
        public ActionResult Test2()
        {
            // Process result from linked in
            var LiServiceProvider = connr.GetServiceDescription();
            var linkedIn = new WebConsumer(LiServiceProvider, connr.tokenManager);
            var accessToken = connr.GetServiceDescription().AccessTokenEndpoint;

            // Retrieve the user's profile information
            var endpoint = new MessageReceivingEndpoint("http://api.linkedin.com/v1/people/~", HttpDeliveryMethods.GetRequest);
            var request = linkedIn.PrepareAuthorizedRequest(endpoint, accessToken);
            var response = request.GetResponse();
            ViewBag.Result = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return View();
        }*/
    }
         
}
