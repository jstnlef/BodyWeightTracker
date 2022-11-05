module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn

open Shared

Storage.init

let webApp =
  Remoting.createApi ()
  |> Remoting.withRouteBuilder Route.builder
  |> Remoting.fromValue Api.weightsApi
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
