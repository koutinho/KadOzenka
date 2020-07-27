CREATE OR REPLACE FUNCTION public.core_register_pkg_getorcreatechangeset(itdinstanceid bigint, iuserid bigint, istatus bigint, changesetdate timestamp without time zone, OUT ichangesetid bigint)
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
END;
$function$
