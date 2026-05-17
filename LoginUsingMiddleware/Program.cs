namespace LoginUsingMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

app.UseLoginMiddleware();
            app.Run((async context => {
                 context.Response.StatusCode = 200;
                await context.Response.WriteAsync("No Response");
            }));
            app.Run();
        }
    }
}
