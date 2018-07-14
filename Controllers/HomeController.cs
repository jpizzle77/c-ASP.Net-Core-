using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Controllers
{
    
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        

        public static void HelloWorld()
        {
            System.Console.WriteLine("Hello Worldddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            
            HelloWorld();
            IndexView model = new IndexView
            {
                
                _product = _context.products.OrderByDescending(product => product.created_at).Take(5).ToList(),
                _customer = _context.customers.OrderByDescending(customer => customer.created_at).Take(3).ToList(),
                _order = _context.orders
                    .Include(order => order.customer_info)
                    .Include(order => order.product_info)
                    .OrderBy(order => order.created_at)
                    .Take(3)
                    .ToList()
            };
            return View(model);
        }

        [HttpGet("products")]
        public IActionResult Product()
        {
            var products = _context.products.ToList();
            return View(products);
        }

        [HttpGet("orders")]
        public IActionResult Order()
        {
            IndexView order_info = new IndexView();
            order_info.Customers = _context.customers
                                    .Select(selector: customer => new SelectListItem()
                                    {
                                        Value = customer.customer_id.ToString(),
                                        Text = customer.customer_name
                                    }).ToList();

            order_info.Products = _context.products
                                    .Select(selector: product => new SelectListItem()
                                    {
                                        Value = product.product_id.ToString(),
                                        Text = product.product_name
                                    }).ToList();
            order_info.Order = new Order();
            
           /* order_info._customer = _context.customers
                    .Include(customer => customer.customer_orders)
                    .ThenInclude(order => order.product_info)
                    .ToList();*/
             order_info._order = _context.orders
                    .Include(order => order.customer_info)
                    .Include(order => order.product_info)
                    .OrderByDescending(order => order.created_at)
                    .ToList();
          

            return View(order_info);
        }

        [HttpGet("customers")]
        public IActionResult Customer()
        {
            //Customer customers = new Customer();
            var customers = _context.customers.ToList();
            return View(customers);
        }

        [HttpGet("settings")]
        public IActionResult Setting()
        {
            return View();
        }

        [HttpPost("create_customer")]
        public IActionResult CreateCustomer(Customer customer)
        {
             if(ModelState.IsValid)
            {
                Customer new_customer = new Customer()
                {
                    customer_id = customer.customer_id,
                    customer_name = customer.customer_name,

                };
                
            _context.customers.Add(new_customer); // add new customer object to database
            _context.SaveChanges(); 
            return RedirectToAction("Customer");

            }

            return View(customer);
        }


        [HttpPost("create_product")]
        public IActionResult CreateProduct(Product product)
        {
             if(ModelState.IsValid)
            {
                Product new_product = new Product()
                {
                    product_id = product.product_id,
                    product_name = product.product_name,
                    product_description = product.product_description,
                    product_quantity = product.product_quantity,
                    product_image = product.product_image,


                };
                
            _context.products.Add(new_product); // add new customer object to database
            _context.SaveChanges(); 
            return RedirectToAction("Product");

            }
            IndexView model = new IndexView();
            model.Product = new Product();
            model.Product.product_name = product.product_name;
            model.Product.product_description = product.product_description;
            model.Product.product_quantity = product.product_quantity;

            return View("NewProductForm", product);
        }
        [HttpPost("create_order")]
        public IActionResult CreateOrder(IndexView model)
        {
            
            Customer customer = model.Customer;
            if(customer.customer_id == 0)  //means a customer WAS NOT selected from dropdown list
                {
                   TempData["CustomError"] = "Pick a customer dipshit";
                }

            Product product = model.Product;
            if(product.product_id == 0) //means a product WAS NOT selected from dropdown list
                { 
                    TempData["CustomProductError"] = "Pick a product dipshit";
                    return RedirectToAction("Order");
                }
            Product product_info = new Product();
            product_info = _context.products.Single(p => p.product_id == product.product_id); // this query will grab the selected product (from dropdown list)
            // and give access to all the product info including its quantity

             Order new_order = model.Order;
             if(new_order.order_quantity == 0) //means a quantity WAS NOT selected from dropdown list
                { 
                    TempData["CustomOrderError"] = "Pick a quantity dipshit";
                    return RedirectToAction("Order");
                }
             else if(new_order.order_quantity > product_info.product_quantity)
                {
                    TempData["NotEnoughError"] = $"Sorry, there is only {product_info.product_quantity} {product_info.product_name}'s left in stock";
                    return RedirectToAction("Order");
                    
                }
            
            product_info.product_quantity = product_info.product_quantity - new_order.order_quantity; //updating the product quantity
            _context.products.Update(product_info);

            new_order.customer_id= customer.customer_id;
            new_order.product_id = product.product_id;
        
            _context.orders.Add(new_order); // add new customer object to database
            _context.SaveChanges(); 

          
            return RedirectToAction("Order");

         
        }


        [HttpGet("search/customers")]
        public IActionResult SearchCustomers(Customer customer_search)
        {
            List<Customer> customer_match = new List<Customer>();
            customer_match = _context.customers
                             .Include(customer => customer.customer_orders)
                                .ThenInclude(order => order.product_info)
                             .Where(customer => customer.customer_name == customer_search.customer_name).ToList();
             if(customer_match.Count == 0)
             {
                 TempData["NoCustomerError"] = "No customer in the Database";
                 IndexView model = new IndexView
                    {
                        _product = _context.products.OrderByDescending(product => product.created_at).Take(5).ToList(),
                        _customer = _context.customers.OrderByDescending(customer => customer.created_at).Take(3).ToList(),
                        _order = _context.orders
                            .Include(order => order.customer_info)
                            .Include(order => order.product_info)
                            .OrderBy(order => order.created_at)
                            .Take(3)
                            .ToList()
                    };
                    return View("Index", model);
             }
             return View(customer_match);
        }
        [HttpPost("search/peeps")]
        public IActionResult Search_Customers(IndexView model)
        {
           Customer customer_search = model.Customer;

           //customer_search = _context.customers.Single(customer => customer.customer_name == customer_search.customer_name);


            return RedirectToAction("SearchCustomers", customer_search);
        }

        [HttpGet("search/products")]
        public IActionResult SearchProducts(Product product_search)
        {
            List<Product> product_match = new List<Product>();
            product_match = _context.products
                             .Include(product => product.product_orders)
                                .ThenInclude(order => order.customer_info)
                             .Where(product => product.product_name == product_search.product_name).ToList();
             if(product_match.Count == 0)
             {
                 TempData["NoProductError"] = "No product in the Database";
                 IndexView model = new IndexView
                    {
                        _product = _context.products.OrderByDescending(product => product.created_at).Take(5).ToList(),
                        _customer = _context.customers.OrderByDescending(customer => customer.created_at).Take(3).ToList(),
                        _order = _context.orders
                            .Include(order => order.customer_info)
                            .Include(order => order.product_info)
                            .OrderBy(order => order.created_at)
                            .Take(3)
                            .ToList()
                    };
                    return View("Product", model);
             }
             return View(product_match);
        }
        [HttpPost("search/pros")]
        public IActionResult Search_Products(Product model)
        {
           //Product product_search = model.Product;

           //customer_search = _context.customers.Single(customer => customer.customer_name == customer_search.customer_name);


            return RedirectToAction("SearchProducts", model);
        }




    }

        
}
