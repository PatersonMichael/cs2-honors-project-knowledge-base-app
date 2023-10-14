using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;


namespace KB.Domain.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        // Get Authors

        /// <summary>
        /// Get Authors
        /// </summary>
        /// <returns>A collection of authors</returns>
        Task<IEnumerable<Author>> GetAuthorsAsync();

        // Get single Author

        /// <summary>
        /// Get Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single author that matches the given id</returns>
        Task<Author> GetAuthorAsync(int id);

        // Post Author

        /// <summary>
        /// Post a new Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns>A new author</returns>
        Task<Author> PostAuthorAsync(Author author);

        // Put Author

        /// <summary>
        /// Put author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns>An updated Author</returns>
        Task<Author> PutAuthorAsync(int id, Author author);

        // Delete Author

        /// <summary>
        /// Delete Author
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAuthorAsync(int id);
    }
}
