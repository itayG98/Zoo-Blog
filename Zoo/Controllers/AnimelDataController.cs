using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using System.ComponentModel;

namespace Zoo.Controllers
{
    public class AnimelDataController : Controller
    {
        private readonly AnimelRepository _animelRepository;
        private readonly CommentRepository _commentRepository;
        private readonly CategoryRepository _categoryRepository;

        public AnimelDataController(AnimelRepository animelRepository, CommentRepository commentRepository,CategoryRepository categoryRepository)
        {
            _animelRepository = animelRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index(Guid id)
        {
            Animel animel = _animelRepository.FindWithComments(id);
            if (animel != null)
            {
                animel.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(animel.CategoryID);
                ViewBag.Animel = animel;
                return View(new Comment() { CommentId = Guid.NewGuid() });
            }
            return Redirect("Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Comment comment)
        {
            _commentRepository.Create(comment);
            return Index(comment.AnimelID);
        }
    }
}
