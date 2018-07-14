using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace E_Commerce.Models

{
    public class Product
    {
        [Key]
        public long product_id { get; set; }

        [Required]
        [Display(Name= "Product Name")]
        [MinLength(2, ErrorMessage="Product name must be more than 4 characters")]
        public string product_name { get; set; }

        [Required]
        [Display(Name= "Initial Quantity")]
        [Range(1, 1000, 
        ErrorMessage = "Quantity has to be more than 0.")]
        public int product_quantity { get; set; }

        [Required]
        [Display(Name= "Product Description")]
        [MinLength(2, ErrorMessage="Product description must be more than 4 characters")]
        public string product_description { get; set; }

        [Required]
        [Display(Name= "Product Image")]
        public string product_image { get; set; }


        
        public List<Order> product_orders { get; set; }
       
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Product()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

    }
}