using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NicelabelAutomation
{
	public class Credential
	{
		XmlDocument config = new XmlDocument();

		public string Username { get; set; }
		public string Password { get; set; }
		public string ServerUrl { get; set; }

		public Credential(string credentialType = "ProMisCredential")
		{
			if (System.IO.File.Exists("credential.config"))
			{
				config.Load("credential.config");
			}
			else
			{
				if (System.IO.File.Exists("credential.sample.config"))
				{
					config.Load("credential.sample.config");
				}
				else
				{
					throw new FileNotFoundException("File credential.sample.config not found");
				}
			}

			var node = config.DocumentElement?.SelectSingleNode(credentialType);
			var xmlAttributeCollection = node?.Attributes;
			if (xmlAttributeCollection == null) throw new ArgumentException("Invalid Credential Type");

			Username = xmlAttributeCollection["username"].Value;
			Password = xmlAttributeCollection["password"].Value;
			ServerUrl = xmlAttributeCollection["url"].Value;
		}


	}
}
