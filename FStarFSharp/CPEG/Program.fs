// Learn more about F# at http://fsharp.org

open System
open Expressions
open TypeInference
open ParsingSemantics

//TEST
let Val = Leaf ("bInt", "1")
let Prod2 = Cap("Mul", Seq[Val;Terminal("*");Val])
let Prod = FCap(Val,"Mul",Nonterm Val)
[<EntryPoint>]
let main argv =
    printfn "CPEG Test Varsion."
    printfn "==Test1: Prod2 type and paring result w.r.t. 1*1=="
    let tp = {chi=Set.empty; gamma = Map.empty} in printfn "%A" <| getShape tp Prod2
    printfn "%A" <| parsing Prod2 "1*1"
    printfn "==Test2: Prod type and paring result w.r.t. 1*1*1=="
    let tp = {chi=Set.empty; gamma = Map.empty} in printfn "%A" <| getShape tp Prod
    printfn "%A" <| parsing Prod "1*1*1"
    0 // return an integer exit code