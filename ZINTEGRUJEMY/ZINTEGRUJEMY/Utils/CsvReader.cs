using System.Globalization;

namespace ZINTEGRUJEMY
{
	public class CsvReader
	{
		public IEnumerable<T> ReadCsv<T>(string fileName)
		{
			var filePath = Path.Combine("Data", fileName);

			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"File {fileName} doesn't exist.");
			}

			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
			{
				return csv.GetRecords<T>().ToList();
			}
		}
	}
}