using System;
using Flower.UI.Models;

namespace Flower.UI.Service
{
	public interface ICrudService
	{
        Task<PaginatedResponse<TResponse>> GetAllPaginated<TResponse>(string path, int page, int size = 10);
        Task<TResponse> Get<TResponse>(string path);
        Task<CreateResponse> Create<TRequest>(TRequest request, string path);
        Task Update<TRequest>(TRequest request, string path);
        Task Delete(string path);
        Task<CreateResponse> CreateFromForm<TRequest>(TRequest request, string path);
        Task<CreateResponse> EditFromForm<TRequest>(TRequest request, string path);
    }
}

