using System;
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

namespace KB.Domain.Repositories
{
    internal class KeywordRepository : IKeywordRepository
    {
        private readonly ILogger<KeywordRepository> _logger;
        private readonly KnowledgeBaseAppContext _context;

        public KeywordRepository(ILogger<KeywordRepository> logger, KnowledgeBaseAppContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET
        public async Task<IEnumerable<Keyword>> GetKeywordsAsync()
        {
            _logger.LogInformation("Begin GetKeywordsAsync from KeywordRepository");

            return await _context.Keywords
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Keyword> GetKeywordAsync(int id)
        {
            _logger.LogInformation("Begin GetKeywordAsync from KeywordRepository");

            if (id == 0)
            {
                throw new BadRequestException("id is needed");
            }

            try
            {
                var keyword = await _context.Keywords
                    .AsNoTracking()
                    .SingleAsync(x => x.KeywordId == id);

                return keyword;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"Keyword with id: {id} is not found");
            }


        }

        // POST

        public async Task<Keyword> PostKeywordAsync(Keyword keyword)
        {
            _logger.LogInformation("Begin PostKeywordAsync from KeywordRepository");

                _context.Keywords.Add(keyword);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KeywordExists(keyword.KeywordId))
                {
                    throw new ConflictException("Keyword already exists");
                }

                throw;
            }

            return keyword;
        }

        // PUT

        public async Task<Keyword> PutKeywordAsync(int id, Keyword keyword)
        {
            _logger.LogInformation("Begin PutKeywordAsync from KeywordRepository");

            if (id == 0)
            {
                throw new BadRequestException("id is needed");
            }

            if (id != keyword.KeywordId)
            {
                return null;
            }

            _context.Keywords.Update(keyword);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeywordExists(keyword.KeywordId))
                {
                    throw new NotFoundException("Keyword not found");
                }

                throw;
            }

            return keyword;
        }

        // DELETE
        public async Task DeleteKeywordAsync(int id)
        {
            _logger.LogInformation("Begin DeleteKeywordAsync from KeywordRepository");

            if (id == 0)
            {
                throw new BadRequestException("id is needed");
            }

            var keyword = await _context.Keywords.FindAsync(id);

            if (keyword != null)
            {
                _context.Remove(keyword);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("Keyword is null");
            }
        }

        private bool KeywordExists(int id)
        {
            return _context.Keywords.Any(x => x.KeywordId == id);
        }
    }
}
