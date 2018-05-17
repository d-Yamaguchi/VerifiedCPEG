module CPEGTree
open CPEGExpressions
open HelperFunctions

type cpegTree =
  | Node : v : cpegTree -> l : label -> cpegTree
  | Concat : vs : list cpegTree -> cpegTree
  | NoTree : cpegTree

val margeTrees : cpegTree -> cpegTree -> Tot cpegTree
let margeTrees v1 v2 = match v1, v2 with
  | NoTree, w2 -> w2
  | w1, NoTree -> w1
  | Concat vs1, Concat vs2 -> Concat (append vs1 vs2)
  | Concat vs1, Node vs2 l -> Concat (append vs1 [Node vs2 l])
  | Node vs1 l, Concat vs2 -> Concat ((Node vs1 l)::vs2)
  | Node vs1 l1, Node vs2 l2 -> Concat [Node vs1 l1; Node vs2 l2]
