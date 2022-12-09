using DaySeven;

var input = File.ReadLines("PuzzleInput.txt");
var folders = new List<Folder>();
var currentFolder = new Folder();
foreach (var line in input)
{
    if (line.StartsWith("$"))
    {
        if (line.StartsWith("$ cd "))
        {
            if (line.StartsWith("$ cd /"))
            {
                if (!folders.Any(x => x.Name == "/"))
                {
                    folders.Add(new Folder()
                    {
                        Name = "/",
                        Id = Guid.NewGuid()
                    });
                }
                currentFolder = folders.First(x => x.Name == "/");

            }
            else if (line.StartsWith("$ cd .."))
            {
                currentFolder = folders.First(x => x.Id == currentFolder.ParentFolderId);
            }
            else // cd subdirectory
            {
                var folderName = line.Split(" ").Last();
                if (!folders.Any(x => x.Name == folderName && x.ParentFolderId == currentFolder.Id))
                {
                    folders.Add(new Folder()
                    {
                        Name = folderName,
                        Id = Guid.NewGuid(),
                        ParentName = currentFolder.Name,
                        ParentFolderId = currentFolder.Id
                    });
                }
                currentFolder = folders.First(x => x.Name == folderName && x.ParentFolderId == currentFolder.Id);
            }
        }
    }
    else
    {
        if (line.StartsWith("dir "))
        {
            var folderName = line.Split(" ").Last();
            if (!folders.Any(x => x.Name == folderName && x.ParentFolderId == currentFolder.Id))
            {
                var guid = Guid.NewGuid();
                folders.Add(new Folder()
                {
                    Name = folderName,
                    Id = guid,
                    ParentName = currentFolder.Name,
                    ParentFolderId = currentFolder.Id
                });
                currentFolder.Subdirectories.Add(guid);
            }
        }
        else
        {
            // File
            var splitLine = line.Split(" ");
            var size = Int64.Parse(splitLine[0]);
            var fileName = splitLine[1];
            currentFolder.Files.Add(fileName, size);
        }
    }
}

var bottomDirs = folders.Where(x => x.Subdirectories.Count == 0).ToList();
while (bottomDirs.Any())
{
    // For each folder at bottom level
    foreach (var subDir in bottomDirs)
    {
        if (subDir.ExceededSize) continue;

        var size = subDir.Files.Values.Sum() + folders.Where(x => subDir.Subdirectories.ToList().Contains(x.Id)).Select(x => x.Size).Sum();
        if (size > 100000)
        {
            subDir.ExceededSize = true;
            // For all directories above this directory, also set ExceededSize to true
            var parentFolder = folders.FirstOrDefault(x => x.Id == subDir.ParentFolderId);
            while (parentFolder is not null)
            {
                parentFolder.ExceededSize = true;
                var folderId = parentFolder.ParentFolderId;
                parentFolder = folders.FirstOrDefault(x => x.Id == folderId);
            }
        }
        else
        {
            subDir.Size = size;
        }
    }
    var parentFolderIds = bottomDirs.Select(x => x.ParentFolderId);
    bottomDirs = folders.Where(f => parentFolderIds.Contains(f.Id) && f.ExceededSize == false).ToList();
}

Console.WriteLine(folders.Where(x => x.ExceededSize == false).Select(x => x.Size).Where(x => x < 100000).Sum());
