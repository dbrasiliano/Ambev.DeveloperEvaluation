using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios for CreateSaleCommand.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateSaleCommand entities.
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.SaleNumber, f => f.Random.AlphaNumeric(6))
        .RuleFor(c => c.BranchId, f => f.Random.AlphaNumeric(6))
        .RuleFor(c => c.CustomerId, f => f.Random.AlphaNumeric(6))
        .RuleFor(c => c.Items, f => GenerateSaleItems())
        .RuleFor(c => c.IsCancelled, f => f.Random.Bool());

    /// <summary>
    /// Generates a valid CreateSaleCommand entity with randomized data.
    /// </summary>
    /// <returns>A valid CreateSaleCommand entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a list of CreateSaleItemCommand with random valid data.
    /// </summary>
    /// <returns>List of CreateSaleItemCommand</returns>
    private static List<CreateSaleItemCommand> GenerateSaleItems()
    {
        return new Faker<CreateSaleItemCommand>()
            .RuleFor(i => i.ProductId, f => f.Commerce.Ean13())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 500))
            .Generate(3);
    }
}
