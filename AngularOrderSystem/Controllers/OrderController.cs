using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularOrderSystem.Data.Interfaces;
using AngularOrderSystem.Data.Models;
using AngularOrderSystem.ViewModels;

namespace AngularOrderSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> GetAll()
        {
            //I should create a view model for this, but for the simplicity, I am using the data model
            return _orderRepository.Query();
        }

        [HttpGet("{id}")]
        public IEnumerable<Order> Get(string id)
        {
            Guid guid = new Guid(id);
            //I should create a view model for this, but for the simplicity, I am using the data model
            return _orderRepository.Query(w => w.Id == guid);
        }

        [HttpPost]
        public string Post([FromBody] OrderViewModel vm)
        {
            try
            {
                Order order = new Order
                {
                   Id = Guid.NewGuid(),
                   Description = vm.Description,
                   OrderDate = vm.OrderDate,
                   Products = vm.Products
                };

                _orderRepository.Add(order);

                return "Success";
            }
            catch (Exception)
            {
                return StatusCode(500).ToString();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _orderRepository.Delete(id.ToString());
        }

        [HttpPut]
        public void Put([FromBody] OrderViewModel vm)
        {
            Order prod = new Order
            {
                Id = new Guid(vm.Id),
                Description = vm.Description,
                OrderDate = vm.OrderDate,
                Products = vm.Products
            };

            _orderRepository.Update(prod);
        }
    }
}