namespace WebHDFS.Kitty.DataModels.Responses
{
    public sealed class SetReplicationResponse : BoolResponse
    {
        public SetReplicationResponse(bool boolean) : base(boolean)
        {
            Boolean = boolean;
        }
    }
}
