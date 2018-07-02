module Combinator

open Expressions
open ParsingSemantics


let ε = Empty

let inline (/) (e1:CPEGExp) (e2:CPEGExp) = Alt(e1,e2)
let (!) (e:CPEGExp) = Not(e)