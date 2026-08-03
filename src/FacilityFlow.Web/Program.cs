using FacilityFlow.Web.Components;
using FacilityFlow.Web.Services;
var builder=WebApplication.CreateBuilder(args);builder.Services.AddRazorComponents().AddInteractiveServerComponents();builder.Services.AddHttpClient<FacilityFlowApiClient>(c=>c.BaseAddress=new Uri(builder.Configuration["ApiBaseUrl"]??"https://localhost:7101/"));
var app=builder.Build();if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Error");app.UseHsts();}app.UseHttpsRedirection();app.UseStaticFiles();app.UseAntiforgery();app.MapRazorComponents<App>().AddInteractiveServerRenderMode();app.Run();
