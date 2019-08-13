using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GolfAPI.DataLayer.DataModels
{
    public class OrderComponent : IOrderComponent
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Component Component { get; set; }
        public int ComponentQuantity { get; set; }
        public int OrderId { get; set; }
        public int ComponentId { get; set; }
    }

    public interface IOrderComponent
    {
        int Id { get; set; }
        //FK to OrderId
        int OrderId { get; set; }
        Order Order { get; set; }
        //FK to ComponentId
        int ComponentId { get; set; }
        Component Component { get; set; }
        //Quantity of the component for the order
        int ComponentQuantity { get; set; }
    }
}
