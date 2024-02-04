using CsvHelper.Configuration;
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

			using (var reader = new StreamReader(filePath))
			{
				var headerLine = reader.ReadLine();
				var delimiter = headerLine.Contains(";") ? ";" : ",";
				var config = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					Delimiter = delimiter,
					HasHeaderRecord = false,
					ShouldSkipRecord = record => record.Row.GetField(0).Equals("__empty_line__"),
				};


				using (var csv = new CsvHelper.CsvReader(reader, config))
				{
					csv.Context.RegisterClassMap<ProductCsvMap>();
					csv.Context.RegisterClassMap<PriceCsvMap>();
					csv.Context.RegisterClassMap<InventoryCsvMap>();

					return csv.GetRecords<T>().ToList();
				}
			}
		}
	}
}