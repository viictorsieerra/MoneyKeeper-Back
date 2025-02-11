namespace Models;

class Categoria{
    public int _idCategoria {get; set;}
    public string _nombre{get; set;}
    public string _descripcion{get; set;}

    public Categoria(){}
    public Categoria (string nombre, string descripcion)
    {
        _nombre = nombre;
        _descripcion = descripcion;
    }
}