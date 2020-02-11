using Microsoft.AspNetCore.Http;
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
            string h = postedFile.ContentType.ToLower();
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (h != "image/jpg" &&
                        h != "image/jpeg" &&
                        h != "image/pjpeg" &&
                        h != "image/gif" &&
                        h != "image/x-png" &&
                        h != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            string i = Path.GetExtension(postedFile.FileName)
                .ToLower();

            if (i != ".jpg"
                && i != ".png"
                && i != ".gif"
                && i != ".jpeg")
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
}
