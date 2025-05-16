using AutoMapper;
using CoreContato.DTOs;
using CoreContato.Models;

namespace CoreContato.Mappings
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<ContatoDTO, Contato>();
            CreateMap<Contato, ContatoDTO>();
        }
    }
}
