module Charts.WeightChart

open Feliz.Recharts
open Shared

let weightChart (data: DataPoint array) =
  Recharts.lineChart [ lineChart.data data
                       lineChart.margin (top = 5, right = 30)
                       lineChart.children [ Recharts.cartesianGrid [ cartesianGrid.strokeDasharray (3, 3) ]
                                            Recharts.xAxis [ xAxis.dataKey (fun data -> data.date.ToString("yyyy-MM-dd")) ]
                                            Recharts.yAxis [ yAxis.domain (domain.auto, domain.auto) ]
                                            Recharts.tooltip []
                                            Recharts.legend []
                                            Recharts.line [ line.monotone
                                                            line.dataKey (fun data -> float data.weight)
                                                            line.stroke "#8884d8" ] ] ]
