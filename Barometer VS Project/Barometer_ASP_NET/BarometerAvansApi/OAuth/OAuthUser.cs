using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oAuthDemo.OAuth
{
    public class OAuthUser
    {
        public string Id { get; set; }
        public string Inlognaam { get; set; }
		public int TelefoonExtern { get; set; }
		public int Toestel { get; set; }
		public string Voornaam { get; set; }
		public string Achternaam { get; set; }
		public string OU { get; set; }
		public string Locatie { get; set; }
		public string Kamer { get; set; }
		public string Mail { get; set; }
		public string Werkdagen { get; set; }
		public string Titel { get; set; }
    }
}