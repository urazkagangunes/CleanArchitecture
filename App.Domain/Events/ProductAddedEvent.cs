namespace App.Domain.Events;

public record ProductAddedEvent(int Id, string Name, decimal Price) : IEventOrMessage;

//public class ProductAddedEvent : IEvent
//{
//    public int Id { get; init; }
//    public string Name { get; init; }
//    public decimal Price { get; init; }

//    public ProductAddedEvent(int id, string name, decimal price)
//    {
//        Id = id;
//        Name = name;
//        Price = price;
//    }

//    public ProductAddedEvent()
//    {
//    }
//}