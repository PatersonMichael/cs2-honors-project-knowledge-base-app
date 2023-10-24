using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Common.Exceptions;
using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace KB.Domain.Repositories
{
    internal class SourceMaterialRepository : ISourceMaterialRepository
    {
        private readonly KnowledgeBaseAppContext _context;
        private readonly ILogger<SourceMaterialRepository> _logger;

        public SourceMaterialRepository(KnowledgeBaseAppContext context, ILogger<SourceMaterialRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET
        public async Task<IEnumerable<SourceMaterial>> GetSourceMaterialsAsync()
        {
            return await _context.SourceMaterials
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SourceMaterial> GetSourceMaterialAsync(int id)
        {
            try
            {
                var sourceMaterial = await _context.SourceMaterials
                    .AsNoTracking()
                    .SingleAsync(x => x.SourceMaterialId == id);

                return sourceMaterial;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"Source Material with id {id} was not found");
            }
        }

        // POST
        public async Task<SourceMaterial> PostSourceMaterialAsync(SourceMaterial sourceMaterial)
        {
            await _context.SourceMaterials.AddAsync(sourceMaterial);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SourceMaterialExists(sourceMaterial.SourceMaterialId))
                {
                    throw new ConflictException("Source Material already exists");
                }

                throw;
            }

            return sourceMaterial;
        }

        // PUT
        public async Task<SourceMaterial> PutSourceMaterialAsync(int id, SourceMaterial sourceMaterial)
        {
            if (id != sourceMaterial.SourceMaterialId)
            {
                throw new BadRequestException($"id {id} is invalid");
            }

            _context.Entry(sourceMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceMaterialExists(sourceMaterial.SourceMaterialId))
                {
                    throw new NotFoundException("Source Material doesn't exist");
                }

                throw;
            }

            return sourceMaterial;
        }

        // DELETE

        public async Task DeleteSourceMaterialAsync(int id)
        {
            var sourceMaterial = await _context.SourceMaterials.FindAsync(id);

            if (sourceMaterial != null)
            {
                _context.SourceMaterials.Remove(sourceMaterial);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("Source Material is Null");
            }
        }

        bool SourceMaterialExists(int id)
        {
            return _context.SourceMaterials.Any(x => x.SourceMaterialId == id);
        }
    }
}
