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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            //In real world application scenario, I should inject business object and not the repository.
            //For the simplicity, I used the repository because it just need to save and retrieve data.
            _productRepository = productRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Product> GetAll()
        {
            //I should create a view model for this, but for the simplicity, I am using the data model
            return   _productRepository.Query();
        }

        [HttpGet("{id}")]
        public IEnumerable<Product> Get(string id)
        {
            Guid guid = new Guid(id);
            //I should create a view model for this, but for the simplicity, I am using the data model
            return _productRepository.Query(w => w.Id == guid);
        }

        [HttpGet("[action]")]
        public IEnumerable<Product> GetByName(string name)
        {
            //I should create a view model for this, but for the simplicity, I am using the data model
            return _productRepository.Query(w => w.Name.ToLower().Contains(name.ToLower()));
        }

        [HttpPost]
        public string Post([FromBody] ProductViewModel vm)
        {
            try
            {
                //This can be map using auto mapper
                Product prod = new Product
                {
                    Name = vm.Name,
                    Descriptions = vm.Descriptions,
                    Price = vm.Price
                };

                var productId = _productRepository.Add(prod);

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
            _productRepository.Delete(id.ToString()); ;
        }

        [HttpPut]
        public void Put([FromBody] ProductViewModel vm)
        {
            Product prod = new Product
            {
                Id = new Guid(vm.Id),
                Name = vm.Name,
                Descriptions = vm.Descriptions,
                Price = vm.Price
            };

            _productRepository.Update(prod);
        }
    }
}