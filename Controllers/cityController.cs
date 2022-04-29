using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using createform.Models;
using OfficeOpenXml;
using Microsoft.Reporting.WebForms;

namespace createform.Controllers
{
    public class cityController : Controller
    {
        testEntities2 ts = new testEntities2();
        public ActionResult Index()
        {

            var db = ts.city2.ToList();
            List<city> li = new List<city>();
            foreach (var i in db)
            {
                city u = new city();
                u.cityid = i.cityid;
                u.cityname = i.cityname;
                u.statename = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                li.Add(u);
            }
            return View(li);
        }

        public ActionResult citypartial()
        {
            var db = ts.city2.ToList();
            List<city> li = new List<city>();
            foreach (var i in db)
            {
                city u = new city();
                u.cityid = i.cityid;
                u.cityname = i.cityname;
                u.statename = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                li.Add(u);
            }
            return View(li);
        }



        //rdlc


        //For PDF
        public ActionResult Reports(string ReportType)
        {
            var db = ts.city2.ToList();
            //LocalReport localreport = new LocalReport();
            ReportViewer rv = new ReportViewer();

            //localreport.ReportPath = Server.MapPath("/Reports/Report2.rdlc");
            rv.LocalReport.ReportPath = Server.MapPath("/Reports/cityreport.rdlc");

            //DataSet  
            //testDataSet frndDataSet = new testDataSet();


            // Create Report DataSource  
            ReportDataSource reportdatasource = new ReportDataSource();
            reportdatasource.Name = "cityDataSet2";

            List<city> li = new List<city>();
            foreach (var i in db)
            {
                city u = new city();
                u.cityid = i.cityid;
                u.cityname = i.cityname;
                u.statename = ts.state1.Where(x => x.stateid == i.stateid).SingleOrDefault().statename;
                li.Add(u);
            }
            reportdatasource.Value = li;
            rv.LocalReport.DataSources.Add(reportdatasource);
            //rv.LocalReport.Refresh();
            //rv.DataBind();
            // Variables  
            Warning[] warnings = null;
            string reportType = ReportType;
            string mimeTime = null;
            string encoding = null;
            string extension = null;
            if (reportType == "Excel")
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
            var EmpList = new List<city2>();
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
                    // (var package = new ExcelPackage(file.InputStream,"password"))
                    {

                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.FirstOrDefault();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new city2();
                            //user.cityid = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            user.cityname=workSheet.Cells[rowIterator, 3].Value.ToString();
                            user.stateid = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                            EmpList.Add(user);
                        }
                    }
                }
            }
            using (testEntities2 ts = new testEntities2())
            {
                foreach (var item in EmpList)
                {
                    ts.city2.AddObject(item);
                }
                ts.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(int cityid)
        {
            var t = ts.city2.Where(x => x.cityid == cityid).SingleOrDefault();
            ts.DeleteObject(t);
            ts.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

        


        
       
        
        
