
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
}

