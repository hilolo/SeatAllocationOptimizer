// Defines a class for individual passengers.
public class Passenger
{
    // Basic properties to store passenger details.
    public bool IsAdult { get; set; }
    public string Family { get; set; }  // Family identifier.
    public int Revenue { get; private set; }
    public int Seats { get; set; }

    public string Key { get; set; }  // Unique identifier for each passenger.

    private static int AdultPrice = 250;  // Static price per adult.
    private static int ChildPrice = 150;  // Static Constructor_WithInputString_CorrectlyParsesAndInitializesPropertiesprice per child.

    // Constructor using explicit details.
    public Passenger(bool isAdult, int seats, string family, string key)
    {
        IsAdult = isAdult;
        Seats = seats;
        Family = family;
        Key = key;
        Revenue = isAdult ? AdultPrice * seats : ChildPrice * seats;
    }

    // Constructor to parse a data string into passenger details.
    public Passenger(string inputString)
    {
        var inputs = inputString.Split(',');
        IsAdult = inputs[1].Equals("Adult");
        Seats = inputs[4].Equals("Yes") ? 2 : 1;
        Revenue = IsAdult ? AdultPrice * Seats : ChildPrice * Seats;
        Family = inputs[3];
        Key = inputs[0];
    }

    // Check if the passenger belongs to a family.
    public bool IsInFamily()
    {
        return !Family.Equals("-");
    }

    // Calculates revenue per seat.
    public int GetPerSeatRevenue()
    {
        return Revenue / Seats;
    }

    // Static methods to set and get prices.
    public static int GetAdultPrice()
    {
        return AdultPrice;
    }

    public static void SetAdultPrice(int adultPrice)
    {
        AdultPrice = adultPrice;
    }

    public static int GetChildPrice()
    {
        return ChildPrice;
    }

    public static void SetChildPrice(int childPrice)
    {
        ChildPrice = childPrice;
    }

    // Returns a string representation of the passenger.
    public override string ToString()
    {
        return $"{Key}{(IsAdult ? "A" : "E")} {Family}";
    }
}
