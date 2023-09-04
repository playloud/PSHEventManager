namespace PSHEventManager.EventManager.Model;

public interface IJwtAuth
{
    Task<string> Authenticate(string username, string password);
}