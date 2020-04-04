using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Common;
using Application;
using Infrastructure;
using Services;

namespace AuthDemo
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options => options.EnableEndpointRouting = false);
      services.AddScoped<IServerUrls, ServerUrls>();
      services.AddScoped<HttpClient>();
      services.AddScoped<IServiceAgent, ServiceAgent>();
      services.AddScoped<ICacheService, CacheService>();
      services.AddScoped<ISecureService, SecureService>();
      services.AddScoped<ICounterService, CounterService>();
      services.AddScoped<GetTokenUseCase>();
      services.AddScoped<GetCountersUseCase>();
      services.AddScoped<RenewTokenUseCase>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvcWithDefaultRoute();

    }
  }
}
