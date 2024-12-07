using OnlineMarketplace.Models;

namespace OnlineMarketplace.Interfaces

{
    // Определение интерфейса IDiscountStrategy, который задаёт метод для расчёта цены с учётом скидки.
    internal interface IDiscountStrategy
    {
        // Метод для расчёта итоговой цены для продукта в зависимости от количества.
        decimal CalculatePrice(Product product, int quantity);
    }
}
