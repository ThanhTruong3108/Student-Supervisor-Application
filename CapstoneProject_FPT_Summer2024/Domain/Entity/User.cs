using System;
using System.Collections.Generic;

namespace Domain.Entity;

public partial class User
{
    public int UserId { get; set; }

    public int? SchoolAdminId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public byte RoleId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual SchoolAdmin? SchoolAdmin { get; set; }

    public virtual ICollection<StudentSupervisor> StudentSupervisors { get; set; } = new List<StudentSupervisor>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
