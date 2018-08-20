from RET import *

#type CType =
#    | TauString
#    | TauProduct of CType list
#    | TauList of CType
#    | TauVar of string
#    | TauUnit

#type MVariant = string * CType list

class CType:
    def __init__(self):
        pass

class TauString(CType):
    def __init__(self):
        pass

class TauProduct(CType):
    def __init__(self, v):
        self.value = v

class TauList(CType):
    def __init__(self, v):
        self.value = v

class TauVar(CType):
    def __init__(self, v):
        self.value = v

class TauUnit(CType):
    def __init__(self):
        pass

class MVariant:
    def __init__(self, v1, v2):
        self.value1 = v1
        self.value2 = v2

#def removeItemFromList(item, list):
#    if len(list) == 0:
#        return list
#    elif list[0] == item:
#        return list[1:]
#    else:
#        return [list[0]] + removeItemFromList(item, list[1:])

#def RET2CType(arg):
#    if isinstance(arg, RETEmpty):
#        return TauUnit()
#    elif isinstance(arg, RETConcat):
#        return TauProduct()
#    elif isinstance(arg, RETUnion):
#        return RET2MVariant(arg.value1)
