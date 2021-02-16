using BlogProject12.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Xml;
using BlogProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using ClosedXML.Excel;

namespace BlogProject12.Areas.PdfExcel.Controllers
{
    [Area("PdfExcel")]
    public class PdfConverterController : Controller
    {

        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        public PdfConverterController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetAll();
            return View(users);
        }

        [HttpPost]
        public FileResult ExportPdf(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Users.pdf");
            }
        }

        [HttpPost]
        public FileResult ExportExcel(string GridHtml1)
        {


            using (MemoryStream stream = new MemoryStream())
            {
                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"), new DataColumn("Name"),new DataColumn("Email") });
                var users = _unitOfWork.User.GetAll();
                foreach (var user in users)
                {
                    dt.Rows.Add(user.Id, user.UserName, user.Email);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        wb.SaveAs(stream2);
                        return File(stream2.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");

                    }
                }
            }

            /*  public void Excel()
              {
                  UserModel model = new UserModel();

                   Export export = new Export();
                  export.ToExcel(HttpResponse, model);
              }
      */


        }

        /*
            //helper class
            public class Export
            {
                public void ToExcel(HttpResponse Response, object clientsList)
                {
                    var grid = new System.Web.UI.WebControls.GridView();
                    grid.DataSource = clientsList;
                    grid.DataBind();
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");
                    Response.ContentType = "application/excel";
                    StringWriter sw = new StringWriter();
                    XmlTextWriter htw = new XmlTextWriter(sw);

                    grid.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }*/
    }
}
