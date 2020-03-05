namespace WebHDFS.Kitty.DataModels.Responses
{
   public sealed class RenameResponse : BoolResponse
    {
        public RenameResponse(bool boolean) : base(boolean)
        {
            Boolean = boolean;
        }
    }
}
