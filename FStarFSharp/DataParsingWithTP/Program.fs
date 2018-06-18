// Learn more about F# at http://fsharp.org

open System
open Expression
open PEGBasedParser
open Combinator

(*Combinator example*)

let Val = Leaf(BInt,Terminal("122"))



[<EntryPoint>]
let main argv =
    printfn "Test Result for Parsers"
    printfn "%A" (parsing(Val,"122"))
    0 // return an integer exit code
