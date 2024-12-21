using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

public enum RequestType
{
    Auth,
    Create
}

public static class Data
{
    public static string authSchemaPath = "./../../../Schemas/AuthSchema.json";
    public static string createSchemaPath = "./../../../Schemas/CreateSchema.json";
    public static string authCasesPath = "./../../../TestCases/AuthTestCases.json";
    public static string createCasesPath = "./../../../TestCases/CreateBookingTestСases.json";
    public static string Valid = "valid";
    public static string Invalid = "invalid";
    public static string StatusCodeKey = "expectedcode";
    public static string? token = null;
    public static string baseUrl = "https://restful-booker.herokuapp.com";

    public static Dictionary<RequestType, JSchema> schemas = new(){
        {
            RequestType.Auth,
            JSchema.Parse(File.ReadAllText(authSchemaPath))
        },
        {
            RequestType.Create,
            JSchema.Parse(File.ReadAllText(createSchemaPath))
        }
    };

    public static Dictionary<RequestType, Dictionary<string, object>?> cases = new() {
        {
            RequestType.Auth,
            JsonConvert.DeserializeObject<Dictionary<string, object>>(
                File.ReadAllText(authCasesPath)
            )
        },

        {
            RequestType.Create,
            JsonConvert.DeserializeObject<Dictionary<string, object>>(
                File.ReadAllText(createCasesPath)
            )
        }
    };

    public static Dictionary<string, string> authHeaders = new(){
        {"Content-Type", "application/json"}
    };

    public static Dictionary<string, string> deleteHeaders = new(){
        {"Content-Type", "application/json"},
        {"Cookie", $"token={token}"}
    };

    public static void SetToken(string? token) => deleteHeaders["Cookie"] = $"token={token}";

    private static IEnumerable<object[]> CaseGenerator(RequestType reqType, string testClass)
    {
        var typeJson = cases[reqType];
        var classJson = (JObject)typeJson[testClass];

        var source = classJson.ToObject<Dictionary<string, object>>();

        foreach (var testCaseEntity in source)
        {
            var testCaseJson = (JObject)testCaseEntity.Value;
            var testCase = testCaseJson.ToObject<Dictionary<string, object>>();
            var code = (Int64)testCase["expectedcode"];
            var rawBody = (JObject)testCase["body"];
            var dictBody = rawBody.ToObject<Dictionary<string, object>>();

            yield return new object[] { (Int32)code, dictBody, testCaseEntity.Key };
        }
    }

    public static IEnumerable<object[]> GetValidAuthCases() => CaseGenerator(RequestType.Auth, Valid);
    public static IEnumerable<object[]> GetInvalidAuthCases() => CaseGenerator(RequestType.Auth, Invalid);
    public static IEnumerable<object[]> GetValidCreateCases() => CaseGenerator(RequestType.Create, Valid);
    public static IEnumerable<object[]> GetInvalidCreateCases() => CaseGenerator(RequestType.Create, Invalid);
}