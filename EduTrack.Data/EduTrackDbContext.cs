using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace EduTrack.Data;



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
    public DbSet<TeacherClassLesson> TeacherClassLessons { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Bileşik anahtarlar
        modelBuilder.Entity<ClassUser>()
          .HasKey(cu => new { cu.ClassRoomId, cu.UserId });

        modelBuilder.Entity<ClassLesson>()
            .HasKey(cl => new { cl.ClassRoomId, cl.LessonId });

        modelBuilder.Entity<TeacherClassLesson>()
            .HasKey(tcl => new { tcl.TeacherId, tcl.ClassRoomId, tcl.LessonId });
    }
}
