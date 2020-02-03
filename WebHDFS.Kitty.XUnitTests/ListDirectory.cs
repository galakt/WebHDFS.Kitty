using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.XUnitTests
{
    public class ListDirectory
    {
        private IWebHdfsClient client;

        [DataTestUtilityControl]
        public async Task ListDir()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.ListStatus(DataTestUtility.HdfsRootDir);
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
