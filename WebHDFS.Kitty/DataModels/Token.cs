namespace WebHDFS.Kitty.DataModels
{
    class Token
    {
        public Token(string urlString)
        {
            UrlString = urlString;
        }

        public string UrlString { get; }
    }
}
