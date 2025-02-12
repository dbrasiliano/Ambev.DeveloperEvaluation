using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for sales to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleNumber (random sale number)
    /// - BranchId (random alphaNumeric)
    /// - CustomerId (random alphaNumeric)
    /// - TotalAmount (random decimal value)
    /// - CreatedAt (random date)
    /// - Items (valid sale items)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.SaleNumber, f => f.Commerce.Ean13())
        .RuleFor(s => s.BranchId, f => f.Random.AlphaNumeric(8))
        .RuleFor(s => s.CustomerId, f => f.Random.AlphaNumeric(7))
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(100, 10000))
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.IsCancelled, f => f.PickRandomParam([true, true, false, true, false, false, true]));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        var sale = SaleFaker.Generate();
        sale.Items = GenerateValidSaleItems(); // Add valid items to the sale
        return sale;
    }

    /// <summary>
    /// Generates valid sale items.
    /// The generated items will:
    /// - Have valid product ids, quantities, and unit prices
    /// </summary>
    /// <returns>A list of valid sale items.</returns>
    public static List<SaleItem> GenerateValidSaleItems(int itemCount = 3)
    {
        var itemFaker = new Faker<SaleItem>()
            .RuleFor(i => i.ProductId, f => f.Commerce.Product())  // Valid product ID
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 5))  // Valid quantity between 1 and 5
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(10, 500));  // Valid unit price between 10 and 500

        return itemFaker.Generate(itemCount);  // Generate a list of sale items
    }

    /// <summary>
    /// Generates a valid sale number using Faker.
    /// The generated sale number will:
    /// - Follow the standard EAN13 format
    /// </summary>
    /// <returns>A valid sale number.</returns>
    public static string GenerateValidSaleNumber()
    {
        return new Faker().Commerce.Ean13();
    }

    /// <summary>
    /// Generates a valid total amount for a sale.
    /// The generated amount will:
    /// - Be a random decimal between 100 and 10000
    /// </summary>
    /// <returns>A valid total amount.</returns>
    public static decimal GenerateValidTotalAmount()
    {
        return new Faker().Finance.Amount(100, 10000);
    }

    /// <summary>
    /// Generates a valid created date for a sale.
    /// The generated date will:
    /// - Be a random date within the past year
    /// </summary>
    /// <returns>A valid created date.</returns>
    public static DateTime GenerateValidCreatedAt()
    {
        return new Faker().Date.Past(1);
    }

    /// <summary>
    /// Generates an invalid sale number for testing negative scenarios.
    /// The generated sale number will:
    /// - Not follow the standard EAN13 format
    /// </summary>
    /// <returns>An invalid sale number.</returns>
    public static string GenerateInvalidSaleNumber()
    {
        return new Faker().Lorem.Word();  // Not a valid EAN13 format
    }

    /// <summary>
    /// Generates an invalid total amount for testing negative scenarios.
    /// The generated amount will:
    /// - Be less than or equal to 0
    /// </summary>
    /// <returns>An invalid total amount.</returns>
    public static decimal GenerateInvalidTotalAmount()
    {
        return new Faker().Random.Int(int.MinValue, 0);  // Invalid negative or zero value
    }

    /// <summary>
    /// Generates an invalid created date for testing negative scenarios.
    /// The generated date will:
    /// - Be in the future
    /// </summary>
    /// <returns>An invalid created date.</returns>
    public static DateTime GenerateInvalidCreatedAt()
    {
        return DateTime.UtcNow.AddDays(1);  // Future date is invalid for sale creation
    }

    /// <summary>
    /// Generates invalid sale items for testing negative scenarios.
    /// The generated items will:
    /// - Have invalid values for product ID, quantity, and unit price
    /// </summary>
    /// <returns>A list of invalid sale items.</returns>
    public static List<SaleItem> GenerateInvalidSaleItems(int itemCount = 3)
    {
        var itemFaker = new Faker<SaleItem>()
            .RuleFor(i => i.ProductId, f => f.Lorem.Word())  // Invalid product ID
            .RuleFor(i => i.Quantity, f => f.Random.Int(-5, 0))  // Invalid negative or zero quantity
            .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(-100, 0));  // Invalid negative or zero unit price

        return itemFaker.Generate(itemCount);
    }
}
