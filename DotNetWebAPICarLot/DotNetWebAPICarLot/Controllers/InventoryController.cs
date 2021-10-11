using AutoMapper;
using DotNetEFAutoLot.DAL.Models;
using DotNetEFAutoLot.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DotNetWebAPICarLot.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        private readonly Mapper mapper;
        private readonly InventoryRepository inventoryRepository = new InventoryRepository();


        public InventoryController()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Inventory, Inventory>().ForMember(x => x.Orders, opt => opt.Ignore())
            );

            mapper = new Mapper(config);
        }

        [HttpGet, Route("")]
        public IEnumerable<Inventory> GetInventory()
        {
            var list = inventoryRepository.GetAll();
            var inventories = mapper.Map<List<Inventory>, List<Inventory>>(list);

            return inventories;
        }

        [HttpGet, Route("{id}", Name = "DisplayRoute")]
        public async Task<IHttpActionResult> GetInventory(int id)
        {
            var inventory = inventoryRepository.GetOne(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Inventory, Inventory>(inventory));
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, Inventory inventory)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.Id)
            {
                return BadRequest();
            }

            try
            {
                inventoryRepository.Save(inventory);

            }
            catch(Exception ex)
            {
                // todo: handel exception
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /*
        [HttpGet, Route("")]
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

        [HttpGet, Route("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                inventoryRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}