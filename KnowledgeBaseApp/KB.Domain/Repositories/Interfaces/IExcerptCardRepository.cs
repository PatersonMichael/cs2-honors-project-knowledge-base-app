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

        public Task<ExcerptCard> GetExcerptCardAsync(int id);
        // POST
        public Task<ExcerptCard> PostExcerptCardAsync(ExcerptCard card);

        // PUT
        public Task<ExcerptCard> PutExcerptCardAsync(int id, ExcerptCard card);

        // DELETE
        public Task DeleteExcerptCardAsync(int id);
    }
}
