using OnlineActivity.EFCore.Domain;
using OnlineActivity.EFCore.Domain.Models;

namespace OnlineActivity.EFCore.Infra
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineActivityDbContext context) : base(context)
        {

        }
    }
}