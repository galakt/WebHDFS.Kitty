namespace WebHDFS.Kitty.DataModels
{
    public sealed class FileStatus
    {
        public FileStatus(long accessTime, int blockSize, string @group, long length, long modificationTime, string owner, string pathSuffix, string permission, int replication, string type)
        {
            AccessTime = accessTime;
            BlockSize = blockSize;
            Group = @group;
            Length = length;
            ModificationTime = modificationTime;
            Owner = owner;
            PathSuffix = pathSuffix;
            Permission = permission;
            Replication = replication;
            Type = type;
        }

        public long AccessTime { get; }

        public int BlockSize { get; }

        public string Group { get; }

        public long Length { get; }

        public long ModificationTime { get; }

        public string Owner { get; }

        public string PathSuffix { get; }

        public string Permission { get; }

        public int Replication { get; }

        public string Type { get; }
    }
}
