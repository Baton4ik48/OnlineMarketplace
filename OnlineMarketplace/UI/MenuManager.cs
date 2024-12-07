using OnlineMarketplace.Models;
using OnlineMarketplace.Observers;
using OnlineMarketplace.Services;

namespace OnlineMarketplace.UI
{
    // Класс MenuManager управляет основным меню приложения, где пользователь может выполнять различные операции.
    internal class MenuManager
    {
        // Метод Start() представляет собой основной цикл программы.
        public void Start(Manager manager, List<Customer> customers, List<Product> products, Transaction transaction, Warehouse warehouse)
        {
            while (true)
            {
                // Показать главное меню
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Показать склад");
                Console.WriteLine("2. Добавить товар на склад");
                Console.WriteLine("3. Оформить заказ");
                Console.WriteLine("4. Выйти");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                // Операции в зависимости от выбора пользователя
                switch (choice)
                {
                    case "1":
                        warehouse.ShowAllProducts(); // Показать все товары на складе
                        break;
                    case "2":
                        AddStockMenu(manager, products); // Меню добавления товара на склад
                        break;
                    case "3":
                        MakeOrderMenu(manager, customers, products, transaction); // Меню оформления заказа
                        break;
                    case "4":
                        Console.WriteLine("Выход из программы...");
                        return;
                    // Некорректный ввод
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова."); 
                        break;
                }
            }
        }

        // Меню для добавления товара на склад
        static void AddStockMenu(Manager manager, List<Product> products)
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ ТОВАРА НА СКЛАД ===");
            Console.WriteLine("Список доступных товаров:");

            // Показать все доступные товары
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name}");
            }

            // Получить выбор товара
            Console.Write("Введите номер товара: ");
            if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count)
            {
                Console.Write("Введите количество для добавления: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    manager.AddProductStock(products[productIndex - 1], quantity); // Добавить товар на склад
                }
                else
                {
                    Console.WriteLine("Некорректное количество.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный выбор товара.");
            }
        }

        // Меню для оформления заказа
        static void MakeOrderMenu(Manager manager, List<Customer> customers, List<Product> products, Transaction transaction)
        {
            Console.WriteLine("\n=== ОФОРМЛЕНИЕ ЗАКАЗА ===");
            Console.WriteLine("Список клиентов:");

            // Показать всех клиентов
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].Name} - Баланс: {customers[i].Balance}$");
            }

            // Получить выбор клиента
            Console.Write("Выберите клиента: ");
            if (!int.TryParse(Console.ReadLine(), out int customerIndex) || customerIndex <= 0 || customerIndex > customers.Count)
            {
                Console.WriteLine("Некорректный выбор клиента."); 
                return;
            }

            Customer selectedCustomer = customers[customerIndex - 1];

            // Показать доступные товары
            Console.WriteLine("Список доступных товаров:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name}");
            }

            // Получить выбор товара
            Console.Write("Выберите товар: ");
            if (!int.TryParse(Console.ReadLine(), out int productIndex) || productIndex <= 0 || productIndex > products.Count)
            {
                Console.WriteLine("Некорректный выбор товара."); 
                return;
            }

            Product selectedProduct = products[productIndex - 1];

            // Получить количество
            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Некорректное количество.");
                return;
            }

            try
            {
                // Оформление заказа
                Order order = selectedCustomer.MakeOrder(selectedProduct, quantity);

                // Создание наблюдателей для менеджера и клиента
                var managerNotifier = new ManagerNotifier(manager);
                var customerNotifier = new CustomerNotifier();

                // Подписка на уведомления об изменении статуса заказа
                order.AddObserver(managerNotifier);
                order.AddObserver(customerNotifier);

                // Подтверждение заказа менеджером
                manager.ConfirmOrder(order, transaction);

                // Отписка от ордера, можно реализовать, код по другому,
                // кому интересны уведомления по ордеру, он подписывается на них (AddObserver), а кому не интересно отписывается (RemoveObserver)
                //order.RemoveObserver(managerNotifier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

}
