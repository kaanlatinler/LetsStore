using LetsStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LetsStore.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly LetsStoreContext _context;

        public FileController(IWebHostEnvironment hostEnvironment, LetsStoreContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string userAgent, string language, string screenResolution, string colorDepth, string ipAddress)
        {
            if (files == null || files.Count == 0)
                return Content("Files not selected");

            var rootFolder = Path.Combine(_hostEnvironment.ContentRootPath, "Files");

            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }

            string DeviceInfo = $"{userAgent}, {language}, {screenResolution}, {colorDepth}, {ipAddress}";

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                string targetFolder;
                if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                {
                    targetFolder = Path.Combine(rootFolder, "Videos");

                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    var filePath = Path.Combine(targetFolder, file.FileName);

                    Guid storageID = Guid.NewGuid();

                    var newFile = new Storage
                    {
                        UploadedDevice = DeviceInfo,
                        CreatedDate = DateTime.Now,
                        FilePath = filePath,
                        StorageId = storageID,
                        StoredFileName = file.FileName,
                        StoredFileType = extension,
                    };
                    _context.Storages.Add(newFile);

                    string videos = "7EE054D3-F188-412F-BC5E-6340F67C2337";

                    Guid catID = Guid.Parse(videos);

                    var cm = new CategoryMap
                    {
                        CategoryMapId = Guid.NewGuid(),
                        StorageId = storageID,
                        CatId = catID,
                    };
                    _context.CategoryMaps.Add(cm);

                    var userID = Guid.Parse(User.Identity.Name);

                    var sm = new StorageMap
                    {
                        StorageMapId = Guid.NewGuid(),
                        StorageId = storageID,
                        UserId = userID,
                    };

                    _context.StorageMaps.Add(sm);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.SaveChanges();
                }
                else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                {
                    targetFolder = Path.Combine(rootFolder, "Photos");

                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    var filePath = Path.Combine(targetFolder, file.FileName);

                    Guid storageID = Guid.NewGuid();

                    var newFile = new Storage
                    {
                        UploadedDevice = DeviceInfo,
                        CreatedDate = DateTime.Now,
                        FilePath = filePath,
                        StorageId = storageID,
                        StoredFileName = file.FileName,
                        StoredFileType = extension,
                    };
                    _context.Storages.Add(newFile);

                    string photos = "AA9ED176-4A85-4DBE-B892-6CA29C54353C";

                    Guid catID = Guid.Parse(photos);

                    var cm = new CategoryMap
                    {
                        CategoryMapId = Guid.NewGuid(),
                        StorageId = storageID,
                        CatId = catID,
                    };
                    _context.CategoryMaps.Add(cm);

                    var userID = Guid.Parse(User.Identity.Name);

                    var sm = new StorageMap
                    {
                        StorageMapId = Guid.NewGuid(),
                        StorageId = storageID,
                        UserId = userID,
                    };

                    _context.StorageMaps.Add(sm);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.SaveChanges();
                }
                else
                {
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
