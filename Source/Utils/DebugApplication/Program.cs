
using Core.ErrorManagment;
using Core.SRD;
using ObjectModel.Market;

namespace DebugApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var test = SRDSession.Current;

                OMCoreObject obj = new OMCoreObject
                {
                    Address = "test",
                    Price = 100,

                };

                obj.Save();
            }
            catch (System.Exception ex)
            {
                ErrorManager.LogError(ex);
            }            
        }
    }
}
