using Serilog;

namespace ZINTEGRUJEMY
{
	public class CsvDownloader
    {
        public async Task<string> DownloadAndSaveCsvAsync(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);
            var savePath = Path.Combine(@".\Data", fileName);

            if (File.Exists(savePath))
            {
                Log.Information($"{fileName} file upload has skiped, the file already exists");
                return fileName;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.ConnectionClose = false;
                httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                httpClient.Timeout = Timeout.InfiniteTimeSpan;

                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    return savePath;
                }
            }
        }
    }
}