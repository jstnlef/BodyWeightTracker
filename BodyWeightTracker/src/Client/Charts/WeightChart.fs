module Charts.WeightChart

open Feliz
open Feliz.Plotly
open Shared

let weightChart (data: DataPoint array) =
  Plotly.plot [ plot.traces [ traces.scatter [ scatter.x [ 1; 2; 3; 4 ]
                                               scatter.y [ 10; 15; 13; 17 ]
                                               scatter.mode.markers ]
                              traces.scatter [ scatter.x [ 2; 3; 4; 5 ]
                                               scatter.y [ 16; 5; 11; 9 ]
                                               scatter.mode.lines ]
                              traces.scatter [ scatter.x [ 1; 2; 3; 4 ]
                                               scatter.y [ 12; 9; 15; 12 ]
                                               scatter.mode [ scatter.mode.lines
                                                              scatter.mode.markers ] ] ] ]











// Recharts.lineChart
// [ lineChart.data
//     data
//     lineChart.margin
//     (top = 5, right = 30)
//     lineChart.children
//     [ Recharts.cartesianGrid [ cartesianGrid.strokeDasharray (3, 3) ]
//       Recharts.xAxis [ xAxis.dataKey (fun data -> data.date.ToString("yyyy-MM-dd")) ]
//       Recharts.yAxis [ yAxis.domain (domain.auto, domain.auto) ]
//       Recharts.tooltip []
//       Recharts.legend []
//       Recharts.line [ line.monotone
//                       line.dataKey (fun data -> float data.weight)
//                       line.stroke "#8884d8" ] ] ]
