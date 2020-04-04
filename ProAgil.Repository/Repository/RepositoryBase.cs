using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProaAgil.Repository.Data;
using ProAgil.Domain;
using ProAgil.Repository.Interface;

namespace ProAgil.Repository.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        public DataContext _contexto { get; }

        public RepositoryBase(DataContext contexto)
        {
            _contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           
             _contexto.Remove(entity);
        }
       public void Update<T>(T entity) where T : class
        {
             _contexto.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
          return  (await _contexto.SaveChangesAsync()) > 0;
        }
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include( x => x.Lotes)
                .Include(x => x.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestrante);
            }
            query = query.OrderByDescending( c => c.DataEvento);
            return await query.ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include( x => x.Lotes)
                .Include(x => x.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestrante);
            }
            query = query.OrderByDescending( c => c.DataEvento)
                .Where( c => c.Tema.Contains(tema));
            return await query.ToArrayAsync();
        }

      

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
              IQueryable<Evento> query = _contexto.Eventos
                .Include( x => x.Lotes)
                .Include(x => x.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestrante);
            }
            query = query.OrderByDescending( c => c.DataEvento)
                .Where( c => c.Id == EventoId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
                .Include(x => x.RedesSociais); 

            if(includeEventos)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Evento);
            }
            query = query.OrderBy( x => x.Nome)
                .Where (x => x.Id == PalestranteId);
                
                    

            return await query.FirstOrDefaultAsync();
        }
         public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includePalestrantes)
        {
            
            IQueryable<Palestrante> query = _contexto.Palestrantes
                .Include(x => x.RedesSociais); 

            if(includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Evento);
            }
            query = query.OrderBy( x => x.Nome)
                .Where (x => x.Nome.ToLower().Contains(name.ToLower()));
                
                    

            return await query.ToArrayAsync();
        }
  
    }
}