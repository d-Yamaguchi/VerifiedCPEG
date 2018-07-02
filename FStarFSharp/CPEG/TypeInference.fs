module TypeInference

open Expressions
open RET

let randomStr = 
    let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
    let charsLen = chars.Length
    let random = System.Random()
    fun len -> 
        let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
        new System.String(randomChars)

let freshName (chi : Set<string>)= let candidate = randomStr 2 in if chi.Contains candidate then candidate else candidate+(randomStr 2)


type TypingContext = {
    mutable chi : Set<string>;
    mutable gamma : Map<CPEGExp,RET>;
}
let addChi (ctx:TypingContext) = fun str -> ctx.chi <- Set.add str ctx.chi
let addGamma (ctx:TypingContext) = fun (e,shape) -> ctx.gamma <- Map.add e shape ctx.gamma

let join (p:Map<'a,'b>) (q:Map<'a,'b>) = 
    Map(Seq.concat [ (Map.toSeq p) ; (Map.toSeq q) ])

(*
    getShape ::
    TypingContext := {gamma, chi}
    CPEGExpression := expression to be typed
    RET := Type for CPEGExpression
    MAP(String to RET) := global set of RET
*)

let rec getShape (ctx:TypingContext) = function
    | Empty -> RETEmpty, Map.empty
    | Terminal _ -> RETEmpty, Map.empty
    | Nonterm pg -> match Map.tryFind (Nonterm pg) ctx.gamma with
                    | Some X -> (X, Map.empty)
                    | None -> let Xa = (freshName ctx.chi)
                              addChi ctx Xa
                              addGamma ctx (Nonterm pg, RETVar Xa)
                              getShape ctx pg |> fun (t, globSet) -> (RETVar Xa, Map.add Xa t globSet)
    | Seq exs -> List.foldBack (fun e T -> (getShape ctx e) :: T) exs List.empty
                    |> List.unzip |> fun (a, bx) -> (RETConcat a, List.fold join Map.empty bx)
    | Alt (e1,e2) -> let (shape1, E1) = getShape ctx e1
                     let (shape2, E2) = getShape ctx e2
                     match (shape1, shape2) with
                        | (RETErr, _) -> (RETErr, E1)
                        | (_, RETErr) -> (RETErr, E2)
                        | (_, _) -> (RETUnion (shape1, shape2), join E1 E2)
    | Rep e -> let (shape0,E0) = getShape ctx e
               match shape0 with
                    | RETEmpty -> (shape0, E0)
                    | RETErr -> (RETErr, E0)
                    | derivedRET -> (RETRep derivedRET, E0)
    | Not _ -> (RETEmpty, Map.empty)
    | Cap (l, e) -> let (shape, E0) = getShape ctx e in (RETLabel (l, shape), E0)
    | FCap (e1, l, e2) -> let (shape1, E1) = getShape ctx e1
                          let (shape2, E2) = getShape ctx e2
                          let Xa = freshName ctx.chi
                          addChi ctx Xa
                          RETVar Xa, (join E1 E2 |> Map.add Xa (RETUnion (RETLabel(l, (RETConcat [RETVar Xa; shape2])),shape1)))
