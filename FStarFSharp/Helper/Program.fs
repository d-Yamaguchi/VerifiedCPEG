// Learn more about F# at http://fsharp.org

open System
open HelperFunctions

[<EntryPoint>]
let main argv =
    let l = [1,2,3] in
    let l' = HelperFunctions.take (Prims.nat.One) l
    printfn "Hello World from F#!"
    0 // return an integer exit code
