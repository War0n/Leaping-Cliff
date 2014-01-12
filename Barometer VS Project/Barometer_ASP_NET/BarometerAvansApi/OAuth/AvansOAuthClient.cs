using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Barometer_ASP_NET.Database;
using System.Web;


namespace oAuthDemo.OAuth
{
    public class AvansOAuthClient : OAuthClient
    {
        private static string consumerKey;
        private static string consumerSecret;

        static AvansOAuthClient()
        {
            consumerKey = ConfigurationManager.AppSettings["AvansOAuthConsumerKey"];
            consumerSecret = ConfigurationManager.AppSettings["AvansOAuthConsumerSecret"];
        }

        public static readonly ServiceProviderDescription AvansServiceDescription = new ServiceProviderDescription
        {
            RequestTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/request_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/login.php", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            AccessTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/access_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new PlaintextSigningBindingElement() }
        };

        public AvansOAuthClient(string consumerKey, string consumerSecret) :
            this(consumerKey, consumerSecret, new AuthenticationOnlyCookieOAuthTokenManager())
        {
        }

        public AvansOAuthClient(string consumerKey, string consumerSecret, IOAuthTokenManager tokenManager)
            : base("avans", AvansServiceDescription, new SimpleConsumerTokenManager(consumerKey, consumerSecret, tokenManager))
        {
        }

        protected override AuthenticationResult VerifyAuthenticationCore(AuthorizedTokenResponse response)
        {
            var profileEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/studentnummer/?format=json", HttpDeliveryMethods.GetRequest);
            string accessToken = response.AccessToken;
            consumerKey = ConfigurationManager.AppSettings["AvansOAuthConsumerKey"];
            consumerSecret = ConfigurationManager.AppSettings["AvansOAuthConsumerSecret"];
            InMemoryOAuthTokenManager tokenManager = new InMemoryOAuthTokenManager(consumerKey, consumerSecret);
            tokenManager.ExpireRequestTokenAndStoreNewAccessToken(String.Empty, String.Empty, accessToken, (response as ITokenSecretContainingMessage).TokenSecret);
            WebConsumer webConsumer = new WebConsumer(AvansServiceDescription, tokenManager);

            HttpWebRequest request = webConsumer.PrepareAuthorizedRequest(profileEndpoint, accessToken);

            try
            {
                using (WebResponse profileResponse = request.GetResponse())
                {
                    using (Stream profileResponseStream = profileResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(profileResponseStream))
                        {
                            string jsonText = reader.ReadToEnd();

                            var user = JsonConvert.DeserializeObject<List<OAuthUser>>(jsonText);

                            Dictionary<string, string> extraData = new Dictionary<string, string>();
                            extraData.Add("Id", user[0].Id ?? "Onbekend");
                            extraData.Add("Login", user[0].Inlognaam ?? "Onbekend");

                            return new DotNetOpenAuth.AspNet.AuthenticationResult(true, ProviderName, extraData["Id"], extraData["Login"], extraData);
                        }
                    }
                }
                return new AuthenticationResult(false);
            }
            catch (WebException ex)
            {
                using (Stream s = ex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        string body = sr.ReadToEnd();
                        return new DotNetOpenAuth.AspNet.AuthenticationResult(new Exception(body, ex));
                    }
                }
            }
        }

	}
}