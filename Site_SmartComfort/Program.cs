using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Site_SmartComfort.GerenciaArquivos;
using Site_SmartComfort.Libraries.Login;
using Site_SmartComfort.Libraries.Middleware;
using Site_SmartComfort.Repository;
using Site_SmartComfort.Repository.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<LoginFuncionario>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

builder.Services.AddScoped<Site_SmartComfort.Libraries.Sessao.Sessao>();

builder.Services.AddScoped<LoginUsuario>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc().AddSessionStateTempDataProvider();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<GerenciadorArquivo>();
builder.Services.AddScoped<Site_SmartComfort.Cookie.Cookie>();
builder.Services.AddScoped<Site_SmartComfort.CarrinhoCompra.CookieCarrinhoCompra>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
//app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();