
using System.Text;

public class Basket
{

    private readonly Dictionary<string, double> stockItems = new Dictionary<string, double>(StringComparer.CurrentCultureIgnoreCase)
    {
        {"milk", 1.5}
    };

    private List<string> items = new List<string>();

    public string GenerateReceipt()
    {
        if (items.Count == 0)
            return "No items";

        var total = 0.0;
        var itemCounter = 1;
        StringBuilder sb = new StringBuilder();
        foreach (var item in items)
        {
            var cost = stockItems[item];
            total+=cost;
            sb.AppendLine($"{itemCounter}) {item}. 1 @ {cost:C} = {cost:C}");
        }
        sb.Append($"Total: {total:C}");
        return sb.ToString();
    }

    public void ScanItem(string item)
    {
        items.Add(item);
    }
}