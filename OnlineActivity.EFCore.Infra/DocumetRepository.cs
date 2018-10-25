using OnlineActivity.EFCore.Domain;
using OnlineActivity.EFCore.Domain.Models;

namespace OnlineActivity.EFCore.Infra
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(OnlineActivityDbContext context) : base(context)
        {

        }
    }
}