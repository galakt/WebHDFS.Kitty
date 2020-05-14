using System;
using System.Collections.Generic;
using System.Text;

namespace WebHDFS.Kitty.DataModels.Responses
{
    public class XAttrsResponse
    {
        public XAttrsResponse(XAttr[] xattrs)
        {
            XAttrs = xattrs;
        }

        public XAttr[] XAttrs;
    }
}
