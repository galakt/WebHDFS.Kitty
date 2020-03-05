namespace WebHDFS.Kitty.DataModels.Responses
{
    class RenewDelegationTokenResponse
    {
        public RenewDelegationTokenResponse(long longvalue)
        {
            Long = longvalue;
        }
        public long Long { get; }
    }
}
