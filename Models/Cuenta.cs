using System.Security.Cryptography.X509Certificates;

namespace Models;

class Cuenta{
    public int _idCuenta{get; set;}
    public int _idUsuario {get; set;}
    public decimal _dineroCuenta {get; set;}
    public bool _activa {get; set;}
    public DateTime _fec_Creacion {get; set;} = DateTime.Now;
}