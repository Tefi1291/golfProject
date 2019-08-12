using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataModels
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

        public int CreatedBy { get; set; }
        public ICollection<IOrderComponent> Components { get; set; }
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
        int CreatedBy { get; set; }
        //
        ICollection<IOrderComponent> Components { get; set; }
    }
}
