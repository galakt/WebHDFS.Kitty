namespace WebHDFS.Kitty.DataModels.Responses
{
    public sealed class CreateSnapshotResponse : BoolResponse
    {
        public CreateSnapshotResponse(bool boolean) : base(boolean)
        {
            Boolean = boolean;
        }
    }
}
