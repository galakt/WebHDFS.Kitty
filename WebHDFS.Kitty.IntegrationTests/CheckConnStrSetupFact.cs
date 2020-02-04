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
            if (string.IsNullOrWhiteSpace(DataTestUtility.HdfsConnStr))
            {
                Skip = "DataTestUtility.HdfsConnStr = null!";
            }
        }
    }
}
