using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICoctailService
    {
        Task<IEnumerable<CoctailEntity>> GetCoctailsAsync();

        Task Add(string name, string description, Dictionary<int, double> ingridients);

        Task Delete(int id);

        Task<CommentEntity> AddCommentAsync(CommentEntity comment);

        Task EditCommentAsync(CommentEntity comment);

        Task DeleteCommentAsync(int id);

        Task<IEnumerable<CommentEntity>> GetComments();
    }
}
