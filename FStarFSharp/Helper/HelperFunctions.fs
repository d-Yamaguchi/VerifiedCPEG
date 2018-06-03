#light "off"
module HelperFunctions
open Prims
open FStar.Pervasives

let rec length = (fun ( l  :  'a Prims.list ) -> (match (l) with
| [] -> begin
(Prims.parse_int "0")
end
| (uu____20)::tl -> begin
((Prims.parse_int "1") + (length tl))
end))


let rec append = (fun ( l1  :  'a Prims.list ) ( l2  :  'a Prims.list ) -> (match (l1) with
| [] -> begin
l2
end
| (hd)::tl -> begin
(hd)::(append tl l2)
end))


let rec take = (fun ( n  :  Prims.nat ) ( s  :  'a Prims.list ) -> (match (n) with
| _0_2 when (_0_2 = (Prims.parse_int "0")) -> begin
[]
end
| m -> begin
(match (s) with
| [] -> begin
[]
end
| (hd)::tl -> begin
(hd)::(take (m - (Prims.parse_int "1")) tl)
end)
end))


let rec drop = (fun ( n  :  Prims.nat ) ( s  :  'a Prims.list ) -> (match (n) with
| _0_3 when (_0_3 = (Prims.parse_int "0")) -> begin
s
end
| m -> begin
(match (s) with
| [] -> begin
[]
end
| (hd)::tl -> begin
(drop (m - (Prims.parse_int "1")) tl)
end)
end))




