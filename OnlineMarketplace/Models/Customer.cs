using OnlineMarketplace.Interfaces;

namespace OnlineMarketplace.Models
{
    // Класс Customer представляет клиента, который может делать заказы и имеет баланс, контактные данные и стратегию скидок.
    internal class Customer
    {
        public string Name { get; protected set; } // Имя клиента, доступное только для чтения и изменения из наследников.
        public int Id { get; protected set; } // Уникальный идентификатор клиента, доступный только для чтения и изменения из наследников.
        public decimal Balance { get; protected set; } // Баланс клиента, доступный только для чтения и изменения из наследников.
        public string ContactDetails { get; protected set; } // Контактные данные клиента, доступные только для чтения и изменения из наследников.
        private IDiscountStrategy _discountStrategy; // Приватное поле, содержащее стратегию расчёта скидок для клиента.

        // Конструктор для инициализации объекта клиента с необходимыми данными.
        public Customer(int id, string name, decimal balance, string contactDetails, IDiscountStrategy discountStrategy)
        {
            Id = id; // Устанавливаем уникальный идентификатор.
            Name = name; // Устанавливаем имя клиента.
            Balance = balance; // Устанавливаем начальный баланс клиента.
            ContactDetails = contactDetails; // Устанавливаем контактные данные клиента.
            _discountStrategy = discountStrategy; // Устанавливаем стратегию расчёта скидок.
        }

        // Метод для создания заказа. Клиент указывает продукт и количество.
        public Order MakeOrder(Product product, int quantity)
        {
            // Проверяем, что количество больше нуля. Если нет, выбрасывается исключение.
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть больше нуля.");

            // Используем стратегию скидок для расчёта общей стоимости.
            decimal totalCost = _discountStrategy.CalculatePrice(product, quantity);

            // Выводим информацию о заказе в консоль.
            Console.WriteLine($"\n{Name} заказал {product.Name} в количестве {quantity} ед. по цене {totalCost / quantity} $ за ед.");

            // Создаём и возвращаем новый объект заказа.
            return new Order(this, product, quantity, totalCost);
        }

        // Метод для проверки, может ли клиент позволить себе покупку на указанную сумму.
        public bool CanAfford(decimal totalCost)
        {
            // Возвращаем true, если баланс клиента больше или равен общей стоимости, иначе false.
            return Balance >= totalCost;
        }

        // Метод для уменьшения баланса клиента при оплате заказа.
        public void DecreaseBalance(Order order)
        {
            // Проверяем, что заказ не равен null.
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            // Уменьшаем баланс клиента на сумму заказа.
            Balance -= order.totalCost;
        }
    }
}
