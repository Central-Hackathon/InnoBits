using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers.api
{
    public class getiovalController : ApiController
    {
        hacaEntities db = new hacaEntities();
        // GET: GetCredits
        public string Getgetioval(int id)
        {
            Devices dev = db.Devices.Find(id);
            string data = "";
            foreach(var devgio in dev.DeviceGIO)
            {
                data += devgio.ioName + "=" + devgio.ioValue + "#";
            }
            //DeviceGIO io = db.DeviceGIO.Find(id);
            return data;
        }
    }
}
