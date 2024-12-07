using OnlineMarketplace.Services;

namespace OnlineMarketplace.Models
{
    // Класс Manager представляет собой менеджера в системе, который управляет товарами на складе и заказами клиентов.
    internal class Manager
    {
        public string Name { get; private set; }// Свойство для хранения имени менеджера.
        public int Id { get; private set; } // Свойство для хранения идентификатора менеджера.
        private Warehouse _warehouse; // Поле для хранения ссылки на склад.
        public decimal Balance { get; protected set; } // Свойство для хранения баланса менеджера.

        // Конструктор, инициализирующий объект Manager с заданными параметрами.
        public Manager(int id, decimal balance, string name, Warehouse warehouse)
        {
            Id = id; // Инициализация идентификатора менеджера.
            Name = name; // Инициализация имени менеджера.
            Balance = balance; // Инициализация баланса менеджера.
            _warehouse = warehouse; // Инициализация склада, на котором менеджер управляет запасами.
        }

        // Метод для добавления продукта на склад(Принимает продукт и его кол-во).
        public void AddProductStock(Product product, int quantityToAdd)
        {
            _warehouse.AddStock(product, quantityToAdd); // Вызов метода складского объекта для увеличения количества товара.
        }

        // Метод для подтверждения заказа.
        public void ConfirmOrder(Order order, Transaction transactionManager)
        {
            // Проверка доступности продукта на складе.
            if (_warehouse.CheckAvailability(order.Product, order.Quantity))
            {
                // Если товар доступен, процесс оплаты и обновление статуса заказа.
                ProcessPayment(order, transactionManager);

                // Передача на склад в метод товара и кол-ва для уменьшения количества товара на складе.
                _warehouse.ReduceStock(order.Product, order.Quantity);

                // Уведомление об успешном подтверждении заказа.
                UpdateOrderStatus(order, "Готов к выдаче", $"\nМенеджер {Name} подтвердил заказ на {order.Quantity} ед. товара {order.Product.Name}.");
            }
            else
            {
                // Если товара недостаточно, отклонение заказа и уведомление.
                UpdateOrderStatus(order, "Отмена", $"\nМенеджер {Name} отклонил заказ на {order.Quantity} ед. товара {order.Product.Name} из-за недостатка товара.");
            }
        }

        // Метод для обработки оплаты заказа.
        private void ProcessPayment(Order order, Transaction transactionManager)
        {
            transactionManager.ProcessOrderPayment(order, this); // Вызов метода класса Transaction для обработки оплаты.
        }

        // Метод для обновления статуса заказа.
        private void UpdateOrderStatus(Order order, string status, string message)
        {
            Console.WriteLine(message); // Вывод сообщения с детализированной информацией.
            order.UpdateStatus(status); // Обновление статуса заказа.
        }

        // Метод для увеличения баланса менеджера после успешной оплаты заказа.
        public void IncreaseBalance(Order order)
        {
            if (order.totalCost <= 0)
                throw new ArgumentException("Сумма должна быть больше нуля."); // Проверка на допустимость суммы.

            Balance += order.totalCost; // Увеличение баланса на общую стоимость заказа.
        }
    }
}
