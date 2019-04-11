using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PN.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=EntityConnection") { }

        #region SETTERS AND GETTERS

        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Edition> Edition { get; set; }
        public virtual DbSet<EditionLinesLess> EditionLinesLess { get; set; }
        public virtual DbSet<EditionLinesPluss> EditionLinesPluss { get; set; }
        public virtual DbSet<Forum> Forum { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagPost> TagPost { get; set; }
        public virtual DbSet<UserForumSubscription> UserForumSubscription { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<TagsView> TagsView { get; set; }
        public virtual DbSet<UserInformationView> UserInformationView { get; set; }

        #endregion

        #region OVERRIDE

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Audit>()
                .Property(e => e.Query)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.Country)
                .Map(m => m.ToTable("UserCountry").MapLeftKey("CountryId").MapRightKey("UserId"));

            modelBuilder.Entity<Edition>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Edition>()
                .HasMany(e => e.Post)
                .WithMany(e => e.Edition)
                .Map(m => m.ToTable("EditionPost").MapLeftKey("EditionId").MapRightKey("PostId"));

            modelBuilder.Entity<Edition>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.Edition)
                .Map(m => m.ToTable("EditionUser").MapLeftKey("EditionId").MapRightKey("UserId"));

            modelBuilder.Entity<EditionLinesLess>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<EditionLinesPluss>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.UserForumSubscription)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Tag)
                .WithMany(e => e.Forum)
                .Map(m => m.ToTable("ForumTag").MapLeftKey("IdForum").MapRightKey("IdTag"));

            modelBuilder.Entity<Post>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.UserInformation)
                .WithMany(e => e.Post)
                .Map(m => m.ToTable("PostUser").MapLeftKey("PostId").MapRightKey("UserId"));

            modelBuilder.Entity<Tag>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Desciption)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.TagPost)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.Sex)
                .IsFixedLength();

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.UserForumSubscription)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TagsView>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TagsView>()
                .Property(e => e.Desciption)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.Sex)
                .IsFixedLength();

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.PostTitle)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.EditionTitle)
                .IsUnicode(false);
        }

        #endregion

        #region STORED PROCEDURES

        public Task<bool> StoredProcedureRegisterUser(string userId, string sex)
        {
            try
            {
                base.Database.ExecuteSqlCommand($"StoredProcedureRegisterUser '{userId}', '{sex}'");
                return Task.FromResult(true); 
            }
            catch (Exception) { return Task.FromResult(false); }
        }

        public Task<UserInformation> StoredProcedureGetUserInformationId(string userId)
        {
            try
            {
                return this.UserInformation.SqlQuery($"StoredProcedureGetUserInformationId '{userId}'").FirstOrDefaultAsync();
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
