using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceLabel5WR;

namespace NicelabelAutomation
{
    public  class IdGenerator:IDisposable
    {
	    private NiceApp _niceLabel;
	    private ICollection<Record> _records; 
		private string LabelFilename;

	    public string Printer { get; set; }
	    public Dictionary<string,string> Data { get; set; }

	    public IdGenerator(string labelFile )
	    {
		    _niceLabel = new NiceApp();
		    LabelFilename = labelFile;
			_records = new List<Record>();
	    }

	    public void Dispose()
	    {
		    //throw new NotImplementedException();
		    _niceLabel.Quit();
		    _niceLabel = null;
	    }


	    public void AddRecord(Record item)
	    {
		    _records.Add(item);

			//Record i = new Record();
			//i.AddFields( new List<Dictionary<string, string>>()
			//{ 
			//	new Dictionary<string, string>() { { "field1","value" } },
			//	new Dictionary<string, string>() { { "field1","value" } },
			//});    
	    }

	    public void Print(string printerName)
	    {
			RecordFetcher fetcher = new RecordFetcher();
		    _records = fetcher.GetRecords().ToList();

		    NiceLabel label = _niceLabel.LabelOpenEx (LabelFilename);
		    label.PrinterName = printerName;
		    label.SessionStart();
		    foreach (var item in _records)
		    {
			    foreach (var field in item.Fields())
			    {
					label.Variables.FindByName(field.Key).SetValue(field.Value);
				}

				label.SessionPrint("1");
			}
			
		    label.SessionEnd();
	    }
    }

	
}
