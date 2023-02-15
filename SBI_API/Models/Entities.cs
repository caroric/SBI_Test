using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SBI_API.Models
{
    public class ServerPost
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public ServerPost() { }

        public Salida getSalida(ServerPost post)
        {
            Mapper mapper = new Mapper(this.setMapper());
            return mapper.Map<ServerPost, Salida>(post);
        }

        private MapperConfiguration setMapper()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<ServerPost, Salida>()
                .ForMember(salida => salida.Id, server => server.MapFrom(s => s.id));

                c.CreateMap<ServerPost, Salida>()
                .ForMember(salida => salida.Titulo, server => server.MapFrom(s => s.title));
            });
        }
    }

    public class Salida
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
    }

    public interface IServicio
    {
        List<ServerPost> deserializarJSON();
    }

    public class Request: MediatR.IRequest<List<ServerPost>> { }

    public class RequestHandler: MediatR.IRequestHandler<Request, List<ServerPost>> 
    {
        private readonly IServicio _servicio;
        public RequestHandler(IServicio servicio)
        {
            _servicio = servicio;
        }
        public Task<List<ServerPost>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_servicio.deserializarJSON());
        }
    }

    public class Servicio: IServicio
    {
        public List<ServerPost> deserializarJSON()
        {
            string json = new WebClient().DownloadString("https://jsonplaceholder.typicode.com/posts");
            List<ServerPost> posts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServerPost>>(json);
            return posts;
        }
    }


}
