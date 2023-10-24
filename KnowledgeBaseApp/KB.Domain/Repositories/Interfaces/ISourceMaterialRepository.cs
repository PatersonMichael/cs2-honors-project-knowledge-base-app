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

        Task<SourceMaterial> GetSourceMaterialAsync(int id);

        // POST
        Task<SourceMaterial> PostSourceMaterialAsync(SourceMaterial sourceMaterial);

        // PUT
        Task<SourceMaterial> PutSourceMaterialAsync(int id, SourceMaterial sourceMaterial);

        // DELETE
        Task DeleteSourceMaterialAsync(int id);
    }
}
