using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarefas.Log
{
    public class Logger : IObserver
    {
        public void Update(string logMessage)
        {
            string logDirectory = "./log"; // Subdiretório para os logs
            string logFilePath = Path.Combine(logDirectory, "app.log"); // Caminho do arquivo de log

            // Verificar se o diretório de log existe, senão, criá-lo
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Gravar o log no arquivo
            File.AppendAllText(logFilePath, $"{logMessage}\n");
            Console.WriteLine($"Log: {logMessage}");
        }
    }

    public class EventSource
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string logMessage)
        {
            foreach (var observer in observers)
            {
                observer.Update(logMessage);
            }
        }
         
       
    }
}
