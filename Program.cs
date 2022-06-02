using System;
using System.IO;

namespace MakeDirCopy
{
    class Program
    {
        static void Main() => ProcessFiles();

        private static void ProcessFiles()
        {
            foreach (FileInfo fi in GetFileInfosFromDir(AppDomain.CurrentDomain.BaseDirectory)) if (FileToBeProcessed(fi)) ProcessFile(fi);
        }

        private static void ProcessFile(FileInfo fi)
        {
            var d = GetNewDirName(fi);
            CreateDir(d);
            MoveFile(fi, d);
        }

        private static string GetNewDirName(FileInfo fi) => Path.Combine(fi.DirectoryName, Path.GetFileNameWithoutExtension(fi.Name));

        private static void MoveFile(FileInfo fi, string dir) => File.Move(fi.FullName, Path.Combine(dir, fi.Name));

        private static void CreateDir(string dir) => Directory.CreateDirectory(dir);

        private static bool FileToBeProcessed(FileInfo fi) => !(fi.Attributes.HasFlag(FileAttributes.Directory) || fi.Extension.Contains("qt") || fi.Extension.Contains("exe"));

        private static FileInfo[] GetFileInfosFromDir(string path) => new DirectoryInfo(path).GetFiles("*.*", SearchOption.TopDirectoryOnly);
    }
}