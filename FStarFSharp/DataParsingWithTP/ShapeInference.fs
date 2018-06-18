module ShapeInference

open System
open Expression
open System.Diagnostics
open System.Data

let primTypes = [typeof<sbyte>; typeof<int16>; typeof<uint16>; typeof<int>; typeof<uint32>; 
                 typeof<int64>; typeof<uint64>; 
                 typeof<char>; typeof<string>; typeof<decimal>; typeof<float32>; typeof<float>;
                 typeof<single>; typeof<double>]

let getPrimType (tag:String) = match List.tryFind (fun t -> t.Name.ToLower() = tag.ToLower()) primTypes with
    | None -> typeof<string>
    | Some x -> x

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

type TypingContext = {
    mutable chi : Set<string>;
    mutable gamma : Map<TPEGExp,Shape>;
}
let addChi (ctx:TypingContext) = fun str -> ctx.chi <- Set.add str ctx.chi
let addGamma (ctx:TypingContext) = fun (e,shape) -> ctx.gamma <- Map.add e shape ctx.gamma
//let addChiGamma (ctx:TypingContext) = fun (str,e,shape) -> {ctx with chi = Set.add str ctx.chi; gamma = Map.add e shape ctx.gamma}

let join (p:Map<'a,'b>) (q:Map<'a,'b>) = 
    Map(Seq.concat [ (Map.toSeq p) ; (Map.toSeq q) ])

let rec getShape (ctx:TypingContext) = function
    | Empty -> (SBaseType typeof<string>), Map.empty
    | Terminal _ -> (SBaseType typeof<string>), Map.empty
    | Nonterm pg -> match Map.tryFind (Nonterm pg) ctx.gamma with
                    | Some X -> (X, Map.empty)
                    | None -> let Xa = (freshName ctx.chi) in addChi ctx Xa ; addGamma ctx (Nonterm pg, SVariable Xa) ; getShape ctx pg |> fun (t, globSet) -> (SVariable Xa, Map.add Xa t globSet)
    | Seq (e1, e2) -> let (shape1, E1) = getShape ctx e1 in let (shape2, E2) = getShape ctx e2 in match (shape1, shape2) with
                    | (SBaseType _, SBaseType _) -> (shape1, join E1 E2)
                    | (_, _) -> (SError "Type error at Seq", Map.empty)
    | Alt (e1,e2) -> let (shape1, E1) = getShape ctx e1 in let (shape2, E2) = getShape ctx e2 in match (shape1, shape2) with
                    | (SError err1, SError err2) -> (SError (err1+err2), E1)
                    | (SError err1, _) -> (SError err1, E1)
                    | (_, SError err2) -> (SError err2, E2)
                    | (_, _) -> (SCommon (shape1::[shape2]), join E1 E2)
    | Rep e -> let (shape0,E0) = getShape ctx e in match shape0 with
                    | SBaseType _ -> (shape0,E0)
                    | _ -> (SError "Type error at Rep", Map.empty)
    | Not e -> let (shape0,E0) = getShape ctx e in match shape0 with
                    | SBaseType _ -> (shape0,E0)
                    | _ -> (SError "Type error at Rep", Map.empty)
    | Leaf (c, e) -> let (shape0,E0) = getShape ctx e in match shape0 with
                    | SBaseType _ -> (SBaseType (getPrimType c),E0) // FIXME:: change TPEGExp > Leaf c : BaseType to Type
                    | _ -> (SError "Type error at Rep", Map.empty)
