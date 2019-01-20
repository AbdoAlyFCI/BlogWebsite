using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogWebsite.Models
{
    public partial class InfiniteBlogDBContext : DbContext
    {
        public InfiniteBlogDBContext()
        {
        }

        public InfiniteBlogDBContext(DbContextOptions<InfiniteBlogDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Directory> Directory { get; set; }
        public virtual DbSet<FileComment> FileComment { get; set; }
        public virtual DbSet<FileReact> FileReact { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FileTag> FileTag { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<React> React { get; set; }
        public virtual DbSet<RelationShip> RelationShip { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersImg> UsersImg { get; set; }
        public virtual DbSet<Ustate> Ustate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=InfiniteBlogDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasKey(e => e.CId);

                entity.Property(e => e.CId)
                    .HasColumnName("C_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CFollowers).HasColumnName("C_Followers");

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("C_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.COwnerId)
                    .IsRequired()
                    .HasColumnName("C_OwnerID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CDescription)
                    .IsRequired()
                    .HasColumnName("CDescription")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CIMG)
                    .HasColumnName("C_img");


                entity.Property(e => e.CTotalWatch).HasColumnName("C_TotalWatch");

                entity.HasOne(d => d.COwner)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.COwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userChannelConstraint");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CId);

                entity.Property(e => e.CId)
                    .HasColumnName("C_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CDepth).HasColumnName("C_Depth");

                entity.Property(e => e.CPid).HasColumnName("C_PID");

                entity.Property(e => e.CUserId)
                    .IsRequired()
                    .HasColumnName("C_UserID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CUser)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userCommetnConstrait");
            });

            modelBuilder.Entity<Directory>(entity =>
            {
                entity.HasKey(e => e.DId);

                entity.Property(e => e.DId)
                    .HasColumnName("D_ID")
                    .HasMaxLength(50)
                    .IsUnicode()
                    .ValueGeneratedNever();

                entity.Property(e => e.DDepth).HasColumnName("D_Depth");

                entity.Property(e => e.DName)
                    .IsRequired()
                    .HasColumnName("D_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DOwnerId)
                    .IsRequired()
                    .HasColumnName("D_OwnerID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DParentId)
                .HasColumnName("D_ParentID")
                .HasMaxLength(50)
                .IsUnicode();

                entity.Property(e => e.DType).HasColumnName("D_type");

                entity.HasOne(d => d.DOwner)
                    .WithMany(p => p.Directory)
                    .HasForeignKey(d => d.DOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userDirectoryConstraint");

                entity.HasOne(d => d.DTypeNavigation)
                    .WithMany(p => p.Directory)
                    .HasForeignKey(d => d.DType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("typeFileDirectoryConstraint");
            });

            modelBuilder.Entity<FileComment>(entity =>
            {
                entity.HasKey(e => new { e.FId, e.CId });

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.Property(e => e.CId).HasColumnName("C_ID");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.FileComment)
                    .HasForeignKey(d => d.CId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CommentConstraint");

                entity.HasOne(d => d.F)
                    .WithMany(p => p.FileComment)
                    .HasForeignKey(d => d.FId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userCommentConstraint");
            });

            modelBuilder.Entity<FileReact>(entity =>
            {
                entity.HasKey(e => new { e.FId, e.UId });

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.Property(e => e.UId)
                    .HasColumnName("U_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RId).HasColumnName("R_ID");

                entity.HasOne(d => d.F)
                    .WithMany(p => p.FileReact)
                    .HasForeignKey(d => d.FId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fileReactConstraint");

                entity.HasOne(d => d.R)
                    .WithMany(p => p.FileReact)
                    .HasForeignKey(d => d.RId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reactConstraint");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.FileReact)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userReactConstraint");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.Property(e => e.FId)
                    .HasColumnName("F_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FCid)
                    .IsRequired()
                    .HasColumnName("F_CID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FDescription)
                    .HasColumnName("F_Description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FImg)
                    .HasColumnName("F_img");
                    

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("F_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FPublishDate)
                    .HasColumnName("F_PublishDate")
                    .HasColumnType("date");

                entity.Property(e => e.FPublishState).HasColumnName("F_PublishState");

                entity.Property(e => e.FText)
                    .HasColumnName("F_Text")
                    .IsUnicode(false);

                entity.Property(e => e.FView).HasColumnName("F_View");

                entity.HasOne(d => d.FC)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FCid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ownerFileConstraint");
            });

            modelBuilder.Entity<FileTag>(entity =>
            {
                entity.HasKey(e => new { e.FId, e.TId });

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.Property(e => e.TId).HasColumnName("T_ID");

                entity.HasOne(d => d.F)
                    .WithMany(p => p.FileTag)
                    .HasForeignKey(d => d.FId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fileTageConstraintS");

                entity.HasOne(d => d.T)
                    .WithMany(p => p.FileTag)
                    .HasForeignKey(d => d.TId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fileTageConstraintSS");
            });

            modelBuilder.Entity<FileType>(entity =>
            {
                entity.HasKey(e => e.TNum);

                entity.ToTable("fileType");

                entity.Property(e => e.TNum)
                    .HasColumnName("T_num")
                    .ValueGeneratedNever();

                entity.Property(e => e.TText)
                    .IsRequired()
                    .HasColumnName("T_text")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<React>(entity =>
            {
                entity.HasKey(e => e.RId);

                entity.Property(e => e.RId)
                    .HasColumnName("R_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RName)
                    .IsRequired()
                    .HasColumnName("R_Name")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RelationShip>(entity =>
            {
                entity.HasKey(e => new { e.RUid, e.RCid });

                entity.Property(e => e.RUid)
                    .HasColumnName("R_UID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RCid)
                    .HasColumnName("R_CID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ActioinUserId)
                    .IsRequired()
                    .HasColumnName("actioinUserID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RStateId).HasColumnName("R_StateID");

                entity.HasOne(d => d.RC)
                    .WithMany(p => p.RelationShip)
                    .HasForeignKey(d => d.RCid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("channelRelationConstraint");

                entity.HasOne(d => d.RState)
                    .WithMany(p => p.RelationShip)
                    .HasForeignKey(d => d.RStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stateRelationsConstraint");

                entity.HasOne(d => d.RU)
                    .WithMany(p => p.RelationShip)
                    .HasForeignKey(d => d.RUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userRelationConstraint");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.TId);

                entity.Property(e => e.TId)
                    .HasColumnName("T_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TText)
                    .IsRequired()
                    .HasColumnName("T_Text")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.HasIndex(e => e.UEmail)
                    .HasName("UQ__Users__B6E45DC270FADCDD")
                    .IsUnique();

                entity.Property(e => e.UId)
                    .HasColumnName("U_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UEmail)
                    .IsRequired()
                    .HasColumnName("U_Email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UFirstName)
                    .IsRequired()
                    .HasColumnName("U_FirstName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ULastName)
                    .IsRequired()
                    .HasColumnName("U_LastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ULogIn)
                    .HasColumnName("U_LogIn")
                    .HasColumnType("date");

                entity.Property(e => e.UPassword)
                    .IsRequired()
                    .HasColumnName("U_Password")
                    .HasMaxLength(250);

                entity.Property(e => e.USignUp)
                    .HasColumnName("U_SignUp")
                    .HasColumnType("date");

                entity.Property(e => e.UBirthDay)
                   .IsRequired()
                   .HasColumnName("U_BirthDay")
                   .HasMaxLength(25)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<UsersImg>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.ToTable("Users_Img");

                entity.Property(e => e.UId)
                    .HasColumnName("U_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UImg).HasColumnName("U_img");
            });

            modelBuilder.Entity<Ustate>(entity =>
            {
                entity.HasKey(e => e.SId);

                entity.ToTable("UState");

                entity.Property(e => e.SId)
                    .HasColumnName("S_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SText)
                    .IsRequired()
                    .HasColumnName("S_text")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
