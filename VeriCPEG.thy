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
  baseValString string
  | baseValInt int
  | baseValBool bool
  | nodeVal string "(string \<times> tree) list"  

(* outputP includes final value and intermediate*)
datatype outputP = 
  fail                                                 
  | simbols string
  | ast tree
(*| none   *)

(* parsing function*)
(*
Each input cpegExp for paring must be a well-formed expression.
How to describe this conditioning in the type of parsing?
*)

fun parsing :: "cpegExp \<Rightarrow> string \<Rightarrow> (outputP \<times> string)"
  and evalCapturedSubterm :: "(nodeExp list) \<Rightarrow> string \<Rightarrow> ((string\<times>tree) list \<times> string) option"
  where
  "parsing empty xs = (simbols [],xs)"
| "parsing (terminal a) (x#xs) = (if a = x then (simbols ''a'',xs) else (fail, (x#xs)))"
| "parsing (nonterm e) xs =  parsing e xs"
| "parsing (seq e1 e2) xs = (case parsing e1 xs of
                             (fail,_) \<Rightarrow> (fail,xs)
                           | (simbols x,ys) \<Rightarrow> (case parsing e2 ys of
                                                (fail,_) \<Rightarrow> (fail,xs)
                                              | (simbols y,zs) \<Rightarrow> (simbols (x@y),zs)
                                              )
                           (* if e1 and e2 are well-formed expressions, they will not derive a value ast (TODO: SHOW IT!) *)
                          )"
| "parsing (alt e1 e2) xs = (case parsing e1 xs of
                             (fail,_) \<Rightarrow> parsing e2 xs
                           | (simbols x, ys) \<Rightarrow> (simbols x, ys)
                           | (ast t, ys) \<Rightarrow> (ast t, ys)
                            )"
| "parsing (not e) xs = (case parsing e xs of
                          (fail,_) \<Rightarrow> (simbols [],xs)
                        | (_, _) \<Rightarrow> (fail, xs)
                        )"
| "parsing (leaf ty e) xs = (case parsing e xs of
                          (fail, _) \<Rightarrow> (fail, xs)
                        | (simbols x, ys) \<Rightarrow> (case ty of
                                               bString \<Rightarrow> (ast (baseValString x), ys)
                                             | bInt    \<Rightarrow> (ast (baseValInt 1), ys)
                                             | bBool   \<Rightarrow> (ast (baseValBool True), ys)
                                             )
                        )"
                        (* if e is a well-formed expression it will not derive a value ast (TODO: SHOW IT!) *)
| "parsing (capture l xis) xs = (case evalCapturedSubterm xis xs of
                                  None \<Rightarrow> (fail, xs)
                                | Some (subtrees, ys) \<Rightarrow> (ast (nodeVal l subtrees),ys)
                                )"
| "evalCapturedSubterm [] xs = Some ([], xs)"
| "evalCapturedSubterm (e#es) xs = (case e of
                                     subtree \<nu> eSub \<Rightarrow> (case parsing eSub xs of
                                                          (fail, _) \<Rightarrow> None
                                                        | (ast d, ys) \<Rightarrow> (case evalCapturedSubterm es ys of
                                                                            None \<Rightarrow> None
                                                                          | Some (dyss,zs) \<Rightarrow> Some ((\<nu>, d)#dyss,zs)
                                                                         )
                                                       )
                                   | absorb eSub \<Rightarrow> (case parsing eSub xs of
                                                       (fail,_) \<Rightarrow> None
                                                     | (simbols x, ys) \<Rightarrow> Some ([],ys)
                                                    )
                                   )"
end