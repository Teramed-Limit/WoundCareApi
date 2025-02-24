namespace WoundCareApi.Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // 提交當前單位的變更
    void Save();
    Task SaveAsync();
}
