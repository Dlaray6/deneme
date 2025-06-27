using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Data
{
    public class EduTrackDbContext : DbContext
    {
        public EduTrackDbContext(DbContextOptions<EduTrackDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClassRoom> Classes { get; set; }
        public DbSet<ClassUser> ClassUsers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ClassLesson> ClassLessons { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ClassUser composite key
            modelBuilder.Entity<ClassUser>()
                .HasKey(cu => new { cu.ClassRoomId, cu.UserId });

            // ClassLesson composite key (ClassId + LessonId)
            modelBuilder.Entity<ClassLesson>()
                .HasKey(cl => new { cl.ClassRoomId, cl.LessonId });

            modelBuilder.Entity<ClassLesson>()
                .HasOne(cl => cl.ClassRoom)
                .WithMany(c => c.ClassLessons)
                .HasForeignKey(cl => cl.ClassRoomId);

            modelBuilder.Entity<ClassLesson>()
                .HasOne(cl => cl.Lesson)
                .WithMany(l => l.ClassLessons)
                .HasForeignKey(cl => cl.LessonId);

            modelBuilder.Entity<ClassLesson>()
     .HasOne(cl => cl.Lesson)
     .WithMany(l => l.ClassLessons)
     .HasForeignKey(cl => cl.LessonId)
     .OnDelete(DeleteBehavior.Restrict);
            // Note → ClassLesson ilişki
            modelBuilder.Entity<Note>()
                .HasOne(n => n.ClassLesson)
                .WithMany(cl => cl.Notes)
                .HasForeignKey(n => new { n.ClassLessonClassId, n.ClassLessonLessonId })
                .OnDelete(DeleteBehavior.Cascade);

            // Note → ClassUser ilişki
            modelBuilder.Entity<Note>()
                .HasOne(n => n.ClassUser)
                .WithMany(cu => cu.Notes)
                .HasForeignKey(n => new { n.ClassUserClassId, n.ClassUserUserId })
                .OnDelete(DeleteBehavior.Restrict);

            // User → School ilişki
            modelBuilder.Entity<User>()
                .HasOne(u => u.School)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SchoolId);

            // ClassRoom → School ilişki
            modelBuilder.Entity<ClassRoom>()
                .HasOne(c => c.School)
                .WithMany()
                .HasForeignKey(c => c.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lesson → School ilişki
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.School)
                .WithMany()
                .HasForeignKey(l => l.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
