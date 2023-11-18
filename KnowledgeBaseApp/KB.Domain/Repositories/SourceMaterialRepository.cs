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
    public class SourceMaterialRepository : ISourceMaterialRepository
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
            _logger.LogInformation("Begin GetSourceMaterialsAsync from Repository");
            return await _context.SourceMaterials
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<IEnumerable<SourceMaterial>> GetUserSourceMaterialsAsync(int userProfileId)
        {
            _logger.LogInformation("Begin GetSourceMaterialsAsync from Repository");
            return await _context.SourceMaterials
                .Where(x => x.UserProfileId == userProfileId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SourceMaterial> GetSourceMaterialAsync(int id)
        {
            _logger.LogInformation("Begin GetSourceMaterialAsync from Repository");

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
        public async Task<SourceMaterial> GetUserSourceMaterialAsync(int sourceId, int userProfileId)
        {
            _logger.LogInformation("Begin GetSourceMaterialAsync from Repository");

            try
            {
                var sourceMaterial = await _context.SourceMaterials
                    .Where(x => x.UserProfileId == userProfileId)
                    .AsNoTracking()
                    .SingleAsync(x => x.SourceMaterialId == sourceId);

                return sourceMaterial;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"Source Material with id {sourceId} was not found");
            }
        }

        // POST
        public async Task<SourceMaterial> PostSourceMaterialAsync(SourceMaterial sourceMaterial)
        {
            _logger.LogInformation("Begin PostSourceMaterialsAsync from Repository");

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
            _logger.LogInformation("Begin PutSourceMaterialsAsync from Repository");

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
            _logger.LogInformation("Begin DeleteSourceMaterialsAsync from Repository");

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
