using System;
using System.Collections.Generic;
using System.Text;

namespace Training1.Tests.Utils
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(
            Func<TDisposable> factory,
            Func<TDisposable, TResult> map)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }
    }
}
