using System.Threading.Tasks;
using ProaAgil.Repository.Data;
using ProAgil.Domain;
using ProAgil.Repository.Interface;

namespace ProAgil.Repository.Repository
{
    public class RepositoryBase : IRepository
    {
        public DataContext _contexto { get; }

        public RepositoryBase(DataContext contexto)
        {
            _contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetAllEventoAsync(bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetAllPalestranteAsyncByName(bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

  
    }
}