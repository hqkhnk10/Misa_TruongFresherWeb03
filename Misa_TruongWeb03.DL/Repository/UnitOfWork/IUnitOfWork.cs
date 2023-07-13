using System.Data;

namespace Misa_TruongWeb03.DL.Repository.UnitOfWorkk
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
    }
}