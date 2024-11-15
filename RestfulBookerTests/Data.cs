public static class Data
{
    public static string? token = null;
    public static string username = "admin";
    public static string password = "password123";
    public static string baseUrl = "https://restful-booker.herokuapp.com";
    public static Dictionary<string, object> booking = new(){
        {"firstname", "James"},
        {"lastname", "Brown"},
        {"totalprice", 111},
        {"depositpaid", true},
        {"bookingdates", new Dictionary<string, string>(){
            {"checkin", "2018-01-01"},
            {"checkout", "2019-01-01"}
        }},
        {"additionalneeds", "Breakfast"}
    };
}