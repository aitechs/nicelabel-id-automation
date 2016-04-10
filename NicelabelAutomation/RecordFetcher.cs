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
				InitialCatalog = "TestDB",
				UserID = credential.Username,
				Password = credential.Password
			};

			using (var db = new SqlConnection(builder.ToString()))
			{
				db.Open();
				var cmd = new SqlCommand("select * from [User]",db);

				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Record record = new Record();
					record.AddField("StudNum", reader["AutoId"].ToString());
					record.AddField("Name", reader.GetString(reader.GetOrdinal("username")));

					result.Add(record);
				}
			}

			return result;
		}
	}


}
