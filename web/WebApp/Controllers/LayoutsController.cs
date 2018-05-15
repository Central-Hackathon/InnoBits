using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    [Authorize]
    public class LayoutsController : Controller
    {
        private hacaEntities db = new hacaEntities();

        // GET: Layouts
        public ActionResult Index()
        {
            return View(db.DevLayout.ToList());
        }

        // GET: Layouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(devLayout);
        }

        // GET: Layouts/Create
        public ActionResult Create()
        {
            ViewLayout ViewdevLayout = new ViewLayout();
            ViewdevLayout.inputCount = 0;
            ViewdevLayout.outputCount = 0;
            ViewdevLayout.virtualCount = 0;
            ViewdevLayout.result = "";
            return View(ViewdevLayout);
        }

        // POST: Layouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewLayout ViewdevLayout)
        {
            if (ModelState.IsValid)
            {
                ViewdevLayout.Devlayout.dvLtVersion = "1";
                ViewdevLayout.Devlayout.dvLtLastUpdate = DateTime.Now.ToString();

                string[] sett = ViewdevLayout.result.Split('#');
                db.DevLayout.Add(ViewdevLayout.Devlayout);
                db.SaveChanges();
                foreach (string s in sett)
                {
                    if (s != "")
                    {
                        LayoutSettings layouteset = new LayoutSettings();
                        string[] data = s.Split(';');
                        foreach (string v in data)
                        {
                            Guid guid = Guid.NewGuid();
                            string[] val = v.Split(':');
                            if (val[0] == "type")
                                layouteset.ltSType = val[1];
                            else if (val[0] == "left")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "top")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "width")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "height")
                                layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            else if (val[0] == "dbid")
                                layouteset.ItSdbid = guid.ToString();
                        }
                        layouteset.ltSDevLayoutId = ViewdevLayout.Devlayout.dvLtAutoId;
                        db.LayoutSettings.Add(layouteset);
                        db.SaveChanges();
                    }
                }
                string subPath = ViewdevLayout.Devlayout.dvLtAutoId.ToString();
                System.IO.Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));
                return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });

                //if (ViewdevLayout.Devlayout.dvLtType == 1)
                //{
                    
                //    db.DevLayout.Add(ViewdevLayout.Devlayout);
                //    db.SaveChanges();
                //    if (ViewdevLayout.result != null)
                //    {
                //        string[] sett = ViewdevLayout.result.Split('#');
                //        foreach (string s in sett)
                //        {
                //            if (s != "")
                //            {
                //                LayoutSettings layouteset = new LayoutSettings();
                //                string[] data = s.Split(';');
                //                foreach (string v in data)
                //                {
                //                    Guid guid = Guid.NewGuid();
                //                    string[] val = v.Split(':');
                //                    if (val[0] == "type")
                //                        layouteset.ltSType = val[1];
                //                    else if (val[0] == "left")
                //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                    else if (val[0] == "top")
                //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                    else if (val[0] == "width")
                //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                    else if (val[0] == "height")
                //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                    else if (val[0] == "dbid")
                //                        layouteset.ItSdbid = guid.ToString();
                //                }
                //                layouteset.ltSDevLayoutId = ViewdevLayout.Devlayout.dvLtAutoId;
                //                db.LayoutSettings.Add(layouteset);
                //                db.SaveChanges();
                //            }
                //        }
                //        string subPath = ViewdevLayout.Devlayout.dvLtAutoId.ToString();
                //        System.IO.Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));
                //    }

                //    DeviceIO addnew;
                //    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "1",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "bit"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "2",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "bit"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "3",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "string"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    db.SaveChanges();
                //    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                //}
                //else if (ViewdevLayout.Devlayout.dvLtType == 2)
                //{
                //    string[] sett = ViewdevLayout.result.Split('#');
                //    db.DevLayout.Add(ViewdevLayout.Devlayout);
                //    db.SaveChanges();
                //    foreach (string s in sett)
                //    {
                //        if (s != "")
                //        {
                //            LayoutSettings layouteset = new LayoutSettings();
                //            string[] data = s.Split(';');
                //            foreach (string v in data)
                //            {
                //                Guid guid = Guid.NewGuid();
                //                string[] val = v.Split(':');
                //                if (val[0] == "type")
                //                    layouteset.ltSType = val[1];
                //                else if (val[0] == "left")
                //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                else if (val[0] == "top")
                //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                else if (val[0] == "width")
                //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                else if (val[0] == "height")
                //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                //                else if (val[0] == "dbid")
                //                    layouteset.ItSdbid = guid.ToString();
                //            }
                //            layouteset.ltSDevLayoutId = ViewdevLayout.Devlayout.dvLtAutoId;
                //            db.LayoutSettings.Add(layouteset);
                //            db.SaveChanges();
                //        }
                //    }
                //    string subPath = ViewdevLayout.Devlayout.dvLtAutoId.ToString();
                //    System.IO.Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));                    
                //    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                //}
                //else if (ViewdevLayout.Devlayout.dvLtType == 3)
                //{
                //    db.DevLayout.Add(ViewdevLayout.Devlayout);
                //    db.SaveChanges();
                //    DeviceIO addnew;
                //    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "1",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "bit"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "2",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "bit"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                //    {
                //        addnew = new DeviceIO
                //        {
                //            ioType = "3",
                //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                //            ioValType = "string"
                //        };
                //        db.DeviceIO.Add(addnew);
                //    }
                //    db.SaveChanges();
                //    return RedirectToAction("Settings", new { id = ViewdevLayout.Devlayout.dvLtAutoId });
                //}
            }            
            return View(ViewdevLayout);
        }

        public ActionResult Settings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<LayoutSettings> devLayout = db.LayoutSettings.Where(ls => ls.ltSDevLayoutId == id).ToList();
            ViewLayoutSettings viewlayout = new ViewLayoutSettings
            {
                idlu = id.Value,
                LayoutSettingsList = devLayout
            };
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(viewlayout);
        }

        [HttpPost]
        public ActionResult Upload(int? id)
        {
            string fileName = "error";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                fileName = Path.GetFileName(file.FileName);
                string path2 = Server.MapPath("~");
                var path = Path.Combine(Server.MapPath("~/files/" + id + "/"), fileName);
                
                
                file.SaveAs(path);
            }
            return Json(new { success = true, responseText = fileName }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Settings(int id, ViewLayoutSettings LayoutSetting)
        {
            DevLayout getDevlayout = db.DevLayout.Find(id);
            getDevlayout.dvLtVersion = (Convert.ToInt32(getDevlayout.dvLtVersion) + 1).ToString();
            db.SaveChanges();
            if (LayoutSetting.LayoutSettingsList != null)
            {
                for (int i = 0; i < LayoutSetting.LayoutSettingsList.Count; i++)
                {
                    LayoutSettings data = db.LayoutSettings.Find(LayoutSetting.LayoutSettingsList[i].ltSId);
                    data.ltSContent = LayoutSetting.LayoutSettingsList[i].ltSContent;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Layouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);

            List<LayoutSettings> set = db.LayoutSettings.Where(sl => sl.ltSDevLayoutId == devLayout.dvLtAutoId).ToList();

            string res = "";
            if (set.Count > 0)
            {                
                for (int i = 0; i < set.Count; i++)
                {
                    res += "id:" + (i+2) + ";type:" + set[i].ltSType + ";" + set[i].ItSPosition + "dbid:" + set[i].ItSdbid;
                    //res = res.Remove(res.Length - 1, 1);
                    res += "#";
                }
            }
            ViewLayout veilout = new ViewLayout {
                Devlayout = devLayout,
                result = res
            };
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(veilout);
        }

        // POST: Layouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ViewLayout ViewdevLayout)
        {
            if (id != null)
            {
                List<LayoutSettings> getloutseting = db.LayoutSettings.Where(x => x.ltSDevLayoutId == id.Value).ToList();
                DevLayout getDevlayout = db.DevLayout.Find(id.Value);
                if (getDevlayout != null)
                {
                    /*foreach (var data in getloutseting)
                    {
                        db.LayoutSettings.Attach(data);
                        db.LayoutSettings.Remove(data);
                        db.SaveChanges();
                    }*/
                    getDevlayout.dvLtLastUpdate = DateTime.Now.ToString();
                    db.SaveChanges();

                    string[] sett = ViewdevLayout.result.Split('#');
                    foreach (string s in sett)
                    {
                        if (s != "")
                        {
                            string[] data = s.Split(';');
                            string[] getguid = data[6].Split(':');
                            string guid = getguid[1];
                            LayoutSettings layouteset = db.LayoutSettings.Where(x => x.ItSdbid == guid).SingleOrDefault();
                            if (layouteset == null)
                            {
                                layouteset = new LayoutSettings();
                            }
                            else
                            {
                                layouteset.ItSPosition = "";
                            }
                            if (layouteset == null)
                            {
                                layouteset = new LayoutSettings();
                            }
                            foreach (string v in data)
                            {
                                string[] val = v.Split(':');
                                if (val[0] == "type")
                                    layouteset.ltSType = val[1];
                                else if (val[0] == "left")
                                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                                else if (val[0] == "top")
                                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                                else if (val[0] == "width")
                                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                                else if (val[0] == "height")
                                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                            }
                            layouteset.ltSDevLayoutId = getDevlayout.dvLtAutoId;
                            if (layouteset.ItSdbid == null)
                            {
                                layouteset.ItSdbid = Guid.NewGuid().ToString();
                                db.LayoutSettings.Add(layouteset);
                            }

                            var getset = getloutseting.Where(x => x.ItSdbid == layouteset.ItSdbid).SingleOrDefault();
                            if (getset != null)
                            {
                                getloutseting.Remove(getset);
                            }
                            db.SaveChanges();
                        }
                    }
                    foreach (var data in getloutseting)
                    {
                        db.LayoutSettings.Attach(data);
                        db.LayoutSettings.Remove(data);
                        db.SaveChanges();
                    }
                    string subPath = getDevlayout.dvLtAutoId.ToString();
                    Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));
                    return RedirectToAction("Settings", new { id = getDevlayout.dvLtAutoId });

                    //if (getDevlayout.dvLtType == 1)
                    //{
                    //    if (ViewdevLayout.result != null)
                    //    {
                    //        string[] sett = ViewdevLayout.result.Split('#');
                    //        foreach (string s in sett)
                    //        {
                    //            if (s != "")
                    //            {
                    //                //LayoutSettings layouteset = new LayoutSettings();
                    //                string[] data = s.Split(';');
                    //                string[] getguid = data[6].Split(':');
                    //                string guid = getguid[1];
                    //                LayoutSettings layouteset = db.LayoutSettings.Where(x => x.ItSdbid == guid).SingleOrDefault();
                    //                if (layouteset ==  null)
                    //                {
                    //                    layouteset = new LayoutSettings();
                    //                }
                    //                else
                    //                {
                    //                    layouteset.ItSPosition = "";
                    //                }
                    //                foreach (string v in data)
                    //                {
                    //                    string[] val = v.Split(':');
                    //                    if (val[0] == "type")
                    //                        layouteset.ltSType = val[1];
                    //                    else if (val[0] == "left")
                    //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                    else if (val[0] == "top")
                    //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                    else if (val[0] == "width")
                    //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                    else if (val[0] == "height")
                    //                        layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                }
                    //                layouteset.ltSDevLayoutId = getDevlayout.dvLtAutoId;
                    //                if (layouteset.ItSdbid == null)
                    //                {
                    //                    layouteset.ItSdbid = Guid.NewGuid().ToString();
                    //                    db.LayoutSettings.Add(layouteset);                                        
                    //                }

                    //                var getset = getloutseting.Where(x => x.ItSdbid == layouteset.ItSdbid).SingleOrDefault();
                    //                if (getset != null)
                    //                {
                    //                    getloutseting.Remove(getset);
                    //                }

                    //                db.SaveChanges();
                    //            }
                    //        }
                            

                    //        string subPath = getDevlayout.dvLtAutoId.ToString();
                    //        Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));
                    //    }

                    //    DeviceIO addnew;
                    //    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "1",
                    //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                    //            ioValType = "bit"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "2",
                    //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                    //            ioValType = "bit"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "3",
                    //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                    //            ioValType = "string"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    db.SaveChanges();
                    //    foreach (var data in getloutseting)
                    //    {
                    //        db.LayoutSettings.Attach(data);
                    //        db.LayoutSettings.Remove(data);
                    //        db.SaveChanges();
                    //    }
                    //    return RedirectToAction("Settings", new { id = getDevlayout.dvLtAutoId });
                    //}
                    //else if (getDevlayout.dvLtType == 2)
                    //{
                    //    string[] sett = ViewdevLayout.result.Split('#');
                    //    foreach (string s in sett)
                    //    {
                    //        if (s != "")
                    //        {
                    //            string[] data = s.Split(';');
                    //            string[] getguid = data[6].Split(':');
                    //            string guid = getguid[1];
                    //            LayoutSettings layouteset = db.LayoutSettings.Where(x => x.ItSdbid == guid).SingleOrDefault();
                    //            if (layouteset == null)
                    //            {
                    //                layouteset = new LayoutSettings();
                    //            }
                    //            else
                    //            {
                    //                layouteset.ItSPosition = "";
                    //            }                                
                    //            if (layouteset == null)
                    //            {
                    //                layouteset = new LayoutSettings();
                    //            }
                    //            foreach (string v in data)
                    //            {
                    //                string[] val = v.Split(':');
                    //                if (val[0] == "type")
                    //                    layouteset.ltSType = val[1];
                    //                else if (val[0] == "left")
                    //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                else if (val[0] == "top")
                    //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                else if (val[0] == "width")
                    //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //                else if (val[0] == "height")
                    //                    layouteset.ItSPosition += val[0] + ":" + val[1] + ";";
                    //            }
                    //            layouteset.ltSDevLayoutId = getDevlayout.dvLtAutoId;
                    //            if (layouteset.ItSdbid == null)
                    //            {
                    //                layouteset.ItSdbid = Guid.NewGuid().ToString();
                    //                db.LayoutSettings.Add(layouteset);                                    
                    //            }

                    //            var getset = getloutseting.Where(x => x.ItSdbid == layouteset.ItSdbid).SingleOrDefault();
                    //            if (getset != null)
                    //            {
                    //                getloutseting.Remove(getset);
                    //            }
                    //            db.SaveChanges();
                    //        }
                    //    }
                    //    foreach (var data in getloutseting)
                    //    {
                    //        db.LayoutSettings.Attach(data);
                    //        db.LayoutSettings.Remove(data);
                    //        db.SaveChanges();
                    //    }
                    //    string subPath = getDevlayout.dvLtAutoId.ToString();
                    //    System.IO.Directory.CreateDirectory(Server.MapPath("~/files/" + subPath));
                    //    return RedirectToAction("Settings", new { id = getDevlayout.dvLtAutoId });
                    //}
                    //else if (getDevlayout.dvLtType == 3)
                    //{
                    //    //db.DevLayout.Add(ViewdevLayout.Devlayout);
                    //    //db.SaveChanges();
                    //    DeviceIO addnew;
                    //    for (int i = 0; i < ViewdevLayout.inputCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "1",
                    //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                    //            ioValType = "bit"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    for (int i = 0; i < ViewdevLayout.outputCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "2",
                    //            ioDeviceId = ViewdevLayout.Devlayout.dvLtAutoId,
                    //            ioValType = "bit"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    for (int i = 0; i < ViewdevLayout.virtualCount; i++)
                    //    {
                    //        addnew = new DeviceIO
                    //        {
                    //            ioType = "3",
                    //            ioDeviceId = getDevlayout.dvLtAutoId,
                    //            ioValType = "string"
                    //        };
                    //        db.DeviceIO.Add(addnew);
                    //    }
                    //    db.SaveChanges();
                    //    foreach (var data in getloutseting)
                    //    {
                    //        db.LayoutSettings.Attach(data);
                    //        db.LayoutSettings.Remove(data);
                    //        db.SaveChanges();
                    //    }
                    //    return RedirectToAction("Settings", new { id = getDevlayout.dvLtAutoId });
                    //}
                   
                }
            }
            return View();
            /*if (ModelState.IsValid)
            {
                devLayout.Devlayout.dvLtLastUpdate = DateTime.Now.ToString();
                devLayout.Devlayout.dvLtVersion = (Convert.ToInt32(devLayout.Devlayout.dvLtVersion) + 1).ToString();

                db.Entry(devLayout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(devLayout);*/
        }

        [AllowAnonymous]
        public ActionResult view(string api, string key)
        {

            string html = "";
            string[] posision;
            var getlay = db.Devices.Where(x => x.devApi == api && x.devApiKey == key).SingleOrDefault();
            if(getlay != null)
            {
                var get = getlay.DevLayout;
                var getlayset = get.LayoutSettings;
                foreach(var data in getlayset)
                {
                    if(data.ltSType == "text")
                    {
                        posision = data.ItSPosition.Split(';');
                        html += "<h3 style='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center; color:  white;'>" + data.ltSContent + "</h3>";
                    }
                    if(data.ltSType == "iframe")
                    {
                        posision = data.ItSPosition.Split(';');
                        html += "<div style='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                        html += data.ltSContent;
                        html +=   "</div>";
                    }

                    if (data.ltSType == "browser")
                    {
                        posision = data.ItSPosition.Split(';');
                        html += "<div style='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                        html += data.ltSContent;
                        html += "</div>";
                    }

                    if (data.ltSType == "slider")
                    {
                        //<div id="myCarousel" class="carousel slide" data-ride="carousel" data-interval="5000">
                        posision = data.ItSPosition.Split(';');
                        string[] sliders = data.ltSContent.Split('#');
                        if(sliders.Count() == 1)
                        {
                            string[] images = sliders[0].Split(',');
                            html += "<div id='myCarousel' class='carousel slide' data-ride='carousel' data-interval='5000' style ='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                            html += "<div class='carousel-inner' id='down' style ='width:100%;'>";
                            if (images.Length > 0)
                            {
                                html += "<div class='item active'>";
                                html += images[0];
                                html += "</div>";

                                for (int i = 1; i < images.Length; i++)
                                {
                                    html += "<div class='item'>";
                                    html += images[i];
                                    html += "</div>";
                                }
                            }
                            html += "</div>";
                            html += "</div>";
                        }
                        else if(sliders.Count() == 2)
                        {
                            string[] images = sliders[0].Split(',');
                            html += "<div id='myCarousel' class='carousel slide' data-ride='carousel' data-interval='5000' style ='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                            html += "<div class='carousel-inner' id='down' style ='width:100%;'>";
                            if (images.Length > 0)
                            {
                                html += "<div class='item active'>";
                                html += images[0];
                                html += "</div>";

                                for (int i = 1; i < images.Length; i++)
                                {
                                    html += "<div class='item'>";
                                    html += images[i];
                                    html += "</div>";
                                }
                            }
                            html += "</div>";
                            html += "</div>";
                            images = sliders[1].Split(',');
                            html += "<div id='myCarousel' class='carousel slide' data-ride='carousel' data-interval='5000' style ='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                            html += "<div class='carousel-inner' id='up' style ='width:100%;display:none;'>";
                            if (images.Length > 0)
                            {
                                html += "<div class='item active'>";
                                html += images[0];
                                html += "</div>";

                                for (int i = 1; i < images.Length; i++)
                                {
                                    html += "<div class='item'>";
                                    html += images[i];
                                    html += "</div>";
                                }
                            }
                            html += "</div>";
                            html += "</div>";
                        }
                    }

                    if (data.ltSType == "image")
                    {
                        //<div id="myCarousel" class="carousel slide" data-ride="carousel" data-interval="5000">
                        posision = data.ItSPosition.Split(';');
                        string[] images = data.ltSContent.Split(',');
                        html += "<div style ='position: absolute;" + posision[0] + "px;" + posision[1] + "px;" + posision[2] + "px;" + posision[3] + "px; line-" + posision[3] + "px;text-align: center;'>";
                        html += data.ltSContent;
                        html += "</div>";
                    }
                }
            }



            ViewBag.HtmlStr = html;

            return View(getlay);
        }

        [AllowAnonymous]
        public string gettemp(string devid)
        {
            var getlay = db.Devices.Find(Convert.ToInt32(devid));
            string temp = getlay.DeviceGIO.Where(x => x.ioName == "temperature").SingleOrDefault().ioValue;
            return temp;
        }

        // GET: Layouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            if (devLayout == null)
            {
                return HttpNotFound();
            }
            return View(devLayout);
        }

        // POST: Layouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<LayoutSettings> getloutseting = db.LayoutSettings.Where(x => x.ltSDevLayoutId == id).ToList();
            if (getloutseting.Count > 0)
            {
                foreach (var data in getloutseting)
                {
                    db.LayoutSettings.Remove(data);
                    db.SaveChanges();
                }
            }
            DevLayout devLayout = db.DevLayout.Find(id);
            db.DevLayout.Remove(devLayout);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
