namespace WebHDFS.Kitty.DataModels.Responses
{
    public class BoolResponse
    {
        public BoolResponse(bool boolean)
        {
            Boolean = boolean;
        }
        public bool Boolean { get; set; }
    }
}
