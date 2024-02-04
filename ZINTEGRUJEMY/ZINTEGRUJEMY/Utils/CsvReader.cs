using CsvHelper.Configuration;
using Serilog;
using System.Globalization;

namespace ZINTEGRUJEMY
{
	public class CsvReader
	{
		public IEnumerable<T> ReadCsv<T>(string fileName)
		{
			var filePath = Path.Combine(@".\Data", fileName);

			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"File {fileName} doesn't exist.");
			}

			var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = ";",
				HasHeaderRecord = false,
				BadDataFound = (error) => { Log.Error($"Error when reading a record: {error.Context}; {error.Field}; {error.RawRecord}"); },
				ReadingExceptionOccurred = (error) => 
				{
					Log.Error($"Error when reading a record: {error.Exception.Message}"); 
					return false; 
				}
			};

			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvHelper.CsvReader(reader, conf))
			{
				csv.Context.RegisterClassMap<ProductCsvMap>();
				csv.Context.RegisterClassMap<PriceCsvMap>();
				csv.Context.RegisterClassMap<InventoryCsvMap>();

				csv.Read();
				return csv.GetRecords<T>().ToList();
			}
		}
	}
}