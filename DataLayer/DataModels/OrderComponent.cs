using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataModels
{
    public class OrderComponent : IOrderComponent
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ComponentId { get; set; }
        public int ComponentQuantity { get; set; }
    }

    public interface IOrderComponent
    {
        int Id { get; set; }
        //FK to OrderId
        int OrderId { get; set; }
        //FK to ComponentId
        int ComponentId { get; set; }
        //Quantity of the component for the order
        int ComponentQuantity { get; set; }

        
    }
}
