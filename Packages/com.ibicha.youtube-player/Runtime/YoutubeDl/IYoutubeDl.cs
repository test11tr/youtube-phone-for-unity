using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YoutubePlayerEditor
{
    interface IYoutubeDl
    {
        Task<T> GetVideoMetaDataAsync<T>(string youtubeUrl, YoutubeDlOptions options,
            IEnumerable<string> schema, YoutubeDlCli cli, CancellationToken cancellationToken = default);
    }
}
