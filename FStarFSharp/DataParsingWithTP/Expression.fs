module Expression

type BaseTypes = BString | BInt | BBool

type TPEGExp =
    | Empty
    | Terminal of string
    | Nonterm of TPEGExp
    | Seq of e1 : TPEGExp * e2 : TPEGExp
    | Alt of e1 : TPEGExp * e2 : TPEGExp
    | Rep of e : TPEGExp
    | Not of e : TPEGExp
    | Leaf of c :BaseTypes * e : TPEGExp
    | Cap of string * (NodeExp list)
    | FCap of string * TPEGExp * string * (NodeExp list) 
and NodeExp =
    | Subtree of nu: string * e: TPEGExp
    | Absorb of e : TPEGExp