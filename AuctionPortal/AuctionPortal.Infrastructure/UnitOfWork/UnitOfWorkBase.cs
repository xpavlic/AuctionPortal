using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionPortal.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly IList<Action> afterCommitActions = new List<Action>();

        /// <summary>
        /// Commits the changes made inside this unit of work.
        /// </summary>
        public async Task Commit()
        {
            await CommitCore();
            foreach (var action in afterCommitActions)
            {
                action();
            }
            afterCommitActions.Clear();
        }

        public void RegisterAction(Action action)
        {
            afterCommitActions.Add(action);
        }

        /// <summary>
        /// Performs the real commit work.
        /// </summary>
        protected abstract Task CommitCore();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();
    }
}
