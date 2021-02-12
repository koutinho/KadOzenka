namespace KadOzenka.Dal.LongProcess.Modeling.InputParameters
{
    public class ObjectFormationInputParameters
    {
        public long ModelId{ get; set; }
        public bool IsExcludeMarketObjectsWithoutUnit { get; set; } = true;
    }
}
