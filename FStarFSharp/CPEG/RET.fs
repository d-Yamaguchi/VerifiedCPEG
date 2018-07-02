module RET

type RET =
    | RETEmpty
    | RETConcat of RET list
    | RETUnion of RET * RET
    | RETRep of RET
    | RETLabel of string * RET
    | RETVar of string
    | RETErr