using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace Zoo.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AnimelRepository _animelRepository;
        private List<Animel> animels;
        public CatalogController(AnimelRepository animelRepository)
        {
            _animelRepository = animelRepository;

            Animels = _animelRepository.FindAllWithComments().ToList();
        }

        [BindProperty]
        public List<Animel> Animels { get => animels; set => animels = value; }


        public IActionResult Index()
        {
            return View(Animels);
        }
    }
}
