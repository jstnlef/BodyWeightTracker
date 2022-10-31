module Storage

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open Npgsql
open System

open Shared

Dapper.FSharp.OptionTypes.register ()

let username = Environment.GetEnvironmentVariable("POSTGRES_USER")
let password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")
let conn = new NpgsqlConnection("")

module Data =
  let dataTable = table<DataPoint>

module Users =
  let userTable = table<User>
