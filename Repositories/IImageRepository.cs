using System.Net;

namespace nz_walks.Repositories;
using NZWalks.Models.Domain;
public interface IImageRepository
{
   Task<Image> Upload(Image image); 
}