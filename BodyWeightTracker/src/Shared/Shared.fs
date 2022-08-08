namespace Shared

open System

[<Measure>]
type inch

[<Measure>]
type year

[<Measure>]
type lbs

type User = { height: float<inch>; age: int<year> }

type DataPoint =
    { date: DateTime
      weight: float<lbs>
      bodyFatPercent: float option }

module DataPoint =
    let empty =
        { date = DateTime.Now
          weight = 0.0<lbs>
          bodyFatPercent = None }

module Route =
    let builder typeName methodName = $"/api/%s{typeName}/%s{methodName}"

type IWeightsApi =
    { getWeights: unit -> Async<DataPoint list> }
