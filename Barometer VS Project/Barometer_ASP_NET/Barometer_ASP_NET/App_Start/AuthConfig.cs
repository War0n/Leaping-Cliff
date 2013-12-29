using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BarometerAvansApi.Models;
using BarometerAvansApi.OAuth;

namespace Barometer_ASP_NET
{
	public static class AuthConfig
	{
		public static void RegisterAuth()
		{
			// To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			// you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

			//OAuthWebSecurity.RegisterMicrosoftClient(
			//    clientId: "",
			//    clientSecret: "");

			//OAuthWebSecurity.RegisterTwitterClient(
			//    consumerKey: "",
			//    consumerSecret: "");

			//OAuthWebSecurity.RegisterFacebookClient(
			//    appId: "",
			//    appSecret: "");

			//OAuthWebSecurity.RegisterGoogleClient();

			OAuthWebSecurity.RegisterClient(new AvansOAuthClient("73d120c3cebc89b2a344ca8b7e434d8f30349c70", "0179ed703f5f15277cefef2ec0259646dc4e2d36"), "Avans", null);
		}
	}
}
