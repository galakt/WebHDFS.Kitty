namespace WebHDFS.Kitty.DataModels.Responses
{
    public sealed class MakeDirectoryResponse : BoolResponse
    {
        public MakeDirectoryResponse(bool boolean) : base(boolean)
        {
            Boolean = boolean;
        }
    }
}
