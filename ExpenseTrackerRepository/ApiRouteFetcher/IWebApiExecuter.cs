using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository.ApiRouteFetcher
{
   public interface IWebApiExecuter
    {
        void AddAuthHeader(string Token);
        Task InvokeDelete<T>(string uri);

        Task<T> InvokeGet<T>(string uri);

        Task<OperationResponse> InvokePost<T>(string uri, T obj);

        Task<T> InvokePostObjResponse<T>(string uri, T obj);

        Task InvokePut<T>(string uri, T obj);
        void RemoveAuthHeader();
    }
}