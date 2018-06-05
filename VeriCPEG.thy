theory VeriCPEG
imports Main "~~/src/HOL/Nominal/Nominal"

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
inductive kined ("_ \<turnstile> _ :: _" 40) where
  kEmpty: "[] \<turnstile> empty :: stringkind"
| kString: "[] \<turnstile> terminal _ :: stringkind"
| kNt : "\<lbrakk>((nonterm e),\<kappa>)#\<Delta> \<turnstile> e :: \<kappa>\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (nonterm e) :: \<kappa>"
| kSeq : "\<lbrakk>\<Delta> \<turnstile> e1:: stringkind; \<Delta> \<turnstile> e2 :: stringkind\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (seq e1 e2) :: stringkind"
| kAlt :  "\<lbrakk>\<Delta> \<turnstile> e1 :: \<kappa>; \<Delta> \<turnstile> e2 :: \<kappa>\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (seq e1 e2) :: \<kappa>"
| kRep : "\<lbrakk>\<Delta> \<turnstile> e :: stringkind\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (rep e) :: stringkind"
| kNot : "\<lbrakk>\<Delta> \<turnstile> e :: stringkind\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (not e) :: stringkind"
| kLeaf : "\<lbrakk>\<Delta> \<turnstile> e :: stringkind\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (leaf _ e) :: treekind"
(* | kCapture : "\<lbrakk>\<forall> \<xi> \<in> l. \<Delta> \<turnstile> \<xi> :: treekind\<rbrakk> \<Longrightarrow> \<Delta> \<turnstile> (capture _ l) :: treekind"*)

(* but not yet *)
                
section{* Parsing Semantics *}
(** parsing results *)

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

(* parsing function*)
fun parsing :: "cpegExp \<Rightarrow> string \<Rightarrow> (outputP \<times> string)" where
  "parsing empty xs = (simbols [],xs)"
| "parsing (terminal a) (x#xs) = (if a = x then (simbols ''a'',xs) else (fail, (x#xs)))"
| "parsing (nonterm e) xs = (case parsing e xs of )"

end