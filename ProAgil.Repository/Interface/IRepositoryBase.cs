using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interface {
    public interface IRepositoryBase
     {
        void Add<T> (T entity) where T : class;
        void Update<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveChangesAsync ();
        Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsync(bool  includePalestrantes);
        Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);
        Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos);
        Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includePalestrantes);
    }
}