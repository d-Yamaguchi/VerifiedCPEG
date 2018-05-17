module CPEGExpressions

type label = string
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
  | Cap : e : cpegExp -> l : label -> cpegExp
  | FCapMany : e1 : cpegExp -> e2 : cpegExp -> l : label -> cpegExp
