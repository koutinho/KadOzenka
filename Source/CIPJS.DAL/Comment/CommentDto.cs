using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.Comment
{
    public class CommentDto
    {
        public string GuidId { get; set; }

        public long CommentId { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Департамент
        /// </summary>
        public string UserDepartment { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? DateCreate { get; set; }

        /// <summary>
        /// Ссылка на реестр дел
        /// </summary>
        public long? LinkObjectId { get; set; }

        /// <summary>
        /// Ссылка на номер реестра
        /// </summary>
        public long? LinkReestrId { get; set; }

        public bool ReadOnly { get; set; }

        public static CommentDto OMMap(OMComment entity)
        {
            return new CommentDto
            {
                GuidId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6),
                CommentId = entity.Id,
                Comment = entity.Comment,
                UserName = entity.ParentUser.FullName,
                UserDepartment = entity.ParentUser.ParentDepartment.Name,
                DateCreate = entity.DateCreate,
                LinkObjectId = entity.LinkObjectId,
                LinkReestrId = entity.LinkReestrId
            };
        }
    }
}
