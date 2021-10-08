using AutoMapper;
using DotNetEFAutoLot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DotNetWebAPICarLot.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        private Mapper mapper;


        public InventoryController()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Inventory, Inventory>().ForMember(x => x.Orders, opt => opt.Ignore())
            );

            mapper = new Mapper(config);
        }

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
    }
}