using UserManagement.Presentation.WebAPI.Filter;

namespace UserManagement.Presentation.WebAPI.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
