using Refit;
using System;
using System.Threading.Tasks;
using server.Facades.Google.Models;
using System.Net.Http;

namespace server.Facades.Google
{
    public interface IDriveApi 
    {
        [Post("/files")]
        Task<File> Create([Body] File file, [Header("Authorization")] string authorization);
    }
}