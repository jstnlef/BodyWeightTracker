module Api

open Shared
open System

let getWeights userId =
  async {
    let weights =
      [ { date = DateTime(2022, 10, 30)
          weight = 210.8<lbs>
          bodyFatPercent = Some 27.9 }
        { date = DateTime(2022, 10, 31)
          weight = 211.4<lbs>
          bodyFatPercent = Some 28.1 }
        { date = DateTime(2022, 11, 1)
          weight = 211.2<lbs>
          bodyFatPercent = Some 28.0 }
        { date = DateTime(2022, 11, 2)
          weight = 210.0<lbs>
          bodyFatPercent = Some 27.8 }
        { date = DateTime(2022, 11, 3)
          weight = 208.8<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 4)
          weight = 211.6<lbs>
          bodyFatPercent = Some 28.0 }
        { date = DateTime(2022, 11, 5)
          weight = 211.4<lbs>
          bodyFatPercent = Some 28.0 }
        { date = DateTime(2022, 11, 6)
          weight = 210.8<lbs>
          bodyFatPercent = Some 27.9 }
        { date = DateTime(2022, 11, 7)
          weight = 209.2<lbs>
          bodyFatPercent = Some 27.7 }
        { date = DateTime(2022, 11, 8)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 9)
          weight = 207.8<lbs>
          bodyFatPercent = Some 27.4 }
        { date = DateTime(2022, 11, 10)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 11)
          weight = 210.6<lbs>
          bodyFatPercent = Some 27.9 }
        { date = DateTime(2022, 11, 12)
          weight = 209.8<lbs>
          bodyFatPercent = Some 27.7 }
        { date = DateTime(2022, 11, 13)
          weight = 209.8<lbs>
          bodyFatPercent = Some 27.8 }
        { date = DateTime(2022, 11, 14)
          weight = 210.4<lbs>
          bodyFatPercent = Some 27.9 }
        { date = DateTime(2022, 11, 15)
          weight = 209.4<lbs>
          bodyFatPercent = Some 27.7 }
        { date = DateTime(2022, 11, 16)
          weight = 207.6<lbs>
          bodyFatPercent = Some 27.4 }
        { date = DateTime(2022, 11, 17)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 18)
          weight = 207.6<lbs>
          bodyFatPercent = Some 27.4 }
        { date = DateTime(2022, 11, 19)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.5 }
        { date = DateTime(2022, 11, 20)
          weight = 209.2<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 21)
          weight = 210.0<lbs>
          bodyFatPercent = Some 27.7 }
        { date = DateTime(2022, 11, 22)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.5 }
        { date = DateTime(2022, 11, 23)
          weight = 209.0<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 11, 24)
          weight = 206.8<lbs>
          bodyFatPercent = Some 27.1 }
        { date = DateTime(2022, 11, 25)
          weight = 208.6<lbs>
          bodyFatPercent = Some 27.5 } ]

    return weights
  }

let weightsApi: IWeightsApi = { getWeights = getWeights }
