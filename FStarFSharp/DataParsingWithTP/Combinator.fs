module Combinator

open Expression
open PEGBasedParser


let ε = Empty
let inline (.>>.) (e1:TPEGExp) (e2:TPEGExp) = Seq(e1,e2)
let inline (/) (e1:TPEGExp) (e2:TPEGExp) = Alt(e1,e2)
let (!) (e:TPEGExp) = Not(e)