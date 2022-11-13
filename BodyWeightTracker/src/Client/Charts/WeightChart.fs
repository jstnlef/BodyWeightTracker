module Charts.WeightChart

open System
open Feliz
open Feliz.Plotly
open Shared

type DataPoints =
  { dates: DateTime array
    weights: float array
    bodyFatPercents: float array
    trendWeights: float array }

let weightChart (dataPoints: DataPoint array) =
  let data =
    { dates =
        [| DateTime(2022, 11, 7)
           DateTime(2022, 11, 8)
           DateTime(2022, 11, 9)
           DateTime(2022, 11, 10)
           DateTime(2022, 11, 11)
           DateTime(2022, 11, 12) |]
      weights =
        [| 209.2
           208.4
           207.8
           208.4
           210.6
           209.8 |]
      bodyFatPercents = [||]
      trendWeights =
        [| 210.4
           210.0
           209.7
           209.7
           209.5
           209.3 |] }

  let earliest = data.dates[0]
  let latest = data.dates[-1]

  Plotly.plot [ plot.traces [ traces.scatter [ scatter.mode.markers
                                               scatter.name "Weight (lbs)"
                                               scatter.x data.dates
                                               scatter.y data.weights
                                               scatter.line [ line.color "#17BECF" ] ]
                              traces.scatter [ scatter.mode.lines
                                               scatter.name "Trend"
                                               scatter.x data.dates
                                               scatter.y data.trendWeights
                                               scatter.line [ line.color "#b21009"
                                                              line.shape.spline ] ] ]
                plot.layout [ layout.title [ title.text "Your Weight" ]
                              layout.xaxis [ xaxis.autorange.true'
                                             xaxis.range [ earliest; latest ]
                                             xaxis.rangeselector [ rangeselector.buttons [ buttons.button [ button.count
                                                                                                              1
                                                                                                            button.label
                                                                                                              "1m"
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
                                             yaxis.type'.linear ] ] ]
