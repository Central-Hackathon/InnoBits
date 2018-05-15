using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers.api
{
    public class AlertsController : ApiController
    {
        hacaEntities db = new hacaEntities();
        // GET: GetCredits
        public string GetAlerts(int id, int devid)
        {
            List<Alerts> allerts = db.Alerts.Where(x => x.DevLayoutId == id).ToList();

            List<DeviceGIO> devIO = db.DeviceGIO.Where(x => x.ioDeviceId == devid && x.ioType == 1 && x.ioName == "proximity").ToList();
            Devices getdev;
            if (devIO.Count > 0)
            {
                if (devIO[0].ioValue == "1" && id != 3)
                {
                    getdev = db.Devices.Find(devid);
                    getdev.devLayoutIOId = id;
                    getdev.devLayoutId = 3;
                    db.SaveChanges();
                }
            }

            getdev = db.Devices.Find(devid);
            if(getdev.devLayoutId == 3)
            {
                devIO = db.DeviceGIO.Where(x => x.ioDeviceId == devid && x.ioType == 1 && x.ioName == "proximity").ToList();
                if (devIO.Count > 0)
                {
                    if (devIO[0].ioValue == "0")
                    {
                        if (getdev.devLayoutIOId != null)
                        {
                            getdev.devLayoutId = getdev.devLayoutIOId;
                            getdev.devLayoutIOId = null;
                            db.SaveChanges();
                        }
                    }
                }
            }
            string returndata = "";

            for(int i = 0; i< allerts.Count;i++)
            {
                if((i+1) == allerts.Count)
                {
                    returndata += "<span style='color: #" + allerts[i].color + "; font-size: " + allerts[i].size + "px;'>" + allerts[i].Message + "</span>";
                }
                else
                {
                    returndata += "<span style='color: #" + allerts[i].color + "; font-size: " + allerts[i].size + "px;'>" + allerts[i].Message + "</span>  ---  ";
                }
            }

            return returndata;

        }

    }
}
