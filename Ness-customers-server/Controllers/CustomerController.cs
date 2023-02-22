using Microsoft.AspNetCore.Mvc;
using Ness_customers_server.Data;
using Ness_customers_server.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace Ness_customers_server.Controllers
{
    [ApiController]
    [Route("/[Controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerApiDbContext? dbcustomer;

        public CustomerController(CustomerApiDbContext dbcustomer)
        {
            this.dbcustomer = dbcustomer;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                return Ok(await dbcustomer.Customers.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]      
        public async Task<IActionResult> AddCustomer(Customer newCustomer)
        {
            try
            {
                if (newCustomer != null && validationFuntion(newCustomer))
                {
                    var newcustomer = new Customer()
                    {
                        customerNumber = newCustomer.customerNumber,
                        customerName = newCustomer.customerName,
                        customerType = newCustomer.customerType,
                        address = newCustomer.address,
                        contentPerson = newCustomer.contentPerson,
                        phoneNumber = newCustomer.phoneNumber
                    };
                    await dbcustomer.Customers.AddAsync(newcustomer);
                    await dbcustomer.SaveChangesAsync();

                    return Ok(newcustomer);
                }
                return NotFound();
            }
              catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{customerNumber:int}")]
        public async Task<IActionResult> UppdateCustomer([FromRoute] int customerNumber, Customer updateCustomer)
        {
            try
            {
                var customer = await dbcustomer.Customers.FindAsync(customerNumber);

                if (updateCustomer != null && validationFuntion(updateCustomer))
                {
                    customer.customerNumber = updateCustomer.customerNumber;
                    customer.customerName = updateCustomer.customerName;
                    customer.address = updateCustomer.address;
                    customer.contentPerson = updateCustomer.contentPerson;
                    customer.phoneNumber = updateCustomer.phoneNumber;

                    await dbcustomer.SaveChangesAsync();
                    return Ok(updateCustomer);
                }
                return NotFound();
            }
          catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{customerNumber:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerNumber)
        {
            try
            {
                var Customer = await dbcustomer.Customers.FindAsync(customerNumber);
                if (Customer != null)
                {
                    dbcustomer.Remove(Customer);
                    await dbcustomer.SaveChangesAsync();
                    return Ok(Customer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool validationFuntion(Customer custumer)
        {
            if(string.IsNullOrEmpty(custumer.phoneNumber) || string.IsNullOrEmpty(custumer.customerName) ||
               string.IsNullOrEmpty(custumer.contentPerson) || string.IsNullOrEmpty(custumer.address))
            {
                throw new ArgumentNullException("one or more from the fields is null or empry");
            }
             
            return true;
        }
    }
}
