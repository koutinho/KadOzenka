using System.Collections.Generic;

namespace KadOzenka.Dal.Modeling.Dto
{
	public class TrainingSet
    {
		public List<string> AttributeNames { get; set; }
		public List<decimal> Coefficients { get; set; }
        public List<decimal> Prices { get; set; }

        public TrainingSet()
		{
			AttributeNames = new List<string>();
			Coefficients = new List<decimal>();
            Prices = new List<decimal>();
        }
	}
}
