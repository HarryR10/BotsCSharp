namespace TestBot
{
    public class Settings
    {
        //Environment variables (user-secrets)
        public string ApiToken { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        //set route for UpdateController
        public const string WebHookRoutePart = "api/update";
    }

}
