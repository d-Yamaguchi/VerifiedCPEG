#light "off"
module FStar.Pervasives
open Prims

type 'Aheap st_pre_h =
'Aheap  ->  obj


type ('Aheap, 'Aa) st_post_h =
'Aa  ->  'Aheap  ->  obj


type ('Aheap, 'Aa) st_wp_h =
Prims.unit  ->  'Aheap st_pre_h


type ('Aheap, 'Aa, 'Ax, 'Ap, 'Auu____55) st_return =
'Ap


type ('Aheap, 'Ar1, 'Aa, 'Ab, 'Awp1, 'Awp2, 'Ap, 'Ah0) st_bind_wp =
'Awp1


type ('Aheap, 'Aa, 'Ap, 'Awp_then, 'Awp_else, 'Apost, 'Ah0) st_if_then_else =
Prims.unit


type ('Aheap, 'Aa, 'Awp, 'Apost, 'Ah0) st_ite_wp =
Prims.unit


type ('Aheap, 'Aa, 'Awp1, 'Awp2) st_stronger =
Prims.unit


type ('Aheap, 'Aa, 'Ab, 'Awp, 'Ap, 'Ah) st_close_wp =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Awp, 'Aq, 'Ah) st_assert_p =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Awp, 'Aq, 'Ah) st_assume_p =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Ah) st_null_wp =
Prims.unit


type ('Aheap, 'Aa, 'Awp) st_trivial =
Prims.unit

type 'Aa result =
| V of 'Aa
| E of Prims.exn
| Err of Prims.string


let uu___is_V = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| V (v) -> begin
true
end
| uu____552 -> begin
false
end))


let __proj__V__item__v = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| V (v) -> begin
v
end))


let uu___is_E = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| E (e) -> begin
true
end
| uu____594 -> begin
false
end))


let __proj__E__item__e = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| E (e) -> begin
e
end))


let uu___is_Err = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| Err (msg) -> begin
true
end
| uu____636 -> begin
false
end))


let __proj__Err__item__msg = (fun ( projectee  :  'Aa result ) -> (match (projectee) with
| Err (msg) -> begin
msg
end))


type ex_pre =
obj


type 'Aa ex_post =
'Aa result  ->  obj


type 'Aa ex_wp =
Prims.unit  ->  ex_pre


type ('Aa, 'Ax, 'Ap) ex_return =
'Ap


type ('Ar1, 'Aa, 'Ab, 'Awp1, 'Awp2, 'Ap) ex_bind_wp =
Prims.unit


type ('Aa, 'Awp, 'Apost) ex_ite_wp =
Prims.unit


type ('Aa, 'Ap, 'Awp_then, 'Awp_else, 'Apost) ex_if_then_else =
Prims.unit


type ('Aa, 'Awp1, 'Awp2) ex_stronger =
Prims.unit


type ('Aa, 'Ab, 'Awp, 'Ap) ex_close_wp =
Prims.unit


type ('Aa, 'Aq, 'Awp, 'Ap) ex_assert_p =
Prims.unit


type ('Aa, 'Aq, 'Awp, 'Ap) ex_assume_p =
Prims.unit


type ('Aa, 'Ap) ex_null_wp =
Prims.unit


type ('Aa, 'Awp) ex_trivial =
'Awp


type ('Aa, 'Awp, 'Ap) lift_div_exn =
'Awp


type 'Ah all_pre_h =
'Ah  ->  obj


type ('Ah, 'Aa) all_post_h =
'Aa result  ->  'Ah  ->  obj


type ('Ah, 'Aa) all_wp_h =
Prims.unit  ->  'Ah all_pre_h


type ('Aheap, 'Aa, 'Awp, 'Apost, 'Ah0) all_ite_wp =
Prims.unit


type ('Aheap, 'Aa, 'Ax, 'Ap, 'Auu____1173) all_return =
'Ap


type ('Aheap, 'Ar1, 'Aa, 'Ab, 'Awp1, 'Awp2, 'Ap, 'Ah0) all_bind_wp =
'Awp1


type ('Aheap, 'Aa, 'Ap, 'Awp_then, 'Awp_else, 'Apost, 'Ah0) all_if_then_else =
Prims.unit


type ('Aheap, 'Aa, 'Awp1, 'Awp2) all_stronger =
Prims.unit


type ('Aheap, 'Aa, 'Ab, 'Awp, 'Ap, 'Ah) all_close_wp =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Awp, 'Aq, 'Ah) all_assert_p =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Awp, 'Aq, 'Ah) all_assume_p =
Prims.unit


type ('Aheap, 'Aa, 'Ap, 'Ah0) all_null_wp =
Prims.unit


type ('Aheap, 'Aa, 'Awp) all_trivial =
Prims.unit


type 'Aa inversion =
Prims.unit


let allow_inversion = ()


let invertOption = (fun ( uu____1569  :  Prims.unit ) -> ())

type ('a, 'b) either =
| Inl of 'a
| Inr of 'b


let uu___is_Inl = (fun ( projectee  :  ('a, 'b) either ) -> (match (projectee) with
| Inl (v) -> begin
true
end
| uu____1620 -> begin
false
end))


let __proj__Inl__item__v = (fun ( projectee  :  ('a, 'b) either ) -> (match (projectee) with
| Inl (v) -> begin
v
end))


let uu___is_Inr = (fun ( projectee  :  ('a, 'b) either ) -> (match (projectee) with
| Inr (v) -> begin
true
end
| uu____1680 -> begin
false
end))


let __proj__Inr__item__v = (fun ( projectee  :  ('a, 'b) either ) -> (match (projectee) with
| Inr (v) -> begin
v
end))


let dfst = (fun ( t  :  ('Aa, 'Ab) Prims.dtuple2 ) -> (Prims.__proj__Mkdtuple2__item___1 t))


let dsnd = (fun ( t  :  ('Aa, 'Ab) Prims.dtuple2 ) -> (Prims.__proj__Mkdtuple2__item___2 t))

type ('Aa, 'Ab, 'Ac) dtuple3 =
| Mkdtuple3 of 'Aa * 'Ab * 'Ac


let uu___is_Mkdtuple3 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac) dtuple3 ) -> true)


let __proj__Mkdtuple3__item___1 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac) dtuple3 ) -> (match (projectee) with
| Mkdtuple3 (_1, _2, _3) -> begin
_1
end))


let __proj__Mkdtuple3__item___2 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac) dtuple3 ) -> (match (projectee) with
| Mkdtuple3 (_1, _2, _3) -> begin
_2
end))


let __proj__Mkdtuple3__item___3 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac) dtuple3 ) -> (match (projectee) with
| Mkdtuple3 (_1, _2, _3) -> begin
_3
end))

type ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 =
| Mkdtuple4 of 'Aa * 'Ab * 'Ac * 'Ad


let uu___is_Mkdtuple4 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 ) -> true)


let __proj__Mkdtuple4__item___1 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 ) -> (match (projectee) with
| Mkdtuple4 (_1, _2, _3, _4) -> begin
_1
end))


let __proj__Mkdtuple4__item___2 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 ) -> (match (projectee) with
| Mkdtuple4 (_1, _2, _3, _4) -> begin
_2
end))


let __proj__Mkdtuple4__item___3 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 ) -> (match (projectee) with
| Mkdtuple4 (_1, _2, _3, _4) -> begin
_3
end))


let __proj__Mkdtuple4__item___4 = (fun ( projectee  :  ('Aa, 'Ab, 'Ac, 'Ad) dtuple4 ) -> (match (projectee) with
| Mkdtuple4 (_1, _2, _3, _4) -> begin
_4
end))


let ignore = (fun ( x  :  'Aa ) -> ())


let rec false_elim = (fun ( u  :  Prims.unit ) -> (false_elim ()))

type __internal_ocaml_attributes =
| PpxDerivingShow
| PpxDerivingShowConstant of Prims.string


let uu___is_PpxDerivingShow : __internal_ocaml_attributes  ->  Prims.bool = (fun ( projectee  :  __internal_ocaml_attributes ) -> (match (projectee) with
| PpxDerivingShow -> begin
true
end
| uu____2494 -> begin
false
end))


let uu___is_PpxDerivingShowConstant : __internal_ocaml_attributes  ->  Prims.bool = (fun ( projectee  :  __internal_ocaml_attributes ) -> (match (projectee) with
| PpxDerivingShowConstant (_0) -> begin
true
end
| uu____2503 -> begin
false
end))


let __proj__PpxDerivingShowConstant__item___0 : __internal_ocaml_attributes  ->  Prims.string = (fun ( projectee  :  __internal_ocaml_attributes ) -> (match (projectee) with
| PpxDerivingShowConstant (_0) -> begin
_0
end))




