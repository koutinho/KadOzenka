CREATE OR REPLACE FUNCTION public.get_market_object_price_for_uprs (
  cadastral_number varchar
)
RETURNS numeric AS
$body$
 declare
	price numeric;
        
BEGIN
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
  into price;
  
  RETURN price;
  
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.get_market_object_price_for_uprs (cadastral_number varchar)
  OWNER TO cipjs_kad_ozenka;