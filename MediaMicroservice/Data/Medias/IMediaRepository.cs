using MediaMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Data.Medias
{
    public interface IMediaRepository
    {
        List<Media> GetMedias();
        Media GetMediaById(Guid mediaId);
        void CreateMedia(Media media);
        void UpdateMedia(Media oldMedia, Media newMedia);
        void DeleteMedia(Guid mediaId);
        bool SaveChanges();
        List<Media> GetMediaByItemForSaleId(Guid itemForSaleId);
        List<Media> GetMediaByAccountId(Guid id);
    }
}
