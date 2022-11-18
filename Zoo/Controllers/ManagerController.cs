using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.Models;
using Model.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zoo.Controllers
{
    public class ManagerController : Controller
    {
        private readonly AnimelRepository _animelRepository;
        private readonly CategoryRepository _categoryRepository;

        public ManagerController(AnimelRepository animelRepository, CategoryRepository categoryRepository)
        {
            _animelRepository = animelRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Animel> animels = _animelRepository.FindAll().ToList();
            animels.ForEach(a => a.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(a.CategoryID));
            return View(animels);
        }

        [HttpGet]
        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animel animel)
        {
            if (ModelState.IsValid)
            {
                animel.CategoryID = _categoryRepository.MatchCategoryEnumToGuidId(animel.CategoryEnum);
                if (animel.ImageFile != default)
                    animel.ImageRawData = ImagesFormater.IFormFileToByteArray(animel.ImageFile);
                else
                    animel.ImageRawData = Animel.DeafualtRawData;
                _animelRepository.Create(animel);
                return Redirect("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            Animel toUpdate = _animelRepository.Find(id);
            toUpdate.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(toUpdate.CategoryID);
            if (toUpdate != null)
                return View(toUpdate);
            return Redirect("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Animel animel)
        {
            if (ModelState.IsValid)
            {
                animel.CategoryID = _categoryRepository.MatchCategoryEnumToGuidId(animel.CategoryEnum);
                if (animel.ImageFile != default)
                    animel.ImageRawData = ImagesFormater.IFormFileToByteArray(animel.ImageFile);
                else
                    animel.ImageRawData = _animelRepository.Find(animel.ID).ImageRawData;
                _animelRepository.Update(animel);
                return Redirect("Index");
            }
            else if (!ModelState.IsValid && ModelState.ErrorCount == 1 && ModelState.GetFieldValidationState("ImageFile") == ModelValidationState.Invalid)
            {
                animel.ImageRawData = _animelRepository.Find(animel.ID).ImageRawData;
                animel.CategoryID = _categoryRepository.MatchCategoryEnumToGuidId(animel.CategoryEnum);
                _animelRepository.Update(animel);
                return Redirect("Index");
            }
            else if (!ModelState.IsValid)
            {
                animel.ImageRawData = _animelRepository.Find(animel.ID).ImageRawData;
                animel.CategoryID = _categoryRepository.MatchCategoryEnumToGuidId(animel.CategoryEnum);
            }
            return View(animel);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Animel ToDelete = _animelRepository.Find(id);
            if (ToDelete != null)
                return View(ToDelete);
            return Redirect("Manager");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Animel animel)
        {
            _animelRepository.Delete(animel.ID);
            return Redirect("Index");
        }

    }
}
