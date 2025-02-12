using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover validation, item addition, and total amount calculation.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a sale is created with valid data, it should be valid.
    /// </summary>
    [Fact(DisplayName = "Sale should be valid when created with valid data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.IsCancelled = false;
        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that when a sale is created with invalid data, it should be invalid.
    /// </summary>
    [Fact(DisplayName = "Sale should be invalid when created with invalid data")]
    public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = new Sale
        {
            SaleNumber = SaleTestData.GenerateInvalidSaleNumber(), // Invalid sale number
            CreatedAt = SaleTestData.GenerateInvalidCreatedAt(), // Invalid created date (future date)
            Items = SaleTestData.GenerateInvalidSaleItems() // Invalid items
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that when an item is added to a sale, the sale's item count increases.
    /// </summary>
    [Fact(DisplayName = "Sale should increase item count when a new item is added")]
    public void Given_Sale_When_ItemAdded_Then_ItemCountShouldIncrease()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var initialItemCount = sale.Items.Count;

        // Act
        sale.AddItem(new SaleItem
        {
            ProductId = "NewProductId", // Example product ID
            Quantity = 2,
            UnitPrice = 100m
        });

        // Assert
        Assert.Equal(initialItemCount + 1, sale.Items.Count);
    }

    /// <summary>
    /// Tests that when a sale's total amount is updated after item addition, the total amount reflects the new value.
    /// </summary>
    [Fact(DisplayName = "Sale's total amount should update correctly after adding items")]
    public void Given_Sale_When_ItemAdded_Then_TotalAmountShouldUpdate()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var initialTotalAmount = sale.TotalAmount;

        var newItem = new SaleItem
        {
            ProductId = "NewProductId",
            Quantity = 2,
            UnitPrice = 150m // Example unit price
        };

        // Act
        sale.AddItem(newItem);

        // Assert
        Assert.True(sale.TotalAmount > initialTotalAmount);
        Assert.Equal(initialTotalAmount + (newItem.Quantity * newItem.UnitPrice), sale.TotalAmount);
    }

    /// <summary>
    /// Tests that the sale's validation fails when invalid items are added.
    /// </summary>
    [Fact(DisplayName = "Sale validation should fail when invalid items are added")]
    public void Given_Sale_When_InvalidItemsAdded_Then_ValidationShouldFail()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.IsCancelled = false;

        var invalidItem = new SaleItem
        {
            ProductId = "", // Invalid product ID
            Quantity = -1,  // Invalid quantity
            UnitPrice = 0   // Invalid unit price
        };

        sale.AddItem(invalidItem);

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
