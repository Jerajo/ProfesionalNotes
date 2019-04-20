using System;
using System.Data.Entity;
using Audit.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace PN.Models
{
    public partial class AppDbContext : AuditDbContext
    {
        public AppDbContext() : base("name=EntityConnection") { }

        #region SETTERS AND GETTERS

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Edition> Edition { get; set; }
        public virtual DbSet<Forum> Forum { get; set; }
        public virtual DbSet<ForumUser> ForumUser { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostUser> PostUser { get; set; }
        public virtual DbSet<RandomLink> RandomLink { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagPost> TagPost { get; set; }
        public virtual DbSet<TagUser> TagUser { get; set; }
        public virtual DbSet<UserCulture> UserCulture { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<ForumUserActivityView> ForumUserActivityView { get; set; }
        public virtual DbSet<PostUserActivityView> PostUserActivityView { get; set; }
        public virtual DbSet<TagUserActivityView> TagUserActivityView { get; set; }
        public virtual DbSet<UserEditionListView> UserEditionListView { get; set; }
        public virtual DbSet<UserForumListView> UserForumListView { get; set; }
        public virtual DbSet<UserInformationView> UserInformationView { get; set; }
        public virtual DbSet<UserListView> UserListView { get; set; }
        public virtual DbSet<UserPostListView> UserPostListView { get; set; }
        public virtual DbSet<UserTagListView> UserTagListView { get; set; }

        #endregion

        #region OVERRIDE

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(e => e.ISORegion)
                .IsFixedLength();

            modelBuilder.Entity<Forum>()
                .Property(e => e.ISOLanguage)
                .IsFixedLength();

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Tag)
                .WithMany(e => e.Forum)
                .Map(m => m.ToTable("ForumTag").MapLeftKey("ForumId").MapRightKey("TagId"));

            modelBuilder.Entity<Language>()
                .Property(e => e.ISOLanguage)
                .IsFixedLength();

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Forum)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<RandomLink>()
                .Property(e => e.ShortedLink)
                .IsFixedLength();

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

            modelBuilder.Entity<UserCulture>()
                .Property(e => e.ISORegion)
                .IsFixedLength();

            modelBuilder.Entity<UserCulture>()
                .Property(e => e.ISOLanguage)
                .IsFixedLength();

            modelBuilder.Entity<UserInformation>()
                .Property(e => e.Sex)
                .IsFixedLength();

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Edition)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Forum)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.ForumUser)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.PostUser)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.Tag)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.TagUser)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInformation>()
                .HasMany(e => e.UserCulture)
                .WithRequired(e => e.UserInformation)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUserActivityView>()
                .Property(e => e.ISOLanguage)
                .IsFixedLength();

            modelBuilder.Entity<PostUserActivityView>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<PostUserActivityView>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<TagUserActivityView>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TagUserActivityView>()
                .Property(e => e.Desciption)
                .IsUnicode(false);

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.Sex)
                .IsFixedLength();

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.ISORegion)
                .IsFixedLength();

            modelBuilder.Entity<UserInformationView>()
                .Property(e => e.ISOLanguage)
                .IsFixedLength();

            modelBuilder.Entity<UserPostListView>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<UserTagListView>()
                .Property(e => e.Name)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Deleted) return true;

            return base.ShouldValidateEntity(entityEntry);  
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry.Entity is UserInformation)
            {
                return new DbEntityValidationResult(entityEntry, new DbValidationError[] 
                {
                    new DbValidationError("Name","No se puede borrar un usuario")
                });
            }

            return base.ValidateEntity(entityEntry, items);
        }
        
        public override int SaveChanges()
        {
            foreach (var errors in base.GetValidationErrors())
            {
                foreach (var error in errors.ValidationErrors)
                {
                    var errorMessage = error.ErrorMessage;
                    var property = error.PropertyName;
                }
                var entity = errors.Entry;
                // Salta las entidades con errores
                //this.Entry(errors.Entry).State = EntityState.Detached;
            }

            try { return base.SaveChanges(); }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion

        #region Auxiliary Methods

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
