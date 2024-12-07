using OnlineMarketplace.Models;

namespace OnlineMarketplace.Services
{
    // Класс Transaction отвечает за проведение транзакции (оплаты) заказа.
    internal class Transaction
    {
        // Метод ProcessOrderPayment обрабатывает оплату заказа клиентом и передачу средств менеджеру.
        public void ProcessOrderPayment(Order order, Manager manager)
        {
            // Проверка, что заказ не является null.
            if (order == null)
                // Исключение выбрасывается, если заказ не передан.
                throw new ArgumentNullException(nameof(order), "Order не может быть null.");

            // Получаем клиента, связанного с заказом.
            Customer customer = order.Customer;

            // Проверяем, может ли клиент оплатить заказ, исходя из его текущего баланса.
            if (!customer.CanAfford(order.totalCost))
                // Если денег недостаточно, выбрасывается исключение.
                throw new InvalidOperationException("Недостаточно средств для оплаты заказа.");

            // Уменьшаем баланс клиента на сумму заказа.
            customer.DecreaseBalance(order);

            // Увеличиваем баланс менеджера на сумму заказа.
            manager.IncreaseBalance(order);

            // Сообщение об успешной оплате заказа с указанием имени клиента, суммы и баланса менеджера.
            Console.WriteLine($"\nОплата успешно проведена: {customer.Name} оплатил(а) {order.totalCost} $. " +
                $"Баланс менеджера {manager.Name} теперь: {manager.Balance} $.");
        }
    }
}
