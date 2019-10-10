using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OCFX.DataModels;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace OCFX.Data.Methods
{
    public static class Checkers
    {
        public const int ImageMinimumBytes = 512;

        /// <summary>
        /// Inspects a file to see if it is an image or not.
        /// </summary>
        /// <param name="postedFile">The questionable image file.</param>
        /// <returns></returns>
        public static bool IsImage(this IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    public static class ImageFileManagement
    {
        /// <summary>
        /// Uploads an image to a designated folder on the server
        /// </summary>
        /// <param name="_environment">The Hosting Environment</param>
        /// <param name="Image">IFormFile Image</param>
        /// <param name="PhotoType">An instance of a Photo type object</param>
        /// <param name="Id">An ID</param>
        /// <param name="PhotoFolder">Name of the folder</param>
        /// <param name="Caption">Optional caption for the photo</param>
        public static void UploadImageToFolder(
            IHostingEnvironment _environment,
            IFormFile Image,
            Photo PhotoType,
            int Id,
            string PhotoFolder,
            string Caption = "")
        {
            // Create the filename and folder path
            string fileName = GetUniqueName(Image.FileName);
            string folderPath = $"images/{Id}/{PhotoFolder}";
            string upload = Path.Combine(_environment.WebRootPath, folderPath);

            // Check if the folder already exists
            CheckFolderPath(upload);
            string filePath = Path.Combine(upload, fileName);

            // Get ready to upload and add some things.
            Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
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
