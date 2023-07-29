
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.Interface
{
    public interface IOderRepository
    {
        Task<OderResponse> CreateOder(OderRequest oderRequest);

        Pagination<OderResponse> GetOder(int currentPage, int pageSize);
    }
}
