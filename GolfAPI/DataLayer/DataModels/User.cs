using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfAPI.DataLayer.DataModels
{
    public class User : IUser
    {
        public User()
        {

        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(16)]
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public enum RoleEnum
    {
        Manager = 0,
        Staff = 1,
    }

    public interface IUser
    {
        int Id { get; set; }

        Guid Guid { get; set; }

        string Username { get; set; }

        string Firstname { get; set; }

        string Lastname { get; set; }

        string Password { get; set; }

        RoleEnum Role { get; set; }

        ICollection<Order> Orders { get; set; }
    }
}
