using Shelterer.Models;
using Shelterer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shelterer.Controllers
{
    public class ImageController : Controller
    {

        //
        // GET: /Image/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Show(int? id)
        {
            string mime;
            byte[] bytes = LoadImage(id.Value, out mime);
            return File(bytes, mime);
        }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            SuccessModel viewModel = new SuccessModel();
            if (Request.Files.Count == 1)
            {
                var name = Request.Files[0].FileName;
                var size = Request.Files[0].ContentLength;
                var type = Request.Files[0].ContentType;
                var shelterId = Convert.ToInt32(Request.Form.Get("id"));
                viewModel.Success = await HandleUpload(Request.Files[0].InputStream, name, size, type, shelterId);
            }
            return Json(viewModel);
        }

        private async Task<bool> HandleUpload(Stream fileStream, string name, int size, string type, int id)
        {
            bool handled = false;

            try
            {
                byte[] documentBytes = new byte[fileStream.Length];
                fileStream.Read(documentBytes, 0, documentBytes.Length);

                Image image = new Image
                {
                    ShelterId = id,
                    CreatedOn = DateTime.Now,
                    FileContent = documentBytes,
                    IsDeleted = false,
                    Name = name,
                    Size = size,
                    Type = type
                };

                using (SheltersContext db = new SheltersContext())
                {
                    db.Images.Add(image);
                    handled = (await db.SaveChangesAsync()> 0);
                }
            }
            catch (Exception ex)
            {
                // Oops, something went wrong, handle the exception
            }

            return handled;
        }

        private byte[] LoadImage(int id, out string type)
        {
            byte[] fileBytes = null;
            string fileType = null;
            using (SheltersContext db = new SheltersContext())
            {
                var image = db.Images.FirstOrDefault(i => i.Id == id);
                if (image != null)
                {
                    fileBytes = image.FileContent;
                    fileType = image.Type;
                }
            }
            type = fileType;
            return fileBytes;
        }

	}
}