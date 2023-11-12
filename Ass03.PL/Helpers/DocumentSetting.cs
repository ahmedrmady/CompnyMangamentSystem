using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.PL.Helpers
{
    public   class DocumentSetting
    {

        public  static async Task<string> UploadFileAsync (IFormFile file , string FolderName)
        {
            // 1.get folder path 
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);

            //2. get file name (unique)

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. file path 
            var filePath = Path.Combine(folderPath,fileName);


            //4. save file o stream (data per time)
           using var fileStream = new FileStream(filePath, FileMode.CreateNew);


            //5. copy file
           await file.CopyToAsync(fileStream);



            return fileName;
        }

        public static void DeleteFile (string fileName, string FolderName)
        {
           

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, fileName);
            var result = Directory.Exists(filePath);
            //var di = @"C:\\Users\\Rmady\\Desktop\\DotNet\\Route ASP C40 - 2023\\RouteAssignments\\MvcAss04 Solution\\Ass03.PL\\wwwroot\\files\\images\\dce177f0 - 32c5 - 4432 - 8259 - 9f71728d0a25Problem 3.png";
           if ( Directory.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
