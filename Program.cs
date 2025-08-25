using GcpMessagingDemo.Cache;
using GcpMessagingDemo.Config;
using GcpMessagingDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // Carrega appsettings.json
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<RedisConfig>(context.Configuration.GetSection("Redis"));
                services.Configure<GcpConfig>(context.Configuration.GetSection("Gcp"));

                // registra serviços
                services.AddSingleton<RedisService>();
                services.AddSingleton<MessageProcessor>();
                services.AddSingleton<PublisherService>();
                services.AddSingleton<SubscriberService>();
            })
            .Build();

        // resolve serviços
        var publisher = host.Services.GetRequiredService<PublisherService>();
        var subscriber = host.Services.GetRequiredService<SubscriberService>();

        // publica uma mensagem no Pub/Sub
        await publisher.PublishAsync("Olá PubSub com C#!");

        // Iniciar subscriber
        await subscriber.StartAsync();

        Console.WriteLine("Pressione [Enter] para encerrar.");
        Console.ReadLine();
    }
}
