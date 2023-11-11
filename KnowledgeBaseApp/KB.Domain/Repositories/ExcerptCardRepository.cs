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
    public class ExcerptCardRepository : IExcerptCardRepository
    {
        private readonly ILogger<ExcerptCardRepository> _logger;
        private readonly KnowledgeBaseAppContext _context;

        public ExcerptCardRepository(ILogger<ExcerptCardRepository> logger, KnowledgeBaseAppContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        // GET
        public async Task<IEnumerable<ExcerptCard>> GetExcerptCardsAsync()
        {
            _logger.LogInformation("Begin GetExcerptCardsAsync from ExcerptCardRepository");

            return await _context.ExcerptCards
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ExcerptCard> GetExcerptCardAsync(int id)
        {
            _logger.LogInformation("Begin GetExcerptCardAsync from ExcerptCardRepository");

            try
            {
                var exCard = await _context.ExcerptCards
                    .AsNoTracking()
                    .Include(x => x.Citation)
                    .Include(x => x.Citation.sourceMaterial)
                    .SingleAsync(x => x.ExcerptCardId == id);

                return exCard;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"Excerpt Card with id {id} was not found");
            }
        }

        // POST

        public async Task<ExcerptCard> PostExcerptCardAsync(ExcerptCard excerptCard)
        {
            _logger.LogInformation("Begin PostExcerptCardAsync from ExcerptCardRepository");

            _context.ExcerptCards.Update(excerptCard);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExcerptCardExists(excerptCard.ExcerptCardId))
                {
                    throw new ConflictException("Excerpt Card already exists");
                }

                throw;
            }
            return excerptCard;
        }

        // PUT
        public async Task<ExcerptCard> PutExcerptCardAsync(int id, ExcerptCard excerptCard)
        {
            _logger.LogInformation("Begin PutExcerptCardAsync from ExcerptCardRepository");

            if (id != excerptCard.ExcerptCardId)
            {
                return null;
            }

            _context.ExcerptCards.Update(excerptCard);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcerptCardExists(excerptCard.ExcerptCardId))
                {
                    throw new NotFoundException("Excerpt Card not found");
                }
                throw;
            }

            return excerptCard;
        }

        // DELETE
        public async Task DeleteExcerptCardAsync(int id)
        {
            _logger.LogInformation("Begin DeleteExcerptCardAsync from ExcerptCardRepository");

            if (id == 0)
            {
                throw new BadRequestException("id is required");
            }

            var excerptCard = await _context.ExcerptCards.FindAsync(id);

            if (excerptCard != null)
            {
                _context.ExcerptCards.Remove(excerptCard);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("Excerpt card entity is null");
            }
        }


        private bool ExcerptCardExists(int id)
        {
            return _context.ExcerptCards.Any(x => x.ExcerptCardId == id);
        }

    }
}
