module Api

open Shared

module Storage =
    let todos = ResizeArray()

    let addTodo (data: DataPoint) = Ok()

let weightsApi: IWeightsApi =
    { getWeights = fun userId -> async { return Storage.todos |> List.ofSeq } }
