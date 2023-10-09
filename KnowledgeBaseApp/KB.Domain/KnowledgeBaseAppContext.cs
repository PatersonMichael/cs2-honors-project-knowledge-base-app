using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain
{
    internal class KnowledgeBaseAppContext : DbContext
    {
        public KnowledgeBaseAppContext(DbContextOptions<KnowledgeBaseAppContext> options) : base(options)
        {
        }

        // DB Entities based on Model classes
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Citation> Citations { get; set; }

        public DbSet<ExcerptCard> ExcerptCards { get; set; }

        public DbSet<Keyword> Keywords { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<SourceMaterial> SourceMaterials { get; set; }

        public DbSet<SourceMaterialAuthor> SourceMaterialAuthors { get; set; }

        public DbSet<ExcerptCardKeyword> ExcerptCardKeywords { get; set; }

        public DbSet<NoteKeyword> NoteKeywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("KnowledgeBase");

            // UserProfile Properties
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile").HasKey(x => x.UserProfileId);

            // Author Properties
            
            // Citation Properties

            // ExcerptCard Properites

            // Keyword Properties

            // Note Properties

            // SourceMaterial Properties

            // SourceMaterialAuthor Properties

            // ExcerptCardKeyword Properties

            // NoteKeyword Properties

        }

    }
}
