namespace SportsStatistics.Web.Api.Endpoints;

internal static class Routes
{
    private const string ApiBase = "/api";

    public static class Identity
    {
        private const string IdentityBase = $"{ApiBase}/identity";

        public const string Signin = $"{IdentityBase}/signin";
        public const string Signout = $"{IdentityBase}/signout";

        public static class Demo
        {
            private const string DemoBase = $"{IdentityBase}/demo";

            public const string Signin = $"{DemoBase}/signin";
        }
    }
}
