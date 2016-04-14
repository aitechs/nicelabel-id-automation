using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceLabel5WR;
using System.IO;

namespace NicelabelAutomation
{
    public  class IdGenerator: IDisposable
    {
	    private NiceApp _niceLabel;
	    private ICollection<Record> _records; 
		private string LabelFilename;

	    public string Printer { get; set; }
	    public Dictionary<string,string> Data { get; set; }

	    public IdGenerator(string labelFile )
	    {
            if (!System.IO.File.Exists(labelFile)) throw new Exception("File Does NOT Exists!");
		    _niceLabel = new NiceApp();
		    LabelFilename = labelFile;
			_records = new List<Record>();
	    }

	    public void Dispose()
	    {
            //throw new NotImplementedException();
            try
            {
                _niceLabel.Quit();
                _niceLabel = null;
            }
            catch
            {

            }
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
            FileInfo info = new FileInfo(LabelFilename);
            
            RecordFetcher fetcher = new RecordFetcher();
		    _records = fetcher.GetRecords().ToList();
            int labelId = _niceLabel.LabelOpen(info.FullName);

            _niceLabel.LabelSetPrinter(labelId, printerName);
            _niceLabel.LabelSessionStart(labelId);

            //var ret = _niceLabel.LabelSetVar(labelId, "Name", "HaroldCris",0, 0);
            //_niceLabel.LabelSessionPrint(labelId, "1");

            foreach (Record item in _records)
            {
                foreach (var field in item.Fields())
                {
                    //Setting: StudentNumber
                    //Setting : BarcodeID
                    //Setting : LRN
                    //Setting : Name
                    //Setting : Address
                    //Setting : ContactAddress
                    //Setting : ContactPerson
                    //Setting : Grade
                    //Setting : PictureBackground

                    Console.WriteLine("Setting : " + field.Key);
                    bool ret = _niceLabel.LabelSetVar(labelId, field.Key, field.Value, 0, 0);
                }

                //var f = item.Fields();
                //string value;

                //value = f.First(x => x.Key == "StudentNumber").Value;
                //var ret = _niceLabel.LabelSetVar(labelId, "StudentNumber", value, 0, 0);

                //value = f.First(x => x.Key == "BarcodeID").Value;
                //_niceLabel.LabelSetVar(labelId, "BarcodeID", value, 0, 0);

                //value = f.First(x => x.Key == "LRN").Value;
                //_niceLabel.LabelSetVar(labelId, "LRN", value, 0, 0);

                //value = f.First(x => x.Key == "Name").Value;
                //_niceLabel.LabelSetVar(labelId, "Name", value, 0, 0);

                //value = f.First(x => x.Key == "Address").Value;
                //_niceLabel.LabelSetVar(labelId, "Address", value, 0, 0);

                //value = f.First(x => x.Key == "ContactName").Value;
                //_niceLabel.LabelSetVar(labelId, "ContactName", value, 0, 0);

                //value = f.First(x => x.Key == "ContactAddress").Value;
                //_niceLabel.LabelSetVar(labelId, "ContactAddress", value, 0, 0);

                //value = f.First(x => x.Key == "ContactNumber").Value;
                //_niceLabel.LabelSetVar(labelId, "ContactNumber", value, 0, 0);


                //value = f.First(x => x.Key == "Grade").Value;
                //_niceLabel.LabelSetVar(labelId, "Grade", value, 0, 0);

                //value = f.FirstOrDefault(x => x.Key == "PictureFile").Value;
                //if(value != null)
                //    _niceLabel.LabelSetVar(labelId, "PictureFile", value, 0, 0);

                _niceLabel.LabelSessionPrint(labelId, "1");
            }
            _niceLabel.LabelSessionEnd(labelId);
            _niceLabel.LabelClose(labelId);
	    }
    }

	
}
