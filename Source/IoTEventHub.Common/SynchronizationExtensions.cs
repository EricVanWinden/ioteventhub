using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IoTEventHub.Common
{
    /// <summary>
    /// Class with methods to synchronize data with the Azure file share
    /// </summary>
    public static class SynchronizationExtensions
    {
        /// <summary>
        /// https://github.com/Azure/azure-sdk-for-net/blob/Azure.Storage.Files.Shares_12.6.0/sdk/storage/Azure.Storage.Files.Shares/README.md
        /// Get a connection string to our Azure Storage account. You can obtain your connection string from the Azure Portal (click Access Keys under Settings in the Portal Storage account blade)
        /// </summary>
        private const string _connectionString = "<connection>";
        
        /// <summary>
        /// The name of the fileshare
        /// </summary>
        private const string _shareName = "weatherfileshare";
        
        /// <summary>
        /// The directory within the share
        /// </summary>
        private const string _dirName = "";
        
        /// <summary>
        /// Uploads the statistics to Azure fileshare
        /// </summary>
        /// <param name="statistics"></param>
        /// <returns></returns>
        public static void PutStatistics(List<Statistics> statistics, string fileName)
        {          
            ShareClient share = new ShareClient(_connectionString, _shareName);
            ShareDirectoryClient directory = share.GetDirectoryClient(_dirName);
            ShareFileClient file = directory.GetFileClient(fileName);
            var data = ToBytes(statistics);
            using (MemoryStream stream = new MemoryStream(data.Length))
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                file.Create(stream.Length);
                file.UploadRange(new HttpRange(0, stream.Length), stream);
            }
        }

        /// <summary>
        /// Gets the statistics
        /// </summary>
        /// <returns></returns>
        public static List<Statistics> GetStatistics()
        {
            var fileName = "Statistics.json";
            var result = DownloadFile(fileName);
            return JsonConvert.DeserializeObject<List<Statistics>>(result);
        }

        /// <summary>
        /// Gets the statistics
        /// </summary>
        /// <returns></returns>
        public static HubConfig GetHubConfig()
        {
            var fileName = "HubConfig.json";
            var result = DownloadFile(fileName);
            return JsonConvert.DeserializeObject<HubConfig>(result);
        }

        /// <summary>
        /// Downloads a file and reads the content
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        private static string DownloadFile(string fileName)
        {
            // Get a reference to the file
            ShareClient share = new ShareClient(_connectionString, _shareName);
            ShareDirectoryClient directory = share.GetDirectoryClient(_dirName);
            ShareFileClient file = directory.GetFileClient(fileName);

            // Download the file
            ShareFileDownloadInfo download = file.Download();

            // Read
            var encoding = new UTF8Encoding(true);
            string result;
            using (MemoryStream stream = new MemoryStream(100))
            {
                download.Content.CopyTo(stream);
                var byteArray = stream.ToArray();
                result = encoding.GetString(byteArray);
            }
            return result;
        }

        /// <summary>
        /// Serialializes the statistics to a string and convert the string to a byte array
        /// </summary>
        /// <param name="statistics">The statistics</param>
        /// <returns>A byte array</returns>
        private static byte[] ToBytes(List<Statistics> statistics)
        {
            var encoding = new UTF8Encoding(true);
            var data = JsonConvert.SerializeObject(statistics);
            return encoding.GetBytes(data);
        }
    }
}
