module Index

open System
open Elmish
open Fable.Remoting.Client
open Shared

open Feliz
open Feliz.Bulma

type Model = { data: DataPoint array; user: User }

type Msg = AddWeightMeasurement of DataPoint

let todosApi =
  Remoting.createApi ()
  |> Remoting.withRouteBuilder Route.builder
  |> Remoting.buildProxy<IWeightsApi>

let init () : Model * Cmd<Msg> =
  let weights =
    [| { date = DateTime.UtcNow.AddDays(-2)
         weight = 208.4<lbs>
         bodyFatPercent = Some 27.6 }
       { date = DateTime.UtcNow.AddDays(-1)
         weight = 210.2<lbs>
         bodyFatPercent = Some 27.9 }
       { date = DateTime.UtcNow
         weight = 209.2<lbs>
         bodyFatPercent = Some 27.7 } |]

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

//let containerBox (model: Model) (dispatch: Msg -> unit) =
//    Bulma.box [
//        Bulma.content [
//            Html.ol [
//                for todo in model.Todos do
//                    Html.li [ prop.text todo.Description ]
//            ]
//        ]
//        Bulma.field.div [
//            field.isGrouped
//            prop.children [
//                Bulma.control.p [
//                    control.isExpanded
//                    prop.children [
//                        Bulma.input.text [
//                            prop.value model.Input
//                            prop.placeholder "What needs to be done? Eh?"
//                            prop.onChange (fun x -> SetInput x |> dispatch)
//                        ]
//                    ]
//                ]
//                Bulma.control.p [
//                    Bulma.button.a [
//                        color.isPrimary
//                        prop.disabled (Todo.isValid model.Input |> not)
//                        prop.onClick (fun _ -> dispatch AddTodo)
//                        prop.text "Add"
//                    ]
//                ]
//            ]
//        ]
//    ]

let view (model: Model) (dispatch: Msg -> unit) =
  Bulma.container [
    Navbar.navbar
    Bulma.container [
      Bulma.columns [
        let weight =
          model.data
          |> Array.tryHead
          |> Option.defaultValue DataPoint.empty

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
