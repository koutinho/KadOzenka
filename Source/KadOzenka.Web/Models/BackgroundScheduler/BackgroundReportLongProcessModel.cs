using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Ko;

namespace KadOzenka.Web.Models.BackgroundScheduler
{
    public class BackgroundReportLongProcessModel : BackgroundLongProcessModel, IValidatableObject
    {
        private const string KeyFromConfigForBasePathToFolders = "KoBaseFolderForBackgroundReportingForms";


        [Display(Name = "Отчетная форма")]
        [Required(ErrorMessage = "Не выбрана Отчетная форма")]
        public long SelectedFormId { get; set; }
        public List<SelectListItem> Forms { get; set; }

        [Display(Name = "Место сохранения")]
        [Required(ErrorMessage = "Не выбрано Место сохранения")]
        public string SelectedFolderName { get; set; }
        public List<SelectListItem> Folders { get; set; }


        public BackgroundReportLongProcessModel()
        {
            Forms = GetForms();
            Folders = GetFolders();
        }


        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext).ToList();

            ValidateFolderPath(errors);

            return errors;
        }


        #region Support Methods

        private List<SelectListItem> GetForms()
        {
            return OMBackgroundReportingForms.Where(x => true).SelectAll().Execute().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        private List<SelectListItem> GetFolders()
        {
            var basePath = GetPreprocessedPath(GetBasePathToStorageWithReports());
            if (!Directory.Exists(basePath))
                return new List<SelectListItem>();

            var directories = Directory.GetDirectories(basePath);
            return directories?.Select(x =>
            {
                var folderName = Path.GetFileName(x.TrimEnd(Path.DirectorySeparatorChar));
                return new SelectListItem
                {
                    Text = folderName,
                    Value = folderName
                };
            }).ToList();
        }

        private void ValidateFolderPath(List<ValidationResult> errors)
        {
            var basePath = GetBasePathToStorageWithReports();
            var fullPath = Path.Combine(basePath, SelectedFolderName);
            var preprocessedPath = GetPreprocessedPath(fullPath);

            if (!Directory.Exists(preprocessedPath))
                errors.Add(new ValidationResult($"Не создана папка по адресу '{preprocessedPath}'"));
        }

        private string GetBasePathToStorageWithReports()
        {
            return FileStorageManager.FileStorages.FileStorages.FirstOrDefault(x => x.Key == KeyFromConfigForBasePathToFolders)?.Path;
        }

        private string GetPreprocessedPath(string path)
        {
            var preprocessedPath = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (!preprocessedPath.EndsWith(Path.DirectorySeparatorChar))
                preprocessedPath += Path.DirectorySeparatorChar.ToString();

            return preprocessedPath;
        }

        #endregion
    }
}
