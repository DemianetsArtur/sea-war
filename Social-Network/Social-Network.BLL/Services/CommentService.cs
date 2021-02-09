using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Social_Network.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;

        public CommentService(IUoW database,
                              IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public void CommentPostCreate(CommentPostDto entity)
        {
            var commentMapper = this._mapper.Map<CommentPost>(entity);
            var dateFormat = DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var time = DateTime.Now.ToString(OptionsInfo.TimeConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            commentMapper.PartitionKey = date + " " + time;
            commentMapper.RowKey = guidKey;

            this._database.Comment.CommentPostCreate(commentMapper);
        }

        public ICollection<CommentPostDto> CommentPostGet(string name) 
        {
            var comments = this._database.Comment.CommentPostGet(name);
            return this._mapper.Map<ICollection<CommentPostDto>>(comments);
        }

        public ICollection<CommentPostDto> CommentPostsGetAll() 
        {
            var commentsMapper = this._mapper.Map<ICollection<CommentPostDto>>(this._database.Comment.CommentPostsGetAll());
            return commentsMapper;
        }
    }
}
