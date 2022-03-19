namespace TaxCalculator;

public class Order
{
    public Order(Customer customer, Address shipTo, Address shipFrom, Money amount)
    {
        Customer = customer;
        ShipTo = shipTo;
        ShipFrom = shipFrom;
        Amount = amount;
    }
    
    public Customer Customer { get; set; }
    public Address ShipTo { get; set; }
    public Address ShipFrom { get; set; }
    
    public Money Amount { get; set; }
}

public class Customer
{
    public Customer(int id, string givenName, string surname)
    {
        Id = id;
        GivenName = givenName;
        Surname = surname;
    }
    
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
}

public class Address
{
    public Address(int id, string street, string city, string state, string zip, string country)
    {
        Id = id;
        Street = street;
        City = city;
        State = state;
        Zip = zip;
        Country = country;
    }

    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
}