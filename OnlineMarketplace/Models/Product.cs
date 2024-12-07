using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketplace.Models
{
    // Класс Product представляет модель продукта с номером(артикл), именем и ценой.
    internal class Product
    {
        public int Number { get; private set; } // Номер(артикл) продукта.
        public string Name { get; private set; } // Наименование продукта.
        public decimal Price { get; private set; } // Цена продукта.

        // Конструктор для инициализации продукта с номером, именем и ценой.
        public Product(int number, string name, decimal price)
        {
            // Инициализация свойств.
            Number = number; // Номер(артикл) продукта.
            Name = name;     // Наименование продукта.
            Price = price;   // Цена продукта.
        }

        // Метод для вывода информации о продукте в консоль.
        public void OutputDescriptionProduct()
        {
            // Форматированный вывод данных о продукте.
            Console.WriteLine($"\nId: {Number}, наименование продукта: {Name}, стоимость : {Price} $");
        }
    }
}
