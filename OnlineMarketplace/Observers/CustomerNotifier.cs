using OnlineMarketplace.Interfaces;
using OnlineMarketplace.Models;

namespace OnlineMarketplace.Observers
{
    // Класс CustomerNotifier реализует интерфейс IObserver и отвечает за уведомление клиентов.
    internal class CustomerNotifier : IObserver
    {
        // Реализация метода Update, который выводит сообщение об изменении статуса заказа.
        public void Update(Order order)
        {
            // Вывод уведомления для клиента с использованием данных о заказе.
            Console.WriteLine($"Уведомление для клиента {order.Customer.Name}: статус заказа #{order.Id} изменён на '{order.Status}'.");
        }
    }
}
