using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers.api
{
    public class LayouteVerController : ApiController
    {
        hacaEntities db = new hacaEntities();
        // GET: GetCredits
        public string GetLayouteVer(int id)
        {
            Devices dev = db.Devices.Find(id);

            //DevLayout Dlout = db.DevLayout.Find(id);
            return dev.devLayoutId.ToString() + "#" + dev.DevLayout.dvLtVersion;

        }

    }
}
