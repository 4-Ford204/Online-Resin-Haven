namespace Customer.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterServices(builder.Configuration);

            var app = builder.Build();

            app.Configure();

            app.Run();
        }
    }
}
