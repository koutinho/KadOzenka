using System.Collections.Generic;

namespace CIPJS.DAL.Comment
{
    public class CommentListDto
    {
        /// <summary>
        /// Ссылка на объект
        /// </summary>
        public long? CommentObjectId { get; set; }
        /// <summary>
        /// Ссылка на реестр
        /// </summary>
        public long? CommentReestrId { get; set; }

        public bool IsCommentReadOnly { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
