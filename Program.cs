ProcessFiles();

static void ProcessFiles()
{
    foreach (FileInfo fi in GetFileInfosFromDir(AppContext.BaseDirectory)) if (FileToBeProcessed(fi)) ProcessFile(fi);
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

static bool FileToBeProcessed(FileInfo fi)
{
    var extension = fi.Extension;
    return extension.Length > 0 && !extension.Contains("qt") && !extension.Contains("exe");
}

static IEnumerable<FileInfo> GetFileInfosFromDir(string path) => new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);
