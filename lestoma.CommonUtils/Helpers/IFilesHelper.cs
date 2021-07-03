using System.IO;

namespace lestoma.CommonUtils.Helpers
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }
}
