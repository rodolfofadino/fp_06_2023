using fiap.core.Models;
using fiap.core.Services;
using fiap.Middlewares;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var connection = "Server=(localdb)\\mssqllocaldb;Database=fiap-musicas;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<MusicaContext>(o => o.UseSqlServer(connection));

builder.Services.AddDataProtection()
    .SetApplicationName("fiap")
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\\"));

builder.Services.AddAuthentication("fiap")
    .AddCookie("fiap", o =>
    {
        o.LoginPath = "/account/login";
        o.AccessDeniedPath = "/account/denied";
        o.Events.OnValidatePrincipal = context =>
        {
            var a = context.Principal.Identity;
            //if (context.Properties.Items.TryGetValue("OpenIdConnect.Code.RedirectUri", out string redirectUri))
            //{
            //    Uri cookieWasSignedForUri = new Uri(redirectUri);
            //    if (context.Request.Host.Host != cookieWasSignedForUri.Host)
            //    {
            //        context.RejectPrincipal();
            //    }
            //}

            return Task.CompletedTask;
        };
    });

builder.Services.AddTransient<NoticiaService>();

var app = builder.Build();


//app.UseMiddleware<MeuMiddleware>();

//app.UseMeuLogger();



if (!app.Environment.IsProduction())
    app.UseDeveloperExceptionPage();


app.UseStaticFiles();


//https://localhost:59148/home/index
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "padraodoproduto",
    defaults: new { Controller = "Home", Action = "Detalhe" },
    pattern: "{categoria}/seuperfil/{idDoProduto}"
);

app.MapControllerRoute(
    name: "default",
    defaults: new { Controller = "Home", Action = "Index" },
    pattern: "{controller}/{action}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    // pattern: "{controller}/{action}/{id?}"
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();




#region 

//app.Use(async (context, next) =>
//{
//    var teste = 123;
//    await next();
//    var teste2 = 123;
//});

//app.Map("/admin", a =>
//{
//    a.Run(async context =>
//    {
//        await context.Response.WriteAsync("admin");
//    });
//});


//app.MapWhen(ctx => ctx.Request.Query.ContainsKey("valordaquery"),
//    a =>
//    {
//        a.Run(async context => await context.Response.WriteAsync("teste da query"));
//    }
//    );


//app.Use(async (context, next) =>
//{
//    var teste = 123;
//    await next();
//    var teste2 = 123;
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Boa noite");

//});

#endregion



//app.MapGet("/", 

//    () => "Boa noite agora que tem energia"

//    );
//app.MapGet("/teste", () => "teste");







