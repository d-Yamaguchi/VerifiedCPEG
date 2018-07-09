module RET2ADT

open RET

type CType = 
    | TauString
    | TauProduct of CType list
    | TauList of CType
    | TauVar of string
    | TauUnit

type MVariant = string * CType list

let rec removeItemFromList item = function
    | [] -> []
    | x::xs when x == item -> xs
    | x::xs -> x :: removeItemFromList item xs

let rec RET2CType = function
    | RETEmpty -> TauUnit
    | RETConcat tlist -> TauProduct <| removeItemFromList TauUnit <| List.map RET2CType tlist
    | RETUnion(t1, t2) -> RET2MVariant t1 
    | RETRep
    | RETLabel
    | RETVar
    | RETErr