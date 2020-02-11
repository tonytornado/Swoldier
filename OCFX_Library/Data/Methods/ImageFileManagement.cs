using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OCFX.DataModels;
using System;
using System.IO;

namespace OCFX.Data.Methods
{
    public static class ImageFileManagement
    {
        /// <summary>
        /// Uploads an image to a designated folder on the server
        /// </summary>
        /// <param name="environment">The Hosting Environment</param>
        /// <param name="Image">IFormFile Image</param>
        /// <param name="PhotoType">An instance of a Photo type object</param>
        /// <param name="Id">An ID</param>
        /// <param name="PhotoFolder">Name of the folder</param>
        /// <param name="Caption">Optional caption for the photo</param>
        public static async System.Threading.Tasks.Task UploadImageToFolderAsync(IHostingEnvironment environment, IFormFile Image, Photo PhotoType, int Id,
                                               string PhotoFolder, string Caption = "")
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (Image is null)
            {
                throw new ArgumentNullException(nameof(Image));
            }

            if (PhotoType is null)
            {
                throw new ArgumentNullException(nameof(PhotoType));
            }

            if (string.IsNullOrEmpty(PhotoFolder))
            {
                throw new ArgumentException("message", nameof(PhotoFolder));
            }

            if (string.IsNullOrEmpty(Caption))
            {
                throw new ArgumentException("message", nameof(Caption));
            }
            // Create the filename and folder path
            string fileName = GetUniqueName(Image.FileName);
            string folderPath = $"images/{Id}/{PhotoFolder}";
            string upload = Path.Combine(environment.WebRootPath, folderPath);

            // Check if the folder already exists
            CheckFolderPath(upload);
            string filePath = Path.Combine(upload, fileName);

            // Get ready to upload and add some things.
            await Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            PhotoType.URL = $"../images/{Id}/{PhotoFolder}/{fileName}";
            PhotoType.Caption = Caption;
        }

        /// <summary>
        /// Checks for folder on the server; and creates it if necessary
        /// </summary>
        /// <param name="folderPath">The folder path</param>
        private static void CheckFolderPath(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// Create a unique file name for the file being uploaded.
        /// </summary>
        /// <param name="fileName">A filename string</param>
        /// <returns></returns>
        private static string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileName)}";
        }
    }

}
