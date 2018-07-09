module Expressions


let primTypes = [typeof<sbyte>; typeof<int16>; typeof<uint16>; typeof<int>; typeof<uint32>; 
                 typeof<int64>; typeof<uint64>; 
                 typeof<char>; typeof<string>; typeof<decimal>; typeof<float32>; typeof<float>;
                 typeof<single>; typeof<double>]

// let getPrimType (tag:string) = match List.tryFind (fun t -> t.Name.ToLower() = tag.ToLower()) primTypes with
//     | None -> typeof<string>
//     | Some x -> x

type CPEGExp =
    | Empty
    | Terminal of string
    | Nonterm of CPEGExp
    | Seq of elist : CPEGExp list
    | Alt of e1 : CPEGExp * e2 : CPEGExp
    | Rep of e : CPEGExp
    | Not of e : CPEGExp
    | Cap of l : string * e : CPEGExp
    | FCap of e1: CPEGExp * l: string * e2: CPEGExp
    | Leaf of l: string * e: string
