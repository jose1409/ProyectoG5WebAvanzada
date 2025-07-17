using API.Models;

namespace API.Repository.AutenticacionRepository
{
    public interface IAutenticacionRepository
    {
        int Register (Autenticacion autenticacion);

        Autenticacion Login(Autenticacion autenticacion);

        Autenticacion RecoverAcces(Autenticacion autenticacion);

        int UpdatePasswordLost(Autenticacion autenticacion, String Contrasenna);
    }
}
