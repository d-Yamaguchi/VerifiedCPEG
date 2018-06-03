module CPEGSemantics
open CPEGExpressions
open CPEGTree
open HelperFunctions

val cpegParse : e0 : cpegExp -> s0 : terminals -> (tuple2 (option cpegTree) terminals)
let rec cpegParse e0 s0 = match e0 with
  | Empty -> (Some NoTree, s0)
  | Term a -> let l = length a in
	     let (hd, tl) = (take l s0, drop l s0) in
	     if a = hd then (Some NoTree, tl)
		       else (None, s0)
  | NonTerm e1 -> cpegParse e1 s0
  | Seq e1 e2 -> (match cpegParse e1 s0 with
		 | (None, _) -> (None, s0)
		 | (Some NoTree, s1) -> cpegParse e2 s1
		 | (Some v1, s1) -> (match cpegParse e2 s1 with
				    | (None, _) -> (None, s0)
				    | (Some v2, s2) -> (Some (margeTrees v1 v2), s2)
				    )
		 )
  | Alt e1 e2 -> (match cpegParse e1 s0 with
		 | (None, _) -> cpegParse e2 s0
		 | (Some r, z) -> (Some r, z)
		)
  | Rep e -> (match cpegParse e s0 with
	     | (None, _) -> (Some NoTree, s0)
	     | (Some v1, s1) -> (match cpegParse (Rep e) s1 with
				| (None, _) -> (Some v1, s1)
				| (Some v2, s2) -> (Some (margeTrees v1 v2), s2)
			       )
	    )
  | Not e -> (match cpegParse e s0 with
	     | (None, _) -> (Some NoTree, s0)
	     | (Some _,_) -> (None, s0)
	    )
  | Cap e l -> (match cpegParse e s0 with
	       | (None, _) -> (None, s0)
	       | (Some v, s1) -> (Some (Node v l), s1)
	      )
//  | FCapMany _ _ _ -> (None, [])
  | FCapMany e1 e2 l -> match cpegParse e1 s0 with
			| (None, _) -> (None, s0)
			| (Some v1, s1) -> (match cpegParse (Seq e2 (Rep e2)) s1 with
					   | (None, _) -> (Some v1, s1)
					   | (Some v2, s2) -> (Some (margeTrees v1 v2), s2)//Note: replace the margeTree function; v1 is the least fixpoint and v2 is a marged Trees and there is no () tree in the marged trees.
					   (*
					   v0,v1,v1,(),v3,v
					   *)
					   )
