using System.Linq;
using AutoMapper;
using ProAgil.Domain.Dtos;

namespace ProAgil.Domain.Profiles {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () 
        {
            //para cada palestrante eventos pegue o palestrante N:N
            CreateMap<Evento, EventoDto> ()
                .ForMember (dest => dest.Palestrantes, opt => {
                    opt.MapFrom (src => src.PalestranteEventos.Select (x => x.Palestrante).ToList ());
                })
                .ReverseMap ();

            CreateMap<Palestrante, PalestranteDto> ()
                .ForMember (dest => dest.Eventos, opt => {
                    opt.MapFrom (src => src.PalestranteEventos.Select (x => x.Evento).ToList ());
                })
                .ReverseMap ();

            CreateMap<Lote, LoteDto> ().ReverseMap ();
            CreateMap<RedeSocial, RedeSocialDto> ().ReverseMap ();
        }
    }
}