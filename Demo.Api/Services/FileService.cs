using Azure.Storage.Blobs;
using Demo.Api.Models;

namespace Demo.Api.Services
{
    public class FileService : IFileService
    {
        private IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFile(IFormFile file) 
        {
            var storageConnectionString = _configuration["StorageAccount:ConnectionString"];
            var containerName = "songcover";
            //Upload a file on blob
            var blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);

            var isSaved = blobContainerClient.GetBlobs().Any(f => f.Name == file.FileName);

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            
            if (!isSaved)
            {
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await blobClient.UploadAsync(memoryStream);
            }

            return  blobClient.Uri.AbsoluteUri; 
        }
    }
}
