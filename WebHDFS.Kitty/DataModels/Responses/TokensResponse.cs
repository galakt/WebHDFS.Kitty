namespace WebHDFS.Kitty.DataModels.Responses
{
    class TokensResponse
    {
        public TokensResponse(Token[] tokens)
        {
            Tokens = tokens;
        }

        public Token[] Tokens { get; }
    }
}
