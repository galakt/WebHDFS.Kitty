namespace WebHDFS.Kitty.DataModels.Responses
{
    public sealed class MakeDirectoryResponse
    {
        public MakeDirectoryResponse(bool boolean)
        {
            Boolean = boolean;
        }
        public bool Boolean { get; }
    }
}
