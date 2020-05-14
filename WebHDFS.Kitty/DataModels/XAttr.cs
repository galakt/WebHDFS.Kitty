namespace WebHDFS.Kitty.DataModels
{
    public class XAttr
    {
        public XAttr(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}
