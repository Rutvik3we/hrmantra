using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NErrorHandler;

namespace NDatabaseHandler
{
    public class DataAccess
    {
        #region "Private Variable"
        private SqlConnection _dbConnection;
        private ErrorHandler _errorHandler;
        #endregion

        #region "Constructor"
        public DataAccess(string connectionString,
                          ErrorHandler objErrorHandler)
        {
            try
            {
                _dbConnection = new SqlConnection(connectionString);
                _errorHandler = objErrorHandler;
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
        }
        #endregion

        #region "Generic Method"
        public DataSet GetDataSet(string strSql)
        {
            DataSet objDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, _dbConnection);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);

                if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                }
                adpt.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
            return objDataSet;
        }

        //For Gettting Multiple DataTable Record.
        public DataSet GetDataSet(string strSql, string[] paramArray, object[] values, CommandType cmdType = CommandType.Text)
        {
            DataSet objDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, _dbConnection);
                //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                cmd.CommandType = cmdType;

                if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                }

                if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                {
                    for (int i = 0; i < paramArray.Length; i++)
                    {
                        //if (values[i] != null)
                        //{
                        cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                        //}
                    }
                }
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
            return objDataSet;
        }

        public DataTable GetDataTable(string strSql)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, _dbConnection);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                if (Equals(_dbConnection.State, ConnectionState.Closed))
                {
                    _dbConnection.Open();
                }
                adpt.Fill(objDataTable);
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
            return objDataTable;
        }

        public DataTable GetDataTable(string strSql, CommandType cmdType = CommandType.Text)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, _dbConnection)
                {
                    CommandType = cmdType
                };
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);

                if (Equals(_dbConnection.State, ConnectionState.Closed))
                {
                    _dbConnection.Open();
                }
                adpt.Fill(objDataTable);
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
            return objDataTable;
        }

        public DataTable GetDataTable(string strSql, string[] paramArray, object[] values, CommandType cmdType = CommandType.Text)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, _dbConnection)
                {
                    CommandType = cmdType
                };

                if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                {
                    for (int i = 0; i < paramArray.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                    }
                }

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(objDataTable);
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
            return objDataTable;
        }

        public int ExecuteNonQuery(string strSql, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    cmd.CommandType = type;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        /// <summary>
        /// Execute Non Query with parameter
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strSql, List<object> sqlParams, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    cmd.CommandType = type;
                    string paramId;
                    object value;

                    for (int index = 0; index < sqlParams.Count; index++)
                    {
                        paramId = "@param" + index;
                        value = sqlParams[index];

                        cmd.Parameters.AddWithValue(paramId, value);
                    }

                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }

                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int ExecuteNonQuery(string strSql, List<object> sqlParams, List<int> paramType, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    cmd.CommandType = type;
                    string paramId;
                    object value;

                    for (int index = 0; index < sqlParams.Count; index++)
                    {
                        paramId = "@param" + index;
                        value = sqlParams[index];

                        if (paramType[index] == 0)
                        {
                            cmd.Parameters.Add(paramId, SqlDbType.Int).Value = Convert.ToInt32(value);
                        }
                        else if (paramType[index] == 1)
                        {
                            cmd.Parameters.Add(paramId, SqlDbType.Int).Value = Convert.ToInt32(value);
                        }
                        else if (paramType[index] == 2)
                        {
                            cmd.Parameters.Add(paramId, SqlDbType.Decimal).Value = Convert.ToDecimal(value);
                        }
                        else if (paramType[index] == 3)
                        {
                            cmd.Parameters.Add(paramId, SqlDbType.VarChar).Value = Convert.ToString(value);
                        }
                        else if (paramType[index] == 4)
                        {
                            cmd.Parameters.Add(paramId, SqlDbType.BigInt).Value = Convert.ToUInt32(value);
                        }
                        //cmd.Parameters.AddWithValue(paramId, value);
                    }

                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }

                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int ExecuteNonQuery(string strSql, string[] paramArray, object[] values, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                    {
                        for (int i = 0; i < paramArray.Length; i++)
                        {
                            cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                        }
                    }

                    cmd.CommandType = type;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int ExecuteNonQuery(SqlCommand objSqlCommand)
        {
            try
            {
                SqlCommand cmd = objSqlCommand;
                cmd.Connection = _dbConnection;
                _dbConnection.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }


        public int ExecuteNonQuery(string strSql, string[] paramArray, object[] values, bool isKeyRequired, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                    {
                        for (int i = 0; i < paramArray.Length; i++)
                        {
                            cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                        }
                    }

                    if (isKeyRequired)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    }

                    cmd.CommandType = type;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int result = cmd.ExecuteNonQuery();

                    int id = Convert.ToInt16(cmd.Parameters["@id"].Value);
                    return id;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int ExecuteNonQuery(string strSql, string[] paramArray, object[] values, ref int id, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                    {
                        for (int i = 0; i < paramArray.Length; i++)
                        {
                            cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                        }
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    }

                    cmd.CommandType = type;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int result = cmd.ExecuteNonQuery();
                    id = Convert.ToInt16(cmd.Parameters["@id"].Value);
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                    _errorHandler.WritetoLogFile(ex);

                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int ExecuteNonQueryWithReturnValue(string strSql, string[] paramArray, object[] values, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    if ((paramArray != null) && (values != null) && (paramArray.Length == values.Length))
                    {
                        for (int i = 0; i < paramArray.Length; i++)
                        {
                            cmd.Parameters.AddWithValue(paramArray[i], values[i]);
                        }
                    }

                    cmd.CommandType = type;
                    if (_dbConnection.State == ConnectionState.Closed)
                    {
                        _dbConnection.Open();
                    }
                    SqlParameter returnsqlParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                    returnsqlParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    int result = (int)cmd.Parameters["@return"].Value;
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                    _errorHandler.WritetoLogFile(ex);

                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public object ExecuteScalar(string strSql, string[] paramArry, object[] values, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    if ((paramArry != null) && (values != null) && (paramArry.Length == values.Length))
                        for (int i = 0; i < paramArry.Length; i++)
                        {
                            cmd.Parameters.AddWithValue(paramArry[i], values[i]);
                        }

                    cmd.CommandType = type;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    object result = cmd.ExecuteScalar();

                    //if (errorHandler != null)
                    //    errorHandler.WritetoLogFile("ExecuteNonQuery function call succesfully.");

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return false;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public object ExecuteScalar(string strSQL, CommandType cmdType = CommandType.Text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, _dbConnection))
                {
                    cmd.CommandType = cmdType;
                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    object result = cmd.ExecuteScalar();

                    //if (errorHandler != null)
                    //    errorHandler.WritetoLogFile("ExecuteNonQuery function call succesfully.");

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return null;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }

        public int InsertDataWithStoredProcedure(string strSql, List<object> sqlParams)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    int index = 1;

                    foreach (object objParma in sqlParams)
                    {
                        cmd.Parameters.AddWithValue("@param" + index, objParma);
                        index += 1;
                    }

                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int i = cmd.ExecuteNonQuery();

                    if (_errorHandler != null)
                    {
                        _errorHandler.WritetoLogFile("InsertDataWithStoredProcedure function call succesfully. strSql = " + strSql);
                    }
                    return i;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }
        #endregion

        #region "Public Method"
        public int InsertData(string strSql, List<object> sqlParams)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _dbConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    string paramId;
                    object value;

                    for (int index = 0; index < sqlParams.Count; index++)
                    {
                        paramId = "@param" + index;
                        value = sqlParams[index];
                        cmd.Parameters.AddWithValue(paramId, value);
                    }

                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
                return 0;
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }


        public void BulkCopyDataTable(DataTable objDataTable, string destinationTableName)
        {
            try
            {
                //using (var bulkCopy = new System.Data.SqlClient.SqlBulkCopy(DbConnection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                using (var bulkCopy = new SqlBulkCopy(_dbConnection))
                {
                    // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match,
                    // just pass in which datatable name matches the SQL column name in Column Mappings
                    foreach (DataColumn col in objDataTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    if (Equals(_dbConnection.State, ConnectionState.Closed))
                    {
                        _dbConnection.Open();
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = destinationTableName;
                    bulkCopy.WriteToServer(objDataTable);
                }
            }
            catch (Exception ex)
            {
                if (_errorHandler != null)
                {
                    _errorHandler.WritetoLogFile(ex);
                }
            }
            finally
            {
                if (_dbConnection != null)
                {
                    _dbConnection.Close();
                }
            }
        }
        #endregion

        #region "Destructor"
        ~DataAccess()
        {
            if (_errorHandler != null)
            {
                _errorHandler = null;
            }
            if (_dbConnection != null)
            {
                _dbConnection = null;
            }
        }
        #endregion
    }
}
