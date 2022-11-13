module Storage

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open Npgsql
open System

open Shared

let init = Dapper.FSharp.OptionTypes.register ()

let connection =
  // TODO: Maybe this stuff can get replaced by asp.net's configuration classes.
  let host = Config.get "POSTGRES_HOST"
  let username = Config.get "POSTGRES_USER"
  let password = Config.get "POSTGRES_PASSWORD"
  let database = Config.get "POSTGRES_DB"
  new NpgsqlConnection($"Host={host};Username={username};Password={password};Database={database}")

module Data =
  let dataTable = table<DataPoint>

module Users =
  let userTable = table<User>
