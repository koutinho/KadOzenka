using System.ComponentModel;

namespace KadOzenka.Dal.Oks
{
    public enum ObjectType
    {
        [Description("Объект капитального строительства")]
        Oks,

        [Description("Земельный участок")]
        ZU
    } 
}
