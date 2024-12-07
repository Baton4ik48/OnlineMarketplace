using OnlineMarketplace.Interfaces;

namespace OnlineMarketplace.Models
{
    // Класс Order представляет собой заказ в системе и содержит всю необходимую информацию о заказе.
    internal class Order
    {
        public int Id { get; private set; } // Уникальный идентификатор заказа, который автоматически увеличивается для каждого нового заказа.
        private static int _nextId = 1; // Вспомогательное поле для отслеживания следующего ID для нового заказа,
                                        // поле статическое потому что нужна уникальность, что каждое последующее добавление ордера,
                                        // будет уникальным (такой метод крайне не подходит для многопоточности, но для студ. работы норм).
        public Customer Customer { get; private set; } // Свойство, представляющее заказавшего клиента.
        public Product Product { get; private set; } // Свойство, представляющее продукт, который заказан.
        public int Quantity { get; private set; } // Количество заказанного продукта.
        public string Status { get; private set; } // Статус заказа.
        public decimal totalCost { get; } // Общее значение стоимости заказа.

        // Список наблюдателей, которые хотят получать уведомления об изменении статуса заказа.
        private List<IObserver> _observers = new List<IObserver>();

        // Конструктор класса Order.
        // - `customer` — клиент, который сделал заказ.
        // - `product` — продукт, который был заказан.
        // - `quantity` — количество заказанного продукта.
        // - `price` — цена за весь товар.
        public Order(Customer customer, Product product, int quantity, decimal price)
        {
            Id = _nextId++; // Устанавливаем ID для текущего заказа и увеличиваем(++) на 1 _nextId для следующего заказа.
            Customer = customer; // Связываем заказ с клиентом.
            Product = product; // Связываем заказ с продуктом.
            Quantity = quantity; // Устанавливаем количество заказанного товара.
            totalCost = price; // Общая цена за товар в ордере.
            Status = "В ожидании"; // По умолчанию статус заказа - "в ожидании".
        }

        // Метод добавления наблюдателя к списку наблюдателей.
        public void AddObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer); // Добавляем наблюдателя.
            }
        }

        // Метод удаления наблюдателя из списка наблюдателей.
        public void RemoveObserver(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer); // Удаляем наблюдателя.
            }
        }

        // Метод уведомления всех наблюдателей о смене статуса заказа.
        public void NotifyObservers()
        {
            foreach (var observer in _observers) // Перебираем список наблюдателей.
            {
                try
                {
                    observer.Update(this); // Уведомляем каждого наблюдателя.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при уведомлении наблюдателя: {ex.Message}"); // Логируем ошибку в случае сбоя при уведомлении.
                }
            }
        }

        // Метод для обновления статуса заказа.
        public void UpdateStatus(string status)
        {
            Status = status; // Обновляем статус заказа.
            NotifyObservers(); // Уведомляем всех наблюдателей о смене статуса.
        }
    }

}
