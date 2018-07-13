module ParsingSemantics

open Expressions

(*
type Tree =
    | TNode of l : string v : Tree
    | TConcat of vlist : Tree list
    | TEmpty
    | Fail
 *)

type Tree =
    | TNode of l : string * v : Tree
    | TConcat of vlist : Tree list
    | TSeq of vlist : Tree list
    | TEmpty
    | Fail
    | TLeaf of l : string * str : string


let rec parsing (se:CPEGExp) (sx:string) =
  match se with
    | Empty -> (TEmpty, sx)
    | Terminal a -> match sx with
                    | "" -> (Fail, "")
                    | _ -> if String.length sx <> 0 && a = sx.[0..(a.Length-1)] then (TEmpty, sx.[a.Length..sx.Length-1]) else (Fail, sx)
    | Nonterm e -> parsing e sx
    | Seq exs -> List.fold (fun v exp -> printfn "%A" v; match v with
                                | (Fail, ys) -> (Fail, ys)
                                | (TSeq oldsubt, xs) -> match parsing exp xs with
                                                | (Fail, _) -> (Fail, sx)
                                                | (TEmpty, ys) -> (TSeq oldsubt, ys)
                                                | (subt, ys) -> (TSeq (subt::oldsubt), ys)//subtをoldsubtの末尾に付くように直す
                                | (_, ys) -> (Fail, ys)
                           ) (TSeq [], sx) exs
                    |> fun (result, ys) -> match result with
                              | TSeq [] -> (TEmpty, ys)
                              | TSeq [x] -> (x, ys) 
                              | TSeq subt -> (TSeq (List.rev <| subt),ys)
                              | _ -> (result, ys)
    | Alt(e1,e2) -> match parsing e1 sx with
                    | (Fail,_) -> parsing e2 sx
                    | a -> a
    | Not e -> match parsing e sx with
                    | (Fail, _) -> (TEmpty, sx)
                    | _ -> (Fail, sx)
    | Rep e -> let mutable inputstr = sx
               let mutable treelist = List.Empty
               let mutable loopcondition = true
               while loopcondition do
                    let newtree, newstr = parsing e inputstr
                    //printfn "%A" newstr
                    inputstr <- newstr
                    if newtree = Fail then 
                      loopcondition <- false 
                    if (newtree <> TEmpty && newtree <> Fail) then
                      treelist <- newtree :: treelist
               match treelist with
                | [] -> (TEmpty, inputstr)
                | [x] -> (x, inputstr)
                | _ -> (TConcat (List.rev treelist), inputstr)
    | Cap(l, e) -> match parsing e sx with
                    | (Fail, _) -> (Fail, sx)
                    | (v, ys) -> (TNode(l, v), ys)
    | FCap(e1, l, e2) -> match parsing e1 sx with
                            | (Fail, _) -> (Fail, sx)
                            | (v, ys) -> match parsing (Seq [e2; Rep e2]) ys with
                                    | (TSeq w, zs) -> let foldTree = List.fold (fun lTree rTree -> TNode(l,TConcat [lTree; rTree])) v w in (foldTree, zs)
                                    | _ -> (v, ys)
    | Leaf(l, e) -> let lenE = e.Length
                    //printfn "call-, %A" lenE
                    if String.length sx <> 0 && e = sx.[0..lenE-1] then (TLeaf(l,e), sx.[lenE..sx.Length-1]) else (Fail, sx)