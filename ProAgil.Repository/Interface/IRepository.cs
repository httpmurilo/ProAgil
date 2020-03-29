using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interface {
    public interface IRepository
     {
         //GERAL
        void Add<T> (T entity) where T : class;
        void Update<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveChangesAsync ();
        
        //EVENTOS
        Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsync(bool  includePalestrantes);
        Task<Evento[]> GetEventoAsyncById(int EventoId, bool includePalestrantes);

        //PALESTRANTE
        Task<Evento[]> GetAllPalestranteAsyncByName(bool  includePalestrantes);
        Task<Evento[]> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes);
    }
}