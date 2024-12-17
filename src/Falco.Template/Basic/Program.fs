namespace AppName

module App =
    open Falco
    open Falco.Routing

    let endpoints =
        [
            get "/" (Response.ofPlainText "Hello World!")
        ]

module Program =
    open Falco
    open Microsoft.AspNetCore.Builder
    open Microsoft.Extensions.DependencyInjection

    [<EntryPoint>]
    let main args =
        let bldr = WebApplication.CreateBuilder(args)

        // register services
        bldr.Services
            .AddRouting()
            |> ignore

        let wapp = bldr.Build()

        // assemble middleware pipeline
        wapp.Use(DeveloperExceptionPageExtensions.UseDeveloperExceptionPage)
            .UseRouting()
            .UseFalco(App.endpoints)
            .UseFalcoNotFound(Response.withStatusCode 404 >> Response.ofPlainText "Not Found")
            .Run()
        0
