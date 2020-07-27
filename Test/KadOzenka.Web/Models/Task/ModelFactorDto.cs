namespace KadOzenka.Web.Models.Task
{
	public class ModelFactorDto
	{
		public long Id { get; set; }
		public long? ModelId { get; set; }
		public long? FactorId { get; set; }
		public string Factor { get; set; }
		public long? MarkerId { get; set; }
		public decimal Weight { get; set; }
		public decimal B0 { get; set; }
		public bool SignDiv { get; set; }
		public bool SignAdd { get; set; }
		public bool SignMarket { get; set; }


        public static ModelFactorDto FromEntity(KadOzenka.Dal.Model.Dto.ModelFactorDto entity)
        {
            return new ModelFactorDto
            {
                Id = entity.Id,
                ModelId = entity.ModelId,
                FactorId = entity.FactorId,
                Factor = entity.Factor,
                MarkerId = entity.MarkerId,
                Weight = entity.Weight,
                B0 = entity.B0,
                SignDiv = entity.SignDiv,
                SignAdd = entity.SignAdd,
                SignMarket = entity.SignMarket
            };
        }
    }
}
