using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Entity;

public partial class SchoolRulesContext : DbContext
{
    public SchoolRulesContext()
    {
    }

    public SchoolRulesContext(DbContextOptions<SchoolRulesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassGroup> ClassGroups { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<HighSchool> HighSchools { get; set; }

    public virtual DbSet<PatrolSchedule> PatrolSchedules { get; set; }

    public virtual DbSet<Penalty> Penalties { get; set; }

    public virtual DbSet<Principal> Principals { get; set; }

    public virtual DbSet<RegisteredSchool> RegisteredSchools { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SchoolConfig> SchoolConfigs { get; set; }

    public virtual DbSet<SchoolYear> SchoolYears { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentInClass> StudentInClasses { get; set; }

    public virtual DbSet<StudentSupervisor> StudentSupervisors { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    public virtual DbSet<ViolationConfig> ViolationConfigs { get; set; }

    public virtual DbSet<ViolationGroup> ViolationGroups { get; set; }

    public virtual DbSet<ViolationType> ViolationTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config.GetConnectionString("DefaultConnection");
        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.ToTable("AttendanceRecord");

            entity.Property(e => e.AttendanceRecordId).HasColumnName("AttendanceRecordID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StudentInClassId).HasColumnName("StudentInClassID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.StudentInClass).WithMany(p => p.AttendanceRecords)
                .HasForeignKey(d => d.StudentInClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttendanceRecord_StudentInClass");

            entity.HasOne(d => d.Teacher).WithMany(p => p.AttendanceRecords)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttendanceRecord_Teacher");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassGroupId).HasColumnName("ClassGroupID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(70);
            entity.Property(e => e.Room).HasMaxLength(10);
            entity.Property(e => e.SchoolYearId).HasColumnName("SchoolYearID");

            entity.HasOne(d => d.ClassGroup).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_ClassGroup");

            entity.HasOne(d => d.SchoolYear).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SchoolYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_SchoolYear");

            entity.HasMany(d => d.Evaluations).WithMany(p => p.Classes)
                .UsingEntity<Dictionary<string, object>>(
                    "EvaluationDetail",
                    r => r.HasOne<Evaluation>().WithMany()
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EvaluationDetail_Evaluation"),
                    l => l.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EvaluationDetail_Class"),
                    j =>
                    {
                        j.HasKey("ClassId", "EvaluationId");
                        j.ToTable("EvaluationDetail");
                        j.IndexerProperty<int>("ClassId").HasColumnName("ClassID");
                        j.IndexerProperty<int>("EvaluationId").HasColumnName("EvaluationID");
                    });
        });

        modelBuilder.Entity<ClassGroup>(entity =>
        {
            entity.ToTable("ClassGroup");

            entity.Property(e => e.ClassGroupId).HasColumnName("ClassGroupID");
            entity.Property(e => e.ClassGroupName).HasMaxLength(50);
            entity.Property(e => e.Hall).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.ToTable("Discipline");

            entity.Property(e => e.DisciplineId).HasColumnName("DisciplineID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PennaltyId).HasColumnName("PennaltyID");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ViolationId).HasColumnName("ViolationID");

            entity.HasOne(d => d.Pennalty).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.PennaltyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discipline_Penalty");

            entity.HasOne(d => d.Violation).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.ViolationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discipline_Violation");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.ToTable("Evaluation");

            entity.Property(e => e.EvaluationId).HasColumnName("EvaluationID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.From).HasColumnType("date");
            entity.Property(e => e.SchoolYearId).HasColumnName("SchoolYearID");
            entity.Property(e => e.To).HasColumnType("date");

            entity.HasOne(d => d.SchoolYear).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.SchoolYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evaluation_SchoolYear");
        });

        modelBuilder.Entity<HighSchool>(entity =>
        {
            entity.HasKey(e => e.SchoolId);

            entity.ToTable("HighSchool");

            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.WebUrl)
                .HasMaxLength(500)
                .HasColumnName("WebURL");
        });

        modelBuilder.Entity<PatrolSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId);

            entity.ToTable("PatrolSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.From).HasColumnType("date");
            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.To).HasColumnType("date");

            entity.HasOne(d => d.Class).WithMany(p => p.PatrolSchedules)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatrolSchedule_Class");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.PatrolSchedules)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatrolSchedule_StudentSupervisor");

            entity.HasOne(d => d.Teacher).WithMany(p => p.PatrolSchedules)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatrolSchedule_Teacher");
        });

        modelBuilder.Entity<Penalty>(entity =>
        {
            entity.HasKey(e => e.PenaltyId).HasName("PK_ActivityType");

            entity.ToTable("Penalty");

            entity.Property(e => e.PenaltyId).HasColumnName("PenaltyID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.School).WithMany(p => p.Penalties)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Penalty_HighSchool");
        });

        modelBuilder.Entity<Principal>(entity =>
        {
            entity.HasKey(e => e.PrincipalId).HasName("PK__Principa__EB24D48FA58CEFA5");

            entity.ToTable("Principal");

            entity.HasIndex(e => e.SchoolId, "UQ__Principa__3DA4677A8BA78226").IsUnique();

            entity.Property(e => e.PrincipalId)
                .ValueGeneratedNever()
                .HasColumnName("PrincipalID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.School).WithOne(p => p.Principal)
                .HasForeignKey<Principal>(d => d.SchoolId)
                .HasConstraintName("FK_Principal_HighSchool");

            entity.HasOne(d => d.User).WithMany(p => p.Principals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Principal_User");
        });

        modelBuilder.Entity<RegisteredSchool>(entity =>
        {
            entity.HasKey(e => e.RegisteredId);

            entity.ToTable("RegisteredSchool");

            entity.Property(e => e.RegisteredId).HasColumnName("RegisteredID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RegisteredDate).HasColumnType("date");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.School).WithMany(p => p.RegisteredSchools)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredSchool_HighSchool");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SchoolConfig>(entity =>
        {
            entity.HasKey(e => e.ConfigId);

            entity.ToTable("SchoolConfig");

            entity.Property(e => e.ConfigId).HasColumnName("ConfigID");
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.School).WithMany(p => p.SchoolConfigs)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolConfig_HighSchool");
        });

        modelBuilder.Entity<SchoolYear>(entity =>
        {
            entity.ToTable("SchoolYear");

            entity.Property(e => e.SchoolYearId).HasColumnName("SchoolYearID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.School).WithMany(p => p.SchoolYears)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolYear_HighSchool");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.School).WithMany(p => p.Students)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_HighSchool");
        });

        modelBuilder.Entity<StudentInClass>(entity =>
        {
            entity.ToTable("StudentInClass");

            entity.Property(e => e.StudentInClassId).HasColumnName("StudentInClassID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.EnrollDate).HasColumnType("date");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentInClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentInClass_Class");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentInClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentInClass_Student");

            entity.HasMany(d => d.Violations).WithMany(p => p.StudentInClasses)
                .UsingEntity<Dictionary<string, object>>(
                    "ViolationReport",
                    r => r.HasOne<Violation>().WithMany()
                        .HasForeignKey("ViolationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ViolationReport_Violation"),
                    l => l.HasOne<StudentInClass>().WithMany()
                        .HasForeignKey("StudentInClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ViolationReport_StudentInClass"),
                    j =>
                    {
                        j.HasKey("StudentInClassId", "ViolationId");
                        j.ToTable("ViolationReport");
                        j.IndexerProperty<int>("StudentInClassId").HasColumnName("StudentInClassID");
                        j.IndexerProperty<int>("ViolationId").HasColumnName("ViolationID");
                    });
        });

        modelBuilder.Entity<StudentSupervisor>(entity =>
        {
            entity.ToTable("StudentSupervisor");

            entity.Property(e => e.StudentSupervisorId).HasColumnName("StudentSupervisorID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.StudentSupervisors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSupervisor_User");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.School).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_HighSchool");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_User");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.ToTable("Time");

            entity.Property(e => e.TimeId).HasColumnName("TimeID");
            entity.Property(e => e.ClassGroupId).HasColumnName("ClassGroupID");
            entity.Property(e => e.Time1).HasColumnName("Time");

            entity.HasOne(d => d.ClassGroup).WithMany(p => p.Times)
                .HasForeignKey(d => d.ClassGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Time_ClassGroup");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Entity");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(70);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.ToTable("Violation");

            entity.Property(e => e.ViolationId).HasColumnName("ViolationID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("date");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.UpdatedAt).HasColumnType("date");
            entity.Property(e => e.ViolationTypeId).HasColumnName("ViolationTypeID");

            entity.HasOne(d => d.Class).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Violation_Class");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Violations)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK_Violation_Teacher");

            entity.HasOne(d => d.ViolationType).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ViolationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Violation_ViolationType");
        });

        modelBuilder.Entity<ViolationConfig>(entity =>
        {
            entity.ToTable("ViolationConfig");

            entity.Property(e => e.ViolationConfigId).HasColumnName("ViolationConfigID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EvaluationId).HasColumnName("EvaluationID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ViolationTypeId).HasColumnName("ViolationTypeID");

            entity.HasOne(d => d.Evaluation).WithMany(p => p.ViolationConfigs)
                .HasForeignKey(d => d.EvaluationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViolationConfig_Evaluation");

            entity.HasOne(d => d.ViolationType).WithMany(p => p.ViolationConfigs)
                .HasForeignKey(d => d.ViolationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViolationConfig_ViolationType");
        });

        modelBuilder.Entity<ViolationGroup>(entity =>
        {
            entity.ToTable("ViolationGroup");

            entity.Property(e => e.ViolationGroupId).HasColumnName("ViolationGroupID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ViolationType>(entity =>
        {
            entity.ToTable("ViolationType");

            entity.Property(e => e.ViolationTypeId).HasColumnName("ViolationTypeID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ViolationGroupId).HasColumnName("ViolationGroupID");

            entity.HasOne(d => d.ViolationGroup).WithMany(p => p.ViolationTypes)
                .HasForeignKey(d => d.ViolationGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViolationType_ViolationGroup");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
