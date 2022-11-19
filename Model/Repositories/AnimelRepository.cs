using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class AnimelRepository : ZooRepository<Animel>
    {
        public AnimelRepository(ZooDBContext dbContext) : base(dbContext)
        {
        }

        public  List<Animel> FindTopTrending(int count)
            => FindAllWithComments().OrderByDescending(c => c.Comments?.Count).Take(count).ToList();

        public IEnumerable<Animel> FindAllWithComments()
            => DbContext.Set<Animel>()!.Include(a => a.Comments).AsEnumerable();

        public Animel FindWithComments(Guid id)
         => DbContext.Set<Animel>().Include(a => a.Comments).FirstOrDefault(a => a.ID == id);
    }
}
