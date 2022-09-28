module Index

open System
open Elmish
open Fable.Remoting.Client
open Shared

open Feliz
open Feliz.Bulma
open Feliz.Recharts

type Model =
    { weights: DataPoint array
      user: User }

type Msg = AddWeightMeasurement of DataPoint

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IWeightsApi>

let init () : Model * Cmd<Msg> =
    let weights =
        [| { date = DateTime.Now
             weight = 214.2<lbs>
             bodyFatPercent = Some 28.7 }
           { date = DateTime.Now - TimeSpan.FromDays(1)
             weight = 222.4<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(2)
             weight = 224.4<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(3)
             weight = 225.6<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(4)
             weight = 222.2<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(5)
             weight = 222.3<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(6)
             weight = 224.6<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(7)
             weight = 223.5<lbs>
             bodyFatPercent = Some 30.1 }
           { date = DateTime.Now - TimeSpan.FromDays(8)
             weight = 224.4<lbs>
             bodyFatPercent = Some 30.1 } |]

    let model =
        { weights = weights
          user =
            { height = 69.0<inch>
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

let calculateBMI (height: float<inch>) (weight: float<lbs>) = 703.0 * (weight / (height * height))

let estimateIdealWeight (data: DataPoint) (idealBodyFatPercent: float) : float<lbs> option =
    data.bodyFatPercent
    |> Option.map (fun bodyFatPercent ->
        let currentBodyFat = bodyFatPercent / 100.0
        let baseWeight = data.weight * (1.0 - currentBodyFat)
        let idealBodyFat = idealBodyFatPercent / 100.0
        baseWeight / (1.0 - idealBodyFat))

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.container [
        Navbar.navbar
        Bulma.container [
            Bulma.columns [
                let weight =
                    model.weights
                    |> Array.tryHead
                    |> Option.defaultValue DataPoint.empty

                let today = DateOnly.FromDateTime(DateTime.UtcNow.Date)
                let birthday = model.user.birthday
                let calcYears = today.Year - birthday.Year

                let age =
                    if today.Month < birthday.Month
                       || ((today.Month = birthday.Month)
                           && (today.Day < birthday.Day)) then
                        calcYears - 1
                    else
                        calcYears

                Bulma.column [
                    StatusBox.statusBox { text = $"{age}"; subtext = "Age" }
                ]

                Bulma.column [
                    StatusBox.statusBox
                        { text = $"{weight.weight}lbs"
                          subtext = "Most recent Weight" }
                ]

                Bulma.column [
                    let bmi = calculateBMI model.user.height weight.weight

                    StatusBox.statusBox
                        { text = $"%0.1f{bmi}"
                          subtext = "Most recent BMI" }
                ]

                Bulma.column [
                    let bodyFat = weight.bodyFatPercent |> Option.defaultValue 0.0

                    StatusBox.statusBox
                        { text = $"{bodyFat}"
                          subtext = "Most recent Body Fat Percentage" }
                ]

                Bulma.column [
                    let idealLower =
                        estimateIdealWeight weight 10.0
                        |> Option.defaultValue 0.0<lbs>

                    let idealHigher =
                        estimateIdealWeight weight 15.0
                        |> Option.defaultValue 0.0<lbs>

                    StatusBox.statusBox
                        { text = $"%0.1f{idealLower}-%0.1f{idealHigher}"
                          subtext = "Ideal weight range by estimated Body Fat" }
                ]
            ]
        ]
        Bulma.container [
        //            Recharts.lineChart [
//                lineChart.width 500
//                lineChart.height 300
//                lineChart.data model.weights
//                lineChart.margin(top=5, right=30)
//                lineChart.children [
//                    Recharts.cartesianGrid [ cartesianGrid.strokeDasharray(3, 3) ]
//                    Recharts.xAxis [ xAxis.dataKey (fun weight -> weight.date.ToString()) ]
//                    Recharts.yAxis [ ]
//                    Recharts.tooltip [ ]
//                    Recharts.legend [ ]
//                    Recharts.line [
//                        line.monotone
//                        line.dataKey (fun weight -> float weight.weight)
//                        line.stroke "#8884d8"
//                    ]
//                ]
//            ]
        ]
    ]
