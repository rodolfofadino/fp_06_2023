using fiap.application.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);


fiap.IoC.DependencyContainer.RegisterServices(builder.Services, builder.Configuration);


builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties†.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});


builder.Services.AddControllersWithViews(options => { 
    options.Filters.Add(
        new AutoValidateAntiforgeryTokenAttribute()); 

});



builder.Services.AddDataProtection()
    .SetApplicationName("fiap")
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\\"));


builder.Services.Configure<GzipCompressionProviderOptions>
    (o=>o.Level = System.IO.Compression.CompressionLevel.Optimal);

builder.Services.AddResponseCompression(
    r => { r.Providers.Add<GzipCompressionProvider>(); 
    });

builder.Services.AddMemoryCache();

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


var app = builder.Build();


//app.UseMiddleware<MeuMiddleware>();

//app.UseMeuLogger();



if (!app.Environment.IsProduction())
    app.UseDeveloperExceptionPage();


app.UseResponseCompression();

app.UseStaticFiles(
    new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            const int durationInSeconds = 60 * 60 * 24 * 30;
            ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={durationInSeconds}");
        }
    

    }); 


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







