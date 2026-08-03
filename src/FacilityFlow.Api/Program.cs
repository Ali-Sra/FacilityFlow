using FacilityFlow.Infrastructure;
using FacilityFlow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);builder.Services.AddControllers();builder.Services.AddEndpointsApiExplorer();builder.Services.AddSwaggerGen();builder.Services.AddProblemDetails();builder.Services.AddCors(o=>o.AddPolicy("Web",p=>p.WithOrigins(builder.Configuration["AllowedOrigins:Web"]??"https://localhost:7201","http://localhost:8080").AllowAnyHeader().AllowAnyMethod()));
var app=builder.Build();
app.UseExceptionHandler(handler=>handler.Run(async context=>{var ex=context.Features.Get<IExceptionHandlerFeature>()?.Error;var status=ex switch{ArgumentException=>400,InvalidOperationException=>409,Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException=>409,_=>500};context.Response.StatusCode=status;await context.Response.WriteAsJsonAsync(new ProblemDetails{Status=status,Title=status==500?"Interner Serverfehler":"Anfrage konnte nicht verarbeitet werden",Detail=app.Environment.IsDevelopment()?ex?.Message:"Die Anfrage konnte nicht verarbeitet werden."});}));
app.UseSwagger();app.UseSwaggerUI();app.UseHttpsRedirection();app.UseCors("Web");app.MapControllers();app.MapGet("/health",()=>Results.Ok(new{status="Healthy",timestamp=DateTime.UtcNow}));
using(var scope=app.Services.CreateScope()){var db=scope.ServiceProvider.GetRequiredService<FacilityFlowDbContext>();await DatabaseSeeder.SeedAsync(db);}
app.Run();
public partial class Program { }
