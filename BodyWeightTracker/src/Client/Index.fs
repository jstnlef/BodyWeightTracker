module Index

open System
open Elmish
open Fable.Remoting.Client
open Shared

[<Measure>] type inch
[<Measure>] type year
[<Measure>] type lbs

type User =
    {
        height: float<inch>
        age: int<year>
    }

type DataPoint =
    {
        date: DateTime
        weight: float<lbs>
        bodyFatPercent: float option
    }

type Model =
    {
        weights: DataPoint list
        user: User
    }

type Msg =
    | AddWeightMeasurement of DataPoint

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let weights =
        [
            { date = DateTime.Now; weight = 224.4<lbs>; bodyFatPercent = Some 30.1}
        ]
    let model = { weights = weights; user = { height = 69.0<inch>; age = 34<year> } }

//    let cmd = Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | AddWeightMeasurement weight -> model, Cmd.none

open Feliz
open Feliz.Bulma

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

let emptyDatapoint =
    {
        date = DateTime.Now
        weight = 0.0<lbs>
        bodyFatPercent = None
    }

let calculateBMI (height: float<inch>) (weight: float<lbs>) =
    703.0 * (weight / (height * height))

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.container [
        prop.children [
            Navbar.navbar
            Bulma.container [
                Bulma.columns [
                    let weight = model.weights |> List.tryHead |> Option.defaultValue emptyDatapoint
                    Bulma.column [
                        StatusBox.statusBox { text = $"{weight.weight}lbs"; subtext = "Most recent Weight"}
                    ]
                    Bulma.column [
                        let bmi = calculateBMI model.user.height weight.weight
                        StatusBox.statusBox { text = $"%0.1f{bmi}"; subtext = "Most recent BMI"}
                    ]
                    Bulma.column [
                        let bodyFat = weight.bodyFatPercent |> Option.defaultValue 0.0
                        StatusBox.statusBox { text = $"{bodyFat}"; subtext = "Most recent Body Fat Percentage"}
                    ]
                ]
            ]
        ]
    ]