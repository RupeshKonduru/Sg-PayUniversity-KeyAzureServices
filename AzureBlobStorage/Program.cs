using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobExample
{
    class Program
    {
        private const string connectionString = "<Your-Azure-Storage-Connection-String>";
        private const string containerName = "mycontainer";
        private const string blobName = "sample.txt";
        private const string localFilePath = @"path\to\local\file.txt";

        static async Task Main(string[] args)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create the container if it does not exist
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            // Upload the file
            Console.WriteLine("Uploading file...");
            await blobClient.UploadAsync(localFilePath, true);
            Console.WriteLine("Upload complete.");

            // Download the file
            string downloadFilePath = @"path\to\download\file.txt";
            Console.WriteLine("Downloading file...");
            await blobClient.DownloadToAsync(downloadFilePath);
            Console.WriteLine("Download complete.");
        }
    }
}
