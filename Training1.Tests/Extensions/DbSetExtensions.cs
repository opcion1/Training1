using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Training1.Tests.Extensions
{
    public static class DbSetExtensions
    {
        public static DbSet<T> MockFromSql<T>(this DbSet<T> dbSet, IQueryable<T> spItems) where T : class
        {
            var queryProviderMock = new Mock<IQueryProvider>();
            queryProviderMock.Setup(p => p.CreateQuery<T>(It.IsAny<MethodCallExpression>()))
                .Returns<MethodCallExpression>(x => spItems);

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>()
                .SetupGet(q => q.Provider)
                .Returns(() => queryProviderMock.Object);

            dbSetMock.As<IQueryable<T>>()
                .Setup(q => q.Expression)
                .Returns(Expression.Constant(dbSetMock.Object));
            return dbSetMock.Object;
        }
    }
}
