namespace DaySeven
{
    public class Folder
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid? ParentFolderId;
        public string? ParentName { get; set; }
        public HashSet<Guid> Subdirectories { get; set; } = new HashSet<Guid>();
        public Dictionary<string, long> Files = new Dictionary<string, long>();
        public long Size;
        public bool ExceededSize = false;
    }
}
