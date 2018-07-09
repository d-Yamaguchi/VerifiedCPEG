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
    | TEmpty
    | Fail
    | TLeaf of l : string * str : string


let rec parsing (se:CPEGExp) (sx:string)  = match se with
    | Empty -> (TEmpty, sx)
    | Terminal a -> match sx with
                    | "" -> (Fail, "")
                    | _ -> if a = sx.[0..(a.Length-1)] then (TEmpty, sx.[a.Length..sx.Length-1]) else (Fail, sx)
    | Nonterm e -> parsing e sx
    | Seq exs -> List.fold (fun v exp -> match v with
                                | (Fail, ys) -> (Fail, ys)
                                | (TConcat oldsubt, xs) -> match parsing exp xs with
                                                | (Fail, _) -> (Fail, sx)
                                                | (subt, ys) -> (TConcat (subt::oldsubt), ys)
                                | (_, ys) -> (Fail, ys)
                           ) (TConcat [], sx) exs
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
                    inputstr <- newstr
                    if newtree = Fail then 
                      loopcondition <- false 
                    else 
                      treelist <- newtree :: treelist
               if (List.isEmpty treelist) then (Fail, inputstr) else (TConcat (List.rev treelist), inputstr)
    | Cap(l, e) -> match parsing e sx with
                    | (Fail, _) -> (Fail, sx)
                    | (v, ys) -> (TNode(l, v), ys)
    | FCap(e1, l, e2) -> match parsing e1 sx with
                            | (Fail, _) -> (Fail, sx)
                            | (v, ys) -> match parsing (Seq [e2; Rep e2]) ys with
                                            | (Fail, _) -> (v, ys)
                                            | (w, zs) -> (TNode (l,w), zs)
    | Leaf(l, e) -> let lenE = e.Length
                    if e = sx.[0..lenE-1] then (TLeaf(l,e), sx.[lenE..sx.Length-1]) else (Fail, sx)