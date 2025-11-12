
using System.Text;

namespace basket.tests;

public class UnitTest1
{
    [Fact]
    public void EmptyBasketsGenerateBlankReceipt()
    {

        var basket = new Basket();

        var receipt = basket.GenerateReceipt();

        Assert.Equal("No items", receipt);

    }

    [Fact]
    public void BasketWithOneItemGeneratesReceipt()
    {

        var basket = new Basket();
        basket.ScanItem("Milk");
        var receipt = basket.GenerateReceipt();

        Assert.Equal("""
        1) Milk. 1 @ £1.50 = £1.50
        Total: £1.50
        """, receipt);
    }

    [Fact]
    public void BasketWithTwoMilkGeneratesReceipt()
    {

        var basket = new Basket();
        basket.ScanItem("Milk");
        basket.ScanItem("Milk");
        var receipt = basket.GenerateReceipt();

        Assert.Equal("""
        1) Milk. 2 @ £1.50 = £3.00
        Total: £3.00
        """, receipt);
    }

    [Fact]
    public void BasketCanContainAMixtureOfItems()
    {

        var basket = new Basket();
        basket.ScanItem("Milk");
        basket.ScanItem("Bread");
        basket.ScanItem("Milk");
        var receipt = basket.GenerateReceipt();

        Assert.Equal("""
        1) Milk. 2 @ £1.50 = £3.00
        2) Bread. 1 @ £1.00 = £1.00
        Total: £4.00
        """, receipt);
    }

    [Fact]
    public void ThreeForTwoDiscountIsApplied()
    {

        var basket = new Basket();
        basket.ScanItem("Milk");
        basket.ScanItem("Milk");
        basket.ScanItem("Milk");
        var receipt = basket.GenerateReceipt();

        Assert.Equal("""
        1) Milk. 3 @ £1.50 = £4.50
        Discount £1.50 3For2
        Total: £3.00
        """, receipt);
    }

    [Theory]
    [InlineData(4, "milk", 1.5)]
    [InlineData(5, "milk", 1.5)]
    [InlineData(6, "milk", 1.5)]
    [InlineData(7, "milk", 1.5)]
    [InlineData(7, "bread", 1.0)]
    public void VerifyThreeForTwoDiscountsApplyCorrectly(int quantity, string item, double itemCost)
    {
        var basket = new Basket();
        for (var i = 0; i < quantity; i++)
            basket.ScanItem(item);
        var receipt = basket.GenerateReceipt();

        StringBuilder expected = new StringBuilder();
        expected.AppendLine($"1) {item}. {quantity} @ {itemCost:C} = {quantity * itemCost:C}");
        var expectedDiscountedItems = quantity/3;
        expected.AppendLine($"Discount {expectedDiscountedItems*itemCost:C} 3For2");
        expected.Append($"Total: {(quantity-expectedDiscountedItems)*itemCost:C}");
        Assert.Equal(expected.ToString(), receipt);
    }
}

