using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketplace.Models
{
    // Класс Warehouse представляет собой склад, где можно хранить и управлять товарами.
    internal class Warehouse
    {
        // Словарь для хранения количества товаров на складе.
        // Ключ - продукт (Product), значение - количество (int).
        private Dictionary<Product, int> _stock = new Dictionary<Product, int>();

        // Метод добавляет количество товара на склад.
        public void AddStock(Product product, int quantity)
        {
            // Проверка, что добавляемое количество больше нуля.
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть больше нуля.");

            // Если товар уже есть на складе, то увеличиваем его количество.
            if (_stock.ContainsKey(product))
            {
                _stock[product] += quantity;
            }
            else
            {
                // Если товара нет на складе, добавляем его и устанавливаем начальное количество.
                _stock[product] = quantity;
            }

            // Вывод информации о добавлении товара на склад.
            Console.WriteLine($"На склад добавлено {quantity} ед. товара {product.Name}. Всего на складе: {_stock[product]} ед.");
        }

        // Метод проверяет доступность требуемого количества товара на складе.
        public bool CheckAvailability(Product product, int requestedQuantity)
        {
            // Попробовать получить количество товара и сравнить его с необходимым количеством.
            return _stock.TryGetValue(product, out int availableQuantity) && availableQuantity >= requestedQuantity;
        }

        // Метод уменьшает количество товара на складе.
        public void ReduceStock(Product product, int quantity)
        {
            // Проверка, есть ли на складе достаточно товара.
            if (!CheckAvailability(product, quantity))
                throw new InvalidOperationException("Недостаточно товара на складе.");

            // Уменьшение количества товара на складе.
            _stock[product] -= quantity;

            // Если количество товара на складе уменьшилось до нуля, удаляем его из словаря.
            if (_stock[product] == 0)
            {
                _stock.Remove(product);
            }

            // Вывод информации о списании товара со склада.
            Console.WriteLine($"Со склада списано {quantity} ед. товара {product.Name}. Осталось: {_stock.GetValueOrDefault(product, 0)} ед.");
        }

        // Метод выводит информацию обо всех продуктах на складе.
        public void ShowAllProducts()
        {
            // Проверка, есть ли продукты на складе.
            if (_stock.Count == 0)
            {
                Console.WriteLine("Склад пуст.");
                return;
            }

            // Вывод заголовка списка товаров.
            Console.WriteLine("\nТовары на складе:");

            // Перебор всех товаров в словаре и вывод их описания и количества.
            foreach (var item in _stock)
            {
                item.Key.OutputDescriptionProduct(); // Вызов метода OutputDescriptionProduct для вывода информации о продукте из класса Product.
                Console.WriteLine($"Кол-во на складе: {item.Value} ед."); // Вывод количества товара.
            }
        }
    }

}
