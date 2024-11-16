using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
public static class Data
{
    public static string? token = null;
    public static string username = "admin";
    public static string password = "password123";
    public static string baseUrl = "https://restful-booker.herokuapp.com";
    public static Dictionary<string, JSchema> schemas = new(){
        {
            "Auth",
            JSchema.Parse(File.ReadAllText($"./../../../Schemas/AuthSchema.json"))
        },
        {
            "Create",
            JSchema.Parse(File.ReadAllText($"./../../../Schemas/CreateSchema.json"))
        }
    };
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
    public static Dictionary<string, string> authHeaders = new(){
        {"Content-Type", "application/json"}
    };
    public static Dictionary<string, string> deleteHeaders = new(){
        {"Content-Type", "application/json"},
        {"Cookie", $"token={token}"}
    };
    public static void SetToken(string? token) => deleteHeaders["Cookie"] = $"token={token}";
}