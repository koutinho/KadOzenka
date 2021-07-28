/*boolin 1*/CREATE OR REPLACE FUNCTION pg_catalog.boolin(cstring)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boolin$function$;/*boolout 2*/CREATE OR REPLACE FUNCTION pg_catalog.boolout(boolean)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boolout$function$;/*byteain 3*/CREATE OR REPLACE FUNCTION pg_catalog.byteain(cstring)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteain$function$;/*byteaout 4*/CREATE OR REPLACE FUNCTION pg_catalog.byteaout(bytea)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaout$function$;/*charin 5*/CREATE OR REPLACE FUNCTION pg_catalog.charin(cstring)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$charin$function$;/*charout 6*/CREATE OR REPLACE FUNCTION pg_catalog.charout("char")
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$charout$function$;/*namein 7*/CREATE OR REPLACE FUNCTION pg_catalog.namein(cstring)
 RETURNS name
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$namein$function$;/*nameout 8*/CREATE OR REPLACE FUNCTION pg_catalog.nameout(name)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$nameout$function$;/*int2in 9*/CREATE OR REPLACE FUNCTION pg_catalog.int2in(cstring)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2in$function$;/*int2out 10*/CREATE OR REPLACE FUNCTION pg_catalog.int2out(smallint)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2out$function$;/*int2vectorin 11*/CREATE OR REPLACE FUNCTION pg_catalog.int2vectorin(cstring)
 RETURNS int2vector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2vectorin$function$;/*int2vectorout 12*/CREATE OR REPLACE FUNCTION pg_catalog.int2vectorout(int2vector)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2vectorout$function$;/*int4in 13*/CREATE OR REPLACE FUNCTION pg_catalog.int4in(cstring)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4in$function$;/*int4out 14*/CREATE OR REPLACE FUNCTION pg_catalog.int4out(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4out$function$;/*regprocin 15*/CREATE OR REPLACE FUNCTION pg_catalog.regprocin(cstring)
 RETURNS regproc
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regprocin$function$;/*regprocout 16*/CREATE OR REPLACE FUNCTION pg_catalog.regprocout(regproc)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regprocout$function$;/*to_regproc 17*/CREATE OR REPLACE FUNCTION pg_catalog.to_regproc(text)
 RETURNS regproc
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regproc$function$;/*to_regprocedure 18*/CREATE OR REPLACE FUNCTION pg_catalog.to_regprocedure(text)
 RETURNS regprocedure
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regprocedure$function$;/*textin 19*/CREATE OR REPLACE FUNCTION pg_catalog.textin(cstring)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textin$function$;/*textout 20*/CREATE OR REPLACE FUNCTION pg_catalog.textout(text)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textout$function$;/*tidin 21*/CREATE OR REPLACE FUNCTION pg_catalog.tidin(cstring)
 RETURNS tid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidin$function$;/*tidout 22*/CREATE OR REPLACE FUNCTION pg_catalog.tidout(tid)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidout$function$;/*xidin 23*/CREATE OR REPLACE FUNCTION pg_catalog.xidin(cstring)
 RETURNS xid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xidin$function$;/*xidout 24*/CREATE OR REPLACE FUNCTION pg_catalog.xidout(xid)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xidout$function$;/*xid8in 25*/CREATE OR REPLACE FUNCTION pg_catalog.xid8in(cstring)
 RETURNS xid8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xid8in$function$;/*xid8out 26*/CREATE OR REPLACE FUNCTION pg_catalog.xid8out(xid8)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xid8out$function$;/*xid8recv 27*/CREATE OR REPLACE FUNCTION pg_catalog.xid8recv(internal)
 RETURNS xid8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xid8recv$function$;/*xid8send 28*/CREATE OR REPLACE FUNCTION pg_catalog.xid8send(xid8)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xid8send$function$;/*cidin 29*/CREATE OR REPLACE FUNCTION pg_catalog.cidin(cstring)
 RETURNS cid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidin$function$;/*cidout 30*/CREATE OR REPLACE FUNCTION pg_catalog.cidout(cid)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidout$function$;/*oidvectorin 31*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorin(cstring)
 RETURNS oidvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidvectorin$function$;/*oidvectorout 32*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorout(oidvector)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidvectorout$function$;/*boollt 33*/CREATE OR REPLACE FUNCTION pg_catalog.boollt(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$boollt$function$;/*boolgt 34*/CREATE OR REPLACE FUNCTION pg_catalog.boolgt(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$boolgt$function$;/*booleq 35*/CREATE OR REPLACE FUNCTION pg_catalog.booleq(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$booleq$function$;/*chareq 36*/CREATE OR REPLACE FUNCTION pg_catalog.chareq("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$chareq$function$;/*nameeq 37*/CREATE OR REPLACE FUNCTION pg_catalog.nameeq(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$nameeq$function$;/*int2eq 38*/CREATE OR REPLACE FUNCTION pg_catalog.int2eq(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2eq$function$;/*int2lt 39*/CREATE OR REPLACE FUNCTION pg_catalog.int2lt(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2lt$function$;/*int4eq 40*/CREATE OR REPLACE FUNCTION pg_catalog.int4eq(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4eq$function$;/*int4lt 41*/CREATE OR REPLACE FUNCTION pg_catalog.int4lt(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4lt$function$;/*texteq 42*/CREATE OR REPLACE FUNCTION pg_catalog.texteq(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$texteq$function$;/*starts_with 43*/CREATE OR REPLACE FUNCTION pg_catalog.starts_with(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_starts_with$function$;/*xideq 44*/CREATE OR REPLACE FUNCTION pg_catalog.xideq(xid, xid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xideq$function$;/*xidneq 45*/CREATE OR REPLACE FUNCTION pg_catalog.xidneq(xid, xid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xidneq$function$;/*xid8eq 46*/CREATE OR REPLACE FUNCTION pg_catalog.xid8eq(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8eq$function$;/*xid8ne 47*/CREATE OR REPLACE FUNCTION pg_catalog.xid8ne(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8ne$function$;/*xid8lt 48*/CREATE OR REPLACE FUNCTION pg_catalog.xid8lt(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8lt$function$;/*xid8gt 49*/CREATE OR REPLACE FUNCTION pg_catalog.xid8gt(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8gt$function$;/*xid8le 50*/CREATE OR REPLACE FUNCTION pg_catalog.xid8le(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8le$function$;/*xid8ge 51*/CREATE OR REPLACE FUNCTION pg_catalog.xid8ge(xid8, xid8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8ge$function$;/*xid8cmp 52*/CREATE OR REPLACE FUNCTION pg_catalog.xid8cmp(xid8, xid8)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xid8cmp$function$;/*xid 53*/CREATE OR REPLACE FUNCTION pg_catalog.xid(xid8)
 RETURNS xid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xid8toxid$function$;/*cideq 54*/CREATE OR REPLACE FUNCTION pg_catalog.cideq(cid, cid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cideq$function$;/*charne 55*/CREATE OR REPLACE FUNCTION pg_catalog.charne("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$charne$function$;/*charlt 56*/CREATE OR REPLACE FUNCTION pg_catalog.charlt("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$charlt$function$;/*charle 57*/CREATE OR REPLACE FUNCTION pg_catalog.charle("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$charle$function$;/*chargt 58*/CREATE OR REPLACE FUNCTION pg_catalog.chargt("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$chargt$function$;/*charge 59*/CREATE OR REPLACE FUNCTION pg_catalog.charge("char", "char")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$charge$function$;/*int4 60*/CREATE OR REPLACE FUNCTION pg_catalog.int4("char")
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$chartoi4$function$;/*char 61*/CREATE OR REPLACE FUNCTION pg_catalog."char"(integer)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i4tochar$function$;/*nameregexeq 62*/CREATE OR REPLACE FUNCTION pg_catalog.nameregexeq(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textregexeq_support
AS $function$nameregexeq$function$;/*nameregexne 63*/CREATE OR REPLACE FUNCTION pg_catalog.nameregexne(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$nameregexne$function$;/*textregexeq 64*/CREATE OR REPLACE FUNCTION pg_catalog.textregexeq(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textregexeq_support
AS $function$textregexeq$function$;/*textregexne 65*/CREATE OR REPLACE FUNCTION pg_catalog.textregexne(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexne$function$;/*textregexeq_support 66*/CREATE OR REPLACE FUNCTION pg_catalog.textregexeq_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexeq_support$function$;/*textlen 67*/CREATE OR REPLACE FUNCTION pg_catalog.textlen(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textlen$function$;/*textcat 68*/CREATE OR REPLACE FUNCTION pg_catalog.textcat(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textcat$function$;/*boolne 69*/CREATE OR REPLACE FUNCTION pg_catalog.boolne(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$boolne$function$;/*version 70*/CREATE OR REPLACE FUNCTION pg_catalog.version()
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pgsql_version$function$;/*pg_ddl_command_in 71*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ddl_command_in(cstring)
 RETURNS pg_ddl_command
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ddl_command_in$function$;/*pg_ddl_command_out 72*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ddl_command_out(pg_ddl_command)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ddl_command_out$function$;/*pg_ddl_command_recv 73*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ddl_command_recv(internal)
 RETURNS pg_ddl_command
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ddl_command_recv$function$;/*pg_ddl_command_send 74*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ddl_command_send(pg_ddl_command)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ddl_command_send$function$;/*eqsel 75*/CREATE OR REPLACE FUNCTION pg_catalog.eqsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$eqsel$function$;/*neqsel 76*/CREATE OR REPLACE FUNCTION pg_catalog.neqsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$neqsel$function$;/*scalarltsel 77*/CREATE OR REPLACE FUNCTION pg_catalog.scalarltsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalarltsel$function$;/*scalargtsel 78*/CREATE OR REPLACE FUNCTION pg_catalog.scalargtsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalargtsel$function$;/*eqjoinsel 79*/CREATE OR REPLACE FUNCTION pg_catalog.eqjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$eqjoinsel$function$;/*neqjoinsel 80*/CREATE OR REPLACE FUNCTION pg_catalog.neqjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$neqjoinsel$function$;/*scalarltjoinsel 81*/CREATE OR REPLACE FUNCTION pg_catalog.scalarltjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalarltjoinsel$function$;/*scalargtjoinsel 82*/CREATE OR REPLACE FUNCTION pg_catalog.scalargtjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalargtjoinsel$function$;/*scalarlesel 83*/CREATE OR REPLACE FUNCTION pg_catalog.scalarlesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalarlesel$function$;/*scalargesel 84*/CREATE OR REPLACE FUNCTION pg_catalog.scalargesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalargesel$function$;/*scalarlejoinsel 85*/CREATE OR REPLACE FUNCTION pg_catalog.scalarlejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalarlejoinsel$function$;/*scalargejoinsel 86*/CREATE OR REPLACE FUNCTION pg_catalog.scalargejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$scalargejoinsel$function$;/*unknownin 87*/CREATE OR REPLACE FUNCTION pg_catalog.unknownin(cstring)
 RETURNS unknown
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unknownin$function$;/*unknownout 88*/CREATE OR REPLACE FUNCTION pg_catalog.unknownout(unknown)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unknownout$function$;/*numeric_fac 89*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_fac(bigint)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_fac$function$;/*box_above_eq 90*/CREATE OR REPLACE FUNCTION pg_catalog.box_above_eq(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_above_eq$function$;/*box_below_eq 91*/CREATE OR REPLACE FUNCTION pg_catalog.box_below_eq(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_below_eq$function$;/*point_in 92*/CREATE OR REPLACE FUNCTION pg_catalog.point_in(cstring)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_in$function$;/*point_out 93*/CREATE OR REPLACE FUNCTION pg_catalog.point_out(point)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_out$function$;/*lseg_in 94*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_in(cstring)
 RETURNS lseg
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_in$function$;/*lseg_out 95*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_out(lseg)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_out$function$;/*path_in 96*/CREATE OR REPLACE FUNCTION pg_catalog.path_in(cstring)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_in$function$;/*path_out 97*/CREATE OR REPLACE FUNCTION pg_catalog.path_out(path)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_out$function$;/*box_in 98*/CREATE OR REPLACE FUNCTION pg_catalog.box_in(cstring)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_in$function$;/*box_out 99*/CREATE OR REPLACE FUNCTION pg_catalog.box_out(box)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_out$function$;/*box_overlap 100*/CREATE OR REPLACE FUNCTION pg_catalog.box_overlap(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_overlap$function$;/*box_ge 101*/CREATE OR REPLACE FUNCTION pg_catalog.box_ge(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_ge$function$;/*box_gt 102*/CREATE OR REPLACE FUNCTION pg_catalog.box_gt(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_gt$function$;/*box_eq 103*/CREATE OR REPLACE FUNCTION pg_catalog.box_eq(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_eq$function$;/*box_lt 104*/CREATE OR REPLACE FUNCTION pg_catalog.box_lt(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_lt$function$;/*box_le 105*/CREATE OR REPLACE FUNCTION pg_catalog.box_le(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_le$function$;/*point_above 106*/CREATE OR REPLACE FUNCTION pg_catalog.point_above(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_above$function$;/*point_left 107*/CREATE OR REPLACE FUNCTION pg_catalog.point_left(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_left$function$;/*point_right 108*/CREATE OR REPLACE FUNCTION pg_catalog.point_right(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_right$function$;/*point_below 109*/CREATE OR REPLACE FUNCTION pg_catalog.point_below(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_below$function$;/*point_eq 110*/CREATE OR REPLACE FUNCTION pg_catalog.point_eq(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_eq$function$;/*on_pb 111*/CREATE OR REPLACE FUNCTION pg_catalog.on_pb(point, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_pb$function$;/*on_ppath 112*/CREATE OR REPLACE FUNCTION pg_catalog.on_ppath(point, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_ppath$function$;/*box_center 113*/CREATE OR REPLACE FUNCTION pg_catalog.box_center(box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_center$function$;/*areasel 114*/CREATE OR REPLACE FUNCTION pg_catalog.areasel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$areasel$function$;/*areajoinsel 115*/CREATE OR REPLACE FUNCTION pg_catalog.areajoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$areajoinsel$function$;/*int4mul 116*/CREATE OR REPLACE FUNCTION pg_catalog.int4mul(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4mul$function$;/*int4ne 117*/CREATE OR REPLACE FUNCTION pg_catalog.int4ne(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4ne$function$;/*int2ne 118*/CREATE OR REPLACE FUNCTION pg_catalog.int2ne(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2ne$function$;/*int2gt 119*/CREATE OR REPLACE FUNCTION pg_catalog.int2gt(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2gt$function$;/*int4gt 120*/CREATE OR REPLACE FUNCTION pg_catalog.int4gt(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4gt$function$;/*int2le 121*/CREATE OR REPLACE FUNCTION pg_catalog.int2le(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2le$function$;/*int4le 122*/CREATE OR REPLACE FUNCTION pg_catalog.int4le(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4le$function$;/*int4ge 123*/CREATE OR REPLACE FUNCTION pg_catalog.int4ge(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int4ge$function$;/*int2ge 124*/CREATE OR REPLACE FUNCTION pg_catalog.int2ge(smallint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int2ge$function$;/*int2mul 125*/CREATE OR REPLACE FUNCTION pg_catalog.int2mul(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2mul$function$;/*int2div 126*/CREATE OR REPLACE FUNCTION pg_catalog.int2div(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2div$function$;/*int4div 127*/CREATE OR REPLACE FUNCTION pg_catalog.int4div(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4div$function$;/*int2mod 128*/CREATE OR REPLACE FUNCTION pg_catalog.int2mod(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2mod$function$;/*int4mod 129*/CREATE OR REPLACE FUNCTION pg_catalog.int4mod(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4mod$function$;/*textne 130*/CREATE OR REPLACE FUNCTION pg_catalog.textne(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textne$function$;/*int24eq 131*/CREATE OR REPLACE FUNCTION pg_catalog.int24eq(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24eq$function$;/*int42eq 132*/CREATE OR REPLACE FUNCTION pg_catalog.int42eq(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42eq$function$;/*int24lt 133*/CREATE OR REPLACE FUNCTION pg_catalog.int24lt(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24lt$function$;/*int42lt 134*/CREATE OR REPLACE FUNCTION pg_catalog.int42lt(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42lt$function$;/*int24gt 135*/CREATE OR REPLACE FUNCTION pg_catalog.int24gt(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24gt$function$;/*int42gt 136*/CREATE OR REPLACE FUNCTION pg_catalog.int42gt(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42gt$function$;/*int24ne 137*/CREATE OR REPLACE FUNCTION pg_catalog.int24ne(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24ne$function$;/*int42ne 138*/CREATE OR REPLACE FUNCTION pg_catalog.int42ne(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42ne$function$;/*int24le 139*/CREATE OR REPLACE FUNCTION pg_catalog.int24le(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24le$function$;/*int42le 140*/CREATE OR REPLACE FUNCTION pg_catalog.int42le(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42le$function$;/*int24ge 141*/CREATE OR REPLACE FUNCTION pg_catalog.int24ge(smallint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int24ge$function$;/*int42ge 142*/CREATE OR REPLACE FUNCTION pg_catalog.int42ge(integer, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int42ge$function$;/*int24mul 143*/CREATE OR REPLACE FUNCTION pg_catalog.int24mul(smallint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int24mul$function$;/*int42mul 144*/CREATE OR REPLACE FUNCTION pg_catalog.int42mul(integer, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int42mul$function$;/*int24div 145*/CREATE OR REPLACE FUNCTION pg_catalog.int24div(smallint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int24div$function$;/*int42div 146*/CREATE OR REPLACE FUNCTION pg_catalog.int42div(integer, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int42div$function$;/*int2pl 147*/CREATE OR REPLACE FUNCTION pg_catalog.int2pl(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2pl$function$;/*int4pl 148*/CREATE OR REPLACE FUNCTION pg_catalog.int4pl(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4pl$function$;/*int24pl 149*/CREATE OR REPLACE FUNCTION pg_catalog.int24pl(smallint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int24pl$function$;/*int42pl 150*/CREATE OR REPLACE FUNCTION pg_catalog.int42pl(integer, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int42pl$function$;/*int2mi 151*/CREATE OR REPLACE FUNCTION pg_catalog.int2mi(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2mi$function$;/*int4mi 152*/CREATE OR REPLACE FUNCTION pg_catalog.int4mi(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4mi$function$;/*int24mi 153*/CREATE OR REPLACE FUNCTION pg_catalog.int24mi(smallint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int24mi$function$;/*int42mi 154*/CREATE OR REPLACE FUNCTION pg_catalog.int42mi(integer, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int42mi$function$;/*oideq 155*/CREATE OR REPLACE FUNCTION pg_catalog.oideq(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oideq$function$;/*oidne 156*/CREATE OR REPLACE FUNCTION pg_catalog.oidne(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidne$function$;/*box_same 157*/CREATE OR REPLACE FUNCTION pg_catalog.box_same(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_same$function$;/*box_contain 158*/CREATE OR REPLACE FUNCTION pg_catalog.box_contain(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_contain$function$;/*box_left 159*/CREATE OR REPLACE FUNCTION pg_catalog.box_left(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_left$function$;/*box_overleft 160*/CREATE OR REPLACE FUNCTION pg_catalog.box_overleft(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_overleft$function$;/*box_overright 161*/CREATE OR REPLACE FUNCTION pg_catalog.box_overright(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_overright$function$;/*box_right 162*/CREATE OR REPLACE FUNCTION pg_catalog.box_right(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_right$function$;/*box_contained 163*/CREATE OR REPLACE FUNCTION pg_catalog.box_contained(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_contained$function$;/*box_contain_pt 164*/CREATE OR REPLACE FUNCTION pg_catalog.box_contain_pt(box, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_contain_pt$function$;/*pg_node_tree_in 165*/CREATE OR REPLACE FUNCTION pg_catalog.pg_node_tree_in(cstring)
 RETURNS pg_node_tree
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_node_tree_in$function$;/*pg_node_tree_out 166*/CREATE OR REPLACE FUNCTION pg_catalog.pg_node_tree_out(pg_node_tree)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_node_tree_out$function$;/*pg_node_tree_recv 167*/CREATE OR REPLACE FUNCTION pg_catalog.pg_node_tree_recv(internal)
 RETURNS pg_node_tree
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_node_tree_recv$function$;/*pg_node_tree_send 168*/CREATE OR REPLACE FUNCTION pg_catalog.pg_node_tree_send(pg_node_tree)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_node_tree_send$function$;/*float4in 169*/CREATE OR REPLACE FUNCTION pg_catalog.float4in(cstring)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4in$function$;/*float4out 170*/CREATE OR REPLACE FUNCTION pg_catalog.float4out(real)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4out$function$;/*float4mul 171*/CREATE OR REPLACE FUNCTION pg_catalog.float4mul(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4mul$function$;/*float4div 172*/CREATE OR REPLACE FUNCTION pg_catalog.float4div(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4div$function$;/*float4pl 173*/CREATE OR REPLACE FUNCTION pg_catalog.float4pl(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4pl$function$;/*float4mi 174*/CREATE OR REPLACE FUNCTION pg_catalog.float4mi(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4mi$function$;/*float4um 175*/CREATE OR REPLACE FUNCTION pg_catalog.float4um(real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4um$function$;/*float4abs 176*/CREATE OR REPLACE FUNCTION pg_catalog.float4abs(real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4abs$function$;/*float4_accum 177*/CREATE OR REPLACE FUNCTION pg_catalog.float4_accum(double precision[], real)
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4_accum$function$;/*float4larger 178*/CREATE OR REPLACE FUNCTION pg_catalog.float4larger(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4larger$function$;/*float4smaller 179*/CREATE OR REPLACE FUNCTION pg_catalog.float4smaller(real, real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4smaller$function$;/*int4um 180*/CREATE OR REPLACE FUNCTION pg_catalog.int4um(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4um$function$;/*int2um 181*/CREATE OR REPLACE FUNCTION pg_catalog.int2um(smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2um$function$;/*float8in 182*/CREATE OR REPLACE FUNCTION pg_catalog.float8in(cstring)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8in$function$;/*float8out 183*/CREATE OR REPLACE FUNCTION pg_catalog.float8out(double precision)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8out$function$;/*float8mul 184*/CREATE OR REPLACE FUNCTION pg_catalog.float8mul(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8mul$function$;/*float8div 185*/CREATE OR REPLACE FUNCTION pg_catalog.float8div(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8div$function$;/*float8pl 186*/CREATE OR REPLACE FUNCTION pg_catalog.float8pl(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8pl$function$;/*float8mi 187*/CREATE OR REPLACE FUNCTION pg_catalog.float8mi(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8mi$function$;/*float8um 188*/CREATE OR REPLACE FUNCTION pg_catalog.float8um(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8um$function$;/*float8abs 189*/CREATE OR REPLACE FUNCTION pg_catalog.float8abs(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8abs$function$;/*float8_accum 190*/CREATE OR REPLACE FUNCTION pg_catalog.float8_accum(double precision[], double precision)
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_accum$function$;/*float8_combine 191*/CREATE OR REPLACE FUNCTION pg_catalog.float8_combine(double precision[], double precision[])
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_combine$function$;/*float8larger 192*/CREATE OR REPLACE FUNCTION pg_catalog.float8larger(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8larger$function$;/*float8smaller 193*/CREATE OR REPLACE FUNCTION pg_catalog.float8smaller(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8smaller$function$;/*lseg_center 194*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_center(lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_center$function$;/*path_center 195*/CREATE OR REPLACE FUNCTION pg_catalog.path_center(path)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_center$function$;/*poly_center 196*/CREATE OR REPLACE FUNCTION pg_catalog.poly_center(polygon)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_center$function$;/*dround 197*/CREATE OR REPLACE FUNCTION pg_catalog.dround(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dround$function$;/*dtrunc 198*/CREATE OR REPLACE FUNCTION pg_catalog.dtrunc(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtrunc$function$;/*ceil 199*/CREATE OR REPLACE FUNCTION pg_catalog.ceil(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dceil$function$;/*ceiling 200*/CREATE OR REPLACE FUNCTION pg_catalog.ceiling(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dceil$function$;/*floor 201*/CREATE OR REPLACE FUNCTION pg_catalog.floor(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dfloor$function$;/*sign 202*/CREATE OR REPLACE FUNCTION pg_catalog.sign(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsign$function$;/*dsqrt 203*/CREATE OR REPLACE FUNCTION pg_catalog.dsqrt(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsqrt$function$;/*dcbrt 204*/CREATE OR REPLACE FUNCTION pg_catalog.dcbrt(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcbrt$function$;/*dpow 205*/CREATE OR REPLACE FUNCTION pg_catalog.dpow(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dpow$function$;/*dexp 206*/CREATE OR REPLACE FUNCTION pg_catalog.dexp(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dexp$function$;/*dlog1 207*/CREATE OR REPLACE FUNCTION pg_catalog.dlog1(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dlog1$function$;/*float8 208*/CREATE OR REPLACE FUNCTION pg_catalog.float8(smallint)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i2tod$function$;/*float4 209*/CREATE OR REPLACE FUNCTION pg_catalog.float4(smallint)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i2tof$function$;/*int2 210*/CREATE OR REPLACE FUNCTION pg_catalog.int2(double precision)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtoi2$function$;/*int2 211*/CREATE OR REPLACE FUNCTION pg_catalog.int2(real)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ftoi2$function$;/*line_distance 212*/CREATE OR REPLACE FUNCTION pg_catalog.line_distance(line, line)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_distance$function$;/*nameeqtext 213*/CREATE OR REPLACE FUNCTION pg_catalog.nameeqtext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$nameeqtext$function$;/*namelttext 214*/CREATE OR REPLACE FUNCTION pg_catalog.namelttext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namelttext$function$;/*nameletext 215*/CREATE OR REPLACE FUNCTION pg_catalog.nameletext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$nameletext$function$;/*namegetext 216*/CREATE OR REPLACE FUNCTION pg_catalog.namegetext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namegetext$function$;/*namegttext 217*/CREATE OR REPLACE FUNCTION pg_catalog.namegttext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namegttext$function$;/*namenetext 218*/CREATE OR REPLACE FUNCTION pg_catalog.namenetext(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namenetext$function$;/*btnametextcmp 219*/CREATE OR REPLACE FUNCTION pg_catalog.btnametextcmp(name, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btnametextcmp$function$;/*texteqname 220*/CREATE OR REPLACE FUNCTION pg_catalog.texteqname(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$texteqname$function$;/*textltname 221*/CREATE OR REPLACE FUNCTION pg_catalog.textltname(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textltname$function$;/*textlename 222*/CREATE OR REPLACE FUNCTION pg_catalog.textlename(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textlename$function$;/*textgename 223*/CREATE OR REPLACE FUNCTION pg_catalog.textgename(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textgename$function$;/*textgtname 224*/CREATE OR REPLACE FUNCTION pg_catalog.textgtname(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textgtname$function$;/*textnename 225*/CREATE OR REPLACE FUNCTION pg_catalog.textnename(text, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$textnename$function$;/*bttextnamecmp 226*/CREATE OR REPLACE FUNCTION pg_catalog.bttextnamecmp(text, name)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bttextnamecmp$function$;/*nameconcatoid 227*/CREATE OR REPLACE FUNCTION pg_catalog.nameconcatoid(name, oid)
 RETURNS name
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$nameconcatoid$function$;/*timeofday 228*/CREATE OR REPLACE FUNCTION pg_catalog.timeofday()
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$timeofday$function$;/*inter_sl 229*/CREATE OR REPLACE FUNCTION pg_catalog.inter_sl(lseg, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inter_sl$function$;/*inter_lb 230*/CREATE OR REPLACE FUNCTION pg_catalog.inter_lb(line, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inter_lb$function$;/*float48mul 231*/CREATE OR REPLACE FUNCTION pg_catalog.float48mul(real, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float48mul$function$;/*float48div 232*/CREATE OR REPLACE FUNCTION pg_catalog.float48div(real, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float48div$function$;/*float48pl 233*/CREATE OR REPLACE FUNCTION pg_catalog.float48pl(real, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float48pl$function$;/*float48mi 234*/CREATE OR REPLACE FUNCTION pg_catalog.float48mi(real, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float48mi$function$;/*float84mul 235*/CREATE OR REPLACE FUNCTION pg_catalog.float84mul(double precision, real)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float84mul$function$;/*float84div 236*/CREATE OR REPLACE FUNCTION pg_catalog.float84div(double precision, real)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float84div$function$;/*float84pl 237*/CREATE OR REPLACE FUNCTION pg_catalog.float84pl(double precision, real)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float84pl$function$;/*float84mi 238*/CREATE OR REPLACE FUNCTION pg_catalog.float84mi(double precision, real)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float84mi$function$;/*float4eq 239*/CREATE OR REPLACE FUNCTION pg_catalog.float4eq(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4eq$function$;/*float4ne 240*/CREATE OR REPLACE FUNCTION pg_catalog.float4ne(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4ne$function$;/*float4lt 241*/CREATE OR REPLACE FUNCTION pg_catalog.float4lt(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4lt$function$;/*float4le 242*/CREATE OR REPLACE FUNCTION pg_catalog.float4le(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4le$function$;/*float4gt 243*/CREATE OR REPLACE FUNCTION pg_catalog.float4gt(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4gt$function$;/*float4ge 244*/CREATE OR REPLACE FUNCTION pg_catalog.float4ge(real, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float4ge$function$;/*float8eq 245*/CREATE OR REPLACE FUNCTION pg_catalog.float8eq(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8eq$function$;/*float8ne 246*/CREATE OR REPLACE FUNCTION pg_catalog.float8ne(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8ne$function$;/*float8lt 247*/CREATE OR REPLACE FUNCTION pg_catalog.float8lt(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8lt$function$;/*float8le 248*/CREATE OR REPLACE FUNCTION pg_catalog.float8le(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8le$function$;/*float8gt 249*/CREATE OR REPLACE FUNCTION pg_catalog.float8gt(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8gt$function$;/*float8ge 250*/CREATE OR REPLACE FUNCTION pg_catalog.float8ge(double precision, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float8ge$function$;/*float48eq 251*/CREATE OR REPLACE FUNCTION pg_catalog.float48eq(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48eq$function$;/*float48ne 252*/CREATE OR REPLACE FUNCTION pg_catalog.float48ne(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48ne$function$;/*float48lt 253*/CREATE OR REPLACE FUNCTION pg_catalog.float48lt(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48lt$function$;/*float48le 254*/CREATE OR REPLACE FUNCTION pg_catalog.float48le(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48le$function$;/*float48gt 255*/CREATE OR REPLACE FUNCTION pg_catalog.float48gt(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48gt$function$;/*float48ge 256*/CREATE OR REPLACE FUNCTION pg_catalog.float48ge(real, double precision)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float48ge$function$;/*float84eq 257*/CREATE OR REPLACE FUNCTION pg_catalog.float84eq(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84eq$function$;/*float84ne 258*/CREATE OR REPLACE FUNCTION pg_catalog.float84ne(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84ne$function$;/*float84lt 259*/CREATE OR REPLACE FUNCTION pg_catalog.float84lt(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84lt$function$;/*float84le 260*/CREATE OR REPLACE FUNCTION pg_catalog.float84le(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84le$function$;/*float84gt 261*/CREATE OR REPLACE FUNCTION pg_catalog.float84gt(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84gt$function$;/*float84ge 262*/CREATE OR REPLACE FUNCTION pg_catalog.float84ge(double precision, real)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$float84ge$function$;/*width_bucket 263*/CREATE OR REPLACE FUNCTION pg_catalog.width_bucket(double precision, double precision, double precision, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$width_bucket_float8$function$;/*float8 264*/CREATE OR REPLACE FUNCTION pg_catalog.float8(real)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ftod$function$;/*float4 265*/CREATE OR REPLACE FUNCTION pg_catalog.float4(double precision)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtof$function$;/*int4 266*/CREATE OR REPLACE FUNCTION pg_catalog.int4(smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i2toi4$function$;/*int2 267*/CREATE OR REPLACE FUNCTION pg_catalog.int2(integer)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i4toi2$function$;/*float8 268*/CREATE OR REPLACE FUNCTION pg_catalog.float8(integer)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i4tod$function$;/*int4 269*/CREATE OR REPLACE FUNCTION pg_catalog.int4(double precision)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtoi4$function$;/*float4 270*/CREATE OR REPLACE FUNCTION pg_catalog.float4(integer)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i4tof$function$;/*int4 271*/CREATE OR REPLACE FUNCTION pg_catalog.int4(real)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ftoi4$function$;/*heap_tableam_handler 272*/CREATE OR REPLACE FUNCTION pg_catalog.heap_tableam_handler(internal)
 RETURNS table_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$heap_tableam_handler$function$;/*bthandler 273*/CREATE OR REPLACE FUNCTION pg_catalog.bthandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$bthandler$function$;/*hashhandler 274*/CREATE OR REPLACE FUNCTION pg_catalog.hashhandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$hashhandler$function$;/*gisthandler 275*/CREATE OR REPLACE FUNCTION pg_catalog.gisthandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$gisthandler$function$;/*ginhandler 276*/CREATE OR REPLACE FUNCTION pg_catalog.ginhandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$ginhandler$function$;/*spghandler 277*/CREATE OR REPLACE FUNCTION pg_catalog.spghandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$spghandler$function$;/*brinhandler 278*/CREATE OR REPLACE FUNCTION pg_catalog.brinhandler(internal)
 RETURNS index_am_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$brinhandler$function$;/*brin_summarize_new_values 279*/CREATE OR REPLACE FUNCTION pg_catalog.brin_summarize_new_values(regclass)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$brin_summarize_new_values$function$;/*brin_summarize_range 280*/CREATE OR REPLACE FUNCTION pg_catalog.brin_summarize_range(regclass, bigint)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$brin_summarize_range$function$;/*brin_desummarize_range 281*/CREATE OR REPLACE FUNCTION pg_catalog.brin_desummarize_range(regclass, bigint)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$brin_desummarize_range$function$;/*amvalidate 282*/CREATE OR REPLACE FUNCTION pg_catalog.amvalidate(oid)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$amvalidate$function$;/*pg_indexam_has_property 283*/CREATE OR REPLACE FUNCTION pg_catalog.pg_indexam_has_property(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_indexam_has_property$function$;/*pg_index_has_property 284*/CREATE OR REPLACE FUNCTION pg_catalog.pg_index_has_property(regclass, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_index_has_property$function$;/*pg_index_column_has_property 285*/CREATE OR REPLACE FUNCTION pg_catalog.pg_index_column_has_property(regclass, integer, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_index_column_has_property$function$;/*pg_indexam_progress_phasename 286*/CREATE OR REPLACE FUNCTION pg_catalog.pg_indexam_progress_phasename(oid, bigint)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_indexam_progress_phasename$function$;/*poly_same 287*/CREATE OR REPLACE FUNCTION pg_catalog.poly_same(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_same$function$;/*poly_contain 288*/CREATE OR REPLACE FUNCTION pg_catalog.poly_contain(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_contain$function$;/*poly_left 289*/CREATE OR REPLACE FUNCTION pg_catalog.poly_left(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_left$function$;/*poly_overleft 290*/CREATE OR REPLACE FUNCTION pg_catalog.poly_overleft(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_overleft$function$;/*poly_overright 291*/CREATE OR REPLACE FUNCTION pg_catalog.poly_overright(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_overright$function$;/*poly_right 292*/CREATE OR REPLACE FUNCTION pg_catalog.poly_right(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_right$function$;/*poly_contained 293*/CREATE OR REPLACE FUNCTION pg_catalog.poly_contained(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_contained$function$;/*poly_overlap 294*/CREATE OR REPLACE FUNCTION pg_catalog.poly_overlap(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_overlap$function$;/*poly_in 295*/CREATE OR REPLACE FUNCTION pg_catalog.poly_in(cstring)
 RETURNS polygon
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_in$function$;/*poly_out 296*/CREATE OR REPLACE FUNCTION pg_catalog.poly_out(polygon)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_out$function$;/*btint2cmp 297*/CREATE OR REPLACE FUNCTION pg_catalog.btint2cmp(smallint, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint2cmp$function$;/*btint2sortsupport 298*/CREATE OR REPLACE FUNCTION pg_catalog.btint2sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btint2sortsupport$function$;/*btint4cmp 299*/CREATE OR REPLACE FUNCTION pg_catalog.btint4cmp(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint4cmp$function$;/*btint4sortsupport 300*/CREATE OR REPLACE FUNCTION pg_catalog.btint4sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btint4sortsupport$function$;/*btint8cmp 301*/CREATE OR REPLACE FUNCTION pg_catalog.btint8cmp(bigint, bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint8cmp$function$;/*btint8sortsupport 302*/CREATE OR REPLACE FUNCTION pg_catalog.btint8sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btint8sortsupport$function$;/*btfloat4cmp 303*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat4cmp(real, real)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btfloat4cmp$function$;/*btfloat4sortsupport 304*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat4sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btfloat4sortsupport$function$;/*btfloat8cmp 305*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat8cmp(double precision, double precision)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btfloat8cmp$function$;/*btfloat8sortsupport 306*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat8sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btfloat8sortsupport$function$;/*btoidcmp 307*/CREATE OR REPLACE FUNCTION pg_catalog.btoidcmp(oid, oid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btoidcmp$function$;/*btoidsortsupport 308*/CREATE OR REPLACE FUNCTION pg_catalog.btoidsortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btoidsortsupport$function$;/*btoidvectorcmp 309*/CREATE OR REPLACE FUNCTION pg_catalog.btoidvectorcmp(oidvector, oidvector)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btoidvectorcmp$function$;/*btcharcmp 310*/CREATE OR REPLACE FUNCTION pg_catalog.btcharcmp("char", "char")
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btcharcmp$function$;/*btnamecmp 311*/CREATE OR REPLACE FUNCTION pg_catalog.btnamecmp(name, name)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btnamecmp$function$;/*btnamesortsupport 312*/CREATE OR REPLACE FUNCTION pg_catalog.btnamesortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btnamesortsupport$function$;/*bttextcmp 313*/CREATE OR REPLACE FUNCTION pg_catalog.bttextcmp(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bttextcmp$function$;/*bttextsortsupport 314*/CREATE OR REPLACE FUNCTION pg_catalog.bttextsortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bttextsortsupport$function$;/*btvarstrequalimage 315*/CREATE OR REPLACE FUNCTION pg_catalog.btvarstrequalimage(oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btvarstrequalimage$function$;/*cash_cmp 316*/CREATE OR REPLACE FUNCTION pg_catalog.cash_cmp(money, money)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_cmp$function$;/*btarraycmp 317*/CREATE OR REPLACE FUNCTION pg_catalog.btarraycmp(anyarray, anyarray)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btarraycmp$function$;/*in_range 318*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(bigint, bigint, bigint, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int8_int8$function$;/*in_range 319*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(integer, integer, bigint, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int4_int8$function$;/*in_range 320*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(integer, integer, integer, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int4_int4$function$;/*in_range 321*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(integer, integer, smallint, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int4_int2$function$;/*in_range 322*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(smallint, smallint, bigint, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int2_int8$function$;/*in_range 323*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(smallint, smallint, integer, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int2_int4$function$;/*in_range 324*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(smallint, smallint, smallint, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_int2_int2$function$;/*in_range 325*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(double precision, double precision, double precision, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_float8_float8$function$;/*in_range 326*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(real, real, double precision, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_float4_float8$function$;/*in_range 327*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(numeric, numeric, numeric, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_numeric_numeric$function$;/*lseg_distance 328*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_distance(lseg, lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_distance$function$;/*lseg_interpt 329*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_interpt(lseg, lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_interpt$function$;/*dist_ps 330*/CREATE OR REPLACE FUNCTION pg_catalog.dist_ps(point, lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_ps$function$;/*dist_sp 331*/CREATE OR REPLACE FUNCTION pg_catalog.dist_sp(lseg, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_sp$function$;/*dist_pb 332*/CREATE OR REPLACE FUNCTION pg_catalog.dist_pb(point, box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_pb$function$;/*dist_bp 333*/CREATE OR REPLACE FUNCTION pg_catalog.dist_bp(box, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_bp$function$;/*dist_sb 334*/CREATE OR REPLACE FUNCTION pg_catalog.dist_sb(lseg, box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_sb$function$;/*dist_bs 335*/CREATE OR REPLACE FUNCTION pg_catalog.dist_bs(box, lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_bs$function$;/*close_ps 336*/CREATE OR REPLACE FUNCTION pg_catalog.close_ps(point, lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_ps$function$;/*close_pb 337*/CREATE OR REPLACE FUNCTION pg_catalog.close_pb(point, box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_pb$function$;/*close_sb 338*/CREATE OR REPLACE FUNCTION pg_catalog.close_sb(lseg, box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_sb$function$;/*on_ps 339*/CREATE OR REPLACE FUNCTION pg_catalog.on_ps(point, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_ps$function$;/*path_distance 340*/CREATE OR REPLACE FUNCTION pg_catalog.path_distance(path, path)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_distance$function$;/*dist_ppath 341*/CREATE OR REPLACE FUNCTION pg_catalog.dist_ppath(point, path)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_ppath$function$;/*dist_pathp 342*/CREATE OR REPLACE FUNCTION pg_catalog.dist_pathp(path, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_pathp$function$;/*on_sb 343*/CREATE OR REPLACE FUNCTION pg_catalog.on_sb(lseg, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_sb$function$;/*inter_sb 344*/CREATE OR REPLACE FUNCTION pg_catalog.inter_sb(lseg, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inter_sb$function$;/*text 345*/CREATE OR REPLACE FUNCTION pg_catalog.text(character)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$rtrim1$function$;/*text 346*/CREATE OR REPLACE FUNCTION pg_catalog.text(name)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$name_text$function$;/*name 347*/CREATE OR REPLACE FUNCTION pg_catalog.name(text)
 RETURNS name
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_name$function$;/*bpchar 348*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar(name)
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$name_bpchar$function$;/*name 349*/CREATE OR REPLACE FUNCTION pg_catalog.name(character)
 RETURNS name
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpchar_name$function$;/*hashint2 350*/CREATE OR REPLACE FUNCTION pg_catalog.hashint2(smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint2$function$;/*hashint2extended 351*/CREATE OR REPLACE FUNCTION pg_catalog.hashint2extended(smallint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint2extended$function$;/*hashint4 352*/CREATE OR REPLACE FUNCTION pg_catalog.hashint4(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint4$function$;/*hashint4extended 353*/CREATE OR REPLACE FUNCTION pg_catalog.hashint4extended(integer, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint4extended$function$;/*hashint8 354*/CREATE OR REPLACE FUNCTION pg_catalog.hashint8(bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint8$function$;/*hashint8extended 355*/CREATE OR REPLACE FUNCTION pg_catalog.hashint8extended(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashint8extended$function$;/*hashfloat4 356*/CREATE OR REPLACE FUNCTION pg_catalog.hashfloat4(real)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashfloat4$function$;/*hashfloat4extended 357*/CREATE OR REPLACE FUNCTION pg_catalog.hashfloat4extended(real, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashfloat4extended$function$;/*hashfloat8 358*/CREATE OR REPLACE FUNCTION pg_catalog.hashfloat8(double precision)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashfloat8$function$;/*hashfloat8extended 359*/CREATE OR REPLACE FUNCTION pg_catalog.hashfloat8extended(double precision, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashfloat8extended$function$;/*hashoid 360*/CREATE OR REPLACE FUNCTION pg_catalog.hashoid(oid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashoid$function$;/*hashoidextended 361*/CREATE OR REPLACE FUNCTION pg_catalog.hashoidextended(oid, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashoidextended$function$;/*hashchar 362*/CREATE OR REPLACE FUNCTION pg_catalog.hashchar("char")
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashchar$function$;/*hashcharextended 363*/CREATE OR REPLACE FUNCTION pg_catalog.hashcharextended("char", bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashcharextended$function$;/*hashname 364*/CREATE OR REPLACE FUNCTION pg_catalog.hashname(name)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashname$function$;/*hashnameextended 365*/CREATE OR REPLACE FUNCTION pg_catalog.hashnameextended(name, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashnameextended$function$;/*hashtext 366*/CREATE OR REPLACE FUNCTION pg_catalog.hashtext(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashtext$function$;/*hashtextextended 367*/CREATE OR REPLACE FUNCTION pg_catalog.hashtextextended(text, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashtextextended$function$;/*hashvarlena 368*/CREATE OR REPLACE FUNCTION pg_catalog.hashvarlena(internal)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashvarlena$function$;/*hashvarlenaextended 369*/CREATE OR REPLACE FUNCTION pg_catalog.hashvarlenaextended(internal, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashvarlenaextended$function$;/*hashoidvector 370*/CREATE OR REPLACE FUNCTION pg_catalog.hashoidvector(oidvector)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashoidvector$function$;/*hashoidvectorextended 371*/CREATE OR REPLACE FUNCTION pg_catalog.hashoidvectorextended(oidvector, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashoidvectorextended$function$;/*hash_aclitem 372*/CREATE OR REPLACE FUNCTION pg_catalog.hash_aclitem(aclitem)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_aclitem$function$;/*hash_aclitem_extended 373*/CREATE OR REPLACE FUNCTION pg_catalog.hash_aclitem_extended(aclitem, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_aclitem_extended$function$;/*hashmacaddr 374*/CREATE OR REPLACE FUNCTION pg_catalog.hashmacaddr(macaddr)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashmacaddr$function$;/*hashmacaddrextended 375*/CREATE OR REPLACE FUNCTION pg_catalog.hashmacaddrextended(macaddr, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashmacaddrextended$function$;/*hashinet 376*/CREATE OR REPLACE FUNCTION pg_catalog.hashinet(inet)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashinet$function$;/*hashinetextended 377*/CREATE OR REPLACE FUNCTION pg_catalog.hashinetextended(inet, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashinetextended$function$;/*hash_numeric 378*/CREATE OR REPLACE FUNCTION pg_catalog.hash_numeric(numeric)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_numeric$function$;/*hash_numeric_extended 379*/CREATE OR REPLACE FUNCTION pg_catalog.hash_numeric_extended(numeric, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_numeric_extended$function$;/*hashmacaddr8 380*/CREATE OR REPLACE FUNCTION pg_catalog.hashmacaddr8(macaddr8)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashmacaddr8$function$;/*hashmacaddr8extended 381*/CREATE OR REPLACE FUNCTION pg_catalog.hashmacaddr8extended(macaddr8, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashmacaddr8extended$function$;/*num_nulls 382*/CREATE OR REPLACE FUNCTION pg_catalog.num_nulls(VARIADIC "any")
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$pg_num_nulls$function$;/*num_nonnulls 383*/CREATE OR REPLACE FUNCTION pg_catalog.num_nonnulls(VARIADIC "any")
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$pg_num_nonnulls$function$;/*text_larger 384*/CREATE OR REPLACE FUNCTION pg_catalog.text_larger(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_larger$function$;/*text_smaller 385*/CREATE OR REPLACE FUNCTION pg_catalog.text_smaller(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_smaller$function$;/*int8in 386*/CREATE OR REPLACE FUNCTION pg_catalog.int8in(cstring)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8in$function$;/*int8out 387*/CREATE OR REPLACE FUNCTION pg_catalog.int8out(bigint)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8out$function$;/*int8um 388*/CREATE OR REPLACE FUNCTION pg_catalog.int8um(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8um$function$;/*int8pl 389*/CREATE OR REPLACE FUNCTION pg_catalog.int8pl(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8pl$function$;/*int8mi 390*/CREATE OR REPLACE FUNCTION pg_catalog.int8mi(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8mi$function$;/*int8mul 391*/CREATE OR REPLACE FUNCTION pg_catalog.int8mul(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8mul$function$;/*int8div 392*/CREATE OR REPLACE FUNCTION pg_catalog.int8div(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8div$function$;/*int8eq 393*/CREATE OR REPLACE FUNCTION pg_catalog.int8eq(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8eq$function$;/*int8ne 394*/CREATE OR REPLACE FUNCTION pg_catalog.int8ne(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8ne$function$;/*int8lt 395*/CREATE OR REPLACE FUNCTION pg_catalog.int8lt(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8lt$function$;/*int8gt 396*/CREATE OR REPLACE FUNCTION pg_catalog.int8gt(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8gt$function$;/*int8le 397*/CREATE OR REPLACE FUNCTION pg_catalog.int8le(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8le$function$;/*int8ge 398*/CREATE OR REPLACE FUNCTION pg_catalog.int8ge(bigint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int8ge$function$;/*int84eq 399*/CREATE OR REPLACE FUNCTION pg_catalog.int84eq(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84eq$function$;/*int84ne 400*/CREATE OR REPLACE FUNCTION pg_catalog.int84ne(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84ne$function$;/*int84lt 401*/CREATE OR REPLACE FUNCTION pg_catalog.int84lt(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84lt$function$;/*int84gt 402*/CREATE OR REPLACE FUNCTION pg_catalog.int84gt(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84gt$function$;/*int84le 403*/CREATE OR REPLACE FUNCTION pg_catalog.int84le(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84le$function$;/*int84ge 404*/CREATE OR REPLACE FUNCTION pg_catalog.int84ge(bigint, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int84ge$function$;/*int4 405*/CREATE OR REPLACE FUNCTION pg_catalog.int4(bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int84$function$;/*int8 406*/CREATE OR REPLACE FUNCTION pg_catalog.int8(integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int48$function$;/*float8 407*/CREATE OR REPLACE FUNCTION pg_catalog.float8(bigint)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i8tod$function$;/*int8 408*/CREATE OR REPLACE FUNCTION pg_catalog.int8(double precision)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtoi8$function$;/*hash_array 409*/CREATE OR REPLACE FUNCTION pg_catalog.hash_array(anyarray)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_array$function$;/*hash_array_extended 410*/CREATE OR REPLACE FUNCTION pg_catalog.hash_array_extended(anyarray, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_array_extended$function$;/*float4 411*/CREATE OR REPLACE FUNCTION pg_catalog.float4(bigint)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i8tof$function$;/*int8 412*/CREATE OR REPLACE FUNCTION pg_catalog.int8(real)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ftoi8$function$;/*int2 413*/CREATE OR REPLACE FUNCTION pg_catalog.int2(bigint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int82$function$;/*int8 414*/CREATE OR REPLACE FUNCTION pg_catalog.int8(smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int28$function$;/*namelt 415*/CREATE OR REPLACE FUNCTION pg_catalog.namelt(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namelt$function$;/*namele 416*/CREATE OR REPLACE FUNCTION pg_catalog.namele(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namele$function$;/*namegt 417*/CREATE OR REPLACE FUNCTION pg_catalog.namegt(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namegt$function$;/*namege 418*/CREATE OR REPLACE FUNCTION pg_catalog.namege(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namege$function$;/*namene 419*/CREATE OR REPLACE FUNCTION pg_catalog.namene(name, name)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$namene$function$;/*bpchar 420*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar(character, integer, boolean)
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpchar$function$;/*varchar_support 421*/CREATE OR REPLACE FUNCTION pg_catalog.varchar_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varchar_support$function$;/*varchar 422*/CREATE OR REPLACE FUNCTION pg_catalog."varchar"(character varying, integer, boolean)
 RETURNS character varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT varchar_support
AS $function$varchar$function$;/*oidvectorne 423*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorne(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectorne$function$;/*oidvectorlt 424*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorlt(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectorlt$function$;/*oidvectorle 425*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorle(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectorle$function$;/*oidvectoreq 426*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectoreq(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectoreq$function$;/*oidvectorge 427*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorge(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectorge$function$;/*oidvectorgt 428*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorgt(oidvector, oidvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidvectorgt$function$;/*getpgusername 429*/CREATE OR REPLACE FUNCTION pg_catalog.getpgusername()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$current_user$function$;/*oidlt 430*/CREATE OR REPLACE FUNCTION pg_catalog.oidlt(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidlt$function$;/*oidle 431*/CREATE OR REPLACE FUNCTION pg_catalog.oidle(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidle$function$;/*octet_length 432*/CREATE OR REPLACE FUNCTION pg_catalog.octet_length(bytea)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaoctetlen$function$;/*get_byte 433*/CREATE OR REPLACE FUNCTION pg_catalog.get_byte(bytea, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaGetByte$function$;/*set_byte 434*/CREATE OR REPLACE FUNCTION pg_catalog.set_byte(bytea, integer, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaSetByte$function$;/*get_bit 435*/CREATE OR REPLACE FUNCTION pg_catalog.get_bit(bytea, bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaGetBit$function$;/*set_bit 436*/CREATE OR REPLACE FUNCTION pg_catalog.set_bit(bytea, bigint, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaSetBit$function$;/*overlay 437*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(bytea, bytea, integer, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaoverlay$function$;/*overlay 438*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(bytea, bytea, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaoverlay_no_len$function$;/*dist_pl 439*/CREATE OR REPLACE FUNCTION pg_catalog.dist_pl(point, line)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_pl$function$;/*dist_lp 440*/CREATE OR REPLACE FUNCTION pg_catalog.dist_lp(line, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_lp$function$;/*dist_lb 441*/CREATE OR REPLACE FUNCTION pg_catalog.dist_lb(line, box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_lb$function$;/*dist_bl 442*/CREATE OR REPLACE FUNCTION pg_catalog.dist_bl(box, line)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_bl$function$;/*dist_sl 443*/CREATE OR REPLACE FUNCTION pg_catalog.dist_sl(lseg, line)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_sl$function$;/*dist_ls 444*/CREATE OR REPLACE FUNCTION pg_catalog.dist_ls(line, lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_ls$function$;/*dist_cpoly 445*/CREATE OR REPLACE FUNCTION pg_catalog.dist_cpoly(circle, polygon)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_cpoly$function$;/*dist_polyc 446*/CREATE OR REPLACE FUNCTION pg_catalog.dist_polyc(polygon, circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_polyc$function$;/*poly_distance 447*/CREATE OR REPLACE FUNCTION pg_catalog.poly_distance(polygon, polygon)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_distance$function$;/*dist_ppoly 448*/CREATE OR REPLACE FUNCTION pg_catalog.dist_ppoly(point, polygon)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_ppoly$function$;/*dist_polyp 449*/CREATE OR REPLACE FUNCTION pg_catalog.dist_polyp(polygon, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_polyp$function$;/*dist_cpoint 450*/CREATE OR REPLACE FUNCTION pg_catalog.dist_cpoint(circle, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_cpoint$function$;/*text_lt 451*/CREATE OR REPLACE FUNCTION pg_catalog.text_lt(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_lt$function$;/*text_le 452*/CREATE OR REPLACE FUNCTION pg_catalog.text_le(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_le$function$;/*text_gt 453*/CREATE OR REPLACE FUNCTION pg_catalog.text_gt(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_gt$function$;/*text_ge 454*/CREATE OR REPLACE FUNCTION pg_catalog.text_ge(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_ge$function$;/*current_user 455*/CREATE OR REPLACE FUNCTION pg_catalog."current_user"()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$current_user$function$;/*session_user 456*/CREATE OR REPLACE FUNCTION pg_catalog."session_user"()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$session_user$function$;/*array_eq 457*/CREATE OR REPLACE FUNCTION pg_catalog.array_eq(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_eq$function$;/*array_ne 458*/CREATE OR REPLACE FUNCTION pg_catalog.array_ne(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_ne$function$;/*array_lt 459*/CREATE OR REPLACE FUNCTION pg_catalog.array_lt(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_lt$function$;/*array_gt 460*/CREATE OR REPLACE FUNCTION pg_catalog.array_gt(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_gt$function$;/*array_le 461*/CREATE OR REPLACE FUNCTION pg_catalog.array_le(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_le$function$;/*array_ge 462*/CREATE OR REPLACE FUNCTION pg_catalog.array_ge(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_ge$function$;/*array_dims 463*/CREATE OR REPLACE FUNCTION pg_catalog.array_dims(anyarray)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_dims$function$;/*array_ndims 464*/CREATE OR REPLACE FUNCTION pg_catalog.array_ndims(anyarray)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_ndims$function$;/*array_in 465*/CREATE OR REPLACE FUNCTION pg_catalog.array_in(cstring, oid, integer)
 RETURNS anyarray
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_in$function$;/*array_out 466*/CREATE OR REPLACE FUNCTION pg_catalog.array_out(anyarray)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_out$function$;/*array_lower 467*/CREATE OR REPLACE FUNCTION pg_catalog.array_lower(anyarray, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_lower$function$;/*array_upper 468*/CREATE OR REPLACE FUNCTION pg_catalog.array_upper(anyarray, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_upper$function$;/*array_length 469*/CREATE OR REPLACE FUNCTION pg_catalog.array_length(anyarray, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_length$function$;/*cardinality 470*/CREATE OR REPLACE FUNCTION pg_catalog.cardinality(anyarray)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_cardinality$function$;/*array_append 471*/CREATE OR REPLACE FUNCTION pg_catalog.array_append(anyarray, anyelement)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_append$function$;/*array_prepend 472*/CREATE OR REPLACE FUNCTION pg_catalog.array_prepend(anyelement, anyarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_prepend$function$;/*array_cat 473*/CREATE OR REPLACE FUNCTION pg_catalog.array_cat(anyarray, anyarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_cat$function$;/*string_to_array 474*/CREATE OR REPLACE FUNCTION pg_catalog.string_to_array(text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$text_to_array$function$;/*array_to_string 475*/CREATE OR REPLACE FUNCTION pg_catalog.array_to_string(anyarray, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_to_text$function$;/*string_to_array 476*/CREATE OR REPLACE FUNCTION pg_catalog.string_to_array(text, text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$text_to_array_null$function$;/*array_to_string 477*/CREATE OR REPLACE FUNCTION pg_catalog.array_to_string(anyarray, text, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$array_to_text_null$function$;/*array_larger 478*/CREATE OR REPLACE FUNCTION pg_catalog.array_larger(anyarray, anyarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_larger$function$;/*array_smaller 479*/CREATE OR REPLACE FUNCTION pg_catalog.array_smaller(anyarray, anyarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_smaller$function$;/*array_position 480*/CREATE OR REPLACE FUNCTION pg_catalog.array_position(anyarray, anyelement)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_position$function$;/*array_position 481*/CREATE OR REPLACE FUNCTION pg_catalog.array_position(anyarray, anyelement, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_position_start$function$;/*array_positions 482*/CREATE OR REPLACE FUNCTION pg_catalog.array_positions(anyarray, anyelement)
 RETURNS integer[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_positions$function$;/*generate_subscripts 483*/CREATE OR REPLACE FUNCTION pg_catalog.generate_subscripts(anyarray, integer, boolean)
 RETURNS SETOF integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_subscripts$function$;/*generate_subscripts 484*/CREATE OR REPLACE FUNCTION pg_catalog.generate_subscripts(anyarray, integer)
 RETURNS SETOF integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_subscripts_nodir$function$;/*array_fill 485*/CREATE OR REPLACE FUNCTION pg_catalog.array_fill(anyelement, integer[])
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_fill$function$;/*array_fill 486*/CREATE OR REPLACE FUNCTION pg_catalog.array_fill(anyelement, integer[], integer[])
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_fill_with_lower_bounds$function$;/*unnest 487*/CREATE OR REPLACE FUNCTION pg_catalog.unnest(anyarray)
 RETURNS SETOF anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100 SUPPORT array_unnest_support
AS $function$array_unnest$function$;/*array_unnest_support 488*/CREATE OR REPLACE FUNCTION pg_catalog.array_unnest_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_unnest_support$function$;/*array_remove 489*/CREATE OR REPLACE FUNCTION pg_catalog.array_remove(anyarray, anyelement)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_remove$function$;/*array_replace 490*/CREATE OR REPLACE FUNCTION pg_catalog.array_replace(anyarray, anyelement, anyelement)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_replace$function$;/*array_agg_transfn 491*/CREATE OR REPLACE FUNCTION pg_catalog.array_agg_transfn(internal, anynonarray)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_agg_transfn$function$;/*array_agg_finalfn 492*/CREATE OR REPLACE FUNCTION pg_catalog.array_agg_finalfn(internal, anynonarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_agg_finalfn$function$;/*array_agg_array_transfn 493*/CREATE OR REPLACE FUNCTION pg_catalog.array_agg_array_transfn(internal, anyarray)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_agg_array_transfn$function$;/*array_agg_array_finalfn 494*/CREATE OR REPLACE FUNCTION pg_catalog.array_agg_array_finalfn(internal, anyarray)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$array_agg_array_finalfn$function$;/*width_bucket 495*/CREATE OR REPLACE FUNCTION pg_catalog.width_bucket(anyelement, anyarray)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$width_bucket_array$function$;/*array_typanalyze 496*/CREATE OR REPLACE FUNCTION pg_catalog.array_typanalyze(internal)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_typanalyze$function$;/*arraycontsel 497*/CREATE OR REPLACE FUNCTION pg_catalog.arraycontsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$arraycontsel$function$;/*arraycontjoinsel 498*/CREATE OR REPLACE FUNCTION pg_catalog.arraycontjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$arraycontjoinsel$function$;/*core_updstru_checkexisttable 499*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttable(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
        sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_tables t
        --TABLE pg_catalog.pg_tables
        WHERE t.tablename = lower(sName); -- AND t.tablespace NOT IN ('pg_catalog', 'information_schema');

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    END $function$;/*int4inc 500*/CREATE OR REPLACE FUNCTION pg_catalog.int4inc(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4inc$function$;/*int4larger 501*/CREATE OR REPLACE FUNCTION pg_catalog.int4larger(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4larger$function$;/*int4smaller 502*/CREATE OR REPLACE FUNCTION pg_catalog.int4smaller(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4smaller$function$;/*int2larger 503*/CREATE OR REPLACE FUNCTION pg_catalog.int2larger(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2larger$function$;/*int2smaller 504*/CREATE OR REPLACE FUNCTION pg_catalog.int2smaller(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2smaller$function$;/*cash_mul_flt4 505*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mul_flt4(money, real)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mul_flt4$function$;/*cash_div_flt4 506*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_flt4(money, real)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_flt4$function$;/*flt4_mul_cash 507*/CREATE OR REPLACE FUNCTION pg_catalog.flt4_mul_cash(real, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$flt4_mul_cash$function$;/*position 508*/CREATE OR REPLACE FUNCTION pg_catalog."position"(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textpos$function$;/*textlike 509*/CREATE OR REPLACE FUNCTION pg_catalog.textlike(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$textlike$function$;/*textlike_support 510*/CREATE OR REPLACE FUNCTION pg_catalog.textlike_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textlike_support$function$;/*textnlike 511*/CREATE OR REPLACE FUNCTION pg_catalog.textnlike(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textnlike$function$;/*int48eq 512*/CREATE OR REPLACE FUNCTION pg_catalog.int48eq(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48eq$function$;/*int48ne 513*/CREATE OR REPLACE FUNCTION pg_catalog.int48ne(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48ne$function$;/*int48lt 514*/CREATE OR REPLACE FUNCTION pg_catalog.int48lt(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48lt$function$;/*int48gt 515*/CREATE OR REPLACE FUNCTION pg_catalog.int48gt(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48gt$function$;/*int48le 516*/CREATE OR REPLACE FUNCTION pg_catalog.int48le(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48le$function$;/*int48ge 517*/CREATE OR REPLACE FUNCTION pg_catalog.int48ge(integer, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int48ge$function$;/*namelike 518*/CREATE OR REPLACE FUNCTION pg_catalog.namelike(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$namelike$function$;/*namenlike 519*/CREATE OR REPLACE FUNCTION pg_catalog.namenlike(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$namenlike$function$;/*bpchar 520*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar("char")
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$char_bpchar$function$;/*current_database 521*/CREATE OR REPLACE FUNCTION pg_catalog.current_database()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$current_database$function$;/*current_query 522*/CREATE OR REPLACE FUNCTION pg_catalog.current_query()
 RETURNS text
 LANGUAGE internal
 PARALLEL RESTRICTED
AS $function$current_query$function$;/*int8_mul_cash 523*/CREATE OR REPLACE FUNCTION pg_catalog.int8_mul_cash(bigint, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8_mul_cash$function$;/*int4_mul_cash 524*/CREATE OR REPLACE FUNCTION pg_catalog.int4_mul_cash(integer, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_mul_cash$function$;/*int2_mul_cash 525*/CREATE OR REPLACE FUNCTION pg_catalog.int2_mul_cash(smallint, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2_mul_cash$function$;/*cash_mul_int8 526*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mul_int8(money, bigint)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mul_int8$function$;/*cash_div_int8 527*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_int8(money, bigint)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_int8$function$;/*cash_mul_int4 528*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mul_int4(money, integer)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mul_int4$function$;/*cash_div_int4 529*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_int4(money, integer)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_int4$function$;/*cash_mul_int2 530*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mul_int2(money, smallint)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mul_int2$function$;/*cash_div_int2 531*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_int2(money, smallint)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_int2$function$;/*cash_in 532*/CREATE OR REPLACE FUNCTION pg_catalog.cash_in(cstring)
 RETURNS money
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$cash_in$function$;/*cash_out 533*/CREATE OR REPLACE FUNCTION pg_catalog.cash_out(money)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$cash_out$function$;/*cash_eq 534*/CREATE OR REPLACE FUNCTION pg_catalog.cash_eq(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_eq$function$;/*cash_ne 535*/CREATE OR REPLACE FUNCTION pg_catalog.cash_ne(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_ne$function$;/*cash_lt 536*/CREATE OR REPLACE FUNCTION pg_catalog.cash_lt(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_lt$function$;/*cash_le 537*/CREATE OR REPLACE FUNCTION pg_catalog.cash_le(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_le$function$;/*cash_gt 538*/CREATE OR REPLACE FUNCTION pg_catalog.cash_gt(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_gt$function$;/*cash_ge 539*/CREATE OR REPLACE FUNCTION pg_catalog.cash_ge(money, money)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$cash_ge$function$;/*cash_pl 540*/CREATE OR REPLACE FUNCTION pg_catalog.cash_pl(money, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_pl$function$;/*cash_mi 541*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mi(money, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mi$function$;/*cash_mul_flt8 542*/CREATE OR REPLACE FUNCTION pg_catalog.cash_mul_flt8(money, double precision)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_mul_flt8$function$;/*cash_div_flt8 543*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_flt8(money, double precision)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_flt8$function$;/*cashlarger 544*/CREATE OR REPLACE FUNCTION pg_catalog.cashlarger(money, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cashlarger$function$;/*cashsmaller 545*/CREATE OR REPLACE FUNCTION pg_catalog.cashsmaller(money, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cashsmaller$function$;/*flt8_mul_cash 546*/CREATE OR REPLACE FUNCTION pg_catalog.flt8_mul_cash(double precision, money)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$flt8_mul_cash$function$;/*cash_words 547*/CREATE OR REPLACE FUNCTION pg_catalog.cash_words(money)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_words$function$;/*cash_div_cash 548*/CREATE OR REPLACE FUNCTION pg_catalog.cash_div_cash(money, money)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_div_cash$function$;/*numeric 549*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(money)
 RETURNS numeric
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$cash_numeric$function$;/*money 550*/CREATE OR REPLACE FUNCTION pg_catalog.money(numeric)
 RETURNS money
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$numeric_cash$function$;/*money 551*/CREATE OR REPLACE FUNCTION pg_catalog.money(integer)
 RETURNS money
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$int4_cash$function$;/*money 552*/CREATE OR REPLACE FUNCTION pg_catalog.money(bigint)
 RETURNS money
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$int8_cash$function$;/*mod 553*/CREATE OR REPLACE FUNCTION pg_catalog.mod(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2mod$function$;/*mod 554*/CREATE OR REPLACE FUNCTION pg_catalog.mod(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4mod$function$;/*int8mod 555*/CREATE OR REPLACE FUNCTION pg_catalog.int8mod(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8mod$function$;/*mod 556*/CREATE OR REPLACE FUNCTION pg_catalog.mod(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8mod$function$;/*gcd 557*/CREATE OR REPLACE FUNCTION pg_catalog.gcd(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4gcd$function$;/*gcd 558*/CREATE OR REPLACE FUNCTION pg_catalog.gcd(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8gcd$function$;/*lcm 559*/CREATE OR REPLACE FUNCTION pg_catalog.lcm(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4lcm$function$;/*lcm 560*/CREATE OR REPLACE FUNCTION pg_catalog.lcm(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8lcm$function$;/*char 561*/CREATE OR REPLACE FUNCTION pg_catalog."char"(text)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_char$function$;/*text 562*/CREATE OR REPLACE FUNCTION pg_catalog.text("char")
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$char_text$function$;/*lo_open 563*/CREATE OR REPLACE FUNCTION pg_catalog.lo_open(oid, integer)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_open$function$;/*lo_close 564*/CREATE OR REPLACE FUNCTION pg_catalog.lo_close(integer)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_close$function$;/*loread 565*/CREATE OR REPLACE FUNCTION pg_catalog.loread(integer, integer)
 RETURNS bytea
 LANGUAGE internal
 STRICT
AS $function$be_loread$function$;/*lowrite 566*/CREATE OR REPLACE FUNCTION pg_catalog.lowrite(integer, bytea)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lowrite$function$;/*lo_lseek 567*/CREATE OR REPLACE FUNCTION pg_catalog.lo_lseek(integer, integer, integer)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_lseek$function$;/*lo_lseek64 568*/CREATE OR REPLACE FUNCTION pg_catalog.lo_lseek64(integer, bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$be_lo_lseek64$function$;/*lo_creat 569*/CREATE OR REPLACE FUNCTION pg_catalog.lo_creat(integer)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$be_lo_creat$function$;/*lo_create 570*/CREATE OR REPLACE FUNCTION pg_catalog.lo_create(oid)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$be_lo_create$function$;/*lo_tell 571*/CREATE OR REPLACE FUNCTION pg_catalog.lo_tell(integer)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_tell$function$;/*lo_tell64 572*/CREATE OR REPLACE FUNCTION pg_catalog.lo_tell64(integer)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$be_lo_tell64$function$;/*lo_truncate 573*/CREATE OR REPLACE FUNCTION pg_catalog.lo_truncate(integer, integer)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_truncate$function$;/*lo_truncate64 574*/CREATE OR REPLACE FUNCTION pg_catalog.lo_truncate64(integer, bigint)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_truncate64$function$;/*lo_from_bytea 575*/CREATE OR REPLACE FUNCTION pg_catalog.lo_from_bytea(oid, bytea)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$be_lo_from_bytea$function$;/*lo_get 576*/CREATE OR REPLACE FUNCTION pg_catalog.lo_get(oid)
 RETURNS bytea
 LANGUAGE internal
 STRICT
AS $function$be_lo_get$function$;/*lo_get 577*/CREATE OR REPLACE FUNCTION pg_catalog.lo_get(oid, bigint, integer)
 RETURNS bytea
 LANGUAGE internal
 STRICT
AS $function$be_lo_get_fragment$function$;/*lo_put 578*/CREATE OR REPLACE FUNCTION pg_catalog.lo_put(oid, bigint, bytea)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$be_lo_put$function$;/*on_pl 579*/CREATE OR REPLACE FUNCTION pg_catalog.on_pl(point, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_pl$function$;/*on_sl 580*/CREATE OR REPLACE FUNCTION pg_catalog.on_sl(lseg, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$on_sl$function$;/*close_pl 581*/CREATE OR REPLACE FUNCTION pg_catalog.close_pl(point, line)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_pl$function$;/*close_sl 582*/CREATE OR REPLACE FUNCTION pg_catalog.close_sl(lseg, line)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_sl$function$;/*close_lb 583*/CREATE OR REPLACE FUNCTION pg_catalog.close_lb(line, box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_lb$function$;/*lo_unlink 584*/CREATE OR REPLACE FUNCTION pg_catalog.lo_unlink(oid)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_unlink$function$;/*path_inter 585*/CREATE OR REPLACE FUNCTION pg_catalog.path_inter(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_inter$function$;/*area 586*/CREATE OR REPLACE FUNCTION pg_catalog.area(box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_area$function$;/*width 587*/CREATE OR REPLACE FUNCTION pg_catalog.width(box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_width$function$;/*height 588*/CREATE OR REPLACE FUNCTION pg_catalog.height(box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_height$function$;/*box_distance 589*/CREATE OR REPLACE FUNCTION pg_catalog.box_distance(box, box)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_distance$function$;/*area 590*/CREATE OR REPLACE FUNCTION pg_catalog.area(path)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_area$function$;/*box_intersect 591*/CREATE OR REPLACE FUNCTION pg_catalog.box_intersect(box, box)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_intersect$function$;/*bound_box 592*/CREATE OR REPLACE FUNCTION pg_catalog.bound_box(box, box)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boxes_bound_box$function$;/*diagonal 593*/CREATE OR REPLACE FUNCTION pg_catalog.diagonal(box)
 RETURNS lseg
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_diagonal$function$;/*path_n_lt 594*/CREATE OR REPLACE FUNCTION pg_catalog.path_n_lt(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_n_lt$function$;/*path_n_gt 595*/CREATE OR REPLACE FUNCTION pg_catalog.path_n_gt(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_n_gt$function$;/*path_n_eq 596*/CREATE OR REPLACE FUNCTION pg_catalog.path_n_eq(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_n_eq$function$;/*path_n_le 597*/CREATE OR REPLACE FUNCTION pg_catalog.path_n_le(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_n_le$function$;/*path_n_ge 598*/CREATE OR REPLACE FUNCTION pg_catalog.path_n_ge(path, path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_n_ge$function$;/*path_length 599*/CREATE OR REPLACE FUNCTION pg_catalog.path_length(path)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_length$function$;/*point_ne 600*/CREATE OR REPLACE FUNCTION pg_catalog.point_ne(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_ne$function$;/*point_vert 601*/CREATE OR REPLACE FUNCTION pg_catalog.point_vert(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_vert$function$;/*point_horiz 602*/CREATE OR REPLACE FUNCTION pg_catalog.point_horiz(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_horiz$function$;/*point_distance 603*/CREATE OR REPLACE FUNCTION pg_catalog.point_distance(point, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_distance$function$;/*slope 604*/CREATE OR REPLACE FUNCTION pg_catalog.slope(point, point)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_slope$function$;/*lseg 605*/CREATE OR REPLACE FUNCTION pg_catalog.lseg(point, point)
 RETURNS lseg
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_construct$function$;/*lseg_intersect 606*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_intersect(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_intersect$function$;/*lseg_parallel 607*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_parallel(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_parallel$function$;/*lseg_perp 608*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_perp(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_perp$function$;/*lseg_vertical 609*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_vertical(lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_vertical$function$;/*lseg_horizontal 610*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_horizontal(lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_horizontal$function$;/*lseg_eq 611*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_eq(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_eq$function$;/*timezone 612*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(interval, timestamp with time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptz_izone$function$;/*aclitemin 613*/CREATE OR REPLACE FUNCTION pg_catalog.aclitemin(cstring)
 RETURNS aclitem
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$aclitemin$function$;/*aclitemout 614*/CREATE OR REPLACE FUNCTION pg_catalog.aclitemout(aclitem)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$aclitemout$function$;/*aclinsert 615*/CREATE OR REPLACE FUNCTION pg_catalog.aclinsert(aclitem[], aclitem)
 RETURNS aclitem[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$aclinsert$function$;/*aclremove 616*/CREATE OR REPLACE FUNCTION pg_catalog.aclremove(aclitem[], aclitem)
 RETURNS aclitem[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$aclremove$function$;/*aclcontains 617*/CREATE OR REPLACE FUNCTION pg_catalog.aclcontains(aclitem[], aclitem)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$aclcontains$function$;/*aclitemeq 618*/CREATE OR REPLACE FUNCTION pg_catalog.aclitemeq(aclitem, aclitem)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$aclitem_eq$function$;/*makeaclitem 619*/CREATE OR REPLACE FUNCTION pg_catalog.makeaclitem(oid, oid, text, boolean)
 RETURNS aclitem
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$makeaclitem$function$;/*acldefault 620*/CREATE OR REPLACE FUNCTION pg_catalog.acldefault("char", oid)
 RETURNS aclitem[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$acldefault_sql$function$;/*aclexplode 621*/CREATE OR REPLACE FUNCTION pg_catalog.aclexplode(acl aclitem[], OUT grantor oid, OUT grantee oid, OUT privilege_type text, OUT is_grantable boolean)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT ROWS 10
AS $function$aclexplode$function$;/*bpcharin 622*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharin(cstring, oid, integer)
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharin$function$;/*bpcharout 623*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharout(character)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharout$function$;/*bpchartypmodin 624*/CREATE OR REPLACE FUNCTION pg_catalog.bpchartypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpchartypmodin$function$;/*bpchartypmodout 625*/CREATE OR REPLACE FUNCTION pg_catalog.bpchartypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpchartypmodout$function$;/*varcharin 626*/CREATE OR REPLACE FUNCTION pg_catalog.varcharin(cstring, oid, integer)
 RETURNS character varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varcharin$function$;/*varcharout 627*/CREATE OR REPLACE FUNCTION pg_catalog.varcharout(character varying)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varcharout$function$;/*varchartypmodin 628*/CREATE OR REPLACE FUNCTION pg_catalog.varchartypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varchartypmodin$function$;/*varchartypmodout 629*/CREATE OR REPLACE FUNCTION pg_catalog.varchartypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varchartypmodout$function$;/*bpchareq 630*/CREATE OR REPLACE FUNCTION pg_catalog.bpchareq(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchareq$function$;/*bpcharlt 631*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharlt(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpcharlt$function$;/*bpcharle 632*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharle(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpcharle$function$;/*bpchargt 633*/CREATE OR REPLACE FUNCTION pg_catalog.bpchargt(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchargt$function$;/*bpcharge 634*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharge(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpcharge$function$;/*bpcharne 635*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharne(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpcharne$function$;/*bpchar_larger 636*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_larger(character, character)
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_larger$function$;/*bpchar_smaller 637*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_smaller(character, character)
 RETURNS character
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_smaller$function$;/*bpcharcmp 638*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharcmp(character, character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpcharcmp$function$;/*bpchar_sortsupport 639*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpchar_sortsupport$function$;/*hashbpchar 640*/CREATE OR REPLACE FUNCTION pg_catalog.hashbpchar(character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashbpchar$function$;/*hashbpcharextended 641*/CREATE OR REPLACE FUNCTION pg_catalog.hashbpcharextended(character, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashbpcharextended$function$;/*format_type 642*/CREATE OR REPLACE FUNCTION pg_catalog.format_type(oid, integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$format_type$function$;/*date_in 643*/CREATE OR REPLACE FUNCTION pg_catalog.date_in(cstring)
 RETURNS date
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_in$function$;/*date_out 644*/CREATE OR REPLACE FUNCTION pg_catalog.date_out(date)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_out$function$;/*date_eq 645*/CREATE OR REPLACE FUNCTION pg_catalog.date_eq(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_eq$function$;/*date_lt 646*/CREATE OR REPLACE FUNCTION pg_catalog.date_lt(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_lt$function$;/*date_le 647*/CREATE OR REPLACE FUNCTION pg_catalog.date_le(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_le$function$;/*date_gt 648*/CREATE OR REPLACE FUNCTION pg_catalog.date_gt(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_gt$function$;/*date_ge 649*/CREATE OR REPLACE FUNCTION pg_catalog.date_ge(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_ge$function$;/*date_ne 650*/CREATE OR REPLACE FUNCTION pg_catalog.date_ne(date, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_ne$function$;/*date_cmp 651*/CREATE OR REPLACE FUNCTION pg_catalog.date_cmp(date, date)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$date_cmp$function$;/*date_sortsupport 652*/CREATE OR REPLACE FUNCTION pg_catalog.date_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_sortsupport$function$;/*in_range 653*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(date, date, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_date_interval$function$;/*time_lt 654*/CREATE OR REPLACE FUNCTION pg_catalog.time_lt(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_lt$function$;/*time_le 655*/CREATE OR REPLACE FUNCTION pg_catalog.time_le(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_le$function$;/*time_gt 656*/CREATE OR REPLACE FUNCTION pg_catalog.time_gt(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_gt$function$;/*time_ge 657*/CREATE OR REPLACE FUNCTION pg_catalog.time_ge(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_ge$function$;/*time_ne 658*/CREATE OR REPLACE FUNCTION pg_catalog.time_ne(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_ne$function$;/*time_cmp 659*/CREATE OR REPLACE FUNCTION pg_catalog.time_cmp(time without time zone, time without time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_cmp$function$;/*date_larger 660*/CREATE OR REPLACE FUNCTION pg_catalog.date_larger(date, date)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_larger$function$;/*date_smaller 661*/CREATE OR REPLACE FUNCTION pg_catalog.date_smaller(date, date)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_smaller$function$;/*date_mi 662*/CREATE OR REPLACE FUNCTION pg_catalog.date_mi(date, date)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_mi$function$;/*date_pli 663*/CREATE OR REPLACE FUNCTION pg_catalog.date_pli(date, integer)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_pli$function$;/*date_mii 664*/CREATE OR REPLACE FUNCTION pg_catalog.date_mii(date, integer)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_mii$function$;/*time_in 665*/CREATE OR REPLACE FUNCTION pg_catalog.time_in(cstring, oid, integer)
 RETURNS time without time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$time_in$function$;/*time_out 666*/CREATE OR REPLACE FUNCTION pg_catalog.time_out(time without time zone)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_out$function$;/*timetypmodin 667*/CREATE OR REPLACE FUNCTION pg_catalog.timetypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetypmodin$function$;/*timetypmodout 668*/CREATE OR REPLACE FUNCTION pg_catalog.timetypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetypmodout$function$;/*time_eq 669*/CREATE OR REPLACE FUNCTION pg_catalog.time_eq(time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$time_eq$function$;/*circle_add_pt 670*/CREATE OR REPLACE FUNCTION pg_catalog.circle_add_pt(circle, point)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_add_pt$function$;/*circle_sub_pt 671*/CREATE OR REPLACE FUNCTION pg_catalog.circle_sub_pt(circle, point)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_sub_pt$function$;/*circle_mul_pt 672*/CREATE OR REPLACE FUNCTION pg_catalog.circle_mul_pt(circle, point)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_mul_pt$function$;/*circle_div_pt 673*/CREATE OR REPLACE FUNCTION pg_catalog.circle_div_pt(circle, point)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_div_pt$function$;/*timestamptz_in 674*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_in(cstring, oid, integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_in$function$;/*timestamptz_out 675*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_out(timestamp with time zone)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_out$function$;/*timestamptztypmodin 676*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptztypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptztypmodin$function$;/*timestamptztypmodout 677*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptztypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptztypmodout$function$;/*timestamptz_eq 678*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_eq(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_eq$function$;/*timestamptz_ne 679*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ne(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_ne$function$;/*timestamptz_lt 680*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_lt(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_lt$function$;/*timestamptz_le 681*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_le(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_le$function$;/*timestamptz_ge 682*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ge(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_ge$function$;/*timestamptz_gt 683*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_gt(timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_gt$function$;/*to_timestamp 684*/CREATE OR REPLACE FUNCTION pg_catalog.to_timestamp(double precision)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_timestamptz$function$;/*timezone 685*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(text, timestamp with time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptz_zone$function$;/*interval_in 686*/CREATE OR REPLACE FUNCTION pg_catalog.interval_in(cstring, oid, integer)
 RETURNS interval
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$interval_in$function$;/*interval_out 687*/CREATE OR REPLACE FUNCTION pg_catalog.interval_out(interval)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_out$function$;/*intervaltypmodin 688*/CREATE OR REPLACE FUNCTION pg_catalog.intervaltypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$intervaltypmodin$function$;/*intervaltypmodout 689*/CREATE OR REPLACE FUNCTION pg_catalog.intervaltypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$intervaltypmodout$function$;/*interval_eq 690*/CREATE OR REPLACE FUNCTION pg_catalog.interval_eq(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_eq$function$;/*interval_ne 691*/CREATE OR REPLACE FUNCTION pg_catalog.interval_ne(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_ne$function$;/*interval_lt 692*/CREATE OR REPLACE FUNCTION pg_catalog.interval_lt(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_lt$function$;/*interval_le 693*/CREATE OR REPLACE FUNCTION pg_catalog.interval_le(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_le$function$;/*interval_ge 694*/CREATE OR REPLACE FUNCTION pg_catalog.interval_ge(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_ge$function$;/*interval_gt 695*/CREATE OR REPLACE FUNCTION pg_catalog.interval_gt(interval, interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_gt$function$;/*interval_um 696*/CREATE OR REPLACE FUNCTION pg_catalog.interval_um(interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_um$function$;/*interval_pl 697*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl(interval, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_pl$function$;/*interval_mi 698*/CREATE OR REPLACE FUNCTION pg_catalog.interval_mi(interval, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_mi$function$;/*date_part 699*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, timestamp with time zone)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_part$function$;/*date_part 700*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, interval)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_part$function$;/*timestamptz 701*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz(date)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_timestamptz$function$;/*justify_interval 702*/CREATE OR REPLACE FUNCTION pg_catalog.justify_interval(interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_justify_interval$function$;/*justify_hours 703*/CREATE OR REPLACE FUNCTION pg_catalog.justify_hours(interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_justify_hours$function$;/*justify_days 704*/CREATE OR REPLACE FUNCTION pg_catalog.justify_days(interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_justify_days$function$;/*timestamptz 705*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz(date, time without time zone)
 RETURNS timestamp with time zone
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select cast(($1 + $2) as timestamp with time zone)$function$;/*date 706*/CREATE OR REPLACE FUNCTION pg_catalog.date(timestamp with time zone)
 RETURNS date
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_date$function$;/*age 707*/CREATE OR REPLACE FUNCTION pg_catalog.age(xid)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$xid_age$function$;/*mxid_age 708*/CREATE OR REPLACE FUNCTION pg_catalog.mxid_age(xid)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$mxid_age$function$;/*timestamptz_mi 709*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_mi(timestamp with time zone, timestamp with time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_mi$function$;/*timestamptz_pl_interval 710*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_pl_interval(timestamp with time zone, interval)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_pl_interval$function$;/*timestamptz_mi_interval 711*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_mi_interval(timestamp with time zone, interval)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_mi_interval$function$;/*timestamptz_smaller 712*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_smaller(timestamp with time zone, timestamp with time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_smaller$function$;/*timestamptz_larger 713*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_larger(timestamp with time zone, timestamp with time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_larger$function$;/*interval_smaller 714*/CREATE OR REPLACE FUNCTION pg_catalog.interval_smaller(interval, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_smaller$function$;/*interval_larger 715*/CREATE OR REPLACE FUNCTION pg_catalog.interval_larger(interval, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_larger$function$;/*age 716*/CREATE OR REPLACE FUNCTION pg_catalog.age(timestamp with time zone, timestamp with time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptz_age$function$;/*interval_support 717*/CREATE OR REPLACE FUNCTION pg_catalog.interval_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_support$function$;/*interval 718*/CREATE OR REPLACE FUNCTION pg_catalog."interval"(interval, integer)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT interval_support
AS $function$interval_scale$function$;/*obj_description 719*/CREATE OR REPLACE FUNCTION pg_catalog.obj_description(oid, name)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$select description from pg_catalog.pg_description where objoid = $1 and classoid = (select oid from pg_catalog.pg_class where relname = $2 and relnamespace = 11) and objsubid = 0$function$;/*col_description 720*/CREATE OR REPLACE FUNCTION pg_catalog.col_description(oid, integer)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$select description from pg_catalog.pg_description where objoid = $1 and classoid = 'pg_catalog.pg_class'::pg_catalog.regclass and objsubid = $2$function$;/*shobj_description 721*/CREATE OR REPLACE FUNCTION pg_catalog.shobj_description(oid, name)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$select description from pg_catalog.pg_shdescription where objoid = $1 and classoid = (select oid from pg_catalog.pg_class where relname = $2 and relnamespace = 11)$function$;/*date_trunc 722*/CREATE OR REPLACE FUNCTION pg_catalog.date_trunc(text, timestamp with time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_trunc$function$;/*date_trunc 723*/CREATE OR REPLACE FUNCTION pg_catalog.date_trunc(text, timestamp with time zone, text)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_trunc_zone$function$;/*date_trunc 724*/CREATE OR REPLACE FUNCTION pg_catalog.date_trunc(text, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_trunc$function$;/*int8inc 725*/CREATE OR REPLACE FUNCTION pg_catalog.int8inc(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8inc$function$;/*int8dec 726*/CREATE OR REPLACE FUNCTION pg_catalog.int8dec(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8dec$function$;/*int8inc_any 727*/CREATE OR REPLACE FUNCTION pg_catalog.int8inc_any(bigint, "any")
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8inc_any$function$;/*int8dec_any 728*/CREATE OR REPLACE FUNCTION pg_catalog.int8dec_any(bigint, "any")
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8dec_any$function$;/*int8abs 729*/CREATE OR REPLACE FUNCTION pg_catalog.int8abs(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8abs$function$;/*int8larger 730*/CREATE OR REPLACE FUNCTION pg_catalog.int8larger(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8larger$function$;/*int8smaller 731*/CREATE OR REPLACE FUNCTION pg_catalog.int8smaller(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8smaller$function$;/*texticregexeq 732*/CREATE OR REPLACE FUNCTION pg_catalog.texticregexeq(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticregexeq_support
AS $function$texticregexeq$function$;/*texticregexeq_support 733*/CREATE OR REPLACE FUNCTION pg_catalog.texticregexeq_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticregexeq_support$function$;/*texticregexne 734*/CREATE OR REPLACE FUNCTION pg_catalog.texticregexne(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticregexne$function$;/*nameicregexeq 735*/CREATE OR REPLACE FUNCTION pg_catalog.nameicregexeq(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticregexeq_support
AS $function$nameicregexeq$function$;/*nameicregexne 736*/CREATE OR REPLACE FUNCTION pg_catalog.nameicregexne(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$nameicregexne$function$;/*int4abs 737*/CREATE OR REPLACE FUNCTION pg_catalog.int4abs(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4abs$function$;/*int2abs 738*/CREATE OR REPLACE FUNCTION pg_catalog.int2abs(smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2abs$function$;/*overlaps 739*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(time with time zone, time with time zone, time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$overlaps_timetz$function$;/*datetime_pl 740*/CREATE OR REPLACE FUNCTION pg_catalog.datetime_pl(date, time without time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datetime_timestamp$function$;/*date_part 741*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, time with time zone)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_part$function$;/*int84pl 742*/CREATE OR REPLACE FUNCTION pg_catalog.int84pl(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int84pl$function$;/*int84mi 743*/CREATE OR REPLACE FUNCTION pg_catalog.int84mi(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int84mi$function$;/*int84mul 744*/CREATE OR REPLACE FUNCTION pg_catalog.int84mul(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int84mul$function$;/*int84div 745*/CREATE OR REPLACE FUNCTION pg_catalog.int84div(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int84div$function$;/*int48pl 746*/CREATE OR REPLACE FUNCTION pg_catalog.int48pl(integer, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int48pl$function$;/*int48mi 747*/CREATE OR REPLACE FUNCTION pg_catalog.int48mi(integer, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int48mi$function$;/*int48mul 748*/CREATE OR REPLACE FUNCTION pg_catalog.int48mul(integer, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int48mul$function$;/*int48div 749*/CREATE OR REPLACE FUNCTION pg_catalog.int48div(integer, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int48div$function$;/*int82pl 750*/CREATE OR REPLACE FUNCTION pg_catalog.int82pl(bigint, smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int82pl$function$;/*int82mi 751*/CREATE OR REPLACE FUNCTION pg_catalog.int82mi(bigint, smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int82mi$function$;/*int82mul 752*/CREATE OR REPLACE FUNCTION pg_catalog.int82mul(bigint, smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int82mul$function$;/*int82div 753*/CREATE OR REPLACE FUNCTION pg_catalog.int82div(bigint, smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int82div$function$;/*int28pl 754*/CREATE OR REPLACE FUNCTION pg_catalog.int28pl(smallint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int28pl$function$;/*int28mi 755*/CREATE OR REPLACE FUNCTION pg_catalog.int28mi(smallint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int28mi$function$;/*int28mul 756*/CREATE OR REPLACE FUNCTION pg_catalog.int28mul(smallint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int28mul$function$;/*int28div 757*/CREATE OR REPLACE FUNCTION pg_catalog.int28div(smallint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int28div$function$;/*oid 758*/CREATE OR REPLACE FUNCTION pg_catalog.oid(bigint)
 RETURNS oid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$i8tooid$function$;/*int8 759*/CREATE OR REPLACE FUNCTION pg_catalog.int8(oid)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidtoi8$function$;/*suppress_redundant_updates_trigger 760*/CREATE OR REPLACE FUNCTION pg_catalog.suppress_redundant_updates_trigger()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$suppress_redundant_updates_trigger$function$;/*tideq 761*/CREATE OR REPLACE FUNCTION pg_catalog.tideq(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tideq$function$;/*currtid 762*/CREATE OR REPLACE FUNCTION pg_catalog.currtid(oid, tid)
 RETURNS tid
 LANGUAGE internal
 STRICT
AS $function$currtid_byreloid$function$;/*currtid2 763*/CREATE OR REPLACE FUNCTION pg_catalog.currtid2(text, tid)
 RETURNS tid
 LANGUAGE internal
 STRICT
AS $function$currtid_byrelname$function$;/*tidne 764*/CREATE OR REPLACE FUNCTION pg_catalog.tidne(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tidne$function$;/*tidgt 765*/CREATE OR REPLACE FUNCTION pg_catalog.tidgt(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tidgt$function$;/*tidlt 766*/CREATE OR REPLACE FUNCTION pg_catalog.tidlt(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tidlt$function$;/*tidge 767*/CREATE OR REPLACE FUNCTION pg_catalog.tidge(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tidge$function$;/*tidle 768*/CREATE OR REPLACE FUNCTION pg_catalog.tidle(tid, tid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$tidle$function$;/*bttidcmp 769*/CREATE OR REPLACE FUNCTION pg_catalog.bttidcmp(tid, tid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bttidcmp$function$;/*tidlarger 770*/CREATE OR REPLACE FUNCTION pg_catalog.tidlarger(tid, tid)
 RETURNS tid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidlarger$function$;/*tidsmaller 771*/CREATE OR REPLACE FUNCTION pg_catalog.tidsmaller(tid, tid)
 RETURNS tid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidsmaller$function$;/*hashtid 772*/CREATE OR REPLACE FUNCTION pg_catalog.hashtid(tid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashtid$function$;/*hashtidextended 773*/CREATE OR REPLACE FUNCTION pg_catalog.hashtidextended(tid, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashtidextended$function$;/*timedate_pl 774*/CREATE OR REPLACE FUNCTION pg_catalog.timedate_pl(time without time zone, date)
 RETURNS timestamp without time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select ($2 + $1)$function$;/*datetimetz_pl 775*/CREATE OR REPLACE FUNCTION pg_catalog.datetimetz_pl(date, time with time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datetimetz_timestamptz$function$;/*timetzdate_pl 776*/CREATE OR REPLACE FUNCTION pg_catalog.timetzdate_pl(time with time zone, date)
 RETURNS timestamp with time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select ($2 + $1)$function$;/*now 777*/CREATE OR REPLACE FUNCTION pg_catalog.now()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$now$function$;/*transaction_timestamp 778*/CREATE OR REPLACE FUNCTION pg_catalog.transaction_timestamp()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$now$function$;/*statement_timestamp 779*/CREATE OR REPLACE FUNCTION pg_catalog.statement_timestamp()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$statement_timestamp$function$;/*clock_timestamp 780*/CREATE OR REPLACE FUNCTION pg_catalog.clock_timestamp()
 RETURNS timestamp with time zone
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$clock_timestamp$function$;/*positionsel 781*/CREATE OR REPLACE FUNCTION pg_catalog.positionsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$positionsel$function$;/*positionjoinsel 782*/CREATE OR REPLACE FUNCTION pg_catalog.positionjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$positionjoinsel$function$;/*contsel 783*/CREATE OR REPLACE FUNCTION pg_catalog.contsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$contsel$function$;/*contjoinsel 784*/CREATE OR REPLACE FUNCTION pg_catalog.contjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$contjoinsel$function$;/*timetz_le 785*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_le(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_le$function$;/*overlaps 786*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp with time zone, timestamp with time zone, timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$overlaps_timestamp$function$;/*overlaps 787*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp with time zone, interval, timestamp with time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 STABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, ($3 + $4))$function$;/*overlaps 788*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp with time zone, timestamp with time zone, timestamp with time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 STABLE PARALLEL SAFE COST 1
AS $function$select ($1, $2) overlaps ($3, ($3 + $4))$function$;/*overlaps 789*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp with time zone, interval, timestamp with time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE sql
 STABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, $4)$function$;/*overlaps 790*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(time without time zone, time without time zone, time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$overlaps_time$function$;/*overlaps 791*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(time without time zone, interval, time without time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, ($3 + $4))$function$;/*overlaps 792*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(time without time zone, time without time zone, time without time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, $2) overlaps ($3, ($3 + $4))$function$;/*overlaps 793*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(time without time zone, interval, time without time zone, time without time zone)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, $4)$function$;/*timestamp_in 794*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_in(cstring, oid, integer)
 RETURNS timestamp without time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_in$function$;/*timestamp_out 795*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_out(timestamp without time zone)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_out$function$;/*timestamptypmodin 796*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptypmodin$function$;/*timestamptypmodout 797*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptypmodout$function$;/*timestamptz_cmp 798*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_cmp(timestamp with time zone, timestamp with time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_cmp$function$;/*interval_cmp 799*/CREATE OR REPLACE FUNCTION pg_catalog.interval_cmp(interval, interval)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$interval_cmp$function$;/*time 800*/CREATE OR REPLACE FUNCTION pg_catalog."time"(timestamp without time zone)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_time$function$;/*length 801*/CREATE OR REPLACE FUNCTION pg_catalog.length(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textlen$function$;/*length 802*/CREATE OR REPLACE FUNCTION pg_catalog.length(character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharlen$function$;/*xideqint4 803*/CREATE OR REPLACE FUNCTION pg_catalog.xideqint4(xid, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xideq$function$;/*xidneqint4 804*/CREATE OR REPLACE FUNCTION pg_catalog.xidneqint4(xid, integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$xidneq$function$;/*interval_div 805*/CREATE OR REPLACE FUNCTION pg_catalog.interval_div(interval, double precision)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_div$function$;/*dlog10 806*/CREATE OR REPLACE FUNCTION pg_catalog.dlog10(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dlog10$function$;/*log 807*/CREATE OR REPLACE FUNCTION pg_catalog.log(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dlog10$function$;/*log10 808*/CREATE OR REPLACE FUNCTION pg_catalog.log10(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dlog10$function$;/*ln 809*/CREATE OR REPLACE FUNCTION pg_catalog.ln(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dlog1$function$;/*round 810*/CREATE OR REPLACE FUNCTION pg_catalog.round(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dround$function$;/*trunc 811*/CREATE OR REPLACE FUNCTION pg_catalog.trunc(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtrunc$function$;/*sqrt 812*/CREATE OR REPLACE FUNCTION pg_catalog.sqrt(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsqrt$function$;/*cbrt 813*/CREATE OR REPLACE FUNCTION pg_catalog.cbrt(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcbrt$function$;/*pow 814*/CREATE OR REPLACE FUNCTION pg_catalog.pow(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dpow$function$;/*power 815*/CREATE OR REPLACE FUNCTION pg_catalog.power(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dpow$function$;/*exp 816*/CREATE OR REPLACE FUNCTION pg_catalog.exp(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dexp$function$;/*obj_description 817*/CREATE OR REPLACE FUNCTION pg_catalog.obj_description(oid)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$select description from pg_catalog.pg_description where objoid = $1 and objsubid = 0$function$;/*oidvectortypes 818*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectortypes(oidvector)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$oidvectortypes$function$;/*timetz_in 819*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_in(cstring, oid, integer)
 RETURNS time with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timetz_in$function$;/*timetz_out 820*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_out(time with time zone)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_out$function$;/*timetztypmodin 821*/CREATE OR REPLACE FUNCTION pg_catalog.timetztypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetztypmodin$function$;/*timetztypmodout 822*/CREATE OR REPLACE FUNCTION pg_catalog.timetztypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetztypmodout$function$;/*timetz_eq 823*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_eq(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_eq$function$;/*timetz_ne 824*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_ne(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_ne$function$;/*timetz_lt 825*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_lt(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_lt$function$;/*timetz_ge 826*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_ge(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_ge$function$;/*timetz_gt 827*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_gt(time with time zone, time with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_gt$function$;/*timetz_cmp 828*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_cmp(time with time zone, time with time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timetz_cmp$function$;/*timestamptz 829*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz(date, time with time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datetimetz_timestamptz$function$;/*character_length 830*/CREATE OR REPLACE FUNCTION pg_catalog.character_length(character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharlen$function$;/*character_length 831*/CREATE OR REPLACE FUNCTION pg_catalog.character_length(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textlen$function$;/*interval 832*/CREATE OR REPLACE FUNCTION pg_catalog."interval"(time without time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_interval$function$;/*char_length 833*/CREATE OR REPLACE FUNCTION pg_catalog.char_length(character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharlen$function$;/*octet_length 834*/CREATE OR REPLACE FUNCTION pg_catalog.octet_length(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textoctetlen$function$;/*octet_length 835*/CREATE OR REPLACE FUNCTION pg_catalog.octet_length(character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bpcharoctetlen$function$;/*time_larger 836*/CREATE OR REPLACE FUNCTION pg_catalog.time_larger(time without time zone, time without time zone)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_larger$function$;/*time_smaller 837*/CREATE OR REPLACE FUNCTION pg_catalog.time_smaller(time without time zone, time without time zone)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_smaller$function$;/*timetz_larger 838*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_larger(time with time zone, time with time zone)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_larger$function$;/*timetz_smaller 839*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_smaller(time with time zone, time with time zone)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_smaller$function$;/*char_length 840*/CREATE OR REPLACE FUNCTION pg_catalog.char_length(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textlen$function$;/*date_part 841*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, date)
 RETURNS double precision
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.date_part($1, cast($2 as timestamp without time zone))$function$;/*date_part 842*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, time without time zone)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_part$function$;/*age 843*/CREATE OR REPLACE FUNCTION pg_catalog.age(timestamp with time zone)
 RETURNS interval
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.age(cast(current_date as timestamp with time zone), $1)$function$;/*timetz 844*/CREATE OR REPLACE FUNCTION pg_catalog.timetz(timestamp with time zone)
 RETURNS time with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_timetz$function$;/*isfinite 845*/CREATE OR REPLACE FUNCTION pg_catalog.isfinite(date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_finite$function$;/*isfinite 846*/CREATE OR REPLACE FUNCTION pg_catalog.isfinite(timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_finite$function$;/*isfinite 847*/CREATE OR REPLACE FUNCTION pg_catalog.isfinite(interval)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_finite$function$;/*factorial 848*/CREATE OR REPLACE FUNCTION pg_catalog.factorial(bigint)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_fac$function$;/*abs 849*/CREATE OR REPLACE FUNCTION pg_catalog.abs(real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4abs$function$;/*abs 850*/CREATE OR REPLACE FUNCTION pg_catalog.abs(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8abs$function$;/*abs 851*/CREATE OR REPLACE FUNCTION pg_catalog.abs(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8abs$function$;/*abs 852*/CREATE OR REPLACE FUNCTION pg_catalog.abs(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4abs$function$;/*abs 853*/CREATE OR REPLACE FUNCTION pg_catalog.abs(smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2abs$function$;/*name 854*/CREATE OR REPLACE FUNCTION pg_catalog.name(character varying)
 RETURNS name
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_name$function$;/*varchar 855*/CREATE OR REPLACE FUNCTION pg_catalog."varchar"(name)
 RETURNS character varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$name_text$function$;/*current_schema 856*/CREATE OR REPLACE FUNCTION pg_catalog."current_schema"()
 RETURNS name
 LANGUAGE internal
 STABLE STRICT
AS $function$current_schema$function$;/*current_schemas 857*/CREATE OR REPLACE FUNCTION pg_catalog.current_schemas(boolean)
 RETURNS name[]
 LANGUAGE internal
 STABLE STRICT
AS $function$current_schemas$function$;/*overlay 858*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(text, text, integer, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textoverlay$function$;/*overlay 859*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(text, text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textoverlay_no_len$function$;/*isvertical 860*/CREATE OR REPLACE FUNCTION pg_catalog.isvertical(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_vert$function$;/*ishorizontal 861*/CREATE OR REPLACE FUNCTION pg_catalog.ishorizontal(point, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_horiz$function$;/*isparallel 862*/CREATE OR REPLACE FUNCTION pg_catalog.isparallel(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_parallel$function$;/*isperp 863*/CREATE OR REPLACE FUNCTION pg_catalog.isperp(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_perp$function$;/*isvertical 864*/CREATE OR REPLACE FUNCTION pg_catalog.isvertical(lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_vertical$function$;/*ishorizontal 865*/CREATE OR REPLACE FUNCTION pg_catalog.ishorizontal(lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_horizontal$function$;/*isparallel 866*/CREATE OR REPLACE FUNCTION pg_catalog.isparallel(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_parallel$function$;/*isperp 867*/CREATE OR REPLACE FUNCTION pg_catalog.isperp(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_perp$function$;/*isvertical 868*/CREATE OR REPLACE FUNCTION pg_catalog.isvertical(line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_vertical$function$;/*ishorizontal 869*/CREATE OR REPLACE FUNCTION pg_catalog.ishorizontal(line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_horizontal$function$;/*point 870*/CREATE OR REPLACE FUNCTION pg_catalog.point(circle)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_center$function$;/*time 871*/CREATE OR REPLACE FUNCTION pg_catalog."time"(interval)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_time$function$;/*box 872*/CREATE OR REPLACE FUNCTION pg_catalog.box(point, point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$points_box$function$;/*box_add 873*/CREATE OR REPLACE FUNCTION pg_catalog.box_add(box, point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_add$function$;/*box_sub 874*/CREATE OR REPLACE FUNCTION pg_catalog.box_sub(box, point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_sub$function$;/*box_mul 875*/CREATE OR REPLACE FUNCTION pg_catalog.box_mul(box, point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_mul$function$;/*box_div 876*/CREATE OR REPLACE FUNCTION pg_catalog.box_div(box, point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_div$function$;/*path_contain_pt 877*/CREATE OR REPLACE FUNCTION pg_catalog.path_contain_pt(path, point)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.on_ppath($2, $1)$function$;/*poly_contain_pt 878*/CREATE OR REPLACE FUNCTION pg_catalog.poly_contain_pt(polygon, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_contain_pt$function$;/*pt_contained_poly 879*/CREATE OR REPLACE FUNCTION pg_catalog.pt_contained_poly(point, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pt_contained_poly$function$;/*isclosed 880*/CREATE OR REPLACE FUNCTION pg_catalog.isclosed(path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_isclosed$function$;/*isopen 881*/CREATE OR REPLACE FUNCTION pg_catalog.isopen(path)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_isopen$function$;/*path_npoints 882*/CREATE OR REPLACE FUNCTION pg_catalog.path_npoints(path)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_npoints$function$;/*pclose 883*/CREATE OR REPLACE FUNCTION pg_catalog.pclose(path)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_close$function$;/*popen 884*/CREATE OR REPLACE FUNCTION pg_catalog.popen(path)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_open$function$;/*path_add 885*/CREATE OR REPLACE FUNCTION pg_catalog.path_add(path, path)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_add$function$;/*path_add_pt 886*/CREATE OR REPLACE FUNCTION pg_catalog.path_add_pt(path, point)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_add_pt$function$;/*path_sub_pt 887*/CREATE OR REPLACE FUNCTION pg_catalog.path_sub_pt(path, point)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_sub_pt$function$;/*path_mul_pt 888*/CREATE OR REPLACE FUNCTION pg_catalog.path_mul_pt(path, point)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_mul_pt$function$;/*path_div_pt 889*/CREATE OR REPLACE FUNCTION pg_catalog.path_div_pt(path, point)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_div_pt$function$;/*point 890*/CREATE OR REPLACE FUNCTION pg_catalog.point(double precision, double precision)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$construct_point$function$;/*point_add 891*/CREATE OR REPLACE FUNCTION pg_catalog.point_add(point, point)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_add$function$;/*point_sub 892*/CREATE OR REPLACE FUNCTION pg_catalog.point_sub(point, point)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_sub$function$;/*point_mul 893*/CREATE OR REPLACE FUNCTION pg_catalog.point_mul(point, point)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_mul$function$;/*point_div 894*/CREATE OR REPLACE FUNCTION pg_catalog.point_div(point, point)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_div$function$;/*poly_npoints 895*/CREATE OR REPLACE FUNCTION pg_catalog.poly_npoints(polygon)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_npoints$function$;/*box 896*/CREATE OR REPLACE FUNCTION pg_catalog.box(polygon)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_box$function$;/*path 897*/CREATE OR REPLACE FUNCTION pg_catalog.path(polygon)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_path$function$;/*polygon 898*/CREATE OR REPLACE FUNCTION pg_catalog.polygon(box)
 RETURNS polygon
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_poly$function$;/*polygon 899*/CREATE OR REPLACE FUNCTION pg_catalog.polygon(path)
 RETURNS polygon
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_poly$function$;/*circle_in 900*/CREATE OR REPLACE FUNCTION pg_catalog.circle_in(cstring)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_in$function$;/*circle_out 901*/CREATE OR REPLACE FUNCTION pg_catalog.circle_out(circle)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_out$function$;/*circle_same 902*/CREATE OR REPLACE FUNCTION pg_catalog.circle_same(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_same$function$;/*circle_contain 903*/CREATE OR REPLACE FUNCTION pg_catalog.circle_contain(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_contain$function$;/*circle_left 904*/CREATE OR REPLACE FUNCTION pg_catalog.circle_left(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_left$function$;/*circle_overleft 905*/CREATE OR REPLACE FUNCTION pg_catalog.circle_overleft(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_overleft$function$;/*circle_overright 906*/CREATE OR REPLACE FUNCTION pg_catalog.circle_overright(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_overright$function$;/*circle_right 907*/CREATE OR REPLACE FUNCTION pg_catalog.circle_right(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_right$function$;/*length 908*/CREATE OR REPLACE FUNCTION pg_catalog.length(lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_length$function$;/*circle_contained 909*/CREATE OR REPLACE FUNCTION pg_catalog.circle_contained(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_contained$function$;/*circle_overlap 910*/CREATE OR REPLACE FUNCTION pg_catalog.circle_overlap(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_overlap$function$;/*circle_below 911*/CREATE OR REPLACE FUNCTION pg_catalog.circle_below(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_below$function$;/*circle_above 912*/CREATE OR REPLACE FUNCTION pg_catalog.circle_above(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_above$function$;/*circle_eq 913*/CREATE OR REPLACE FUNCTION pg_catalog.circle_eq(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_eq$function$;/*circle_ne 914*/CREATE OR REPLACE FUNCTION pg_catalog.circle_ne(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_ne$function$;/*circle_lt 915*/CREATE OR REPLACE FUNCTION pg_catalog.circle_lt(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_lt$function$;/*circle_gt 916*/CREATE OR REPLACE FUNCTION pg_catalog.circle_gt(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_gt$function$;/*circle_le 917*/CREATE OR REPLACE FUNCTION pg_catalog.circle_le(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_le$function$;/*circle_ge 918*/CREATE OR REPLACE FUNCTION pg_catalog.circle_ge(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$circle_ge$function$;/*area 919*/CREATE OR REPLACE FUNCTION pg_catalog.area(circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_area$function$;/*diameter 920*/CREATE OR REPLACE FUNCTION pg_catalog.diameter(circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_diameter$function$;/*radius 921*/CREATE OR REPLACE FUNCTION pg_catalog.radius(circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_radius$function$;/*circle_distance 922*/CREATE OR REPLACE FUNCTION pg_catalog.circle_distance(circle, circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_distance$function$;/*circle_center 923*/CREATE OR REPLACE FUNCTION pg_catalog.circle_center(circle)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_center$function$;/*circle 924*/CREATE OR REPLACE FUNCTION pg_catalog.circle(point, double precision)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cr_circle$function$;/*circle 925*/CREATE OR REPLACE FUNCTION pg_catalog.circle(polygon)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_circle$function$;/*polygon 926*/CREATE OR REPLACE FUNCTION pg_catalog.polygon(integer, circle)
 RETURNS polygon
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_poly$function$;/*dist_pc 927*/CREATE OR REPLACE FUNCTION pg_catalog.dist_pc(point, circle)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dist_pc$function$;/*circle_contain_pt 928*/CREATE OR REPLACE FUNCTION pg_catalog.circle_contain_pt(circle, point)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_contain_pt$function$;/*pt_contained_circle 929*/CREATE OR REPLACE FUNCTION pg_catalog.pt_contained_circle(point, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pt_contained_circle$function$;/*box 930*/CREATE OR REPLACE FUNCTION pg_catalog.box(point)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_box$function$;/*circle 931*/CREATE OR REPLACE FUNCTION pg_catalog.circle(box)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_circle$function$;/*box 932*/CREATE OR REPLACE FUNCTION pg_catalog.box(circle)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_box$function$;/*lseg_ne 933*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_ne(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_ne$function$;/*lseg_lt 934*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_lt(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_lt$function$;/*lseg_le 935*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_le(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_le$function$;/*lseg_gt 936*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_gt(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_gt$function$;/*lseg_ge 937*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_ge(lseg, lseg)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$lseg_ge$function$;/*lseg_length 938*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_length(lseg)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_length$function$;/*close_ls 939*/CREATE OR REPLACE FUNCTION pg_catalog.close_ls(line, lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_ls$function$;/*close_lseg 940*/CREATE OR REPLACE FUNCTION pg_catalog.close_lseg(lseg, lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$close_lseg$function$;/*line_in 941*/CREATE OR REPLACE FUNCTION pg_catalog.line_in(cstring)
 RETURNS line
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_in$function$;/*line_out 942*/CREATE OR REPLACE FUNCTION pg_catalog.line_out(line)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_out$function$;/*line_eq 943*/CREATE OR REPLACE FUNCTION pg_catalog.line_eq(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_eq$function$;/*line 944*/CREATE OR REPLACE FUNCTION pg_catalog.line(point, point)
 RETURNS line
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_construct_pp$function$;/*line_interpt 945*/CREATE OR REPLACE FUNCTION pg_catalog.line_interpt(line, line)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_interpt$function$;/*line_intersect 946*/CREATE OR REPLACE FUNCTION pg_catalog.line_intersect(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_intersect$function$;/*line_parallel 947*/CREATE OR REPLACE FUNCTION pg_catalog.line_parallel(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_parallel$function$;/*line_perp 948*/CREATE OR REPLACE FUNCTION pg_catalog.line_perp(line, line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_perp$function$;/*line_vertical 949*/CREATE OR REPLACE FUNCTION pg_catalog.line_vertical(line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_vertical$function$;/*line_horizontal 950*/CREATE OR REPLACE FUNCTION pg_catalog.line_horizontal(line)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_horizontal$function$;/*length 951*/CREATE OR REPLACE FUNCTION pg_catalog.length(path)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_length$function$;/*point 952*/CREATE OR REPLACE FUNCTION pg_catalog.point(lseg)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_center$function$;/*point 953*/CREATE OR REPLACE FUNCTION pg_catalog.point(path)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_center$function$;/*point 954*/CREATE OR REPLACE FUNCTION pg_catalog.point(box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_center$function$;/*point 955*/CREATE OR REPLACE FUNCTION pg_catalog.point(polygon)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_center$function$;/*lseg 956*/CREATE OR REPLACE FUNCTION pg_catalog.lseg(box)
 RETURNS lseg
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_diagonal$function$;/*center 957*/CREATE OR REPLACE FUNCTION pg_catalog.center(box)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_center$function$;/*center 958*/CREATE OR REPLACE FUNCTION pg_catalog.center(circle)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_center$function$;/*polygon 959*/CREATE OR REPLACE FUNCTION pg_catalog.polygon(circle)
 RETURNS polygon
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.polygon(12, $1)$function$;/*npoints 960*/CREATE OR REPLACE FUNCTION pg_catalog.npoints(path)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_npoints$function$;/*npoints 961*/CREATE OR REPLACE FUNCTION pg_catalog.npoints(polygon)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_npoints$function$;/*bit_in 962*/CREATE OR REPLACE FUNCTION pg_catalog.bit_in(cstring, oid, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_in$function$;/*bit_out 963*/CREATE OR REPLACE FUNCTION pg_catalog.bit_out(bit)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_out$function$;/*bittypmodin 964*/CREATE OR REPLACE FUNCTION pg_catalog.bittypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bittypmodin$function$;/*bittypmodout 965*/CREATE OR REPLACE FUNCTION pg_catalog.bittypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bittypmodout$function$;/*like 966*/CREATE OR REPLACE FUNCTION pg_catalog."like"(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$textlike$function$;/*notlike 967*/CREATE OR REPLACE FUNCTION pg_catalog.notlike(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textnlike$function$;/*like 968*/CREATE OR REPLACE FUNCTION pg_catalog."like"(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$namelike$function$;/*notlike 969*/CREATE OR REPLACE FUNCTION pg_catalog.notlike(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$namenlike$function$;/*nextval 970*/CREATE OR REPLACE FUNCTION pg_catalog.nextval(regclass)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$nextval_oid$function$;/*currval 971*/CREATE OR REPLACE FUNCTION pg_catalog.currval(regclass)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$currval_oid$function$;/*setval 972*/CREATE OR REPLACE FUNCTION pg_catalog.setval(regclass, bigint)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$setval_oid$function$;/*setval 973*/CREATE OR REPLACE FUNCTION pg_catalog.setval(regclass, bigint, boolean)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$setval3_oid$function$;/*pg_sequence_parameters 974*/CREATE OR REPLACE FUNCTION pg_catalog.pg_sequence_parameters(sequence_oid oid, OUT start_value bigint, OUT minimum_value bigint, OUT maximum_value bigint, OUT increment bigint, OUT cycle_option boolean, OUT cache_size bigint, OUT data_type oid)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_sequence_parameters$function$;/*pg_sequence_last_value 975*/CREATE OR REPLACE FUNCTION pg_catalog.pg_sequence_last_value(regclass)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$pg_sequence_last_value$function$;/*pg_nextoid 976*/CREATE OR REPLACE FUNCTION pg_catalog.pg_nextoid(regclass, name, regclass)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$pg_nextoid$function$;/*varbit_in 977*/CREATE OR REPLACE FUNCTION pg_catalog.varbit_in(cstring, oid, integer)
 RETURNS bit varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbit_in$function$;/*varbit_out 978*/CREATE OR REPLACE FUNCTION pg_catalog.varbit_out(bit varying)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbit_out$function$;/*varbittypmodin 979*/CREATE OR REPLACE FUNCTION pg_catalog.varbittypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbittypmodin$function$;/*varbittypmodout 980*/CREATE OR REPLACE FUNCTION pg_catalog.varbittypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbittypmodout$function$;/*biteq 981*/CREATE OR REPLACE FUNCTION pg_catalog.biteq(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$biteq$function$;/*bitne 982*/CREATE OR REPLACE FUNCTION pg_catalog.bitne(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitne$function$;/*bitge 983*/CREATE OR REPLACE FUNCTION pg_catalog.bitge(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitge$function$;/*bitgt 984*/CREATE OR REPLACE FUNCTION pg_catalog.bitgt(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitgt$function$;/*bitle 985*/CREATE OR REPLACE FUNCTION pg_catalog.bitle(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitle$function$;/*bitlt 986*/CREATE OR REPLACE FUNCTION pg_catalog.bitlt(bit, bit)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitlt$function$;/*bitcmp 987*/CREATE OR REPLACE FUNCTION pg_catalog.bitcmp(bit, bit)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitcmp$function$;/*random 988*/CREATE OR REPLACE FUNCTION pg_catalog.random()
 RETURNS double precision
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$drandom$function$;/*setseed 989*/CREATE OR REPLACE FUNCTION pg_catalog.setseed(double precision)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$setseed$function$;/*asin 990*/CREATE OR REPLACE FUNCTION pg_catalog.asin(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dasin$function$;/*acos 991*/CREATE OR REPLACE FUNCTION pg_catalog.acos(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dacos$function$;/*atan 992*/CREATE OR REPLACE FUNCTION pg_catalog.atan(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datan$function$;/*atan2 993*/CREATE OR REPLACE FUNCTION pg_catalog.atan2(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datan2$function$;/*sin 994*/CREATE OR REPLACE FUNCTION pg_catalog.sin(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsin$function$;/*cos 995*/CREATE OR REPLACE FUNCTION pg_catalog.cos(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcos$function$;/*tan 996*/CREATE OR REPLACE FUNCTION pg_catalog.tan(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtan$function$;/*cot 997*/CREATE OR REPLACE FUNCTION pg_catalog.cot(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcot$function$;/*asind 998*/CREATE OR REPLACE FUNCTION pg_catalog.asind(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dasind$function$;/*acosd 999*/CREATE OR REPLACE FUNCTION pg_catalog.acosd(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dacosd$function$;/*atand 1000*/CREATE OR REPLACE FUNCTION pg_catalog.atand(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datand$function$;/*atan2d 1001*/CREATE OR REPLACE FUNCTION pg_catalog.atan2d(double precision, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datan2d$function$;/*sind 1002*/CREATE OR REPLACE FUNCTION pg_catalog.sind(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsind$function$;/*cosd 1003*/CREATE OR REPLACE FUNCTION pg_catalog.cosd(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcosd$function$;/*tand 1004*/CREATE OR REPLACE FUNCTION pg_catalog.tand(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtand$function$;/*cotd 1005*/CREATE OR REPLACE FUNCTION pg_catalog.cotd(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcotd$function$;/*degrees 1006*/CREATE OR REPLACE FUNCTION pg_catalog.degrees(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$degrees$function$;/*radians 1007*/CREATE OR REPLACE FUNCTION pg_catalog.radians(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$radians$function$;/*pi 1008*/CREATE OR REPLACE FUNCTION pg_catalog.pi()
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dpi$function$;/*sinh 1009*/CREATE OR REPLACE FUNCTION pg_catalog.sinh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsinh$function$;/*cosh 1010*/CREATE OR REPLACE FUNCTION pg_catalog.cosh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dcosh$function$;/*tanh 1011*/CREATE OR REPLACE FUNCTION pg_catalog.tanh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dtanh$function$;/*asinh 1012*/CREATE OR REPLACE FUNCTION pg_catalog.asinh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dasinh$function$;/*acosh 1013*/CREATE OR REPLACE FUNCTION pg_catalog.acosh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dacosh$function$;/*atanh 1014*/CREATE OR REPLACE FUNCTION pg_catalog.atanh(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datanh$function$;/*interval_mul 1015*/CREATE OR REPLACE FUNCTION pg_catalog.interval_mul(interval, double precision)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_mul$function$;/*ascii 1016*/CREATE OR REPLACE FUNCTION pg_catalog.ascii(text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ascii$function$;/*chr 1017*/CREATE OR REPLACE FUNCTION pg_catalog.chr(integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$chr$function$;/*repeat 1018*/CREATE OR REPLACE FUNCTION pg_catalog.repeat(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$repeat$function$;/*similar_escape 1019*/CREATE OR REPLACE FUNCTION pg_catalog.similar_escape(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$similar_escape$function$;/*similar_to_escape 1020*/CREATE OR REPLACE FUNCTION pg_catalog.similar_to_escape(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$similar_to_escape_2$function$;/*similar_to_escape 1021*/CREATE OR REPLACE FUNCTION pg_catalog.similar_to_escape(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$similar_to_escape_1$function$;/*mul_d_interval 1022*/CREATE OR REPLACE FUNCTION pg_catalog.mul_d_interval(double precision, interval)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$mul_d_interval$function$;/*bpcharlike 1023*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharlike(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$textlike$function$;/*bpcharnlike 1024*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharnlike(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textnlike$function$;/*texticlike 1025*/CREATE OR REPLACE FUNCTION pg_catalog.texticlike(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticlike_support
AS $function$texticlike$function$;/*texticlike_support 1026*/CREATE OR REPLACE FUNCTION pg_catalog.texticlike_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticlike_support$function$;/*texticnlike 1027*/CREATE OR REPLACE FUNCTION pg_catalog.texticnlike(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticnlike$function$;/*nameiclike 1028*/CREATE OR REPLACE FUNCTION pg_catalog.nameiclike(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticlike_support
AS $function$nameiclike$function$;/*nameicnlike 1029*/CREATE OR REPLACE FUNCTION pg_catalog.nameicnlike(name, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$nameicnlike$function$;/*like_escape 1030*/CREATE OR REPLACE FUNCTION pg_catalog.like_escape(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$like_escape$function$;/*bpcharicregexeq 1031*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharicregexeq(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticregexeq_support
AS $function$texticregexeq$function$;/*bpcharicregexne 1032*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharicregexne(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticregexne$function$;/*bpcharregexeq 1033*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharregexeq(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textregexeq_support
AS $function$textregexeq$function$;/*bpcharregexne 1034*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharregexne(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexne$function$;/*bpchariclike 1035*/CREATE OR REPLACE FUNCTION pg_catalog.bpchariclike(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT texticlike_support
AS $function$texticlike$function$;/*bpcharicnlike 1036*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharicnlike(character, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$texticnlike$function$;/*strpos 1037*/CREATE OR REPLACE FUNCTION pg_catalog.strpos(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textpos$function$;/*lower 1038*/CREATE OR REPLACE FUNCTION pg_catalog.lower(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lower$function$;/*upper 1039*/CREATE OR REPLACE FUNCTION pg_catalog.upper(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$upper$function$;/*initcap 1040*/CREATE OR REPLACE FUNCTION pg_catalog.initcap(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$initcap$function$;/*lpad 1041*/CREATE OR REPLACE FUNCTION pg_catalog.lpad(text, integer, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lpad$function$;/*rpad 1042*/CREATE OR REPLACE FUNCTION pg_catalog.rpad(text, integer, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$rpad$function$;/*ltrim 1043*/CREATE OR REPLACE FUNCTION pg_catalog.ltrim(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ltrim$function$;/*rtrim 1044*/CREATE OR REPLACE FUNCTION pg_catalog.rtrim(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$rtrim$function$;/*substr 1045*/CREATE OR REPLACE FUNCTION pg_catalog.substr(text, integer, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_substr$function$;/*translate 1046*/CREATE OR REPLACE FUNCTION pg_catalog.translate(text, text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$translate$function$;/*lpad 1047*/CREATE OR REPLACE FUNCTION pg_catalog.lpad(text, integer)
 RETURNS text
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.lpad($1, $2, ' ')$function$;/*rpad 1048*/CREATE OR REPLACE FUNCTION pg_catalog.rpad(text, integer)
 RETURNS text
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.rpad($1, $2, ' ')$function$;/*ltrim 1049*/CREATE OR REPLACE FUNCTION pg_catalog.ltrim(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ltrim1$function$;/*rtrim 1050*/CREATE OR REPLACE FUNCTION pg_catalog.rtrim(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$rtrim1$function$;/*substr 1051*/CREATE OR REPLACE FUNCTION pg_catalog.substr(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_substr_no_len$function$;/*btrim 1052*/CREATE OR REPLACE FUNCTION pg_catalog.btrim(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btrim$function$;/*btrim 1053*/CREATE OR REPLACE FUNCTION pg_catalog.btrim(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btrim1$function$;/*substring 1054*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(text, integer, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_substr$function$;/*substring 1055*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_substr_no_len$function$;/*replace 1056*/CREATE OR REPLACE FUNCTION pg_catalog.replace(text, text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$replace_text$function$;/*regexp_replace 1057*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_replace(text, text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexreplace_noopt$function$;/*regexp_replace 1058*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_replace(text, text, text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexreplace$function$;/*regexp_match 1059*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_match(text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_match_no_flags$function$;/*regexp_match 1060*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_match(text, text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_match$function$;/*regexp_matches 1061*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_matches(text, text)
 RETURNS SETOF text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 1
AS $function$regexp_matches_no_flags$function$;/*regexp_matches 1062*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_matches(text, text, text)
 RETURNS SETOF text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 10
AS $function$regexp_matches$function$;/*split_part 1063*/CREATE OR REPLACE FUNCTION pg_catalog.split_part(text, text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$split_text$function$;/*regexp_split_to_table 1064*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_split_to_table(text, text)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_split_to_table_no_flags$function$;/*regexp_split_to_table 1065*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_split_to_table(text, text, text)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_split_to_table$function$;/*regexp_split_to_array 1066*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_split_to_array(text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_split_to_array_no_flags$function$;/*regexp_split_to_array 1067*/CREATE OR REPLACE FUNCTION pg_catalog.regexp_split_to_array(text, text, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regexp_split_to_array$function$;/*to_hex 1068*/CREATE OR REPLACE FUNCTION pg_catalog.to_hex(integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$to_hex32$function$;/*to_hex 1069*/CREATE OR REPLACE FUNCTION pg_catalog.to_hex(bigint)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$to_hex64$function$;/*getdatabaseencoding 1070*/CREATE OR REPLACE FUNCTION pg_catalog.getdatabaseencoding()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$getdatabaseencoding$function$;/*pg_client_encoding 1071*/CREATE OR REPLACE FUNCTION pg_catalog.pg_client_encoding()
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_client_encoding$function$;/*length 1072*/CREATE OR REPLACE FUNCTION pg_catalog.length(bytea, name)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$length_in_encoding$function$;/*convert_from 1073*/CREATE OR REPLACE FUNCTION pg_catalog.convert_from(bytea, name)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_convert_from$function$;/*convert_to 1074*/CREATE OR REPLACE FUNCTION pg_catalog.convert_to(text, name)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_convert_to$function$;/*convert 1075*/CREATE OR REPLACE FUNCTION pg_catalog.convert(bytea, name, name)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_convert$function$;/*pg_char_to_encoding 1076*/CREATE OR REPLACE FUNCTION pg_catalog.pg_char_to_encoding(name)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$PG_char_to_encoding$function$;/*pg_encoding_to_char 1077*/CREATE OR REPLACE FUNCTION pg_catalog.pg_encoding_to_char(integer)
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$PG_encoding_to_char$function$;/*pg_encoding_max_length 1078*/CREATE OR REPLACE FUNCTION pg_catalog.pg_encoding_max_length(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_encoding_max_length_sql$function$;/*oidgt 1079*/CREATE OR REPLACE FUNCTION pg_catalog.oidgt(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidgt$function$;/*oidge 1080*/CREATE OR REPLACE FUNCTION pg_catalog.oidge(oid, oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$oidge$function$;/*pg_get_ruledef 1081*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_ruledef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_ruledef$function$;/*pg_get_viewdef 1082*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_viewdef(text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_get_viewdef_name$function$;/*pg_get_viewdef 1083*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_viewdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_get_viewdef$function$;/*pg_get_userbyid 1084*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_userbyid(oid)
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_userbyid$function$;/*pg_get_indexdef 1085*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_indexdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_indexdef$function$;/*pg_get_statisticsobjdef 1086*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_statisticsobjdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_statisticsobjdef$function$;/*pg_get_partkeydef 1087*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_partkeydef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_partkeydef$function$;/*pg_get_partition_constraintdef 1088*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_partition_constraintdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_partition_constraintdef$function$;/*pg_get_triggerdef 1089*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_triggerdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_triggerdef$function$;/*pg_get_constraintdef 1090*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_constraintdef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_constraintdef$function$;/*pg_get_expr 1091*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_expr(pg_node_tree, oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_expr$function$;/*pg_get_serial_sequence 1092*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_serial_sequence(text, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_serial_sequence$function$;/*pg_get_functiondef 1093*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_functiondef(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_functiondef$function$;/*pg_get_function_arguments 1094*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_function_arguments(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_function_arguments$function$;/*pg_get_function_identity_arguments 1095*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_function_identity_arguments(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_function_identity_arguments$function$;/*pg_get_function_result 1096*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_function_result(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_function_result$function$;/*pg_get_function_arg_default 1097*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_function_arg_default(oid, integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_function_arg_default$function$;/*pg_get_keywords 1098*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_keywords(OUT word text, OUT catcode "char", OUT catdesc text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10 ROWS 400
AS $function$pg_get_keywords$function$;/*pg_options_to_table 1099*/CREATE OR REPLACE FUNCTION pg_catalog.pg_options_to_table(options_array text[], OUT option_name text, OUT option_value text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT ROWS 3
AS $function$pg_options_to_table$function$;/*pg_typeof 1100*/CREATE OR REPLACE FUNCTION pg_catalog.pg_typeof("any")
 RETURNS regtype
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$pg_typeof$function$;/*pg_collation_for 1101*/CREATE OR REPLACE FUNCTION pg_catalog.pg_collation_for("any")
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$pg_collation_for$function$;/*pg_relation_is_updatable 1102*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_is_updatable(regclass, boolean)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_relation_is_updatable$function$;/*pg_column_is_updatable 1103*/CREATE OR REPLACE FUNCTION pg_catalog.pg_column_is_updatable(regclass, smallint, boolean)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_column_is_updatable$function$;/*pg_get_replica_identity_index 1104*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_replica_identity_index(regclass)
 RETURNS regclass
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_get_replica_identity_index$function$;/*unique_key_recheck 1105*/CREATE OR REPLACE FUNCTION pg_catalog.unique_key_recheck()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$unique_key_recheck$function$;/*RI_FKey_check_ins 1106*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_check_ins"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_check_ins$function$;/*RI_FKey_check_upd 1107*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_check_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_check_upd$function$;/*RI_FKey_cascade_del 1108*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_cascade_del"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_cascade_del$function$;/*RI_FKey_cascade_upd 1109*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_cascade_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_cascade_upd$function$;/*RI_FKey_restrict_del 1110*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_restrict_del"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_restrict_del$function$;/*RI_FKey_restrict_upd 1111*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_restrict_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_restrict_upd$function$;/*RI_FKey_setnull_del 1112*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_setnull_del"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_setnull_del$function$;/*RI_FKey_setnull_upd 1113*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_setnull_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_setnull_upd$function$;/*RI_FKey_setdefault_del 1114*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_setdefault_del"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_setdefault_del$function$;/*RI_FKey_setdefault_upd 1115*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_setdefault_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_setdefault_upd$function$;/*RI_FKey_noaction_del 1116*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_noaction_del"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_noaction_del$function$;/*RI_FKey_noaction_upd 1117*/CREATE OR REPLACE FUNCTION pg_catalog."RI_FKey_noaction_upd"()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$RI_FKey_noaction_upd$function$;/*varbiteq 1118*/CREATE OR REPLACE FUNCTION pg_catalog.varbiteq(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$biteq$function$;/*varbitne 1119*/CREATE OR REPLACE FUNCTION pg_catalog.varbitne(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitne$function$;/*varbitge 1120*/CREATE OR REPLACE FUNCTION pg_catalog.varbitge(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitge$function$;/*varbitgt 1121*/CREATE OR REPLACE FUNCTION pg_catalog.varbitgt(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitgt$function$;/*varbitle 1122*/CREATE OR REPLACE FUNCTION pg_catalog.varbitle(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitle$function$;/*varbitlt 1123*/CREATE OR REPLACE FUNCTION pg_catalog.varbitlt(bit varying, bit varying)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitlt$function$;/*varbitcmp 1124*/CREATE OR REPLACE FUNCTION pg_catalog.varbitcmp(bit varying, bit varying)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bitcmp$function$;/*bitand 1125*/CREATE OR REPLACE FUNCTION pg_catalog.bitand(bit, bit)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_and$function$;/*bitor 1126*/CREATE OR REPLACE FUNCTION pg_catalog.bitor(bit, bit)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_or$function$;/*bitxor 1127*/CREATE OR REPLACE FUNCTION pg_catalog.bitxor(bit, bit)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitxor$function$;/*bitnot 1128*/CREATE OR REPLACE FUNCTION pg_catalog.bitnot(bit)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitnot$function$;/*bitshiftleft 1129*/CREATE OR REPLACE FUNCTION pg_catalog.bitshiftleft(bit, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitshiftleft$function$;/*bitshiftright 1130*/CREATE OR REPLACE FUNCTION pg_catalog.bitshiftright(bit, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitshiftright$function$;/*bitcat 1131*/CREATE OR REPLACE FUNCTION pg_catalog.bitcat(bit varying, bit varying)
 RETURNS bit varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitcat$function$;/*substring 1132*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(bit, integer, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitsubstr$function$;/*length 1133*/CREATE OR REPLACE FUNCTION pg_catalog.length(bit)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitlength$function$;/*octet_length 1134*/CREATE OR REPLACE FUNCTION pg_catalog.octet_length(bit)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitoctetlength$function$;/*bit 1135*/CREATE OR REPLACE FUNCTION pg_catalog."bit"(integer, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitfromint4$function$;/*int4 1136*/CREATE OR REPLACE FUNCTION pg_catalog.int4(bit)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bittoint4$function$;/*bit 1137*/CREATE OR REPLACE FUNCTION pg_catalog."bit"(bit, integer, boolean)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit$function$;/*varbit_support 1138*/CREATE OR REPLACE FUNCTION pg_catalog.varbit_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbit_support$function$;/*varbit 1139*/CREATE OR REPLACE FUNCTION pg_catalog.varbit(bit varying, integer, boolean)
 RETURNS bit varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT varbit_support
AS $function$varbit$function$;/*position 1140*/CREATE OR REPLACE FUNCTION pg_catalog."position"(bit, bit)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitposition$function$;/*substring 1141*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(bit, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitsubstr_no_len$function$;/*overlay 1142*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(bit, bit, integer, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitoverlay$function$;/*overlay 1143*/CREATE OR REPLACE FUNCTION pg_catalog."overlay"(bit, bit, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitoverlay_no_len$function$;/*get_bit 1144*/CREATE OR REPLACE FUNCTION pg_catalog.get_bit(bit, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitgetbit$function$;/*set_bit 1145*/CREATE OR REPLACE FUNCTION pg_catalog.set_bit(bit, integer, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitsetbit$function$;/*macaddr_in 1146*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_in(cstring)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_in$function$;/*macaddr_out 1147*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_out(macaddr)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_out$function$;/*trunc 1148*/CREATE OR REPLACE FUNCTION pg_catalog.trunc(macaddr)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_trunc$function$;/*macaddr_eq 1149*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_eq(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_eq$function$;/*macaddr_lt 1150*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_lt(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_lt$function$;/*macaddr_le 1151*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_le(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_le$function$;/*macaddr_gt 1152*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_gt(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_gt$function$;/*macaddr_ge 1153*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_ge(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_ge$function$;/*macaddr_ne 1154*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_ne(macaddr, macaddr)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_ne$function$;/*macaddr_cmp 1155*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_cmp(macaddr, macaddr)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr_cmp$function$;/*macaddr_not 1156*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_not(macaddr)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_not$function$;/*macaddr_and 1157*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_and(macaddr, macaddr)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_and$function$;/*macaddr_or 1158*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_or(macaddr, macaddr)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_or$function$;/*macaddr_sortsupport 1159*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_sortsupport$function$;/*macaddr8_in 1160*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_in(cstring)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_in$function$;/*macaddr8_out 1161*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_out(macaddr8)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_out$function$;/*trunc 1162*/CREATE OR REPLACE FUNCTION pg_catalog.trunc(macaddr8)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_trunc$function$;/*macaddr8_eq 1163*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_eq(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_eq$function$;/*macaddr8_lt 1164*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_lt(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_lt$function$;/*macaddr8_le 1165*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_le(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_le$function$;/*macaddr8_gt 1166*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_gt(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_gt$function$;/*macaddr8_ge 1167*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_ge(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_ge$function$;/*macaddr8_ne 1168*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_ne(macaddr8, macaddr8)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_ne$function$;/*macaddr8_cmp 1169*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_cmp(macaddr8, macaddr8)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$macaddr8_cmp$function$;/*macaddr8_not 1170*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_not(macaddr8)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_not$function$;/*macaddr8_and 1171*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_and(macaddr8, macaddr8)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_and$function$;/*macaddr8_or 1172*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_or(macaddr8, macaddr8)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_or$function$;/*macaddr8 1173*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8(macaddr)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddrtomacaddr8$function$;/*macaddr 1174*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr(macaddr8)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8tomacaddr$function$;/*macaddr8_set7bit 1175*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_set7bit(macaddr8)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_set7bit$function$;/*inet_in 1176*/CREATE OR REPLACE FUNCTION pg_catalog.inet_in(cstring)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_in$function$;/*inet_out 1177*/CREATE OR REPLACE FUNCTION pg_catalog.inet_out(inet)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_out$function$;/*cidr_in 1178*/CREATE OR REPLACE FUNCTION pg_catalog.cidr_in(cstring)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_in$function$;/*cidr_out 1179*/CREATE OR REPLACE FUNCTION pg_catalog.cidr_out(cidr)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_out$function$;/*network_eq 1180*/CREATE OR REPLACE FUNCTION pg_catalog.network_eq(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_eq$function$;/*network_lt 1181*/CREATE OR REPLACE FUNCTION pg_catalog.network_lt(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_lt$function$;/*network_le 1182*/CREATE OR REPLACE FUNCTION pg_catalog.network_le(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_le$function$;/*network_gt 1183*/CREATE OR REPLACE FUNCTION pg_catalog.network_gt(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_gt$function$;/*network_ge 1184*/CREATE OR REPLACE FUNCTION pg_catalog.network_ge(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_ge$function$;/*network_ne 1185*/CREATE OR REPLACE FUNCTION pg_catalog.network_ne(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_ne$function$;/*network_larger 1186*/CREATE OR REPLACE FUNCTION pg_catalog.network_larger(inet, inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_larger$function$;/*network_smaller 1187*/CREATE OR REPLACE FUNCTION pg_catalog.network_smaller(inet, inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_smaller$function$;/*network_cmp 1188*/CREATE OR REPLACE FUNCTION pg_catalog.network_cmp(inet, inet)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$network_cmp$function$;/*network_sub 1189*/CREATE OR REPLACE FUNCTION pg_catalog.network_sub(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT network_subset_support
AS $function$network_sub$function$;/*network_subeq 1190*/CREATE OR REPLACE FUNCTION pg_catalog.network_subeq(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT network_subset_support
AS $function$network_subeq$function$;/*network_sup 1191*/CREATE OR REPLACE FUNCTION pg_catalog.network_sup(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT network_subset_support
AS $function$network_sup$function$;/*network_supeq 1192*/CREATE OR REPLACE FUNCTION pg_catalog.network_supeq(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT network_subset_support
AS $function$network_supeq$function$;/*network_subset_support 1193*/CREATE OR REPLACE FUNCTION pg_catalog.network_subset_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_subset_support$function$;/*network_overlap 1194*/CREATE OR REPLACE FUNCTION pg_catalog.network_overlap(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_overlap$function$;/*network_sortsupport 1195*/CREATE OR REPLACE FUNCTION pg_catalog.network_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_sortsupport$function$;/*abbrev 1196*/CREATE OR REPLACE FUNCTION pg_catalog.abbrev(inet)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_abbrev$function$;/*abbrev 1197*/CREATE OR REPLACE FUNCTION pg_catalog.abbrev(cidr)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_abbrev$function$;/*set_masklen 1198*/CREATE OR REPLACE FUNCTION pg_catalog.set_masklen(inet, integer)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_set_masklen$function$;/*set_masklen 1199*/CREATE OR REPLACE FUNCTION pg_catalog.set_masklen(cidr, integer)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_set_masklen$function$;/*family 1200*/CREATE OR REPLACE FUNCTION pg_catalog.family(inet)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_family$function$;/*network 1201*/CREATE OR REPLACE FUNCTION pg_catalog.network(inet)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_network$function$;/*netmask 1202*/CREATE OR REPLACE FUNCTION pg_catalog.netmask(inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_netmask$function$;/*masklen 1203*/CREATE OR REPLACE FUNCTION pg_catalog.masklen(inet)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_masklen$function$;/*broadcast 1204*/CREATE OR REPLACE FUNCTION pg_catalog.broadcast(inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_broadcast$function$;/*host 1205*/CREATE OR REPLACE FUNCTION pg_catalog.host(inet)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_host$function$;/*text 1206*/CREATE OR REPLACE FUNCTION pg_catalog.text(inet)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_show$function$;/*hostmask 1207*/CREATE OR REPLACE FUNCTION pg_catalog.hostmask(inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$network_hostmask$function$;/*cidr 1208*/CREATE OR REPLACE FUNCTION pg_catalog.cidr(inet)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_to_cidr$function$;/*inet_client_addr 1209*/CREATE OR REPLACE FUNCTION pg_catalog.inet_client_addr()
 RETURNS inet
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED
AS $function$inet_client_addr$function$;/*inet_client_port 1210*/CREATE OR REPLACE FUNCTION pg_catalog.inet_client_port()
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED
AS $function$inet_client_port$function$;/*inet_server_addr 1211*/CREATE OR REPLACE FUNCTION pg_catalog.inet_server_addr()
 RETURNS inet
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$inet_server_addr$function$;/*inet_server_port 1212*/CREATE OR REPLACE FUNCTION pg_catalog.inet_server_port()
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$inet_server_port$function$;/*inetnot 1213*/CREATE OR REPLACE FUNCTION pg_catalog.inetnot(inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetnot$function$;/*inetand 1214*/CREATE OR REPLACE FUNCTION pg_catalog.inetand(inet, inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetand$function$;/*inetor 1215*/CREATE OR REPLACE FUNCTION pg_catalog.inetor(inet, inet)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetor$function$;/*inetpl 1216*/CREATE OR REPLACE FUNCTION pg_catalog.inetpl(inet, bigint)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetpl$function$;/*int8pl_inet 1217*/CREATE OR REPLACE FUNCTION pg_catalog.int8pl_inet(bigint, inet)
 RETURNS inet
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*inetmi_int8 1218*/CREATE OR REPLACE FUNCTION pg_catalog.inetmi_int8(inet, bigint)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetmi_int8$function$;/*inetmi 1219*/CREATE OR REPLACE FUNCTION pg_catalog.inetmi(inet, inet)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inetmi$function$;/*inet_same_family 1220*/CREATE OR REPLACE FUNCTION pg_catalog.inet_same_family(inet, inet)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_same_family$function$;/*inet_merge 1221*/CREATE OR REPLACE FUNCTION pg_catalog.inet_merge(inet, inet)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_merge$function$;/*inet_gist_consistent 1222*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_consistent(internal, inet, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_consistent$function$;/*inet_gist_union 1223*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_union(internal, internal)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_union$function$;/*inet_gist_compress 1224*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_compress$function$;/*inet_gist_fetch 1225*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_fetch(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_fetch$function$;/*inet_gist_penalty 1226*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_penalty$function$;/*inet_gist_picksplit 1227*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_picksplit$function$;/*inet_gist_same 1228*/CREATE OR REPLACE FUNCTION pg_catalog.inet_gist_same(inet, inet, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_gist_same$function$;/*inet_spg_config 1229*/CREATE OR REPLACE FUNCTION pg_catalog.inet_spg_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_spg_config$function$;/*inet_spg_choose 1230*/CREATE OR REPLACE FUNCTION pg_catalog.inet_spg_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_spg_choose$function$;/*inet_spg_picksplit 1231*/CREATE OR REPLACE FUNCTION pg_catalog.inet_spg_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_spg_picksplit$function$;/*inet_spg_inner_consistent 1232*/CREATE OR REPLACE FUNCTION pg_catalog.inet_spg_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_spg_inner_consistent$function$;/*inet_spg_leaf_consistent 1233*/CREATE OR REPLACE FUNCTION pg_catalog.inet_spg_leaf_consistent(internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_spg_leaf_consistent$function$;/*networksel 1234*/CREATE OR REPLACE FUNCTION pg_catalog.networksel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$networksel$function$;/*networkjoinsel 1235*/CREATE OR REPLACE FUNCTION pg_catalog.networkjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$networkjoinsel$function$;/*time_mi_time 1236*/CREATE OR REPLACE FUNCTION pg_catalog.time_mi_time(time without time zone, time without time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_mi_time$function$;/*boolle 1237*/CREATE OR REPLACE FUNCTION pg_catalog.boolle(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$boolle$function$;/*boolge 1238*/CREATE OR REPLACE FUNCTION pg_catalog.boolge(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$boolge$function$;/*btboolcmp 1239*/CREATE OR REPLACE FUNCTION pg_catalog.btboolcmp(boolean, boolean)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btboolcmp$function$;/*time_hash 1240*/CREATE OR REPLACE FUNCTION pg_catalog.time_hash(time without time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_hash$function$;/*time_hash_extended 1241*/CREATE OR REPLACE FUNCTION pg_catalog.time_hash_extended(time without time zone, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_hash_extended$function$;/*timetz_hash 1242*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_hash(time with time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_hash$function$;/*timetz_hash_extended 1243*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_hash_extended(time with time zone, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_hash_extended$function$;/*interval_hash 1244*/CREATE OR REPLACE FUNCTION pg_catalog.interval_hash(interval)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_hash$function$;/*interval_hash_extended 1245*/CREATE OR REPLACE FUNCTION pg_catalog.interval_hash_extended(interval, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_hash_extended$function$;/*numeric_in 1246*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_in(cstring, oid, integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_in$function$;/*numeric_out 1247*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_out(numeric)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_out$function$;/*numerictypmodin 1248*/CREATE OR REPLACE FUNCTION pg_catalog.numerictypmodin(cstring[])
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numerictypmodin$function$;/*numerictypmodout 1249*/CREATE OR REPLACE FUNCTION pg_catalog.numerictypmodout(integer)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numerictypmodout$function$;/*numeric_support 1250*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_support$function$;/*numeric 1251*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(numeric, integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT numeric_support
AS $function$numeric$function$;/*numeric_abs 1252*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_abs(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_abs$function$;/*abs 1253*/CREATE OR REPLACE FUNCTION pg_catalog.abs(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_abs$function$;/*sign 1254*/CREATE OR REPLACE FUNCTION pg_catalog.sign(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_sign$function$;/*round 1255*/CREATE OR REPLACE FUNCTION pg_catalog.round(numeric, integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_round$function$;/*round 1256*/CREATE OR REPLACE FUNCTION pg_catalog.round(numeric)
 RETURNS numeric
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.round($1,0)$function$;/*trunc 1257*/CREATE OR REPLACE FUNCTION pg_catalog.trunc(numeric, integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_trunc$function$;/*trunc 1258*/CREATE OR REPLACE FUNCTION pg_catalog.trunc(numeric)
 RETURNS numeric
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.trunc($1,0)$function$;/*ceil 1259*/CREATE OR REPLACE FUNCTION pg_catalog.ceil(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ceil$function$;/*ceiling 1260*/CREATE OR REPLACE FUNCTION pg_catalog.ceiling(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ceil$function$;/*floor 1261*/CREATE OR REPLACE FUNCTION pg_catalog.floor(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_floor$function$;/*numeric_eq 1262*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_eq(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_eq$function$;/*numeric_ne 1263*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_ne(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ne$function$;/*numeric_gt 1264*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_gt(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_gt$function$;/*numeric_ge 1265*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_ge(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ge$function$;/*numeric_lt 1266*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_lt(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_lt$function$;/*numeric_le 1267*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_le(numeric, numeric)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_le$function$;/*numeric_add 1268*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_add(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_add$function$;/*numeric_sub 1269*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_sub(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_sub$function$;/*numeric_mul 1270*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_mul(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_mul$function$;/*numeric_div 1271*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_div(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_div$function$;/*mod 1272*/CREATE OR REPLACE FUNCTION pg_catalog.mod(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_mod$function$;/*numeric_mod 1273*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_mod(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_mod$function$;/*gcd 1274*/CREATE OR REPLACE FUNCTION pg_catalog.gcd(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_gcd$function$;/*lcm 1275*/CREATE OR REPLACE FUNCTION pg_catalog.lcm(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_lcm$function$;/*sqrt 1276*/CREATE OR REPLACE FUNCTION pg_catalog.sqrt(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_sqrt$function$;/*numeric_sqrt 1277*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_sqrt(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_sqrt$function$;/*exp 1278*/CREATE OR REPLACE FUNCTION pg_catalog.exp(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_exp$function$;/*numeric_exp 1279*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_exp(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_exp$function$;/*ln 1280*/CREATE OR REPLACE FUNCTION pg_catalog.ln(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ln$function$;/*numeric_ln 1281*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_ln(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_ln$function$;/*log 1282*/CREATE OR REPLACE FUNCTION pg_catalog.log(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_log$function$;/*numeric_log 1283*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_log(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_log$function$;/*pow 1284*/CREATE OR REPLACE FUNCTION pg_catalog.pow(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_power$function$;/*power 1285*/CREATE OR REPLACE FUNCTION pg_catalog.power(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_power$function$;/*numeric_power 1286*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_power(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_power$function$;/*scale 1287*/CREATE OR REPLACE FUNCTION pg_catalog.scale(numeric)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_scale$function$;/*min_scale 1288*/CREATE OR REPLACE FUNCTION pg_catalog.min_scale(numeric)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_min_scale$function$;/*trim_scale 1289*/CREATE OR REPLACE FUNCTION pg_catalog.trim_scale(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_trim_scale$function$;/*numeric 1290*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_numeric$function$;/*log 1291*/CREATE OR REPLACE FUNCTION pg_catalog.log(numeric)
 RETURNS numeric
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.log(10, $1)$function$;/*log10 1292*/CREATE OR REPLACE FUNCTION pg_catalog.log10(numeric)
 RETURNS numeric
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.log(10, $1)$function$;/*numeric 1293*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(real)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4_numeric$function$;/*numeric 1294*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(double precision)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_numeric$function$;/*int4 1295*/CREATE OR REPLACE FUNCTION pg_catalog.int4(numeric)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_int4$function$;/*float4 1296*/CREATE OR REPLACE FUNCTION pg_catalog.float4(numeric)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_float4$function$;/*float8 1297*/CREATE OR REPLACE FUNCTION pg_catalog.float8(numeric)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_float8$function$;/*div 1298*/CREATE OR REPLACE FUNCTION pg_catalog.div(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_div_trunc$function$;/*numeric_div_trunc 1299*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_div_trunc(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_div_trunc$function$;/*width_bucket 1300*/CREATE OR REPLACE FUNCTION pg_catalog.width_bucket(numeric, numeric, numeric, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$width_bucket_numeric$function$;/*time_pl_interval 1301*/CREATE OR REPLACE FUNCTION pg_catalog.time_pl_interval(time without time zone, interval)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_pl_interval$function$;/*time_mi_interval 1302*/CREATE OR REPLACE FUNCTION pg_catalog.time_mi_interval(time without time zone, interval)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_mi_interval$function$;/*timetz_pl_interval 1303*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_pl_interval(time with time zone, interval)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_pl_interval$function$;/*timetz_mi_interval 1304*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_mi_interval(time with time zone, interval)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_mi_interval$function$;/*numeric_inc 1305*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_inc(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_inc$function$;/*numeric_smaller 1306*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_smaller(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_smaller$function$;/*numeric_larger 1307*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_larger(numeric, numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_larger$function$;/*numeric_cmp 1308*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_cmp(numeric, numeric)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_cmp$function$;/*numeric_sortsupport 1309*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_sortsupport$function$;/*numeric_uminus 1310*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_uminus(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_uminus$function$;/*int8 1311*/CREATE OR REPLACE FUNCTION pg_catalog.int8(numeric)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_int8$function$;/*numeric 1312*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(bigint)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8_numeric$function$;/*numeric 1313*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(smallint)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2_numeric$function$;/*int2 1314*/CREATE OR REPLACE FUNCTION pg_catalog.int2(numeric)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_int2$function$;/*bool 1315*/CREATE OR REPLACE FUNCTION pg_catalog.bool(jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_bool$function$;/*numeric 1316*/CREATE OR REPLACE FUNCTION pg_catalog."numeric"(jsonb)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_numeric$function$;/*int2 1317*/CREATE OR REPLACE FUNCTION pg_catalog.int2(jsonb)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_int2$function$;/*int4 1318*/CREATE OR REPLACE FUNCTION pg_catalog.int4(jsonb)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_int4$function$;/*int8 1319*/CREATE OR REPLACE FUNCTION pg_catalog.int8(jsonb)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_int8$function$;/*float4 1320*/CREATE OR REPLACE FUNCTION pg_catalog.float4(jsonb)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_float4$function$;/*float8 1321*/CREATE OR REPLACE FUNCTION pg_catalog.float8(jsonb)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_float8$function$;/*to_char 1322*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(timestamp with time zone, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_to_char$function$;/*to_char 1323*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(numeric, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$numeric_to_char$function$;/*to_char 1324*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(integer, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$int4_to_char$function$;/*to_char 1325*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(bigint, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$int8_to_char$function$;/*to_char 1326*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(real, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$float4_to_char$function$;/*to_char 1327*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(double precision, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$float8_to_char$function$;/*to_number 1328*/CREATE OR REPLACE FUNCTION pg_catalog.to_number(text, text)
 RETURNS numeric
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$numeric_to_number$function$;/*to_timestamp 1329*/CREATE OR REPLACE FUNCTION pg_catalog.to_timestamp(text, text)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_timestamp$function$;/*to_date 1330*/CREATE OR REPLACE FUNCTION pg_catalog.to_date(text, text)
 RETURNS date
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_date$function$;/*to_char 1331*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(interval, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$interval_to_char$function$;/*quote_ident 1332*/CREATE OR REPLACE FUNCTION pg_catalog.quote_ident(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$quote_ident$function$;/*quote_literal 1333*/CREATE OR REPLACE FUNCTION pg_catalog.quote_literal(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$quote_literal$function$;/*quote_literal 1334*/CREATE OR REPLACE FUNCTION pg_catalog.quote_literal(anyelement)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.quote_literal($1::pg_catalog.text)$function$;/*quote_nullable 1335*/CREATE OR REPLACE FUNCTION pg_catalog.quote_nullable(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$quote_nullable$function$;/*quote_nullable 1336*/CREATE OR REPLACE FUNCTION pg_catalog.quote_nullable(anyelement)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE COST 1
AS $function$select pg_catalog.quote_nullable($1::pg_catalog.text)$function$;/*oidin 1337*/CREATE OR REPLACE FUNCTION pg_catalog.oidin(cstring)
 RETURNS oid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidin$function$;/*oidout 1338*/CREATE OR REPLACE FUNCTION pg_catalog.oidout(oid)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidout$function$;/*concat 1339*/CREATE OR REPLACE FUNCTION pg_catalog.concat(VARIADIC "any")
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$text_concat$function$;/*concat_ws 1340*/CREATE OR REPLACE FUNCTION pg_catalog.concat_ws(text, VARIADIC "any")
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$text_concat_ws$function$;/*left 1341*/CREATE OR REPLACE FUNCTION pg_catalog."left"(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_left$function$;/*right 1342*/CREATE OR REPLACE FUNCTION pg_catalog."right"(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_right$function$;/*reverse 1343*/CREATE OR REPLACE FUNCTION pg_catalog.reverse(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$text_reverse$function$;/*format 1344*/CREATE OR REPLACE FUNCTION pg_catalog.format(text, VARIADIC "any")
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$text_format$function$;/*format 1345*/CREATE OR REPLACE FUNCTION pg_catalog.format(text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$text_format_nv$function$;/*bit_length 1346*/CREATE OR REPLACE FUNCTION pg_catalog.bit_length(bytea)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.octet_length($1) * 8$function$;/*bit_length 1347*/CREATE OR REPLACE FUNCTION pg_catalog.bit_length(text)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.octet_length($1) * 8$function$;/*bit_length 1348*/CREATE OR REPLACE FUNCTION pg_catalog.bit_length(bit)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.length($1)$function$;/*iclikesel 1349*/CREATE OR REPLACE FUNCTION pg_catalog.iclikesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$iclikesel$function$;/*icnlikesel 1350*/CREATE OR REPLACE FUNCTION pg_catalog.icnlikesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icnlikesel$function$;/*iclikejoinsel 1351*/CREATE OR REPLACE FUNCTION pg_catalog.iclikejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$iclikejoinsel$function$;/*icnlikejoinsel 1352*/CREATE OR REPLACE FUNCTION pg_catalog.icnlikejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icnlikejoinsel$function$;/*regexeqsel 1353*/CREATE OR REPLACE FUNCTION pg_catalog.regexeqsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regexeqsel$function$;/*likesel 1354*/CREATE OR REPLACE FUNCTION pg_catalog.likesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$likesel$function$;/*icregexeqsel 1355*/CREATE OR REPLACE FUNCTION pg_catalog.icregexeqsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icregexeqsel$function$;/*regexnesel 1356*/CREATE OR REPLACE FUNCTION pg_catalog.regexnesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regexnesel$function$;/*nlikesel 1357*/CREATE OR REPLACE FUNCTION pg_catalog.nlikesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$nlikesel$function$;/*icregexnesel 1358*/CREATE OR REPLACE FUNCTION pg_catalog.icregexnesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icregexnesel$function$;/*regexeqjoinsel 1359*/CREATE OR REPLACE FUNCTION pg_catalog.regexeqjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regexeqjoinsel$function$;/*likejoinsel 1360*/CREATE OR REPLACE FUNCTION pg_catalog.likejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$likejoinsel$function$;/*icregexeqjoinsel 1361*/CREATE OR REPLACE FUNCTION pg_catalog.icregexeqjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icregexeqjoinsel$function$;/*regexnejoinsel 1362*/CREATE OR REPLACE FUNCTION pg_catalog.regexnejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regexnejoinsel$function$;/*nlikejoinsel 1363*/CREATE OR REPLACE FUNCTION pg_catalog.nlikejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$nlikejoinsel$function$;/*icregexnejoinsel 1364*/CREATE OR REPLACE FUNCTION pg_catalog.icregexnejoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$icregexnejoinsel$function$;/*prefixsel 1365*/CREATE OR REPLACE FUNCTION pg_catalog.prefixsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$prefixsel$function$;/*prefixjoinsel 1366*/CREATE OR REPLACE FUNCTION pg_catalog.prefixjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$prefixjoinsel$function$;/*float8_avg 1367*/CREATE OR REPLACE FUNCTION pg_catalog.float8_avg(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_avg$function$;/*float8_var_pop 1368*/CREATE OR REPLACE FUNCTION pg_catalog.float8_var_pop(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_var_pop$function$;/*float8_var_samp 1369*/CREATE OR REPLACE FUNCTION pg_catalog.float8_var_samp(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_var_samp$function$;/*float8_stddev_pop 1370*/CREATE OR REPLACE FUNCTION pg_catalog.float8_stddev_pop(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_stddev_pop$function$;/*float8_stddev_samp 1371*/CREATE OR REPLACE FUNCTION pg_catalog.float8_stddev_samp(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_stddev_samp$function$;/*numeric_accum 1372*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_accum(internal, numeric)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_accum$function$;/*numeric_combine 1373*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_combine(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_combine$function$;/*numeric_avg_accum 1374*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_avg_accum(internal, numeric)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_avg_accum$function$;/*numeric_avg_combine 1375*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_avg_combine(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_avg_combine$function$;/*numeric_avg_serialize 1376*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_avg_serialize(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_avg_serialize$function$;/*numeric_avg_deserialize 1377*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_avg_deserialize(bytea, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_avg_deserialize$function$;/*numeric_serialize 1378*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_serialize(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_serialize$function$;/*numeric_deserialize 1379*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_deserialize(bytea, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_deserialize$function$;/*numeric_accum_inv 1380*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_accum_inv(internal, numeric)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_accum_inv$function$;/*int2_accum 1381*/CREATE OR REPLACE FUNCTION pg_catalog.int2_accum(internal, smallint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int2_accum$function$;/*int4_accum 1382*/CREATE OR REPLACE FUNCTION pg_catalog.int4_accum(internal, integer)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int4_accum$function$;/*int8_accum 1383*/CREATE OR REPLACE FUNCTION pg_catalog.int8_accum(internal, bigint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_accum$function$;/*numeric_poly_combine 1384*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_combine(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_combine$function$;/*numeric_poly_serialize 1385*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_serialize(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_poly_serialize$function$;/*numeric_poly_deserialize 1386*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_deserialize(bytea, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_poly_deserialize$function$;/*int8_avg_accum 1387*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg_accum(internal, bigint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_avg_accum$function$;/*int2_accum_inv 1388*/CREATE OR REPLACE FUNCTION pg_catalog.int2_accum_inv(internal, smallint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int2_accum_inv$function$;/*int4_accum_inv 1389*/CREATE OR REPLACE FUNCTION pg_catalog.int4_accum_inv(internal, integer)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int4_accum_inv$function$;/*int8_accum_inv 1390*/CREATE OR REPLACE FUNCTION pg_catalog.int8_accum_inv(internal, bigint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_accum_inv$function$;/*int8_avg_accum_inv 1391*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg_accum_inv(internal, bigint)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_avg_accum_inv$function$;/*int8_avg_combine 1392*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg_combine(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_avg_combine$function$;/*int8_avg_serialize 1393*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg_serialize(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8_avg_serialize$function$;/*int8_avg_deserialize 1394*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg_deserialize(bytea, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8_avg_deserialize$function$;/*int4_avg_combine 1395*/CREATE OR REPLACE FUNCTION pg_catalog.int4_avg_combine(bigint[], bigint[])
 RETURNS bigint[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_avg_combine$function$;/*numeric_sum 1396*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_sum(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_sum$function$;/*numeric_avg 1397*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_avg(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_avg$function$;/*numeric_var_pop 1398*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_var_pop(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_var_pop$function$;/*numeric_var_samp 1399*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_var_samp(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_var_samp$function$;/*numeric_stddev_pop 1400*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_stddev_pop(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_stddev_pop$function$;/*numeric_stddev_samp 1401*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_stddev_samp(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_stddev_samp$function$;/*int2_sum 1402*/CREATE OR REPLACE FUNCTION pg_catalog.int2_sum(bigint, smallint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int2_sum$function$;/*int4_sum 1403*/CREATE OR REPLACE FUNCTION pg_catalog.int4_sum(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int4_sum$function$;/*int8_sum 1404*/CREATE OR REPLACE FUNCTION pg_catalog.int8_sum(numeric, bigint)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$int8_sum$function$;/*numeric_poly_sum 1405*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_sum(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_sum$function$;/*numeric_poly_avg 1406*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_avg(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_avg$function$;/*numeric_poly_var_pop 1407*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_var_pop(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_var_pop$function$;/*numeric_poly_var_samp 1408*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_var_samp(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_var_samp$function$;/*numeric_poly_stddev_pop 1409*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_stddev_pop(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_stddev_pop$function$;/*numeric_poly_stddev_samp 1410*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_poly_stddev_samp(internal)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$numeric_poly_stddev_samp$function$;/*interval_accum 1411*/CREATE OR REPLACE FUNCTION pg_catalog.interval_accum(interval[], interval)
 RETURNS interval[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_accum$function$;/*interval_combine 1412*/CREATE OR REPLACE FUNCTION pg_catalog.interval_combine(interval[], interval[])
 RETURNS interval[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_combine$function$;/*interval_accum_inv 1413*/CREATE OR REPLACE FUNCTION pg_catalog.interval_accum_inv(interval[], interval)
 RETURNS interval[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_accum_inv$function$;/*interval_avg 1414*/CREATE OR REPLACE FUNCTION pg_catalog.interval_avg(interval[])
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_avg$function$;/*int2_avg_accum 1415*/CREATE OR REPLACE FUNCTION pg_catalog.int2_avg_accum(bigint[], smallint)
 RETURNS bigint[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2_avg_accum$function$;/*int4_avg_accum 1416*/CREATE OR REPLACE FUNCTION pg_catalog.int4_avg_accum(bigint[], integer)
 RETURNS bigint[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_avg_accum$function$;/*int2_avg_accum_inv 1417*/CREATE OR REPLACE FUNCTION pg_catalog.int2_avg_accum_inv(bigint[], smallint)
 RETURNS bigint[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2_avg_accum_inv$function$;/*int4_avg_accum_inv 1418*/CREATE OR REPLACE FUNCTION pg_catalog.int4_avg_accum_inv(bigint[], integer)
 RETURNS bigint[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_avg_accum_inv$function$;/*int8_avg 1419*/CREATE OR REPLACE FUNCTION pg_catalog.int8_avg(bigint[])
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8_avg$function$;/*int2int4_sum 1420*/CREATE OR REPLACE FUNCTION pg_catalog.int2int4_sum(bigint[])
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2int4_sum$function$;/*int8inc_float8_float8 1421*/CREATE OR REPLACE FUNCTION pg_catalog.int8inc_float8_float8(bigint, double precision, double precision)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8inc_float8_float8$function$;/*float8_regr_accum 1422*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_accum(double precision[], double precision, double precision)
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_accum$function$;/*float8_regr_combine 1423*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_combine(double precision[], double precision[])
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_combine$function$;/*float8_regr_sxx 1424*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_sxx(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_sxx$function$;/*float8_regr_syy 1425*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_syy(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_syy$function$;/*float8_regr_sxy 1426*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_sxy(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_sxy$function$;/*float8_regr_avgx 1427*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_avgx(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_avgx$function$;/*float8_regr_avgy 1428*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_avgy(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_avgy$function$;/*float8_regr_r2 1429*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_r2(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_r2$function$;/*float8_regr_slope 1430*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_slope(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_slope$function$;/*float8_regr_intercept 1431*/CREATE OR REPLACE FUNCTION pg_catalog.float8_regr_intercept(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_regr_intercept$function$;/*float8_covar_pop 1432*/CREATE OR REPLACE FUNCTION pg_catalog.float8_covar_pop(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_covar_pop$function$;/*float8_covar_samp 1433*/CREATE OR REPLACE FUNCTION pg_catalog.float8_covar_samp(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_covar_samp$function$;/*float8_corr 1434*/CREATE OR REPLACE FUNCTION pg_catalog.float8_corr(double precision[])
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8_corr$function$;/*string_agg_transfn 1435*/CREATE OR REPLACE FUNCTION pg_catalog.string_agg_transfn(internal, text, text)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$string_agg_transfn$function$;/*string_agg_finalfn 1436*/CREATE OR REPLACE FUNCTION pg_catalog.string_agg_finalfn(internal)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$string_agg_finalfn$function$;/*bytea_string_agg_transfn 1437*/CREATE OR REPLACE FUNCTION pg_catalog.bytea_string_agg_transfn(internal, bytea, bytea)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$bytea_string_agg_transfn$function$;/*bytea_string_agg_finalfn 1438*/CREATE OR REPLACE FUNCTION pg_catalog.bytea_string_agg_finalfn(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$bytea_string_agg_finalfn$function$;/*to_ascii 1439*/CREATE OR REPLACE FUNCTION pg_catalog.to_ascii(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$to_ascii_default$function$;/*to_ascii 1440*/CREATE OR REPLACE FUNCTION pg_catalog.to_ascii(text, integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$to_ascii_enc$function$;/*to_ascii 1441*/CREATE OR REPLACE FUNCTION pg_catalog.to_ascii(text, name)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$to_ascii_encname$function$;/*interval_pl_time 1442*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl_time(interval, time without time zone)
 RETURNS time without time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*int28eq 1443*/CREATE OR REPLACE FUNCTION pg_catalog.int28eq(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28eq$function$;/*int28ne 1444*/CREATE OR REPLACE FUNCTION pg_catalog.int28ne(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28ne$function$;/*int28lt 1445*/CREATE OR REPLACE FUNCTION pg_catalog.int28lt(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28lt$function$;/*int28gt 1446*/CREATE OR REPLACE FUNCTION pg_catalog.int28gt(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28gt$function$;/*int28le 1447*/CREATE OR REPLACE FUNCTION pg_catalog.int28le(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28le$function$;/*int28ge 1448*/CREATE OR REPLACE FUNCTION pg_catalog.int28ge(smallint, bigint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int28ge$function$;/*int82eq 1449*/CREATE OR REPLACE FUNCTION pg_catalog.int82eq(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82eq$function$;/*int82ne 1450*/CREATE OR REPLACE FUNCTION pg_catalog.int82ne(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82ne$function$;/*int82lt 1451*/CREATE OR REPLACE FUNCTION pg_catalog.int82lt(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82lt$function$;/*int82gt 1452*/CREATE OR REPLACE FUNCTION pg_catalog.int82gt(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82gt$function$;/*int82le 1453*/CREATE OR REPLACE FUNCTION pg_catalog.int82le(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82le$function$;/*int82ge 1454*/CREATE OR REPLACE FUNCTION pg_catalog.int82ge(bigint, smallint)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$int82ge$function$;/*int2and 1455*/CREATE OR REPLACE FUNCTION pg_catalog.int2and(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2and$function$;/*int2or 1456*/CREATE OR REPLACE FUNCTION pg_catalog.int2or(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2or$function$;/*int2xor 1457*/CREATE OR REPLACE FUNCTION pg_catalog.int2xor(smallint, smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2xor$function$;/*int2not 1458*/CREATE OR REPLACE FUNCTION pg_catalog.int2not(smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2not$function$;/*int2shl 1459*/CREATE OR REPLACE FUNCTION pg_catalog.int2shl(smallint, integer)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2shl$function$;/*int2shr 1460*/CREATE OR REPLACE FUNCTION pg_catalog.int2shr(smallint, integer)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2shr$function$;/*int4and 1461*/CREATE OR REPLACE FUNCTION pg_catalog.int4and(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4and$function$;/*int4or 1462*/CREATE OR REPLACE FUNCTION pg_catalog.int4or(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4or$function$;/*int4xor 1463*/CREATE OR REPLACE FUNCTION pg_catalog.int4xor(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4xor$function$;/*int4not 1464*/CREATE OR REPLACE FUNCTION pg_catalog.int4not(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4not$function$;/*int4shl 1465*/CREATE OR REPLACE FUNCTION pg_catalog.int4shl(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4shl$function$;/*int4shr 1466*/CREATE OR REPLACE FUNCTION pg_catalog.int4shr(integer, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4shr$function$;/*int8and 1467*/CREATE OR REPLACE FUNCTION pg_catalog.int8and(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8and$function$;/*int8or 1468*/CREATE OR REPLACE FUNCTION pg_catalog.int8or(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8or$function$;/*int8xor 1469*/CREATE OR REPLACE FUNCTION pg_catalog.int8xor(bigint, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8xor$function$;/*int8not 1470*/CREATE OR REPLACE FUNCTION pg_catalog.int8not(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8not$function$;/*int8shl 1471*/CREATE OR REPLACE FUNCTION pg_catalog.int8shl(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8shl$function$;/*int8shr 1472*/CREATE OR REPLACE FUNCTION pg_catalog.int8shr(bigint, integer)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8shr$function$;/*int8up 1473*/CREATE OR REPLACE FUNCTION pg_catalog.int8up(bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8up$function$;/*int2up 1474*/CREATE OR REPLACE FUNCTION pg_catalog.int2up(smallint)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2up$function$;/*int4up 1475*/CREATE OR REPLACE FUNCTION pg_catalog.int4up(integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4up$function$;/*float4up 1476*/CREATE OR REPLACE FUNCTION pg_catalog.float4up(real)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4up$function$;/*float8up 1477*/CREATE OR REPLACE FUNCTION pg_catalog.float8up(double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8up$function$;/*numeric_uplus 1478*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_uplus(numeric)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_uplus$function$;/*has_table_privilege 1479*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_name_name$function$;/*has_table_privilege 1480*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_name_id$function$;/*has_table_privilege 1481*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_id_name$function$;/*has_table_privilege 1482*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_id_id$function$;/*has_table_privilege 1483*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_name$function$;/*has_table_privilege 1484*/CREATE OR REPLACE FUNCTION pg_catalog.has_table_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_table_privilege_id$function$;/*has_sequence_privilege 1485*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_name_name$function$;/*has_sequence_privilege 1486*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_name_id$function$;/*has_sequence_privilege 1487*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_id_name$function$;/*has_sequence_privilege 1488*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_id_id$function$;/*has_sequence_privilege 1489*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_name$function$;/*has_sequence_privilege 1490*/CREATE OR REPLACE FUNCTION pg_catalog.has_sequence_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_sequence_privilege_id$function$;/*has_column_privilege 1491*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(name, text, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_name_name$function$;/*has_column_privilege 1492*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(name, text, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_name_attnum$function$;/*has_column_privilege 1493*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(name, oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_id_name$function$;/*has_column_privilege 1494*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(name, oid, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_id_attnum$function$;/*has_column_privilege 1495*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, text, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_name_name$function$;/*has_column_privilege 1496*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, text, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_name_attnum$function$;/*has_column_privilege 1497*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_id_name$function$;/*has_column_privilege 1498*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, oid, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_id_attnum$function$;/*has_column_privilege 1499*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(text, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_name$function$;/*has_column_privilege 1500*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(text, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_name_attnum$function$;/*has_column_privilege 1501*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_name$function$;/*has_column_privilege 1502*/CREATE OR REPLACE FUNCTION pg_catalog.has_column_privilege(oid, smallint, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_column_privilege_id_attnum$function$;/*has_any_column_privilege 1503*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_name_name$function$;/*has_any_column_privilege 1504*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_name_id$function$;/*has_any_column_privilege 1505*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_id_name$function$;/*has_any_column_privilege 1506*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_id_id$function$;/*has_any_column_privilege 1507*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_name$function$;/*has_any_column_privilege 1508*/CREATE OR REPLACE FUNCTION pg_catalog.has_any_column_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$has_any_column_privilege_id$function$;/*pg_ndistinct_in 1509*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ndistinct_in(cstring)
 RETURNS pg_ndistinct
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ndistinct_in$function$;/*pg_ndistinct_out 1510*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ndistinct_out(pg_ndistinct)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_ndistinct_out$function$;/*pg_ndistinct_recv 1511*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ndistinct_recv(internal)
 RETURNS pg_ndistinct
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_ndistinct_recv$function$;/*pg_ndistinct_send 1512*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ndistinct_send(pg_ndistinct)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_ndistinct_send$function$;/*pg_dependencies_in 1513*/CREATE OR REPLACE FUNCTION pg_catalog.pg_dependencies_in(cstring)
 RETURNS pg_dependencies
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_dependencies_in$function$;/*pg_dependencies_out 1514*/CREATE OR REPLACE FUNCTION pg_catalog.pg_dependencies_out(pg_dependencies)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_dependencies_out$function$;/*pg_dependencies_recv 1515*/CREATE OR REPLACE FUNCTION pg_catalog.pg_dependencies_recv(internal)
 RETURNS pg_dependencies
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_dependencies_recv$function$;/*pg_dependencies_send 1516*/CREATE OR REPLACE FUNCTION pg_catalog.pg_dependencies_send(pg_dependencies)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_dependencies_send$function$;/*pg_mcv_list_in 1517*/CREATE OR REPLACE FUNCTION pg_catalog.pg_mcv_list_in(cstring)
 RETURNS pg_mcv_list
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_mcv_list_in$function$;/*pg_mcv_list_out 1518*/CREATE OR REPLACE FUNCTION pg_catalog.pg_mcv_list_out(pg_mcv_list)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_mcv_list_out$function$;/*pg_mcv_list_recv 1519*/CREATE OR REPLACE FUNCTION pg_catalog.pg_mcv_list_recv(internal)
 RETURNS pg_mcv_list
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_mcv_list_recv$function$;/*pg_mcv_list_send 1520*/CREATE OR REPLACE FUNCTION pg_catalog.pg_mcv_list_send(pg_mcv_list)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_mcv_list_send$function$;/*pg_mcv_list_items 1521*/CREATE OR REPLACE FUNCTION pg_catalog.pg_mcv_list_items(mcv_list pg_mcv_list, OUT index integer, OUT "values" text[], OUT nulls boolean[], OUT frequency double precision, OUT base_frequency double precision)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_stats_ext_mcvlist_items$function$;/*pg_stat_get_numscans 1522*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_numscans(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_numscans$function$;/*pg_stat_get_tuples_returned 1523*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_returned(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_returned$function$;/*pg_stat_get_tuples_fetched 1524*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_fetched$function$;/*pg_stat_get_tuples_inserted 1525*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_inserted(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_inserted$function$;/*pg_stat_get_tuples_updated 1526*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_updated(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_updated$function$;/*pg_stat_get_tuples_deleted 1527*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_deleted(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_deleted$function$;/*pg_stat_get_tuples_hot_updated 1528*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_tuples_hot_updated(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_tuples_hot_updated$function$;/*pg_stat_get_live_tuples 1529*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_live_tuples(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_live_tuples$function$;/*pg_stat_get_dead_tuples 1530*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_dead_tuples(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_dead_tuples$function$;/*pg_stat_get_mod_since_analyze 1531*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_mod_since_analyze(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_mod_since_analyze$function$;/*pg_stat_get_ins_since_vacuum 1532*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_ins_since_vacuum(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_ins_since_vacuum$function$;/*pg_stat_get_blocks_fetched 1533*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_blocks_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_blocks_fetched$function$;/*pg_stat_get_blocks_hit 1534*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_blocks_hit(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_blocks_hit$function$;/*pg_stat_get_last_vacuum_time 1535*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_last_vacuum_time(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_last_vacuum_time$function$;/*pg_stat_get_last_autovacuum_time 1536*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_last_autovacuum_time(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_last_autovacuum_time$function$;/*pg_stat_get_last_analyze_time 1537*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_last_analyze_time(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_last_analyze_time$function$;/*pg_stat_get_last_autoanalyze_time 1538*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_last_autoanalyze_time(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_last_autoanalyze_time$function$;/*pg_stat_get_vacuum_count 1539*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_vacuum_count(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_vacuum_count$function$;/*pg_stat_get_autovacuum_count 1540*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_autovacuum_count(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_autovacuum_count$function$;/*pg_stat_get_analyze_count 1541*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_analyze_count(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_analyze_count$function$;/*pg_stat_get_autoanalyze_count 1542*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_autoanalyze_count(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_autoanalyze_count$function$;/*pg_stat_get_backend_idset 1543*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_idset()
 RETURNS SETOF integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT ROWS 100
AS $function$pg_stat_get_backend_idset$function$;/*pg_stat_get_activity 1544*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_activity(pid integer, OUT datid oid, OUT pid integer, OUT usesysid oid, OUT application_name text, OUT state text, OUT query text, OUT wait_event_type text, OUT wait_event text, OUT xact_start timestamp with time zone, OUT query_start timestamp with time zone, OUT backend_start timestamp with time zone, OUT state_change timestamp with time zone, OUT client_addr inet, OUT client_hostname text, OUT client_port integer, OUT backend_xid xid, OUT backend_xmin xid, OUT backend_type text, OUT ssl boolean, OUT sslversion text, OUT sslcipher text, OUT sslbits integer, OUT sslcompression boolean, OUT ssl_client_dn text, OUT ssl_client_serial numeric, OUT ssl_issuer_dn text, OUT gss_auth boolean, OUT gss_princ text, OUT gss_enc boolean, OUT leader_pid integer)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED ROWS 100
AS $function$pg_stat_get_activity$function$;/*pg_stat_get_progress_info 1545*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_progress_info(cmdtype text, OUT pid integer, OUT datid oid, OUT relid oid, OUT param1 bigint, OUT param2 bigint, OUT param3 bigint, OUT param4 bigint, OUT param5 bigint, OUT param6 bigint, OUT param7 bigint, OUT param8 bigint, OUT param9 bigint, OUT param10 bigint, OUT param11 bigint, OUT param12 bigint, OUT param13 bigint, OUT param14 bigint, OUT param15 bigint, OUT param16 bigint, OUT param17 bigint, OUT param18 bigint, OUT param19 bigint, OUT param20 bigint)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT ROWS 100
AS $function$pg_stat_get_progress_info$function$;/*pg_stat_get_wal_senders 1546*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_wal_senders(OUT pid integer, OUT state text, OUT sent_lsn pg_lsn, OUT write_lsn pg_lsn, OUT flush_lsn pg_lsn, OUT replay_lsn pg_lsn, OUT write_lag interval, OUT flush_lag interval, OUT replay_lag interval, OUT sync_priority integer, OUT sync_state text, OUT reply_time timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED ROWS 10
AS $function$pg_stat_get_wal_senders$function$;/*pg_stat_get_db_temp_bytes 1547*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_temp_bytes(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_temp_bytes$function$;/*pg_stat_get_db_blk_read_time 1548*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_blk_read_time(oid)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_blk_read_time$function$;/*pg_stat_get_wal_receiver 1549*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_wal_receiver(OUT pid integer, OUT status text, OUT receive_start_lsn pg_lsn, OUT receive_start_tli integer, OUT written_lsn pg_lsn, OUT flushed_lsn pg_lsn, OUT received_tli integer, OUT last_msg_send_time timestamp with time zone, OUT last_msg_receipt_time timestamp with time zone, OUT latest_end_lsn pg_lsn, OUT latest_end_time timestamp with time zone, OUT slot_name text, OUT sender_host text, OUT sender_port integer, OUT conninfo text)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED
AS $function$pg_stat_get_wal_receiver$function$;/*pg_stat_get_subscription 1550*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_subscription(subid oid, OUT subid oid, OUT relid oid, OUT pid integer, OUT received_lsn pg_lsn, OUT last_msg_send_time timestamp with time zone, OUT last_msg_receipt_time timestamp with time zone, OUT latest_end_lsn pg_lsn, OUT latest_end_time timestamp with time zone)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED
AS $function$pg_stat_get_subscription$function$;/*pg_backend_pid 1551*/CREATE OR REPLACE FUNCTION pg_catalog.pg_backend_pid()
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_backend_pid$function$;/*pg_stat_get_backend_pid 1552*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_pid(integer)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_pid$function$;/*pg_stat_get_backend_dbid 1553*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_dbid(integer)
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_dbid$function$;/*pg_stat_get_backend_userid 1554*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_userid(integer)
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_userid$function$;/*pg_stat_get_backend_activity 1555*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_activity(integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_activity$function$;/*pg_stat_get_backend_wait_event_type 1556*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_wait_event_type(integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_wait_event_type$function$;/*pg_stat_get_backend_wait_event 1557*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_wait_event(integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_wait_event$function$;/*pg_stat_get_backend_activity_start 1558*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_activity_start(integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_activity_start$function$;/*pg_stat_get_backend_xact_start 1559*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_xact_start(integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_xact_start$function$;/*pg_stat_get_backend_start 1560*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_start(integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_start$function$;/*pg_stat_get_backend_client_addr 1561*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_client_addr(integer)
 RETURNS inet
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_client_addr$function$;/*pg_stat_get_backend_client_port 1562*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_backend_client_port(integer)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_backend_client_port$function$;/*pg_stat_get_db_numbackends 1563*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_numbackends(oid)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_numbackends$function$;/*pg_stat_get_db_xact_commit 1564*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_xact_commit(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_xact_commit$function$;/*pg_stat_get_db_xact_rollback 1565*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_xact_rollback(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_xact_rollback$function$;/*pg_stat_get_db_blocks_fetched 1566*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_blocks_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_blocks_fetched$function$;/*pg_stat_get_db_blocks_hit 1567*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_blocks_hit(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_blocks_hit$function$;/*pg_stat_get_db_tuples_returned 1568*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_tuples_returned(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_tuples_returned$function$;/*pg_stat_get_db_tuples_fetched 1569*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_tuples_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_tuples_fetched$function$;/*pg_stat_get_db_tuples_inserted 1570*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_tuples_inserted(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_tuples_inserted$function$;/*pg_stat_get_db_tuples_updated 1571*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_tuples_updated(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_tuples_updated$function$;/*pg_stat_get_db_tuples_deleted 1572*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_tuples_deleted(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_tuples_deleted$function$;/*pg_stat_get_db_conflict_tablespace 1573*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_tablespace(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_tablespace$function$;/*pg_stat_get_db_conflict_lock 1574*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_lock(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_lock$function$;/*pg_stat_get_db_conflict_snapshot 1575*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_snapshot(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_snapshot$function$;/*pg_stat_get_db_conflict_bufferpin 1576*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_bufferpin(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_bufferpin$function$;/*pg_stat_get_db_conflict_startup_deadlock 1577*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_startup_deadlock(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_startup_deadlock$function$;/*pg_stat_get_db_conflict_all 1578*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_conflict_all(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_conflict_all$function$;/*pg_stat_get_db_deadlocks 1579*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_deadlocks(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_deadlocks$function$;/*pg_stat_get_db_checksum_failures 1580*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_checksum_failures(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_checksum_failures$function$;/*pg_stat_get_db_checksum_last_failure 1581*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_checksum_last_failure(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_checksum_last_failure$function$;/*pg_stat_get_db_stat_reset_time 1582*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_stat_reset_time(oid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_stat_reset_time$function$;/*pg_stat_get_db_temp_files 1583*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_temp_files(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_temp_files$function$;/*pg_stat_get_db_blk_write_time 1584*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_db_blk_write_time(oid)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_db_blk_write_time$function$;/*pg_stat_get_archiver 1585*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_archiver(OUT archived_count bigint, OUT last_archived_wal text, OUT last_archived_time timestamp with time zone, OUT failed_count bigint, OUT last_failed_wal text, OUT last_failed_time timestamp with time zone, OUT stats_reset timestamp with time zone)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED
AS $function$pg_stat_get_archiver$function$;/*pg_stat_get_bgwriter_timed_checkpoints 1586*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_timed_checkpoints()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_timed_checkpoints$function$;/*pg_stat_get_bgwriter_requested_checkpoints 1587*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_requested_checkpoints()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_requested_checkpoints$function$;/*pg_stat_get_bgwriter_buf_written_checkpoints 1588*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_buf_written_checkpoints()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_buf_written_checkpoints$function$;/*pg_stat_get_bgwriter_buf_written_clean 1589*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_buf_written_clean()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_buf_written_clean$function$;/*pg_stat_get_bgwriter_maxwritten_clean 1590*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_maxwritten_clean()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_maxwritten_clean$function$;/*pg_stat_get_bgwriter_stat_reset_time 1591*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_bgwriter_stat_reset_time()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_bgwriter_stat_reset_time$function$;/*pg_stat_get_checkpoint_write_time 1592*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_checkpoint_write_time()
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_checkpoint_write_time$function$;/*pg_stat_get_checkpoint_sync_time 1593*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_checkpoint_sync_time()
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_checkpoint_sync_time$function$;/*pg_stat_get_buf_written_backend 1594*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_buf_written_backend()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_buf_written_backend$function$;/*pg_stat_get_buf_fsync_backend 1595*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_buf_fsync_backend()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_buf_fsync_backend$function$;/*pg_stat_get_buf_alloc 1596*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_buf_alloc()
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_buf_alloc$function$;/*pg_stat_get_slru 1597*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_slru(OUT name text, OUT blks_zeroed bigint, OUT blks_hit bigint, OUT blks_read bigint, OUT blks_written bigint, OUT blks_exists bigint, OUT flushes bigint, OUT truncates bigint, OUT stats_reset timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED ROWS 100
AS $function$pg_stat_get_slru$function$;/*pg_stat_get_function_calls 1598*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_function_calls(oid)
 RETURNS bigint
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_function_calls$function$;/*pg_stat_get_function_total_time 1599*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_function_total_time(oid)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_function_total_time$function$;/*pg_stat_get_function_self_time 1600*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_function_self_time(oid)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_function_self_time$function$;/*pg_stat_get_xact_numscans 1601*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_numscans(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_numscans$function$;/*pg_stat_get_xact_tuples_returned 1602*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_returned(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_returned$function$;/*pg_stat_get_xact_tuples_fetched 1603*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_fetched$function$;/*pg_stat_get_xact_tuples_inserted 1604*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_inserted(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_inserted$function$;/*pg_stat_get_xact_tuples_updated 1605*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_updated(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_updated$function$;/*pg_stat_get_xact_tuples_deleted 1606*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_deleted(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_deleted$function$;/*pg_stat_get_xact_tuples_hot_updated 1607*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_tuples_hot_updated(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_tuples_hot_updated$function$;/*pg_stat_get_xact_blocks_fetched 1608*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_blocks_fetched(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_blocks_fetched$function$;/*pg_stat_get_xact_blocks_hit 1609*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_blocks_hit(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_blocks_hit$function$;/*pg_stat_get_xact_function_calls 1610*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_function_calls(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_function_calls$function$;/*pg_stat_get_xact_function_total_time 1611*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_function_total_time(oid)
 RETURNS double precision
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_function_total_time$function$;/*pg_stat_get_xact_function_self_time 1612*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_xact_function_self_time(oid)
 RETURNS double precision
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_xact_function_self_time$function$;/*pg_stat_get_snapshot_timestamp 1613*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_get_snapshot_timestamp()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_stat_get_snapshot_timestamp$function$;/*pg_stat_clear_snapshot 1614*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_clear_snapshot()
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED
AS $function$pg_stat_clear_snapshot$function$;/*core_updstru_checkexistcolumn 1615*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistcolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
		sColName ALIAS FOR $2;
		nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema = 'public';

      	if nCnt = 0 then
        	return false;
	    else
    	    return true;
        end if;

    END
$function$;/*getsizeofrelation 1616*/CREATE OR REPLACE FUNCTION public.getsizeofrelation(character varying)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
	relName ALIAS FOR $1;
    result bigint;
begin
	SELECT pg_total_relation_size(relName) INTO result;
    return result;
exception
	WHEN others THEN
		return 0;
END
$function$;/*pg_trigger_depth 1617*/CREATE OR REPLACE FUNCTION pg_catalog.pg_trigger_depth()
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_trigger_depth$function$;/*pg_tablespace_location 1618*/CREATE OR REPLACE FUNCTION pg_catalog.pg_tablespace_location(oid)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_tablespace_location$function$;/*encode 1619*/CREATE OR REPLACE FUNCTION pg_catalog.encode(bytea, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$binary_encode$function$;/*decode 1620*/CREATE OR REPLACE FUNCTION pg_catalog.decode(text, text)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$binary_decode$function$;/*byteaeq 1621*/CREATE OR REPLACE FUNCTION pg_catalog.byteaeq(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteaeq$function$;/*bytealt 1622*/CREATE OR REPLACE FUNCTION pg_catalog.bytealt(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bytealt$function$;/*byteale 1623*/CREATE OR REPLACE FUNCTION pg_catalog.byteale(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteale$function$;/*byteagt 1624*/CREATE OR REPLACE FUNCTION pg_catalog.byteagt(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteagt$function$;/*byteage 1625*/CREATE OR REPLACE FUNCTION pg_catalog.byteage(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteage$function$;/*byteane 1626*/CREATE OR REPLACE FUNCTION pg_catalog.byteane(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteane$function$;/*byteacmp 1627*/CREATE OR REPLACE FUNCTION pg_catalog.byteacmp(bytea, bytea)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$byteacmp$function$;/*bytea_sortsupport 1628*/CREATE OR REPLACE FUNCTION pg_catalog.bytea_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytea_sortsupport$function$;/*timestamp_support 1629*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_support$function$;/*time_support 1630*/CREATE OR REPLACE FUNCTION pg_catalog.time_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_support$function$;/*timestamp 1631*/CREATE OR REPLACE FUNCTION pg_catalog."timestamp"(timestamp without time zone, integer)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT timestamp_support
AS $function$timestamp_scale$function$;/*oidlarger 1632*/CREATE OR REPLACE FUNCTION pg_catalog.oidlarger(oid, oid)
 RETURNS oid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidlarger$function$;/*oidsmaller 1633*/CREATE OR REPLACE FUNCTION pg_catalog.oidsmaller(oid, oid)
 RETURNS oid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidsmaller$function$;/*timestamptz 1634*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz(timestamp with time zone, integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT timestamp_support
AS $function$timestamptz_scale$function$;/*time 1635*/CREATE OR REPLACE FUNCTION pg_catalog."time"(time without time zone, integer)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT time_support
AS $function$time_scale$function$;/*timetz 1636*/CREATE OR REPLACE FUNCTION pg_catalog.timetz(time with time zone, integer)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT time_support
AS $function$timetz_scale$function$;/*textanycat 1637*/CREATE OR REPLACE FUNCTION pg_catalog.textanycat(text, anynonarray)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select $1 || $2::pg_catalog.text$function$;/*anytextcat 1638*/CREATE OR REPLACE FUNCTION pg_catalog.anytextcat(anynonarray, text)
 RETURNS text
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select $1::pg_catalog.text || $2$function$;/*bytealike 1639*/CREATE OR REPLACE FUNCTION pg_catalog.bytealike(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$bytealike$function$;/*byteanlike 1640*/CREATE OR REPLACE FUNCTION pg_catalog.byteanlike(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteanlike$function$;/*like 1641*/CREATE OR REPLACE FUNCTION pg_catalog."like"(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT textlike_support
AS $function$bytealike$function$;/*notlike 1642*/CREATE OR REPLACE FUNCTION pg_catalog.notlike(bytea, bytea)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteanlike$function$;/*like_escape 1643*/CREATE OR REPLACE FUNCTION pg_catalog.like_escape(bytea, bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$like_escape_bytea$function$;/*length 1644*/CREATE OR REPLACE FUNCTION pg_catalog.length(bytea)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteaoctetlen$function$;/*byteacat 1645*/CREATE OR REPLACE FUNCTION pg_catalog.byteacat(bytea, bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteacat$function$;/*substring 1646*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(bytea, integer, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytea_substr$function$;/*substring 1647*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(bytea, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytea_substr_no_len$function$;/*substr 1648*/CREATE OR REPLACE FUNCTION pg_catalog.substr(bytea, integer, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytea_substr$function$;/*substr 1649*/CREATE OR REPLACE FUNCTION pg_catalog.substr(bytea, integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytea_substr_no_len$function$;/*position 1650*/CREATE OR REPLACE FUNCTION pg_catalog."position"(bytea, bytea)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteapos$function$;/*btrim 1651*/CREATE OR REPLACE FUNCTION pg_catalog.btrim(bytea, bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteatrim$function$;/*time 1652*/CREATE OR REPLACE FUNCTION pg_catalog."time"(timestamp with time zone)
 RETURNS time without time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_time$function$;/*date_trunc 1653*/CREATE OR REPLACE FUNCTION pg_catalog.date_trunc(text, timestamp without time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_trunc$function$;/*date_part 1654*/CREATE OR REPLACE FUNCTION pg_catalog.date_part(text, timestamp without time zone)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_part$function$;/*timestamp 1655*/CREATE OR REPLACE FUNCTION pg_catalog."timestamp"(date)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_timestamp$function$;/*timestamp 1656*/CREATE OR REPLACE FUNCTION pg_catalog."timestamp"(date, time without time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$datetime_timestamp$function$;/*timestamp 1657*/CREATE OR REPLACE FUNCTION pg_catalog."timestamp"(timestamp with time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_timestamp$function$;/*timestamptz 1658*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz(timestamp without time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_timestamptz$function$;/*date 1659*/CREATE OR REPLACE FUNCTION pg_catalog.date(timestamp without time zone)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_date$function$;/*timestamp_mi 1660*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_mi(timestamp without time zone, timestamp without time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_mi$function$;/*timestamp_pl_interval 1661*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_pl_interval(timestamp without time zone, interval)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_pl_interval$function$;/*timestamp_mi_interval 1662*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_mi_interval(timestamp without time zone, interval)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_mi_interval$function$;/*timestamp_smaller 1663*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_smaller(timestamp without time zone, timestamp without time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_smaller$function$;/*timestamp_larger 1664*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_larger(timestamp without time zone, timestamp without time zone)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_larger$function$;/*timezone 1665*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(text, time with time zone)
 RETURNS time with time zone
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$timetz_zone$function$;/*timezone 1666*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(interval, time with time zone)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_izone$function$;/*timestamp_hash 1667*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_hash(timestamp without time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_hash$function$;/*timestamp_hash_extended 1668*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_hash_extended(timestamp without time zone, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_hash_extended$function$;/*overlaps 1669*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp without time zone, timestamp without time zone, timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$overlaps_timestamp$function$;/*overlaps 1670*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp without time zone, interval, timestamp without time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, ($3 + $4))$function$;/*overlaps 1671*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp without time zone, timestamp without time zone, timestamp without time zone, interval)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, $2) overlaps ($3, ($3 + $4))$function$;/*overlaps 1672*/CREATE OR REPLACE FUNCTION pg_catalog."overlaps"(timestamp without time zone, interval, timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE COST 1
AS $function$select ($1, ($1 + $2)) overlaps ($3, $4)$function$;/*timestamp_cmp 1673*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_cmp(timestamp without time zone, timestamp without time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_cmp$function$;/*timestamp_sortsupport 1674*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_sortsupport$function$;/*in_range 1675*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(timestamp without time zone, timestamp without time zone, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_timestamp_interval$function$;/*in_range 1676*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(timestamp with time zone, timestamp with time zone, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$in_range_timestamptz_interval$function$;/*in_range 1677*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(interval, interval, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_interval_interval$function$;/*in_range 1678*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(time without time zone, time without time zone, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_time_interval$function$;/*in_range 1679*/CREATE OR REPLACE FUNCTION pg_catalog.in_range(time with time zone, time with time zone, interval, boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$in_range_timetz_interval$function$;/*time 1680*/CREATE OR REPLACE FUNCTION pg_catalog."time"(time with time zone)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_time$function$;/*timetz 1681*/CREATE OR REPLACE FUNCTION pg_catalog.timetz(time without time zone)
 RETURNS time with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$time_timetz$function$;/*isfinite 1682*/CREATE OR REPLACE FUNCTION pg_catalog.isfinite(timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_finite$function$;/*to_char 1683*/CREATE OR REPLACE FUNCTION pg_catalog.to_char(timestamp without time zone, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_to_char$function$;/*timestamp_eq 1684*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_eq(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_eq$function$;/*timestamp_ne 1685*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ne(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_ne$function$;/*timestamp_lt 1686*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_lt(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_lt$function$;/*timestamp_le 1687*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_le(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_le$function$;/*timestamp_ge 1688*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ge(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_ge$function$;/*timestamp_gt 1689*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_gt(timestamp without time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$timestamp_gt$function$;/*age 1690*/CREATE OR REPLACE FUNCTION pg_catalog.age(timestamp without time zone, timestamp without time zone)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_age$function$;/*age 1691*/CREATE OR REPLACE FUNCTION pg_catalog.age(timestamp without time zone)
 RETURNS interval
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.age(cast(current_date as timestamp without time zone), $1)$function$;/*timezone 1692*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(text, timestamp without time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_zone$function$;/*timezone 1693*/CREATE OR REPLACE FUNCTION pg_catalog.timezone(interval, timestamp without time zone)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_izone$function$;/*date_pl_interval 1694*/CREATE OR REPLACE FUNCTION pg_catalog.date_pl_interval(date, interval)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_pl_interval$function$;/*date_mi_interval 1695*/CREATE OR REPLACE FUNCTION pg_catalog.date_mi_interval(date, interval)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_mi_interval$function$;/*substring 1696*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(text, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$textregexsubstr$function$;/*substring 1697*/CREATE OR REPLACE FUNCTION pg_catalog."substring"(text, text, text)
 RETURNS text
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.substring($1, pg_catalog.similar_to_escape($2, $3))$function$;/*bit 1698*/CREATE OR REPLACE FUNCTION pg_catalog."bit"(bigint, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bitfromint8$function$;/*int8 1699*/CREATE OR REPLACE FUNCTION pg_catalog.int8(bit)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bittoint8$function$;/*current_setting 1700*/CREATE OR REPLACE FUNCTION pg_catalog.current_setting(text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$show_config_by_name$function$;/*current_setting 1701*/CREATE OR REPLACE FUNCTION pg_catalog.current_setting(text, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$show_config_by_name_missing_ok$function$;/*set_config 1702*/CREATE OR REPLACE FUNCTION pg_catalog.set_config(text, text, boolean)
 RETURNS text
 LANGUAGE internal
AS $function$set_config_by_name$function$;/*pg_show_all_settings 1703*/CREATE OR REPLACE FUNCTION pg_catalog.pg_show_all_settings(OUT name text, OUT setting text, OUT unit text, OUT category text, OUT short_desc text, OUT extra_desc text, OUT context text, OUT vartype text, OUT source text, OUT min_val text, OUT max_val text, OUT enumvals text[], OUT boot_val text, OUT reset_val text, OUT sourcefile text, OUT sourceline integer, OUT pending_restart boolean)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$show_all_settings$function$;/*core_updstru_checkexistconstraint 1704*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistconstraint(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM information_schema.constraint_table_usage c
          --TABLE pg_catalog.pg_constraint --LEFT OUTER JOIN pg_catalog.pg_namespace AS n ON n.oid = c.relnamespace
          WHERE lower(c.constraint_name) = lower(sName);

          if nCnt = 0 then
              return false;
          else
              return true;
          end if;

    END $function$;/*pg_lock_status 1705*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lock_status(OUT locktype text, OUT database oid, OUT relation oid, OUT page integer, OUT tuple smallint, OUT virtualxid text, OUT transactionid xid, OUT classid oid, OUT objid oid, OUT objsubid smallint, OUT virtualtransaction text, OUT pid integer, OUT mode text, OUT granted boolean, OUT fastpath boolean)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_lock_status$function$;/*pg_blocking_pids 1706*/CREATE OR REPLACE FUNCTION pg_catalog.pg_blocking_pids(integer)
 RETURNS integer[]
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_blocking_pids$function$;/*pg_safe_snapshot_blocking_pids 1707*/CREATE OR REPLACE FUNCTION pg_catalog.pg_safe_snapshot_blocking_pids(integer)
 RETURNS integer[]
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_safe_snapshot_blocking_pids$function$;/*pg_isolation_test_session_is_blocked 1708*/CREATE OR REPLACE FUNCTION pg_catalog.pg_isolation_test_session_is_blocked(integer, integer[])
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_isolation_test_session_is_blocked$function$;/*pg_prepared_xact 1709*/CREATE OR REPLACE FUNCTION pg_catalog.pg_prepared_xact(OUT transaction xid, OUT gid text, OUT prepared timestamp with time zone, OUT ownerid oid, OUT dbid oid)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_prepared_xact$function$;/*pg_get_multixact_members 1710*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_multixact_members(multixid xid, OUT xid xid, OUT mode text)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_get_multixact_members$function$;/*pg_xact_commit_timestamp 1711*/CREATE OR REPLACE FUNCTION pg_catalog.pg_xact_commit_timestamp(xid)
 RETURNS timestamp with time zone
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_xact_commit_timestamp$function$;/*pg_last_committed_xact 1712*/CREATE OR REPLACE FUNCTION pg_catalog.pg_last_committed_xact(OUT xid xid, OUT "timestamp" timestamp with time zone)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_last_committed_xact$function$;/*pg_describe_object 1713*/CREATE OR REPLACE FUNCTION pg_catalog.pg_describe_object(oid, oid, integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_describe_object$function$;/*pg_identify_object 1714*/CREATE OR REPLACE FUNCTION pg_catalog.pg_identify_object(classid oid, objid oid, objsubid integer, OUT type text, OUT schema text, OUT name text, OUT identity text)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_identify_object$function$;/*pg_identify_object_as_address 1715*/CREATE OR REPLACE FUNCTION pg_catalog.pg_identify_object_as_address(classid oid, objid oid, objsubid integer, OUT type text, OUT object_names text[], OUT object_args text[])
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_identify_object_as_address$function$;/*pg_get_object_address 1716*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_object_address(type text, object_names text[], object_args text[], OUT classid oid, OUT objid oid, OUT objsubid integer)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_object_address$function$;/*pg_table_is_visible 1717*/CREATE OR REPLACE FUNCTION pg_catalog.pg_table_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_table_is_visible$function$;/*pg_type_is_visible 1718*/CREATE OR REPLACE FUNCTION pg_catalog.pg_type_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_type_is_visible$function$;/*pg_function_is_visible 1719*/CREATE OR REPLACE FUNCTION pg_catalog.pg_function_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_function_is_visible$function$;/*pg_operator_is_visible 1720*/CREATE OR REPLACE FUNCTION pg_catalog.pg_operator_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_operator_is_visible$function$;/*pg_opclass_is_visible 1721*/CREATE OR REPLACE FUNCTION pg_catalog.pg_opclass_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_opclass_is_visible$function$;/*pg_opfamily_is_visible 1722*/CREATE OR REPLACE FUNCTION pg_catalog.pg_opfamily_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_opfamily_is_visible$function$;/*pg_conversion_is_visible 1723*/CREATE OR REPLACE FUNCTION pg_catalog.pg_conversion_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_conversion_is_visible$function$;/*pg_statistics_obj_is_visible 1724*/CREATE OR REPLACE FUNCTION pg_catalog.pg_statistics_obj_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_statistics_obj_is_visible$function$;/*pg_ts_parser_is_visible 1725*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ts_parser_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_ts_parser_is_visible$function$;/*pg_ts_dict_is_visible 1726*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ts_dict_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_ts_dict_is_visible$function$;/*pg_ts_template_is_visible 1727*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ts_template_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_ts_template_is_visible$function$;/*pg_ts_config_is_visible 1728*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ts_config_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_ts_config_is_visible$function$;/*pg_collation_is_visible 1729*/CREATE OR REPLACE FUNCTION pg_catalog.pg_collation_is_visible(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10
AS $function$pg_collation_is_visible$function$;/*pg_my_temp_schema 1730*/CREATE OR REPLACE FUNCTION pg_catalog.pg_my_temp_schema()
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_my_temp_schema$function$;/*pg_is_other_temp_schema 1731*/CREATE OR REPLACE FUNCTION pg_catalog.pg_is_other_temp_schema(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_is_other_temp_schema$function$;/*pg_cancel_backend 1732*/CREATE OR REPLACE FUNCTION pg_catalog.pg_cancel_backend(integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_cancel_backend$function$;/*pg_terminate_backend 1733*/CREATE OR REPLACE FUNCTION pg_catalog.pg_terminate_backend(integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_terminate_backend$function$;/*core_updstru_checkexistfunction 1734*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistfunction(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sFuncName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_proc as p
        --TABLE pg_catalog.pg_proc
        WHERE p.proname = lower(sFuncName);

        if nCnt = 0 then
        	return false;
        else
        	return true;
        end if;

    END $function$;/*core_updstru_checkexisttrigger 1735*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttrigger(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
    	sTriggerName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.triggers as t
        --TABLE information_schema.triggers
        WHERE lower(t.trigger_name) = lower(sTriggerName) AND t.trigger_schema NOT IN ('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    END $function$;/*pg_is_in_backup 1736*/CREATE OR REPLACE FUNCTION pg_catalog.pg_is_in_backup()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_is_in_backup$function$;/*pg_backup_start_time 1737*/CREATE OR REPLACE FUNCTION pg_catalog.pg_backup_start_time()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_backup_start_time$function$;/*core_updstru_checkexisttype 1738*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttype(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
    	sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_type t
        --TABLE pg_catalog.pg_type
        WHERE t.typname = lower(sName);

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    END $function$;/*core_updstru_checkexistview 1739*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistview(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
	    sName ALIAS FOR $1;
        nCnt numeric;
    begin
	    SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.views as v
        --TABLE information_schema.views
        WHERE lower(v.table_name) = lower(sName) AND v.table_schema = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    END
$function$;/*pg_current_wal_lsn 1740*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_wal_lsn()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_current_wal_lsn$function$;/*pg_current_wal_insert_lsn 1741*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_wal_insert_lsn()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_current_wal_insert_lsn$function$;/*pg_current_wal_flush_lsn 1742*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_wal_flush_lsn()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_current_wal_flush_lsn$function$;/*pg_walfile_name_offset 1743*/CREATE OR REPLACE FUNCTION pg_catalog.pg_walfile_name_offset(lsn pg_lsn, OUT file_name text, OUT file_offset integer)
 RETURNS record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_walfile_name_offset$function$;/*pg_walfile_name 1744*/CREATE OR REPLACE FUNCTION pg_catalog.pg_walfile_name(pg_lsn)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_walfile_name$function$;/*pg_wal_lsn_diff 1745*/CREATE OR REPLACE FUNCTION pg_catalog.pg_wal_lsn_diff(pg_lsn, pg_lsn)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_wal_lsn_diff$function$;/*pg_export_snapshot 1746*/CREATE OR REPLACE FUNCTION pg_catalog.pg_export_snapshot()
 RETURNS text
 LANGUAGE internal
 STRICT
AS $function$pg_export_snapshot$function$;/*pg_is_in_recovery 1747*/CREATE OR REPLACE FUNCTION pg_catalog.pg_is_in_recovery()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_is_in_recovery$function$;/*pg_last_wal_receive_lsn 1748*/CREATE OR REPLACE FUNCTION pg_catalog.pg_last_wal_receive_lsn()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_last_wal_receive_lsn$function$;/*pg_last_wal_replay_lsn 1749*/CREATE OR REPLACE FUNCTION pg_catalog.pg_last_wal_replay_lsn()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_last_wal_replay_lsn$function$;/*pg_last_xact_replay_timestamp 1750*/CREATE OR REPLACE FUNCTION pg_catalog.pg_last_xact_replay_timestamp()
 RETURNS timestamp with time zone
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_last_xact_replay_timestamp$function$;/*pg_is_wal_replay_paused 1751*/CREATE OR REPLACE FUNCTION pg_catalog.pg_is_wal_replay_paused()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_is_wal_replay_paused$function$;/*pg_rotate_logfile_old 1752*/CREATE OR REPLACE FUNCTION pg_catalog.pg_rotate_logfile_old()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_rotate_logfile$function$;/*pg_stat_file 1753*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_file(filename text, OUT size bigint, OUT access timestamp with time zone, OUT modification timestamp with time zone, OUT change timestamp with time zone, OUT creation timestamp with time zone, OUT isdir boolean)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_stat_file_1arg$function$;/*pg_stop_backup 1754*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stop_backup()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stop_backup$function$;/*pg_stat_file 1755*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_file(filename text, missing_ok boolean, OUT size bigint, OUT access timestamp with time zone, OUT modification timestamp with time zone, OUT change timestamp with time zone, OUT creation timestamp with time zone, OUT isdir boolean)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_stat_file$function$;/*pg_read_file_old 1756*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_file_old(text, bigint, bigint)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_file$function$;/*pg_sleep 1757*/CREATE OR REPLACE FUNCTION pg_catalog.pg_sleep(double precision)
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_sleep$function$;/*pg_sleep_for 1758*/CREATE OR REPLACE FUNCTION pg_catalog.pg_sleep_for(interval)
 RETURNS void
 LANGUAGE sql
 PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.pg_sleep(extract(epoch from pg_catalog.clock_timestamp() operator(pg_catalog.+) $1) operator(pg_catalog.-) extract(epoch from pg_catalog.clock_timestamp()))$function$;/*pg_sleep_until 1759*/CREATE OR REPLACE FUNCTION pg_catalog.pg_sleep_until(timestamp with time zone)
 RETURNS void
 LANGUAGE sql
 PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.pg_sleep(extract(epoch from $1) operator(pg_catalog.-) extract(epoch from pg_catalog.clock_timestamp()))$function$;/*pg_jit_available 1760*/CREATE OR REPLACE FUNCTION pg_catalog.pg_jit_available()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_jit_available$function$;/*text 1761*/CREATE OR REPLACE FUNCTION pg_catalog.text(boolean)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$booltext$function$;/*pg_read_binary_file 1762*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_binary_file(text)
 RETURNS bytea
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_binary_file_all$function$;/*pg_read_binary_file 1763*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_binary_file(text, bigint, bigint)
 RETURNS bytea
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_binary_file_off_len$function$;/*pg_ls_dir 1764*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_dir(text)
 RETURNS SETOF text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_ls_dir_1arg$function$;/*pg_ls_dir 1765*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_dir(text, boolean, boolean)
 RETURNS SETOF text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_ls_dir$function$;/*text_pattern_lt 1766*/CREATE OR REPLACE FUNCTION pg_catalog.text_pattern_lt(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_pattern_lt$function$;/*text_pattern_le 1767*/CREATE OR REPLACE FUNCTION pg_catalog.text_pattern_le(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_pattern_le$function$;/*text_pattern_ge 1768*/CREATE OR REPLACE FUNCTION pg_catalog.text_pattern_ge(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_pattern_ge$function$;/*text_pattern_gt 1769*/CREATE OR REPLACE FUNCTION pg_catalog.text_pattern_gt(text, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$text_pattern_gt$function$;/*bttext_pattern_cmp 1770*/CREATE OR REPLACE FUNCTION pg_catalog.bttext_pattern_cmp(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bttext_pattern_cmp$function$;/*bttext_pattern_sortsupport 1771*/CREATE OR REPLACE FUNCTION pg_catalog.bttext_pattern_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bttext_pattern_sortsupport$function$;/*bpchar_pattern_lt 1772*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_pattern_lt(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_pattern_lt$function$;/*bpchar_pattern_le 1773*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_pattern_le(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_pattern_le$function$;/*bpchar_pattern_ge 1774*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_pattern_ge(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_pattern_ge$function$;/*bpchar_pattern_gt 1775*/CREATE OR REPLACE FUNCTION pg_catalog.bpchar_pattern_gt(character, character)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$bpchar_pattern_gt$function$;/*btbpchar_pattern_cmp 1776*/CREATE OR REPLACE FUNCTION pg_catalog.btbpchar_pattern_cmp(character, character)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btbpchar_pattern_cmp$function$;/*btbpchar_pattern_sortsupport 1777*/CREATE OR REPLACE FUNCTION pg_catalog.btbpchar_pattern_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btbpchar_pattern_sortsupport$function$;/*btint48cmp 1778*/CREATE OR REPLACE FUNCTION pg_catalog.btint48cmp(integer, bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint48cmp$function$;/*btint84cmp 1779*/CREATE OR REPLACE FUNCTION pg_catalog.btint84cmp(bigint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint84cmp$function$;/*btint24cmp 1780*/CREATE OR REPLACE FUNCTION pg_catalog.btint24cmp(smallint, integer)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint24cmp$function$;/*btint42cmp 1781*/CREATE OR REPLACE FUNCTION pg_catalog.btint42cmp(integer, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint42cmp$function$;/*btint28cmp 1782*/CREATE OR REPLACE FUNCTION pg_catalog.btint28cmp(smallint, bigint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint28cmp$function$;/*btint82cmp 1783*/CREATE OR REPLACE FUNCTION pg_catalog.btint82cmp(bigint, smallint)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btint82cmp$function$;/*btfloat48cmp 1784*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat48cmp(real, double precision)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btfloat48cmp$function$;/*btfloat84cmp 1785*/CREATE OR REPLACE FUNCTION pg_catalog.btfloat84cmp(double precision, real)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$btfloat84cmp$function$;/*regprocedurein 1786*/CREATE OR REPLACE FUNCTION pg_catalog.regprocedurein(cstring)
 RETURNS regprocedure
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regprocedurein$function$;/*regprocedureout 1787*/CREATE OR REPLACE FUNCTION pg_catalog.regprocedureout(regprocedure)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regprocedureout$function$;/*regoperin 1788*/CREATE OR REPLACE FUNCTION pg_catalog.regoperin(cstring)
 RETURNS regoper
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regoperin$function$;/*regoperout 1789*/CREATE OR REPLACE FUNCTION pg_catalog.regoperout(regoper)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regoperout$function$;/*to_regoper 1790*/CREATE OR REPLACE FUNCTION pg_catalog.to_regoper(text)
 RETURNS regoper
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regoper$function$;/*to_regoperator 1791*/CREATE OR REPLACE FUNCTION pg_catalog.to_regoperator(text)
 RETURNS regoperator
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regoperator$function$;/*regoperatorin 1792*/CREATE OR REPLACE FUNCTION pg_catalog.regoperatorin(cstring)
 RETURNS regoperator
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regoperatorin$function$;/*regoperatorout 1793*/CREATE OR REPLACE FUNCTION pg_catalog.regoperatorout(regoperator)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regoperatorout$function$;/*regclassin 1794*/CREATE OR REPLACE FUNCTION pg_catalog.regclassin(cstring)
 RETURNS regclass
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regclassin$function$;/*regclassout 1795*/CREATE OR REPLACE FUNCTION pg_catalog.regclassout(regclass)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regclassout$function$;/*to_regclass 1796*/CREATE OR REPLACE FUNCTION pg_catalog.to_regclass(text)
 RETURNS regclass
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regclass$function$;/*regcollationin 1797*/CREATE OR REPLACE FUNCTION pg_catalog.regcollationin(cstring)
 RETURNS regcollation
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regcollationin$function$;/*regcollationout 1798*/CREATE OR REPLACE FUNCTION pg_catalog.regcollationout(regcollation)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regcollationout$function$;/*to_regcollation 1799*/CREATE OR REPLACE FUNCTION pg_catalog.to_regcollation(text)
 RETURNS regcollation
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regcollation$function$;/*regtypein 1800*/CREATE OR REPLACE FUNCTION pg_catalog.regtypein(cstring)
 RETURNS regtype
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regtypein$function$;/*regtypeout 1801*/CREATE OR REPLACE FUNCTION pg_catalog.regtypeout(regtype)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regtypeout$function$;/*to_regtype 1802*/CREATE OR REPLACE FUNCTION pg_catalog.to_regtype(text)
 RETURNS regtype
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regtype$function$;/*regclass 1803*/CREATE OR REPLACE FUNCTION pg_catalog.regclass(text)
 RETURNS regclass
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$text_regclass$function$;/*regrolein 1804*/CREATE OR REPLACE FUNCTION pg_catalog.regrolein(cstring)
 RETURNS regrole
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regrolein$function$;/*regroleout 1805*/CREATE OR REPLACE FUNCTION pg_catalog.regroleout(regrole)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regroleout$function$;/*to_regrole 1806*/CREATE OR REPLACE FUNCTION pg_catalog.to_regrole(text)
 RETURNS regrole
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regrole$function$;/*regnamespacein 1807*/CREATE OR REPLACE FUNCTION pg_catalog.regnamespacein(cstring)
 RETURNS regnamespace
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regnamespacein$function$;/*regnamespaceout 1808*/CREATE OR REPLACE FUNCTION pg_catalog.regnamespaceout(regnamespace)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regnamespaceout$function$;/*to_regnamespace 1809*/CREATE OR REPLACE FUNCTION pg_catalog.to_regnamespace(text)
 RETURNS regnamespace
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_regnamespace$function$;/*set_limit 1810*/CREATE OR REPLACE FUNCTION public.set_limit(real)
 RETURNS real
 LANGUAGE c
 STRICT
AS '$libdir/pg_trgm', $function$set_limit$function$;/*fmgr_internal_validator 1811*/CREATE OR REPLACE FUNCTION pg_catalog.fmgr_internal_validator(oid)
 RETURNS void
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$fmgr_internal_validator$function$;/*fmgr_c_validator 1812*/CREATE OR REPLACE FUNCTION pg_catalog.fmgr_c_validator(oid)
 RETURNS void
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$fmgr_c_validator$function$;/*fmgr_sql_validator 1813*/CREATE OR REPLACE FUNCTION pg_catalog.fmgr_sql_validator(oid)
 RETURNS void
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$fmgr_sql_validator$function$;/*has_database_privilege 1814*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_name_name$function$;/*has_database_privilege 1815*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_name_id$function$;/*has_database_privilege 1816*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_id_name$function$;/*has_database_privilege 1817*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_id_id$function$;/*has_database_privilege 1818*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_name$function$;/*has_database_privilege 1819*/CREATE OR REPLACE FUNCTION pg_catalog.has_database_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_database_privilege_id$function$;/*has_function_privilege 1820*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_name_name$function$;/*has_function_privilege 1821*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_name_id$function$;/*has_function_privilege 1822*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_id_name$function$;/*has_function_privilege 1823*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_id_id$function$;/*has_function_privilege 1824*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_name$function$;/*has_function_privilege 1825*/CREATE OR REPLACE FUNCTION pg_catalog.has_function_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_function_privilege_id$function$;/*has_language_privilege 1826*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_name_name$function$;/*has_language_privilege 1827*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_name_id$function$;/*has_language_privilege 1828*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_id_name$function$;/*has_language_privilege 1829*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_id_id$function$;/*has_language_privilege 1830*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_name$function$;/*has_language_privilege 1831*/CREATE OR REPLACE FUNCTION pg_catalog.has_language_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_language_privilege_id$function$;/*has_schema_privilege 1832*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_name_name$function$;/*has_schema_privilege 1833*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_name_id$function$;/*has_schema_privilege 1834*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_id_name$function$;/*has_schema_privilege 1835*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_id_id$function$;/*has_schema_privilege 1836*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_name$function$;/*has_schema_privilege 1837*/CREATE OR REPLACE FUNCTION pg_catalog.has_schema_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_schema_privilege_id$function$;/*has_tablespace_privilege 1838*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_name_name$function$;/*has_tablespace_privilege 1839*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_name_id$function$;/*has_tablespace_privilege 1840*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_id_name$function$;/*has_tablespace_privilege 1841*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_id_id$function$;/*has_tablespace_privilege 1842*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_name$function$;/*has_tablespace_privilege 1843*/CREATE OR REPLACE FUNCTION pg_catalog.has_tablespace_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_tablespace_privilege_id$function$;/*has_foreign_data_wrapper_privilege 1844*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_name_name$function$;/*has_foreign_data_wrapper_privilege 1845*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_name_id$function$;/*has_foreign_data_wrapper_privilege 1846*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_id_name$function$;/*has_foreign_data_wrapper_privilege 1847*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_id_id$function$;/*has_foreign_data_wrapper_privilege 1848*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_name$function$;/*has_foreign_data_wrapper_privilege 1849*/CREATE OR REPLACE FUNCTION pg_catalog.has_foreign_data_wrapper_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_foreign_data_wrapper_privilege_id$function$;/*has_server_privilege 1850*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_name_name$function$;/*has_server_privilege 1851*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_name_id$function$;/*has_server_privilege 1852*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_id_name$function$;/*has_server_privilege 1853*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_id_id$function$;/*has_server_privilege 1854*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_name$function$;/*has_server_privilege 1855*/CREATE OR REPLACE FUNCTION pg_catalog.has_server_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_server_privilege_id$function$;/*has_type_privilege 1856*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(name, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_name_name$function$;/*has_type_privilege 1857*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_name_id$function$;/*has_type_privilege 1858*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(oid, text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_id_name$function$;/*has_type_privilege 1859*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_id_id$function$;/*has_type_privilege 1860*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_name$function$;/*has_type_privilege 1861*/CREATE OR REPLACE FUNCTION pg_catalog.has_type_privilege(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$has_type_privilege_id$function$;/*pg_has_role 1862*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(name, name, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_name_name$function$;/*pg_has_role 1863*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(name, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_name_id$function$;/*pg_has_role 1864*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(oid, name, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_id_name$function$;/*pg_has_role 1865*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(oid, oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_id_id$function$;/*pg_has_role 1866*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(name, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_name$function$;/*pg_has_role 1867*/CREATE OR REPLACE FUNCTION pg_catalog.pg_has_role(oid, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_has_role_id$function$;/*pg_column_size 1868*/CREATE OR REPLACE FUNCTION pg_catalog.pg_column_size("any")
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_column_size$function$;/*pg_tablespace_size 1869*/CREATE OR REPLACE FUNCTION pg_catalog.pg_tablespace_size(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_tablespace_size_oid$function$;/*pg_tablespace_size 1870*/CREATE OR REPLACE FUNCTION pg_catalog.pg_tablespace_size(name)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_tablespace_size_name$function$;/*pg_database_size 1871*/CREATE OR REPLACE FUNCTION pg_catalog.pg_database_size(oid)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_database_size_oid$function$;/*pg_database_size 1872*/CREATE OR REPLACE FUNCTION pg_catalog.pg_database_size(name)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_database_size_name$function$;/*pg_relation_size 1873*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_size(regclass)
 RETURNS bigint
 LANGUAGE sql
 PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.pg_relation_size($1, 'main')$function$;/*pg_relation_size 1874*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_size(regclass, text)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_relation_size$function$;/*pg_total_relation_size 1875*/CREATE OR REPLACE FUNCTION pg_catalog.pg_total_relation_size(regclass)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_total_relation_size$function$;/*pg_size_pretty 1876*/CREATE OR REPLACE FUNCTION pg_catalog.pg_size_pretty(bigint)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_size_pretty$function$;/*pg_size_pretty 1877*/CREATE OR REPLACE FUNCTION pg_catalog.pg_size_pretty(numeric)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_size_pretty_numeric$function$;/*pg_size_bytes 1878*/CREATE OR REPLACE FUNCTION pg_catalog.pg_size_bytes(text)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_size_bytes$function$;/*pg_table_size 1879*/CREATE OR REPLACE FUNCTION pg_catalog.pg_table_size(regclass)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_table_size$function$;/*pg_indexes_size 1880*/CREATE OR REPLACE FUNCTION pg_catalog.pg_indexes_size(regclass)
 RETURNS bigint
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_indexes_size$function$;/*pg_relation_filenode 1881*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_filenode(regclass)
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_relation_filenode$function$;/*pg_filenode_relation 1882*/CREATE OR REPLACE FUNCTION pg_catalog.pg_filenode_relation(oid, oid)
 RETURNS regclass
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_filenode_relation$function$;/*pg_relation_filepath 1883*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_filepath(regclass)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_relation_filepath$function$;/*postgresql_fdw_validator 1884*/CREATE OR REPLACE FUNCTION pg_catalog.postgresql_fdw_validator(text[], oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$postgresql_fdw_validator$function$;/*record_in 1885*/CREATE OR REPLACE FUNCTION pg_catalog.record_in(cstring, oid, integer)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$record_in$function$;/*record_out 1886*/CREATE OR REPLACE FUNCTION pg_catalog.record_out(record)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$record_out$function$;/*cstring_in 1887*/CREATE OR REPLACE FUNCTION pg_catalog.cstring_in(cstring)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cstring_in$function$;/*cstring_out 1888*/CREATE OR REPLACE FUNCTION pg_catalog.cstring_out(cstring)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cstring_out$function$;/*any_in 1889*/CREATE OR REPLACE FUNCTION pg_catalog.any_in(cstring)
 RETURNS "any"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$any_in$function$;/*any_out 1890*/CREATE OR REPLACE FUNCTION pg_catalog.any_out("any")
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$any_out$function$;/*anyarray_in 1891*/CREATE OR REPLACE FUNCTION pg_catalog.anyarray_in(cstring)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anyarray_in$function$;/*anyarray_out 1892*/CREATE OR REPLACE FUNCTION pg_catalog.anyarray_out(anyarray)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyarray_out$function$;/*void_in 1893*/CREATE OR REPLACE FUNCTION pg_catalog.void_in(cstring)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$void_in$function$;/*void_out 1894*/CREATE OR REPLACE FUNCTION pg_catalog.void_out(void)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$void_out$function$;/*trigger_in 1895*/CREATE OR REPLACE FUNCTION pg_catalog.trigger_in(cstring)
 RETURNS trigger
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$trigger_in$function$;/*trigger_out 1896*/CREATE OR REPLACE FUNCTION pg_catalog.trigger_out(trigger)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$trigger_out$function$;/*event_trigger_in 1897*/CREATE OR REPLACE FUNCTION pg_catalog.event_trigger_in(cstring)
 RETURNS event_trigger
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$event_trigger_in$function$;/*event_trigger_out 1898*/CREATE OR REPLACE FUNCTION pg_catalog.event_trigger_out(event_trigger)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$event_trigger_out$function$;/*language_handler_in 1899*/CREATE OR REPLACE FUNCTION pg_catalog.language_handler_in(cstring)
 RETURNS language_handler
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$language_handler_in$function$;/*language_handler_out 1900*/CREATE OR REPLACE FUNCTION pg_catalog.language_handler_out(language_handler)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$language_handler_out$function$;/*internal_in 1901*/CREATE OR REPLACE FUNCTION pg_catalog.internal_in(cstring)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$internal_in$function$;/*internal_out 1902*/CREATE OR REPLACE FUNCTION pg_catalog.internal_out(internal)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$internal_out$function$;/*anyelement_in 1903*/CREATE OR REPLACE FUNCTION pg_catalog.anyelement_in(cstring)
 RETURNS anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anyelement_in$function$;/*anyelement_out 1904*/CREATE OR REPLACE FUNCTION pg_catalog.anyelement_out(anyelement)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anyelement_out$function$;/*shell_in 1905*/CREATE OR REPLACE FUNCTION pg_catalog.shell_in(cstring)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$shell_in$function$;/*shell_out 1906*/CREATE OR REPLACE FUNCTION pg_catalog.shell_out(void)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$shell_out$function$;/*domain_in 1907*/CREATE OR REPLACE FUNCTION pg_catalog.domain_in(cstring, oid, integer)
 RETURNS "any"
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$domain_in$function$;/*domain_recv 1908*/CREATE OR REPLACE FUNCTION pg_catalog.domain_recv(internal, oid, integer)
 RETURNS "any"
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$domain_recv$function$;/*anynonarray_in 1909*/CREATE OR REPLACE FUNCTION pg_catalog.anynonarray_in(cstring)
 RETURNS anynonarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anynonarray_in$function$;/*anynonarray_out 1910*/CREATE OR REPLACE FUNCTION pg_catalog.anynonarray_out(anynonarray)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anynonarray_out$function$;/*fdw_handler_in 1911*/CREATE OR REPLACE FUNCTION pg_catalog.fdw_handler_in(cstring)
 RETURNS fdw_handler
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$fdw_handler_in$function$;/*fdw_handler_out 1912*/CREATE OR REPLACE FUNCTION pg_catalog.fdw_handler_out(fdw_handler)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$fdw_handler_out$function$;/*index_am_handler_in 1913*/CREATE OR REPLACE FUNCTION pg_catalog.index_am_handler_in(cstring)
 RETURNS index_am_handler
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$index_am_handler_in$function$;/*index_am_handler_out 1914*/CREATE OR REPLACE FUNCTION pg_catalog.index_am_handler_out(index_am_handler)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$index_am_handler_out$function$;/*tsm_handler_in 1915*/CREATE OR REPLACE FUNCTION pg_catalog.tsm_handler_in(cstring)
 RETURNS tsm_handler
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$tsm_handler_in$function$;/*tsm_handler_out 1916*/CREATE OR REPLACE FUNCTION pg_catalog.tsm_handler_out(tsm_handler)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsm_handler_out$function$;/*table_am_handler_in 1917*/CREATE OR REPLACE FUNCTION pg_catalog.table_am_handler_in(cstring)
 RETURNS table_am_handler
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$table_am_handler_in$function$;/*table_am_handler_out 1918*/CREATE OR REPLACE FUNCTION pg_catalog.table_am_handler_out(table_am_handler)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$table_am_handler_out$function$;/*anycompatible_in 1919*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatible_in(cstring)
 RETURNS anycompatible
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anycompatible_in$function$;/*anycompatible_out 1920*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatible_out(anycompatible)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anycompatible_out$function$;/*anycompatiblearray_in 1921*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblearray_in(cstring)
 RETURNS anycompatiblearray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anycompatiblearray_in$function$;/*anycompatiblearray_out 1922*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblearray_out(anycompatiblearray)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anycompatiblearray_out$function$;/*anycompatiblearray_recv 1923*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblearray_recv(internal)
 RETURNS anycompatiblearray
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anycompatiblearray_recv$function$;/*anycompatiblearray_send 1924*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblearray_send(anycompatiblearray)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anycompatiblearray_send$function$;/*anycompatiblenonarray_in 1925*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblenonarray_in(cstring)
 RETURNS anycompatiblenonarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anycompatiblenonarray_in$function$;/*anycompatiblenonarray_out 1926*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblenonarray_out(anycompatiblenonarray)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anycompatiblenonarray_out$function$;/*anycompatiblerange_in 1927*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblerange_in(cstring, oid, integer)
 RETURNS anycompatiblerange
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anycompatiblerange_in$function$;/*anycompatiblerange_out 1928*/CREATE OR REPLACE FUNCTION pg_catalog.anycompatiblerange_out(anycompatiblerange)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anycompatiblerange_out$function$;/*bernoulli 1929*/CREATE OR REPLACE FUNCTION pg_catalog.bernoulli(internal)
 RETURNS tsm_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$tsm_bernoulli_handler$function$;/*system 1930*/CREATE OR REPLACE FUNCTION pg_catalog.system(internal)
 RETURNS tsm_handler
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$tsm_system_handler$function$;/*md5 1931*/CREATE OR REPLACE FUNCTION pg_catalog.md5(text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$md5_text$function$;/*md5 1932*/CREATE OR REPLACE FUNCTION pg_catalog.md5(bytea)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$md5_bytea$function$;/*sha224 1933*/CREATE OR REPLACE FUNCTION pg_catalog.sha224(bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$sha224_bytea$function$;/*sha256 1934*/CREATE OR REPLACE FUNCTION pg_catalog.sha256(bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$sha256_bytea$function$;/*sha384 1935*/CREATE OR REPLACE FUNCTION pg_catalog.sha384(bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$sha384_bytea$function$;/*sha512 1936*/CREATE OR REPLACE FUNCTION pg_catalog.sha512(bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$sha512_bytea$function$;/*date_lt_timestamp 1937*/CREATE OR REPLACE FUNCTION pg_catalog.date_lt_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_lt_timestamp$function$;/*date_le_timestamp 1938*/CREATE OR REPLACE FUNCTION pg_catalog.date_le_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_le_timestamp$function$;/*date_eq_timestamp 1939*/CREATE OR REPLACE FUNCTION pg_catalog.date_eq_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_eq_timestamp$function$;/*date_gt_timestamp 1940*/CREATE OR REPLACE FUNCTION pg_catalog.date_gt_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_gt_timestamp$function$;/*date_ge_timestamp 1941*/CREATE OR REPLACE FUNCTION pg_catalog.date_ge_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_ge_timestamp$function$;/*date_ne_timestamp 1942*/CREATE OR REPLACE FUNCTION pg_catalog.date_ne_timestamp(date, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_ne_timestamp$function$;/*date_cmp_timestamp 1943*/CREATE OR REPLACE FUNCTION pg_catalog.date_cmp_timestamp(date, timestamp without time zone)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_cmp_timestamp$function$;/*date_lt_timestamptz 1944*/CREATE OR REPLACE FUNCTION pg_catalog.date_lt_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_lt_timestamptz$function$;/*date_le_timestamptz 1945*/CREATE OR REPLACE FUNCTION pg_catalog.date_le_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_le_timestamptz$function$;/*date_eq_timestamptz 1946*/CREATE OR REPLACE FUNCTION pg_catalog.date_eq_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_eq_timestamptz$function$;/*date_gt_timestamptz 1947*/CREATE OR REPLACE FUNCTION pg_catalog.date_gt_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_gt_timestamptz$function$;/*date_ge_timestamptz 1948*/CREATE OR REPLACE FUNCTION pg_catalog.date_ge_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_ge_timestamptz$function$;/*date_ne_timestamptz 1949*/CREATE OR REPLACE FUNCTION pg_catalog.date_ne_timestamptz(date, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_ne_timestamptz$function$;/*date_cmp_timestamptz 1950*/CREATE OR REPLACE FUNCTION pg_catalog.date_cmp_timestamptz(date, timestamp with time zone)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$date_cmp_timestamptz$function$;/*timestamp_lt_date 1951*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_lt_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_lt_date$function$;/*timestamp_le_date 1952*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_le_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_le_date$function$;/*timestamp_eq_date 1953*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_eq_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_eq_date$function$;/*timestamp_gt_date 1954*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_gt_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_gt_date$function$;/*timestamp_ge_date 1955*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ge_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_ge_date$function$;/*timestamp_ne_date 1956*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ne_date(timestamp without time zone, date)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_ne_date$function$;/*timestamp_cmp_date 1957*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_cmp_date(timestamp without time zone, date)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_cmp_date$function$;/*timestamptz_lt_date 1958*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_lt_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_lt_date$function$;/*timestamptz_le_date 1959*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_le_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_le_date$function$;/*timestamptz_eq_date 1960*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_eq_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_eq_date$function$;/*timestamptz_gt_date 1961*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_gt_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_gt_date$function$;/*timestamptz_ge_date 1962*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ge_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_ge_date$function$;/*timestamptz_ne_date 1963*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ne_date(timestamp with time zone, date)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_ne_date$function$;/*timestamptz_cmp_date 1964*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_cmp_date(timestamp with time zone, date)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_cmp_date$function$;/*timestamp_lt_timestamptz 1965*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_lt_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_lt_timestamptz$function$;/*timestamp_le_timestamptz 1966*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_le_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_le_timestamptz$function$;/*timestamp_eq_timestamptz 1967*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_eq_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_eq_timestamptz$function$;/*timestamp_gt_timestamptz 1968*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_gt_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_gt_timestamptz$function$;/*timestamp_ge_timestamptz 1969*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ge_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_ge_timestamptz$function$;/*timestamp_ne_timestamptz 1970*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_ne_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_ne_timestamptz$function$;/*timestamp_cmp_timestamptz 1971*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_cmp_timestamptz(timestamp without time zone, timestamp with time zone)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamp_cmp_timestamptz$function$;/*timestamptz_lt_timestamp 1972*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_lt_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_lt_timestamp$function$;/*timestamptz_le_timestamp 1973*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_le_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_le_timestamp$function$;/*timestamptz_eq_timestamp 1974*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_eq_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_eq_timestamp$function$;/*timestamptz_gt_timestamp 1975*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_gt_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_gt_timestamp$function$;/*timestamptz_ge_timestamp 1976*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ge_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_ge_timestamp$function$;/*timestamptz_ne_timestamp 1977*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_ne_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_ne_timestamp$function$;/*timestamptz_cmp_timestamp 1978*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_cmp_timestamp(timestamp with time zone, timestamp without time zone)
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$timestamptz_cmp_timestamp$function$;/*array_recv 1979*/CREATE OR REPLACE FUNCTION pg_catalog.array_recv(internal, oid, integer)
 RETURNS anyarray
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_recv$function$;/*array_send 1980*/CREATE OR REPLACE FUNCTION pg_catalog.array_send(anyarray)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_send$function$;/*record_recv 1981*/CREATE OR REPLACE FUNCTION pg_catalog.record_recv(internal, oid, integer)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$record_recv$function$;/*record_send 1982*/CREATE OR REPLACE FUNCTION pg_catalog.record_send(record)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$record_send$function$;/*int2recv 1983*/CREATE OR REPLACE FUNCTION pg_catalog.int2recv(internal)
 RETURNS smallint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2recv$function$;/*int2send 1984*/CREATE OR REPLACE FUNCTION pg_catalog.int2send(smallint)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2send$function$;/*int4recv 1985*/CREATE OR REPLACE FUNCTION pg_catalog.int4recv(internal)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4recv$function$;/*int4send 1986*/CREATE OR REPLACE FUNCTION pg_catalog.int4send(integer)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4send$function$;/*int8recv 1987*/CREATE OR REPLACE FUNCTION pg_catalog.int8recv(internal)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8recv$function$;/*int8send 1988*/CREATE OR REPLACE FUNCTION pg_catalog.int8send(bigint)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8send$function$;/*int2vectorrecv 1989*/CREATE OR REPLACE FUNCTION pg_catalog.int2vectorrecv(internal)
 RETURNS int2vector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2vectorrecv$function$;/*int2vectorsend 1990*/CREATE OR REPLACE FUNCTION pg_catalog.int2vectorsend(int2vector)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int2vectorsend$function$;/*bytearecv 1991*/CREATE OR REPLACE FUNCTION pg_catalog.bytearecv(internal)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bytearecv$function$;/*byteasend 1992*/CREATE OR REPLACE FUNCTION pg_catalog.byteasend(bytea)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$byteasend$function$;/*textrecv 1993*/CREATE OR REPLACE FUNCTION pg_catalog.textrecv(internal)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$textrecv$function$;/*textsend 1994*/CREATE OR REPLACE FUNCTION pg_catalog.textsend(text)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$textsend$function$;/*unknownrecv 1995*/CREATE OR REPLACE FUNCTION pg_catalog.unknownrecv(internal)
 RETURNS unknown
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unknownrecv$function$;/*unknownsend 1996*/CREATE OR REPLACE FUNCTION pg_catalog.unknownsend(unknown)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unknownsend$function$;/*oidrecv 1997*/CREATE OR REPLACE FUNCTION pg_catalog.oidrecv(internal)
 RETURNS oid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidrecv$function$;/*oidsend 1998*/CREATE OR REPLACE FUNCTION pg_catalog.oidsend(oid)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidsend$function$;/*oidvectorrecv 1999*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorrecv(internal)
 RETURNS oidvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidvectorrecv$function$;/*oidvectorsend 2000*/CREATE OR REPLACE FUNCTION pg_catalog.oidvectorsend(oidvector)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$oidvectorsend$function$;/*namerecv 2001*/CREATE OR REPLACE FUNCTION pg_catalog.namerecv(internal)
 RETURNS name
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$namerecv$function$;/*namesend 2002*/CREATE OR REPLACE FUNCTION pg_catalog.namesend(name)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$namesend$function$;/*float4recv 2003*/CREATE OR REPLACE FUNCTION pg_catalog.float4recv(internal)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4recv$function$;/*float4send 2004*/CREATE OR REPLACE FUNCTION pg_catalog.float4send(real)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float4send$function$;/*float8recv 2005*/CREATE OR REPLACE FUNCTION pg_catalog.float8recv(internal)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8recv$function$;/*float8send 2006*/CREATE OR REPLACE FUNCTION pg_catalog.float8send(double precision)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$float8send$function$;/*point_recv 2007*/CREATE OR REPLACE FUNCTION pg_catalog.point_recv(internal)
 RETURNS point
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_recv$function$;/*point_send 2008*/CREATE OR REPLACE FUNCTION pg_catalog.point_send(point)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$point_send$function$;/*bpcharrecv 2009*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharrecv(internal, oid, integer)
 RETURNS character
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$bpcharrecv$function$;/*bpcharsend 2010*/CREATE OR REPLACE FUNCTION pg_catalog.bpcharsend(character)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$bpcharsend$function$;/*varcharrecv 2011*/CREATE OR REPLACE FUNCTION pg_catalog.varcharrecv(internal, oid, integer)
 RETURNS character varying
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$varcharrecv$function$;/*varcharsend 2012*/CREATE OR REPLACE FUNCTION pg_catalog.varcharsend(character varying)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$varcharsend$function$;/*charrecv 2013*/CREATE OR REPLACE FUNCTION pg_catalog.charrecv(internal)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$charrecv$function$;/*charsend 2014*/CREATE OR REPLACE FUNCTION pg_catalog.charsend("char")
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$charsend$function$;/*boolrecv 2015*/CREATE OR REPLACE FUNCTION pg_catalog.boolrecv(internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boolrecv$function$;/*boolsend 2016*/CREATE OR REPLACE FUNCTION pg_catalog.boolsend(boolean)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boolsend$function$;/*tidrecv 2017*/CREATE OR REPLACE FUNCTION pg_catalog.tidrecv(internal)
 RETURNS tid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidrecv$function$;/*tidsend 2018*/CREATE OR REPLACE FUNCTION pg_catalog.tidsend(tid)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tidsend$function$;/*xidrecv 2019*/CREATE OR REPLACE FUNCTION pg_catalog.xidrecv(internal)
 RETURNS xid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xidrecv$function$;/*xidsend 2020*/CREATE OR REPLACE FUNCTION pg_catalog.xidsend(xid)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xidsend$function$;/*cidrecv 2021*/CREATE OR REPLACE FUNCTION pg_catalog.cidrecv(internal)
 RETURNS cid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidrecv$function$;/*cidsend 2022*/CREATE OR REPLACE FUNCTION pg_catalog.cidsend(cid)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidsend$function$;/*regprocrecv 2023*/CREATE OR REPLACE FUNCTION pg_catalog.regprocrecv(internal)
 RETURNS regproc
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regprocrecv$function$;/*regprocsend 2024*/CREATE OR REPLACE FUNCTION pg_catalog.regprocsend(regproc)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regprocsend$function$;/*regprocedurerecv 2025*/CREATE OR REPLACE FUNCTION pg_catalog.regprocedurerecv(internal)
 RETURNS regprocedure
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regprocedurerecv$function$;/*regproceduresend 2026*/CREATE OR REPLACE FUNCTION pg_catalog.regproceduresend(regprocedure)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regproceduresend$function$;/*regoperrecv 2027*/CREATE OR REPLACE FUNCTION pg_catalog.regoperrecv(internal)
 RETURNS regoper
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regoperrecv$function$;/*regopersend 2028*/CREATE OR REPLACE FUNCTION pg_catalog.regopersend(regoper)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regopersend$function$;/*regoperatorrecv 2029*/CREATE OR REPLACE FUNCTION pg_catalog.regoperatorrecv(internal)
 RETURNS regoperator
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regoperatorrecv$function$;/*regoperatorsend 2030*/CREATE OR REPLACE FUNCTION pg_catalog.regoperatorsend(regoperator)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regoperatorsend$function$;/*regclassrecv 2031*/CREATE OR REPLACE FUNCTION pg_catalog.regclassrecv(internal)
 RETURNS regclass
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regclassrecv$function$;/*regclasssend 2032*/CREATE OR REPLACE FUNCTION pg_catalog.regclasssend(regclass)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regclasssend$function$;/*regcollationrecv 2033*/CREATE OR REPLACE FUNCTION pg_catalog.regcollationrecv(internal)
 RETURNS regcollation
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regcollationrecv$function$;/*regcollationsend 2034*/CREATE OR REPLACE FUNCTION pg_catalog.regcollationsend(regcollation)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regcollationsend$function$;/*regtyperecv 2035*/CREATE OR REPLACE FUNCTION pg_catalog.regtyperecv(internal)
 RETURNS regtype
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regtyperecv$function$;/*regtypesend 2036*/CREATE OR REPLACE FUNCTION pg_catalog.regtypesend(regtype)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regtypesend$function$;/*regrolerecv 2037*/CREATE OR REPLACE FUNCTION pg_catalog.regrolerecv(internal)
 RETURNS regrole
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regrolerecv$function$;/*regrolesend 2038*/CREATE OR REPLACE FUNCTION pg_catalog.regrolesend(regrole)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regrolesend$function$;/*regnamespacerecv 2039*/CREATE OR REPLACE FUNCTION pg_catalog.regnamespacerecv(internal)
 RETURNS regnamespace
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regnamespacerecv$function$;/*regnamespacesend 2040*/CREATE OR REPLACE FUNCTION pg_catalog.regnamespacesend(regnamespace)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regnamespacesend$function$;/*bit_recv 2041*/CREATE OR REPLACE FUNCTION pg_catalog.bit_recv(internal, oid, integer)
 RETURNS bit
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_recv$function$;/*bit_send 2042*/CREATE OR REPLACE FUNCTION pg_catalog.bit_send(bit)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bit_send$function$;/*varbit_recv 2043*/CREATE OR REPLACE FUNCTION pg_catalog.varbit_recv(internal, oid, integer)
 RETURNS bit varying
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbit_recv$function$;/*varbit_send 2044*/CREATE OR REPLACE FUNCTION pg_catalog.varbit_send(bit varying)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$varbit_send$function$;/*numeric_recv 2045*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_recv(internal, oid, integer)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_recv$function$;/*numeric_send 2046*/CREATE OR REPLACE FUNCTION pg_catalog.numeric_send(numeric)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numeric_send$function$;/*date_recv 2047*/CREATE OR REPLACE FUNCTION pg_catalog.date_recv(internal)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_recv$function$;/*date_send 2048*/CREATE OR REPLACE FUNCTION pg_catalog.date_send(date)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$date_send$function$;/*time_recv 2049*/CREATE OR REPLACE FUNCTION pg_catalog.time_recv(internal, oid, integer)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_recv$function$;/*time_send 2050*/CREATE OR REPLACE FUNCTION pg_catalog.time_send(time without time zone)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$time_send$function$;/*timetz_recv 2051*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_recv(internal, oid, integer)
 RETURNS time with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_recv$function$;/*timetz_send 2052*/CREATE OR REPLACE FUNCTION pg_catalog.timetz_send(time with time zone)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timetz_send$function$;/*timestamp_recv 2053*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_recv(internal, oid, integer)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_recv$function$;/*timestamp_send 2054*/CREATE OR REPLACE FUNCTION pg_catalog.timestamp_send(timestamp without time zone)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamp_send$function$;/*timestamptz_recv 2055*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_recv(internal, oid, integer)
 RETURNS timestamp with time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptz_recv$function$;/*timestamptz_send 2056*/CREATE OR REPLACE FUNCTION pg_catalog.timestamptz_send(timestamp with time zone)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$timestamptz_send$function$;/*interval_recv 2057*/CREATE OR REPLACE FUNCTION pg_catalog.interval_recv(internal, oid, integer)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_recv$function$;/*interval_send 2058*/CREATE OR REPLACE FUNCTION pg_catalog.interval_send(interval)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$interval_send$function$;/*lseg_recv 2059*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_recv(internal)
 RETURNS lseg
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_recv$function$;/*lseg_send 2060*/CREATE OR REPLACE FUNCTION pg_catalog.lseg_send(lseg)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$lseg_send$function$;/*path_recv 2061*/CREATE OR REPLACE FUNCTION pg_catalog.path_recv(internal)
 RETURNS path
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_recv$function$;/*path_send 2062*/CREATE OR REPLACE FUNCTION pg_catalog.path_send(path)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$path_send$function$;/*box_recv 2063*/CREATE OR REPLACE FUNCTION pg_catalog.box_recv(internal)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_recv$function$;/*box_send 2064*/CREATE OR REPLACE FUNCTION pg_catalog.box_send(box)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_send$function$;/*poly_recv 2065*/CREATE OR REPLACE FUNCTION pg_catalog.poly_recv(internal)
 RETURNS polygon
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_recv$function$;/*poly_send 2066*/CREATE OR REPLACE FUNCTION pg_catalog.poly_send(polygon)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_send$function$;/*line_recv 2067*/CREATE OR REPLACE FUNCTION pg_catalog.line_recv(internal)
 RETURNS line
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_recv$function$;/*line_send 2068*/CREATE OR REPLACE FUNCTION pg_catalog.line_send(line)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$line_send$function$;/*circle_recv 2069*/CREATE OR REPLACE FUNCTION pg_catalog.circle_recv(internal)
 RETURNS circle
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_recv$function$;/*circle_send 2070*/CREATE OR REPLACE FUNCTION pg_catalog.circle_send(circle)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_send$function$;/*cash_recv 2071*/CREATE OR REPLACE FUNCTION pg_catalog.cash_recv(internal)
 RETURNS money
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_recv$function$;/*cash_send 2072*/CREATE OR REPLACE FUNCTION pg_catalog.cash_send(money)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cash_send$function$;/*macaddr_recv 2073*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_recv(internal)
 RETURNS macaddr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_recv$function$;/*macaddr_send 2074*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr_send(macaddr)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr_send$function$;/*inet_recv 2075*/CREATE OR REPLACE FUNCTION pg_catalog.inet_recv(internal)
 RETURNS inet
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_recv$function$;/*inet_send 2076*/CREATE OR REPLACE FUNCTION pg_catalog.inet_send(inet)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$inet_send$function$;/*cidr_recv 2077*/CREATE OR REPLACE FUNCTION pg_catalog.cidr_recv(internal)
 RETURNS cidr
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_recv$function$;/*cidr_send 2078*/CREATE OR REPLACE FUNCTION pg_catalog.cidr_send(cidr)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$cidr_send$function$;/*cstring_recv 2079*/CREATE OR REPLACE FUNCTION pg_catalog.cstring_recv(internal)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$cstring_recv$function$;/*cstring_send 2080*/CREATE OR REPLACE FUNCTION pg_catalog.cstring_send(cstring)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$cstring_send$function$;/*anyarray_recv 2081*/CREATE OR REPLACE FUNCTION pg_catalog.anyarray_recv(internal)
 RETURNS anyarray
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyarray_recv$function$;/*anyarray_send 2082*/CREATE OR REPLACE FUNCTION pg_catalog.anyarray_send(anyarray)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyarray_send$function$;/*void_recv 2083*/CREATE OR REPLACE FUNCTION pg_catalog.void_recv(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$void_recv$function$;/*void_send 2084*/CREATE OR REPLACE FUNCTION pg_catalog.void_send(void)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$void_send$function$;/*macaddr8_recv 2085*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_recv(internal)
 RETURNS macaddr8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_recv$function$;/*macaddr8_send 2086*/CREATE OR REPLACE FUNCTION pg_catalog.macaddr8_send(macaddr8)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$macaddr8_send$function$;/*pg_get_ruledef 2087*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_ruledef(oid, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_ruledef_ext$function$;/*pg_get_viewdef 2088*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_viewdef(text, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_get_viewdef_name_ext$function$;/*pg_get_viewdef 2089*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_viewdef(oid, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_get_viewdef_ext$function$;/*pg_get_viewdef 2090*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_viewdef(oid, integer)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_get_viewdef_wrap$function$;/*pg_get_indexdef 2091*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_indexdef(oid, integer, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_indexdef_ext$function$;/*pg_get_constraintdef 2092*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_constraintdef(oid, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_constraintdef_ext$function$;/*pg_get_expr 2093*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_expr(pg_node_tree, oid, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_expr_ext$function$;/*pg_prepared_statement 2094*/CREATE OR REPLACE FUNCTION pg_catalog.pg_prepared_statement(OUT name text, OUT statement text, OUT prepare_time timestamp with time zone, OUT parameter_types regtype[], OUT from_sql boolean)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_prepared_statement$function$;/*pg_cursor 2095*/CREATE OR REPLACE FUNCTION pg_catalog.pg_cursor(OUT name text, OUT statement text, OUT is_holdable boolean, OUT is_binary boolean, OUT is_scrollable boolean, OUT creation_time timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_cursor$function$;/*pg_timezone_abbrevs 2096*/CREATE OR REPLACE FUNCTION pg_catalog.pg_timezone_abbrevs(OUT abbrev text, OUT utc_offset interval, OUT is_dst boolean)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_timezone_abbrevs$function$;/*pg_timezone_names 2097*/CREATE OR REPLACE FUNCTION pg_catalog.pg_timezone_names(OUT name text, OUT abbrev text, OUT utc_offset interval, OUT is_dst boolean)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_timezone_names$function$;/*pg_get_triggerdef 2098*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_triggerdef(oid, boolean)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_triggerdef_ext$function$;/*pg_listening_channels 2099*/CREATE OR REPLACE FUNCTION pg_catalog.pg_listening_channels()
 RETURNS SETOF text
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT ROWS 10
AS $function$pg_listening_channels$function$;/*pg_notify 2100*/CREATE OR REPLACE FUNCTION pg_catalog.pg_notify(text, text)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED
AS $function$pg_notify$function$;/*pg_notification_queue_usage 2101*/CREATE OR REPLACE FUNCTION pg_catalog.pg_notification_queue_usage()
 RETURNS double precision
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_notification_queue_usage$function$;/*show_limit 2102*/CREATE OR REPLACE FUNCTION public.show_limit()
 RETURNS real
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$show_limit$function$;/*generate_series 2103*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(integer, integer, integer)
 RETURNS SETOF integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT generate_series_int4_support
AS $function$generate_series_step_int4$function$;/*generate_series 2104*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(integer, integer)
 RETURNS SETOF integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT generate_series_int4_support
AS $function$generate_series_int4$function$;/*generate_series_int4_support 2105*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series_int4_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_series_int4_support$function$;/*generate_series 2106*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(bigint, bigint, bigint)
 RETURNS SETOF bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT generate_series_int8_support
AS $function$generate_series_step_int8$function$;/*generate_series 2107*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(bigint, bigint)
 RETURNS SETOF bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT SUPPORT generate_series_int8_support
AS $function$generate_series_int8$function$;/*generate_series_int8_support 2108*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series_int8_support(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_series_int8_support$function$;/*generate_series 2109*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(numeric, numeric, numeric)
 RETURNS SETOF numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_series_step_numeric$function$;/*generate_series 2110*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(numeric, numeric)
 RETURNS SETOF numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_series_numeric$function$;/*generate_series 2111*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(timestamp without time zone, timestamp without time zone, interval)
 RETURNS SETOF timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$generate_series_timestamp$function$;/*generate_series 2112*/CREATE OR REPLACE FUNCTION pg_catalog.generate_series(timestamp with time zone, timestamp with time zone, interval)
 RETURNS SETOF timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$generate_series_timestamptz$function$;/*booland_statefunc 2113*/CREATE OR REPLACE FUNCTION pg_catalog.booland_statefunc(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$booland_statefunc$function$;/*boolor_statefunc 2114*/CREATE OR REPLACE FUNCTION pg_catalog.boolor_statefunc(boolean, boolean)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$boolor_statefunc$function$;/*bool_accum 2115*/CREATE OR REPLACE FUNCTION pg_catalog.bool_accum(internal, boolean)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$bool_accum$function$;/*bool_accum_inv 2116*/CREATE OR REPLACE FUNCTION pg_catalog.bool_accum_inv(internal, boolean)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$bool_accum_inv$function$;/*bool_alltrue 2117*/CREATE OR REPLACE FUNCTION pg_catalog.bool_alltrue(internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bool_alltrue$function$;/*bool_anytrue 2118*/CREATE OR REPLACE FUNCTION pg_catalog.bool_anytrue(internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bool_anytrue$function$;/*interval_pl_date 2119*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl_date(interval, date)
 RETURNS timestamp without time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*interval_pl_timetz 2120*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl_timetz(interval, time with time zone)
 RETURNS time with time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*interval_pl_timestamp 2121*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl_timestamp(interval, timestamp without time zone)
 RETURNS timestamp without time zone
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*interval_pl_timestamptz 2122*/CREATE OR REPLACE FUNCTION pg_catalog.interval_pl_timestamptz(interval, timestamp with time zone)
 RETURNS timestamp with time zone
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*integer_pl_date 2123*/CREATE OR REPLACE FUNCTION pg_catalog.integer_pl_date(integer, date)
 RETURNS date
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select $2 + $1$function$;/*pg_tablespace_databases 2124*/CREATE OR REPLACE FUNCTION pg_catalog.pg_tablespace_databases(oid)
 RETURNS SETOF oid
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_tablespace_databases$function$;/*bool 2125*/CREATE OR REPLACE FUNCTION pg_catalog.bool(integer)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4_bool$function$;/*int4 2126*/CREATE OR REPLACE FUNCTION pg_catalog.int4(boolean)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$bool_int4$function$;/*lastval 2127*/CREATE OR REPLACE FUNCTION pg_catalog.lastval()
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$lastval$function$;/*pg_postmaster_start_time 2128*/CREATE OR REPLACE FUNCTION pg_catalog.pg_postmaster_start_time()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_postmaster_start_time$function$;/*pg_conf_load_time 2129*/CREATE OR REPLACE FUNCTION pg_catalog.pg_conf_load_time()
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_conf_load_time$function$;/*box_below 2130*/CREATE OR REPLACE FUNCTION pg_catalog.box_below(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_below$function$;/*box_overbelow 2131*/CREATE OR REPLACE FUNCTION pg_catalog.box_overbelow(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_overbelow$function$;/*box_overabove 2132*/CREATE OR REPLACE FUNCTION pg_catalog.box_overabove(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_overabove$function$;/*box_above 2133*/CREATE OR REPLACE FUNCTION pg_catalog.box_above(box, box)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$box_above$function$;/*poly_below 2134*/CREATE OR REPLACE FUNCTION pg_catalog.poly_below(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_below$function$;/*poly_overbelow 2135*/CREATE OR REPLACE FUNCTION pg_catalog.poly_overbelow(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_overbelow$function$;/*poly_overabove 2136*/CREATE OR REPLACE FUNCTION pg_catalog.poly_overabove(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_overabove$function$;/*poly_above 2137*/CREATE OR REPLACE FUNCTION pg_catalog.poly_above(polygon, polygon)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$poly_above$function$;/*circle_overbelow 2138*/CREATE OR REPLACE FUNCTION pg_catalog.circle_overbelow(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_overbelow$function$;/*circle_overabove 2139*/CREATE OR REPLACE FUNCTION pg_catalog.circle_overabove(circle, circle)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$circle_overabove$function$;/*gist_box_consistent 2140*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_consistent(internal, box, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_consistent$function$;/*gist_box_penalty 2141*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_penalty$function$;/*gist_box_picksplit 2142*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_picksplit$function$;/*gist_box_union 2143*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_union(internal, internal)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_union$function$;/*gist_box_same 2144*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_same(box, box, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_same$function$;/*gist_box_distance 2145*/CREATE OR REPLACE FUNCTION pg_catalog.gist_box_distance(internal, box, smallint, oid, internal)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_box_distance$function$;/*gist_poly_consistent 2146*/CREATE OR REPLACE FUNCTION pg_catalog.gist_poly_consistent(internal, polygon, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_poly_consistent$function$;/*gist_poly_compress 2147*/CREATE OR REPLACE FUNCTION pg_catalog.gist_poly_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_poly_compress$function$;/*gist_circle_consistent 2148*/CREATE OR REPLACE FUNCTION pg_catalog.gist_circle_consistent(internal, circle, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_circle_consistent$function$;/*gist_circle_compress 2149*/CREATE OR REPLACE FUNCTION pg_catalog.gist_circle_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_circle_compress$function$;/*gist_point_compress 2150*/CREATE OR REPLACE FUNCTION pg_catalog.gist_point_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_point_compress$function$;/*gist_point_fetch 2151*/CREATE OR REPLACE FUNCTION pg_catalog.gist_point_fetch(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_point_fetch$function$;/*gist_point_consistent 2152*/CREATE OR REPLACE FUNCTION pg_catalog.gist_point_consistent(internal, point, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_point_consistent$function$;/*gist_point_distance 2153*/CREATE OR REPLACE FUNCTION pg_catalog.gist_point_distance(internal, point, smallint, oid, internal)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_point_distance$function$;/*gist_circle_distance 2154*/CREATE OR REPLACE FUNCTION pg_catalog.gist_circle_distance(internal, circle, smallint, oid, internal)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_circle_distance$function$;/*gist_poly_distance 2155*/CREATE OR REPLACE FUNCTION pg_catalog.gist_poly_distance(internal, polygon, smallint, oid, internal)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gist_poly_distance$function$;/*ginarrayextract 2156*/CREATE OR REPLACE FUNCTION pg_catalog.ginarrayextract(anyarray, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ginarrayextract$function$;/*ginqueryarrayextract 2157*/CREATE OR REPLACE FUNCTION pg_catalog.ginqueryarrayextract(anyarray, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ginqueryarrayextract$function$;/*ginarrayconsistent 2158*/CREATE OR REPLACE FUNCTION pg_catalog.ginarrayconsistent(internal, smallint, anyarray, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ginarrayconsistent$function$;/*ginarraytriconsistent 2159*/CREATE OR REPLACE FUNCTION pg_catalog.ginarraytriconsistent(internal, smallint, anyarray, integer, internal, internal, internal)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ginarraytriconsistent$function$;/*ginarrayextract 2160*/CREATE OR REPLACE FUNCTION pg_catalog.ginarrayextract(anyarray, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ginarrayextract_2args$function$;/*arrayoverlap 2161*/CREATE OR REPLACE FUNCTION pg_catalog.arrayoverlap(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$arrayoverlap$function$;/*arraycontains 2162*/CREATE OR REPLACE FUNCTION pg_catalog.arraycontains(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$arraycontains$function$;/*arraycontained 2163*/CREATE OR REPLACE FUNCTION pg_catalog.arraycontained(anyarray, anyarray)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$arraycontained$function$;/*brin_minmax_opcinfo 2164*/CREATE OR REPLACE FUNCTION pg_catalog.brin_minmax_opcinfo(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_minmax_opcinfo$function$;/*brin_minmax_add_value 2165*/CREATE OR REPLACE FUNCTION pg_catalog.brin_minmax_add_value(internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_minmax_add_value$function$;/*brin_minmax_consistent 2166*/CREATE OR REPLACE FUNCTION pg_catalog.brin_minmax_consistent(internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_minmax_consistent$function$;/*brin_minmax_union 2167*/CREATE OR REPLACE FUNCTION pg_catalog.brin_minmax_union(internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_minmax_union$function$;/*brin_inclusion_opcinfo 2168*/CREATE OR REPLACE FUNCTION pg_catalog.brin_inclusion_opcinfo(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_inclusion_opcinfo$function$;/*brin_inclusion_add_value 2169*/CREATE OR REPLACE FUNCTION pg_catalog.brin_inclusion_add_value(internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_inclusion_add_value$function$;/*brin_inclusion_consistent 2170*/CREATE OR REPLACE FUNCTION pg_catalog.brin_inclusion_consistent(internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_inclusion_consistent$function$;/*brin_inclusion_union 2171*/CREATE OR REPLACE FUNCTION pg_catalog.brin_inclusion_union(internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$brin_inclusion_union$function$;/*pg_advisory_lock 2172*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_lock(bigint)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_lock_int8$function$;/*pg_advisory_xact_lock 2173*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_xact_lock(bigint)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_xact_lock_int8$function$;/*pg_advisory_lock_shared 2174*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_lock_shared(bigint)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_lock_shared_int8$function$;/*pg_advisory_xact_lock_shared 2175*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_xact_lock_shared(bigint)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_xact_lock_shared_int8$function$;/*pg_try_advisory_lock 2176*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_lock(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_lock_int8$function$;/*pg_try_advisory_xact_lock 2177*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_xact_lock(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_xact_lock_int8$function$;/*pg_try_advisory_lock_shared 2178*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_lock_shared(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_lock_shared_int8$function$;/*pg_try_advisory_xact_lock_shared 2179*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_xact_lock_shared(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_xact_lock_shared_int8$function$;/*pg_advisory_unlock 2180*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_unlock(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_unlock_int8$function$;/*pg_advisory_unlock_shared 2181*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_unlock_shared(bigint)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_unlock_shared_int8$function$;/*pg_advisory_lock 2182*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_lock(integer, integer)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_lock_int4$function$;/*pg_advisory_xact_lock 2183*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_xact_lock(integer, integer)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_xact_lock_int4$function$;/*pg_advisory_lock_shared 2184*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_lock_shared(integer, integer)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_lock_shared_int4$function$;/*pg_advisory_xact_lock_shared 2185*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_xact_lock_shared(integer, integer)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_xact_lock_shared_int4$function$;/*pg_try_advisory_lock 2186*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_lock(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_lock_int4$function$;/*pg_try_advisory_xact_lock 2187*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_xact_lock(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_xact_lock_int4$function$;/*pg_try_advisory_lock_shared 2188*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_lock_shared(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_lock_shared_int4$function$;/*pg_try_advisory_xact_lock_shared 2189*/CREATE OR REPLACE FUNCTION pg_catalog.pg_try_advisory_xact_lock_shared(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_try_advisory_xact_lock_shared_int4$function$;/*pg_advisory_unlock 2190*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_unlock(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_unlock_int4$function$;/*pg_advisory_unlock_shared 2191*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_unlock_shared(integer, integer)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_unlock_shared_int4$function$;/*pg_advisory_unlock_all 2192*/CREATE OR REPLACE FUNCTION pg_catalog.pg_advisory_unlock_all()
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_advisory_unlock_all$function$;/*xml_in 2193*/CREATE OR REPLACE FUNCTION pg_catalog.xml_in(cstring)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$xml_in$function$;/*xml_out 2194*/CREATE OR REPLACE FUNCTION pg_catalog.xml_out(xml)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xml_out$function$;/*xmlcomment 2195*/CREATE OR REPLACE FUNCTION pg_catalog.xmlcomment(text)
 RETURNS xml
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xmlcomment$function$;/*xml 2196*/CREATE OR REPLACE FUNCTION pg_catalog.xml(text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$texttoxml$function$;/*xmlvalidate 2197*/CREATE OR REPLACE FUNCTION pg_catalog.xmlvalidate(xml, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xmlvalidate$function$;/*xml_recv 2198*/CREATE OR REPLACE FUNCTION pg_catalog.xml_recv(internal)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$xml_recv$function$;/*xml_send 2199*/CREATE OR REPLACE FUNCTION pg_catalog.xml_send(xml)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$xml_send$function$;/*xmlconcat2 2200*/CREATE OR REPLACE FUNCTION pg_catalog.xmlconcat2(xml, xml)
 RETURNS xml
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$xmlconcat2$function$;/*text 2201*/CREATE OR REPLACE FUNCTION pg_catalog.text(xml)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xmltotext$function$;/*table_to_xml 2202*/CREATE OR REPLACE FUNCTION pg_catalog.table_to_xml(tbl regclass, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$table_to_xml$function$;/*query_to_xml 2203*/CREATE OR REPLACE FUNCTION pg_catalog.query_to_xml(query text, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STRICT COST 100
AS $function$query_to_xml$function$;/*cursor_to_xml 2204*/CREATE OR REPLACE FUNCTION pg_catalog.cursor_to_xml(cursor refcursor, count integer, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STRICT COST 100
AS $function$cursor_to_xml$function$;/*table_to_xmlschema 2205*/CREATE OR REPLACE FUNCTION pg_catalog.table_to_xmlschema(tbl regclass, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$table_to_xmlschema$function$;/*query_to_xmlschema 2206*/CREATE OR REPLACE FUNCTION pg_catalog.query_to_xmlschema(query text, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STRICT COST 100
AS $function$query_to_xmlschema$function$;/*cursor_to_xmlschema 2207*/CREATE OR REPLACE FUNCTION pg_catalog.cursor_to_xmlschema(cursor refcursor, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STRICT COST 100
AS $function$cursor_to_xmlschema$function$;/*table_to_xml_and_xmlschema 2208*/CREATE OR REPLACE FUNCTION pg_catalog.table_to_xml_and_xmlschema(tbl regclass, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$table_to_xml_and_xmlschema$function$;/*query_to_xml_and_xmlschema 2209*/CREATE OR REPLACE FUNCTION pg_catalog.query_to_xml_and_xmlschema(query text, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STRICT COST 100
AS $function$query_to_xml_and_xmlschema$function$;/*schema_to_xml 2210*/CREATE OR REPLACE FUNCTION pg_catalog.schema_to_xml(schema name, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$schema_to_xml$function$;/*schema_to_xmlschema 2211*/CREATE OR REPLACE FUNCTION pg_catalog.schema_to_xmlschema(schema name, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$schema_to_xmlschema$function$;/*schema_to_xml_and_xmlschema 2212*/CREATE OR REPLACE FUNCTION pg_catalog.schema_to_xml_and_xmlschema(schema name, nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$schema_to_xml_and_xmlschema$function$;/*database_to_xml 2213*/CREATE OR REPLACE FUNCTION pg_catalog.database_to_xml(nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$database_to_xml$function$;/*database_to_xmlschema 2214*/CREATE OR REPLACE FUNCTION pg_catalog.database_to_xmlschema(nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$database_to_xmlschema$function$;/*database_to_xml_and_xmlschema 2215*/CREATE OR REPLACE FUNCTION pg_catalog.database_to_xml_and_xmlschema(nulls boolean, tableforest boolean, targetns text)
 RETURNS xml
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 100
AS $function$database_to_xml_and_xmlschema$function$;/*xpath 2216*/CREATE OR REPLACE FUNCTION pg_catalog.xpath(text, xml, text[])
 RETURNS xml[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xpath$function$;/*xpath 2217*/CREATE OR REPLACE FUNCTION pg_catalog.xpath(text, xml)
 RETURNS xml[]
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.xpath($1, $2, '{}'::pg_catalog.text[])$function$;/*xmlexists 2218*/CREATE OR REPLACE FUNCTION pg_catalog."xmlexists"(text, xml)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xmlexists$function$;/*xpath_exists 2219*/CREATE OR REPLACE FUNCTION pg_catalog.xpath_exists(text, xml, text[])
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xpath_exists$function$;/*xpath_exists 2220*/CREATE OR REPLACE FUNCTION pg_catalog.xpath_exists(text, xml)
 RETURNS boolean
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT COST 1
AS $function$select pg_catalog.xpath_exists($1, $2, '{}'::pg_catalog.text[])$function$;/*xml_is_well_formed 2221*/CREATE OR REPLACE FUNCTION pg_catalog.xml_is_well_formed(text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$xml_is_well_formed$function$;/*xml_is_well_formed_document 2222*/CREATE OR REPLACE FUNCTION pg_catalog.xml_is_well_formed_document(text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xml_is_well_formed_document$function$;/*xml_is_well_formed_content 2223*/CREATE OR REPLACE FUNCTION pg_catalog.xml_is_well_formed_content(text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$xml_is_well_formed_content$function$;/*json_in 2224*/CREATE OR REPLACE FUNCTION pg_catalog.json_in(cstring)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_in$function$;/*json_out 2225*/CREATE OR REPLACE FUNCTION pg_catalog.json_out(json)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_out$function$;/*json_recv 2226*/CREATE OR REPLACE FUNCTION pg_catalog.json_recv(internal)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_recv$function$;/*json_send 2227*/CREATE OR REPLACE FUNCTION pg_catalog.json_send(json)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_send$function$;/*array_to_json 2228*/CREATE OR REPLACE FUNCTION pg_catalog.array_to_json(anyarray)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_to_json$function$;/*array_to_json 2229*/CREATE OR REPLACE FUNCTION pg_catalog.array_to_json(anyarray, boolean)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$array_to_json_pretty$function$;/*row_to_json 2230*/CREATE OR REPLACE FUNCTION pg_catalog.row_to_json(record)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$row_to_json$function$;/*row_to_json 2231*/CREATE OR REPLACE FUNCTION pg_catalog.row_to_json(record, boolean)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$row_to_json_pretty$function$;/*json_agg_transfn 2232*/CREATE OR REPLACE FUNCTION pg_catalog.json_agg_transfn(internal, anyelement)
 RETURNS internal
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_agg_transfn$function$;/*json_agg_finalfn 2233*/CREATE OR REPLACE FUNCTION pg_catalog.json_agg_finalfn(internal)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$json_agg_finalfn$function$;/*json_object_agg_transfn 2234*/CREATE OR REPLACE FUNCTION pg_catalog.json_object_agg_transfn(internal, "any", "any")
 RETURNS internal
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_object_agg_transfn$function$;/*json_object_agg_finalfn 2235*/CREATE OR REPLACE FUNCTION pg_catalog.json_object_agg_finalfn(internal)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$json_object_agg_finalfn$function$;/*json_build_array 2236*/CREATE OR REPLACE FUNCTION pg_catalog.json_build_array(VARIADIC "any")
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_build_array$function$;/*json_build_array 2237*/CREATE OR REPLACE FUNCTION pg_catalog.json_build_array()
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_build_array_noargs$function$;/*json_build_object 2238*/CREATE OR REPLACE FUNCTION pg_catalog.json_build_object(VARIADIC "any")
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_build_object$function$;/*json_build_object 2239*/CREATE OR REPLACE FUNCTION pg_catalog.json_build_object()
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_build_object_noargs$function$;/*json_object 2240*/CREATE OR REPLACE FUNCTION pg_catalog.json_object(text[])
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_object$function$;/*json_object 2241*/CREATE OR REPLACE FUNCTION pg_catalog.json_object(text[], text[])
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_object_two_arg$function$;/*to_json 2242*/CREATE OR REPLACE FUNCTION pg_catalog.to_json(anyelement)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_json$function$;/*json_strip_nulls 2243*/CREATE OR REPLACE FUNCTION pg_catalog.json_strip_nulls(json)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_strip_nulls$function$;/*json_object_field 2244*/CREATE OR REPLACE FUNCTION pg_catalog.json_object_field(from_json json, field_name text)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_object_field$function$;/*json_object_field_text 2245*/CREATE OR REPLACE FUNCTION pg_catalog.json_object_field_text(from_json json, field_name text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_object_field_text$function$;/*json_array_element 2246*/CREATE OR REPLACE FUNCTION pg_catalog.json_array_element(from_json json, element_index integer)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_array_element$function$;/*json_array_element_text 2247*/CREATE OR REPLACE FUNCTION pg_catalog.json_array_element_text(from_json json, element_index integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_array_element_text$function$;/*json_extract_path 2248*/CREATE OR REPLACE FUNCTION pg_catalog.json_extract_path(from_json json, VARIADIC path_elems text[])
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_extract_path$function$;/*json_extract_path_text 2249*/CREATE OR REPLACE FUNCTION pg_catalog.json_extract_path_text(from_json json, VARIADIC path_elems text[])
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_extract_path_text$function$;/*json_array_elements 2250*/CREATE OR REPLACE FUNCTION pg_catalog.json_array_elements(from_json json, OUT value json)
 RETURNS SETOF json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$json_array_elements$function$;/*json_array_elements_text 2251*/CREATE OR REPLACE FUNCTION pg_catalog.json_array_elements_text(from_json json, OUT value text)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$json_array_elements_text$function$;/*json_array_length 2252*/CREATE OR REPLACE FUNCTION pg_catalog.json_array_length(json)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_array_length$function$;/*json_object_keys 2253*/CREATE OR REPLACE FUNCTION pg_catalog.json_object_keys(json)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$json_object_keys$function$;/*json_each 2254*/CREATE OR REPLACE FUNCTION pg_catalog.json_each(from_json json, OUT key text, OUT value json)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$json_each$function$;/*json_each_text 2255*/CREATE OR REPLACE FUNCTION pg_catalog.json_each_text(from_json json, OUT key text, OUT value text)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$json_each_text$function$;/*show_trgm 2256*/CREATE OR REPLACE FUNCTION public.show_trgm(text)
 RETURNS text[]
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$show_trgm$function$;/*similarity 2257*/CREATE OR REPLACE FUNCTION public.similarity(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$similarity$function$;/*json_to_record 2258*/CREATE OR REPLACE FUNCTION pg_catalog.json_to_record(json)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$json_to_record$function$;/*json_to_recordset 2259*/CREATE OR REPLACE FUNCTION pg_catalog.json_to_recordset(json)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE ROWS 100
AS $function$json_to_recordset$function$;/*json_typeof 2260*/CREATE OR REPLACE FUNCTION pg_catalog.json_typeof(json)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$json_typeof$function$;/*uuid_in 2261*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_in(cstring)
 RETURNS uuid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_in$function$;/*uuid_out 2262*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_out(uuid)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_out$function$;/*uuid_lt 2263*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_lt(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_lt$function$;/*uuid_le 2264*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_le(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_le$function$;/*uuid_eq 2265*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_eq(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_eq$function$;/*uuid_ge 2266*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_ge(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_ge$function$;/*uuid_gt 2267*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_gt(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_gt$function$;/*uuid_ne 2268*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_ne(uuid, uuid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_ne$function$;/*uuid_cmp 2269*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_cmp(uuid, uuid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$uuid_cmp$function$;/*uuid_sortsupport 2270*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_sortsupport(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_sortsupport$function$;/*uuid_recv 2271*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_recv(internal)
 RETURNS uuid
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_recv$function$;/*uuid_send 2272*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_send(uuid)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_send$function$;/*uuid_hash 2273*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_hash(uuid)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_hash$function$;/*uuid_hash_extended 2274*/CREATE OR REPLACE FUNCTION pg_catalog.uuid_hash_extended(uuid, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$uuid_hash_extended$function$;/*gen_random_uuid 2275*/CREATE OR REPLACE FUNCTION pg_catalog.gen_random_uuid()
 RETURNS uuid
 LANGUAGE internal
 PARALLEL SAFE STRICT LEAKPROOF
AS $function$gen_random_uuid$function$;/*pg_lsn_in 2276*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_in(cstring)
 RETURNS pg_lsn
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_in$function$;/*pg_lsn_out 2277*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_out(pg_lsn)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_out$function$;/*pg_lsn_lt 2278*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_lt(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_lt$function$;/*pg_lsn_le 2279*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_le(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_le$function$;/*pg_lsn_eq 2280*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_eq(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_eq$function$;/*pg_lsn_ge 2281*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_ge(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_ge$function$;/*pg_lsn_gt 2282*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_gt(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_gt$function$;/*pg_lsn_ne 2283*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_ne(pg_lsn, pg_lsn)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_ne$function$;/*pg_lsn_mi 2284*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_mi(pg_lsn, pg_lsn)
 RETURNS numeric
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_mi$function$;/*pg_lsn_recv 2285*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_recv(internal)
 RETURNS pg_lsn
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_recv$function$;/*pg_lsn_send 2286*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_send(pg_lsn)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_send$function$;/*pg_lsn_cmp 2287*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_cmp(pg_lsn, pg_lsn)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT LEAKPROOF
AS $function$pg_lsn_cmp$function$;/*pg_lsn_hash 2288*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_hash(pg_lsn)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_hash$function$;/*pg_lsn_hash_extended 2289*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_hash_extended(pg_lsn, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_hash_extended$function$;/*pg_lsn_larger 2290*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_larger(pg_lsn, pg_lsn)
 RETURNS pg_lsn
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_larger$function$;/*pg_lsn_smaller 2291*/CREATE OR REPLACE FUNCTION pg_catalog.pg_lsn_smaller(pg_lsn, pg_lsn)
 RETURNS pg_lsn
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_lsn_smaller$function$;/*anyenum_in 2292*/CREATE OR REPLACE FUNCTION pg_catalog.anyenum_in(cstring)
 RETURNS anyenum
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$anyenum_in$function$;/*anyenum_out 2293*/CREATE OR REPLACE FUNCTION pg_catalog.anyenum_out(anyenum)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyenum_out$function$;/*enum_in 2294*/CREATE OR REPLACE FUNCTION pg_catalog.enum_in(cstring, oid)
 RETURNS anyenum
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$enum_in$function$;/*enum_out 2295*/CREATE OR REPLACE FUNCTION pg_catalog.enum_out(anyenum)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$enum_out$function$;/*enum_eq 2296*/CREATE OR REPLACE FUNCTION pg_catalog.enum_eq(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_eq$function$;/*enum_ne 2297*/CREATE OR REPLACE FUNCTION pg_catalog.enum_ne(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_ne$function$;/*enum_lt 2298*/CREATE OR REPLACE FUNCTION pg_catalog.enum_lt(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_lt$function$;/*enum_gt 2299*/CREATE OR REPLACE FUNCTION pg_catalog.enum_gt(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_gt$function$;/*enum_le 2300*/CREATE OR REPLACE FUNCTION pg_catalog.enum_le(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_le$function$;/*enum_ge 2301*/CREATE OR REPLACE FUNCTION pg_catalog.enum_ge(anyenum, anyenum)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_ge$function$;/*enum_cmp 2302*/CREATE OR REPLACE FUNCTION pg_catalog.enum_cmp(anyenum, anyenum)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_cmp$function$;/*hashenum 2303*/CREATE OR REPLACE FUNCTION pg_catalog.hashenum(anyenum)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashenum$function$;/*hashenumextended 2304*/CREATE OR REPLACE FUNCTION pg_catalog.hashenumextended(anyenum, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hashenumextended$function$;/*enum_smaller 2305*/CREATE OR REPLACE FUNCTION pg_catalog.enum_smaller(anyenum, anyenum)
 RETURNS anyenum
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_smaller$function$;/*enum_larger 2306*/CREATE OR REPLACE FUNCTION pg_catalog.enum_larger(anyenum, anyenum)
 RETURNS anyenum
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$enum_larger$function$;/*enum_first 2307*/CREATE OR REPLACE FUNCTION pg_catalog.enum_first(anyenum)
 RETURNS anyenum
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$enum_first$function$;/*enum_last 2308*/CREATE OR REPLACE FUNCTION pg_catalog.enum_last(anyenum)
 RETURNS anyenum
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$enum_last$function$;/*enum_range 2309*/CREATE OR REPLACE FUNCTION pg_catalog.enum_range(anyenum, anyenum)
 RETURNS anyarray
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$enum_range_bounds$function$;/*enum_range 2310*/CREATE OR REPLACE FUNCTION pg_catalog.enum_range(anyenum)
 RETURNS anyarray
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$enum_range_all$function$;/*enum_recv 2311*/CREATE OR REPLACE FUNCTION pg_catalog.enum_recv(internal, oid)
 RETURNS anyenum
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$enum_recv$function$;/*enum_send 2312*/CREATE OR REPLACE FUNCTION pg_catalog.enum_send(anyenum)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$enum_send$function$;/*tsvectorin 2313*/CREATE OR REPLACE FUNCTION pg_catalog.tsvectorin(cstring)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvectorin$function$;/*tsvectorrecv 2314*/CREATE OR REPLACE FUNCTION pg_catalog.tsvectorrecv(internal)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvectorrecv$function$;/*tsvectorout 2315*/CREATE OR REPLACE FUNCTION pg_catalog.tsvectorout(tsvector)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvectorout$function$;/*tsvectorsend 2316*/CREATE OR REPLACE FUNCTION pg_catalog.tsvectorsend(tsvector)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvectorsend$function$;/*tsqueryin 2317*/CREATE OR REPLACE FUNCTION pg_catalog.tsqueryin(cstring)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsqueryin$function$;/*tsqueryrecv 2318*/CREATE OR REPLACE FUNCTION pg_catalog.tsqueryrecv(internal)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsqueryrecv$function$;/*tsqueryout 2319*/CREATE OR REPLACE FUNCTION pg_catalog.tsqueryout(tsquery)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsqueryout$function$;/*tsquerysend 2320*/CREATE OR REPLACE FUNCTION pg_catalog.tsquerysend(tsquery)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquerysend$function$;/*gtsvectorin 2321*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvectorin(cstring)
 RETURNS gtsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvectorin$function$;/*gtsvectorout 2322*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvectorout(gtsvector)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvectorout$function$;/*tsvector_lt 2323*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_lt(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_lt$function$;/*tsvector_le 2324*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_le(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_le$function$;/*tsvector_eq 2325*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_eq(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_eq$function$;/*tsvector_ne 2326*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_ne(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_ne$function$;/*tsvector_ge 2327*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_ge(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_ge$function$;/*tsvector_gt 2328*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_gt(tsvector, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_gt$function$;/*tsvector_cmp 2329*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_cmp(tsvector, tsvector)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_cmp$function$;/*length 2330*/CREATE OR REPLACE FUNCTION pg_catalog.length(tsvector)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_length$function$;/*strip 2331*/CREATE OR REPLACE FUNCTION pg_catalog.strip(tsvector)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_strip$function$;/*setweight 2332*/CREATE OR REPLACE FUNCTION pg_catalog.setweight(tsvector, "char")
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_setweight$function$;/*setweight 2333*/CREATE OR REPLACE FUNCTION pg_catalog.setweight(tsvector, "char", text[])
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_setweight_by_filter$function$;/*tsvector_concat 2334*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_concat(tsvector, tsvector)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_concat$function$;/*ts_delete 2335*/CREATE OR REPLACE FUNCTION pg_catalog.ts_delete(tsvector, text)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_delete_str$function$;/*ts_delete 2336*/CREATE OR REPLACE FUNCTION pg_catalog.ts_delete(tsvector, text[])
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_delete_arr$function$;/*unnest 2337*/CREATE OR REPLACE FUNCTION pg_catalog.unnest(tsvector tsvector, OUT lexeme text, OUT positions smallint[], OUT weights text[])
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 10
AS $function$tsvector_unnest$function$;/*tsvector_to_array 2338*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_to_array(tsvector)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_to_array$function$;/*array_to_tsvector 2339*/CREATE OR REPLACE FUNCTION pg_catalog.array_to_tsvector(text[])
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$array_to_tsvector$function$;/*ts_filter 2340*/CREATE OR REPLACE FUNCTION pg_catalog.ts_filter(tsvector, "char"[])
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsvector_filter$function$;/*ts_match_vq 2341*/CREATE OR REPLACE FUNCTION pg_catalog.ts_match_vq(tsvector, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_match_vq$function$;/*ts_match_qv 2342*/CREATE OR REPLACE FUNCTION pg_catalog.ts_match_qv(tsquery, tsvector)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_match_qv$function$;/*ts_match_tt 2343*/CREATE OR REPLACE FUNCTION pg_catalog.ts_match_tt(text, text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_match_tt$function$;/*ts_match_tq 2344*/CREATE OR REPLACE FUNCTION pg_catalog.ts_match_tq(text, tsquery)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_match_tq$function$;/*gtsvector_compress 2345*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_compress$function$;/*gtsvector_decompress 2346*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_decompress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_decompress$function$;/*gtsvector_picksplit 2347*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_picksplit$function$;/*gtsvector_union 2348*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_union(internal, internal)
 RETURNS gtsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_union$function$;/*gtsvector_same 2349*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_same(gtsvector, gtsvector, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_same$function$;/*gtsvector_penalty 2350*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_penalty$function$;/*gtsvector_consistent 2351*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_consistent(internal, tsvector, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_consistent$function$;/*gtsvector_consistent 2352*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_consistent(internal, gtsvector, integer, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsvector_consistent_oldsig$function$;/*gtsvector_options 2353*/CREATE OR REPLACE FUNCTION pg_catalog.gtsvector_options(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$gtsvector_options$function$;/*gin_extract_tsvector 2354*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_tsvector(tsvector, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_tsvector$function$;/*gin_extract_tsquery 2355*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_tsquery(tsvector, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_tsquery$function$;/*gin_tsquery_consistent 2356*/CREATE OR REPLACE FUNCTION pg_catalog.gin_tsquery_consistent(internal, smallint, tsvector, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_tsquery_consistent$function$;/*gin_tsquery_triconsistent 2357*/CREATE OR REPLACE FUNCTION pg_catalog.gin_tsquery_triconsistent(internal, smallint, tsvector, integer, internal, internal, internal)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_tsquery_triconsistent$function$;/*gin_cmp_tslexeme 2358*/CREATE OR REPLACE FUNCTION pg_catalog.gin_cmp_tslexeme(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_cmp_tslexeme$function$;/*gin_cmp_prefix 2359*/CREATE OR REPLACE FUNCTION pg_catalog.gin_cmp_prefix(text, text, smallint, internal)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_cmp_prefix$function$;/*gin_extract_tsvector 2360*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_tsvector(tsvector, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_tsvector_2args$function$;/*gin_extract_tsquery 2361*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_tsquery(tsquery, internal, smallint, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_tsquery_5args$function$;/*gin_tsquery_consistent 2362*/CREATE OR REPLACE FUNCTION pg_catalog.gin_tsquery_consistent(internal, smallint, tsquery, integer, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_tsquery_consistent_6args$function$;/*gin_extract_tsquery 2363*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_tsquery(tsquery, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_tsquery_oldsig$function$;/*gin_tsquery_consistent 2364*/CREATE OR REPLACE FUNCTION pg_catalog.gin_tsquery_consistent(internal, smallint, tsquery, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_tsquery_consistent_oldsig$function$;/*gin_clean_pending_list 2365*/CREATE OR REPLACE FUNCTION pg_catalog.gin_clean_pending_list(regclass)
 RETURNS bigint
 LANGUAGE internal
 STRICT
AS $function$gin_clean_pending_list$function$;/*tsquery_lt 2366*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_lt(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_lt$function$;/*tsquery_le 2367*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_le(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_le$function$;/*tsquery_eq 2368*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_eq(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_eq$function$;/*tsquery_ne 2369*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_ne(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_ne$function$;/*tsquery_ge 2370*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_ge(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_ge$function$;/*tsquery_gt 2371*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_gt(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_gt$function$;/*tsquery_cmp 2372*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_cmp(tsquery, tsquery)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_cmp$function$;/*tsquery_and 2373*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_and(tsquery, tsquery)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_and$function$;/*tsquery_or 2374*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_or(tsquery, tsquery)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_or$function$;/*tsquery_phrase 2375*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_phrase(tsquery, tsquery)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_phrase$function$;/*tsquery_phrase 2376*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_phrase(tsquery, tsquery, integer)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_phrase_distance$function$;/*tsquery_not 2377*/CREATE OR REPLACE FUNCTION pg_catalog.tsquery_not(tsquery)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_not$function$;/*tsq_mcontains 2378*/CREATE OR REPLACE FUNCTION pg_catalog.tsq_mcontains(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsq_mcontains$function$;/*tsq_mcontained 2379*/CREATE OR REPLACE FUNCTION pg_catalog.tsq_mcontained(tsquery, tsquery)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsq_mcontained$function$;/*numnode 2380*/CREATE OR REPLACE FUNCTION pg_catalog.numnode(tsquery)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_numnode$function$;/*querytree 2381*/CREATE OR REPLACE FUNCTION pg_catalog.querytree(tsquery)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquerytree$function$;/*ts_rewrite 2382*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rewrite(tsquery, tsquery, tsquery)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsquery_rewrite$function$;/*ts_rewrite 2383*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rewrite(tsquery, text)
 RETURNS tsquery
 LANGUAGE internal
 STRICT COST 100
AS $function$tsquery_rewrite_query$function$;/*gtsquery_compress 2384*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_compress(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_compress$function$;/*gtsquery_picksplit 2385*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_picksplit$function$;/*gtsquery_union 2386*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_union(internal, internal)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_union$function$;/*gtsquery_same 2387*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_same(bigint, bigint, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_same$function$;/*gtsquery_penalty 2388*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_penalty$function$;/*gtsquery_consistent 2389*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_consistent(internal, tsquery, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_consistent$function$;/*gtsquery_consistent 2390*/CREATE OR REPLACE FUNCTION pg_catalog.gtsquery_consistent(internal, internal, integer, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gtsquery_consistent_oldsig$function$;/*tsmatchsel 2391*/CREATE OR REPLACE FUNCTION pg_catalog.tsmatchsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$tsmatchsel$function$;/*tsmatchjoinsel 2392*/CREATE OR REPLACE FUNCTION pg_catalog.tsmatchjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$tsmatchjoinsel$function$;/*ts_typanalyze 2393*/CREATE OR REPLACE FUNCTION pg_catalog.ts_typanalyze(internal)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$ts_typanalyze$function$;/*ts_stat 2394*/CREATE OR REPLACE FUNCTION pg_catalog.ts_stat(query text, OUT word text, OUT ndoc integer, OUT nentry integer)
 RETURNS SETOF record
 LANGUAGE internal
 STRICT COST 10 ROWS 10000
AS $function$ts_stat1$function$;/*ts_stat 2395*/CREATE OR REPLACE FUNCTION pg_catalog.ts_stat(query text, weights text, OUT word text, OUT ndoc integer, OUT nentry integer)
 RETURNS SETOF record
 LANGUAGE internal
 STRICT COST 10 ROWS 10000
AS $function$ts_stat2$function$;/*ts_rank 2396*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank(real[], tsvector, tsquery, integer)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rank_wttf$function$;/*ts_rank 2397*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank(real[], tsvector, tsquery)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rank_wtt$function$;/*ts_rank 2398*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank(tsvector, tsquery, integer)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rank_ttf$function$;/*ts_rank 2399*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank(tsvector, tsquery)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rank_tt$function$;/*ts_rank_cd 2400*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank_cd(real[], tsvector, tsquery, integer)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rankcd_wttf$function$;/*ts_rank_cd 2401*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank_cd(real[], tsvector, tsquery)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rankcd_wtt$function$;/*ts_rank_cd 2402*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank_cd(tsvector, tsquery, integer)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rankcd_ttf$function$;/*ts_rank_cd 2403*/CREATE OR REPLACE FUNCTION pg_catalog.ts_rank_cd(tsvector, tsquery)
 RETURNS real
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_rankcd_tt$function$;/*ts_token_type 2404*/CREATE OR REPLACE FUNCTION pg_catalog.ts_token_type(parser_oid oid, OUT tokid integer, OUT alias text, OUT description text)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 16
AS $function$ts_token_type_byid$function$;/*ts_token_type 2405*/CREATE OR REPLACE FUNCTION pg_catalog.ts_token_type(parser_name text, OUT tokid integer, OUT alias text, OUT description text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT ROWS 16
AS $function$ts_token_type_byname$function$;/*ts_parse 2406*/CREATE OR REPLACE FUNCTION pg_catalog.ts_parse(parser_oid oid, txt text, OUT tokid integer, OUT token text)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_parse_byid$function$;/*ts_parse 2407*/CREATE OR REPLACE FUNCTION pg_catalog.ts_parse(parser_name text, txt text, OUT tokid integer, OUT token text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$ts_parse_byname$function$;/*prsd_start 2408*/CREATE OR REPLACE FUNCTION pg_catalog.prsd_start(internal, integer)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$prsd_start$function$;/*prsd_nexttoken 2409*/CREATE OR REPLACE FUNCTION pg_catalog.prsd_nexttoken(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$prsd_nexttoken$function$;/*prsd_end 2410*/CREATE OR REPLACE FUNCTION pg_catalog.prsd_end(internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$prsd_end$function$;/*prsd_headline 2411*/CREATE OR REPLACE FUNCTION pg_catalog.prsd_headline(internal, internal, tsquery)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$prsd_headline$function$;/*prsd_lextype 2412*/CREATE OR REPLACE FUNCTION pg_catalog.prsd_lextype(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$prsd_lextype$function$;/*ts_lexize 2413*/CREATE OR REPLACE FUNCTION pg_catalog.ts_lexize(regdictionary, text)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$ts_lexize$function$;/*dsimple_init 2414*/CREATE OR REPLACE FUNCTION pg_catalog.dsimple_init(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsimple_init$function$;/*dsimple_lexize 2415*/CREATE OR REPLACE FUNCTION pg_catalog.dsimple_lexize(internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsimple_lexize$function$;/*dsynonym_init 2416*/CREATE OR REPLACE FUNCTION pg_catalog.dsynonym_init(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsynonym_init$function$;/*dsynonym_lexize 2417*/CREATE OR REPLACE FUNCTION pg_catalog.dsynonym_lexize(internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dsynonym_lexize$function$;/*dispell_init 2418*/CREATE OR REPLACE FUNCTION pg_catalog.dispell_init(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dispell_init$function$;/*dispell_lexize 2419*/CREATE OR REPLACE FUNCTION pg_catalog.dispell_lexize(internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$dispell_lexize$function$;/*thesaurus_init 2420*/CREATE OR REPLACE FUNCTION pg_catalog.thesaurus_init(internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$thesaurus_init$function$;/*thesaurus_lexize 2421*/CREATE OR REPLACE FUNCTION pg_catalog.thesaurus_lexize(internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$thesaurus_lexize$function$;/*ts_headline 2422*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, text, tsquery, text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_byid_opt$function$;/*ts_headline 2423*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, text, tsquery)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_byid$function$;/*ts_headline 2424*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(text, tsquery, text)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_opt$function$;/*ts_headline 2425*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(text, tsquery)
 RETURNS text
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline$function$;/*ts_headline 2426*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, jsonb, tsquery, text)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_jsonb_byid_opt$function$;/*ts_headline 2427*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, jsonb, tsquery)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_jsonb_byid$function$;/*ts_headline 2428*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(jsonb, tsquery, text)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_jsonb_opt$function$;/*ts_headline 2429*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(jsonb, tsquery)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_jsonb$function$;/*ts_headline 2430*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, json, tsquery, text)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_json_byid_opt$function$;/*ts_headline 2431*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(regconfig, json, tsquery)
 RETURNS json
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_json_byid$function$;/*ts_headline 2432*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(json, tsquery, text)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_json_opt$function$;/*ts_headline 2433*/CREATE OR REPLACE FUNCTION pg_catalog.ts_headline(json, tsquery)
 RETURNS json
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$ts_headline_json$function$;/*to_tsvector 2434*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(regconfig, text)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$to_tsvector_byid$function$;/*to_tsquery 2435*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsquery(regconfig, text)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$to_tsquery_byid$function$;/*plainto_tsquery 2436*/CREATE OR REPLACE FUNCTION pg_catalog.plainto_tsquery(regconfig, text)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$plainto_tsquery_byid$function$;/*phraseto_tsquery 2437*/CREATE OR REPLACE FUNCTION pg_catalog.phraseto_tsquery(regconfig, text)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$phraseto_tsquery_byid$function$;/*websearch_to_tsquery 2438*/CREATE OR REPLACE FUNCTION pg_catalog.websearch_to_tsquery(regconfig, text)
 RETURNS tsquery
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$websearch_to_tsquery_byid$function$;/*to_tsvector 2439*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(text)
 RETURNS tsvector
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$to_tsvector$function$;/*to_tsquery 2440*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsquery(text)
 RETURNS tsquery
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$to_tsquery$function$;/*plainto_tsquery 2441*/CREATE OR REPLACE FUNCTION pg_catalog.plainto_tsquery(text)
 RETURNS tsquery
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$plainto_tsquery$function$;/*phraseto_tsquery 2442*/CREATE OR REPLACE FUNCTION pg_catalog.phraseto_tsquery(text)
 RETURNS tsquery
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$phraseto_tsquery$function$;/*websearch_to_tsquery 2443*/CREATE OR REPLACE FUNCTION pg_catalog.websearch_to_tsquery(text)
 RETURNS tsquery
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$websearch_to_tsquery$function$;/*to_tsvector 2444*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(jsonb)
 RETURNS tsvector
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$jsonb_string_to_tsvector$function$;/*jsonb_to_tsvector 2445*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_to_tsvector(jsonb, jsonb)
 RETURNS tsvector
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$jsonb_to_tsvector$function$;/*to_tsvector 2446*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(json)
 RETURNS tsvector
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$json_string_to_tsvector$function$;/*json_to_tsvector 2447*/CREATE OR REPLACE FUNCTION pg_catalog.json_to_tsvector(json, jsonb)
 RETURNS tsvector
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 100
AS $function$json_to_tsvector$function$;/*to_tsvector 2448*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(regconfig, jsonb)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$jsonb_string_to_tsvector_byid$function$;/*jsonb_to_tsvector 2449*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_to_tsvector(regconfig, jsonb, jsonb)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$jsonb_to_tsvector_byid$function$;/*to_tsvector 2450*/CREATE OR REPLACE FUNCTION pg_catalog.to_tsvector(regconfig, json)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$json_string_to_tsvector_byid$function$;/*json_to_tsvector 2451*/CREATE OR REPLACE FUNCTION pg_catalog.json_to_tsvector(regconfig, json, jsonb)
 RETURNS tsvector
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT COST 100
AS $function$json_to_tsvector_byid$function$;/*tsvector_update_trigger 2452*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_update_trigger()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE
AS $function$tsvector_update_trigger_byid$function$;/*tsvector_update_trigger_column 2453*/CREATE OR REPLACE FUNCTION pg_catalog.tsvector_update_trigger_column()
 RETURNS trigger
 LANGUAGE internal
 PARALLEL SAFE
AS $function$tsvector_update_trigger_bycolumn$function$;/*get_current_ts_config 2454*/CREATE OR REPLACE FUNCTION pg_catalog.get_current_ts_config()
 RETURNS regconfig
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$get_current_ts_config$function$;/*regconfigin 2455*/CREATE OR REPLACE FUNCTION pg_catalog.regconfigin(cstring)
 RETURNS regconfig
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regconfigin$function$;/*regconfigout 2456*/CREATE OR REPLACE FUNCTION pg_catalog.regconfigout(regconfig)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regconfigout$function$;/*regconfigrecv 2457*/CREATE OR REPLACE FUNCTION pg_catalog.regconfigrecv(internal)
 RETURNS regconfig
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regconfigrecv$function$;/*regconfigsend 2458*/CREATE OR REPLACE FUNCTION pg_catalog.regconfigsend(regconfig)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regconfigsend$function$;/*regdictionaryin 2459*/CREATE OR REPLACE FUNCTION pg_catalog.regdictionaryin(cstring)
 RETURNS regdictionary
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regdictionaryin$function$;/*regdictionaryout 2460*/CREATE OR REPLACE FUNCTION pg_catalog.regdictionaryout(regdictionary)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$regdictionaryout$function$;/*regdictionaryrecv 2461*/CREATE OR REPLACE FUNCTION pg_catalog.regdictionaryrecv(internal)
 RETURNS regdictionary
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regdictionaryrecv$function$;/*regdictionarysend 2462*/CREATE OR REPLACE FUNCTION pg_catalog.regdictionarysend(regdictionary)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$regdictionarysend$function$;/*jsonb_in 2463*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_in(cstring)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_in$function$;/*jsonb_recv 2464*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_recv(internal)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_recv$function$;/*jsonb_out 2465*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_out(jsonb)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_out$function$;/*jsonb_send 2466*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_send(jsonb)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_send$function$;/*jsonb_object 2467*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object(text[])
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_object$function$;/*jsonb_object 2468*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object(text[], text[])
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_object_two_arg$function$;/*to_jsonb 2469*/CREATE OR REPLACE FUNCTION pg_catalog.to_jsonb(anyelement)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$to_jsonb$function$;/*jsonb_agg_transfn 2470*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_agg_transfn(internal, anyelement)
 RETURNS internal
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_agg_transfn$function$;/*jsonb_agg_finalfn 2471*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_agg_finalfn(internal)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_agg_finalfn$function$;/*jsonb_object_agg_transfn 2472*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object_agg_transfn(internal, "any", "any")
 RETURNS internal
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_object_agg_transfn$function$;/*jsonb_object_agg_finalfn 2473*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object_agg_finalfn(internal)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_object_agg_finalfn$function$;/*jsonb_build_array 2474*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_build_array(VARIADIC "any")
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_build_array$function$;/*jsonb_build_array 2475*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_build_array()
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_build_array_noargs$function$;/*jsonb_build_object 2476*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_build_object(VARIADIC "any")
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_build_object$function$;/*jsonb_build_object 2477*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_build_object()
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_build_object_noargs$function$;/*jsonb_strip_nulls 2478*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_strip_nulls(jsonb)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_strip_nulls$function$;/*jsonb_object_field 2479*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object_field(from_json jsonb, field_name text)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_object_field$function$;/*jsonb_object_field_text 2480*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object_field_text(from_json jsonb, field_name text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_object_field_text$function$;/*jsonb_array_element 2481*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_array_element(from_json jsonb, element_index integer)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_array_element$function$;/*jsonb_array_element_text 2482*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_array_element_text(from_json jsonb, element_index integer)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_array_element_text$function$;/*jsonb_extract_path 2483*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_extract_path(from_json jsonb, VARIADIC path_elems text[])
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_extract_path$function$;/*jsonb_extract_path_text 2484*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_extract_path_text(from_json jsonb, VARIADIC path_elems text[])
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_extract_path_text$function$;/*jsonb_array_elements 2485*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_array_elements(from_json jsonb, OUT value jsonb)
 RETURNS SETOF jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$jsonb_array_elements$function$;/*jsonb_array_elements_text 2486*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_array_elements_text(from_json jsonb, OUT value text)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$jsonb_array_elements_text$function$;/*jsonb_array_length 2487*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_array_length(jsonb)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_array_length$function$;/*jsonb_object_keys 2488*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_object_keys(jsonb)
 RETURNS SETOF text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$jsonb_object_keys$function$;/*jsonb_each 2489*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_each(from_json jsonb, OUT key text, OUT value jsonb)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$jsonb_each$function$;/*jsonb_each_text 2490*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_each_text(from_json jsonb, OUT key text, OUT value text)
 RETURNS SETOF record
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 100
AS $function$jsonb_each_text$function$;/*jsonb_populate_record 2491*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_populate_record(anyelement, jsonb)
 RETURNS anyelement
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$jsonb_populate_record$function$;/*jsonb_populate_recordset 2492*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_populate_recordset(anyelement, jsonb)
 RETURNS SETOF anyelement
 LANGUAGE internal
 STABLE PARALLEL SAFE ROWS 100
AS $function$jsonb_populate_recordset$function$;/*jsonb_to_record 2493*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_to_record(jsonb)
 RETURNS record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_to_record$function$;/*jsonb_to_recordset 2494*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_to_recordset(jsonb)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE ROWS 100
AS $function$jsonb_to_recordset$function$;/*jsonb_typeof 2495*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_typeof(jsonb)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_typeof$function$;/*jsonb_ne 2496*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_ne(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_ne$function$;/*jsonb_lt 2497*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_lt(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_lt$function$;/*jsonb_gt 2498*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_gt(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_gt$function$;/*jsonb_le 2499*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_le(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_le$function$;/*jsonb_ge 2500*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_ge(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_ge$function$;/*jsonb_eq 2501*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_eq(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_eq$function$;/*jsonb_cmp 2502*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_cmp(jsonb, jsonb)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_cmp$function$;/*jsonb_hash 2503*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_hash(jsonb)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_hash$function$;/*jsonb_hash_extended 2504*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_hash_extended(jsonb, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_hash_extended$function$;/*jsonb_contains 2505*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_contains(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_contains$function$;/*jsonb_exists 2506*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_exists(jsonb, text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_exists$function$;/*jsonb_exists_any 2507*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_exists_any(jsonb, text[])
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_exists_any$function$;/*jsonb_exists_all 2508*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_exists_all(jsonb, text[])
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_exists_all$function$;/*jsonb_contained 2509*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_contained(jsonb, jsonb)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_contained$function$;/*gin_compare_jsonb 2510*/CREATE OR REPLACE FUNCTION pg_catalog.gin_compare_jsonb(text, text)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_compare_jsonb$function$;/*gin_extract_jsonb 2511*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_jsonb(jsonb, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_jsonb$function$;/*gin_extract_jsonb_query 2512*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_jsonb_query(jsonb, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_jsonb_query$function$;/*gin_consistent_jsonb 2513*/CREATE OR REPLACE FUNCTION pg_catalog.gin_consistent_jsonb(internal, smallint, jsonb, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_consistent_jsonb$function$;/*gin_triconsistent_jsonb 2514*/CREATE OR REPLACE FUNCTION pg_catalog.gin_triconsistent_jsonb(internal, smallint, jsonb, integer, internal, internal, internal)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_triconsistent_jsonb$function$;/*gin_extract_jsonb_path 2515*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_jsonb_path(jsonb, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_jsonb_path$function$;/*gin_extract_jsonb_query_path 2516*/CREATE OR REPLACE FUNCTION pg_catalog.gin_extract_jsonb_query_path(jsonb, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_extract_jsonb_query_path$function$;/*gin_consistent_jsonb_path 2517*/CREATE OR REPLACE FUNCTION pg_catalog.gin_consistent_jsonb_path(internal, smallint, jsonb, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_consistent_jsonb_path$function$;/*gin_triconsistent_jsonb_path 2518*/CREATE OR REPLACE FUNCTION pg_catalog.gin_triconsistent_jsonb_path(internal, smallint, jsonb, integer, internal, internal, internal)
 RETURNS "char"
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$gin_triconsistent_jsonb_path$function$;/*jsonb_concat 2519*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_concat(jsonb, jsonb)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_concat$function$;/*jsonb_delete 2520*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_delete(jsonb, text)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_delete$function$;/*jsonb_delete 2521*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_delete(jsonb, integer)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_delete_idx$function$;/*jsonb_delete 2522*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_delete(from_json jsonb, VARIADIC path_elems text[])
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_delete_array$function$;/*jsonb_delete_path 2523*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_delete_path(jsonb, text[])
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_delete_path$function$;/*similarity_op 2524*/CREATE OR REPLACE FUNCTION public.similarity_op(text, text)
 RETURNS boolean
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$similarity_op$function$;/*word_similarity 2525*/CREATE OR REPLACE FUNCTION public.word_similarity(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$word_similarity$function$;/*jsonb_pretty 2526*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_pretty(jsonb)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_pretty$function$;/*jsonpath_in 2527*/CREATE OR REPLACE FUNCTION pg_catalog.jsonpath_in(cstring)
 RETURNS jsonpath
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonpath_in$function$;/*jsonpath_recv 2528*/CREATE OR REPLACE FUNCTION pg_catalog.jsonpath_recv(internal)
 RETURNS jsonpath
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonpath_recv$function$;/*jsonpath_out 2529*/CREATE OR REPLACE FUNCTION pg_catalog.jsonpath_out(jsonpath)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonpath_out$function$;/*jsonpath_send 2530*/CREATE OR REPLACE FUNCTION pg_catalog.jsonpath_send(jsonpath)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonpath_send$function$;/*word_similarity_op 2531*/CREATE OR REPLACE FUNCTION public.word_similarity_op(text, text)
 RETURNS boolean
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$word_similarity_op$function$;/*jsonb_insert 2532*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_insert(jsonb_in jsonb, path text[], replacement jsonb, insert_after boolean DEFAULT false)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_insert$function$;/*word_similarity_commutator_op 2533*/CREATE OR REPLACE FUNCTION public.word_similarity_commutator_op(text, text)
 RETURNS boolean
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$word_similarity_commutator_op$function$;/*similarity_dist 2534*/CREATE OR REPLACE FUNCTION public.similarity_dist(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$similarity_dist$function$;/*jsonb_path_exists_opr 2535*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_exists_opr(jsonb, jsonpath)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_exists_opr$function$;/*jsonb_path_match_opr 2536*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_match_opr(jsonb, jsonpath)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_match_opr$function$;/*txid_snapshot_in 2537*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_in(cstring)
 RETURNS txid_snapshot
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_in$function$;/*txid_snapshot_out 2538*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_out(txid_snapshot)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_out$function$;/*txid_snapshot_recv 2539*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_recv(internal)
 RETURNS txid_snapshot
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_recv$function$;/*txid_snapshot_send 2540*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_send(txid_snapshot)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_send$function$;/*txid_current 2541*/CREATE OR REPLACE FUNCTION pg_catalog.txid_current()
 RETURNS bigint
 LANGUAGE internal
 STABLE STRICT
AS $function$pg_current_xact_id$function$;/*txid_current_if_assigned 2542*/CREATE OR REPLACE FUNCTION pg_catalog.txid_current_if_assigned()
 RETURNS bigint
 LANGUAGE internal
 STABLE STRICT
AS $function$pg_current_xact_id_if_assigned$function$;/*txid_current_snapshot 2543*/CREATE OR REPLACE FUNCTION pg_catalog.txid_current_snapshot()
 RETURNS txid_snapshot
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_current_snapshot$function$;/*txid_snapshot_xmin 2544*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_xmin(txid_snapshot)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_xmin$function$;/*txid_snapshot_xmax 2545*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_xmax(txid_snapshot)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_xmax$function$;/*txid_snapshot_xip 2546*/CREATE OR REPLACE FUNCTION pg_catalog.txid_snapshot_xip(txid_snapshot)
 RETURNS SETOF bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 50
AS $function$pg_snapshot_xip$function$;/*txid_visible_in_snapshot 2547*/CREATE OR REPLACE FUNCTION pg_catalog.txid_visible_in_snapshot(bigint, txid_snapshot)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_visible_in_snapshot$function$;/*txid_status 2548*/CREATE OR REPLACE FUNCTION pg_catalog.txid_status(bigint)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_xact_status$function$;/*pg_snapshot_in 2549*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_in(cstring)
 RETURNS pg_snapshot
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_in$function$;/*pg_snapshot_out 2550*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_out(pg_snapshot)
 RETURNS cstring
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_out$function$;/*pg_snapshot_recv 2551*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_recv(internal)
 RETURNS pg_snapshot
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_recv$function$;/*pg_snapshot_send 2552*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_send(pg_snapshot)
 RETURNS bytea
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_send$function$;/*pg_current_snapshot 2553*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_snapshot()
 RETURNS pg_snapshot
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_current_snapshot$function$;/*pg_snapshot_xmin 2554*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_xmin(pg_snapshot)
 RETURNS xid8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_xmin$function$;/*pg_snapshot_xmax 2555*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_xmax(pg_snapshot)
 RETURNS xid8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_snapshot_xmax$function$;/*pg_snapshot_xip 2556*/CREATE OR REPLACE FUNCTION pg_catalog.pg_snapshot_xip(pg_snapshot)
 RETURNS SETOF xid8
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT ROWS 50
AS $function$pg_snapshot_xip$function$;/*pg_visible_in_snapshot 2557*/CREATE OR REPLACE FUNCTION pg_catalog.pg_visible_in_snapshot(xid8, pg_snapshot)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_visible_in_snapshot$function$;/*pg_current_xact_id 2558*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_xact_id()
 RETURNS xid8
 LANGUAGE internal
 STABLE STRICT
AS $function$pg_current_xact_id$function$;/*pg_current_xact_id_if_assigned 2559*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_xact_id_if_assigned()
 RETURNS xid8
 LANGUAGE internal
 STABLE STRICT
AS $function$pg_current_xact_id_if_assigned$function$;/*pg_xact_status 2560*/CREATE OR REPLACE FUNCTION pg_catalog.pg_xact_status(xid8)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_xact_status$function$;/*record_eq 2561*/CREATE OR REPLACE FUNCTION pg_catalog.record_eq(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_eq$function$;/*record_ne 2562*/CREATE OR REPLACE FUNCTION pg_catalog.record_ne(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_ne$function$;/*record_lt 2563*/CREATE OR REPLACE FUNCTION pg_catalog.record_lt(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_lt$function$;/*record_gt 2564*/CREATE OR REPLACE FUNCTION pg_catalog.record_gt(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_gt$function$;/*record_le 2565*/CREATE OR REPLACE FUNCTION pg_catalog.record_le(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_le$function$;/*record_ge 2566*/CREATE OR REPLACE FUNCTION pg_catalog.record_ge(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_ge$function$;/*jsonb_path_exists_tz 2567*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_exists_tz(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_exists_tz$function$;/*jsonb_path_query_tz 2568*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query_tz(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS SETOF jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query_tz$function$;/*btrecordcmp 2569*/CREATE OR REPLACE FUNCTION pg_catalog.btrecordcmp(record, record)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btrecordcmp$function$;/*record_image_eq 2570*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_eq(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_eq$function$;/*record_image_ne 2571*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_ne(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_ne$function$;/*record_image_lt 2572*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_lt(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_lt$function$;/*record_image_gt 2573*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_gt(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_gt$function$;/*record_image_le 2574*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_le(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_le$function$;/*record_image_ge 2575*/CREATE OR REPLACE FUNCTION pg_catalog.record_image_ge(record, record)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$record_image_ge$function$;/*btrecordimagecmp 2576*/CREATE OR REPLACE FUNCTION pg_catalog.btrecordimagecmp(record, record)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btrecordimagecmp$function$;/*btequalimage 2577*/CREATE OR REPLACE FUNCTION pg_catalog.btequalimage(oid)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$btequalimage$function$;/*pg_available_extensions 2578*/CREATE OR REPLACE FUNCTION pg_catalog.pg_available_extensions(OUT name name, OUT default_version text, OUT comment text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10 ROWS 100
AS $function$pg_available_extensions$function$;/*pg_available_extension_versions 2579*/CREATE OR REPLACE FUNCTION pg_catalog.pg_available_extension_versions(OUT name name, OUT version text, OUT superuser boolean, OUT trusted boolean, OUT relocatable boolean, OUT schema name, OUT requires name[], OUT comment text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10 ROWS 100
AS $function$pg_available_extension_versions$function$;/*pg_extension_update_paths 2580*/CREATE OR REPLACE FUNCTION pg_catalog.pg_extension_update_paths(name name, OUT source text, OUT target text, OUT path text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT COST 10 ROWS 100
AS $function$pg_extension_update_paths$function$;/*pg_extension_config_dump 2581*/CREATE OR REPLACE FUNCTION pg_catalog.pg_extension_config_dump(regclass, text)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_extension_config_dump$function$;/*row_number 2582*/CREATE OR REPLACE FUNCTION pg_catalog.row_number()
 RETURNS bigint
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE
AS $function$window_row_number$function$;/*rank 2583*/CREATE OR REPLACE FUNCTION pg_catalog.rank()
 RETURNS bigint
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE
AS $function$window_rank$function$;/*dense_rank 2584*/CREATE OR REPLACE FUNCTION pg_catalog.dense_rank()
 RETURNS bigint
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE
AS $function$window_dense_rank$function$;/*percent_rank 2585*/CREATE OR REPLACE FUNCTION pg_catalog.percent_rank()
 RETURNS double precision
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE
AS $function$window_percent_rank$function$;/*cume_dist 2586*/CREATE OR REPLACE FUNCTION pg_catalog.cume_dist()
 RETURNS double precision
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE
AS $function$window_cume_dist$function$;/*ntile 2587*/CREATE OR REPLACE FUNCTION pg_catalog.ntile(integer)
 RETURNS integer
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_ntile$function$;/*lag 2588*/CREATE OR REPLACE FUNCTION pg_catalog.lag(anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lag$function$;/*lag 2589*/CREATE OR REPLACE FUNCTION pg_catalog.lag(anyelement, integer)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lag_with_offset$function$;/*lag 2590*/CREATE OR REPLACE FUNCTION pg_catalog.lag(anyelement, integer, anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lag_with_offset_and_default$function$;/*lead 2591*/CREATE OR REPLACE FUNCTION pg_catalog.lead(anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lead$function$;/*lead 2592*/CREATE OR REPLACE FUNCTION pg_catalog.lead(anyelement, integer)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lead_with_offset$function$;/*lead 2593*/CREATE OR REPLACE FUNCTION pg_catalog.lead(anyelement, integer, anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_lead_with_offset_and_default$function$;/*first_value 2594*/CREATE OR REPLACE FUNCTION pg_catalog.first_value(anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_first_value$function$;/*last_value 2595*/CREATE OR REPLACE FUNCTION pg_catalog.last_value(anyelement)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_last_value$function$;/*nth_value 2596*/CREATE OR REPLACE FUNCTION pg_catalog.nth_value(anyelement, integer)
 RETURNS anyelement
 LANGUAGE internal
 WINDOW IMMUTABLE PARALLEL SAFE STRICT
AS $function$window_nth_value$function$;/*anyrange_in 2597*/CREATE OR REPLACE FUNCTION pg_catalog.anyrange_in(cstring, oid, integer)
 RETURNS anyrange
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyrange_in$function$;/*anyrange_out 2598*/CREATE OR REPLACE FUNCTION pg_catalog.anyrange_out(anyrange)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$anyrange_out$function$;/*range_in 2599*/CREATE OR REPLACE FUNCTION pg_catalog.range_in(cstring, oid, integer)
 RETURNS anyrange
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$range_in$function$;/*range_out 2600*/CREATE OR REPLACE FUNCTION pg_catalog.range_out(anyrange)
 RETURNS cstring
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$range_out$function$;/*range_recv 2601*/CREATE OR REPLACE FUNCTION pg_catalog.range_recv(internal, oid, integer)
 RETURNS anyrange
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$range_recv$function$;/*range_send 2602*/CREATE OR REPLACE FUNCTION pg_catalog.range_send(anyrange)
 RETURNS bytea
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$range_send$function$;/*lower 2603*/CREATE OR REPLACE FUNCTION pg_catalog.lower(anyrange)
 RETURNS anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_lower$function$;/*upper 2604*/CREATE OR REPLACE FUNCTION pg_catalog.upper(anyrange)
 RETURNS anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_upper$function$;/*isempty 2605*/CREATE OR REPLACE FUNCTION pg_catalog.isempty(anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_empty$function$;/*lower_inc 2606*/CREATE OR REPLACE FUNCTION pg_catalog.lower_inc(anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_lower_inc$function$;/*upper_inc 2607*/CREATE OR REPLACE FUNCTION pg_catalog.upper_inc(anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_upper_inc$function$;/*lower_inf 2608*/CREATE OR REPLACE FUNCTION pg_catalog.lower_inf(anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_lower_inf$function$;/*upper_inf 2609*/CREATE OR REPLACE FUNCTION pg_catalog.upper_inf(anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_upper_inf$function$;/*range_eq 2610*/CREATE OR REPLACE FUNCTION pg_catalog.range_eq(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_eq$function$;/*range_ne 2611*/CREATE OR REPLACE FUNCTION pg_catalog.range_ne(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_ne$function$;/*range_overlaps 2612*/CREATE OR REPLACE FUNCTION pg_catalog.range_overlaps(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_overlaps$function$;/*range_contains_elem 2613*/CREATE OR REPLACE FUNCTION pg_catalog.range_contains_elem(anyrange, anyelement)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_contains_elem$function$;/*range_contains 2614*/CREATE OR REPLACE FUNCTION pg_catalog.range_contains(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_contains$function$;/*elem_contained_by_range 2615*/CREATE OR REPLACE FUNCTION pg_catalog.elem_contained_by_range(anyelement, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$elem_contained_by_range$function$;/*range_contained_by 2616*/CREATE OR REPLACE FUNCTION pg_catalog.range_contained_by(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_contained_by$function$;/*range_adjacent 2617*/CREATE OR REPLACE FUNCTION pg_catalog.range_adjacent(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_adjacent$function$;/*range_before 2618*/CREATE OR REPLACE FUNCTION pg_catalog.range_before(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_before$function$;/*range_after 2619*/CREATE OR REPLACE FUNCTION pg_catalog.range_after(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_after$function$;/*range_overleft 2620*/CREATE OR REPLACE FUNCTION pg_catalog.range_overleft(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_overleft$function$;/*range_overright 2621*/CREATE OR REPLACE FUNCTION pg_catalog.range_overright(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_overright$function$;/*range_union 2622*/CREATE OR REPLACE FUNCTION pg_catalog.range_union(anyrange, anyrange)
 RETURNS anyrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_union$function$;/*range_merge 2623*/CREATE OR REPLACE FUNCTION pg_catalog.range_merge(anyrange, anyrange)
 RETURNS anyrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_merge$function$;/*range_intersect 2624*/CREATE OR REPLACE FUNCTION pg_catalog.range_intersect(anyrange, anyrange)
 RETURNS anyrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_intersect$function$;/*range_minus 2625*/CREATE OR REPLACE FUNCTION pg_catalog.range_minus(anyrange, anyrange)
 RETURNS anyrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_minus$function$;/*range_cmp 2626*/CREATE OR REPLACE FUNCTION pg_catalog.range_cmp(anyrange, anyrange)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_cmp$function$;/*range_lt 2627*/CREATE OR REPLACE FUNCTION pg_catalog.range_lt(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_lt$function$;/*range_le 2628*/CREATE OR REPLACE FUNCTION pg_catalog.range_le(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_le$function$;/*range_ge 2629*/CREATE OR REPLACE FUNCTION pg_catalog.range_ge(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_ge$function$;/*range_gt 2630*/CREATE OR REPLACE FUNCTION pg_catalog.range_gt(anyrange, anyrange)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gt$function$;/*range_gist_consistent 2631*/CREATE OR REPLACE FUNCTION pg_catalog.range_gist_consistent(internal, anyrange, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gist_consistent$function$;/*range_gist_union 2632*/CREATE OR REPLACE FUNCTION pg_catalog.range_gist_union(internal, internal)
 RETURNS anyrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gist_union$function$;/*range_gist_penalty 2633*/CREATE OR REPLACE FUNCTION pg_catalog.range_gist_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gist_penalty$function$;/*range_gist_picksplit 2634*/CREATE OR REPLACE FUNCTION pg_catalog.range_gist_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gist_picksplit$function$;/*range_gist_same 2635*/CREATE OR REPLACE FUNCTION pg_catalog.range_gist_same(anyrange, anyrange, internal)
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$range_gist_same$function$;/*hash_range 2636*/CREATE OR REPLACE FUNCTION pg_catalog.hash_range(anyrange)
 RETURNS integer
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_range$function$;/*hash_range_extended 2637*/CREATE OR REPLACE FUNCTION pg_catalog.hash_range_extended(anyrange, bigint)
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$hash_range_extended$function$;/*range_typanalyze 2638*/CREATE OR REPLACE FUNCTION pg_catalog.range_typanalyze(internal)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$range_typanalyze$function$;/*rangesel 2639*/CREATE OR REPLACE FUNCTION pg_catalog.rangesel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$rangesel$function$;/*int4range_canonical 2640*/CREATE OR REPLACE FUNCTION pg_catalog.int4range_canonical(int4range)
 RETURNS int4range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4range_canonical$function$;/*int8range_canonical 2641*/CREATE OR REPLACE FUNCTION pg_catalog.int8range_canonical(int8range)
 RETURNS int8range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8range_canonical$function$;/*daterange_canonical 2642*/CREATE OR REPLACE FUNCTION pg_catalog.daterange_canonical(daterange)
 RETURNS daterange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$daterange_canonical$function$;/*int4range_subdiff 2643*/CREATE OR REPLACE FUNCTION pg_catalog.int4range_subdiff(integer, integer)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int4range_subdiff$function$;/*int8range_subdiff 2644*/CREATE OR REPLACE FUNCTION pg_catalog.int8range_subdiff(bigint, bigint)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$int8range_subdiff$function$;/*numrange_subdiff 2645*/CREATE OR REPLACE FUNCTION pg_catalog.numrange_subdiff(numeric, numeric)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$numrange_subdiff$function$;/*daterange_subdiff 2646*/CREATE OR REPLACE FUNCTION pg_catalog.daterange_subdiff(date, date)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$daterange_subdiff$function$;/*tsrange_subdiff 2647*/CREATE OR REPLACE FUNCTION pg_catalog.tsrange_subdiff(timestamp without time zone, timestamp without time zone)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tsrange_subdiff$function$;/*tstzrange_subdiff 2648*/CREATE OR REPLACE FUNCTION pg_catalog.tstzrange_subdiff(timestamp with time zone, timestamp with time zone)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$tstzrange_subdiff$function$;/*int4range 2649*/CREATE OR REPLACE FUNCTION pg_catalog.int4range(integer, integer)
 RETURNS int4range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*int4range 2650*/CREATE OR REPLACE FUNCTION pg_catalog.int4range(integer, integer, text)
 RETURNS int4range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*numrange 2651*/CREATE OR REPLACE FUNCTION pg_catalog.numrange(numeric, numeric)
 RETURNS numrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*numrange 2652*/CREATE OR REPLACE FUNCTION pg_catalog.numrange(numeric, numeric, text)
 RETURNS numrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*tsrange 2653*/CREATE OR REPLACE FUNCTION pg_catalog.tsrange(timestamp without time zone, timestamp without time zone)
 RETURNS tsrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*tsrange 2654*/CREATE OR REPLACE FUNCTION pg_catalog.tsrange(timestamp without time zone, timestamp without time zone, text)
 RETURNS tsrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*tstzrange 2655*/CREATE OR REPLACE FUNCTION pg_catalog.tstzrange(timestamp with time zone, timestamp with time zone)
 RETURNS tstzrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*tstzrange 2656*/CREATE OR REPLACE FUNCTION pg_catalog.tstzrange(timestamp with time zone, timestamp with time zone, text)
 RETURNS tstzrange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*daterange 2657*/CREATE OR REPLACE FUNCTION pg_catalog.daterange(date, date)
 RETURNS daterange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*daterange 2658*/CREATE OR REPLACE FUNCTION pg_catalog.daterange(date, date, text)
 RETURNS daterange
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*int8range 2659*/CREATE OR REPLACE FUNCTION pg_catalog.int8range(bigint, bigint)
 RETURNS int8range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor2$function$;/*int8range 2660*/CREATE OR REPLACE FUNCTION pg_catalog.int8range(bigint, bigint, text)
 RETURNS int8range
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$range_constructor3$function$;/*make_date 2661*/CREATE OR REPLACE FUNCTION pg_catalog.make_date(year integer, month integer, day integer)
 RETURNS date
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$make_date$function$;/*make_time 2662*/CREATE OR REPLACE FUNCTION pg_catalog.make_time(hour integer, min integer, sec double precision)
 RETURNS time without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$make_time$function$;/*make_timestamp 2663*/CREATE OR REPLACE FUNCTION pg_catalog.make_timestamp(year integer, month integer, mday integer, hour integer, min integer, sec double precision)
 RETURNS timestamp without time zone
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$make_timestamp$function$;/*make_timestamptz 2664*/CREATE OR REPLACE FUNCTION pg_catalog.make_timestamptz(year integer, month integer, mday integer, hour integer, min integer, sec double precision)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$make_timestamptz$function$;/*make_timestamptz 2665*/CREATE OR REPLACE FUNCTION pg_catalog.make_timestamptz(year integer, month integer, mday integer, hour integer, min integer, sec double precision, timezone text)
 RETURNS timestamp with time zone
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$make_timestamptz_at_timezone$function$;/*word_similarity_dist_op 2666*/CREATE OR REPLACE FUNCTION public.word_similarity_dist_op(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$word_similarity_dist_op$function$;/*spg_quad_config 2667*/CREATE OR REPLACE FUNCTION pg_catalog.spg_quad_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_quad_config$function$;/*spg_quad_choose 2668*/CREATE OR REPLACE FUNCTION pg_catalog.spg_quad_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_quad_choose$function$;/*spg_quad_picksplit 2669*/CREATE OR REPLACE FUNCTION pg_catalog.spg_quad_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_quad_picksplit$function$;/*spg_quad_inner_consistent 2670*/CREATE OR REPLACE FUNCTION pg_catalog.spg_quad_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_quad_inner_consistent$function$;/*spg_quad_leaf_consistent 2671*/CREATE OR REPLACE FUNCTION pg_catalog.spg_quad_leaf_consistent(internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_quad_leaf_consistent$function$;/*spg_kd_config 2672*/CREATE OR REPLACE FUNCTION pg_catalog.spg_kd_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_kd_config$function$;/*spg_kd_choose 2673*/CREATE OR REPLACE FUNCTION pg_catalog.spg_kd_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_kd_choose$function$;/*spg_kd_picksplit 2674*/CREATE OR REPLACE FUNCTION pg_catalog.spg_kd_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_kd_picksplit$function$;/*spg_kd_inner_consistent 2675*/CREATE OR REPLACE FUNCTION pg_catalog.spg_kd_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_kd_inner_consistent$function$;/*spg_text_config 2676*/CREATE OR REPLACE FUNCTION pg_catalog.spg_text_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_text_config$function$;/*spg_text_choose 2677*/CREATE OR REPLACE FUNCTION pg_catalog.spg_text_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_text_choose$function$;/*spg_text_picksplit 2678*/CREATE OR REPLACE FUNCTION pg_catalog.spg_text_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_text_picksplit$function$;/*spg_text_inner_consistent 2679*/CREATE OR REPLACE FUNCTION pg_catalog.spg_text_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_text_inner_consistent$function$;/*spg_text_leaf_consistent 2680*/CREATE OR REPLACE FUNCTION pg_catalog.spg_text_leaf_consistent(internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_text_leaf_consistent$function$;/*spg_range_quad_config 2681*/CREATE OR REPLACE FUNCTION pg_catalog.spg_range_quad_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_range_quad_config$function$;/*spg_range_quad_choose 2682*/CREATE OR REPLACE FUNCTION pg_catalog.spg_range_quad_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_range_quad_choose$function$;/*spg_range_quad_picksplit 2683*/CREATE OR REPLACE FUNCTION pg_catalog.spg_range_quad_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_range_quad_picksplit$function$;/*spg_range_quad_inner_consistent 2684*/CREATE OR REPLACE FUNCTION pg_catalog.spg_range_quad_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_range_quad_inner_consistent$function$;/*spg_range_quad_leaf_consistent 2685*/CREATE OR REPLACE FUNCTION pg_catalog.spg_range_quad_leaf_consistent(internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_range_quad_leaf_consistent$function$;/*word_similarity_dist_commutator_op 2686*/CREATE OR REPLACE FUNCTION public.word_similarity_dist_commutator_op(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$word_similarity_dist_commutator_op$function$;/*spg_box_quad_config 2687*/CREATE OR REPLACE FUNCTION pg_catalog.spg_box_quad_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_box_quad_config$function$;/*spg_box_quad_choose 2688*/CREATE OR REPLACE FUNCTION pg_catalog.spg_box_quad_choose(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_box_quad_choose$function$;/*spg_box_quad_picksplit 2689*/CREATE OR REPLACE FUNCTION pg_catalog.spg_box_quad_picksplit(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_box_quad_picksplit$function$;/*spg_box_quad_inner_consistent 2690*/CREATE OR REPLACE FUNCTION pg_catalog.spg_box_quad_inner_consistent(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_box_quad_inner_consistent$function$;/*spg_box_quad_leaf_consistent 2691*/CREATE OR REPLACE FUNCTION pg_catalog.spg_box_quad_leaf_consistent(internal, internal)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_box_quad_leaf_consistent$function$;/*spg_bbox_quad_config 2692*/CREATE OR REPLACE FUNCTION pg_catalog.spg_bbox_quad_config(internal, internal)
 RETURNS void
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_bbox_quad_config$function$;/*spg_poly_quad_compress 2693*/CREATE OR REPLACE FUNCTION pg_catalog.spg_poly_quad_compress(polygon)
 RETURNS box
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$spg_poly_quad_compress$function$;/*gtrgm_in 2694*/CREATE OR REPLACE FUNCTION public.gtrgm_in(cstring)
 RETURNS gtrgm
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_in$function$;/*pg_copy_physical_replication_slot 2695*/CREATE OR REPLACE FUNCTION pg_catalog.pg_copy_physical_replication_slot(src_slot_name name, dst_slot_name name, temporary boolean, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_copy_physical_replication_slot_a$function$;/*pg_copy_physical_replication_slot 2696*/CREATE OR REPLACE FUNCTION pg_catalog.pg_copy_physical_replication_slot(src_slot_name name, dst_slot_name name, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_copy_physical_replication_slot_b$function$;/*pg_drop_replication_slot 2697*/CREATE OR REPLACE FUNCTION pg_catalog.pg_drop_replication_slot(name)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_drop_replication_slot$function$;/*pg_get_replication_slots 2698*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_replication_slots(OUT slot_name name, OUT plugin name, OUT slot_type text, OUT datoid oid, OUT temporary boolean, OUT active boolean, OUT active_pid integer, OUT xmin xid, OUT catalog_xmin xid, OUT restart_lsn pg_lsn, OUT confirmed_flush_lsn pg_lsn, OUT wal_status text, OUT safe_wal_size bigint)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL SAFE ROWS 10
AS $function$pg_get_replication_slots$function$;/*pg_copy_logical_replication_slot 2699*/CREATE OR REPLACE FUNCTION pg_catalog.pg_copy_logical_replication_slot(src_slot_name name, dst_slot_name name, temporary boolean, plugin name, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_copy_logical_replication_slot_a$function$;/*pg_copy_logical_replication_slot 2700*/CREATE OR REPLACE FUNCTION pg_catalog.pg_copy_logical_replication_slot(src_slot_name name, dst_slot_name name, temporary boolean, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_copy_logical_replication_slot_b$function$;/*pg_copy_logical_replication_slot 2701*/CREATE OR REPLACE FUNCTION pg_catalog.pg_copy_logical_replication_slot(src_slot_name name, dst_slot_name name, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_copy_logical_replication_slot_c$function$;/*gtrgm_out 2702*/CREATE OR REPLACE FUNCTION public.gtrgm_out(gtrgm)
 RETURNS cstring
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_out$function$;/*gtrgm_consistent 2703*/CREATE OR REPLACE FUNCTION public.gtrgm_consistent(internal, text, smallint, oid, internal)
 RETURNS boolean
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_consistent$function$;/*pg_replication_slot_advance 2704*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_slot_advance(slot_name name, upto_lsn pg_lsn, OUT slot_name name, OUT end_lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_replication_slot_advance$function$;/*pg_logical_emit_message 2705*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_emit_message(boolean, text, text)
 RETURNS pg_lsn
 LANGUAGE internal
 STRICT
AS $function$pg_logical_emit_message_text$function$;/*pg_logical_emit_message 2706*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_emit_message(boolean, text, bytea)
 RETURNS pg_lsn
 LANGUAGE internal
 STRICT
AS $function$pg_logical_emit_message_bytea$function$;/*pg_event_trigger_dropped_objects 2707*/CREATE OR REPLACE FUNCTION pg_catalog.pg_event_trigger_dropped_objects(OUT classid oid, OUT objid oid, OUT objsubid integer, OUT original boolean, OUT normal boolean, OUT is_temporary boolean, OUT object_type text, OUT schema_name text, OUT object_name text, OUT object_identity text, OUT address_names text[], OUT address_args text[])
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 10 ROWS 100
AS $function$pg_event_trigger_dropped_objects$function$;/*pg_event_trigger_table_rewrite_oid 2708*/CREATE OR REPLACE FUNCTION pg_catalog.pg_event_trigger_table_rewrite_oid(OUT oid oid)
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_event_trigger_table_rewrite_oid$function$;/*pg_logical_slot_get_binary_changes 2709*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_slot_get_binary_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, VARIADIC options text[] DEFAULT '{}'::text[], OUT lsn pg_lsn, OUT xid xid, OUT data bytea)
 RETURNS SETOF record
 LANGUAGE internal
 COST 1000
AS $function$pg_logical_slot_get_binary_changes$function$;/*pg_logical_slot_peek_binary_changes 2710*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_slot_peek_binary_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, VARIADIC options text[] DEFAULT '{}'::text[], OUT lsn pg_lsn, OUT xid xid, OUT data bytea)
 RETURNS SETOF record
 LANGUAGE internal
 COST 1000
AS $function$pg_logical_slot_peek_binary_changes$function$;/*pg_create_logical_replication_slot 2711*/CREATE OR REPLACE FUNCTION pg_catalog.pg_create_logical_replication_slot(slot_name name, plugin name, temporary boolean DEFAULT false, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_create_logical_replication_slot$function$;/*pg_event_trigger_table_rewrite_reason 2712*/CREATE OR REPLACE FUNCTION pg_catalog.pg_event_trigger_table_rewrite_reason()
 RETURNS integer
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT
AS $function$pg_event_trigger_table_rewrite_reason$function$;/*pg_event_trigger_ddl_commands 2713*/CREATE OR REPLACE FUNCTION pg_catalog.pg_event_trigger_ddl_commands(OUT classid oid, OUT objid oid, OUT objsubid integer, OUT command_tag text, OUT object_type text, OUT schema_name text, OUT object_identity text, OUT in_extension boolean, OUT command pg_ddl_command)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT COST 10 ROWS 100
AS $function$pg_event_trigger_ddl_commands$function$;/*ordered_set_transition 2714*/CREATE OR REPLACE FUNCTION pg_catalog.ordered_set_transition(internal, "any")
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$ordered_set_transition$function$;/*ordered_set_transition_multi 2715*/CREATE OR REPLACE FUNCTION pg_catalog.ordered_set_transition_multi(internal, VARIADIC "any")
 RETURNS internal
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$ordered_set_transition_multi$function$;/*percentile_disc_final 2716*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_disc_final(internal, double precision, anyelement)
 RETURNS anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_disc_final$function$;/*percentile_cont_float8_final 2717*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_cont_float8_final(internal, double precision)
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_cont_float8_final$function$;/*percentile_cont_interval_final 2718*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_cont_interval_final(internal, double precision)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_cont_interval_final$function$;/*percentile_disc_multi_final 2719*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_disc_multi_final(internal, double precision[], anyelement)
 RETURNS anyarray
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_disc_multi_final$function$;/*percentile_cont_float8_multi_final 2720*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_cont_float8_multi_final(internal, double precision[])
 RETURNS double precision[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_cont_float8_multi_final$function$;/*percentile_cont_interval_multi_final 2721*/CREATE OR REPLACE FUNCTION pg_catalog.percentile_cont_interval_multi_final(internal, double precision[])
 RETURNS interval[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$percentile_cont_interval_multi_final$function$;/*mode_final 2722*/CREATE OR REPLACE FUNCTION pg_catalog.mode_final(internal, anyelement)
 RETURNS anyelement
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$mode_final$function$;/*rank_final 2723*/CREATE OR REPLACE FUNCTION pg_catalog.rank_final(internal, VARIADIC "any")
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$hypothetical_rank_final$function$;/*percent_rank_final 2724*/CREATE OR REPLACE FUNCTION pg_catalog.percent_rank_final(internal, VARIADIC "any")
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$hypothetical_percent_rank_final$function$;/*cume_dist_final 2725*/CREATE OR REPLACE FUNCTION pg_catalog.cume_dist_final(internal, VARIADIC "any")
 RETURNS double precision
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$hypothetical_cume_dist_final$function$;/*dense_rank_final 2726*/CREATE OR REPLACE FUNCTION pg_catalog.dense_rank_final(internal, VARIADIC "any")
 RETURNS bigint
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$hypothetical_dense_rank_final$function$;/*binary_upgrade_set_next_pg_type_oid 2727*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_pg_type_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_pg_type_oid$function$;/*binary_upgrade_set_next_array_pg_type_oid 2728*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_array_pg_type_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_array_pg_type_oid$function$;/*binary_upgrade_set_next_toast_pg_type_oid 2729*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_toast_pg_type_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_toast_pg_type_oid$function$;/*binary_upgrade_set_next_heap_pg_class_oid 2730*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_heap_pg_class_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_heap_pg_class_oid$function$;/*binary_upgrade_set_next_index_pg_class_oid 2731*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_index_pg_class_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_index_pg_class_oid$function$;/*binary_upgrade_set_next_toast_pg_class_oid 2732*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_toast_pg_class_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_toast_pg_class_oid$function$;/*binary_upgrade_set_next_pg_enum_oid 2733*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_pg_enum_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_pg_enum_oid$function$;/*binary_upgrade_set_next_pg_authid_oid 2734*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_next_pg_authid_oid(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_next_pg_authid_oid$function$;/*binary_upgrade_create_empty_extension 2735*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_create_empty_extension(text, text, boolean, text, oid[], text[], text[])
 RETURNS void
 LANGUAGE internal
AS $function$binary_upgrade_create_empty_extension$function$;/*binary_upgrade_set_record_init_privs 2736*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_record_init_privs(boolean)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$binary_upgrade_set_record_init_privs$function$;/*binary_upgrade_set_missing_value 2737*/CREATE OR REPLACE FUNCTION pg_catalog.binary_upgrade_set_missing_value(oid, text, text)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$binary_upgrade_set_missing_value$function$;/*koi8r_to_mic 2738*/CREATE OR REPLACE FUNCTION pg_catalog.koi8r_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$koi8r_to_mic$function$;/*mic_to_koi8r 2739*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_koi8r(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$mic_to_koi8r$function$;/*iso_to_mic 2740*/CREATE OR REPLACE FUNCTION pg_catalog.iso_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$iso_to_mic$function$;/*mic_to_iso 2741*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_iso(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$mic_to_iso$function$;/*win1251_to_mic 2742*/CREATE OR REPLACE FUNCTION pg_catalog.win1251_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win1251_to_mic$function$;/*mic_to_win1251 2743*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_win1251(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$mic_to_win1251$function$;/*win866_to_mic 2744*/CREATE OR REPLACE FUNCTION pg_catalog.win866_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win866_to_mic$function$;/*mic_to_win866 2745*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_win866(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$mic_to_win866$function$;/*koi8r_to_win1251 2746*/CREATE OR REPLACE FUNCTION pg_catalog.koi8r_to_win1251(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$koi8r_to_win1251$function$;/*win1251_to_koi8r 2747*/CREATE OR REPLACE FUNCTION pg_catalog.win1251_to_koi8r(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win1251_to_koi8r$function$;/*koi8r_to_win866 2748*/CREATE OR REPLACE FUNCTION pg_catalog.koi8r_to_win866(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$koi8r_to_win866$function$;/*win866_to_koi8r 2749*/CREATE OR REPLACE FUNCTION pg_catalog.win866_to_koi8r(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win866_to_koi8r$function$;/*win866_to_win1251 2750*/CREATE OR REPLACE FUNCTION pg_catalog.win866_to_win1251(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win866_to_win1251$function$;/*win1251_to_win866 2751*/CREATE OR REPLACE FUNCTION pg_catalog.win1251_to_win866(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win1251_to_win866$function$;/*iso_to_koi8r 2752*/CREATE OR REPLACE FUNCTION pg_catalog.iso_to_koi8r(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$iso_to_koi8r$function$;/*koi8r_to_iso 2753*/CREATE OR REPLACE FUNCTION pg_catalog.koi8r_to_iso(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$koi8r_to_iso$function$;/*iso_to_win1251 2754*/CREATE OR REPLACE FUNCTION pg_catalog.iso_to_win1251(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$iso_to_win1251$function$;/*win1251_to_iso 2755*/CREATE OR REPLACE FUNCTION pg_catalog.win1251_to_iso(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win1251_to_iso$function$;/*iso_to_win866 2756*/CREATE OR REPLACE FUNCTION pg_catalog.iso_to_win866(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$iso_to_win866$function$;/*win866_to_iso 2757*/CREATE OR REPLACE FUNCTION pg_catalog.win866_to_iso(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/cyrillic_and_mic', $function$win866_to_iso$function$;/*euc_cn_to_mic 2758*/CREATE OR REPLACE FUNCTION pg_catalog.euc_cn_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_cn_and_mic', $function$euc_cn_to_mic$function$;/*mic_to_euc_cn 2759*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_euc_cn(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_cn_and_mic', $function$mic_to_euc_cn$function$;/*euc_jp_to_sjis 2760*/CREATE OR REPLACE FUNCTION pg_catalog.euc_jp_to_sjis(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$euc_jp_to_sjis$function$;/*sjis_to_euc_jp 2761*/CREATE OR REPLACE FUNCTION pg_catalog.sjis_to_euc_jp(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$sjis_to_euc_jp$function$;/*euc_jp_to_mic 2762*/CREATE OR REPLACE FUNCTION pg_catalog.euc_jp_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$euc_jp_to_mic$function$;/*sjis_to_mic 2763*/CREATE OR REPLACE FUNCTION pg_catalog.sjis_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$sjis_to_mic$function$;/*mic_to_euc_jp 2764*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_euc_jp(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$mic_to_euc_jp$function$;/*mic_to_sjis 2765*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_sjis(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_jp_and_sjis', $function$mic_to_sjis$function$;/*euc_kr_to_mic 2766*/CREATE OR REPLACE FUNCTION pg_catalog.euc_kr_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_kr_and_mic', $function$euc_kr_to_mic$function$;/*mic_to_euc_kr 2767*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_euc_kr(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_kr_and_mic', $function$mic_to_euc_kr$function$;/*euc_tw_to_big5 2768*/CREATE OR REPLACE FUNCTION pg_catalog.euc_tw_to_big5(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$euc_tw_to_big5$function$;/*big5_to_euc_tw 2769*/CREATE OR REPLACE FUNCTION pg_catalog.big5_to_euc_tw(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$big5_to_euc_tw$function$;/*euc_tw_to_mic 2770*/CREATE OR REPLACE FUNCTION pg_catalog.euc_tw_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$euc_tw_to_mic$function$;/*big5_to_mic 2771*/CREATE OR REPLACE FUNCTION pg_catalog.big5_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$big5_to_mic$function$;/*mic_to_euc_tw 2772*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_euc_tw(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$mic_to_euc_tw$function$;/*mic_to_big5 2773*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_big5(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc_tw_and_big5', $function$mic_to_big5$function$;/*latin2_to_mic 2774*/CREATE OR REPLACE FUNCTION pg_catalog.latin2_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$latin2_to_mic$function$;/*mic_to_latin2 2775*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_latin2(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$mic_to_latin2$function$;/*win1250_to_mic 2776*/CREATE OR REPLACE FUNCTION pg_catalog.win1250_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$win1250_to_mic$function$;/*mic_to_win1250 2777*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_win1250(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$mic_to_win1250$function$;/*latin2_to_win1250 2778*/CREATE OR REPLACE FUNCTION pg_catalog.latin2_to_win1250(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$latin2_to_win1250$function$;/*win1250_to_latin2 2779*/CREATE OR REPLACE FUNCTION pg_catalog.win1250_to_latin2(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin2_and_win1250', $function$win1250_to_latin2$function$;/*latin1_to_mic 2780*/CREATE OR REPLACE FUNCTION pg_catalog.latin1_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$latin1_to_mic$function$;/*mic_to_latin1 2781*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_latin1(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$mic_to_latin1$function$;/*latin3_to_mic 2782*/CREATE OR REPLACE FUNCTION pg_catalog.latin3_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$latin3_to_mic$function$;/*mic_to_latin3 2783*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_latin3(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$mic_to_latin3$function$;/*latin4_to_mic 2784*/CREATE OR REPLACE FUNCTION pg_catalog.latin4_to_mic(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$latin4_to_mic$function$;/*mic_to_latin4 2785*/CREATE OR REPLACE FUNCTION pg_catalog.mic_to_latin4(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/latin_and_mic', $function$mic_to_latin4$function$;/*big5_to_utf8 2786*/CREATE OR REPLACE FUNCTION pg_catalog.big5_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_big5', $function$big5_to_utf8$function$;/*utf8_to_big5 2787*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_big5(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_big5', $function$utf8_to_big5$function$;/*utf8_to_koi8r 2788*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_koi8r(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_cyrillic', $function$utf8_to_koi8r$function$;/*koi8r_to_utf8 2789*/CREATE OR REPLACE FUNCTION pg_catalog.koi8r_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_cyrillic', $function$koi8r_to_utf8$function$;/*utf8_to_koi8u 2790*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_koi8u(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_cyrillic', $function$utf8_to_koi8u$function$;/*koi8u_to_utf8 2791*/CREATE OR REPLACE FUNCTION pg_catalog.koi8u_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_cyrillic', $function$koi8u_to_utf8$function$;/*utf8_to_win 2792*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_win(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_win', $function$utf8_to_win$function$;/*win_to_utf8 2793*/CREATE OR REPLACE FUNCTION pg_catalog.win_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_win', $function$win_to_utf8$function$;/*euc_cn_to_utf8 2794*/CREATE OR REPLACE FUNCTION pg_catalog.euc_cn_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_cn', $function$euc_cn_to_utf8$function$;/*utf8_to_euc_cn 2795*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_euc_cn(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_cn', $function$utf8_to_euc_cn$function$;/*euc_jp_to_utf8 2796*/CREATE OR REPLACE FUNCTION pg_catalog.euc_jp_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_jp', $function$euc_jp_to_utf8$function$;/*utf8_to_euc_jp 2797*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_euc_jp(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_jp', $function$utf8_to_euc_jp$function$;/*euc_kr_to_utf8 2798*/CREATE OR REPLACE FUNCTION pg_catalog.euc_kr_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_kr', $function$euc_kr_to_utf8$function$;/*utf8_to_euc_kr 2799*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_euc_kr(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_kr', $function$utf8_to_euc_kr$function$;/*euc_tw_to_utf8 2800*/CREATE OR REPLACE FUNCTION pg_catalog.euc_tw_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_tw', $function$euc_tw_to_utf8$function$;/*utf8_to_euc_tw 2801*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_euc_tw(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc_tw', $function$utf8_to_euc_tw$function$;/*gb18030_to_utf8 2802*/CREATE OR REPLACE FUNCTION pg_catalog.gb18030_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_gb18030', $function$gb18030_to_utf8$function$;/*utf8_to_gb18030 2803*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_gb18030(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_gb18030', $function$utf8_to_gb18030$function$;/*gbk_to_utf8 2804*/CREATE OR REPLACE FUNCTION pg_catalog.gbk_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_gbk', $function$gbk_to_utf8$function$;/*utf8_to_gbk 2805*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_gbk(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_gbk', $function$utf8_to_gbk$function$;/*utf8_to_iso8859 2806*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_iso8859(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_iso8859', $function$utf8_to_iso8859$function$;/*iso8859_to_utf8 2807*/CREATE OR REPLACE FUNCTION pg_catalog.iso8859_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_iso8859', $function$iso8859_to_utf8$function$;/*iso8859_1_to_utf8 2808*/CREATE OR REPLACE FUNCTION pg_catalog.iso8859_1_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_iso8859_1', $function$iso8859_1_to_utf8$function$;/*utf8_to_iso8859_1 2809*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_iso8859_1(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_iso8859_1', $function$utf8_to_iso8859_1$function$;/*johab_to_utf8 2810*/CREATE OR REPLACE FUNCTION pg_catalog.johab_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_johab', $function$johab_to_utf8$function$;/*utf8_to_johab 2811*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_johab(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_johab', $function$utf8_to_johab$function$;/*sjis_to_utf8 2812*/CREATE OR REPLACE FUNCTION pg_catalog.sjis_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_sjis', $function$sjis_to_utf8$function$;/*utf8_to_sjis 2813*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_sjis(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_sjis', $function$utf8_to_sjis$function$;/*uhc_to_utf8 2814*/CREATE OR REPLACE FUNCTION pg_catalog.uhc_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_uhc', $function$uhc_to_utf8$function$;/*utf8_to_uhc 2815*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_uhc(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_uhc', $function$utf8_to_uhc$function$;/*euc_jis_2004_to_utf8 2816*/CREATE OR REPLACE FUNCTION pg_catalog.euc_jis_2004_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc2004', $function$euc_jis_2004_to_utf8$function$;/*utf8_to_euc_jis_2004 2817*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_euc_jis_2004(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_euc2004', $function$utf8_to_euc_jis_2004$function$;/*shift_jis_2004_to_utf8 2818*/CREATE OR REPLACE FUNCTION pg_catalog.shift_jis_2004_to_utf8(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_sjis2004', $function$shift_jis_2004_to_utf8$function$;/*utf8_to_shift_jis_2004 2819*/CREATE OR REPLACE FUNCTION pg_catalog.utf8_to_shift_jis_2004(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/utf8_and_sjis2004', $function$utf8_to_shift_jis_2004$function$;/*euc_jis_2004_to_shift_jis_2004 2820*/CREATE OR REPLACE FUNCTION pg_catalog.euc_jis_2004_to_shift_jis_2004(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc2004_sjis2004', $function$euc_jis_2004_to_shift_jis_2004$function$;/*shift_jis_2004_to_euc_jis_2004 2821*/CREATE OR REPLACE FUNCTION pg_catalog.shift_jis_2004_to_euc_jis_2004(integer, integer, cstring, internal, integer)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/euc2004_sjis2004', $function$shift_jis_2004_to_euc_jis_2004$function$;/*matchingsel 2822*/CREATE OR REPLACE FUNCTION pg_catalog.matchingsel(internal, oid, internal, integer)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$matchingsel$function$;/*matchingjoinsel 2823*/CREATE OR REPLACE FUNCTION pg_catalog.matchingjoinsel(internal, oid, internal, smallint, internal)
 RETURNS double precision
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$matchingjoinsel$function$;/*pg_replication_origin_create 2824*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_create(text)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_create$function$;/*pg_replication_origin_drop 2825*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_drop(text)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_drop$function$;/*pg_replication_origin_oid 2826*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_oid(text)
 RETURNS oid
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_replication_origin_oid$function$;/*pg_replication_origin_session_setup 2827*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_session_setup(text)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_session_setup$function$;/*pg_replication_origin_session_reset 2828*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_session_reset()
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_session_reset$function$;/*pg_replication_origin_session_is_setup 2829*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_session_is_setup()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_replication_origin_session_is_setup$function$;/*pg_replication_origin_session_progress 2830*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_session_progress(boolean)
 RETURNS pg_lsn
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_session_progress$function$;/*pg_replication_origin_xact_setup 2831*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_xact_setup(pg_lsn, timestamp with time zone)
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_replication_origin_xact_setup$function$;/*pg_replication_origin_xact_reset 2832*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_xact_reset()
 RETURNS void
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_replication_origin_xact_reset$function$;/*pg_replication_origin_advance 2833*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_advance(text, pg_lsn)
 RETURNS void
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_advance$function$;/*pg_replication_origin_progress 2834*/CREATE OR REPLACE FUNCTION pg_catalog.pg_replication_origin_progress(text, boolean)
 RETURNS pg_lsn
 LANGUAGE internal
 STRICT
AS $function$pg_replication_origin_progress$function$;/*pg_show_replication_origin_status 2835*/CREATE OR REPLACE FUNCTION pg_catalog.pg_show_replication_origin_status(OUT local_id oid, OUT external_id text, OUT remote_lsn pg_lsn, OUT local_lsn pg_lsn)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL RESTRICTED ROWS 100
AS $function$pg_show_replication_origin_status$function$;/*pg_get_publication_tables 2836*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_publication_tables(pubname text, OUT relid oid)
 RETURNS SETOF oid
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_get_publication_tables$function$;/*pg_relation_is_publishable 2837*/CREATE OR REPLACE FUNCTION pg_catalog.pg_relation_is_publishable(regclass)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$pg_relation_is_publishable$function$;/*row_security_active 2838*/CREATE OR REPLACE FUNCTION pg_catalog.row_security_active(oid)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$row_security_active$function$;/*row_security_active 2839*/CREATE OR REPLACE FUNCTION pg_catalog.row_security_active(text)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$row_security_active_name$function$;/*pg_control_system 2840*/CREATE OR REPLACE FUNCTION pg_catalog.pg_control_system(OUT pg_control_version integer, OUT catalog_version_no integer, OUT system_identifier bigint, OUT pg_control_last_modified timestamp with time zone)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_control_system$function$;/*pg_control_checkpoint 2841*/CREATE OR REPLACE FUNCTION pg_catalog.pg_control_checkpoint(OUT checkpoint_lsn pg_lsn, OUT redo_lsn pg_lsn, OUT redo_wal_file text, OUT timeline_id integer, OUT prev_timeline_id integer, OUT full_page_writes boolean, OUT next_xid text, OUT next_oid oid, OUT next_multixact_id xid, OUT next_multi_offset xid, OUT oldest_xid xid, OUT oldest_xid_dbid oid, OUT oldest_active_xid xid, OUT oldest_multi_xid xid, OUT oldest_multi_dbid oid, OUT oldest_commit_ts_xid xid, OUT newest_commit_ts_xid xid, OUT checkpoint_time timestamp with time zone)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_control_checkpoint$function$;/*pg_control_recovery 2842*/CREATE OR REPLACE FUNCTION pg_catalog.pg_control_recovery(OUT min_recovery_end_lsn pg_lsn, OUT min_recovery_end_timeline integer, OUT backup_start_lsn pg_lsn, OUT backup_end_lsn pg_lsn, OUT end_of_backup_record_required boolean)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_control_recovery$function$;/*pg_control_init 2843*/CREATE OR REPLACE FUNCTION pg_catalog.pg_control_init(OUT max_data_alignment integer, OUT database_block_size integer, OUT blocks_per_segment integer, OUT wal_block_size integer, OUT bytes_per_wal_segment integer, OUT max_identifier_length integer, OUT max_index_columns integer, OUT max_toast_chunk_size integer, OUT large_object_chunk_size integer, OUT float8_pass_by_value boolean, OUT data_page_checksum_version integer)
 RETURNS record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_control_init$function$;/*pg_import_system_collations 2844*/CREATE OR REPLACE FUNCTION pg_catalog.pg_import_system_collations(regnamespace)
 RETURNS integer
 LANGUAGE internal
 STRICT COST 100
AS $function$pg_import_system_collations$function$;/*pg_collation_actual_version 2845*/CREATE OR REPLACE FUNCTION pg_catalog.pg_collation_actual_version(oid)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 100
AS $function$pg_collation_actual_version$function$;/*gtrgm_distance 2846*/CREATE OR REPLACE FUNCTION public.gtrgm_distance(internal, text, smallint, oid, internal)
 RETURNS double precision
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_distance$function$;/*gtrgm_compress 2847*/CREATE OR REPLACE FUNCTION public.gtrgm_compress(internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_compress$function$;/*gtrgm_decompress 2848*/CREATE OR REPLACE FUNCTION public.gtrgm_decompress(internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_decompress$function$;/*gtrgm_penalty 2849*/CREATE OR REPLACE FUNCTION public.gtrgm_penalty(internal, internal, internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_penalty$function$;/*gtrgm_picksplit 2850*/CREATE OR REPLACE FUNCTION public.gtrgm_picksplit(internal, internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_picksplit$function$;/*satisfies_hash_partition 2851*/CREATE OR REPLACE FUNCTION pg_catalog.satisfies_hash_partition(oid, integer, integer, VARIADIC "any")
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$satisfies_hash_partition$function$;/*pg_partition_tree 2852*/CREATE OR REPLACE FUNCTION pg_catalog.pg_partition_tree(rootrelid regclass, OUT relid regclass, OUT parentrelid regclass, OUT isleaf boolean, OUT level integer)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_partition_tree$function$;/*pg_partition_ancestors 2853*/CREATE OR REPLACE FUNCTION pg_catalog.pg_partition_ancestors(partitionid regclass, OUT relid regclass)
 RETURNS SETOF regclass
 LANGUAGE internal
 PARALLEL SAFE STRICT ROWS 10
AS $function$pg_partition_ancestors$function$;/*pg_partition_root 2854*/CREATE OR REPLACE FUNCTION pg_catalog.pg_partition_root(regclass)
 RETURNS regclass
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$pg_partition_root$function$;/*pg_show_all_file_settings 2855*/CREATE OR REPLACE FUNCTION pg_catalog.pg_show_all_file_settings(OUT sourcefile text, OUT sourceline integer, OUT seqno integer, OUT name text, OUT setting text, OUT applied boolean, OUT error text)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$show_all_file_settings$function$;/*pg_hba_file_rules 2856*/CREATE OR REPLACE FUNCTION pg_catalog.pg_hba_file_rules(OUT line_number integer, OUT type text, OUT database text[], OUT user_name text[], OUT address text, OUT netmask text, OUT auth_method text, OUT options text[], OUT error text)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_hba_file_rules$function$;/*pg_config 2857*/CREATE OR REPLACE FUNCTION pg_catalog.pg_config(OUT name text, OUT setting text)
 RETURNS SETOF record
 LANGUAGE internal
 STABLE PARALLEL RESTRICTED STRICT ROWS 23
AS $function$pg_config$function$;/*pg_get_shmem_allocations 2858*/CREATE OR REPLACE FUNCTION pg_catalog.pg_get_shmem_allocations(OUT name text, OUT off bigint, OUT size bigint, OUT allocated_size bigint)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT ROWS 50
AS $function$pg_get_shmem_allocations$function$;/*normalize 2859*/CREATE OR REPLACE FUNCTION pg_catalog."normalize"(text, text DEFAULT 'NFC'::text)
 RETURNS text
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unicode_normalize_func$function$;/*is_normalized 2860*/CREATE OR REPLACE FUNCTION pg_catalog.is_normalized(text, text DEFAULT 'NFC'::text)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$unicode_is_normalized$function$;/*pg_reload_conf 2861*/CREATE OR REPLACE FUNCTION pg_catalog.pg_reload_conf()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_reload_conf$function$;/*gtrgm_union 2862*/CREATE OR REPLACE FUNCTION public.gtrgm_union(internal, internal)
 RETURNS gtrgm
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_union$function$;/*gtrgm_same 2863*/CREATE OR REPLACE FUNCTION public.gtrgm_same(gtrgm, gtrgm, internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gtrgm_same$function$;/*gin_extract_value_trgm 2864*/CREATE OR REPLACE FUNCTION public.gin_extract_value_trgm(text, internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gin_extract_value_trgm$function$;/*ts_debug 2865*/CREATE OR REPLACE FUNCTION pg_catalog.ts_debug(config regconfig, document text, OUT alias text, OUT description text, OUT token text, OUT dictionaries regdictionary[], OUT dictionary regdictionary, OUT lexemes text[])
 RETURNS SETOF record
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$
SELECT
    tt.alias AS alias,
    tt.description AS description,
    parse.token AS token,
    ARRAY ( SELECT m.mapdict::pg_catalog.regdictionary
            FROM pg_catalog.pg_ts_config_map AS m
            WHERE m.mapcfg = $1 AND m.maptokentype = parse.tokid
            ORDER BY m.mapseqno )
    AS dictionaries,
    ( SELECT mapdict::pg_catalog.regdictionary
      FROM pg_catalog.pg_ts_config_map AS m
      WHERE m.mapcfg = $1 AND m.maptokentype = parse.tokid
      ORDER BY pg_catalog.ts_lexize(mapdict, parse.token) IS NULL, m.mapseqno
      LIMIT 1
    ) AS dictionary,
    ( SELECT pg_catalog.ts_lexize(mapdict, parse.token)
      FROM pg_catalog.pg_ts_config_map AS m
      WHERE m.mapcfg = $1 AND m.maptokentype = parse.tokid
      ORDER BY pg_catalog.ts_lexize(mapdict, parse.token) IS NULL, m.mapseqno
      LIMIT 1
    ) AS lexemes
FROM pg_catalog.ts_parse(
        (SELECT cfgparser FROM pg_catalog.pg_ts_config WHERE oid = $1 ), $2
    ) AS parse,
     pg_catalog.ts_token_type(
        (SELECT cfgparser FROM pg_catalog.pg_ts_config WHERE oid = $1 )
    ) AS tt
WHERE tt.tokid = parse.tokid
$function$;/*ts_debug 2866*/CREATE OR REPLACE FUNCTION pg_catalog.ts_debug(document text, OUT alias text, OUT description text, OUT token text, OUT dictionaries regdictionary[], OUT dictionary regdictionary, OUT lexemes text[])
 RETURNS SETOF record
 LANGUAGE sql
 STABLE PARALLEL SAFE STRICT
AS $function$
    SELECT * FROM pg_catalog.ts_debug( pg_catalog.get_current_ts_config(), $1);
$function$;/*gin_extract_query_trgm 2867*/CREATE OR REPLACE FUNCTION public.gin_extract_query_trgm(text, internal, smallint, internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gin_extract_query_trgm$function$;/*gin_trgm_consistent 2868*/CREATE OR REPLACE FUNCTION public.gin_trgm_consistent(internal, smallint, text, integer, internal, internal, internal, internal)
 RETURNS boolean
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gin_trgm_consistent$function$;/*json_populate_record 2869*/CREATE OR REPLACE FUNCTION pg_catalog.json_populate_record(base anyelement, from_json json, use_json_as_text boolean DEFAULT false)
 RETURNS anyelement
 LANGUAGE internal
 STABLE PARALLEL SAFE
AS $function$json_populate_record$function$;/*json_populate_recordset 2870*/CREATE OR REPLACE FUNCTION pg_catalog.json_populate_recordset(base anyelement, from_json json, use_json_as_text boolean DEFAULT false)
 RETURNS SETOF anyelement
 LANGUAGE internal
 STABLE PARALLEL SAFE ROWS 100
AS $function$json_populate_recordset$function$;/*pg_logical_slot_get_changes 2871*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_slot_get_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, VARIADIC options text[] DEFAULT '{}'::text[], OUT lsn pg_lsn, OUT xid xid, OUT data text)
 RETURNS SETOF record
 LANGUAGE internal
 COST 1000
AS $function$pg_logical_slot_get_changes$function$;/*pg_logical_slot_peek_changes 2872*/CREATE OR REPLACE FUNCTION pg_catalog.pg_logical_slot_peek_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, VARIADIC options text[] DEFAULT '{}'::text[], OUT lsn pg_lsn, OUT xid xid, OUT data text)
 RETURNS SETOF record
 LANGUAGE internal
 COST 1000
AS $function$pg_logical_slot_peek_changes$function$;/*pg_create_physical_replication_slot 2873*/CREATE OR REPLACE FUNCTION pg_catalog.pg_create_physical_replication_slot(slot_name name, immediately_reserve boolean DEFAULT false, temporary boolean DEFAULT false, OUT slot_name name, OUT lsn pg_lsn)
 RETURNS record
 LANGUAGE internal
 STRICT
AS $function$pg_create_physical_replication_slot$function$;/*make_interval 2874*/CREATE OR REPLACE FUNCTION pg_catalog.make_interval(years integer DEFAULT 0, months integer DEFAULT 0, weeks integer DEFAULT 0, days integer DEFAULT 0, hours integer DEFAULT 0, mins integer DEFAULT 0, secs double precision DEFAULT 0.0)
 RETURNS interval
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$make_interval$function$;/*jsonb_set 2875*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_set(jsonb_in jsonb, path text[], replacement jsonb, create_if_missing boolean DEFAULT true)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_set$function$;/*pg_current_logfile 2876*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_logfile()
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE
AS $function$pg_current_logfile$function$;/*gin_trgm_triconsistent 2877*/CREATE OR REPLACE FUNCTION public.gin_trgm_triconsistent(internal, smallint, text, integer, internal, internal, internal)
 RETURNS "char"
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$gin_trgm_triconsistent$function$;/*strict_word_similarity 2878*/CREATE OR REPLACE FUNCTION public.strict_word_similarity(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$strict_word_similarity$function$;/*strict_word_similarity_op 2879*/CREATE OR REPLACE FUNCTION public.strict_word_similarity_op(text, text)
 RETURNS boolean
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$strict_word_similarity_op$function$;/*jsonb_set_lax 2880*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_set_lax(jsonb_in jsonb, path text[], replacement jsonb, create_if_missing boolean DEFAULT true, null_value_treatment text DEFAULT 'use_json_null'::text)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE
AS $function$jsonb_set_lax$function$;/*parse_ident 2881*/CREATE OR REPLACE FUNCTION pg_catalog.parse_ident(str text, strict boolean DEFAULT true)
 RETURNS text[]
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$parse_ident$function$;/*jsonb_path_exists 2882*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_exists(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_exists$function$;/*jsonb_path_match 2883*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_match(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS boolean
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_match$function$;/*jsonb_path_query 2884*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS SETOF jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query$function$;/*jsonb_path_query_array 2885*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query_array(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query_array$function$;/*jsonb_path_query_first 2886*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query_first(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS jsonb
 LANGUAGE internal
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query_first$function$;/*jsonb_path_match_tz 2887*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_match_tz(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS boolean
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_match_tz$function$;/*jsonb_path_query_array_tz 2888*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query_array_tz(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query_array_tz$function$;/*jsonb_path_query_first_tz 2889*/CREATE OR REPLACE FUNCTION pg_catalog.jsonb_path_query_first_tz(target jsonb, path jsonpath, vars jsonb DEFAULT '{}'::jsonb, silent boolean DEFAULT false)
 RETURNS jsonb
 LANGUAGE internal
 STABLE PARALLEL SAFE STRICT
AS $function$jsonb_path_query_first_tz$function$;/*pg_start_backup 2890*/CREATE OR REPLACE FUNCTION pg_catalog.pg_start_backup(label text, fast boolean DEFAULT false, exclusive boolean DEFAULT true)
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_start_backup$function$;/*pg_stop_backup 2891*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stop_backup(exclusive boolean, wait_for_archive boolean DEFAULT true, OUT lsn pg_lsn, OUT labelfile text, OUT spcmapfile text)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL RESTRICTED STRICT
AS $function$pg_stop_backup_v2$function$;/*pg_create_restore_point 2892*/CREATE OR REPLACE FUNCTION pg_catalog.pg_create_restore_point(text)
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_create_restore_point$function$;/*pg_switch_wal 2893*/CREATE OR REPLACE FUNCTION pg_catalog.pg_switch_wal()
 RETURNS pg_lsn
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_switch_wal$function$;/*pg_wal_replay_pause 2894*/CREATE OR REPLACE FUNCTION pg_catalog.pg_wal_replay_pause()
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_wal_replay_pause$function$;/*pg_wal_replay_resume 2895*/CREATE OR REPLACE FUNCTION pg_catalog.pg_wal_replay_resume()
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_wal_replay_resume$function$;/*pg_rotate_logfile 2896*/CREATE OR REPLACE FUNCTION pg_catalog.pg_rotate_logfile()
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_rotate_logfile_v2$function$;/*pg_current_logfile 2897*/CREATE OR REPLACE FUNCTION pg_catalog.pg_current_logfile(text)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE
AS $function$pg_current_logfile_1arg$function$;/*pg_promote 2898*/CREATE OR REPLACE FUNCTION pg_catalog.pg_promote(wait boolean DEFAULT true, wait_seconds integer DEFAULT 60)
 RETURNS boolean
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_promote$function$;/*pg_stat_reset 2899*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_reset()
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE
AS $function$pg_stat_reset$function$;/*pg_stat_reset_shared 2900*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_reset_shared(text)
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_stat_reset_shared$function$;/*pg_stat_reset_slru 2901*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_reset_slru(text)
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE
AS $function$pg_stat_reset_slru$function$;/*pg_stat_reset_single_table_counters 2902*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_reset_single_table_counters(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_stat_reset_single_table_counters$function$;/*pg_stat_reset_single_function_counters 2903*/CREATE OR REPLACE FUNCTION pg_catalog.pg_stat_reset_single_function_counters(oid)
 RETURNS void
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_stat_reset_single_function_counters$function$;/*lo_import 2904*/CREATE OR REPLACE FUNCTION pg_catalog.lo_import(text)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$be_lo_import$function$;/*lo_import 2905*/CREATE OR REPLACE FUNCTION pg_catalog.lo_import(text, oid)
 RETURNS oid
 LANGUAGE internal
 STRICT
AS $function$be_lo_import_with_oid$function$;/*lo_export 2906*/CREATE OR REPLACE FUNCTION pg_catalog.lo_export(oid, text)
 RETURNS integer
 LANGUAGE internal
 STRICT
AS $function$be_lo_export$function$;/*pg_read_file 2907*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_file(text)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_file_all$function$;/*pg_read_file 2908*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_file(text, bigint, bigint)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_file_off_len$function$;/*pg_read_file 2909*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_file(text, bigint, bigint, boolean)
 RETURNS text
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_file_v2$function$;/*pg_read_binary_file 2910*/CREATE OR REPLACE FUNCTION pg_catalog.pg_read_binary_file(text, bigint, bigint, boolean)
 RETURNS bytea
 LANGUAGE internal
 PARALLEL SAFE STRICT
AS $function$pg_read_binary_file$function$;/*pg_ls_logdir 2911*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_logdir(OUT name text, OUT size bigint, OUT modification timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 10 ROWS 20
AS $function$pg_ls_logdir$function$;/*pg_ls_waldir 2912*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_waldir(OUT name text, OUT size bigint, OUT modification timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 10 ROWS 20
AS $function$pg_ls_waldir$function$;/*pg_ls_archive_statusdir 2913*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_archive_statusdir(OUT name text, OUT size bigint, OUT modification timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 10 ROWS 20
AS $function$pg_ls_archive_statusdir$function$;/*pg_ls_tmpdir 2914*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_tmpdir(OUT name text, OUT size bigint, OUT modification timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 10 ROWS 20
AS $function$pg_ls_tmpdir_noargs$function$;/*pg_ls_tmpdir 2915*/CREATE OR REPLACE FUNCTION pg_catalog.pg_ls_tmpdir(tablespace oid, OUT name text, OUT size bigint, OUT modification timestamp with time zone)
 RETURNS SETOF record
 LANGUAGE internal
 PARALLEL SAFE STRICT COST 10 ROWS 20
AS $function$pg_ls_tmpdir_1arg$function$;/*dsnowball_init 2916*/CREATE OR REPLACE FUNCTION pg_catalog.dsnowball_init(internal)
 RETURNS internal
 LANGUAGE c
 STRICT
AS '$libdir/dict_snowball', $function$dsnowball_init$function$;/*dsnowball_lexize 2917*/CREATE OR REPLACE FUNCTION pg_catalog.dsnowball_lexize(internal, internal, internal, internal)
 RETURNS internal
 LANGUAGE c
 STRICT
AS '$libdir/dict_snowball', $function$dsnowball_lexize$function$;/*_pg_expandarray 2918*/CREATE OR REPLACE FUNCTION information_schema._pg_expandarray(anyarray, OUT x anyelement, OUT n integer)
 RETURNS SETOF record
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$select $1[s], s - pg_catalog.array_lower($1,1) + 1
        from pg_catalog.generate_series(pg_catalog.array_lower($1,1),
                                        pg_catalog.array_upper($1,1),
                                        1) as g(s)$function$;/*_pg_index_position 2919*/CREATE OR REPLACE FUNCTION information_schema._pg_index_position(oid, smallint)
 RETURNS integer
 LANGUAGE sql
 STABLE STRICT
AS $function$
SELECT (ss.a).n FROM
  (SELECT information_schema._pg_expandarray(indkey) AS a
   FROM pg_catalog.pg_index WHERE indexrelid = $1) ss
  WHERE (ss.a).x = $2;
$function$;/*_pg_truetypid 2920*/CREATE OR REPLACE FUNCTION information_schema._pg_truetypid(pg_attribute, pg_type)
 RETURNS oid
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT CASE WHEN $2.typtype = 'd' THEN $2.typbasetype ELSE $1.atttypid END$function$;/*_pg_truetypmod 2921*/CREATE OR REPLACE FUNCTION information_schema._pg_truetypmod(pg_attribute, pg_type)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT CASE WHEN $2.typtype = 'd' THEN $2.typtypmod ELSE $1.atttypmod END$function$;/*_pg_char_max_length 2922*/CREATE OR REPLACE FUNCTION information_schema._pg_char_max_length(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $2 = -1 /* default typmod */
       THEN null
       WHEN $1 IN (1042, 1043) /* char, varchar */
       THEN $2 - 4
       WHEN $1 IN (1560, 1562) /* bit, varbit */
       THEN $2
       ELSE null
  END$function$;/*strict_word_similarity_commutator_op 2923*/CREATE OR REPLACE FUNCTION public.strict_word_similarity_commutator_op(text, text)
 RETURNS boolean
 LANGUAGE c
 STABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$strict_word_similarity_commutator_op$function$;/*_pg_char_octet_length 2924*/CREATE OR REPLACE FUNCTION information_schema._pg_char_octet_length(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $1 IN (25, 1042, 1043) /* text, char, varchar */
       THEN CASE WHEN $2 = -1 /* default typmod */
                 THEN CAST(2^30 AS integer)
                 ELSE information_schema._pg_char_max_length($1, $2) *
                      pg_catalog.pg_encoding_max_length((SELECT encoding FROM pg_catalog.pg_database WHERE datname = pg_catalog.current_database()))
            END
       ELSE null
  END$function$;/*_pg_numeric_precision 2925*/CREATE OR REPLACE FUNCTION information_schema._pg_numeric_precision(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE $1
         WHEN 21 /*int2*/ THEN 16
         WHEN 23 /*int4*/ THEN 32
         WHEN 20 /*int8*/ THEN 64
         WHEN 1700 /*numeric*/ THEN
              CASE WHEN $2 = -1
                   THEN null
                   ELSE (($2 - 4) >> 16) & 65535
                   END
         WHEN 700 /*float4*/ THEN 24 /*FLT_MANT_DIG*/
         WHEN 701 /*float8*/ THEN 53 /*DBL_MANT_DIG*/
         ELSE null
  END$function$;/*_pg_numeric_precision_radix 2926*/CREATE OR REPLACE FUNCTION information_schema._pg_numeric_precision_radix(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $1 IN (21, 23, 20, 700, 701) THEN 2
       WHEN $1 IN (1700) THEN 10
       ELSE null
  END$function$;/*_pg_numeric_scale 2927*/CREATE OR REPLACE FUNCTION information_schema._pg_numeric_scale(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $1 IN (21, 23, 20) THEN 0
       WHEN $1 IN (1700) THEN
            CASE WHEN $2 = -1
                 THEN null
                 ELSE ($2 - 4) & 65535
                 END
       ELSE null
  END$function$;/*_pg_datetime_precision 2928*/CREATE OR REPLACE FUNCTION information_schema._pg_datetime_precision(typid oid, typmod integer)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $1 IN (1082) /* date */
           THEN 0
       WHEN $1 IN (1083, 1114, 1184, 1266) /* time, timestamp, same + tz */
           THEN CASE WHEN $2 < 0 THEN 6 ELSE $2 END
       WHEN $1 IN (1186) /* interval */
           THEN CASE WHEN $2 < 0 OR $2 & 65535 = 65535 THEN 6 ELSE $2 & 65535 END
       ELSE null
  END$function$;/*_pg_interval_type 2929*/CREATE OR REPLACE FUNCTION information_schema._pg_interval_type(typid oid, mod integer)
 RETURNS text
 LANGUAGE sql
 IMMUTABLE PARALLEL SAFE STRICT
AS $function$SELECT
  CASE WHEN $1 IN (1186) /* interval */
           THEN pg_catalog.upper(substring(pg_catalog.format_type($1, $2) from 'interval[()0-9]* #"%#"' for '#'))
       ELSE null
  END$function$;/*plpgsql_call_handler 2930*/CREATE OR REPLACE FUNCTION pg_catalog.plpgsql_call_handler()
 RETURNS language_handler
 LANGUAGE c
AS '$libdir/plpgsql', $function$plpgsql_call_handler$function$;/*plpgsql_inline_handler 2931*/CREATE OR REPLACE FUNCTION pg_catalog.plpgsql_inline_handler(internal)
 RETURNS void
 LANGUAGE c
 STRICT
AS '$libdir/plpgsql', $function$plpgsql_inline_handler$function$;/*plpgsql_validator 2932*/CREATE OR REPLACE FUNCTION pg_catalog.plpgsql_validator(oid)
 RETURNS void
 LANGUAGE c
 STRICT
AS '$libdir/plpgsql', $function$plpgsql_validator$function$;/*strict_word_similarity_dist_op 2933*/CREATE OR REPLACE FUNCTION public.strict_word_similarity_dist_op(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$strict_word_similarity_dist_op$function$;/*strict_word_similarity_dist_commutator_op 2934*/CREATE OR REPLACE FUNCTION public.strict_word_similarity_dist_commutator_op(text, text)
 RETURNS real
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE STRICT
AS '$libdir/pg_trgm', $function$strict_word_similarity_dist_commutator_op$function$;/*gtrgm_options 2935*/CREATE OR REPLACE FUNCTION public.gtrgm_options(internal)
 RETURNS void
 LANGUAGE c
 IMMUTABLE PARALLEL SAFE
AS '$libdir/pg_trgm', $function$gtrgm_options$function$;/*core_srd_role_pkg_getfullfunctionname 2936*/CREATE OR REPLACE FUNCTION public.core_srd_role_pkg_getfullfunctionname(p_function_id bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
cur record;
vFullName varchar(4000) := '';
begin
for cur in (
	with recursive csf as (
	select f.id, f.parent_id, f.functionname , 1 lvl
	from core_srd_function f
	where f.id = p_function_id
	union all
	select f.id, f.parent_id, f.functionname , lvl+1
	from core_srd_function f
	join csf on csf.parent_id = f.id
	)
	select *
	from csf
	order by lvl
) loop
	vFullName := cur.functionname || ' - ' || vFullName;
end loop;
	return substr(vFullName,1,length(vFullName)-3);
end;
$function$;/*core_long_process_type_cache_trg 2937*/CREATE OR REPLACE FUNCTION public.core_long_process_type_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.long.process');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;/*core_register_attr_cache_trg 2938*/CREATE OR REPLACE FUNCTION public.core_register_attr_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;/*core_register_cache_trg 2939*/CREATE OR REPLACE FUNCTION public.core_register_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;/*core_register_rel_cache_trg 2940*/CREATE OR REPLACE FUNCTION public.core_register_rel_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;/*core_srd_department_trgi_fnc 2941*/CREATE OR REPLACE FUNCTION public.core_srd_department_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	   	 execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*last_post 2942*/CREATE OR REPLACE FUNCTION public.last_post(text, character)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE
AS $function$ 
     select length($1)- length(regexp_replace($1, '.*' || $2,''));
$function$;/*core_updstru_checkexistsequence 2943*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistsequence(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM pg_catalog.pg_sequences as s
          --TABLE pg_catalog.pg_sequences
          WHERE s.sequencename = lower(sName) AND s.schemaname NOT IN ('pg_catalog', 'information_schema');

          if (nCnt = 0) then
              return false;
          else
              return true;
          end if;

    END $function$;/*core_srd_function_trgi_fnc 2944*/CREATE OR REPLACE FUNCTION public.core_srd_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select DISTINCT r.user_id
                  from core_srd_role_function f
                  join core_srd_user_role r
                    on r.role_id = f.role_id
                 where f.function_id = NEW.ID) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_updstru_checkexistindex 2945*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexistindex(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
  declare
		sIndexName ALIAS FOR $1;
        nCnt numeric;
  begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_indexes as i
        --TABLE pg_catalog.pg_indexes
        WHERE lower(i.indexname) = lower(sIndexName) AND i.schemaname = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

  END
$function$;/*core_srd_role_function_trgd_fnc 2946*/CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = OLD.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_role_function_trgi_fnc 2947*/CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_role_register_trgi_fnc 2948*/CREATE OR REPLACE FUNCTION public.core_srd_role_register_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop

    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
       
	   if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_role_trgi_fnc 2949*/CREATE OR REPLACE FUNCTION public.core_srd_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*fill_system_daily_statistics 2950*/CREATE OR REPLACE FUNCTION public.fill_system_daily_statistics()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
  ID_VAL                       numeric;
  STAT_DATE_VAL                date;
  DB_SIZE_VAL                  numeric;
  ERRORS_VAL                   numeric;
  WARNINGS_VAL                 numeric;
  ACTIONS_VAL                  numeric;
  SESSIONS_VAL                 numeric;
  CHANGES_VAL                  numeric;
  DIAGNOSTICS_SLOW_VAL         numeric;
  
  LONG_PROC_RUN_VAL                numeric;
  LONG_PROC_RUN_ERROR_VAL          numeric;
  
begin
  -- DATE
  --STAT_DATE_VAL := trunc(sysdate) - 1;
  STAT_DATE_VAL := CURRENT_DATE - 1;
  
  INSERT INTO system_daily_stat_db_obj
  (SELECT STAT_DATE_VAL as STAT_DATE
       , 'INDEX' as SEGMENT_TYPE
       , i.indexname as SEGMENT_NAME
       , i.tablename as TABLE_NAME
       , GetSizeOfRelation(i.indexname::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM pg_indexes as i
  WHERE i.schemaname = 'public'

  UNION ALL

  SELECT STAT_DATE_VAL as STAT_DATE
       , 'TABLE' as SEGMENT_TYPE
       , t.table_name as SEGMENT_NAME
       , t.table_name as TABLE_NAME
       , GetSizeOfRelation(t.table_name::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM information_schema.tables as t);

  --DB_SIZE
  --select sum(t.bytes) / (1024 * 1024) into DB_SIZE_VAL from user_extents t;
  --SELECT pg_catalog.pg_database_size('cipjs_main') / (1024 * 1024) INTO DB_SIZE_VAL;
  SELECT sum(t.size_meg) INTO DB_SIZE_VAL from system_daily_stat_db_obj t where t.stat_date = STAT_DATE_VAL;
  
  -- ID
  select count(1) + 1 into ID_VAL from SYSTEM_DAILY_STATISTICS;

  --ERRORS
  select count(1)
    into ERRORS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'ERROR';

  --WARNINGS
  select count(1)
    into WARNINGS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'WARNING';

  --ACTIONS
  select count(1)
    into ACTIONS_VAL
    from core_srd_audit t
   where t.actiontime >= STAT_DATE_VAL
     and t.actiontime < STAT_DATE_VAL + 1;

  --SESSIONS
  select count(1)
    into SESSIONS_VAL
    from core_srd_session t
   where t.logintime >= STAT_DATE_VAL
     and t.logintime < STAT_DATE_VAL + 1;

  --CHANGES
  select count(1)
    into CHANGES_VAL
    from core_td_change ch
    join core_td_changeset chs
      on chs.id = ch.changeset_id
   where chs.changeset_date >= STAT_DATE_VAL
     and chs.changeset_date < STAT_DATE_VAL + 1;

  --DIAGNOSTICS_SLOW
  select count(1)
    into DIAGNOSTICS_SLOW_VAL
    from core_diagnostics t
   where t.action_date >= STAT_DATE_VAL
     and t.action_date < STAT_DATE_VAL + 1
     and t.execution_duration > 10000000;
  
  -- LONG PROCESS
  select count(1)
    into LONG_PROC_RUN_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 3;
  
  -- LONG PROCESS ERROR
  select count(1)
    into LONG_PROC_RUN_ERROR_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 4;

  insert into SYSTEM_DAILY_STATISTICS
    (ID,
     STAT_DATE,
     DB_SIZE,
     ERRORS,
     WARNINGS,
     ACTIONS,
     SESSIONS,
     CHANGES,
     DIAGNOSTICS_SLOW,
     
     LONG_PROC_RUN,
     LONG_PROC_RUN_ERROR
	 
     )
  VALUES
    (ID_VAL,
     STAT_DATE_VAL,
     DB_SIZE_VAL,
     ERRORS_VAL,
     WARNINGS_VAL,
     ACTIONS_VAL,
     SESSIONS_VAL,
     CHANGES_VAL,
     DIAGNOSTICS_SLOW_VAL,
     
     LONG_PROC_RUN_VAL,
     LONG_PROC_RUN_ERROR_VAL
     
     );

END
$function$;/*core_srd_role_attr_trgi_fnc 2951*/CREATE OR REPLACE FUNCTION public.core_srd_role_attr_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
                join core_srd_role_register rr on rr.role_id = ur.role_id
               where rr.id = NEW.rule_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_user_role_trgd_fnc 2952*/CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user',OLD.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_user_role_trgi_fnc 2953*/CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_user_trgi_fnc 2954*/CREATE OR REPLACE FUNCTION public.core_srd_user_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.ID);
	     execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_srd_usersettings_trgi_fnc 2955*/CREATE OR REPLACE FUNCTION public.core_srd_usersettings_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.USERID);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;/*core_cachmanagment_updatetimestamp 2956*/CREATE OR REPLACE FUNCTION public.core_cachmanagment_updatetimestamp(pcacheobject character varying, pcachekey bigint DEFAULT 0, pextradata character varying DEFAULT 'NULL'::character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
	begin
		update core_cache_updates set cache_timestamp = current_timestamp
		where cacheobject = pcacheobject 
			and cachekey = coalesce(pcachekey,0)
			and extradata = coalesce(pextradata,'NULL');
		if not found then
			INSERT INTO CORE_CACHE_UPDATES(id,cacheobject, CACHEKEY, EXTRADATA, CACHE_TIMESTAMP)
        	VALUES (nextval('reg_object_seq'), pcacheobject, pcachekey, pextradata, current_timestamp);
		end if;
	END
$function$;/*core_numerator_sregnomdecrement 2957*/CREATE OR REPLACE FUNCTION public.core_numerator_sregnomdecrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_decrementstep bigint, OUT p_sequenceid bigint, OUT p_numdecrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$
DECLARE
  v_sequenceid      bigint default -1;
  v_newsequenceid   bigint default -1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numdecrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0, '') when coalesce(p_par0, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1, '') when coalesce(p_par1, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2, '') when coalesce(p_par2, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3, '') when coalesce(p_par3, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4, '') when coalesce(p_par4, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5, '') when coalesce(p_par5, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6, '') when coalesce(p_par6, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7, '') when coalesce(p_par7, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8, '') when coalesce(p_par8, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9, '') when coalesce(p_par9, '') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
       
      update core_regnom_sequences
        set currentincrement = core_regnom_sequences.currentincrement - p_decrementstep
      where id = v_sequenceid
      returning currentincrement into p_numdecrement;
      p_sequenceid := v_sequenceid;          
    end if;
END
$function$;/*core_numerator_sregnomincrement 2958*/CREATE OR REPLACE FUNCTION public.core_numerator_sregnomincrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_minval bigint, p_maxval bigint, p_incrementstep bigint, OUT p_sequenceid bigint, OUT p_numincrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$
DECLARE
  v_sequenceid      bigint default - 1;
  v_newsequenceid   bigint default - 1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numincrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0,'') when coalesce(p_par0,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1,'') when coalesce(p_par1,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2,'') when coalesce(p_par2,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3,'') when coalesce(p_par3,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4,'') when coalesce(p_par4,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5,'') when coalesce(p_par5,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6,'') when coalesce(p_par6,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7,'') when coalesce(p_par7,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8,'') when coalesce(p_par8,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9,'') when coalesce(p_par9,'') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
      if v_currentsequence = p_maxval then
      	begin
          update core_regnom_sequences
             set currentincrement = p_minval
           where id = v_sequenceid
          returning v_currentsequence into p_numincrement;
        end;
      else
      	begin
          update core_regnom_sequences
             set currentincrement = core_regnom_sequences.currentincrement +
                                                    p_incrementstep
           where id = v_sequenceid
          returning currentincrement into p_numincrement;
          p_sequenceid := v_sequenceid;
        end;
      end if;
    else
      select coalesce(max(id) + 1, 1) into v_newsequenceid from core_regnom_sequences;
      insert into core_regnom_sequences
        (id,
         numeratorid,
         regnomtype,
         par0,
         par1,
         par2,
         par3,
         par4,
         par5,
         par6,
         par7,
         par8,
         par9,
         currentincrement)
      values
        (v_newsequenceid,
         p_numeratorid,
         p_regnomtype,
         p_par0,
         p_par1,
         p_par2,
         p_par3,
         p_par4,
         p_par5,
         p_par6,
         p_par7,
         p_par8,
         p_par9,
         p_minval)
      returning p_minval into p_numincrement;
      p_sequenceid := v_newsequenceid;
    end if;
END
$function$;/*core_register_pkg_getorcreatechangeset 2959*/CREATE OR REPLACE FUNCTION public.core_register_pkg_getorcreatechangeset(itdinstanceid bigint, iuserid bigint, istatus bigint, changesetdate timestamp without time zone, OUT ichangesetid bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
BEGIN
  begin
      -- проверка есть ли сохраненный ранее набор изменений для данного документа и пользователя со статусом 1
      select chs.id
        into strict ichangesetid
        from core_td_changeset chs
       where chs.status = istatus
         and chs.td_id = itdinstanceid
         and chs.user_id = iuserid;
    exception
      when no_data_found then
        --создание нового набора изменений
        insert into core_td_changeset
          (id, td_id, changeset_date, status, user_id)
        values
          (nextval('seq_core_td'), itdinstanceid, changesetdate, istatus, iuserid)
        returning id into ichangesetid;
    end;
END
$function$;/*core_register_pkg_getuserkeystring 2960*/CREATE OR REPLACE FUNCTION public.core_register_pkg_getuserkeystring(p_register_id bigint, p_object_id bigint, p_date timestamp without time zone)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
objexists bool;
quant_table varchar(40);
primary_key varchar(50);
storage_type int8;
attr RECORD;
query varchar;
val varchar;
str varchar := '';
begin
    if p_register_id is null or p_object_id is null then
        return null;
    end if;

    select cr.quant_table, cr.storage_type, cra.value_field
    into quant_table, storage_type, primary_key
    from core_register cr, core_register_attribute cra
    where cr.registerid = p_register_id
        and cra.registerid = p_register_id
        and cra.primary_key = 1;
    IF NOT FOUND THEN
        return 'Не найдены метаданные реестра ' || p_register_id;
    END IF;

    EXECUTE format('select exists(select 1 from %s where %s=$1)', quant_table, primary_key) using p_object_id into objexists;
    IF NOT objexists THEN
          return 'Объект не найден: реестр = ' || p_register_id || '; ид объекта = ' || p_object_id;
    END IF;

    FOR attr IN (
        select ra.name, ra.value_field, ra.type
        from core_register_attribute ra
        where ra.registerid = p_register_id
            and ra.user_key is not null
        order by ra.user_key
    ) loop
        query := 'select coalesce('
            || case attr.type
                when 4 then '%s'
                when 5 then 'to_char(%s, ''DD.MM.YYYY HH24:MI:SS'')'
                else        '%s::varchar' end
            || ', ''NULL'') from %s where %s=$1'
            || case when (storage_type = 1 or storage_type = 2) then ' and $2 between s_ and po_' else '' end;
        EXECUTE format(query, attr.value_field, quant_table, primary_key) using p_object_id, p_date into val;
        str := str || val || '; ';
    END LOOP;
    IF NOT FOUND THEN
        return 'ИД: ' || p_object_id;
    END IF;

    return case when char_length(str)>2 then substring(str, 1, char_length(str)-2) else 'ИД: ' || p_object_id end;
END
$function$;/*core_updstru_checkexist_primarykey 2961*/CREATE OR REPLACE FUNCTION public.core_updstru_checkexist_primarykey(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
DECLARE
	tablename ALIAS FOR $1;
    result int = 0;
BEGIN
	SELECT COUNT(*)
    INTO result
	FROM information_schema.table_constraints as tc
	WHERE lower(tc.table_name) = lower(tablename) AND tc.constraint_type = 'PRIMARY KEY' AND tc.table_schema = 'public';

    if(result > 0)then
    	return true;
    else
    	return false;
    end if;
END $function$;/*additional_analysis_checker 2962*/CREATE OR REPLACE FUNCTION public.additional_analysis_checker(a_param integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
declare count_object int := 0;
BEGIN
/*task CIPJSKO-81*/
/*Создаем таблицу в которой будем хранить данные всех объектов которым был проставлен параметр дополнительного анализа*/
create temporary table return_gid (
      	id_object int,
		type_obj int,
		kn varchar,
		address varchar,
		date_definition TIMESTAMP,
		kc numeric,
		sud_number character varying,
		parameter_case int
);
/*1*/
/*джойним только последние заключения и решения*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 1
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
left join sud_zak as zak on zak.id = zaklink.id_zak
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where sud.sud_date < zak.date and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null);


/*2*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 2
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where (SELECT date_part('year', CAST(sud.sud_date AS DATE))) <> (SELECT date_part('year', CAST(ob.date AS DATE)))
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;


 
 /*3*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 3
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where ob.date > '2013-01-01 00:00:00' and ob.date < '2015-12-15 00:00:00'
and sud.sud_date > '2019-01-01 00:00:00' and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*4*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 4
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
left join sud_zak as zak on zak.id = zaklink.id_zak
where zak.date +  interval '2 month' < current_date and (select count(*) from sud_sudlink where id_object = ob.id) = 0
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*5*/
/*Джойним таблицу с изменениями значений кадастровой стоимости,
в джойне сразу сортируем по дате изменнеия и сначала берем последню запись для кадастровой стоимости
а в следующем джойне пред последнюю запись изменения и в условии ищем дельту
Переписать надо*/
/*insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 5 from sud_object as ob
left join sud_sudlink as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
join
(select param_double, id from (SELECT *,
							   row_number () over (PARTITION BY id order by date_user desc) as r_n
	FROM public.sud_param where id_table = 1 and param_name = 'kc')
 x where r_n = 1) as var1/*последнее изменение*/ on var1.id = ob.id
 join
(select param_double, id from (SELECT *,
							   row_number () over (PARTITION BY id order by date_user desc) as r_n
	FROM public.sud_param where id_table = 1 and param_name = 'kc')
 x where r_n = 2) as var2 /*пред последнее изменение*/ on var2.id = ob.id
 where var2.param_double is not null and var1.param_double is not null
 and @(var1.param_double - var2.param_double) > @(var2.param_double/2)
 and (ob.exception = 0 or ob.exception is null)
 and (ob.additional_analysis = 0 or ob.additional_analysis is null)*/
 
 
/*5*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние решение*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, resh.number, 5
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
where @(ob.kc/2) > resh.rs
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null);



 /*6*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 6
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where resh.rs > ob.kc  and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*8*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 8
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where resh.rs is not null and (sud.status = 1 or sud.status = 3)
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*9*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 9
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where (resh.rs is null or resh.rs = 0) and sud.status = 2
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*10*/
/*групперуем по суд номерам и истцам (соответственно если разные будут истцы и номера то будет несколько записей),
сортируем по количеству, тех что больше будут выше
так же простовляем номера строк и берем соответственно не первую строчку чтобы проставить флаг тем которых меньше
далее джойним  полученную таблицу к объектам и получаем объекты которым надо проставить условие*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 10 from sud_object as ob
left join sud_sudlink as slink on slink.id_object = ob.id
left join sud_sud as sud on sud.id = slink.id_sud
left join (select * from(select sud.number, ob.owner, count(*),
			  row_number () over (PARTITION BY sud.number ORDER BY sud.count DESC ) as r_n
from sud_sud as sud
left join sud_sudlink as slink on slink.id_sud = sud.id
left join sud_object as ob on slink.id_object = ob.id
group by sud.number, ob.owner)
		   x where x.r_n > 1) as temp_table
on temp_table.owner = ob.owner and temp_table.number = sud.number
where (select count(*) from sud_sudlink where id_object = ob.id) > 0
and temp_table.number is not null
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*11*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 11 from sud_object as ob
left join sud_sudlink as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
join
(select kn, kc, count(*)
from sud_object
where kn is not null
group by kn, kc) as tmp_grup on tmp_grup.kn = ob.kn and tmp_grup.kc = ob.kc
where tmp_grup.count > 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*12*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sudlink.number, 12
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as sudlink on sudlink.id_object = ob.id
left join sud_zaklink as zaklink on zaklink.id_object = ob.id
where sudlink.rs <> zaklink.rs and (select count(*)
from sud_zaklink as z group by z.id_object having z.id_object = ob.id) = 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*13*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sudlink.number, 13
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as sudlink on sudlink.id_object = ob.id
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
where sudlink.rs <> zaklink.rs and (select count(*)
from sud_zaklink as z group by z.id_object having z.id_object = ob.id) > 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*14*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select
obj.id, obj.typeobj, obj.kn, obj.adres, obj.date, obj.kc, sud.number, 14
from sud_object as obj
left join sud_sudlink as resh on resh.id_object = obj.id
left join sud_sud as sud on sud.id = resh.id_sud
left join (select * from (select otchet.id, otchetl.id_object, otchetl.id_otchet, otchetl.rs, ROW_NUMBER () OVER (PARTITION BY otchetl.id_object order by otchet.date DESC) as rn
							from sud_otchetlink as otchetl
							left join sud_otchet as otchet on otchetl.id_otchet = otchet.id) x
			where x.rn = 1) as ol on  ol.id_object = obj.id
left join (select * from (select zakl.id, zakl.id_object, zakl.id_zak, zakl.rs, ROW_NUMBER () OVER (PARTITION BY zakl.id_object order by zak.date DESC) as rn
							from sud_zaklink as zakl
							left join sud_zak as zak on zakl.id_zak = zak.id) x
			where x.rn = 1) as zl on zl.id_object = obj.id
where ol.rs=zl.rs
and (obj.exception = 0 or obj.exception is null)
and (obj.additional_analysis = 0 or obj.additional_analysis is null)
/*and obj.id not in (select id_object from return_gid)*/;

update sud_object
set additional_analysis = 1
where id in (select id_object from return_gid);

insert into sud_dopanaliz_log (id_object, kn, address, date_definition, kc, sud_number, parameter_case, id_process, typeobj)
select r_grid.id_object, r_grid.kn, r_grid.address, r_grid.date_definition, r_grid.kc, r_grid.sud_number,
r_grid.parameter_case, a_param, r_grid.type_obj
from return_gid as r_grid;

count_object := (select count(*) from (select id_object from return_gid group by id_object) x_1);
drop table return_gid;
RETURN count_object;

END;
$function$;/*notify_gbu_long_proc_updating 2963*/CREATE OR REPLACE FUNCTION public.notify_gbu_long_proc_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		if (NEW.process_type_id = 12  or NEW.process_type_id = 13 or NEW.process_type_id = 14) then
			PERFORM pg_notify('notify_gbu_long_proc_updating'::text, 'notify_gbu_long_proc_updating'::text);
		end if;
		return null;
	end;
	$function$;/*core_updstru_deletefkrefconstraints 2964*/CREATE OR REPLACE FUNCTION public.core_updstru_deletefkrefconstraints(character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
  declare
		sTable ALIAS FOR $1;
        qRow RECORD;
  begin
    -- Удаление внешних ссылок
    FOR qRow IN (SELECT tc.table_name as table_name_pk, tc.constraint_name as constraint_name_pk
                      , rc.constraint_name as constraint_name_fk, trc.table_name as table_name_fk
                 FROM information_schema.table_constraints as tc
                      INNER JOIN information_schema.referential_constraints as rc ON rc.unique_constraint_name = tc.constraint_name
                      INNER JOIN information_schema.table_constraints as trc ON trc.constraint_name = rc.constraint_name
                 WHERE tc.constraint_type = 'PRIMARY KEY' AND lower(tc.table_name) = lower(sTable)) LOOP
      execute immediate 'alter table ' || qRow.table_name_fk || ' drop constraint ' || qRow.constraint_name_fk;
    end loop;

  END $function$;/*core_updstru_getcolumndefault 2965*/CREATE OR REPLACE FUNCTION public.core_updstru_getcolumndefault(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
  declare
		tableName ALIAS FOR $1;
        columnName  ALIAS FOR $2;
		defaultValue varchar(200);
  begin
    SELECT c.column_default
    INTO defaultValue
    FROM information_schema.columns as c
    WHERE lower(c.table_name) = lower(tableName) AND lower(c.column_name) = lower(columnName);

    return defaultValue;
  END $function$;/*core_updstru_getcolumntype 2966*/CREATE OR REPLACE FUNCTION public.core_updstru_getcolumntype(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
	    tableName ALIAS FOR $1;
        columnName ALIAS FOR $2;
	    columnType varchar(100);
	begin
      	SELECT c.data_type
        INTO columnType
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE c.table_name = lower(tableName) AND c.column_name = lower(columnName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');

        return columnType;
	END $function$;/*core_updstru_isnullablecolumn 2967*/CREATE OR REPLACE FUNCTION public.core_updstru_isnullablecolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
        sColName ALIAS FOR $2;
        sNullable varchar(3);
    begin
        begin
			SELECT c.is_nullable
			INTO sNullable
            FROM information_schema.columns c
            --TABLE information_schema.columns
        	WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');
        exception
          when NO_DATA_FOUND then
            return false;
        end;

        if sNullable = 'Yes' then
			return true;
        else
          	return false;
        end if;

    END $function$;/*notify_core_long_queue_for_widget 2968*/CREATE OR REPLACE FUNCTION public.notify_core_long_queue_for_widget()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
notification json;
BEGIN
if(NEW.process_type_id is not null) then
notification = json_build_object(
                          'id', new.id,
                          'status', new.status,
                          'errorId', new.error_id,
                          'progress', new.progress,
                          'processTypeId', new.process_type_id,
                          'message', new.message,
                          'userId', NEW.user_id
                          );
			PERFORM pg_notify('notify_core_long_queue_for_widget'::text, notification::text);
            end if;
            return null;
END;
$function$;/*proc_cancel_test_proc 2969*/CREATE OR REPLACE FUNCTION public.proc_cancel_test_proc(a_param integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
declare count_object int := 0;
BEGIN
	
	FOR i IN 0..a_param LOOP
		insert into proc_cancel_test_table
		values('1', '1', '1');	
	END LOOP;
	
	PERFORM  pg_sleep(15);
	--RAISE EXCEPTION 'Cancel exception!'  USING ERRCODE = '57014';
	
	count_object := (select count(*) from proc_cancel_test_table);
RETURN count_object;

END;
$function$;/*notify_ko_unload_result_proc_updating 2970*/CREATE OR REPLACE FUNCTION public.notify_ko_unload_result_proc_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		PERFORM pg_notify('notify_ko_unload_result_proc_updating'::text, 'notify_ko_unload_result_proc_updating'::text);
		return null;
	end;
	$function$;/*ko_get_full_group_name 2971*/CREATE OR REPLACE FUNCTION public.ko_get_full_group_name(bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
		_groupId ALIAS FOR $1;
		_resultGroupName character varying;
		_groupName character varying;
		_groupAlgoritm bigint;
		_parentId bigint;
		_tourYear bigint;
    begin
		if _groupId is null then
			return null;
		end if;
		
		select g.group_name, g.group_algoritm, g.parent_id, t.year
		into _groupName, _groupAlgoritm, _parentId, _tourYear
		from ko_tour_groups k
		JOIN ko_group g on k.group_id=g.id
		JOIN ko_tour t on k.tour_id=t.id
		where g.id=_groupId;
		IF NOT FOUND THEN
				select g.group_name, g.group_algoritm, g.parent_id
				into _groupName, _groupAlgoritm, _parentId
				from ko_group g 
				where g.id=_groupId;
				IF NOT FOUND THEN
					return null;
				END IF;
		END IF;

		_resultGroupName := _groupName;
		
		WHILE _parentId <> -1
        LOOP
			_groupId := _parentId;
			
			select g.group_name, g.group_algoritm, g.parent_id
			into _groupName, _groupAlgoritm, _parentId
			from ko_tour_groups k
			join ko_group g on k.group_id=g.id
			join ko_tour t on k.tour_id=t.id
			where g.id=_groupId;
			IF NOT FOUND THEN
				select g.group_name, g.group_algoritm, g.parent_id
				into _groupName, _groupAlgoritm, _parentId
				from ko_group g 
				where g.id=_groupId;
				IF NOT FOUND THEN
					EXIT;
				END IF;
			END IF;
			
			_resultGroupName := CONCAT(_resultGroupName, ' (', _groupName, ')');
        END LOOP;

		if _groupAlgoritm = 98 then
			_resultGroupName := CONCAT(_resultGroupName, ' (ОКС)');
		end if;
			
		if _groupAlgoritm = 99 then
			_resultGroupName := CONCAT(_resultGroupName,' (ЗУ)');
		end if;
			
		if _tourYear is not null then
			_resultGroupName := CONCAT(_resultGroupName, '(Тур ', _tourYear, ')');
		end if;
		
		return _resultGroupName;
		
	END $function$;/*get_market_object_price_for_uprs 2972*/CREATE OR REPLACE FUNCTION public.get_market_object_price_for_uprs(cadastral_number character varying)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
 declare
	price numeric;
        
BEGIN


select
-- проверяем, есть ли объект-аналог с подходящим КН
case when not exists
(
	SELECT 1
          FROM MARKET_CORE_OBJECT L1_R100
          WHERE 
          (
            (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
            AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
          )
)
then
(
	-- если объекта нет
  	-1
 )
 
ELSE
(
    -- если объекты есть, смотрим - есть ли объект, поступивший из Росреестра (market_code = 737)
    select
    case when exists 
          (SELECT 1
            FROM MARKET_CORE_OBJECT L1_R100
            WHERE 
            (
              (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
              AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              and L1_R100.market_code = 737
            )
          )
      then 
      (
          -- есть объекты из Росрееста: берем первый с самой последней датой
          SELECT 
            L1_R100.PRICE as "MarketObjectPrice"
            FROM MARKET_CORE_OBJECT L1_R100
            WHERE 
            (
              (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
              AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              and L1_R100.market_code = 737
            )
            ORDER BY COALESCE(L1_R100.last_date_update, L1_R100.parser_time) desc
            limit 1
      )
      else 
      (
      		-- нет объекта из Росреестра: берем первый с самой последней датой
            SELECT 
              L1_R100.PRICE as "MarketObjectPrice"
              FROM MARKET_CORE_OBJECT L1_R100
              WHERE 
              (
                (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
                AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              )
              ORDER BY COALESCE(L1_R100.last_date_update, L1_R100.parser_time) desc
              limit 1
      )
    end
)
  
END
into price;

RETURN price;
  
END;

/* Для тестирования
--нет объекта, должно быть "-1"
select get_market_object_price_for_uprs('50:27:0020315:11');
--есть объект, должна быть цена
select get_market_object_price_for_uprs('50:26:0151306:108');
--есть объект, должно быть NULL
select get_market_object_price_for_uprs('77:08:0002007:2409');
*/
$function$;/*gbu_get_allpri_attribute_values 2973*/CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_values(objectids bigint[], attributeid bigint)
 RETURNS TABLE(objectid bigint, attributevalue character varying)
 LANGUAGE plpgsql
AS $function$
    declare
                _query character varying;
                _allpriTableName character varying;
                _allpriPartitioning bigint;
                _allpriTablePostfix character varying;
                 _fullAllpriTableName character varying;
       		 	_additionalConditionForTablesWithPartitionByData character varying;
                _attributeType bigint;
                _currentEndDate timestamp without time zone;

    begin

                if array_length(objectids, 1) IS NULL or array_length(objectids, 1)=0 then
                        return;
                end if;
                --raise notice '_array: %', array_length(ARRAY(1,2), 1);
                select CAST((CURRENT_DATE + INTERVAL '1 day - 1 second') AS TIMESTAMP) into _currentEndDate;

                select r.allpri_table, r.allpri_partitioning, a.type
                into _allpriTableName, _allpriPartitioning, _attributeType
                from core_register r
                join core_register_attribute a on a.registerid=r.registerid
                where a.id=attributeId;
                IF NOT FOUND THEN
                        return;
                END IF;

                if(_allpriPartitioning=2)then
                _allpriTablePostfix := CAST(attributeId as character varying);
        else
                case
                                when _attributeType=1 or _attributeType=2 or _attributeType=3
                                        then _allpriTablePostfix := 'NUM';
                                when _attributeType=4 then _allpriTablePostfix := 'TXT';
                                when _attributeType=5 then _allpriTablePostfix := 'DT';
                        end case;
        end if;

				_fullAllpriTableName :=  _allpriTableName || '_' || _allpriTablePostfix;
                _query := concat('select a.object_id as objectid, CAST(a.value as character varying) as attributeValue from ', _fullAllpriTableName, ' a where a.object_id in (', array_to_string(objectids, ','), ')');

                if _allpriPartitioning <> 2 then
                	_query = concat(_query, '  and a.attribute_id=', attributeId, ' and 
                      A.ID = (SELECT MAX(a2.id) FROM ', _fullAllpriTableName, ' a2 
                      WHERE a2.object_id = a.object_id AND a2.attribute_id = a.attribute_id AND 
                      a2.s <= ''', _currentEndDate, '''::timestamp without time zone AND 
                      a2.ot = (SELECT MAX(a3.ot) FROM ', _fullAllpriTableName, ' a3 WHERE a3.object_id = a.object_id AND 
                      a3.attribute_id = a.attribute_id AND a3.s <= ''', _currentEndDate, '''::timestamp without time zone ))');
                else
                	_query = concat(_query, ' AND a.s <= ''', _currentEndDate, '''::timestamp without time zone ',
                        ' and a.OT = (SELECT MAX(A2.OT) FROM ', _fullAllpriTableName, ' A2
                                                WHERE A2.object_id = a.object_id', ' AND A2.s <= ''', _currentEndDate, '''::timestamp without time zone )');
                end if;

 
                --raise notice '_query: %', _query;
                
                RETURN QUERY EXECUTE _query;

        END
$function$;/*gbu_get_allpri_attribute_value 2974*/CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_value(objectid bigint, attributeid bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
         _attributeValueString character varying;

	begin
                
		select attributevalue from gbu_get_allpri_attribute_values(ARRAY[objectid], attributeid) into _attributeValueString;
    	return _attributeValueString;

	END
$function$;/*get_parent_info 2975*/CREATE OR REPLACE FUNCTION public.get_parent_info("parentCadastralNumbers" character varying[], "buildingPurposeAttributeId" bigint, "constructionPurposeAttributeId" bigint, "groupAttributeId" bigint)
 RETURNS TABLE(purpose character varying, "group" character varying, "cadastralNumberOutPut" character varying)
 LANGUAGE plpgsql
AS $function$
    declare	
    	_objectId BIGINT;
        _objectTypeCode BIGINT;
		_resultPurposeAttributeId BIGINT;
		_purpose character varying;
        _group character varying;
        _parentCadastralNumber character varying;
        
    begin
      FOREACH _parentCadastralNumber IN ARRAY "parentCadastralNumbers"
      LOOP
          _objectId := 0;
          --ищем парент-объект 
          select 
              obj.id, obj.object_type_code into _objectId, _objectTypeCode
          from gbu_main_object obj where cadastral_number = _parentCadastralNumber limit 1;
		  
		  IF FOUND THEN
			--если тип объекта - "Здание"
            if(_objectTypeCode = 5) then
                _resultPurposeAttributeId := "buildingPurposeAttributeId";
            end if;
            --если тип объекта - "Сооружение"
            if(_objectTypeCode = 7) then
                _resultPurposeAttributeId := "constructionPurposeAttributeId";
            end if;
            
            select * from gbu_get_allpri_attribute_value(_objectId, _resultPurposeAttributeId) into _purpose;
            select * from gbu_get_allpri_attribute_value(_objectId, "groupAttributeId") into _group;
            
            RETURN QUERY SELECT _purpose, _group, _parentCadastralNumber;	
		  END IF;
          
      END LOOP;
      
    	/* для тестирования
      		select * from get_parent_info(ARRAY['77:22:0020229:2534', '77:22:0030404:31', '77:22:0020229:2534213'], 14, 22, 589)
      	*/
	END
$function$;/*notify_market_outliers_checking_updating 2976*/CREATE OR REPLACE FUNCTION public.notify_market_outliers_checking_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		PERFORM pg_notify('notify_market_outliers_checking_updating'::text, 'notify_market_outliers_checking_updating'::text);
		return null;
	end;
	$function$;/*get_first_zach_date 2977*/CREATE OR REPLACE FUNCTION public.get_first_zach_date(p_flat_id bigint)
 RETURNS timestamp without time zone
 LANGUAGE plpgsql
AS $function$
declare
 rval timestamp;
begin
with s as(
select f.emp_id
from insur_fsp_q f
where f.obj_id = p_flat_id and f.actual = 1
union 
select ff.fsp_id
from insur_link_fsp_flat ff
where ff.obj_id = p_flat_id
)
select min(iiz.period_reg_date)
into rval
from s 
join insur_input_plat iiz on iiz.fsp_id = s.emp_id
;
return rval;
END
$function$;/*notify_core_message_to_insert_and_update 2978*/CREATE OR REPLACE FUNCTION public.notify_core_message_to_insert_and_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
BEGIN
if (NEW.was_readed is null or (NEW.was_readed is not null and OLD.was_readed is null)) then
   notification = json_build_object(
                          'id', new.id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_to_insert_and_update'::text, notification::text);
		end if;
		return null;
END;
$function$;/*notify_core_message_update 2979*/CREATE OR REPLACE FUNCTION public.notify_core_message_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
BEGIN
if (NEW.is_urgent = 1 and (NEW.urgent_expire_date is null or NEW.urgent_expire_date > current_timestamp)) then
   notification = json_build_object(
                          'id', new.id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_update'::text, notification::text);
		end if;
		return null;
END;
$function$;/*notify_core_message_to_update 2980*/CREATE OR REPLACE FUNCTION public.notify_core_message_to_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
  is_send int;
BEGIN
is_send := (select 1 from core_messages where id = NEW.message_id and is_urgent = 1 and 
  (urgent_expire_date > current_timestamp or urgent_expire_date is null));
if (is_send = 1) then
   notification = json_build_object(
                          'id', new.message_id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_update'::text, notification::text);
		end if;
		return null;
END;
$function$;/*update_cache_table_for_data_composition_reports_for_registers 2981*/CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_registers()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
	row RECORD;
    is_attribute_already_added bigint;
    attribute_id bigint;

BEGIN
	/* Для Росреестра передаем ИД атрибута в параметры, для остальных реестров - берем из вновь вставленной строки */
	IF (TG_ARGV[0] IS NULL) THEN
    	attribute_id := NEW.attribute_id;
    ELSE
    	attribute_id := TG_ARGV[0];
    END IF;

    SELECT * INTO row FROM data_composition_by_characteristics_by_tables WHERE object_id = NEW.object_id;
    
    IF NOT FOUND THEN
        INSERT INTO data_composition_by_characteristics_by_tables (object_id, attributes) VALUES (NEW.object_id, array[ attribute_id ]);
    ELSE
    	/*Если атрибут не был добавлен ранее, то добавляем его*/
        IF (array_position(row.attributes, attribute_id) is NULL) THEN
    		update data_composition_by_characteristics_by_tables cache_table set attributes = array_append(attributes, attribute_id) 
            	where cache_table.object_id = NEW.object_id;
        END IF;
    END IF;

    
	RETURN NULL;
END;
$function$;/*update_cache_table_for_data_composition_reports_for_main_object 2982*/CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_main_object()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    INSERT INTO data_composition_by_characteristics_by_tables (object_id, cadastral_number, object_type_code) 
    VALUES (NEW.id, NEW.cadastral_number, NEW.object_type_code);    
	
    RETURN NULL;
END;
$function$;/*ko_delete_tasks 2983*/CREATE OR REPLACE FUNCTION public.ko_delete_tasks(VARIADIC tasks bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE	
	taskId bigint;
	_query character varying;
	gbuAttributeInfoRow record;
	registerData record;
	attachmentsIds bigint[];
	
	taskTourId bigint;
	taskDocumentId bigint;
	gbuObjectCount bigint;
BEGIN
	FOREACH taskId IN ARRAY tasks
    LOOP
    	RAISE NOTICE 'task_ID is %', taskId;
		
		select t.tour_id, t.document_id
		into taskTourId, taskDocumentId
		from ko_task t
		where t.id=taskId;
		IF NOT FOUND THEN
			RAISE NOTICE 'task was not found';
			return;
		END IF;
		
		RAISE NOTICE 'taskTourId is %', taskTourId;
		RAISE NOTICE 'taskDocumentId is %', taskDocumentId;
		
		select count(*) into gbuObjectCount from (
			select distinct u.object_id 
			from ko_unit u
			where u.task_id=taskId 
		) d;			
		RAISE NOTICE 'gbuObjects count=%', gbuObjectCount;

		/*удаление из ГБУ части значений связанных атрибутов*/
		FOR gbuAttributeInfoRow IN
			select a.id, r.allpri_table from core_register_attribute a
			join core_register r on a.registerid=r.registerid 
			where r.registerdescription='Источник: Росреестр' 
				and (a.primary_key=0 or a.primary_key is null) 
				and (a.is_deleted=0 or a.is_deleted is null)
			order by a.id
		LOOP
			_query := concat('delete from ', gbuAttributeInfoRow.allpri_table, '_', gbuAttributeInfoRow.id, ' attr ', 
							 ' where attr.change_doc_id=', taskDocumentId,
							 ' and exists (select 1 from ko_unit u where u.object_id=attr.object_id and u.task_id=', taskId, ')'
							);
			raise notice '%', _query;
			EXECUTE _query;
		END LOOP;

		
		/*удаление из КО части значений из таблиц срезов факторов*/	
		FOR registerData IN
			select r.quant_table
			from core_register r
			where r.registerid in (select fr.register_id
								  from ko_tour_factor_register fr
								  where fr.tour_id=taskTourId)
		LOOP
			_query := concat('delete from ', registerData.quant_table, ' t ',
							 ' where exists (select 1 from ko_unit u where u.task_id=', taskId, ' and u.id=t.id)'
							);
			raise notice '%', _query;
			EXECUTE _query;
		END LOOP;
		
		/*удаление из таблицы изменений юнитов*/
		RAISE NOTICE 'delete from ko_unit_change';
		delete from ko_unit_change where id_unit in (
			select id from ko_unit
			where task_id= taskId
		);
		
		/*удаление из таблицы данных о кадастровой стоимости из Росреестра*/
		RAISE NOTICE 'delete from KO_COST_ROSREESTR';
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		/*логическое удаление связанных с таской образов*/
		RAISE NOTICE 'update core_attachment_object';
		attachmentsIds := ARRAY(
			select distinct at1.id 
			from core_attachment at1
				left join core_attachment_object at_obj on at1.id=at_obj.attachment_id
			where at_obj.register_id=203 and at_obj.object_id=taskId 
				and (at1.is_deleted=0 or at1.is_deleted is null)
				and (at_obj.is_deleted=0 or at_obj.is_deleted is null)
				and not exists (select * 
								from core_attachment at2 
									join core_attachment_object at_obj2 on at2.id=at_obj2.attachment_id
								where at1.id=at2.id and (at_obj2.register_id<>203 or at_obj2.object_id<>taskId) 
									and (at2.is_deleted=0 or at2.is_deleted is null)
							   		and (at_obj2.is_deleted=0 or at_obj2.is_deleted is null))
		);	
		update core_attachment_object set is_deleted=1, deleted_date=CURRENT_DATE
		where attachment_id =any(attachmentsIds);		
		update core_attachment set is_deleted=1, deleted_date=CURRENT_DATE
		where id =any(attachmentsIds);
		
		/* удаление записей из журналов при загрузки юнитов*/
		RAISE NOTICE 'delete from core_long_process_queue';
		delete from core_long_process_queue
			where process_type_id in (select id from core_long_process_type where process_name ='DataImporterGkn')
			and object_id in (select id from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId);
		delete from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId;
		
		/*удаление связанных с таской юнитов*/
		RAISE NOTICE 'delete from ko_unit';
		delete from ko_unit where task_id=taskId;

		/*удаление таски*/
		RAISE NOTICE 'delete from ko_task';
		delete from ko_task where id=taskId;
		
    END LOOP;
END

$function$;/*synchronize_tables_with_deleted_data 2984*/CREATE OR REPLACE FUNCTION public.synchronize_tables_with_deleted_data()
 RETURNS event_trigger
 LANGUAGE plpgsql
AS $function$
	DECLARE r RECORD;
	BEGIN
		FOR r IN SELECT * FROM pg_event_trigger_ddl_commands() LOOP
		  IF ( r.objid::regclass::text = 'test_logical_delete_table' )
		  THEN
				RAISE EXCEPTION 'You are not allowed to change %', r.object_identity;
		  END IF;
		END LOOP;
	END;
	$function$;/*get_modeling_results 2985*/CREATE OR REPLACE FUNCTION public.get_modeling_results("taskIds" bigint[], "modelId" bigint, "groupId" bigint, "addressAttributeId" bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare	
        _columns varchar;
        _tables character varying;
        _counter BIGINT;
        _query varchar;
        _baseSelect varchar;
        registerInfoRow record;
        attributesInfowRow record;
    BEGIN
   
    _tables := '';
    _columns := '';
    _counter := 1;
    FOR registerInfoRow IN
            with attributesInfo as(
            select attribute.id, attribute.name, attribute.value_field, attribute.registerId, register.quant_table
                from KO_MODEL_FACTOR factor 
                    left join core_register_attribute attribute on factor.FACTOR_ID = attribute.Id
                    left join core_register register on attribute.RegisterId = register.registerid
                where factor.model_id = "modelId"
            order by attribute.Name
            )
            select registerId, max(quant_table) as quant_table from attributesInfo group by registerId
        LOOP 
            FOR attributesInfowRow IN
                --SELECT value_field, id from attributesInfo where register_Id = registerInfoRow.registerId
               with attributesInfo as(
                select attribute.id, attribute.name, attribute.value_field, attribute.registerId, register.quant_table
                    from KO_MODEL_FACTOR factor 
                        left join core_register_attribute attribute on factor.FACTOR_ID = attribute.Id
                        left join core_register register on attribute.RegisterId = register.registerid
                    where factor.model_id = "modelId"
                order by attribute.Name
                )
                select * from attributesInfo where registerId = registerInfoRow.registerId
                LOOP
                    _columns := concat(_columns, 'factorsTable', _counter, '.', attributesInfowRow.value_field, 
                    ' as "', attributesInfowRow.id, '", ');				
                END LOOP; 
    		
            _tables := concat(_tables, ' left join ', registerInfoRow.quant_table, ' factorsTable', _counter,
            ' on unit.id = factorsTable', _counter, '.Id ');
            _counter := _counter + 1;
        END LOOP;  
    	
        _baseSelect := concat('SELECT unit.Id, unit.PROPERTY_TYPE, unit.CADASTRAL_BLOCK,', 
        ' unit.CADASTRAL_NUMBER, unit.SQUARE, unit.UPKS, unit.CADASTRAL_COST, ',
        '(select * from  gbu_get_allpri_attribute_value( unit.id,', "addressAttributeId", ')) as Address ');
        if(_columns = '') then
            _query := concat(_query, _baseSelect);
        else
            _query := concat(_query, _baseSelect, ', ', LEFT(_columns,-2), ' ');
        end if;
        
        _query := concat(_query, 'FROM ko_unit unit ', _tables, ' WHERE unit.TASK_ID in (', array_to_string("taskIds", ','),
         ') and unit.GROUP_ID=', "groupId", ' order by unit.CADASTRAL_BLOCK');
        
        --RETURN QUERY EXECUTE _query;
        RETURN _query;
            /* для тестирования
                select * from get_modeling_results(ARRAY[2 , 3], 7977718, 10)
            */
    END
$function$;/*ko_delete_task 2986*/CREATE OR REPLACE FUNCTION public.ko_delete_task(taskid bigint)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    declare	
		_query character varying;
		gbuObject record;
		gbuAttributeInfoRow record;
		unitObject record; 
		attachmentsIds bigint[];
    begin
	
		--удаление из ГБУ части значений связанных атрибутов
		FOR gbuObject IN
			select distinct u.object_id, t.document_id, t.estimation_date
			from ko_unit u
				join ko_task t on t.id=u.task_id
			where u.task_id=taskid
		LOOP
			FOR gbuAttributeInfoRow IN
				select a.id, r.allpri_table from core_register_attribute a
				join core_register r on a.registerid=r.registerid 
				where r.registerdescription='Источник: Росреестр' 
					and (a.primary_key=0 or a.primary_key is null) 
					and (a.is_deleted=0 or a.is_deleted is null)
				order by a.id
			LOOP
				_query := concat('delete from ', gbuAttributeInfoRow.allpri_table, '_', gbuAttributeInfoRow.id, 
								 ' where object_id=', gbuObject.object_id,
								 ' and change_doc_id=', gbuObject.document_id,
								' and s= ''', gbuObject.estimation_date, '''::timestamp without time zone '
								' and ot= ''', gbuObject.estimation_date, '''::timestamp without time zone ');
				--raise notice '%', _query;
				EXECUTE _query;
				
			END LOOP;
		END LOOP;
		
		--удаление из КО части значений из таблиц срезов факторов
		FOR unitObject IN
			with unit_data as (
				select u.id,
				(select fr.register_id
					from ko_tour_factor_register fr
					where fr.tour_id=t.tour_id
					and case when u.property_type_code=4
							then fr.object_type_code=4
							else fr.object_type_code<>4
						end
				limit 1) as register_id
				from ko_unit u
					join ko_task t on t.id=u.task_id
				where u.task_id=taskid)
			select 	d.id, r.quant_table
			from unit_data d
				join core_register r on d.register_id=r.registerid
		LOOP
			_query := concat('delete from ', unitObject.quant_table,
							' where id=', unitObject.id);
			--raise notice '%', _query;
			EXECUTE _query;
		END LOOP;
		
		--удаление из таблицы изменений юнитов
		delete from ko_unit_change where id_unit in (
			select id from ko_unit
			where task_id= taskid
		);
		
		--удаление из таблицы данных о кадастровой стоимости из Росреестра
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		--удаление из таблицы данных о кадастровой стоимости из Росреестра
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		--логическое удаление связанных с таской образов
		attachmentsIds := ARRAY(
			select distinct at1.id 
			from core_attachment at1
				left join core_attachment_object at_obj on at1.id=at_obj.attachment_id
			where at_obj.register_id=203 and at_obj.object_id=taskId 
				and (at1.is_deleted=0 or at1.is_deleted is null)
				and (at_obj.is_deleted=0 or at_obj.is_deleted is null)
				and not exists (select * 
								from core_attachment at2 
									join core_attachment_object at_obj2 on at2.id=at_obj2.attachment_id
								where at1.id=at2.id and (at_obj2.register_id<>203 or at_obj2.object_id<>taskId) 
									and (at2.is_deleted=0 or at2.is_deleted is null)
							   		and (at_obj2.is_deleted=0 or at_obj2.is_deleted is null))
		);	
		update core_attachment_object set is_deleted=1, deleted_date=CURRENT_DATE
		where attachment_id =any(attachmentsIds);		
		update core_attachment set is_deleted=1, deleted_date=CURRENT_DATE
		where id =any(attachmentsIds);
		
		-- удаление записей из журналов при загрузки юнитов
		delete from core_long_process_queue
			where process_type_id in (select id from core_long_process_type where process_name ='DataImporterGkn')
			and object_id in (select id from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId);
		delete from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId;
		
		--удаление связанных с таской юнитов
		delete from ko_unit where task_id=taskId;

		--удаление таски
		delete from ko_task where id=taskId;
	END
$function$;/*deconfigure_logging_for_register 2987*/CREATE OR REPLACE FUNCTION public.deconfigure_logging_for_register(register_code bigint, drop_history_table boolean DEFAULT false, detach_history_table boolean DEFAULT false, remove_user_tracking boolean DEFAULT false, remove_date_tracking boolean DEFAULT false)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
begin
    select *
    from core_register
    where registerid = register_code
    into reg;

    if not found then
        raise exception 'Реестр с номером % не найден', register_code;
    end if;

    if remove_user_tracking then
        execute format('alter table %s drop column if exists CHANGE_USER_ID;', reg.quant_table);
        update core_register set track_changes_userid = NULL where registerid = reg.registerid;
    end if;

    if remove_date_tracking then
        execute format('alter table %s drop column if exists CHANGE_DATE;', reg.quant_table);
        update core_register set track_changes_date = NULL where registerid = reg.registerid;
    end if;

    if detach_history_table then
        update core_register set allpri_table = NULL where registerid = reg.registerid;
    end if;

    if drop_history_table then
        update core_register set allpri_table = NULL where registerid = reg.registerid;
        execute format('drop table %s', reg.allpri_table);
    end if;

    return reg.registerid;
end
$function$;/*configure_logging_for_register 2988*/CREATE OR REPLACE FUNCTION public.configure_logging_for_register(register_code bigint, create_history_table boolean DEFAULT false, track_user boolean DEFAULT true, track_change_date boolean DEFAULT true)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
begin
    select *
    from core_register
    where registerid = register_code
    into reg;

    if not found then
        raise exception 'Реестр с номером % не найден', register_code;
    end if;

    if track_user then
        execute format('alter table %s add column if not exists CHANGE_USER_ID bigint;', reg.quant_table);
        update core_register set track_changes_userid = 'CHANGE_USER_ID' where registerid = reg.registerid;
    end if;

    if track_change_date then
        execute format('alter table %s add column if not exists CHANGE_DATE timestamp;', reg.quant_table);
        update core_register set track_changes_date = 'CHANGE_DATE' where registerid = reg.registerid;
    end if;

    if create_history_table then
        execute format('create table %s_a
        (
            id             bigint not null
                constraint %1$s_a_pkey
                    primary key,
            object_id      bigint not null,
            attribute_id   bigint not null,
            s              timestamp,
            po             timestamp,
            ref_item_id    bigint,
            text_value     varchar,
            number_value   numeric,
            date_value     timestamp,
            change_user_id bigint
        );

        alter table %1$s_a
            owner to cipjs_kad_ozenka;

        grant select on %1$s_a to test_role;

        create index %1$s_a_obj_attr_idx
        on %1$s_a (object_id, attribute_id);', reg.quant_table);
        update core_register set allpri_table = concat(reg.quant_table,'_A') where registerid = reg.registerid;
    end if;

    return reg.registerid;
end
$function$;/*create_trigger_for_new_gbu_source_table_with_partition_by_data 2989*/CREATE OR REPLACE FUNCTION public.create_trigger_for_new_gbu_source_table_with_partition_by_data()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
	prefix text;
    table_name text;
    trigger_name text;
	prefixes text[3];

BEGIN
	prefixes := ARRAY ['txt', 'num', 'dt'];
    
	FOREACH prefix IN ARRAY prefixes
      LOOP
      	  table_name := NEW.allpri_table || '_' || prefix;
          trigger_name := 'trigger_for_' || table_name;
          EXECUTE 'DROP TRIGGER IF EXISTS ' || trigger_name || ' ON ' || table_name || ';' ||
                              'CREATE TRIGGER ' ||  trigger_name ||
                              ' AFTER INSERT 
                              ON ' || table_name || '
                              FOR EACH ROW
                              EXECUTE FUNCTION update_cache_table_for_data_composition_reports_for_registers();';

      END LOOP;
	
	RETURN NULL;
END;
$function$;/*common_registers_with_soft_deletion_update_trg 2990*/CREATE OR REPLACE FUNCTION public.common_registers_with_soft_deletion_update_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
declare
	register_code bigint;
    reg core_register%rowtype;
	gbuAttributeInfoRow record;
	allpri_table_name text;
begin
		if TG_OP = 'DELETE' then register_code := OLD.register_id;
           else register_code := NEW.register_id;
         end if;
		 
		select *
		from core_register
		where registerid = register_code
		into reg;
		if not found then
			raise exception 'Реестр с номером % не найден', register_code;
		end if;
		
		if reg.storage_type=4 then 
			CASE TG_OP
			WHEN 'INSERT' THEN	
				execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_idx ON %1$s_deleted (EVENT_ID)', NEW.main_table_name, NEW.register_id);
			WHEN 'DELETE' THEN
			   execute format('DROP TABLE IF EXISTS %s_deleted', OLD.main_table_name);
			WHEN 'UPDATE' THEN
			   raise exception 'Операция обновления не поддерживается для таблицы common_registers_with_soft_deletion. Удалите и создайте запись заново';
			END CASE;
		elsif reg.storage_type=5 THEN
			CASE TG_OP
			WHEN 'INSERT' THEN	
				if reg.ALLPRI_PARTITIONING=1 then	
					allpri_table_name := concat(NEW.main_table_name, '_NUM');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_num_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
							   
				    allpri_table_name := concat(NEW.main_table_name, '_DT');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_dt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
							   
				    allpri_table_name := concat(NEW.main_table_name, '_TXT');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_txt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
				elsif reg.ALLPRI_PARTITIONING=2 THEN
					FOR gbuAttributeInfoRow IN
						select a.id from core_register_attribute a
						where a.registerid=register_code
							and (a.primary_key=0 or a.primary_key is null) 
							and (a.is_deleted=0 or a.is_deleted is null)
						order by a.id
					LOOP
						allpri_table_name := concat(NEW.main_table_name, '_', gbuAttributeInfoRow.id);
			   			execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_%3$s_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id, gbuAttributeInfoRow.id);
					END LOOP;
				else 
					raise exception 'Неподдерживаемый тип разбиения allpri: %', reg.ALLPRI_PARTITIONING; 
				end if;
				
			WHEN 'DELETE' THEN
				if reg.ALLPRI_PARTITIONING=1 then	
					allpri_table_name := concat(OLD.main_table_name, '_NUM');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					
					allpri_table_name := concat(OLD.main_table_name, '_DT');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					
					allpri_table_name := concat(OLD.main_table_name, '_TXT');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
				elsif reg.ALLPRI_PARTITIONING=2 THEN
					FOR gbuAttributeInfoRow IN
						select a.id from core_register_attribute a
						where a.registerid=register_code
							and (a.primary_key=0 or a.primary_key is null) 
							and (a.is_deleted=0 or a.is_deleted is null)
						order by a.id
					LOOP
						allpri_table_name := concat(OLD.main_table_name, '_', gbuAttributeInfoRow.id);
			   			execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					END LOOP;
				else 
					raise exception 'Неподдерживаемый тип разбиения allpri: %', reg.ALLPRI_PARTITIONING;
				end if;
			   
			WHEN 'UPDATE' THEN
			   raise exception 'Операция обновления не поддерживается для таблицы common_registers_with_soft_deletion. Удалите и создайте запись заново';
			END CASE;
			
		else 
			raise exception 'Реестр с типом хранения данных % не поддерживается', reg.storage_type;
        end if;
		
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;/*core_register_attr_sync_delete_table_trg 2991*/CREATE OR REPLACE FUNCTION public.core_register_attr_sync_delete_table_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
declare
	register_code bigint;
    reg core_register%rowtype;
	reg_with_soft_deletion common_registers_with_soft_deletion%rowtype;
	command text;
	allpri_table_name text;
	value_type text;
	ref_item_col text;
begin
		if TG_OP = 'DELETE' then register_code := OLD.registerid;
           else register_code := NEW.registerid;
         end if;
		 
		select *
		from core_register
		where registerid = register_code
		into reg;
		if not found then
			raise exception 'Реестр с номером % не найден', register_code;
		end if;
		
		
			if TG_OP='INSERT' THEN
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;

				if not found then
					return NEW;
				end if;
				
				if reg.storage_type=4 then
					if NEW.value_field is not null and NEW.value_field <> '' then
						command := format('ALTER TABLE %1$s_deleted ADD COLUMN IF NOT EXISTS %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);

						CASE NEW.type
							WHEN 1 THEN
								command := concat(command,'  BIGINT');
							WHEN 2 THEN
								command := concat(command,'  NUMERIC');
							WHEN 3 THEN
								command := concat(command,'  SMALLINT');
							WHEN 4 THEN
								command := concat(command,'  VARCHAR(250)');
							WHEN 5 THEN
								command := concat(command,'  TIMESTAMP');
							WHEN 6 THEN
								command := concat(command,'  BYTEA');
							ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
						END CASE;

						if NEW.is_nullable is null or NEW.is_nullable<>1 then
							command := concat(command,'  NOT NULL');
						end if;
						execute command;
					end if;

					if NEW.code_field is not null and NEW.code_field <> '' then
						command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s BIGINT', reg_with_soft_deletion.main_table_name, NEW.code_field);
						if NEW.is_nullable is null or NEW.is_nullable<>1 then
							command := concat(command,'  NOT NULL');
						end if;

						execute command;
					end if;
				elsif reg.storage_type=5 THEN
					if reg.ALLPRI_PARTITIONING=1 then
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_NUM');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									value numeric, 
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_num_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);
						
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_DT');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									value timestamp without time zone, 
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_dt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);
								   
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_TXT');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									ref_item_id bigint,
    								value character varying(4000),
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_txt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);

					elsif reg.ALLPRI_PARTITIONING=2 THEN
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', NEW.id);
						ref_item_col := '';
						CASE NEW.type
							WHEN 1 THEN
								value_type := 'BIGINT';
							WHEN 2 THEN
								value_type := 'NUMERIC';
							WHEN 3 THEN
								value_type := 'SMALLINT';
							WHEN 4 THEN
								value_type := 'VARCHAR(4000)';
								ref_item_col := 'ref_item_id bigint,';
							WHEN 5 THEN
								value_type := 'timestamp without time zone';
							WHEN 6 THEN
								value_type := 'BYTEA';
						END CASE;
						
						command := format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (id bigint NOT NULL,
								object_id bigint NOT NULL,
								ot timestamp without time zone NOT NULL,
								s timestamp without time zone NOT NULL,
								value %3$s,
								%4$s	
								change_date timestamp without time zone NOT NULL,
								change_user_id bigint NOT NULL,
								change_doc_id bigint, 
							    EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_%5$s_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid, value_type, ref_item_col, NEW.id);
						execute	command;
					end if;
				end if;

			elsif TG_OP='UPDATE' THEN
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;
				if  not found then
					return NEW;
				end if;
				
				if reg.storage_type=4 then 
					if (NEW.value_field is null and OLD.value_field is not null) or (NEW.value_field is not null and OLD.value_field is null) or  NEW.value_field <> OLD.value_field then
						if(OLD.value_field is not null and OLD.value_field<>'') then
							command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s ', reg_with_soft_deletion.main_table_name, OLD.value_field);
							execute command;
						end if;
						if NEW.value_field is not null and NEW.value_field <> '' then
							command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);

							CASE NEW.type
								WHEN 1 THEN
									command := concat(command,'  BIGINT');
								WHEN 2 THEN
									command := concat(command,'  NUMERIC');
								WHEN 3 THEN
									command := concat(command,'  SMALLINT');
								WHEN 4 THEN
									command := concat(command,'  VARCHAR(255)');
								WHEN 5 THEN
									command := concat(command,'  TIMESTAMP');
								WHEN 6 THEN
									command := concat(command,'  BYTEA');
								ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
							END CASE;

							if NEW.is_nullable is null or NEW.is_nullable<>1 then
								command := concat(command,'  NOT NULL');
							end if;

							execute command;
						end if;
					else
						if NEW.value_field is not null and NEW.value_field <> '' then
							if NEW.type<>OLD.type then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);
								CASE NEW.type
										WHEN 1 THEN
											command := concat(command,'TYPE  BIGINT USING ', NEW.value_field,'::bigint');
										WHEN 2 THEN
											command := concat(command,'TYPE  NUMERIC USING ', NEW.value_field,'::NUMERIC');
										WHEN 3 THEN
											command := concat(command,'TYPE  SMALLINT USING ', NEW.value_field,'::SMALLINT');
										WHEN 4 THEN
											command := concat(command,'TYPE  VARCHAR(255) USING ', NEW.value_field,'::VARCHAR(255)');
										WHEN 5 THEN
											command := concat(command,'TYPE  TIMESTAMP USING ', NEW.value_field,'::timestamp without time zone');
										WHEN 6 THEN
											command := concat(command,'TYPE  BYTEA USING ', NEW.value_field,'::BYTEA');
										ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
								END CASE;
								execute command;
							end if;

							if (NEW.is_nullable is null and OLD.is_nullable is not null) or (NEW.is_nullable is not null and OLD.is_nullable is null) or NEW.is_nullable<>OLD.is_nullable then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);
								if (NEW.is_nullable is null or NEW.is_nullable<>1) and OLD.is_nullable=1 then
									command := concat(command,' SET NOT NULL');
								end if;
								if NEW.is_nullable=1 and (OLD.is_nullable is null or OLD.is_nullable<>1) then
									command := concat(command,' DROP NOT NULL');
								end if;

								execute command;
							end if;
						end if;
					end if;

					if (NEW.code_field is null and OLD.code_field is not null) or (NEW.code_field is not null and OLD.code_field is null) or NEW.code_field <> OLD.code_field then
						if(OLD.code_field is not null and OLD.code_field<>'') then
							command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s ', reg_with_soft_deletion.main_table_name, OLD.code_field);
							execute command;
						end if;

						if NEW.code_field is not null and NEW.code_field <> '' then
							command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s BIGINT', reg_with_soft_deletion.main_table_name, NEW.code_field);
							if NEW.is_nullable is null or NEW.is_nullable<>1 then
								command := concat(command,'  NOT NULL');
							end if;

							execute command;
						end if;
					else
						if NEW.code_field is not null and NEW.code_field <> '' then
							if (NEW.is_nullable is null and OLD.is_nullable is not null) or (NEW.is_nullable is not null and OLD.is_nullable is null) or NEW.is_nullable<>OLD.is_nullable then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.code_field);
								if NEW.is_nullable is null or NEW.is_nullable<>1 then
									command := concat(command,' SET NOT NULL');
								end if;
								if NEW.is_nullable=1 and (OLD.is_nullable is null or OLD.is_nullable<>1) then
									command := concat(command,' DROP NOT NULL');
								end if;

								execute command;
							end if;
						end if;
					end if;
				elsif reg.storage_type=5 and reg.ALLPRI_PARTITIONING=2 and NEW.type<>OLD.type THEN
					allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', NEW.id);
					command := format('ALTER TABLE %s_deleted ALTER COLUMN value ', allpri_table_name);
					CASE NEW.type
							WHEN 1 THEN
								command := concat(command,'TYPE  BIGINT USING value::bigint');
							WHEN 2 THEN
								command := concat(command,'TYPE  NUMERIC USING value::NUMERIC');
							WHEN 3 THEN
								command := concat(command,'TYPE  SMALLINT USING value::SMALLINT');
							WHEN 4 THEN
								command := concat(command,'TYPE  VARCHAR(4000) USING value::VARCHAR(255)');
							WHEN 5 THEN
								command := concat(command,'TYPE  TIMESTAMP USING value::timestamp without time zone');
							WHEN 6 THEN
								command := concat(command,'TYPE  BYTEA USING value::BYTEA');
							ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
					END CASE;
					execute command;
					if NEW.type=4 then
						execute format('ALTER TABLE %s_deleted ADD COLUMN ref_item_id bigint', allpri_table_name);
					end if;
					if OLD.type=4 then
						execute format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS ref_item_id', allpri_table_name);
					end if;
								
				end if;
				
			else	
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;
				if not found then
					return OLD;
				end if;
				
				if reg.storage_type=4 then 
					if OLD.value_field is not null and OLD.value_field <> '' then
						command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s', reg_with_soft_deletion.main_table_name, OLD.value_field);					
						execute command;
					end if;

					if OLD.code_field is not null and OLD.code_field <> '' then
						command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s', reg_with_soft_deletion.main_table_name, OLD.code_field);					
						execute command;
					end if;	
				elsif reg.storage_type=5 and reg.ALLPRI_PARTITIONING=2 then
					allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', OLD.id);
					execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
				end if;
		END IF;
		 
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;/*create_source_register 2992*/CREATE OR REPLACE FUNCTION public.create_source_register(register_code integer, register_description text, partitioning_type integer DEFAULT 1)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
begin
    insert into core_register
    values (register_code, concat('Gbu.Source',register_code), concat('Источник: ',register_description),
            concat('GBU_SOURCE',register_code,'_A'), null, 'GBU_MAIN_OBJECT', null, 5, 'REG_OBJECT_SEQ', 0, 0, null, null,
            null, 0, partitioning_type, 200);
    insert into core_register_attribute (id,name,registerid,type,value_field,is_deleted,change_date,hidden,primary_key)
    values (register_code*100000+100, 'Идентификатор', register_code, 1, 'ID', 0, now(),0,1);
end
$function$;/*create_source_register_attribute 2993*/CREATE OR REPLACE FUNCTION public.create_source_register_attribute(attribute_id integer, description text, register_id integer, type integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
begin
    insert into core_register_attribute (id,name,registerid,type,is_deleted,change_date,hidden)
    values (attribute_id, description, register_id, type, 0, now(),0);
end
$function$;/*create_source_register_table_for_datatype_partitioning 2994*/CREATE OR REPLACE FUNCTION public.create_source_register_table_for_datatype_partitioning(register_id integer, datatype text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
    script text;
begin
    select *
    from core_register
    where registerid = register_id
    into reg;

    script := format('create table gbu_source%1$s_a_%2$s
(
    id             bigint    not null
        constraint reg_%1$s_a_%2$s_pk
            primary key,
    object_id      bigint    not null
        constraint reg_%1$s_a_%2$s_fk_o
            references gbu_main_object,
    attribute_id   bigint    not null,
    ot             timestamp not null,
    s              timestamp not null,
    ',register_id,datatype);

    if datatype='txt'
    then script := concat(script,
        'ref_item_id    bigint,
         value varchar(5000),');
    elsif datatype='num'
    then script := concat(script, 'value numeric,');
    elsif datatype='dt'
    then script := concat(script, 'value timestamp,');
    else raise 'Неизвестный тип данных';
    end if;

    script := concat(script, '
    change_id      bigint,
    change_date    timestamp not null,
    change_user_id bigint    not null,
    change_doc_id  bigint)');

    execute script;
    execute format('create index reg_%1$s_a_%2$s_inx_obj_attr_id
    on gbu_source%1$s_a_%2$s (object_id, attribute_id);',register_id,datatype);
end
$function$;/*create_source_register_table_for_attribute_partitioning 2995*/CREATE OR REPLACE FUNCTION public.create_source_register_table_for_attribute_partitioning(attribute_id bigint)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
    attr core_register_attribute%rowtype;
    script text;
begin
    select *
    from core_register_attribute
    where id = attribute_id
    into attr;

    if attr.type = 1 then return;
    end if;

    script := format('create table gbu_source%1$s_a_%2$s
(
    id             bigint    not null
        constraint reg_%1$s_a_%2$s__pk
            primary key,
    object_id      bigint    not null
        constraint reg_%1$s_a_%2$s_fk_o
            references gbu_main_object,
    ot             timestamp not null,
    s              timestamp not null,
    ',attr.registerid,attr.id);

    if attr.type=2
    then script := concat(script,'value          numeric,');
    elsif attr.type=3
    then script := concat(script,
        'ref_item_id    bigint,
         value smallint,');
    elsif attr.type=4
    then script := concat(script,
        'ref_item_id    bigint,
         value varchar(5000),');
    elsif attr.type=5
    then script := concat(script, 'value timestamp,');
    else raise 'Неизвестный тип данных';
    end if;

    script := concat(script, '
    change_date    timestamp not null,
    change_user_id bigint    not null,
    change_doc_id  bigint)');

    execute script;
    execute format('create index reg_%1$s_a_%2$s_inx_obj_attr_id
    on gbu_source%1$s_a_%2$s (object_id, ot);',attr.registerid,attr.id);
end
$function$;/*create_source_register_tables_from_records 2996*/CREATE OR REPLACE FUNCTION public.create_source_register_tables_from_records(register_id integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
    attr core_register_attribute%rowtype;
begin
    select *
    from core_register
    where registerid = register_id
    into reg;

    if reg.allpri_partitioning = 1
    then
        perform create_source_register_table_for_datatype_partitioning(register_id,'txt');
        perform create_source_register_table_for_datatype_partitioning(register_id,'num');
        perform create_source_register_table_for_datatype_partitioning(register_id,'dt');
    end if;

    if reg.allpri_partitioning = 2
    then
        select *
        from core_register_attribute
        where registerid = register_id
        into attr;
        for attr in execute format('select * from core_register_attribute where registerid=%s',attr.registerid) loop
            perform create_source_register_table_for_attribute_partitioning(attr.id);
        end loop;
    end if;
end
$function$;/*notify_ko_grouping_dictionaries_updating 2997*/CREATE OR REPLACE FUNCTION public.notify_ko_grouping_dictionaries_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
begin
    PERFORM pg_notify('notify_ko_grouping_dictionaries_updating'::text, 'notify_ko_grouping_dictionaries_updating'::text);
    return null;
end;
$function$;