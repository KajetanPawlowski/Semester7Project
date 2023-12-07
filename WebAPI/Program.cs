using System.Text;
using Application.DAOInterface;
using Application.Logic;
using Application.LogicInterface;
using Domain.Auth;
using InstantDataAccess;
using InstantDataAccess.DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add DataStorage
builder.Services.AddScoped<InstantDataContext>();

//Add DAOS
builder.Services.AddScoped<ISupplierDAO, SupplierInstantDAO>();
builder.Services.AddScoped<IQuestionDAO, QuestionInstantDAO>();
builder.Services.AddScoped<IRiskAttributeDAO, RiskAttributeInstantDAO>();
builder.Services.AddScoped<IRiskCategoryDAO, RiskCategoryInstantDAO>();
builder.Services.AddScoped<IRiskDAO, RiskInstantDAO>();
builder.Services.AddScoped<IUserDAO, UserInstantDao>();
builder.Services.AddScoped<ISurveyDAO, SurveyInstantDAO>();
builder.Services.AddScoped<ICountryDAO, CountryInstantDAO>();
builder.Services.AddScoped<IQuestionCategoryDAO, QuestionCategoryInstantDAO>();
//Add Logic
builder.Services.AddScoped<ISupplierLogic, SupplierLogic>();
builder.Services.AddScoped<ISurveyLogic, SurveyLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IRiskLogic, RiskLogic>();


AuthorizationPolicies.AddPolicies(builder.Services);
//Authorisation Stuff
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();