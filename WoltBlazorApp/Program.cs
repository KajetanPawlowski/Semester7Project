using System.Text;
using Application.DAOInterface;
using Application.Logic;
using Application.LogicInterface;
using Domain.Auth;
using HttpClients.Implementations;
using HttpClients.Interfaces;
using InstantDataAccess;
using InstantDataAccess.DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using WoltBlazorApp.Auth;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



//From Domain
AuthorizationPolicies.AddPolicies(builder.Services);

//HTTP Client
builder.Services.AddScoped(
    sp =>
        new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5036")
        }
);

builder.Services.AddScoped<IAuthHttpClient, JwtHttpClient>();
builder.Services.AddScoped<ISupplierHttpClient, SupplierHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();