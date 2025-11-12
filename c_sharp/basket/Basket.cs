
using System.Runtime.CompilerServices;
using System.Text;

public class Basket
{

    private readonly Dictionary<string, double> stockItems = new Dictionary<string, double>(StringComparer.CurrentCultureIgnoreCase)
    {
        {"milk", 1.5},
        {"bread", 1.0}
    };

    private List<string> items = new List<string>();

    public string GenerateReceipt()
    {
        if (items.Count == 0)
            return "No items";


        var groupedItems = items
            .Select((s, i) => new { Order = i, Item = s })
            .GroupBy(x => x.Item)
            .Select(x => new { ItemName = x.Key, Quantity = x.Count(), Order = x.Min(y => y.Order) })
            .OrderBy(o => o.Order)
            .ToList();

        var total = 0.0;
        var itemCounter = 1;
        StringBuilder sb = new StringBuilder();

        foreach (var item in groupedItems)
        {
            var cost = stockItems[item.ItemName];
            var lineCost = item.Quantity * cost;
            total += lineCost;
            sb.AppendLine($"{itemCounter++}) {item.ItemName}. {item.Quantity} @ {cost:C} = {lineCost:C}");

            var discountedItems = (int)(item.Quantity / 3);
            if (discountedItems > 0)
            {
                var discount = discountedItems * cost;
                sb.AppendLine($"Discount {discount:C} 3For2");
                total -= discount;
            }
        }
        sb.Append($"Total: {total:C}");
        return sb.ToString();
    }

    public void ScanItem(string item)
    {
        items.Add(item);
    }
}