from Expressions import *
from RET import *
import random
import functools

def randomStr(l):
    chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
    charsLen = len(chars)

    randomChars = (chars[random.randrange(charsLen)] for i in range(l))
    return "".join(randomChars)

def freshName (chi, str):
    candidate = str + randomStr(2)
    if candidate in chi:
        return freshName(chi, candidate)
    else:
        chi.add(candidate)
        return candidate

class TypingContext:
    def __init__(self, chi, gamma):
        self.chi = chi
        self.gamma = gamma

def unzip(li):
    s = []
    t = []
    for (i, j) in li:
        s.append(i)
        t.append(j)

    return (s, t)

def addChi(ctx: TypingContext, str):
    ctx.chi.add(str)
def addGamma(ctx: TypingContext, e, shape):
    ctx.gamma[e] = shape
def join(p, q):
    p.update(q)
    return p

#    getShape ::
#    TypingContext := {gamma, chi}
#    CPEGExpression := expression to be typed
#    RET := Type for CPEGExpression
#    MAP(String to RET) := global set of RET

def getShape(ctx: TypingContext, arg: CPEGExp):
    if isinstance(arg, Empty) or isinstance(arg, Terminal) or isinstance(arg, Not):
        return (RETEmpty(), {})
    elif isinstance(arg, Nonterm):
        if arg in ctx.gamma:
            return (X, {})
        else:
            Xa = freshName(ctx.chi, "")
            addChi(ctx, Xa)
            addGamma(ctx, arg, RETVar(Xa))
            (t, globSet) = getShape(ctx, pg)
            globSet[Xa] = t
            return (RETVar(Xa), grobSet)
    elif isinstance(arg, Seq):
        (a, bx) = unzip(functools.reduce(lambda T, e: [getShape(ctx, e)] + T, reversed(arg.elist), []))
        return (RETConcat(a), functools.reduce(lambda p, q: join(p, q), bx, {}))
    elif isinstance(arg, Alt):
        (shape1, E1) = getShape(ctx, arg.e1)
        (shape2, E2) = getShape(ctx, arg.e2)
        if isinstance(shape1, RETErr):
            return (RETErr(), E1)
        elif isinstance(shape2, RETErr):
            return (RETErr(), E2)
        else:
            return (RETUnion(shape1, shape2), join(E1, E2))
    elif isinstance(arg, Rep):
        (shape0, E0) = getShape(ctx, arg.value)
        if isinstance(shape0, RETEmpty) or isinstance(shape0, RETErr):
            return (shape0, E0)
        else:
            return (RETRep(shape0), E0)
    elif isinstance(arg, Cap):
        (shape, E0) = getShape(ctx, arg.e)
        return (RETLabel(arg.l, shape), E0)
    elif isinstance(arg, FCap):
        (shape1, E1) = getShape(ctx, arg.e1)
        (shape2, E2) = getShape(ctx, arg.e2)
        Xa = freshName(ctx.chi, "")
        addChi(ctx, Xa)
        newE = join(E1, E2)
        newE[Xa] = RETUnion(RETLabel(arg.l, (RETConcat([RETVar(Xa), shape2]))), shape1)
        return (RETVar(Xa), newE)
    elif isinstance(arg, Leaf):
        return (RETLeaf(arg.l), {})
        #Type checking for sigma is altavatively executed by F# type system
    else:
        print(type(arg), "ERROR")
        return (RETEmpty(), {})
