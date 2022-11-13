module Charts.BmiChart

open System
open Feliz.Plotly
open Shared

type BmiDataPoint =
  { date: DateTime
    bmi: float<lbs / inch^2> }

let bmiChart (user: User) (data: DataPoint array) =
  let earliest = DateTime.UtcNow.AddDays(-3)
  let latest = DateTime.UtcNow.AddDays(1)

  let dates =
    [| DateTime.UtcNow.AddDays(-2)
       DateTime.UtcNow.AddDays(-1)
       DateTime.UtcNow |]

  Plotly.plot [ plot.traces [ traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 16; 16 ]
                                               scatter.fillcolor "#ebaaa3"
                                               scatter.fill.tozeroy
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 18.5; 18.5 ]
                                               scatter.fillcolor "#f3eac2"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 25; 25 ]
                                               scatter.fillcolor "#c1dad4"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 30; 30 ]
                                               scatter.fillcolor "#f3eac2"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 35; 35 ]
                                               scatter.fillcolor "#f2c5a2"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 40; 40 ]
                                               scatter.fillcolor "#eeb09b"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 60; 60 ]
                                               scatter.fillcolor "#ebaaa3"
                                               scatter.fill.tonexty
                                               scatter.mode.none ]
                              traces.scatter [ scatter.name "BMI"
                                               scatter.x dates
                                               scatter.y [ 31.0; 30.4; 30.2 ]
                                               scatter.mode [ scatter.mode.lines
                                                              scatter.mode.markers ]
                                               scatter.line [ line.color "#ee00d6" ] ] ]
                plot.layout [ layout.title [ title.text "BMI over time" ]
                              layout.xaxis [ xaxis.range [ earliest; latest ]
                                             xaxis.rangeselector [ rangeselector.buttons [ buttons.button [ button.count
                                                                                                              1
                                                                                                            button.label
                                                                                                              "1m"
                                                                                                            button.step.month
                                                                                                            button.stepmode.backward ]
                                                                                           buttons.button [ button.count
                                                                                                              3
                                                                                                            button.label
                                                                                                              "3m"
                                                                                                            button.step.month
                                                                                                            button.stepmode.backward ]
                                                                                           buttons.button [ button.count
                                                                                                              6
                                                                                                            button.label
                                                                                                              "6m"
                                                                                                            button.step.month
                                                                                                            button.stepmode.backward ]
                                                                                           buttons.button [ button.step.all ] ] ]
                                             xaxis.rangeslider [ rangeslider.range [ earliest; latest ] ]
                                             xaxis.type'.date ]
                              layout.yaxis [ yaxis.autorange.false'
                                             yaxis.range [ 10; 60 ] ] ] ]

// let bmis =
//   data
//   |> Array.map (fun d ->
//     { date = d.date
//       bmi = DataPoint.calculateBMI user.height d })

// Plotly.plot [ plot.traces [ traces.scatter
//                               [ scatter.x [ 1; 2; 3; 4 ] scatter.y [ 10; 15; 13; 17 ] scatter.mode.markers ]
//                               traces.scatter
//                               [ scatter.x [ 2; 3; 4; 5 ] scatter.y [ 16; 5; 11; 9 ] scatter.mode.lines ]
//                               traces.scatter
//                               [ scatter.x
//                                   [ 1; 2; 3; 4 ]
//                                   scatter.y
//                                   [ 12; 9; 15; 12 ]
//                                   scatter.mode
//                                   [ scatter.mode.lines scatter.mode.markers ] ] ] ]
