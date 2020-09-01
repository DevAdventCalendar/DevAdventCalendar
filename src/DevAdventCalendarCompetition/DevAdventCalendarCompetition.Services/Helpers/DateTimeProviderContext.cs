using System;
using System.Collections;
using System.Threading;

namespace DevAdventCalendarCompetition.Services
{
    public class DateTimeProviderContext : IDisposable
    {
        private static readonly ThreadLocal<Stack> ThreadScopeStack = new ThreadLocal<Stack>(() => new Stack());

        public DateTimeProviderContext(DateTime contextDateTimeUtcNow)
        {
            this.ContextDateTimeNow = contextDateTimeUtcNow;
            ThreadScopeStack.Value.Push(this);
        }

        public static DateTimeProviderContext Current
        {
            get
            {
                if (ThreadScopeStack.Value.Count == 0)
                {
                    return null;
                }

                return ThreadScopeStack.Value.Peek() as DateTimeProviderContext;
            }
        }

        public DateTime ContextDateTimeNow { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            ThreadScopeStack.Value.Pop();
        }
    }
}
