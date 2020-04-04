using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Common;

public class ServerUrls : IServerUrls
{
  public ServerUrls(IWebHostEnvironment Environment)
  {
    environment = Environment;
  }

  private IWebHostEnvironment environment { get; set; }

  public string API
  {
    get => environment.IsDevelopment()
            ? "http://counter-culture:4000"
            : "https://api.counter-culture.io";
  }
  public string APP
  {
    get => environment.IsDevelopment()
            ? "http://counter-culture:8080"
            : "https://www.counter-culture.io";
  }
  public string DEV
  {
    get => environment.IsDevelopment()
            ? "http://counter-culture:9000"
            : "https://geeks.counter-culture.io";
  }
  public string SECURE
  {
    get => environment.IsDevelopment()
            ? "http://counter-culture:5000"
            : "https://secure.counter-culture.io";
  }
}