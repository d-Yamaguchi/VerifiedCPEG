# Learn more about F# at http://fsharp.org

import sys
from Expressions import *
from TypeInference import *
from ParsingSemantics import *

#TEST
Val = Leaf ("bInt", "1")
Prod2 = Cap("Mul", Seq([Val,Terminal("*"),Val]))
Prod = FCap(Val,"Mul",Seq([Terminal("*"),Val]))
ProdM = Rep(Val)
SeqREP = Rep(Seq([Terminal("a"), Val]))
Se = Seq([Terminal("a"), Terminal("a"), Terminal("a")])


if __name__ == '__main__':
    print("CPEG Test Version.")
    print("==Test1: Prod2 type and parsing result w.r.t. 1*1==")
    tp = TypingContext(set([]), {})
    print(getShape(tp, Prod2))
    #print(parsing(Prod2, "1*1"))
    print("==Test2: Prod type and parsing result w.r.t. 1*1*1==")
    tp = TypingContext(set([]), {})
    print(getShape(tp, Prod))
    #print(parsing(Prod, "1*1*1"))
    #print(parsing(Se, "aaaaa"))
    #printfn "==Test3: Prod type and parsing result w.r.t. 1*1*1=="
    #printfn "%A" <| parsing ProdM "11111"
    #printfn "==Test4: Prod type and parsing result w.r.t. ε=="
    #printfn "%A" <| parsing ProdM ""
    #printfn "==Test5: Prod type and parsing result w.r.t. ε=="
    #printfn "%A" <| parsing SeqREP "a1a1a1"
    # return an integer exit code
