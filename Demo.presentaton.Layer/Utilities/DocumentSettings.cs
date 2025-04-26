namespace Demo.presentaton.Layer.Utilities
{
    public static class DocumentSettings
    {
        public static async Task <string> UploadFileAsync(IFormFile file , string folderName)
        {
            // craete FolderPath
            // //E:/NewFolder/MVCDemoSln/Demo.Pl/wwwroot/Files/FolderName
            //string folderPath = Directory.GetCurrentDirectory()+ @"wwwroot/Files";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files", folderName);

            // Create unique Name for File
            string fileName =$"{Guid.NewGuid}-{file.FileName}";
            // Create File path
            // //E:/NewFolder/MVCDemoSln/Demo.Pl/wwwroot/Files/FolderName/IMage.Png
            string filePath = Path.Combine(folderPath, fileName);
            // Create fileStream to save File
            using var stream = new FileStream(filePath, FileMode.Create);
            // Copy File to FileStream
            await file.CopyToAsync(stream);

            return fileName;
        }
        public static void DeleteFile(string folderName, string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files", folderName, fileName);

            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
