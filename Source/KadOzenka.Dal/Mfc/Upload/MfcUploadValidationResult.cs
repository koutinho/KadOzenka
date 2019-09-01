using System.Collections.Generic;

namespace CIPJS.DAL.Mfc.Upload
{
    public class MfcUploadValidationResult
    {
        public MfcUploadValidationResult()
        {
            Errors = new List<string>();
        }

        public bool Success
        {
            get
            {
                return Errors.Count == 0;
            }
        }
           

        public List<string> Errors { get; set; }
    }
}
