using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MediatR;
using SBI_API.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace SBI_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SBI_Test : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;

        public SBI_Test(MediatR.IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET api/<SBI_Test>/5
        [HttpGet]
        public async Task<Salida> Get([FromQuery]int id)
        {
            Salida salida = null;
            try
            {
                List<ServerPost> posts = await this._mediator.Send(new Models.Request());

                if (posts.Count > 0)
                {
                    ServerPost post = posts.Find(p => p.id == id);
                    salida = post.getSalida(post);
                }
            }
            catch (System.Exception ex)
            {
                await Task.Run(() => throw new Exception(ex.ToString()));
            }
            return await Task<Salida>.Factory.StartNew(() => salida);
        }

        // POST api/<SBI_Test>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SBI_Test>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SBI_Test>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
