CREATE OR REPLACE FUNCTION public.fias_addressobjects_addressobjecttree(a_aoguid character varying, a_currstatus integer DEFAULT NULL::integer)
 RETURNS TABLE(rtf_aoguid character varying, rtf_currstatus integer, rtf_actstatus integer, rtf_aolevel integer, rtf_shorttypename character varying, rtf_addressobjectname character varying)
 LANGUAGE plpgsql
AS $function$
DECLARE
 c_ActualStatusCode CONSTANT INTEGER :=1;    /* Признак актуальной записи адресообразующего элемента */
 c_NotActualStatusCode CONSTANT INTEGER :=0; /* Значение кода актуальной записи */
 v_AOGUID        VARCHAR(36); /* ИД адресообразующего элемента */
 v_ParentGUID    VARCHAR(36); /* Идентификатор родительского элемента */
 v_CurrStatus    INTEGER;     /* Статус актуальности КЛАДР 4*/
 v_ActStatus     INTEGER;     /* Статус актуальности адресообразующего элемента ФИАС. */
 v_AOLevel       INTEGER;     /* Уровень адресообразующего элемента  */
 v_ShortName     VARCHAR(10); /* Краткое наименование типа элемента */
 v_FormalName    VARCHAR(120);/* Формализованное наименование элемента */
 v_Return_Error  INTEGER;     /* Код возврата */
--***********************************************************************
--***********************************************************************
 BEGIN
 IF a_CurrStatus IS NOT NULL THEN
    SELECT INTO  v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                               ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                              ao.ShortName, ao.FormalName
                  FROM fias_addrobj ao
	WHERE ao.AOGUID=a_AOGUID AND ao.CurrStatus=a_CurrStatus;
 ELSE
    SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                              ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                              ao.ShortName, ao.FormalName
                   FROM fias_addrobj ao
	WHERE ao.AOGUID=a_AOGUID AND ao.ActStatus=c_ActualStatusCode;
   IF NOT FOUND THEN
      SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                               v_ShortName, v_FormalName
                                  ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                                ao.ShortName, ao.FormalName
              FROM fias_addrobj ao
              WHERE ao.AOGUID=a_AOGUID 
                       AND ao.ActStatus=c_NotActualStatusCode
                      AND ao.currstatus = (SELECT MAX(iao.currstatus) 
                                                                 FROM fias_addrobj iao 
                                                                 WHERE ao.aoguid = iao.aoguid);
    END IF;
 END IF;
 
 IF v_AOGUID IS NOT NULL THEN
 	RETURN QUERY SELECT v_AOGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                                               v_ShortName,v_FormalName;
 END IF;
 
 WHILE v_ParentGUID IS NOT NULL LOOP
     SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                              v_ShortName, v_FormalName
                           ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                             ao.ShortName,ao.FormalName
         FROM fias_addrobj ao
         WHERE ao.AOGUID=v_ParentGUID AND ao.ActStatus=c_ActualStatusCode;
          IF NOT FOUND THEN   
             SELECT INTO v_AOGUID,v_ParentGUID,v_CurrStatus,v_ActStatus,v_AOLevel,
                                        v_ShortName,v_FormalName
                             ao.AOGUID,ao.ParentGUID,ao.CurrStatus,ao.ActStatus,ao.AOLevel,
                                        ao.ShortName, ao.FormalName
                 FROM fias_addrobj ao
                 WHERE ao.AOGUID=v_ParentGUID 
                               AND ao.ActStatus=c_NotActualStatusCode
                              AND ao.currstatus = (SELECT MAX(iao.currstatus) 
                                                               FROM fias_addrobj iao 
                                                               WHERE ao.aoguid = iao.aoguid);
          END IF;
          
          IF v_AOGUID IS NOT NULL THEN
             RETURN QUERY SELECT v_AOGUID,v_CurrStatus,v_ActStatus,v_AOLevel,v_ShortName,
                                                   v_FormalName;
          END IF;
          
 END LOOP;
END;
$function$
