using OnlineMarketplace.Models;

namespace OnlineMarketplace.Interfaces
{
    // Определение интерфейса IObserver, который задаёт метод для получения уведомлений об изменениях.
    internal interface IObserver
    {
        // Метод, вызываемый для передачи обновлений, связанных с объектом типа Order.
        void Update(Order order);
    }
}
