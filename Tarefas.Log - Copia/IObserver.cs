namespace Tarefas.Log
{
    public interface IObserver
    {
        void Update(string logMessage);
    }
}