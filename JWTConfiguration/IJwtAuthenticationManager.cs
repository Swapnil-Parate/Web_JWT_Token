using WebAPI_With_JWT.Models;

namespace WebAPI_With_JWT.JWTConfiguration
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(EmployeeCred credentials);
    }
}