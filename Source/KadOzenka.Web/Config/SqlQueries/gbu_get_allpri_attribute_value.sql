CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_value (
  objectid bigint,
  attributeid bigint
)
RETURNS varchar AS
$body$
    declare	
		_query character varying;
		
		_allpriTableName character varying;
		_allpriPartitioning bigint;
		_allpriTablePostfix character varying;
		_attributeType bigint;
		_currentEndDate timestamp without time zone;
		
		_attributeValueString character varying;
    begin
		select CAST((CURRENT_DATE + INTERVAL '1 day - 1 second') AS TIMESTAMP) into _currentEndDate;
		
		select r.allpri_table, r.allpri_partitioning, a.type
		into _allpriTableName, _allpriPartitioning, _attributeType
		from core_register r
		join core_register_attribute a on a.registerid=r.registerid
		where a.id=attributeId;
		IF NOT FOUND THEN
			return null;
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

		_query := concat('select value from ', _allpriTableName, '_', _allpriTablePostfix, ' a where a.object_id=', objectId);
		
		if _allpriPartitioning <> 2 then
			_query = concat(_query, '  and a.attribute_id=', attributeId);
		end if;
		
		_query = concat(_query, ' AND a.s <= ''', _currentEndDate, '''::timestamp without time zone ',
			' and a.OT = (SELECT MAX(A2.OT) 
						FROM ', _allpriTableName, '_', _allpriTablePostfix, ' A2 
						WHERE A2.object_id = a.object_id  AND A2.s <= ''', _currentEndDate, '''::timestamp without time zone )');

		EXECUTE _query into _attributeValueString;
		
		return _attributeValueString;
		
	END
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.gbu_get_allpri_attribute_value (objectid bigint, attributeid bigint)
  OWNER TO cipjs_kad_ozenka;