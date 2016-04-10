using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicelabelAutomation
{
	public class Record
	{
		private Dictionary<string,string> _Fields = new Dictionary<string, string>();

		public void AddField(string fieldName, string value)
		{
			_Fields.Add(fieldName,value);	
		}

		public Dictionary<string, string> Fields()
		{
			return _Fields;
		}
	}
	
}
