using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public sealed class CheckConnStrSetupFact : FactAttribute
    {
        public CheckConnStrSetupFact()
        {
            if (DataTestUtility.HdfsConnStr == null)
            {
                Skip = "DataTestUtility.HdfsConnStr = null!";
            }
        }
    }
}
