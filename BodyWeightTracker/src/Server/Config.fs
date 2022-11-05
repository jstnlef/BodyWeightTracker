module Config

open System

let get key =
  DotEnv.init
  Environment.GetEnvironmentVariable key
