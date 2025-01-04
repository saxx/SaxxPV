namespace SaxxPv.Web.Models.Database;

public class Pricing
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
}
