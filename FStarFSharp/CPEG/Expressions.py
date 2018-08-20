#primTypes = [typeof<sbyte>; typeof<int16>; typeof<uint16>; typeof<int>; typeof<uint32>;
#                 typeof<int64>; typeof<uint64>;
#                 typeof<char>; typeof<string>; typeof<decimal>; typeof<float32>; typeof<float>;
#                 typeof<single>; typeof<double>]

# let getPrimType (tag:string) = match List.tryFind (fun t -> t.Name.ToLower() = tag.ToLower()) primTypes with
#     | None -> typeof<string>
#     | Some x -> x

#type CPEGExp =
#    | Empty
#    | Terminal of string
#    | Nonterm of CPEGExp
#    | Seq of elist : CPEGExp list
#    | Alt of e1 : CPEGExp * e2 : CPEGExp
#    | Rep of e : CPEGExp
#    | Not of e : CPEGExp
#    | Cap of l : string * e : CPEGExp
#    | FCap of e1: CPEGExp * l: string * e2: CPEGExp
#    | Leaf of l: string * e: string

class CPEGExp:
    def __div__(self, other):
        return Alt(self, other)

class Empty(CPEGExp):
    def __repr__(self):
        return "Empty"

class Terminal(CPEGExp):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "Terminal %s" % self.value

class Nonterm(CPEGExp):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "Nonterm %s" % self.value

class Seq(CPEGExp):
    def __init__(self, v):
        self.elist = v

    def __repr__(self):
        return "Seq %s" % self.elist

class Alt(CPEGExp):
    def __init__(self, v1, v2):
        self.e1 = v1
        self.e2 = v2

    def __repr__(self):
        return "Alt (%s, %s)" % (self.e1, self.e2)

class Rep(CPEGExp):
    def __init__(self, v):
        self.e = v

    def __repr__(self):
        return "Rep %s" % self.e

class Not(CPEGExp):
    def __init__(self, v):
        self.e = v

    def __repr__(self):
        return "Not %s" % self.e

class Cap(CPEGExp):
    def __init__(self, v1, v2):
        self.l = v1
        self.e = v2

    def __repr__(self):
        return "Cap (%s, %s)" % (self.l, self.e)

class FCap(CPEGExp):
    def __init__(self, v1, v2, v3):
        self.e1 = v1
        self.l = v2
        self.e2 = v3

    def __repr__(self):
        return "FCap (%s, %s, %s)" % (self.e1, self.l, self.e2)

class Leaf(CPEGExp):
    def __init__(self, v1, v2):
        self.l = v1
        self.e = v2

    def __repr__(self):
        return "Leaf (%s, %s)" % (self.l, self.e)
