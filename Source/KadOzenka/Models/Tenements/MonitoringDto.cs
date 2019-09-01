namespace CIPJS.Models.Tenements
{
    public class MonitoringDto
    {
        public string GroupName { get; set; }

        public int OrdinalNumber { get; set; }

        public string Name { get; set; }

        public object EgrnBuilding { get; set; }
               
        public object EgrnFlat { get; set; }
               
        public object BtiBuilding { get; set; }
               
        public object BtiFlat { get; set; }
               
        public object Building { get; set; }
               
        public object Flat { get; set; }
    }
}