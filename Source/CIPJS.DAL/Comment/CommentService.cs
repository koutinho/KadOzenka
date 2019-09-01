using Core.SRD;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.Comment
{
    public class CommentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reestrId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        private CommentDto CreateDefault(long? reestrId, long? objectId)
        {
            return new CommentDto
            {
                CommentId = -1,
                LinkReestrId = reestrId,
                LinkObjectId = objectId,
                GuidId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reestrId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentDto GetComment(long? reestrId, long? id)
        {
            if (id.HasValue && id > -1)
            {
                OMComment comment = OMComment.Where(x => x.Id == id)
                      .SelectAll()
                      .Select(x => x.ParentUser.FullName)
                      .Select(x => x.ParentUser.ParentDepartment.Name)
                      .Execute()
                      .FirstOrDefault();

                return CommentDto.OMMap(comment);
            }

            return CreateDefault(reestrId, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reestrId"></param>
        /// <param name="objectId"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public CommentListDto Get(long? reestrId, long? objectId, bool isReadOnly)
        {
            CommentListDto model = new CommentListDto
            {
                CommentReestrId = reestrId,
                CommentObjectId = objectId,
                IsCommentReadOnly = isReadOnly,
                Comments = GetComments(reestrId, objectId, isReadOnly)
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Save(CommentListDto model)
        {
            if (model.Comments != null && model.Comments.Count > 0)
            {
                foreach (CommentDto commentDto in model.Comments)
                {
                    OMComment comment = null;

                    if (commentDto.CommentId > 0)
                    {
                        comment = OMComment.Where(x => x.Id == commentDto.CommentId).SelectAll().Execute().FirstOrDefault();
                    }

                    if (comment == null)
                    {
                        comment = new OMComment
                        {
                            DateCreate = DateTime.Now,
                            UserId = SRDSession.GetCurrentUserId()
                        };
                    }

                    comment.Comment = commentDto.Comment;
                    comment.LinkReestrId = model.CommentReestrId;
                    comment.LinkObjectId = model.CommentObjectId;

                    comment.Save();
                }
            }
        }

        public void Delete(long? id)
        {
            if (id.HasValue)
            {
                OMComment comment = OMComment.Where(x => x.Id == id).SelectAll(false).Execute().FirstOrDefault();
                if(comment != null)
                {
                    comment.Destroy();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reestrId"></param>
        /// <param name="objectId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        private List<CommentDto> GetComments(long? reestrId, long? objectId, bool readOnly)
        {
            List<CommentDto> list = new List<CommentDto>();
            List<OMComment> comments = null;
            if (objectId.HasValue)
            {
                comments = OMComment
                    .Where(x => x.LinkReestrId == reestrId && x.LinkObjectId == objectId)
                    .SelectAll()
                    .Select(x => x.ParentUser.FullName)
                    .Select(x => x.ParentUser.ParentDepartment.Name)
                    //.OrderBy(x => x.DateCreate)
                    .Execute()
                    .ToList();
            }
            if (comments != null)
            {
                foreach (OMComment comment in comments)
                {
                    CommentDto comm = CommentDto.OMMap(comment);
                    comm.ReadOnly = readOnly;
                    list.Add(comm);
                }
            }

            return list;
        }
    }
}
