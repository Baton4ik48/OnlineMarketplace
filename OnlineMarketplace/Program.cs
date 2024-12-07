using OnlineMarketplace.Models;
using OnlineMarketplace.Services;
using OnlineMarketplace.Strategies;
using OnlineMarketplace.UI;

namespace OnlineMarketplace
{
    // Определение основного класса программы
    class Program
    {
        // Точка входа в программу
        static void Main(string[] args)
        {
            // Создаем объект склада (Warehouse), который будет хранить информацию о продуктах и их количестве
            Warehouse warehouse = new Warehouse();

            // Создаем менеджера (Manager), передавая его ID, баланс денег, имя и ссылку на склад
            Manager manager = new Manager(1, 0m, "Alice", warehouse);

            // Создаем объект транзакции (Transaction), который будет использоваться для обработки покупок
            Transaction transaction = new Transaction();

            // Создаем список покупателей (Customer)
            List<Customer> customers = new List<Customer>
        {
            // Создаем первого покупателя без скидки
            new Customer(1, "Иван", 10000, "ivan@example.com", new NoDiscountStrategy()),

            // Создаем второго покупателя с премиальной скидкой 20%
            new Customer(2, "Анна", 20000, "anna@example.com", new PremiumDiscountStrategy(0.2m))
        };

            // Создаем список продуктов (Product)
            List<Product> products = new List<Product>
        {
            new Product(113, "Lenovo Laptop", 500m),
            new Product(114, "iPhone", 1000m),
            new Product(115, "LG TV", 900m)
        };

            // Добавляем продукты на склад через менеджера (Manager)
            manager.AddProductStock(products[0], 10);
            manager.AddProductStock(products[1], 10);
            manager.AddProductStock(products[2], 10);

            // Создаем объект MenuManager, который управляет взаимодействием с пользователем через меню
            MenuManager menuManager = new MenuManager();

            // Запускаем меню, передавая менеджера, список покупателей, продуктов, объект транзакции и склад
            menuManager.Start(manager, customers, products, transaction, warehouse);
        }
    }



}
