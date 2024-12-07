using OnlineMarketplace.Interfaces;
using OnlineMarketplace.Models;

namespace OnlineMarketplace.Strategies
{
    // Класс NoDiscountStrategy реализует интерфейс IDiscountStrategy для сценария без скидки.
    internal class NoDiscountStrategy : IDiscountStrategy
    {
        // Реализация метода CalculatePrice, который возвращает цену без применения скидки.
        public decimal CalculatePrice(Product product, int quantity)
        {
            // Рассчитываем итоговую стоимость как цену продукта, умноженную на количество.
            return product.Price * quantity;
        }
    }
}
