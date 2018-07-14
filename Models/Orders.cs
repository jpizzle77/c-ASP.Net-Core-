using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace E_Commerce.Models

{
    public class Order
    {
        [Key]
        [Display(Name = "Customer and product")]
        [Required(ErrorMessage = "{0} is required.")]
        public long order_id { get; set; } 
        
       [Required] public long customer_id { get; set; }

        
        public long product_id { get; set; }
        
        
        [Display(Name = "Product Quantity")]
        [Required(ErrorMessage = "Quantity is required.")]
        public int order_quantity { get; set; }

        public Customer customer_info { get; set; }
        public Product product_info { get; set; }


       

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Order()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

    }
}