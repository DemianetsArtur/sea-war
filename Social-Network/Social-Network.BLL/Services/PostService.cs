using AutoMapper;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.Models;
using Social_Network.BLL.ModelsDto;
using Social_Network.DAL.Entities;
using Social_Network.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Social_Network.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUoW _database;
        private readonly IMapper _mapper;
        public PostService(IUoW database, IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public Task<Uri> FileUploadToBlobAsync(Stream content, string contentType, string fileName) 
        {
            return this._database.Post.FileUploadToBlobAsync(content, contentType, fileName);
        }

        public void PostCreate(PostDto entity) 
        {
            var postMapper = this._mapper.Map<Post>(entity);
            var dateFormat = DateTimeFormatInfo.InvariantInfo;
            var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
            var time = DateTime.Now.ToString(OptionsInfo.TimeConfig, dateFormat);
            var guidKey = Guid.NewGuid().ToString();
            postMapper.PartitionKey = date + " " + time;
            postMapper.RowKey = guidKey;

            this._database.Post.PostCreate(postMapper);
        }

        public ICollection<PostDto> PostsGet(string name) 
        {
            return this._mapper.Map<ICollection<PostDto>>(this._database.Post.PostsGet(name));
        }
    }
}
