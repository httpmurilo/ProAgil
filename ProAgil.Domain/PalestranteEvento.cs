using ProAgil.Domain;

namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        //N:N
        public int PalestranteId { get; set; }
        public Palestrante Palestrante{get;set;}
        public int EventoId {get;set;}
        public Evento Evento {get;set;}
    }
}