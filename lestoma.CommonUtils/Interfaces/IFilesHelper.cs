using System.IO;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }
}
