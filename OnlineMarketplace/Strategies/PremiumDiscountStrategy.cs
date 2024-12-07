using OnlineMarketplace.Interfaces;
using OnlineMarketplace.Models;

namespace OnlineMarketplace.Strategies
{
    // Класс PremiumDiscountStrategy реализует интерфейс IDiscountStrategy и представляет стратегию расчёта цены с премиальной скидкой.
    internal class PremiumDiscountStrategy : IDiscountStrategy
    {
        // Поле для хранения размера скидки в виде десятичной дроби (например, 0.2 для 20%).
        private decimal _discountRate;

        // Конструктор, принимающий размер скидки и инициализирующий поле _discountRate.
        public PremiumDiscountStrategy(decimal discountRate)
        {
            _discountRate = discountRate; // Установка размера скидки, переданного в конструктор.
        }

        // Реализация метода CalculatePrice, который рассчитывает цену с учётом премиальной скидки.
        public decimal CalculatePrice(Product product, int quantity)
        {
            // Итоговая цена вычисляется следующим образом:
            // 1. Берётся базовая цена продукта (`product.Price`).
            // 2. Вычитается часть, соответствующая скидке (умножение на `(1 - _discountRate)`).
            // 3. Умножается на количество продуктов (`quantity`).
            return product.Price * (1 - _discountRate) * quantity;
        }
    }

}
