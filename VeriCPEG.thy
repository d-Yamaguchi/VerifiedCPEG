theory VeriCPEG
imports Main

begin

section{* Syntax of CPEG *}

(* baseTypes are given each literals of leafs *)
datatype baseTypes = bString | bInt | bBool

(* Here is the syntax of CPEG*)
datatype cpegExp = 
  empty
  | terminal char
  | nonterm cpegExp
  | seq cpegExp cpegExp
  | alt cpegExp cpegExp
  | rep cpegExp
  | not cpegExp
  | leaf baseTypes cpegExp
  | capture string "nodeExp list"
  | foldCap string cpegExp string "nodeExp list"  (* \<nu>0: string, e0: cpegExp,  L: string, [\<xi>]: nodeExp list *)
and  nodeExp =
  subtree string cpegExp
  | absorb cpegExp

(* To difine well-formed CPEG Expressions, we introduce a kind system *)
datatype cpegKind = treekind | stringkind

(* Kinding Rule is defind as follows *)

(* but not yet *)

section{* Parsing Semantics *}
(* parsing results *)

(* tree is a final value of parsing result of well-formed expressions*)
datatype tree =
  baseVal baseTypes
  | nodeVal string "(string \<times> tree) list"  

(* outputP includes final value and intermediate*)
datatype outputP = 
  fail
  | simbols string
  | asts tree
  | none 
  



end