using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
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

        //public DbSet<Author> Authors { get; set; }

        public DbSet<Citation> Citations { get; set; }

        public DbSet<ExcerptCard> ExcerptCards { get; set; }

        public DbSet<Keyword> Keywords { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<SourceMaterial> SourceMaterials { get; set; }

        //public DbSet<SourceMaterialAuthor> SourceMaterialAuthors { get; set; }

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
            //modelBuilder.Entity<Author>().ToTable("Author").HasKey(x => x.AuthorId);
            //modelBuilder.Entity<Author>().Property(x => x.AuthorId).HasColumnName("AuthorID");
            //modelBuilder.Entity<Author>().Property(x => x.FirstName).HasColumnName("FirstName");
            //modelBuilder.Entity<Author>().Property(x => x.LastName).HasColumnName("LastName");
            //modelBuilder.Entity<Author>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            //modelBuilder.Entity<Author>().HasOne(a => a.UserProfile)
            //    .WithMany(u => u.Authors)
            //    .HasForeignKey(a => a.UserProfileId);
            //modelBuilder.Entity<Author>().HasMany(a => a.SourceMaterials)
            //    .WithMany(s => s.Authors)
            //    .UsingEntity<SourceMaterialAuthor>();


            // Citation Properties
            modelBuilder.Entity<Citation>().ToTable("Citation").HasKey(x => x.CitationId);
            modelBuilder.Entity<Citation>().Property(x => x.CitationId).HasColumnName("CitationID");
            modelBuilder.Entity<Citation>().Property(x => x.Format).HasColumnName("Format");
            modelBuilder.Entity<Citation>().Property(x => x.ExcerptLocation).HasColumnName("ExcerptLocation");
            modelBuilder.Entity<Citation>().Property(x => x.CreationDate).HasColumnName("CreationDate");
            modelBuilder.Entity<Citation>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<Citation>().Property(x => x.SourceMaterialId).HasColumnName("SourceMaterialID");
            modelBuilder.Entity<Citation>().HasOne(c => c.sourceMaterial).WithMany()
                .HasForeignKey(c => c.SourceMaterialId);

            // ExcerptCard Properites
            modelBuilder.Entity<ExcerptCard>().ToTable("ExcerptCard").HasKey(x => x.ExcerptCardId);
            modelBuilder.Entity<ExcerptCard>().Property(x => x.ExcerptCardId).HasColumnName("ExcerptCardID");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.Title).HasColumnName("Title");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.Excerpt).HasColumnName("Excerpt");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.CreationDate).HasColumnName("CreationDate");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<ExcerptCard>().Property(x => x.CitationId).HasColumnName("CitationID");
            modelBuilder.Entity<ExcerptCard>().HasOne(e => e.Citation).WithOne();
            modelBuilder.Entity<ExcerptCard>().HasMany(e => e.Keywords)
                .WithMany()
                .UsingEntity<ExcerptCardKeyword>();

            // Keyword Properties
            modelBuilder.Entity<Keyword>().ToTable("Keyword").HasKey(x => x.KeywordId);
            modelBuilder.Entity<Keyword>().Property(x => x.KeywordId).HasColumnName("KeywordID");
            modelBuilder.Entity<Keyword>().Property(x => x.Name).HasColumnName("Name");
            modelBuilder.Entity<Keyword>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");

            // Note Properties
            modelBuilder.Entity<Note>().ToTable("Note").HasKey(x => x.NoteId);
            modelBuilder.Entity<Note>().Property(x => x.NoteId).HasColumnName("NoteID");
            modelBuilder.Entity<Note>().Property(x => x.Title).HasColumnName("Title");
            modelBuilder.Entity<Note>().Property(x => x.Body).HasColumnName("Body");
            modelBuilder.Entity<Note>().Property(x => x.CreationDate).HasColumnName("CreationDate");
            modelBuilder.Entity<Note>().Property(x => x.LastUpdateDate).HasColumnName("LastUpdatedDate");
            modelBuilder.Entity<Note>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<Note>().HasMany(x => x.Keywords)
                .WithMany()
                .UsingEntity<NoteKeyword>();

            // SourceMaterial Properties
            modelBuilder.Entity<SourceMaterial>().ToTable("SourceMaterial").HasKey(x => x.SourceMaterialId);
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialId).HasColumnName("SourceMaterialID");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.Title).HasColumnName("Title");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.Publisher).HasColumnName("Publisher");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.PublishDate).HasColumnName("PublishDate");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialEdition).HasColumnName("SourceMaterialEdition");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.SourceMaterialType).HasColumnName("SourceMaterialType");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.UserProfileId).HasColumnName("UserProfileID");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.AuthorFirstName).HasColumnName("AuthorFirstName");
            modelBuilder.Entity<SourceMaterial>().Property(x => x.AuthorLastName).HasColumnName("AuthorLastName");
            
            //modelBuilder.Entity<SourceMaterial>().HasMany(s => s.Authors)
            //    .WithMany(a => a.SourceMaterials)
            //    .UsingEntity<SourceMaterialAuthor>();

            // SourceMaterialAuthor Properties
            //modelBuilder.Entity<SourceMaterialAuthor>().ToTable("SourceMaterialAuthor").HasKey(x => new {x.SourceMaterialId, x.AuthorId});
            //modelBuilder.Entity<SourceMaterialAuthor>().Property(x => x.SourceMaterialId).HasColumnName("SourceMaterialID");
            //modelBuilder.Entity<SourceMaterialAuthor>().Property(x => x.AuthorId).HasColumnName("AuthorID");

            // ExcerptCardKeyword Properties
            modelBuilder.Entity<ExcerptCardKeyword>().ToTable("ExcerptCardKeyword").HasKey(x => new {x.ExcerptCardId, x.KeywordId});
            modelBuilder.Entity<ExcerptCardKeyword>().Property(x => x.ExcerptCardId).HasColumnName("ExcerptCardID");
            modelBuilder.Entity<ExcerptCardKeyword>().Property(x => x.KeywordId).HasColumnName("KeywordID");

            // NoteKeyword Properties
            modelBuilder.Entity<NoteKeyword>().ToTable("NoteKeyword").HasKey(x => new { x.KeywordId, x.NoteId });
            modelBuilder.Entity<NoteKeyword>().Property(x => x.NoteId).HasColumnName("NoteID");
            modelBuilder.Entity<NoteKeyword>().Property(x => x.KeywordId).HasColumnName("KeywordID");

        }

    }
}
