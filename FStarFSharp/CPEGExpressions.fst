module CPEGExpressions

type label = string

type beseLabel = string

type termSymbol =
  | A : termSymbol
  | B : termSymbol
  
type terminals = list termSymbol

type cpegExp =
  | Empty : cpegExp
  | Term : a: terminals -> cpegExp
  | NonTerm : cpegExp -> cpegExp
  | Seq : e1 : cpegExp -> e2 : cpegExp -> cpegExp
  | Alt : e1 : cpegExp -> e2 : cpegExp -> cpegExp
  | Rep : e : cpegExp -> cpegExp
  | Not : e : cpegExp -> cpegExp
  | Leaf : e : cpegExp -> c : baseLabel -> cpegExp
  | CapMany : xi: list nodeExp -> l : label -> cpegExp
  | FCapMany : e1 : cpegExp -> e2 : list nodeExp -> l : label -> cpegExp
  | None : cpegExp

type nodeExp =
  | Subtree : cpegExp -> nodeExp
  | Absorb : cpegExp -> nodeExp
