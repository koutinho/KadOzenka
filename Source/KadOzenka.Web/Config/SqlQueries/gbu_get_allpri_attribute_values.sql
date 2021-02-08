CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_values (
  objectids bigint [],
  attributeid bigint
)
RETURNS TABLE (
  objectid bigint,
  attributevalue varchar
) AS
$body$
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
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100 ROWS 1000;

ALTER FUNCTION public.gbu_get_allpri_attribute_values (objectids bigint [], attributeid bigint)
  OWNER TO cipjs_kad_ozenka;