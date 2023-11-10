﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class CitationRepository : ICitationRepository
    {
        private readonly ILogger<CitationRepository> _logger;

        private readonly KnowledgeBaseAppContext _context;

        public CitationRepository(ILogger<CitationRepository> logger, KnowledgeBaseAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Citation>> GetCitationsAsync()
        {
            _logger.LogInformation("Begin GetCitationsAsync from CitationRepository");

            return await _context.Citations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Citation> GetCitationAsync(int id)
        {
            _logger.LogInformation("Begin GetCitationAsync from CitationRepository");

            try
            {
                var Citation = await _context.Citations
                    .AsNoTracking()
                    .SingleAsync(x => x.CitationId == id);

                return Citation;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"User with id: {id} was not found.");
            }
        }
        // POST

        public async Task<Citation> PostCitationAsync(Citation citation)
        {
            _logger.LogInformation("Begin PostCitationAsync from CitationRepository");


            await _context.Citations.AddAsync(citation);

            try
            {
                // if source material within citation already exists, use the existing source material
                if (SourceMaterialExists(citation.sourceMaterial.SourceMaterialEdition, citation.sourceMaterial.Title))
                {
                    citation.sourceMaterial =
                        ExistingSourceMaterial(citation.sourceMaterial.SourceMaterialEdition,
                            citation.sourceMaterial.Title);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CitationExists(citation.CitationId))
                {
                    throw new ConflictException("Citation already exists");
                }

                throw;
            }

            return citation;
        }

        // PUT

        public async Task<Citation> PutCitationAsync(int id, Citation citation)
        {
            _logger.LogInformation("Begin PutCitationAsync from CitationRepository");

            if (id != citation.CitationId)
            {
                return null;
            }

            _context.Entry(citation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitationExists(citation.CitationId))
                {
                    throw new NotFoundException("Citation was not found");
                }

                throw;
            }

            return citation;
        }

        // DELETE

        public async Task DeleteCitationAsync(int id)
        {
            _logger.LogInformation("Begin DeleteCitationAsync from CitationRepository");

            var citation = await _context.Citations.FindAsync(id);
            if (citation != null)
            {
                _context.Citations.Remove(citation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("Citation is null");
            }
        }

        private bool CitationExists(int id)
        {
            return _context.Citations.Any(x => x.CitationId == id);
        }

        private bool SourceMaterialExists(string edition, string title)
        {
            return _context.Citations.Any(x =>
                x.sourceMaterial.SourceMaterialEdition == edition && x.sourceMaterial.Title == title);
        }

        private SourceMaterial ExistingSourceMaterial(string edition, string title)
        {
            // find the source material where edition and title match the parameters
            // return the id of that source material

            return _context.SourceMaterials.Single(x => x.Title == title && x.SourceMaterialEdition == edition);
        }

    }
}