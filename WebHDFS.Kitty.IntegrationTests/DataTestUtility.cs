using System;

namespace WebHDFS.Kitty.IntegrationTests
{
    public static class DataTestUtility
    {
        public static readonly string HdfsConnStr = null;
        public static readonly string HdfsRootDir = null;

        static DataTestUtility()
        {
            HdfsConnStr = "http://slake-master.avp.ru:14000";// Environment.GetEnvironmentVariable("KITTY_TEST_CONN_STR");
            HdfsRootDir = "/user/prk_kolesnikov_v";// Environment.GetEnvironmentVariable("KITTY_TEST_ROOT_DIR");
        }
    }
}