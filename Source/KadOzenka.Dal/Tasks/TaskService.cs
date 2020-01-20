﻿using System.Linq;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Core.TD;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks
{
    public class TaskService
    {
        public TaskDto GetTaskById(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).SelectAll().Execute().FirstOrDefault();
            if (task == null)
                return null;

            var responseDocument = GetDocumentById(task.ResponseDocId);
            var incomingDocument = GetDocumentById(task.DocumentId);
            var tour = GetTourById(task.TourId);

            return new TaskDto
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                Tour = tour,
                NoteType = task.NoteType,
                Status = task.Status,
                ResponseDocument = responseDocument,
                IncomingDocument = incomingDocument
            };
        }


        #region Support Methods

        private DocumentDto GetDocumentById(long? documentId)
        {
            if (documentId == null)
                return null;

            var document = OMInstance.Where(x => x.Id == documentId).SelectAll().Execute()
                .FirstOrDefault();
            if (document == null)
                return null;

            return new DocumentDto
            {
                Id = document.Id,
                RegNumber = document.RegNumber,
                CreationDate = document.CreateDate
            };
        }

        private TourDto GetTourById(long? tourId)
        {
            if (tourId == null)
                return null;

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
                return null;

            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }

        #endregion
    }
}
