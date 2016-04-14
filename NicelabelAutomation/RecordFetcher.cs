using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NicelabelAutomation
{
	class RecordFetcher
	{

		public IEnumerable<Record> GetRecords()
		{
			ICollection<Record> result = new List<Record>();
			Credential credential = new Credential();

			var builder = new SqlConnectionStringBuilder
			{
				DataSource = credential.ServerUrl,
				InitialCatalog = "promis",
				UserID = credential.Username,
				Password = credential.Password
			};

			using (var db = new SqlConnection(builder.ToString()))
			{
				db.Open();
				var cmd = new SqlCommand(@"select top 20
	                                        dbo.StudNumFormat(s.Studnum ) StudentNum
                                            , s.Studnum BarcodeID
	                                        , LRN
	                                        , dbo.fullname2(lastname, firstname,middlename,mi,1,nameextension) Fullname
	                                        , 'Grade ' + convert(nvarchar, year + 6) Grade	
	                                        , h.Cameracounter
	                                        , ContactName
	                                        , ContactAddress
	                                        , ContactNum

	                                        , dbo.FullAddress(h.street, h.barangay, h.town,1) Address
	                                        from students s inner join students_history h on s.studnum = h.studnum 
	                                        where batch ='2015-2016' and batchtype = 'regular'
                                        ", db);

				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Record record = new Record();

                    record.AddField("StudentNumber", reader["StudentNum"].ToString());
                    record.AddField("BarcodeID", reader["BarcodeID"].ToString());
                    record.AddField("LRN", reader["LRN"].ToString());
                    record.AddField("Name", reader["Fullname"].ToString().ToUpper());

                    record.AddField("Address", reader["Address"].ToString());
                    record.AddField("ContactAddress", reader["ContactAddress"].ToString());
                    record.AddField("ContactName", reader["ContactName"].ToString());
                    record.AddField("ContactNumber", reader["ContactNum"].ToString());
                    record.AddField("Grade", reader["Grade"].ToString());

                    //record.AddField("PictureBackground", reader["Grade"].ToString());

                    //var filename = string.Format(@"C:\_SQL Database\promis\Pictures\students\2015-2016\{0}.jpg", reader["CameraCounter"]);
                    var filename = string.Format(@"\\172.16.0.10\d$\SQL Database\promis\Pictures\students\2015-2016\{0}.jpg", reader["CameraCounter"]);
                    

                    if (System.IO.File.Exists(filename))
                    {
                        record.AddField("PictureFile", filename);
                    } else
                    {
                        record.AddField("PictureFile", "");
                    }

					result.Add(record);
				}
			}

			return result;
		}
	}


}
