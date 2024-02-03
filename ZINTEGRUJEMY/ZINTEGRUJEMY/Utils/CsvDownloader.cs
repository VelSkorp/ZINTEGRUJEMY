using System.Net;

namespace ZINTEGRUJEMY
{
	public class CsvDownloader
	{
		public async Task<string> DownloadAndSaveCsvAsync(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);
            var savePath = Path.Combine("Data", fileName);

            try
			{
				using (var client = new WebClient())
				{
					await client.DownloadFileTaskAsync(uri, savePath);
				}
            }
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while downloading and saving the CSV file: {ex.Message}");
            }

            return fileName;
        }
	}
}