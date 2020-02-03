using System;

namespace WebHDFS.Kitty.XUnitTests
{
    public static class DataTestUtility
    {
        public static readonly string HdfsConnStr = null;
        public static readonly string HdfsRootDir = null;

        static DataTestUtility()
        {
            HdfsConnStr = Environment.GetEnvironmentVariable("KITTY_TEST_CONN_STR");
            HdfsRootDir = Environment.GetEnvironmentVariable("KITTY_TEST_ROOT_DIR");
        }
    }
}
