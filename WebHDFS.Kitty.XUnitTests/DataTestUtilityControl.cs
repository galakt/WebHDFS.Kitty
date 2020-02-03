using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebHDFS.Kitty.XUnitTests
{
    public sealed class DataTestUtilityControl : FactAttribute
    {
       public DataTestUtilityControl()
       {
            if (DataTestUtility.HdfsConnStr == null)
            {
                Skip = "DataTestUtility.HdfsConnStr = null!";   
            }
       }
    }
}
