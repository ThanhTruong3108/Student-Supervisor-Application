using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.UserRequest
{
    public class RequestOfUser
    {
        public RequestOfUser(int schoolAdminId, byte roleId, string code, string name, string phone, string password, string address)
        {
            SchoolAdminId = schoolAdminId;
            RoleId = roleId;
            Code = code;
            Name = name;
            Phone = phone;
            Password = password;
            Address = address;
        }

        [Required(ErrorMessage = "The SchoolAdminId field is required.")]
        public int? SchoolAdminId { get; set; }
        [Required(ErrorMessage = "The RoleId field is required.")]
        public byte RoleId { get; set; }
        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "The Phone field is required.")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "The Address field is required.")]
        public string? Address { get; set; }
    }
}
