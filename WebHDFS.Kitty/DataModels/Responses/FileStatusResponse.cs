namespace WebHDFS.Kitty.DataModels.Responses
{
    public sealed class FileStatusResponse
    {
        public FileStatusResponse(FileStatus fileStatus)
        {
            FileStatus = fileStatus;
        }

        public FileStatus FileStatus { get; }
    }
}
