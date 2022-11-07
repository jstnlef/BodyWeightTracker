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

module User =
  let age (today: DateOnly) (user: User) =
    let birthday = user.birthday
    let calcYears = today.Year - birthday.Year

    if today.Month < birthday.Month
       || ((today.Month = birthday.Month)
           && (today.Day < birthday.Day)) then
      calcYears - 1
    else
      calcYears

  let ageToday = age (DateOnly.FromDateTime(DateTime.UtcNow.Date))

type DataPoint =
  { date: DateTime
    weight: float<lbs>
    bodyFatPercent: float option }

module DataPoint =
  let empty =
    { date = DateTime.UtcNow
      weight = 0.0<lbs>
      bodyFatPercent = None }

  let calculateBMI (height: float<inch>) (datapoint: DataPoint) =
    703.0 * (datapoint.weight / (height * height))

  let estimateBodyFatFromBMI user datapoint =
    let bmi = calculateBMI user.height datapoint
    let age = User.ageToday user
    1.20 * (float) bmi + 0.23 * (float) age - 16.2

  let bodyFatPercentOrEstimate (user: User) (data: DataPoint) =
    data.bodyFatPercent
    |> Option.defaultValue (estimateBodyFatFromBMI user data)

  let estimateIdealWeight (user: User) (idealBodyFatPercent: float) (data: DataPoint) : float<lbs> =
    let bodyFatPercent = bodyFatPercentOrEstimate user data
    let currentBodyFat = bodyFatPercent / 100.0
    let baseWeight = data.weight * (1.0 - currentBodyFat)
    let idealBodyFat = idealBodyFatPercent / 100.0
    baseWeight / (1.0 - idealBodyFat)

module Route =
  let builder typeName methodName = $"/api/%s{typeName}/%s{methodName}"

type IWeightsApi =
  { getWeights: string -> DataPoint list Async }
