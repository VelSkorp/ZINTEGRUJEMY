using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace ZINTEGRUJEMY
{
	public class CustomDoubleConverter : DoubleConverter
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			if (text.Equals("O", StringComparison.OrdinalIgnoreCase))
			{
				return 0.0;
			}

			return base.ConvertFromString(text, row, memberMapData);
		}
	}
}