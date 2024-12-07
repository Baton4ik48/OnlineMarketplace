using OnlineMarketplace.Interfaces;
using OnlineMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketplace.Observers
{
    // Класс ManagerNotifier реализует интерфейс IObserver и уведомляет менеджеров о статусах заказов.
    internal class ManagerNotifier : IObserver
    {
        // Поле для хранения ссылки на менеджера, которому нужно отправлять уведомления.
        private Manager _manager;

        // Конструктор принимает объект Manager и сохраняет его в приватное поле _manager.
        public ManagerNotifier(Manager manager)
        {
            _manager = manager; // Инициализация поля _manager переданного через параметр конструктора объекта Manager.
        }

        // Реализация метода Update для отправки уведомления менеджеру.
        public void Update(Order order)
        {
            // Вывод уведомления для менеджера с использованием данных о заказе.
            Console.WriteLine($"Уведомление для менеджера {_manager.Name}: заказ #{order.Id} в статусе '{order.Status}'.");
        }
    }
}
