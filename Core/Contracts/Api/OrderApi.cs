using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Api
{
    public class OrderApi
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRequired { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
      
    }

}
