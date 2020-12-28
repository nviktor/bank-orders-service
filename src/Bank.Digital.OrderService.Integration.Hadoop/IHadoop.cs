using System.Threading.Tasks;

namespace Bank.Digital.OrderService.Integration.Hadoop
{
    /// <summary>
    /// Интерфейс доступа к корпоративной ноде Hadoop
    /// </summary>
    public interface IHadoop
    {
        /// <summary>
        /// Отправить файл в Hadoop
        /// </summary>
        Task<HadoopFileInfo> UploadFileAsync(string hadoopPath, string fileName, byte[] file);

        /// <summary>
        /// Получить файл из Hadoop
        /// </summary>
        Task<HadoopFileInfo> DownloadFileAsync(string hadoopPath, string fileName, byte[] file);
    }
}
