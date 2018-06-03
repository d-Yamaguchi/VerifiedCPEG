#light "off"
module FStar.Pervasives.Native
open Prims
type 'Aa option =
| None
| Some of 'Aa


let uu___is_None = (fun ( projectee  :  'Aa option ) -> (match (projectee) with
| None -> begin
true
end
| uu____28 -> begin
false
end))


let uu___is_Some = (fun ( projectee  :  'Aa option ) -> (match (projectee) with
| Some (v) -> begin
true
end
| uu____49 -> begin
false
end))


let __proj__Some__item__v = (fun ( projectee  :  'Aa option ) -> (match (projectee) with
| Some (v) -> begin
v
end))

type ('a, 'b) tuple2 =
| Mktuple2 of 'a * 'b


let uu___is_Mktuple2 = (fun ( projectee  :  ('a * 'b) ) -> true)


let __proj__Mktuple2__item___1 = (fun ( projectee  :  ('a * 'b) ) -> (match (projectee) with
| (_1, _2) -> begin
_1
end))


let __proj__Mktuple2__item___2 = (fun ( projectee  :  ('a * 'b) ) -> (match (projectee) with
| (_1, _2) -> begin
_2
end))


let fst = (fun ( x  :  ('a * 'b) ) -> (__proj__Mktuple2__item___1 x))


let snd = (fun ( x  :  ('a * 'b) ) -> (__proj__Mktuple2__item___2 x))

type ('a, 'b, 'c) tuple3 =
| Mktuple3 of 'a * 'b * 'c


let uu___is_Mktuple3 = (fun ( projectee  :  ('a * 'b * 'c) ) -> true)


let __proj__Mktuple3__item___1 = (fun ( projectee  :  ('a * 'b * 'c) ) -> (match (projectee) with
| (_1, _2, _3) -> begin
_1
end))


let __proj__Mktuple3__item___2 = (fun ( projectee  :  ('a * 'b * 'c) ) -> (match (projectee) with
| (_1, _2, _3) -> begin
_2
end))


let __proj__Mktuple3__item___3 = (fun ( projectee  :  ('a * 'b * 'c) ) -> (match (projectee) with
| (_1, _2, _3) -> begin
_3
end))

type ('a, 'b, 'c, 'd) tuple4 =
| Mktuple4 of 'a * 'b * 'c * 'd


let uu___is_Mktuple4 = (fun ( projectee  :  ('a * 'b * 'c * 'd) ) -> true)


let __proj__Mktuple4__item___1 = (fun ( projectee  :  ('a * 'b * 'c * 'd) ) -> (match (projectee) with
| (_1, _2, _3, _4) -> begin
_1
end))


let __proj__Mktuple4__item___2 = (fun ( projectee  :  ('a * 'b * 'c * 'd) ) -> (match (projectee) with
| (_1, _2, _3, _4) -> begin
_2
end))


let __proj__Mktuple4__item___3 = (fun ( projectee  :  ('a * 'b * 'c * 'd) ) -> (match (projectee) with
| (_1, _2, _3, _4) -> begin
_3
end))


let __proj__Mktuple4__item___4 = (fun ( projectee  :  ('a * 'b * 'c * 'd) ) -> (match (projectee) with
| (_1, _2, _3, _4) -> begin
_4
end))

type ('a, 'b, 'c, 'd, 'e) tuple5 =
| Mktuple5 of 'a * 'b * 'c * 'd * 'e


let uu___is_Mktuple5 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> true)


let __proj__Mktuple5__item___1 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5) -> begin
_1
end))


let __proj__Mktuple5__item___2 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5) -> begin
_2
end))


let __proj__Mktuple5__item___3 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5) -> begin
_3
end))


let __proj__Mktuple5__item___4 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5) -> begin
_4
end))


let __proj__Mktuple5__item___5 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5) -> begin
_5
end))

type ('a, 'b, 'c, 'd, 'e, 'f) tuple6 =
| Mktuple6 of 'a * 'b * 'c * 'd * 'e * 'f


let uu___is_Mktuple6 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> true)


let __proj__Mktuple6__item___1 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_1
end))


let __proj__Mktuple6__item___2 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_2
end))


let __proj__Mktuple6__item___3 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_3
end))


let __proj__Mktuple6__item___4 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_4
end))


let __proj__Mktuple6__item___5 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_5
end))


let __proj__Mktuple6__item___6 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6) -> begin
_6
end))

type ('a, 'b, 'c, 'd, 'e, 'f, 'g) tuple7 =
| Mktuple7 of 'a * 'b * 'c * 'd * 'e * 'f * 'g


let uu___is_Mktuple7 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> true)


let __proj__Mktuple7__item___1 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_1
end))


let __proj__Mktuple7__item___2 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_2
end))


let __proj__Mktuple7__item___3 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_3
end))


let __proj__Mktuple7__item___4 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_4
end))


let __proj__Mktuple7__item___5 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_5
end))


let __proj__Mktuple7__item___6 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_6
end))


let __proj__Mktuple7__item___7 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7) -> begin
_7
end))

type ('a, 'b, 'c, 'd, 'e, 'f, 'g, 'h) tuple8 =
| Mktuple8 of 'a * 'b * 'c * 'd * 'e * 'f * 'g * 'h


let uu___is_Mktuple8 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> true)


let __proj__Mktuple8__item___1 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_1
end))


let __proj__Mktuple8__item___2 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_2
end))


let __proj__Mktuple8__item___3 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_3
end))


let __proj__Mktuple8__item___4 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_4
end))


let __proj__Mktuple8__item___5 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_5
end))


let __proj__Mktuple8__item___6 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_6
end))


let __proj__Mktuple8__item___7 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_7
end))


let __proj__Mktuple8__item___8 = (fun ( projectee  :  ('a * 'b * 'c * 'd * 'e * 'f * 'g * 'h) ) -> (match (projectee) with
| (_1, _2, _3, _4, _5, _6, _7, _8) -> begin
_8
end))




