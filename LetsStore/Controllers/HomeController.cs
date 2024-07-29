using AutoMapper;
using LetsStore.DTO;
using LetsStore.Models;
using LetsStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LetsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly LetsStoreContext _context;
        private readonly IMapper _mapper;

        public HomeController(LetsStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            var UserID = Guid.Parse(User.Identity.Name);

            var storages = _context.Storages
                .Join(_context.CategoryMaps, s => s.StorageId, cm => cm.StorageId, (s, cm) => new { s, cm })
                .Join(_context.FileCategories, scm => scm.cm.CatId, fc => fc.CatId, (scm, fc) => new { scm.s, scm.cm, fc })
                .Join(_context.StorageMaps, scmf => scmf.s.StorageId, sm => sm.StorageId, (scmf, sm) => new { scmf.s, scmf.cm, scmf.fc, sm })
                .Where(joined => joined.sm.UserId == UserID)
                .Select(joined => new
                {
                    Storage = joined.s,
                    CategoryMap = joined.cm,
                    FileCategory = joined.fc,
                    StorageMap = joined.sm
                })
                .ToList();

            var vm = storages.Select(result => new AllFilesViewModel
            {
                StorageDTO = _mapper.Map<StorageDTO>(result.Storage),
                CategoryMapDTO = new List<CategoryMapDTO> { _mapper.Map<CategoryMapDTO>(result.CategoryMap) },
                FileCategoryDTO = new List<FileCategoryDTO> { _mapper.Map<FileCategoryDTO>(result.FileCategory) },
                StorageMapDTO = new List<StorageMapDTO>
                {
                    new StorageMapDTO
                    {
                        StorageId = result.StorageMap.StorageId,
                        UserId = result.StorageMap.UserId,
                        UserDTO = _mapper.Map<UserDTO>(_context.Users.Find(result.StorageMap.UserId))
                    }
                }
            }).ToList();

            return View(vm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
