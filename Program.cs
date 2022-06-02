ProcessFiles();

static void ProcessFiles()
{
    foreach (FileInfo fi in GetFileInfosFromDir(AppDomain.CurrentDomain.BaseDirectory)) if (FileToBeProcessed(fi)) ProcessFile(fi);
}

static void ProcessFile(FileInfo fi)
{
    var d = GetNewDirName(fi);
    CreateDir(d);
    MoveFile(fi, d);
}

static string GetNewDirName(FileInfo fi) => Path.Combine(fi.DirectoryName is null ? string.Empty : fi.DirectoryName, Path.GetFileNameWithoutExtension(fi.Name));

static void MoveFile(FileInfo fi, string dir) => File.Move(fi.FullName, Path.Combine(dir, fi.Name));

static void CreateDir(string dir) => Directory.CreateDirectory(dir);

static bool FileToBeProcessed(FileInfo fi) => !(fi.Attributes.HasFlag(FileAttributes.Directory) || fi.Extension.Contains("qt") || fi.Extension.Contains("exe"));

static FileInfo[] GetFileInfosFromDir(string path) => new DirectoryInfo(path).GetFiles("*.*", SearchOption.TopDirectoryOnly);
