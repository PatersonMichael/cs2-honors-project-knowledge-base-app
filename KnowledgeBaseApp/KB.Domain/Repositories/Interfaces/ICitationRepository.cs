using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain.Repositories.Interfaces
{
    public interface ICitationRepository
    {
        // GET
        public Task<IEnumerable<Citation>> GetCitationsAsync();

        public Task<IEnumerable<Citation>> GetUserCitationsAsync(int userProfileId);

        public Task<Citation> GetCitationAsync(int id);
        public Task<Citation> GetUserCitationAsync(int citationId, int userProfileId);
        // POST

        public Task<Citation> PostCitationAsync(Citation citation);

        // PUT

        public Task<Citation> PutCitationAsync(int id, Citation citation);

        // DELETE

        public Task DeleteCitationAsync(int id);
    }
}
