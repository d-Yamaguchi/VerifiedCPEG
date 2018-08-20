from Expressions import *
import functools

#type Tree =
#    | TNode of l : string v : Tree
#    | TConcat of vlist : Tree list
#    | TEmpty
#    | Fail

class Tree:
    def __init__(self):
        pass

class TNode(Tree):
    def __init__(self, v1, v2):
        self.l = v1
        self.v = v2

class TConcat(Tree):
    def __init__(self, v):
        self.vlist = v

class TSeq(Tree):
    def __init__(self, v):
        self.vlist = v

class TEmpty(Tree):
    def __init__(self):
        pass

class Fail(Tree):
    def __init__(self):
        pass

class TLeaf(Tree):
    def __init__(self, v1, v2):
        self.l = v1
        self.str = v2


def parsing (se: CPEGExp, sx: str):
    if isinstance(se, Empty):
        return (TEmpty(), sx)
    elif isinstance(se, Terminal):
        if len(sx) == 0:
            return (Fail(), "")
        else:
            return (TEmpty(), sx[len(se.value):]) if len(sx) != 0 and a == sx[:len(se.value)] else (Fail(), sx)
    elif isinstance(se, Nonterm):
        return parsing(se.value, sx)
    elif isinstance(se, Seq):
        def closure(v, exp):
            if isinstance(v[0], Fail):
                return v
            elif isinstance(v[0], TSeq):
                t = parsing(exp, v[1])
                if isinstance(t[0], Fail):
                    return (Fail(), sx)
                elif isinstance(t[0], TEmpty):
                    return (v[0], t[1])
                else:
                    return ((TSeq([t[0]] + v[0].vlist)), t[1])#subtをoldsubtの末尾に付くように直す
            else:
                return (Fail(), v[1])

        (result, ys) = functools.reduce(closure, (TSeq([]), sx), se.elist)
        if isinstance(result, TSeq):
            if len(result.vlist) == 0:
                return (TEmpty(), ys)
            elif len(result.vlist) == 1:
                return (result.vlist[0], ys)
            else:
                return (TSeq(reversed(result.vlist)), ys)
        else:
            return (result, ys)
    elif isinstance(se, Alt):
        t = parsing(se.e1, sx)
        if isinstance(t[0], Fail):
            return parsing(se.e2, sx)
        else:
            return t
    elif isinstance(se, Not):
        t = parsing(se.e, sx)
        if isinstance(t[0], Fail):
            return (TEmpty(), sx)
        else:
            return (Fail(), sx)
    elif isinstance(se, Rep):
        inputstr = sx
        treelist = []
        loopcondition = True
        while loopcondition:
            (newtree, newstr) = parsing(se.e, inputstr)
            #printfn "%A" newstr
            inputstr = newstr
            if isinstance(newtree, Fail):
                loopcondition = False
            if (not isinstance(newtree, TEmpty)) and (not isinstance(newtree, Fail)):
                treelist.append(newtree)
        if len(treelist) == 0:
            return (TEmpty, inputstr)
        elif len(treelist) == 1:
            return (treelist[0], inputstr)
        else:
            return (TConcat(treelist), inputstr)
    elif isinstance(se, Cap):
        (v, ys) = parsing(se.e, sx)
        if isinstance(v, Fail):
            return (Fail(), sx)
        else:
            return (TNode(se.l, v), ys)
    elif isinstance(se, FCap):
        (v, ys) = parsing(se.e1, sx)
        if isinstance(v, Fail):
            return (Fail(), sx)
        else:
            (w, zs) = parsing(Seq([se.e2, Rep(se.e2)]), ys)
            if isinstance(w, TSeq):
                foldTree = reduce(lambda lTree, rTree: TNode(se.l, TConcat([lTree, rTree])), v, w.vlist)
                return (foldTree, zs)
            else:
                return (v, ys)
    elif isinstance(se, Leaf):
        lenE = len(se.e)
        #printfn "call-, %A" lenE
        return (TLeaf(se.l, se.e), sx[lenE:]) if len(sx) != 0 and se.e == sx[:lenE] else (Fail(), sx)
