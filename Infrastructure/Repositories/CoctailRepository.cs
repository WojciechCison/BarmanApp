using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CoctailRepository : ICoctailRepository
    {
        private readonly CoctailsContext context;

        public CoctailRepository(CoctailsContext context)
        {
            this.context = context;
        }

        public async Task<CoctailEntity> AddAsync(CoctailEntity coctail)
        {
            this.context.Coctails.Add(coctail);
            await this.context.SaveChangesAsync();
            return coctail;
        }

        public async Task DeleteAsync(int id)
        {
            var igridient = this.context.Coctails.FirstOrDefault(i => i.Id == id);

            if (igridient != null)
            {
                this.context.Coctails.Remove(igridient);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task EditAsync(CoctailEntity coctail)
        {
            this.context.Coctails.Update(coctail);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoctailEntity>> GetCoctailsAsync()
        {
            var coctails = await this.context.Coctails.ToListAsync();

            foreach (var item in coctails)
            {
                item.CoctailIngridients = await this.context.CoctailIngridients.Where(x => x.CoctailId == item.Id).ToListAsync();
            }

            return coctails;
        }

        public async Task<IEnumerable<CommentEntity>> GetComments()
        {
            return await this.context.Comments.ToListAsync();
        }

        public async Task<CommentEntity> AddCommentAsync(CommentEntity comment)
        {
            this.context.Comments.Add(comment);
            await this.context.SaveChangesAsync();
            return comment;
        }

        public async Task EditCommentAsync(CommentEntity comment)
        {
            this.context.Comments.Update(comment);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comments = this.context.Comments.FirstOrDefault(i => i.Id == id);

            if (comments != null)
            {
                this.context.Comments.Remove(comments);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
