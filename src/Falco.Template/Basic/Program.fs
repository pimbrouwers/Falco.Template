module AppName.Program

open Falco
open Microsoft.AspNetCore.Builder 

[<EntryPoint>]
let main args =
    let bldr = WebApplication.CreateBuilder(args)
    let wapp = bldr.Build()
    
    wapp.Use(DeveloperExceptionPageExtensions.UseDeveloperExceptionPage)
        .UseFalco()
        .FalcoGet("/", Response.ofPlainText "Hello World")
        .FalcoNotFound(Response.withStatusCode 404 >> Response.ofEmpty)
        .Run()
    0