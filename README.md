# MakeDirCopy
Create separate folders for files in current folder
I use this to organise downloaded torrents so that each completed file is in its own folder. This makes it cleaner for use in Kodi. 

Requires .NET SDK 10.0.403 or a newer patch in the same SDK feature band to build.
To build a standalone Windows x64 executable, run:

```powershell
dotnet publish -r win-x64 -c Release
```

Native AOT publishing also requires Visual Studio 2026 (or its Build Tools) with the **Desktop development with C++** workload and Windows SDK installed.

The executable is at `bin\Release\net10.0\win-x64\publish\MakeDirCopy.exe`.
Copy it into the folder to organize, then run it. It processes files beside the
executable, regardless of the working directory, and does not scan subfolders.
Extensions containing `qt` or `exe` are skipped (case-sensitive, as before).
Files without an extension are skipped because their folder would have the same name as the file.
Existing destination files are never overwritten.

The release build uses .NET 10 Native AOT, favoring execution speed with no JIT
startup or separate runtime installation. File enumeration is streamed to avoid
allocating an array for the entire directory. Files are moved on the same volume
rather than copied, so their contents do not need to be read and rewritten.
