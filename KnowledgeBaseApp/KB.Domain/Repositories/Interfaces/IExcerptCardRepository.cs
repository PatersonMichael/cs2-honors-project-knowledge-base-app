using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain.Repositories.Interfaces
{
    public interface IExcerptCardRepository
    {
        // GET
        public Task<IEnumerable<ExcerptCard>> GetExcerptCardsAsync();
        public Task<IEnumerable<ExcerptCard>> GetUserExcerptCardsAsync(int userProfileId);

        public Task<ExcerptCard> GetExcerptCardAsync(int id);
        public Task<ExcerptCard> GetUserExcerptCardAsync(int cardId, int userProfileId);
        // POST
        public Task<ExcerptCard> PostExcerptCardAsync(ExcerptCard card);

        // PUT
        public Task<ExcerptCard> PutExcerptCardAsync(int id, ExcerptCard card);

        // DELETE
        public Task DeleteExcerptCardAsync(int id);
    }
}
