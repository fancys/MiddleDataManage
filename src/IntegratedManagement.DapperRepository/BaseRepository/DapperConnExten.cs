using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.RepositoryDapper.BaseRepository
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 11:10:02
	===============================================================================================================================*/
    public static class DapperConnExten
    {
        public static IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(
this IDbConnection connection,
string sql,
Func<TParent, TParentKey> parentKeySelector,
Func<TParent, IList<TChild>> childSelector,
dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "DocEntry", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            connection.Query<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }




        public async static Task<IEnumerable<TParent>> QueryParentChildAsync<TParent, TChild, TParentKey>(
this IDbConnection connection,
string sql,
Func<TParent, TParentKey> parentKeySelector,
Func<TParent, IList<TChild>> childSelector,
dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "DocEntry", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            await  connection.QueryAsync<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }

        public async static Task<IEnumerable<TParent>> QueryParentChildAsync<TParent, TChild1, TChild2, TParentKey>(
this IDbConnection connection,
string sql,
Func<TParent, TParentKey> parentKeySelector,
Func<TParent, IList<TChild1>> childSelector1,
Func<TParent, IList<TChild2>> childSelector2,
dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "DocEntry", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            await connection.QueryAsync<TParent, TChild1,TChild2, TParent>(
                sql,
                (parent, child1,child2) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild1> children1 = childSelector1(cachedParent);
                    IList<TChild2> children2 = childSelector2(cachedParent);
                    children1.Add(child1);
                    children2.Add(child2);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }

    }


}
