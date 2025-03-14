public interface IRecursoRepository
{
    Task<IEnumerable<Recurso>> GetAll();
    Task<Recurso> GetById(int id);
    Task<Recurso> Add(Recurso recurso);
    Task<bool> Delete(int id);
}