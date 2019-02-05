using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    public interface ICRUDRepository
    {
        #region CREATE
        // Insert a single record and return the key
        object Create<T>(T entity, string connectionString);
        // Insert multiple records using a query and a list of params
        void CreateMany<T>(string query, List<object> queryParams, string connectionString);
        #endregion

        #region READ
        // Get single record by primary key
        T ReadByPK<T>(object primaryKey, string connectionString);
        // Get first or default record with where clause
        T ReadFirstOrDefault<T>(string query, object queryParams, string connectionString);
        // Get first or error record with where clause
        T ReadFirst<T>(string query, object queryParams, string connectionString);
        // Get first or default record with where clause
        T ReadSingleOrDefault<T>(string query, object queryParams, string connectionString);
        // Get first or error record with where clause
        T ReadSingle<T>(string query, object queryParams, string connectionString);
        // Get list with advanced where clause
        List<T> ReadList<T>(string query, object queryParams, string connectionString);
        // Get list with basic where clause
        List<T> ReadListBasic<T>(object queryParams, string connectionString);
        // Get list paged with string where clause and sort
        List<T> ReadListPaged<T>(int pagenumber, int itemsperpage, string conditions, string order, string connectionString);
        // Get first or default with with params from stored procedure
        T ReadFirstOrDefaultSP<T>(string query, object queryParams, string connectionString);
        // Get first or error with with params from stored procedure
        T ReadFirstSP<T>(string query, object queryParams, string connectionString);
        // Get list with with params from stored procedure
        List<T> ReadListSP<T>(string query, object queryParams, string connectionString);
        #endregion

        #region UPDATE
        // Update a record using the entity
        int Update<T>(T entity, string connectionString);
        #endregion

        #region DELETE
        // Delete a record using the entity
        int Delete<T>(T entity, string connectionString);
        // Delete a record using the id
        int DeleteById<T>(object id, string connectionString);
        // Delete list of records using and anonymous object for where clause
        int DeleteList<T>(object queryParams, string connectionString);
        #endregion

        #region MISC
        // Get a count from a type
        long ReadRecordCount<T>(object queryParams, string connectionString);
        #endregion
    }
    public class CRUDRepository : ICRUDRepository
    {
        #region CREATE
        public object Create<T>(T entity, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return connection.Insert<T>(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        // TODO: THIS IS A BETA AND NEEDS TO BE TESTED
        public void CreateMany<T>(string query, List<object> queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    connection.Execute(query, queryParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region READ
        public T ReadByPK<T>(object primaryKey, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Get<T>(primaryKey);
            }
        }
        public T ReadFirstOrDefault<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.QueryFirstOrDefault<T>(query, queryParams);
            }
        }
        public T ReadFirst<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.QueryFirst<T>(query, queryParams);
            }
        }
        public T ReadSingleOrDefault<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.QuerySingleOrDefault<T>(query, queryParams);
            }
        }
        public T ReadSingle<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.QuerySingle<T>(query, queryParams);
            }
        }
        public List<T> ReadList<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<T>(query, queryParams).ToList();
            }
        }
        public List<T> ReadListBasic<T>(object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.GetList<T>(queryParams).ToList();
            }
        }
        public List<T> ReadListPaged<T>(int pagenumber, int itemsperpage, string conditions, string order, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.GetListPaged<T>(pagenumber, itemsperpage, conditions, order).ToList();
            }
        }
        public T ReadFirstOrDefaultSP<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();


                return connection.QueryFirstOrDefault<T>(query, queryParams, commandType: CommandType.StoredProcedure);
            }
        }
        public T ReadFirstSP<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();


                return connection.QueryFirst<T>(query, queryParams, commandType: CommandType.StoredProcedure);
            }
        }
        public List<T> ReadListSP<T>(string query, object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.Query<T>(query, queryParams, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        #endregion
        
        #region UPDATE
        public int Update<T>(T entity, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return connection.Update<T>(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region DELETE
        public int Delete<T>(T entity, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return connection.Delete<T>(entity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public int DeleteById<T>(object id, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return connection.Delete<T>(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public int DeleteList<T>(object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return connection.DeleteList<T>(queryParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region MISC
        public long ReadRecordCount<T>(object queryParams, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                return connection.RecordCount<T>(queryParams);
            }
        }
        #endregion
    }
}
