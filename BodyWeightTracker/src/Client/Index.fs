module Index

open System
open Elmish
open Fable.Remoting.Client
open Shared

open Feliz
open Feliz.Bulma

type Model = { data: DataPoint list; user: User }

type Msg = AddWeightMeasurement of DataPoint

let todosApi =
  Remoting.createApi ()
  |> Remoting.withRouteBuilder Route.builder
  |> Remoting.buildProxy<IWeightsApi>

let init () : Model * Cmd<Msg> =
  let weights =
    [ { date = DateTime(2022, 11, 8)
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
        bodyFatPercent = Some 27.5 } ]

  let model =
    { data = weights
      user =
        { sex = Male
          height = 69.0<inch>
          birthday = DateOnly(1987, 10, 3) } }

  //    let cmd = Cmd.OfAsync.perform todosApi.getTodos () GotTodos

  model, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
  match msg with
  | AddWeightMeasurement weight -> model, Cmd.none

let view (model: Model) (dispatch: Msg -> unit) =
  Bulma.container [
    Navbar.navbar
    Bulma.container [
      Bulma.columns [
        let weight = model.data[model.data.Length - 1]

        Bulma.column [
          StatusBox.statusBox
            { text = $"{weight.weight}lbs"
              subtext = "Most recent Weight" }
        ]

        Bulma.column [
          let bmi = DataPoint.calculateBMI model.user.height weight

          StatusBox.statusBox
            { text = $"%0.1f{bmi}"
              subtext = "Most recent BMI" }
        ]

        Bulma.column [
          let bodyFat = DataPoint.bodyFatPercentOrEstimate model.user weight

          StatusBox.statusBox
            { text = $"%0.1f{bodyFat}"
              subtext = "Most recent Body Fat Percentage" }
        ]

        Bulma.column [
          let leanMass = DataPoint.calculateLeanMass model.user weight

          StatusBox.statusBox
            { text = $"%0.1f{leanMass}"
              subtext = "Most recent Lean Mass" }
        ]

        Bulma.column [
          let leanMass = DataPoint.calculateLeanMass model.user weight
          let fatMass = weight.weight - leanMass

          StatusBox.statusBox
            { text = $"%0.1f{fatMass}"
              subtext = "Most recent Fat Mass" }
        ]

        Bulma.column [
          let idealLower =
            weight
            |> DataPoint.estimateIdealWeight model.user 8.0

          let idealHigher =
            weight
            |> DataPoint.estimateIdealWeight model.user 19.0

          StatusBox.statusBox
            { text = $"%0.1f{idealLower}-%0.1f{idealHigher}"
              subtext = "Ideal weight range by estimated Body Fat (8% - 19%)" }
        ]
      ]
    ]
    Bulma.container [
      Charts.WeightChart.weightChart model.data
    ]

    Bulma.container [
      Charts.BmiChart.bmiChart model.user model.data
    ]
  ]
