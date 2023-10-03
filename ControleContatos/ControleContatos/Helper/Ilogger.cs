namespace ControleContatos.Helper
{
    public interface ILogger
    {
        void LogError(Exception ex, string message);
        void LogInformation(string message);
        void LogWarning(string message);
    }
}
