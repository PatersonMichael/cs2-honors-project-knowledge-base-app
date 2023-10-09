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

            // ------- Mapping Properties from Domain Models to DB Tables using Entity Framework ModelBuilder --------

            // UserProfile Properties
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile").HasKey(x => x.UserProfileId);
            modelBuilder.Entity<UserProfile>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<UserProfile>().Property(x => x.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<UserProfile>().Property(x => x.LastName).HasColumnName("LastName");
            modelBuilder.Entity<UserProfile>().Property(x => x.Email).HasColumnName("Email");
            modelBuilder.Entity<UserProfile>().Property(x => x.CreationDate).HasColumnName("CreationDate");
            modelBuilder.Entity<UserProfile>().Property(x => x.BirthDate).HasColumnName("BirthDate");
            modelBuilder.Entity<UserProfile>().Property(x => x.Password).HasColumnName("Password");
            modelBuilder.Entity<UserProfile>().Property(x => x.Nametag).HasColumnName("Nametag");

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
