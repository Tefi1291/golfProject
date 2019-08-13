using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataModels
{
    public class Component : IComponent
    {
        public int Id { get; set; }
        public string ComponentCode { get; set; }
        public string Description { get; set; }
        public ICollection<OrderComponent> Orders { get; set; }

        public Component()
        {

        }
    }

    public interface IComponent
    {
        //Primary Key
        int Id { get; set; }
        //
        string ComponentCode { get; set; }
        // Optional description
        string Description { get; set; }
        //
        ICollection<OrderComponent> Orders { get; set; }
    }
}
