using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Models;
using Model.Repositories;
using NUnit.Framework.Internal;

namespace ModelTests
{
    public class Tests
    {
        private ZooDBContext _context;
        private AnimelRepository? _animelRepository;
        private CommentRepository? _commentRepository;
        private CategoryRepository? _categoryRepository;

        private Animel? animelTest;
        private Animel? Do_Not_Insret_Animel;
        private Category? categoryTest;
        private Category? Do_Not_Insret_Category;
        private Comment? commentTest;

        private string _connString;

        [SetUp]
        public void Setup()
        {
            _connString = "Data Source=MockDB.db;";
            byte[] DeafualtRawData = Animel.DeafualtRawData;
            var optionsBuilder = new DbContextOptionsBuilder<ZooDBContext>();
            optionsBuilder.UseSqlite(_connString);
            _context = new ZooDBContext(optionsBuilder.Options);
            if (_context != default)
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _animelRepository = new AnimelRepository(_context);
                _commentRepository = new CommentRepository(_context);
                _categoryRepository = new CategoryRepository(_context);
                categoryTest = new() { Name = "Test", CategoryID = Guid.NewGuid() };
                animelTest = new() { ID = Guid.NewGuid(), Name = "Test", BirthDate = new DateTime(2002, 6, 12), Description = "", CategoryID = categoryTest.CategoryID, ImageRawData = DeafualtRawData };
                commentTest = new() { CommentId = Guid.NewGuid(), AnimelID = animelTest.ID, Content = "Content" };
                Do_Not_Insret_Animel = new Animel() { ID = Guid.NewGuid(), Name = "Never_Insert" };
                Do_Not_Insret_Category = new Category() { CategoryID = Guid.NewGuid(), Name = "Nevet_Insert" };
            }
            if (_animelRepository is null ||
                _categoryRepository is null ||
                _commentRepository is null)
                throw new Exception("Repositories were not set");
        }

        [Test]
        public void CreatingRepositories()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_animelRepository, Is.Not.Null);
                Assert.That(_commentRepository, Is.Not.Null);
                Assert.That(_categoryRepository, Is.Not.Null);
            });
        }

        [Test]
        public void AddToDB()
        {
            Task<bool> AddCategoty = _categoryRepository!.CreateAsync(categoryTest!);
            AddCategoty.Wait();
            Assert.That(AddCategoty.Result, Is.True);

            Task<bool> AddAnimal = _animelRepository!.CreateAsync(animelTest!);
            AddAnimal.Wait();
            Assert.That(AddAnimal.Result, Is.True);

            Task<bool> AddComment = _commentRepository!.CreateAsync(commentTest!);
            AddComment.Wait();
            Assert.That(AddComment.Result, Is.True);
        }

        [Test]
        public void AddToDBUDuplicate()
        {
            bool AddCategory = _categoryRepository!.Create(categoryTest!);
            Assert.That(AddCategory, Is.True);

            bool AddAnimal = _animelRepository!.Create(animelTest!);
            Assert.That(AddAnimal, Is.True);

            bool AddComment = _commentRepository!.Create(commentTest!);
            Assert.That(AddComment, Is.True);

            AddCategory = _categoryRepository.Create(categoryTest!);
            Assert.That(AddCategory, Is.False);

            AddAnimal = _animelRepository.Create(animelTest!);
            Assert.That(AddAnimal, Is.False);

            AddComment = _commentRepository.Create(commentTest!);
            Assert.That(AddComment, Is.False);
        }

        [Test]
        public void DeleteNotExicst()
        {
            bool DeleteNotExicstAnimel = _animelRepository!.Delete(Do_Not_Insret_Animel!);
            Assert.That(DeleteNotExicstAnimel, Is.False);
        }
        [Test]
        public void AddToDBUDuplicateAsync()
        {

            Task<bool> AddCategory = _categoryRepository!.CreateAsync(categoryTest!);
            AddCategory.Wait();
            Assert.That(AddCategory.Result, Is.True);

            Task<bool> AddAnimal = _animelRepository!.CreateAsync(animelTest!);
            AddAnimal.Wait();
            Assert.That(AddAnimal.Result, Is.True);

            Task<bool> AddComment = _commentRepository!.CreateAsync(commentTest!);
            AddComment.Wait();
            Assert.That(AddComment.Result, Is.True);


            Task<bool> AddCategorySecond = _categoryRepository.CreateAsync(categoryTest!);
            AddCategorySecond.Wait();
            Assert.That(AddCategorySecond.Result, Is.False);

            Task<bool> AddAnimalSecond = _animelRepository.CreateAsync(animelTest!);
            AddAnimalSecond.Wait();
            Assert.That(AddAnimalSecond.Result, Is.False);

            Task<bool> AddCommentSecond = _commentRepository.CreateAsync(commentTest!);
            AddCommentSecond.Wait();
            Assert.That(AddCommentSecond.Result, Is.False);


        }

        [Test]
        public void DeleteInDBUDuplicate()
        {
            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);


            bool DeleteAddComment = _commentRepository.Delete(commentTest!);
            Assert.That(DeleteAddComment, Is.True);

            bool DeleteAddAnimal = _animelRepository.Delete(animelTest!);
            Assert.That(DeleteAddAnimal, Is.True);

            bool DeleteCategory = _categoryRepository.Delete(categoryTest!);
            Assert.That(DeleteCategory, Is.True);

            DeleteAddComment = _commentRepository.Delete(commentTest!);
            Assert.That(DeleteAddComment, Is.False);

            DeleteAddAnimal = _animelRepository.Delete(animelTest!);
            Assert.That(DeleteAddAnimal, Is.False);

            DeleteCategory = _categoryRepository.Delete(categoryTest!);
            Assert.That(DeleteCategory, Is.False);
        }


        [Test]
        public void DeleteAsync()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);


            Task<bool> animelDeleted = _animelRepository!.DeleteAsync(animelTest!);
            animelDeleted.ContinueWith(_ => { Assert.That(animelDeleted.Result, Is.True); });
            Task<bool> Invalid_animel_Deleted = _animelRepository.DeleteAsync(Do_Not_Insret_Animel!);
            Invalid_animel_Deleted.ContinueWith(_ => { Assert.That(Invalid_animel_Deleted.Result, Is.False); });
            Task<bool> categoryDeleted = _categoryRepository!.DeleteAsync(categoryTest!);
            categoryDeleted.ContinueWith(_ => { Assert.That(categoryDeleted.Result, Is.True); });
            Task<bool> commentDeleted = _commentRepository!.DeleteAsync(commentTest!);
            commentDeleted.ContinueWith(_ => { Assert.That(commentDeleted.Result, Is.True); });
        }
        [Test]
        public void Update()
        {
            string test = "Update Test";

            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);

            Do_Not_Insret_Animel!.Name = test;
            animelTest!.Name = test;
            animelTest!.Description = test;
            commentTest!.Content = test;
            categoryTest!.Name = test;

            bool animelUpdated = _animelRepository.Update(animelTest);
            bool Invalid_animel_Updated = _animelRepository.Update(Do_Not_Insret_Animel);
            bool categoryUpdated = _categoryRepository.Update(categoryTest);
            bool commentUpdated = _commentRepository.Update(commentTest);

            Assert.That(animelUpdated, Is.True);
            Assert.That(categoryUpdated, Is.True);
            Assert.That(commentUpdated, Is.True);
            Assert.That(Invalid_animel_Updated, Is.False);

        }

        [Test]
        public void UpdateAsync()
        {
            string test = "Update Test";

            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);

            Do_Not_Insret_Animel!.Name = test;
            animelTest!.Name = test;
            animelTest!.Description = test;
            commentTest!.Content = test;
            categoryTest!.Name = test;

            Task<bool> animelUpdated = _animelRepository.UpdateAsync(animelTest);
            animelUpdated.ContinueWith(_ => { Assert.That(animelUpdated.Result, Is.True); });
            Task<bool> Invalid_animel_Updated = _animelRepository.UpdateAsync(Do_Not_Insret_Animel);
            Invalid_animel_Updated.ContinueWith(_ => { Assert.That(Invalid_animel_Updated.Result, Is.False); });
            Task<bool> categoryUpdated = _categoryRepository.UpdateAsync(categoryTest);
            categoryUpdated.ContinueWith(_ => { Assert.That(categoryUpdated.Result, Is.True); });
            Task<bool> commentUpdated = _commentRepository.UpdateAsync(commentTest);
            commentUpdated.ContinueWith(_ => { Assert.That(commentUpdated.Result, Is.True); });
        }

        [Test]
        public void FindById()
        {
            _categoryRepository!.Create(categoryTest!);
            _animelRepository!.Create(animelTest!);
            _commentRepository!.Create(commentTest!);
            Assert.Multiple(() =>
            {
                Assert.That(_animelRepository.Find(Do_Not_Insret_Animel!.ID), Is.Null);
                Assert.That(_animelRepository.Find(animelTest!.ID), Is.Not.Null);
                Assert.That(_commentRepository.Find(commentTest!.CommentId), Is.Not.Null);
                Assert.That(_categoryRepository.Find(categoryTest!.CategoryID), Is.Not.Null);
            });
        }

        [Test]
        public void FindByIdAsync()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);

            Task<Animel> animelFound = _animelRepository!.FindByIDAsync(animelTest!.ID);
            animelFound.ContinueWith(_ => { Assert.That(animelFound.Result, Is.EqualTo(animelTest)); });
            Task<Animel> animelNotFound = _animelRepository.FindByIDAsync(Do_Not_Insret_Animel!.ID);
            animelNotFound.ContinueWith(_ => { Assert.That(animelFound.Result, Is.Null); });
            Task<Comment> commentFound = _commentRepository!.FindByIDAsync(commentTest!.CommentId);
            commentFound.ContinueWith(_ => { Assert.That(commentFound.Result, Is.EqualTo(commentTest)); });
            Task<Category> categoryFound = _categoryRepository!.FindByIDAsync(categoryTest!.CategoryID);
            categoryFound.ContinueWith(_ => { Assert.That(categoryFound.Result, Is.EqualTo(categoryTest)); });
        }
        [Test]
        public void FindByReference()
        {
            _categoryRepository?.Create(categoryTest!);
            _animelRepository?.Create(animelTest!);
            _commentRepository?.Create(commentTest!);
            Assert.That(_animelRepository?.FindByReference(Do_Not_Insret_Animel!), Is.Null);
            Assert.That(_animelRepository?.FindByReference(animelTest!), Is.Not.Null);
            Assert.That(_commentRepository?.FindByReference(commentTest!), Is.Not.Null);
            Assert.That(_categoryRepository?.FindByReference(categoryTest!), Is.Not.Null);
        }

        [Test]
        public void FindAll()
        {
            int count = _categoryRepository!.FindAll().ToList().Count();
            _categoryRepository.Create(categoryTest!);
            Assert.That(_categoryRepository!.FindAll().ToList().Count(), Is.EqualTo(count + 1));
        }

        [Test]
        public void FindAllAsync()
        {
            Task categoryTask = new(() =>
            {
                Assert.That(_categoryRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> categoryCreation = _categoryRepository.CreateAsync(categoryTest!);
                Task<IEnumerable<Category>> categoryLength = _categoryRepository.FindAllAsync();
                categoryCreation.ContinueWith(_ => categoryLength);
                categoryLength.ContinueWith(_ => Assert.That(categoryLength.Result.ToArray().Length.Equals(3)));
            });
            Task animelTask = new(() =>
            {
                Assert.That(_animelRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> animelCreation = _animelRepository.CreateAsync(animelTest!);
                Task<IEnumerable<Animel>> animelLength = _animelRepository.FindAllAsync();
                animelCreation.ContinueWith(_ => animelLength);
                animelLength.ContinueWith(_ => Assert.That(animelLength.Result.ToArray().Length.Equals(3)));

            });
            Task commentTask = new(() =>
            {
                Assert.That(_commentRepository!.FindAll().ToList().Count(), Is.EqualTo(2));
                Task<bool> commentCreation = _commentRepository.CreateAsync(commentTest!);
                Task<IEnumerable<Comment>> commentLength = _commentRepository.FindAllAsync();
                commentCreation.ContinueWith(_ => commentLength);
                commentLength.ContinueWith(_ => Assert.That(commentLength.Result.ToArray().Length.Equals(3)));

            });
        }

        [Test]
        public void FindByCondition()
        {
            _categoryRepository!.Create(categoryTest!);
            Category categoryFound = _categoryRepository.FindFirstByConditionOrDefault(x => x.Equals(categoryTest));
            Category categoryNotFound = _categoryRepository.FindFirstByConditionOrDefault(x => x.Equals(Do_Not_Insret_Category));
            Assert.That(categoryFound, Is.EqualTo(categoryTest));
            Assert.That(categoryNotFound, Is.EqualTo(null));
        }

        [Test]
        public void FindByConditionAsync()
        {
            _categoryRepository!.Create(categoryTest!);
            Task<Category> taskFound = _categoryRepository.FindFirstByConditionOrDefaultAsync(x => x.Equals(categoryTest));
            Task<Category> taskNotFound = _categoryRepository.FindFirstByConditionOrDefaultAsync(x => x.Equals(Do_Not_Insret_Category));
            taskFound.ContinueWith(_ => { Assert.That(taskFound.Result, Is.EqualTo(categoryTest)); });
            taskNotFound.ContinueWith(_ => { Assert.That(taskNotFound.Result, Is.EqualTo(null)); });
        }
        [Test]
        public void FindAllByCondition()
        {
            _categoryRepository!.Create(categoryTest!);
            IEnumerable<Category> categories = _categoryRepository.FindByCondition(c => c.Name!.Contains("aA"));
            Assert.That(categories.Contains(categoryTest), Is.Not.True);
            Assert.That(categories.All(C => C.Name!.Contains("aA")), Is.True);
        }
        [Test]
        public void FindAllByConditionAsync()
        {
            _categoryRepository!.Create(categoryTest!);
            Task<IEnumerable<Category>> categoriesTask = _categoryRepository.FindByConditionAsync(c => c.Name!.Contains("aA"));
            categoriesTask.ContinueWith(_ =>
            {
                Assert.That(categoriesTask.Result.Contains(categoryTest), Is.Not.True);
                Assert.That(categoriesTask.Result.All(C => C.Name!.Contains("aA")), Is.True);
            });
        }

        [Test]
        public void FindAllWithComments()
        {
            List<Animel> animels = _animelRepository!.FindAllWithComments().ToList();
            Assert.That(animels.All(a => a.Comments != null), Is.True);
        }
        [Test]
        public void FindTopTrending()
        {
            int check = 2;
            List<Animel> animels = _animelRepository!.FindTopTrending(check);
            Assert.That(animels.Count == check, Is.True);
            Assert.That(animels.OrderByDescending(a => a?.Comments?.Count).Take(check).SequenceEqual(animels), Is.True); ;
        }

        [Test]
        public void GetCategoryEnum()
        {
            List<Category> categories = _categoryRepository!.FindAll().Where(c => c.Name != "Test").ToList();
            Guid id = categories.First().CategoryID;
            string name = categories.First().Name;
            CategoriesEnum catEnum = _categoryRepository.MatchGuidIdToCategoryEnum(id);
            Assert.That(catEnum.ToString(), Is.EqualTo(name));
        }
        [Test]
        public void GetCategoryID()
        {
            List<Category> categories = _categoryRepository!.FindAll().Where(c => c.Name != "Test").ToList();
            string name = categories.First().Name;
            Guid id = categories.First().CategoryID;
            CategoriesEnum cat = (CategoriesEnum)Enum.Parse(typeof(CategoriesEnum), name);
            Guid generetedId = _categoryRepository.MatchCategoryEnumToGuidId(cat);
            Assert.That(generetedId == id, Is.True);
        }

        [Test]
        public void FindWithComments()
        {
            _categoryRepository.Create(categoryTest);
            _animelRepository.Create(animelTest);
            _commentRepository.Create(commentTest);
            List<Comment> comments = _commentRepository.FindByCondition(c => c.AnimelID == animelTest.ID).ToList();
            Animel a = _animelRepository.FindWithComments(animelTest.ID);
            Assert.That(comments.SequenceEqual(a.Comments), Is.True);

        }
    }
}
