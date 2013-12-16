using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Configuration;
using DotNetOpenAuth.Xrds;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OpenId.Extensions.OAuth;
using DotNetOpenAuth.AspNet;


namespace Barometer_ASP_NET.Database
{
	public class LDAPConnector
	{
         private static LDAPConnector instance;
         public  InMemoryOAuthTokenManager tokenManager;       

        public static LDAPConnector GetInstance()
        {
           
            if (instance == null)
                instance = new LDAPConnector("73d120c3cebc89b2a344ca8b7e434d8f30349c70", "0179ed703f5f15277cefef2ec0259646dc4e2d36");
            return instance;
            
        }

        //Handles the connection info
        public   ServiceProviderDescription GetServiceDescription()
        {
             return new ServiceProviderDescription
            {
            RequestTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/request_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/saml.php", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            AccessTokenEndpoint = new MessageReceivingEndpoint("https://publicapi.avans.nl/oauth/access_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
            TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() }
            };
        }

        
         public LDAPConnector(String consumerKey,String consumerSecret) 
        {
            tokenManager = new InMemoryOAuthTokenManager(consumerKey, consumerSecret);  
             
        }

        
        
       
        public DotNetOpenAuth.AspNet.AuthenticationResult VerifyAuthenticationCore(DotNetOpenAuth.OAuth.Messages.AuthorizedTokenResponse response)
        {
            //Perform the verification process
            return new AuthenticationResult(true);
        }
	}

}