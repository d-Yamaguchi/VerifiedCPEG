module ShapeInference

open System
open Expression
open System.Diagnostics
open System.Data

let primTypes = [typeof<sbyte>; typeof<int16>; typeof<uint16>; typeof<int>; typeof<uint32>; 
                 typeof<int64>; typeof<uint64>; 
                 typeof<char>; typeof<string>; typeof<decimal>; typeof<float32>; typeof<float>;
                 typeof<single>; typeof<double>]

// let getPrimType (tag:String) = match List.tryFind (fun t -> t.Name.ToLower() = tag.ToLower()) primTypes with
//     | None -> typeof<string>
//     | Some x -> x

let randomStr = 
    let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
    let charsLen = chars.Length
    let random = System.Random()

    fun len -> 
        let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
        new System.String(randomChars)

let freshName (chi : Set<string>)= let candidate = randomStr 2 in if chi.Contains candidate then candidate else candidate+(randomStr 2)

type Shape =
    | SBaseType of Type
    | SRecode of string * ((string * Shape) list)
    | SCommon of Shape list
    | SVariable of string
    | SError of massege : string

type GlobalSetOfShapes = { mutable Chi : string list; mutable E : Map<string,Shape>;}

type TypingContext = {
    mutable chi : Set<string>;
    mutable gamma : Map<TPEGExp,Shape>;
}
let addChi (ctx:TypingContext) = fun str -> ctx.chi <- Set.add str ctx.chi
let addGamma (ctx:TypingContext) = fun (e,shape) -> ctx.gamma <- Map.add e shape ctx.gamma
//let addChiGamma (ctx:TypingContext) = fun (str,e,shape) -> {ctx with chi = Set.add str ctx.chi; gamma = Map.add e shape ctx.gamma}

let rec getShape (ctx:TypingContext) = function
    | Empty -> (SBaseType typeof<string>), Map.empty
    | Terminal _ -> (SBaseType typeof<string>), Map.empty
    | Nonterm pg -> match Map.tryFind (Nonterm pg) ctx.gamma with
                    | Some X -> (X, Map.empty)
                    | None -> let Xa = (freshName ctx.chi) in addChi ctx Xa ; addGamma ctx (Nonterm pg, SVariable Xa) ; getShape ctx pg |> fun (t, globSet) -> (SVariable Xa, Map.add Xa t globSet)

