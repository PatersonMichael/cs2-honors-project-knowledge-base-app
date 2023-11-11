using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain.Repositories.Interfaces
{
    public interface IKeywordRepository
    {
        // GET
        public Task<IEnumerable<Keyword>> GetKeywordsAsync();

        public Task<Keyword> GetKeywordAsync(int id);
        // POST

        public Task<Keyword> PostKeywordAsync(Keyword keyword);

        // PUT
        public Task<Keyword> PutKeywordAsync(int id, Keyword keyword);

        // DELETE
        public Task DeleteKeywordAsync(int id);
    }
}
