using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GolfAPI.DataLayer.DataModels
{
    public class Order : IOrder
    {
        public Order()
        {

        }

        public int Id { get; set ; }
        public string OrderNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRequired { get; set; }
        public string Description { get; set; }
        public ICollection<OrderComponent> Components { get; set; }

        public int UserForeignKey { get; set; }
        public User CreatedBy { get; set; }
    }

    public interface IOrder
    {
        int Id { get; set; }

        string OrderNumber { get; set; }
        //Order creation date
        DateTime DateCreated { get; set; }
        //Order required for
        DateTime DateRequired { get; set; }

        string Description { get; set; }
        //Foreign key to User->Id        
        int UserForeignKey { get; set; }

        User CreatedBy { get; set; }
        //
        ICollection<OrderComponent> Components { get; set; }
    }
}
