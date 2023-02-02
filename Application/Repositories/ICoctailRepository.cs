using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICoctailRepository
    {
        Task<IEnumerable<CoctailEntity>> GetCoctailsAsync();

        Task<CoctailEntity> AddAsync(CoctailEntity coctail);

        Task DeleteAsync(int id);

        Task EditAsync(CoctailEntity coctail);

        Task<CommentEntity> AddCommentAsync(CommentEntity comment);

        Task EditCommentAsync(CommentEntity comment);

        Task DeleteCommentAsync(int id);

        Task<IEnumerable<CommentEntity>> GetComments();
    }
}
