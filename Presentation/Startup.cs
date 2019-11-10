﻿using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Common;
using AuthDemo.Constants;
using AuthDemo.API;
using AuthDemo.UseCases;
using AuthDemo.Interfaces;

namespace AuthDemo
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddScoped<IServerUrls, ServerUrls>();
      services.AddScoped<HttpClient>();
      services.AddScoped<ISecureApi, SecureApi>();
      services.AddScoped<ICoreApi, CoreApi>();
      services.AddScoped<GetTokenUseCase>();
      services.AddScoped<GetCountersUseCase>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvcWithDefaultRoute();

    }
  }
}