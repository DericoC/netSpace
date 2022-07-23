using System.IO;
using System.Threading.Tasks;

namespace NetSpace
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}