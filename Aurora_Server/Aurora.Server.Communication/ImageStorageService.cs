using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public class ImageStorageService
    {
        private readonly string _basePath;

        public ImageStorageService(string basePath)
        {
            _basePath = basePath;

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> SaveImageAsync(string fileName, string sourceImagePath, string username)
        {
            try
            {
                // Ensure the user-specific directory exists
                var userDirectory = Path.Combine(_basePath, username);
                if (!Directory.Exists(userDirectory))
                {
                    Directory.CreateDirectory(userDirectory);
                }

                // Generate a unique filename
                var uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now.Ticks}{Path.GetExtension(fileName)}";
                var destinationFilePath = Path.Combine(userDirectory, uniqueFileName);

                // Asynchronously copy the file
                using (var sourceStream = File.OpenRead(sourceImagePath))
                using (var destinationStream = File.Create(destinationFilePath))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }

                return destinationFilePath;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("An error occurred while saving the image.", ex);
            }
        }



    }
}
