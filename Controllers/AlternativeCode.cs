//Note: this was for the CreateOrder(). However I went with a <SelectListItem> way which is a different way to 
// display the list that you query. Its much cleaner as you dont need to use a foreach loop to run through the
//list


/*Customer = new Customer(),
Product = new Product(),
Order = new Order(),

_customer = _context.customers
        .Include(customer => customer.customer_orders)
        .ThenInclude(order => order.product_info)
        .ToList(),

_product = _context.products
        .Include(product => product.product_orders)
        .ThenInclude(order => order.customer_info)
        .ToList(),

_order = _context.orders.ToList()*/