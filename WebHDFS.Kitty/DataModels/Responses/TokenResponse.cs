namespace WebHDFS.Kitty.DataModels.Responses
{
    class TokenResponse
    {
        public TokenResponse(Token token)
        {
            Token = token;
        }

        public Token Token { get; }
    }
}
