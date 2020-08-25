CREATE OR REPLACE FUNCTION public.get_market_object_price_for_uprs (
  cadastral_number varchar
)
RETURNS numeric AS
$body$
 declare
	price numeric;
        
BEGIN


select
-- ���������, ���� �� ������-������ � ���������� ��
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
	-- ���� ������� ���
  	-1
 )
 
ELSE
(
    -- ���� ������� ����, ������� - ���� �� ������, ����������� �� ���������� (market_code = 737)
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
          -- ���� ������� �� ���������: ����� ������ � ����� ��������� �����
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
      		-- ��� ������� �� ����������: ����� ������ � ����� ��������� �����
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

/* ��� ������������
--��� �������, ������ ���� "-1"
select get_market_object_price_for_uprs('50:27:0020315:11');
--���� ������, ������ ���� ����
select get_market_object_price_for_uprs('50:26:0151306:108');
--���� ������, ������ ���� NULL
select get_market_object_price_for_uprs('77:08:0002007:2409');
*/
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.get_market_object_price_for_uprs (cadastral_number varchar)
  OWNER TO cipjs_kad_ozenka;