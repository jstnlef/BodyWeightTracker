module Api

open Shared
open System

let getWeights userId =
  async {
    let weights =
      [ { date = DateTime(2022, 4, 18)
          weight = 240.0<lbs>
          bodyFatPercent = None }
        { date = DateTime(2022, 4, 19)
          weight = 238.7<lbs>
          bodyFatPercent = None }
        { date = DateTime(2022, 4, 20)
          weight = 239.4<lbs>
          bodyFatPercent = None }
        { date = DateTime(2022, 10, 30)
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
          bodyFatPercent = Some 27.5 }
        { date = DateTime(2022, 11, 26)
          weight = 207.4<lbs>
          bodyFatPercent = Some 27.2 }
        { date = DateTime(2022, 11, 27)
          weight = 209.8<lbs>
          bodyFatPercent = Some 27.7 }
        { date = DateTime(2022, 11, 28)
          weight = 209.4<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 12, 4)
          weight = 207.0<lbs>
          bodyFatPercent = Some 27.2 }
        { date = DateTime(2022, 12, 15)
          weight = 209.0<lbs>
          bodyFatPercent = Some 27.6 }
        { date = DateTime(2022, 12, 16)
          weight = 208.4<lbs>
          bodyFatPercent = Some 27.5 }
        { date = DateTime(2022, 12, 31)
          weight = 208.2<lbs>
          bodyFatPercent = Some 27.3 }
        { date = DateTime(2023, 1, 1)
          weight = 207.4<lbs>
          bodyFatPercent = Some 27.2 }
        { date = DateTime(2023, 1, 2)
          weight = 207.2<lbs>
          bodyFatPercent = Some 27.2 }
        { date = DateTime(2023, 1, 3)
          weight = 207.2<lbs>
          bodyFatPercent = Some 27.2 }
        { date = DateTime(2023, 1, 4)
          weight = 208.2<lbs>
          bodyFatPercent = Some 27.4 }
        { date = DateTime(2023, 1, 5)
          weight = 205.4<lbs>
          bodyFatPercent = Some 27.0 }
        { date = DateTime(2023, 1, 6)
          weight = 206.0<lbs>
          bodyFatPercent = Some 27.0 }
        { date = DateTime(2023, 1, 7)
          weight = 205.0<lbs>
          bodyFatPercent = Some 26.8 }
        { date = DateTime(2023, 1, 8)
          weight = 205.6<lbs>
          bodyFatPercent = Some 26.9 }
        { date = DateTime(2023, 1, 9)
          weight = 206.4<lbs>
          bodyFatPercent = Some 27.0 }
        { date = DateTime(2023, 1, 10)
          weight = 205.4<lbs>
          bodyFatPercent = Some 26.8 }
        { date = DateTime(2023, 1, 11)
          weight = 205.0<lbs>
          bodyFatPercent = Some 26.8 } ]

    return weights
  }

let weightsApi: WeightsApi = { getWeights = getWeights }