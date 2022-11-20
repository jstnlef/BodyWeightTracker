module Charts.BmiChart

open System
open Feliz.Plotly
open Shared

type BmiData =
  { dates: DateTime list
    bmis: float list }

let toBmiChartData height (dataPoints: DataPoint list) =
  let accumulatePoint state point =
    { state with
        dates = state.dates @ [ point.date ]
        bmis =
          state.bmis
          @ [ (float) (DataPoint.calculateBMI height point) ] }

  ({ dates = []; bmis = [] }, dataPoints)
  ||> List.fold accumulatePoint

let bmiChart (user: User) (dataPoints: DataPoint list) =
  let data = toBmiChartData user.height dataPoints
  let earliest = data.dates.Head
  let latest = DateTime.UtcNow

  let minBmi = data.bmis |> List.min
  let maxBmi = data.bmis |> List.max

  Plotly.plot [ plot.traces [ traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 16; 16 ]
                                               scatter.fillcolor "#ebaaa3"
                                               scatter.fill.tozeroy
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 18.5; 18.5 ]
                                               scatter.fillcolor "#f3eac2"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 25; 25 ]
                                               scatter.fillcolor "#c1dad4"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 30; 30 ]
                                               scatter.fillcolor "#f3eac2"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 35; 35 ]
                                               scatter.fillcolor "#f2c5a2"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 40; 40 ]
                                               scatter.fillcolor "#eeb09b"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.x [| earliest; latest |]
                                               scatter.y [ 80; 80 ]
                                               scatter.fillcolor "#ebaaa3"
                                               scatter.fill.tonexty
                                               scatter.mode.none
                                               scatter.showlegend false ]
                              traces.scatter [ scatter.name "BMI"
                                               scatter.x data.dates
                                               scatter.y data.bmis
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
                                             yaxis.range [ (int) (minBmi - 10.0)
                                                           (int) (maxBmi + 10.0) ] ] ]
                plot.config [ config.responsive false
                              config.displaylogo false
                              config.modeBarButtonsToRemove [ modeBarButtons.lasso2d
                                                              modeBarButtons.select2d
                                                              modeBarButtons.resetScale2d ] ] ]
