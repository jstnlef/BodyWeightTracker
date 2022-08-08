module Nivo

// TODO: Would really like to use this but I suspect I'm going to need to do a bunch of work to make this useful.
// Using recharts for now.

open Fable.Core

[<ImportDefault("nivo/core")>]
let nivoCore: obj = jsNative

[<ImportDefault("nivo/line")>]
let nivoLine: obj = jsNative
