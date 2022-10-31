namespace Shared

open System

[<Measure>]
type inch

[<Measure>]
type year

[<Measure>]
type lbs

type User =
    { height: float<inch>
      birthday: DateOnly }

type DataPoint =
    { date: DateTime
      weight: float<lbs>
      bodyFatPercent: float option }

module DataPoint =
    let empty =
        { date = DateTime.UtcNow
          weight = 0.0<lbs>
          bodyFatPercent = None }

module Route =
    let builder typeName methodName = $"/api/%s{typeName}/%s{methodName}"

type IWeightsApi =
    { getWeights: string -> DataPoint list Async }
