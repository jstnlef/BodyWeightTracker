module Charts.WeightChart

open System
open Feliz
open Feliz.Plotly
open Shared

type WeightData =
  { dates: DateTime list
    weights: float list
    bodyFatPercents: float option list
    trendWeights: float list }

let calculate7DayTrend (previousWeights: float seq) =
  let last7 (xs: float seq) = Seq.skip ((Seq.length xs) - 7) xs
  [ previousWeights |> last7 |> Seq.average ]

let toChartWeightData (dataPoints: DataPoint list) =
  let accumulatePoint state point =
    let newWeights = state.weights @ [ float point.weight ]

    { state with
        dates = state.dates @ [ point.date ]
        weights = newWeights
        bodyFatPercents = state.bodyFatPercents @ [ point.bodyFatPercent ]
        trendWeights = state.trendWeights @ calculate7DayTrend newWeights }

  ({ dates = []
     weights = []
     bodyFatPercents = []
     trendWeights = [] },
   dataPoints)
  ||> List.fold accumulatePoint


let weightChart (dataPoints: DataPoint list) =
  let data = toChartWeightData dataPoints
  let earliest = data.dates.Head
  let latest = DateTime.UtcNow

  Plotly.plot [ plot.traces [ traces.scatter [ scatter.mode.markers
                                               scatter.name "Weight (lbs)"
                                               scatter.x data.dates
                                               scatter.y data.weights
                                               scatter.line [ line.color "#17BECF" ] ]
                              traces.scatter [ scatter.mode.lines
                                               scatter.name "7 Day Trend"
                                               scatter.x data.dates
                                               scatter.y data.trendWeights
                                               scatter.line [ line.color "#b21009"
                                                              line.shape.spline ] ] ]
                plot.layout [ layout.title [ title.text "Your weight over time" ]
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
                              layout.yaxis [ yaxis.autorange.true'
                                             yaxis.type'.linear ] ]
                plot.config [ config.responsive false
                              config.displaylogo false
                              config.modeBarButtonsToRemove [ modeBarButtons.lasso2d
                                                              modeBarButtons.select2d
                                                              modeBarButtons.resetScale2d ] ] ]