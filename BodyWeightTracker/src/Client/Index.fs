module Index

open System
open Elmish
open Fable.Remoting.Client
open Shared

open Feliz
open Feliz.Bulma

type Model = { weights: DataPoint list; user: User }

type Msg =
  | GotWeights of DataPoint list
  | AddWeightMeasurement of DataPoint

let weightsApi =
  Remoting.createApi ()
  |> Remoting.withRouteBuilder Route.builder
  |> Remoting.buildProxy<IWeightsApi>

let init () : Model * Cmd<Msg> =
  let model =
    { weights =
        [ { date = DateTime(2022, 11, 19)
            weight = 208.4<lbs>
            bodyFatPercent = Some 27.5 } ]
      user =
        { sex = Male
          height = 69.0<inch>
          birthday = DateOnly(1987, 10, 3) } }

  let cmd = Cmd.OfAsync.perform weightsApi.getWeights "" GotWeights

  model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
  match msg with
  | GotWeights weights -> { model with weights = weights }, Cmd.none
  | AddWeightMeasurement weight -> model, Cmd.none

let view (model: Model) (dispatch: Msg -> unit) =
  Bulma.container [
    Navbar.navbar
    Bulma.button.button [
      Bulma.color.isPrimary
      prop.ariaHasPopup true
      prop.target "add-weight-modal"
      prop.text "Add Weight"
    ]
    AddWeightModal.modal
    Bulma.container [
      Bulma.columns [
        let weight = model.weights[model.weights.Length - 1]

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
      Charts.WeightChart.weightChart model.weights
    ]

    Bulma.container [
      Charts.BmiChart.bmiChart model.user model.weights
    ]
  ]
