namespace ProxyGas.WebApi;

internal class ApiRoutes
{
    public const string Root = "api/v{version:apiVersion}";

    public class UserProfiles
    {
        public const string Base = ApiRoutes.Root + "/user-profiles";
        public const string ById = "{id}";
        public const string BasicInfoById = ById + "/basic-info";
        public const string ByUserId =  "user/{userId}";
    }
    
    //Orders
    public class Orders
    {
        public const string Base = ApiRoutes.Root + "/orders";
        public const string ById =  "{id}";
    }
}