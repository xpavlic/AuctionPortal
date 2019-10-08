using System;
using System.Threading.Tasks;

namespace AuctionPortal.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persists all changes made within this unit of work.
        /// </summary>
        Task Commit();

        /// <summary>
        /// Registers an action, which is executed if and only if commit is succesfull.
        /// </summary>
        void RegisterAction(Action action);
    }
}
