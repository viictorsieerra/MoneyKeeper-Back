public class RecursoService
{
    private readonly IRecursoRepository _repository;

    public RecursoService(IRecursoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Recurso>> GetAll() => await _repository.GetAll();

    public async Task<Recurso> Add(RecursoDTO recursoDTO)
    {
        var recurso = new Recurso
        {
            Nombre = recursoDTO.Nombre,
            Descripcion = recursoDTO.Descripcion,
            Precio = recursoDTO.Precio
        };
        return await _repository.Add(recurso);
    }

    public async Task<bool> Delete(int id) => await _repository.Delete(id);
}