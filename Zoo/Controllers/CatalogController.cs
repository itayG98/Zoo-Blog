using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace Zoo.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AnimelRepository _animelRepository;
        public CatalogController(AnimelRepository animelRepository)
        {
            _animelRepository = animelRepository;
        }
        public IActionResult Index()
        {
            return View(_animelRepository.FindAllWithComments().ToList());
        }
    }
}
