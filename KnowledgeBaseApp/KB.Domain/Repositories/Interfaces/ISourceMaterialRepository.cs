using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain.Repositories.Interfaces
{
    public interface ISourceMaterialRepository
    {
        // GET
        Task<IEnumerable<SourceMaterial>> GetSourceMaterialsAsync();
        Task<IEnumerable<SourceMaterial>> GetUserSourceMaterialsAsync(int userProfileId);

        Task<SourceMaterial> GetSourceMaterialAsync(int id);
        Task<SourceMaterial> GetUserSourceMaterialAsync(int sourceId, int userProfileId);

        // POST
        Task<SourceMaterial> PostSourceMaterialAsync(SourceMaterial sourceMaterial);

        // PUT
        Task<SourceMaterial> PutSourceMaterialAsync(int id, SourceMaterial sourceMaterial);

        // DELETE
        Task DeleteSourceMaterialAsync(int id);
    }
}
