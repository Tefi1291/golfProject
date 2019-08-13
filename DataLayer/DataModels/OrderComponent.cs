using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.DataModels
{
    public class OrderComponent : IOrderComponent
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Component Component { get; set; }
        public int ComponentQuantity { get; set; }
    }

    public interface IOrderComponent
    {
        int Id { get; set; }
        //FK to OrderId
        Order Order { get; set; }
        //FK to ComponentId
        Component Component { get; set; }
        //Quantity of the component for the order
        int ComponentQuantity { get; set; }
    }
}
