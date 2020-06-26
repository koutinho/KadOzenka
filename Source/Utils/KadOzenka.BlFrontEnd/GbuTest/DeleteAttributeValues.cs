using Core.Register;
using Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KadOzenka.BlFrontEnd.GbuTest
{
    public static class DeleteAttributeValues
    {
        const string Attributes =
@"1595
1596
1597
1598
1599
1600
1601
1603
1606
1607
1609
1610
1611
1612
1613
1614
1615
1616
1617
1618
1619
1620
1621
1622
1623
1624
1638
1639
1640
1642
1643
1644
1645
1646
1647
1648
1649
1650
1653
1654
1655
1656
1657
1658
1659
1660
1661
1662
1663
1664
1665
1666
1667
1668
1669
1670
1671
1672
1673
1674
1675
1676
1677
1678
1679
1680
1681
1682
1683
1684
1685
1686
1687
1688
1689
1690
1691
1692
1693
1694
1695
1696
1697
1698
1699
1700
1701
1702
1703
1704
1706
1707
1708
1709
1710
1711
1712
1713
1714
1715
1716";

        public static string GenerateSql()
        {
            string sql = String.Empty;

            List<long> attributesIds = Attributes.Split("\n").Select(x => x.Trim().ParseToLong()).ToList();

            attributesIds.ForEach(x =>
            {
                var attributeData = Core.Register.RegisterCache.GetAttributeData(x);
                var registerData = Core.Register.RegisterCache.GetRegisterData(attributeData.RegisterId);

                if(registerData.StorageType == Core.Register.StorageType.Type5)
                {
                    if(registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
                    {
                        sql += $"delete from {registerData.AllpriTable}_{x};\n";
                    }
                    else
                    {
                        string postfix = String.Empty;

                        switch (attributeData.Type)
                        {
                            case RegisterAttributeType.INTEGER:
                            case RegisterAttributeType.DECIMAL:
                            case RegisterAttributeType.BOOLEAN:
                                postfix = "NUM";
                                break;
                            case RegisterAttributeType.DATE:
                                postfix = "DT";
                                break;
                            default:
                                postfix = "TXT";
                                break;
                        }

                        sql += $"delete from {registerData.AllpriTable}_{postfix} t where t.attribute_id = {x};\n";
                    }
                }


            });

            return sql;
        }
    }
}
