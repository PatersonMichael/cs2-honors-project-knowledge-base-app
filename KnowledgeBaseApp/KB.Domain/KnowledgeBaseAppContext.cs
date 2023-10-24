using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain
{
    public class KnowledgeBaseAppContext : DbContext
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




        // ------- Mapping Properties from Domain Models to DB Tables using Entity Framework ModelBuilder --------

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("KnowledgeBase");

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
            modelBuilder.Entity<Author>().ToTable("Author").HasKey(x => x.AuthorId);
            modelBuilder.Entity<Author>().Property(x => x.AuthorId).HasColumnName("AuthorID");
            modelBuilder.Entity<Author>().Property(x => x.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Author>().Property(x => x.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Author>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<Author>().HasOne(a => a.UserProfile)
                .WithMany(u => u.Authors)
                .HasForeignKey(a => a.UserProfileId);

            // Citation Properties

            // ExcerptCard Properites

            // Keyword Properties

            // Note Properties

            // SourceMaterial Properties
            modelBuilder.Entity<SourceMaterial>().ToTable("SourceMaterial").HasKey(x => x.SourceMaterialId);
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialId).HasColumnName("SourceMaterialID");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.Title).HasColumnName("Title");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.Publisher).HasColumnName("Publisher");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.PublishDate).HasColumnName("PublishDate");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialEdition).HasColumnName("SourceMaterialEdition");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialType).HasColumnName("SourceMaterialType");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");

            // SourceMaterialAuthor Properties

            // ExcerptCardKeyword Properties

            // NoteKeyword Properties

        }

    }
}
