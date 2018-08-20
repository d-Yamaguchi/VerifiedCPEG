#type RET =
#    | RETEmpty
#    | RETConcat of RET list
#    | RETUnion of RET * RET
#    | RETRep of RET
#    | RETLabel of string * RET
#    | RETVar of string
#    | RETErr
#    | RETLeaf of string

class RET:
    def __init__(self):
        pass

class RETEmpty(RET):
    def __init__(self):
        pass

    def __repr__(self):
        return "RETEmpty"

class RETConcat(RET):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "RETConcat %s" % self.value

class RETUnion(RET):
    def __init__(self, v1, v2):
        self.value1 = v1
        self.value2 = v2

    def __repr__(self):
        return "RETUnion (%s, %s)" % (self.value1, self.value2)

class RETRep(RET):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "RETRep %s" % self.value

class RETLabel(RET):
    def __init__(self, v1, v2):
        self.value1 = v1
        self.value2 = v2

    def __repr__(self):
        return "RETLabel (\"%s\", %s)" % (self.value1, self.value2)

class RETVar(RET):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "RETVar \"%s\"" % self.value

class RETErr(RET):
    def __init__(self):
        pass

    def __repr__(self):
        return "RETErr"

class RETLeaf(RET):
    def __init__(self, v):
        self.value = v

    def __repr__(self):
        return "RETLeaf \"%s\"" % self.value
