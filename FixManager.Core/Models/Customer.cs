namespace FixManager.Core.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<ServiceOrder> ServiceOrders { get; set; } = [];

}