namespace WebHDFS.Kitty.DataModels
{
    public sealed class ContentSummary
    {
        public ContentSummary(long directoryCount, long fileCount, long length, long quota, long spaceConsumed, long spaceQuota)
        {
            DirectoryCount = directoryCount;
            FIleCount = fileCount;
            Length = length;
            Quota = quota;
            SpaceConsumed = spaceConsumed;
            SpaceQuota = spaceConsumed;
        }

        public long DirectoryCount { get; }

        public long FIleCount { get; }

        public long Length { get; }

        public long Quota { get; }

        public long SpaceConsumed { get; }

        public long SpaceQuota { get; }
    }
}
