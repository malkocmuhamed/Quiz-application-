using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Web_API.Models
{
    public partial class Quiz_DBContext : DbContext
    {
        public Quiz_DBContext(DbContextOptions<Quiz_DBContext> options)
            : base(options)
        {
        }

        //public Quiz_DBContext()
        //    : base("name=Quiz_DBContext")
        //{
        //}

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<QuizQuestionsAnswer> QuizQuestionsAnswers { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=quiz_DB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_courseSemesterId");
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.ToTable("degrees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("quizzes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_quizCourseId");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.ToTable("quizQuestions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Answer).HasColumnName("answer");
                
                entity.Property(e => e.Option1).HasColumnName("option1");

                entity.Property(e => e.Option2).HasColumnName("option2");

                entity.Property(e => e.Option3).HasColumnName("option3");

                entity.Property(e => e.Option4).HasColumnName("option4");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_quizQuestionsQuizId");
            });

            modelBuilder.Entity<QuizQuestionsAnswer>(entity =>
            {
                entity.ToTable("quizQuestionsAnswers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrdinalNo).HasColumnName("ordinal_no");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizQuestionsAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_quizQuestionsAnswersQuestionId");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("semesters");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DegreeId).HasColumnName("degree_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.DegreeId)
                    .HasConstraintName("FK_deegreid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

              
                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_usersUserTypeId");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("userTypes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
