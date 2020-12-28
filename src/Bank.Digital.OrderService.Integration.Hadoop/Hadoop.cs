using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Digital.OrderService.Integration.Hadoop
{
    /// <inheritdoc />
    public class Hadoop : IHadoop
    {
        private readonly HttpClient _httpClient;

        public Hadoop(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public Task<HadoopFileInfo> UploadFileAsync(string hadoopPath, string fileName, byte[] file)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<HadoopFileInfo> DownloadFileAsync(string hadoopPath, string fileName, byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
