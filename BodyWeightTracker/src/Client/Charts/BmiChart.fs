module Charts.BmiChart

open System
open Feliz.Recharts
open Shared

type BmiDataPoint =
  { date: DateTime
    bmi: float<lbs / inch^2> }

let bmiChart (user: User) (data: DataPoint array) =
  let bmis =
    data
    |> Array.map (fun d ->
      { date = d.date
        bmi = DataPoint.calculateBMI user.height d })

  Recharts.areaChart [ areaChart.width 500
                       areaChart.height 400
                       areaChart.data bmis
                       areaChart.margin (top = 10, right = 30)
                       areaChart.children [ Recharts.cartesianGrid [ cartesianGrid.strokeDasharray (3, 3) ]
                                            Recharts.xAxis [ xAxis.dataKey (fun data -> data.date.ToString("yyyy-MM-dd")) ]
                                            Recharts.yAxis []
                                            Recharts.tooltip []

                                            Recharts.area [ area.monotone
                                                            area.stackId "1"
                                                            area.stroke "#8884d8"
                                                            area.fill "#8884d8" ]

                                            Recharts.area [ area.monotone
                                                            area.stackId "1"
                                                            area.stroke "#82ca9d"
                                                            area.fill "#82ca9d" ]

                                            Recharts.area [ area.monotone
                                                            area.stackId "1"
                                                            area.stroke "#ffc658"
                                                            area.fill "#ffc658" ] ] ]
