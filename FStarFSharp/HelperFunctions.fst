module HelperFunctions

val length: list 'a -> Tot nat
let rec length l = match l with
  | [] -> 0
  | _ :: tl -> 1 + length tl

val append : l1:list 'a -> l2:list 'a -> Tot (l:list 'a{length l = length l1 + length l2})
let rec append l1 l2 = match l1 with
  | [] -> l2
  | hd :: tl -> hd :: append tl l2

val take : n : nat -> s : list 'a -> Tot(list 'a)
let rec take n s = match n with
  | 0 -> []
  | m -> match s with
	      | [] -> []
	      | hd::tl -> hd::(take (m-1) tl)

val drop : n : nat -> s : list 'a -> Tot(list 'a)
let rec drop n s = match n with
  | 0 -> s
  | m -> match s with
	      | [] -> []
	      | hd::tl -> drop (m-1) tl
