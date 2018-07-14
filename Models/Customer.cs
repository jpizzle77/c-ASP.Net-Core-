using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace E_Commerce.Models

{
    public class Customer
    {
        [Key]
        [Required]
        public long customer_id { get; set; } 

        [Required]
        [Display(Name= "Name")]
        [MinLength(2, ErrorMessage="Name must be more than 4 characters")]
        public string customer_name { get; set; }

        public List<Order> customer_orders { get; set; }


        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Customer()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

    }
}