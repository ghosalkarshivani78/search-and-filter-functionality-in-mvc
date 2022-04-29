using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using createform.Models;
using System.Data;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;



namespace createform.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        testEntities2 ts = new testEntities2();

        public ActionResult Index()
        {
            var db = ts.empstates.ToList();
            List<userform> li = new List<userform>();
            foreach(var i in db)
            {
                userform u = new userform();
                u.id = i.id;
                u.firstname = i.firstname;
                u.lastname = i.lastname;
                u.email = i.email;
                u.address = i.address;
                u.countryname = ts.countries.Where(x => x.countryid == i.countryid).SingleOrDefault().countryname;
                u.stateid = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                u.cityid = ts.city2.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                u.number =i.number.ToString();
                li.Add(u);
            }
            return View(li);
        }


      // for serach
        [HttpGet]
        public JsonResult SearchCountry(string countryname)
        {
            testEntities2 ts = new testEntities2();
            List<empstate> customers = null;
            if (countryname == null || countryname == "")
            {
                customers = ts.empstates.ToList();
                //return Json(li, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var C1 = ts.countries.Where(X => X.countryname == countryname).SingleOrDefault();
                 customers = ts.empstates.Where(X => X.countryid == C1.countryid).ToList();
                //var customerlist=customers.ToList();
            }
                List<userform> li = new List<userform>();
                foreach (var i in customers)
                {
                    userform u = new userform();
                    u.id = i.id;
                    u.firstname = i.firstname;
                    u.lastname = i.lastname;
                    u.email = i.email;
                    u.address = i.address;
                    u.countryname = ts.countries.Where(x => x.countryid == i.countryid).SingleOrDefault().countryname;
                    u.stateid = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                    u.cityid = ts.city2.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                    u.number = i.number.ToString();
                    li.Add(u);
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            
        }



        [HttpPost]
        public ActionResult ExportData()
        {
            testEntities2 ts = new testEntities2();
            List<userform> FileData = new List<userform>();
                    var db = ts.empstates.ToList();
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel.Columns.Add("ID", typeof(String));  
                        dtExcel.Columns.Add("First Name", typeof(String));
                        dtExcel.Columns.Add("Last Name", typeof(String));
                        dtExcel.Columns.Add("Email", typeof(String));
                        dtExcel.Columns.Add("Address", typeof(String));
                        dtExcel.Columns.Add("Country", typeof(String));
                        dtExcel.Columns.Add("State", typeof(String));
                        dtExcel.Columns.Add("City", typeof(String));
                        dtExcel.Columns.Add("Number", typeof(String));

                        foreach (var i in db)
                        {
                            DataRow row = dtExcel.NewRow();
                            row[0] = i.id;
                            row[1] = i.firstname;
                            row[2] = i.lastname;
                            row[3] = i.email;
                            row[4] = i.address;
                            row[5] = ts.countries.Where(x => x.countryid == i.countryid).SingleOrDefault().countryname;
                            row[6] = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                            row[7] = ts.city2.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                            row[8] = i.number;
                            dtExcel.Rows.Add(row);
                        }

                        var memoryStream = new MemoryStream();
                        using (var excelPackage = new ExcelPackage(memoryStream))
                        {
                            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                            worksheet.Cells["A1"].LoadFromDataTable(dtExcel, true, TableStyles.None);
                            worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                            worksheet.DefaultRowHeight = 18;

                            worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.DefaultColWidth = 20;
                            worksheet.Column(2).AutoFit();

                            Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }

                    }
                    catch(Exception)
                    {
                        throw ;
                    }

                }

        public ActionResult Download() 
        {
            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "FileManager.xlsx");
            }
            else 
            {
                return new EmptyResult();  
            }
        }

        //For PDF
        public ActionResult Reports(string ReportType) 
        {
            var db = ts.empstates.ToList();
            //LocalReport localreport = new LocalReport();
            ReportViewer rv = new ReportViewer();

            //localreport.ReportPath = Server.MapPath("/Reports/Report2.rdlc");
            rv.LocalReport.ReportPath = Server.MapPath("/Reports/Report1.rdlc");

             //DataSet  m
            testDataSet1 frndDataSet = new testDataSet1();  

  
        // Create Report DataSource  
        ReportDataSource reportdatasource = new ReportDataSource();
        reportdatasource.Name = "DataSet3";
  
        List<userform> li = new List<userform>();
        foreach (var i in db)
        {
            userform u = new userform();
            u.id = i.id;
            u.firstname = i.firstname;
            u.lastname = i.lastname;
            u.email = i.email;
            u.address = i.address;
            u.countryname = ts.countries.Where(x => x.countryid == i.countryid).SingleOrDefault().countryname;
            u.stateid = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
            u.cityid = ts.city2.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
            u.number = i.number.ToString();
            li.Add(u);
        }
        reportdatasource.Value = li;
        rv.LocalReport.DataSources.Add(reportdatasource);
        //rv.LocalReport.Refresh();
        //rv.DataBind();
        // Variables  
        Warning[] warnings = null;
        string reportType = ReportType;
        string mimeTime= null;  
        string encoding= null;
        string extension = null;  
        if(reportType =="Excel")
        {
            extension = "xlsx";
        }
        if (reportType == "Word")
        {
            extension = "docx";
        }
        if (reportType == "PDF")
        {
            extension = "pdf";
        }
        // Setup the report viewer object and get the array of bytes  

        string[] streams = null;
        byte[] renderdByte;

        renderdByte = rv.LocalReport.Render(reportType, "", out mimeTime, out encoding, out extension, out streams, out warnings);
        Response.AddHeader("content-disposition", "Attachment;fileName=studentReport." + extension);
        return File(renderdByte, extension);
        }


        public ActionResult Upload(HttpPostedFileBase UploadedFile) 
        {
            var EmpList = new List<empstate>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (ExcelPackage package = new ExcelPackage(file.InputStream))
                    //using (var package = new ExcelPackage(file.InputStream,"password"))
                    {
                        
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.FirstOrDefault();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 5; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new empstate();
                            //user.id = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            user.firstname = workSheet.Cells[rowIterator, 3].Value.ToString();
                            user.lastname = workSheet.Cells[rowIterator, 4].Value.ToString();
                            user.email = workSheet.Cells[rowIterator, 5].Value.ToString();
                            user.address = workSheet.Cells[rowIterator, 6].Value.ToString();
                            user.countryid = Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value);
                            user.stateid = Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value);
                            user.cityid = Convert.ToInt32(workSheet.Cells[rowIterator, 9].Value);
                            user.number = workSheet.Cells[rowIterator, 10].Value.ToString();
                            EmpList.Add(user);
                        }
                    }
                }
            }
            using (testEntities2 ts = new testEntities2())
            {
                foreach (var item in EmpList)
                {
                    ts.empstates.AddObject(item);
                }
                ts.SaveChanges();
            }
            return View("Index");
        }
            
        public ActionResult empartial() 
        {
            var db = ts.empstates.ToList();
            List<userform> li = new List<userform>();
            foreach (var i in db)
            {
                userform u = new userform();
                u.id = i.id;
                u.firstname = i.firstname;
                u.lastname = i.lastname;
                u.email = i.email;
                u.address = i.address;
                u.countryname = ts.countries.Where(x => x.countryid == i.countryid).SingleOrDefault().countryname;
                u.stateid = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                u.cityid = ts.city2.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                u.number = i.number;
                li.Add(u);
            }
            return View("empartial",li);
        }
        // GET: /Home/Create

        public ActionResult Create()
        {
            List<ddl> dll = new List<Models.ddl>(){
            new ddl{
            id=0,
            name="Please Select"
          
                }
            };
            userform u = new userform();
            var countrylist=ts.countries.ToList();
            u.countries = new SelectList(countrylist, "countryid", "countryname");

            var statelist = ts.state1.ToList();
            u.states = new SelectList(dll, "id", "name");

            var citylist = ts.city2.ToList();
            u.cities = new SelectList(dll, "id", "name");
            return View(u);
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(userform uf)
        {
            userform u = new userform();
            var countrylist = ts.countries.ToList();
            u.countries = new SelectList(countrylist, "countryid", "countryname");

            var statelist = ts.state1.ToList();
            u.states = new SelectList(statelist, "id", "name");

            var citylist = ts.city2.ToList();
            u.cities = new SelectList(citylist, "id", "name");
            try
            {
                var t = ts.empstates.Where(x => x.email == uf.email).SingleOrDefault();
                if (t == null)
                {
                    empstate s = new empstate();
                    s.id = uf.id;
                    s.firstname = uf.firstname;
                    s.lastname = uf.lastname;
                    s.email = uf.email;
                    s.address = uf.address;
                    s.countryid = Convert.ToInt32(uf.countryname);
                    s.stateid = Convert.ToInt32(uf.stateid);
                    s.cityid = Convert.ToInt32(uf.cityid);
                    s.number = uf.number;
                    ts.empstates.AddObject(s);
                    ts.SaveChanges();
                    bool msg = false;
                    return Json(msg);
                }
                else
                {
                    bool msg = true;
                    return Json(msg);
                }
            }
            catch
            {
                bool msg = true;
                return Json(msg);
                //return View(uf);
            }
        }
        
        //
        // GET: /Home/Edit/5
        public ActionResult Edit(int id = 0)
        {

            userform us = new userform();

            var t = ts.empstates.Where(x => x.id == id).SingleOrDefault();
            var countrylist = ts.countries.ToList();
            us.countries = new SelectList(countrylist, "countryid", "countryname");

            var statelist = ts.state1.Where(x=>x.countryid==t.countryid).ToList();
            us.states = new SelectList(statelist, "stateid", "statename");

            var citylist = ts.city2.Where(x=>x.stateid==t.stateid).ToList();
            us.cities = new SelectList(citylist, "cityid", "cityname");

            us.id = t.id;
            us.firstname = t.firstname;
            us.lastname = t.lastname;
            us.email = t.email;
            us.address = t.address;
            us.countryname = t.countryid.ToString();
            us.stateid = t.stateid.ToString();
            us.cityid = t.cityid.ToString();
            us.number =t.number.ToString();
            return View(us);
        }


        //POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(userform uf)
        {
            var countrylist = ts.countries.ToList();
            uf.countries = new SelectList(countrylist, "countryid", "countryname");

            var statelist = ts.state1.ToList();
            uf.states = new SelectList(statelist, "stateid", "statename");

            var citylist = ts.city2.ToList();
            uf.cities = new SelectList(citylist, "cityid", "cityname");
            try
            {
                var t = ts.empstates.Where(x => x.id == uf.id).SingleOrDefault();
                t.id = uf.id;
                t.firstname = uf.firstname;
                t.lastname = uf.lastname;
                t.email = uf.email;
                t.address = uf.address;
                t.countryid = Convert.ToInt32(uf.countryname);
                t.stateid = Convert.ToInt32(uf.stateid);
                t.cityid = Convert.ToInt32(uf.cityid);
                t.number =uf.number;
                ts.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(uf);
            }
        }


        // GET: /Home/Delete/5
        public ActionResult Delete(int id)
        {
            var t = ts.empstates.Where(x => x.id == id).SingleOrDefault();
            ts.DeleteObject(t);
            ts.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Home/Delete/5

        [HttpGet]
        public ActionResult Deletedata(int id)
        {
            try
            {
                var t = ts.empstates.Where(x => x.id == id).SingleOrDefault();
                ts.DeleteObject(t);
                ts.SaveChanges();
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult setemailid(string email) 
        {
            testEntities2 ts = new testEntities2();
            var t = ts.empstates.Where(x => x.email == email).FirstOrDefault();
            if (t != null)
            {
                return Json("1",JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetstateBycountryId(int countryid) 
        {
            List<state1> li = new List<state1>();
            var lblstate = ts.state1.Where(x => x.countryid == countryid).ToList();
            userform f = new userform();
            f.states = new SelectList(lblstate, "stateid", "statename");
            return Json(f.states, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetcityBystateId(int stateid) 
        {
            List<city2> li = new List<city2>();
            var lblcity = ts.city2.Where(x => x.stateid == stateid).ToList();
            userform f = new userform();
            f.cities = new SelectList(lblcity, "cityid", "cityname");
            return Json(f.cities, JsonRequestBehavior.AllowGet);
        }
    }
}



