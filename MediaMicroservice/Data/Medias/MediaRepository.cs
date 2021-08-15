using MediaMicroservice.DBContexts;
using MediaMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Data.Medias
{
    public class MediaRepository : IMediaRepository
    {
        private readonly MediaDbContext context;

        public MediaRepository(MediaDbContext context)
        {
            this.context = context;
        }

        public void CreateMedia(Media media)
        {
            context.Medias.Add(media);
        }

        public void DeleteMedia(Guid mediaId)
        {
            var media = GetMediaById(mediaId);
            if (media != null)
            {
                context.Remove(media);
            }
        }

        public List<Media> GetMediaByAccountId(Guid id)
        {
            return context.Medias.Where(e => (e.AccountId == id)).ToList();
        }

        public Media GetMediaById(Guid mediaId)
        {
            return context.Medias.FirstOrDefault(e => e.MediaId == mediaId);
        }

        public List<Media> GetMediaByItemForSaleId(Guid itemForSaleId)
        {
            return context.Medias.Where(e => (e.ItemForSaleId == itemForSaleId)).ToList();
        }

        public List<Media> GetMedias()
        {
            return context.Medias.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateMedia(Media oldMedia, Media newMedia)
        {
            oldMedia.FilePath = newMedia.FilePath;
        }
    }
}
