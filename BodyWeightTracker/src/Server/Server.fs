module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared

module Storage =
    let todos = ResizeArray()

    let addTodo (data: DataPoint) = Ok()

let weightsApi =
    { getWeights = fun () -> async { return Storage.todos |> List.ofSeq } }

let webApp =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue weightsApi
    |> Remoting.buildHttpHandler

let app =
    application {
        url "http://*:8085"
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }

[<EntryPoint>]
let main _ =
    run app
    0
