using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using KB.Common.Exceptions;
using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KB.Domain.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        // db context
        private readonly KnowledgeBaseAppContext _context;

        // logger
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(KnowledgeBaseAppContext context, ILogger<AuthorRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            _logger.LogInformation("Begin GetAuthorsAsync in AuthorRepository ");

            return await _context.Authors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            _logger.LogInformation("Begin GetAuthorAsync in AuthorRepository ");

            try
            {
                var Author = await _context.Authors
                    .AsNoTracking()
                    .SingleAsync(x => x.AuthorId == id);

                return Author;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"Author with Id {id} was not found.");
            }
        }

        public async Task<Author> PostAuthorAsync(Author author)
        {
            _logger.LogInformation("Begin PostAuthorAsync in AuthorRepository ");

            await _context.Authors.AddAsync(author);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.AuthorId))
                {
                    throw new ConflictException("Author already exists");
                }

                throw;
            }

            return author;
        }

        public async Task<Author> PutAuthorAsync(int id, Author author)
        {
            _logger.LogInformation("Begin PutAuthorsAsync in AuthorRepository ");

            if (id != author.AuthorId)
            {
                throw new BadRequestException($"id {id} is invalid");
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.AuthorId))
                {
                    throw new NotFoundException("Author was not found");
                }

                throw;
            }

            return author;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            _logger.LogInformation("Begin DeleteAuthorsAsync in AuthorRepository ");

            var author = await _context.Authors.FindAsync(id);

            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("Author is null");
            }
        }


        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
        
    }
}
