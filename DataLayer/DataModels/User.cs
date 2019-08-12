using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataModels
{
    public class User : IUser
    {
        public User()
        {

        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public RoleEnum Role { get; set; }

        public ICollection<IOrder> Orders { get; set; }
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

        RoleEnum Role { get; set; }

        ICollection<IOrder> Orders { get; set; }
    }
}
