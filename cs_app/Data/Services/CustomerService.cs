using cs_app.Data;
using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace cs_app.Data.Services
{
    public class CustomerService
    {
        private EducationContext _context;
        public CustomerService(EducationContext context)
        {
            _context = context;
        }


        //метод создания клиента
        public async Task<Customer?> AddCustomer(CustomerDTO customer)
        {
            Customer ncustomer = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                SecName = customer.SecName,
                Age = customer.Age,
                Doc = customer.Doc
            };
            if (customer.Hotels.Any())
            {
                ncustomer.Hotels = _context.Hotels.ToList().
                    IntersectBy(customer.Tickets, order => order.Id).ToList();
            }
            var result = _context.Customers.Add(ncustomer);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }
        //Метод получения клиента
        public async Task<List<Customer>> GetCustomers()
        {
            var result = await _context.Customers.
                Include(a => a.Hotels).
                Include(b => b.Ticket).
                ToListAsync();
            return await Task.FromResult(result);
        }
        //Метод получения клиента по ID
        public async Task<Customer?> GetCustomers(int id)
        {

            var result = _context.Customers.Include(a => a.Hotels).
                                        Include(b => b.Ticket).
                                        FirstOrDefault(customer => customer.Id == id);

            return await Task.FromResult(result);
        }
        //Метод получения неполной информации об отеле
        public async Task<List<IncompleteCustomerDTO>> GetCustomersIncomplete()
        {
            var customers = await _context.Customers.ToListAsync();
            List<IncompleteCustomerDTO> result = new List<IncompleteCustomerDTO>();
            customers.ForEach(au => result.Add(new IncompleteCustomerDTO
            {
                Id = au.Id,
                Name = au.Name,
                Surname = au.Surname
            }));
            return await Task.FromResult(result);
        }
        //Метод обновления информации об отеле
        public async Task<Customer?> UpdateCustomer(int id, CustomerDTO updatedCustomer)
        {
            var customer = await _context.Customers.
                Include(customer => customer.Hotels).
                Include(b => b.Ticket).
                FirstOrDefaultAsync(au => au.Id == id);

            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.Surname = updatedCustomer.Surname;
                customer.SecName = updatedCustomer.SecName;
                customer.Age = updatedCustomer.Age;
                customer.Doc = updatedCustomer.Doc;
                if (updatedCustomer.Hotels.Any())
                {
                    customer.Hotels = _context.Hotels.ToList().
                        IntersectBy(updatedCustomer.Hotels, orders => orders.Id).
                        ToList();
                }
                if (updatedCustomer.Tickets.Any())
                {
                    customer.Ticket = _context.Tickets.ToList().
                        IntersectBy(updatedCustomer.Tickets, avia => avia.Id).
                        ToList();
                }
                _context.Customers.Update(customer);
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return customer;
            }
            return null;
        }
        //метод удаления отеля
        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(au => au.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}