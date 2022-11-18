using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using System.Linq.Expressions;

namespace Zoo.Views.ViewComponents
{
    public class AnimelsViewComponent : ViewComponent
    {
        private readonly AnimelRepository _animelRepository;
        private readonly CategoryRepository _categoryRepository;

        public AnimelsViewComponent(AnimelRepository animelRepository,CategoryRepository categoryRepository)
        {
            _animelRepository = animelRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Animel> TopTrendingAnimels(int count)
            => _animelRepository.FindTopTrending(count);

        public IViewComponentResult Invoke(ICollection<Animel> animels)
        {
            foreach (Animel animel in animels)
            {
                animel.CategoryEnum = _categoryRepository.MatchGuidIdToCategoryEnum(animel.CategoryID);
            }
            return View(animels);
        }
    }
}
