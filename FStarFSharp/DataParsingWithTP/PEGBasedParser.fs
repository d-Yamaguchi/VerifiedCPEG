module PEGBasedParser

open Expression

(* Helper Functions *)
let st2bool (str:string) = match str with
    | "True" | "true" -> true
    | "False" | "false" -> false


type Tree =
    | BaseValString of string
    | BaseValInt of int
    | BaseValBool of bool
    | NodeVal of string * ((string * Tree) list)

type ParserOutput =
    | Fail
    | Simbols of string
    | AST of Tree

type Subtrees = (string * Tree) list

//leftAssocTreeConstructor : string -> Tree -> string -> thetas -> tree
let rec leftAssocTreeConstructor (nu0:string, d0:Tree, l:string, thetas:Subtrees list) = match thetas with
    | [theta1] -> NodeVal(l, ((nu0,d0)::theta1))
    | thetaTl::theta -> NodeVal(l,( (nu0,leftAssocTreeConstructor(nu0, d0, l, theta))) ::thetaTl)
    | [] -> BaseValString "null Subtrees Theta was given"

let rec parsing (se:TPEGExp, sx:string) = match se with
    | Empty -> (Simbols "", sx)
    | Terminal a -> match sx with
                    | "" -> (Fail, "")
                    | _ -> if a = sx.[0..(a.Length-1)] then (Simbols a, sx.Remove 0) else (Fail, sx)
    | Nonterm e -> parsing(e, sx)
    | Seq(e1, e2) -> match parsing(e1, sx) with
                    | (Fail, _) -> (Fail, sx)
                    | (Simbols x, ys) -> match parsing(e2,ys) with
                                        | (Fail, _) -> (Fail, sx)
                                        | (Simbols y, zs) -> (Simbols (x+y), zs)
    | Alt(e1,e2) -> match parsing(e1, sx) with
                    | (Fail,_) -> parsing(e2,sx)
                    | a -> a
    | Not e -> match parsing(e, sx) with
                    | (Fail, _) -> (Simbols "",sx)
                    | _ -> (Fail, sx)
    | Rep e -> match parsing(e, sx) with
                    | (Fail,_) -> (Simbols "", sx)
                    | (Simbols x, ys) -> match parsing(Rep e, ys) with
                                            | (Fail, _) -> (Simbols x, ys)
                                            | (Simbols y, zs) -> (Simbols (x+y), zs)
    | Leaf(ty,e) -> match parsing(e,sx) with
                    | (Fail, _) -> (Fail, sx)
                    | (Simbols x, ys) -> match ty with
                                        | BString -> (AST (BaseValString x), ys)
                                        | BInt -> (AST (BaseValInt (int(x))), ys)
                                        | BBool -> (AST (BaseValBool (st2bool(x))),ys)
    | Cap(l, xis) -> match evalCapturedSubterm(xis, sx) with
                    | None -> (Fail, sx)
                    | Some(subtrees, ys) -> (AST (NodeVal(l, subtrees)), ys)
    | FCap(nu0,e0,l,xis) -> match parsing(e0,sx) with
                            | (Fail, _) -> (Fail, sx)
                            | (AST d, ys) -> match evalCapturedSubterm(xis, ys) with
                                                | None -> (AST d, ys)
                                                | Some(dyss, zs) -> let (theta,zt) = greedyRsTreeGene(xis, ([dyss],zs)) in (AST(leftAssocTreeConstructor(nu0,d,l,theta)), zt)
and evalCapturedSubterm(exprssions:(NodeExp list), xs:string) = match exprssions with
    | [] -> Some([], xs)
    | e::es -> match e with
                | Subtree(nu,eSub) -> match parsing(eSub, xs) with
                                        | (Fail, _) -> None
                                        | (AST d, ys) -> match evalCapturedSubterm(es, ys) with
                                                        | None -> None
                                                        | Some (dyss, zs) -> Some((nu,d)::dyss, zs)
                | Absorb eSub -> match parsing(eSub, xs) with
                                    | (Fail, _) -> None
                                    | (_, ys) -> Some([],ys)
and greedyRsTreeGene(xis,(theta,xs)) = match evalCapturedSubterm(xis, xs) with
                                        | None -> (theta, xs)
                                        | Some(thetaHd, ys) -> greedyRsTreeGene(xis,(thetaHd::theta,ys))
