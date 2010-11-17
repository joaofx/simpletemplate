
namespace SimpleTemplate
{
    using System.IO;

    public static class FileSystemUtil
    {
        public static void CreateDirectoryIfNotExist(string file)
        {
            var path = Path.GetDirectoryName(file);

            if (string.IsNullOrEmpty(path) == false && Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void MoveFile(string tempFile, string destinationFile)
        {
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }

            File.Move(tempFile, destinationFile);
        }

        public static void DeleteIfExist(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public static string GetAllText(string fileName)
        {
            using (var reader = new StreamReader(File.OpenRead(fileName)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}