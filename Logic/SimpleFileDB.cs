using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using SubaruEfficiencyTracking.Models;


namespace SubaruEfficiencyTracking.Logic
{

    public class SimpleFileDB : IDBConnector
    {
        private string _FileStoreLocation;
        private Dictionary<string, DataTable> _Tables;

        public SimpleFileDB(string FileStoreLocation)
        {
            _FileStoreLocation = FileStoreLocation;
            if (!_FileStoreLocation.EndsWith("\\")) { _FileStoreLocation += "\\"; }

            _Tables = new Dictionary<string, DataTable>();
        }

        public string GetTableName<T>()
        {
            string TableName = typeof(T).GetCustomAttribute<DataContractAttribute>()?.Name;
            if (string.IsNullOrWhiteSpace(TableName)) { TableName = typeof(T).Name.ToLower(); }
            return TableName;
        }

        public List<T> SelectRows<T>()
        {
            string TableName = GetTableName<T>();
            VerifyTableExists<T>(TableName);

            List<T> Output = new List<T>();
            for (int r = 0; r < _Tables[TableName].Rows.Count; r++)
            {
                T newObj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    prop.SetValue(newObj, _Tables[TableName].Rows[r][prop.Name]);
                }
                Output.Add(newObj);
            }

            return Output;
        }

        public void CreateRow<T>(T RowData)
        {
            string TableName = GetTableName<T>();
            VerifyTableExists<T>(TableName);

            DataRow newRow = _Tables[TableName].NewRow();
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                newRow[prop.Name] = prop.GetValue(RowData);
            }
            _Tables[TableName].Rows.Add(newRow);

            CommitChangesToFile(TableName);
        }

        public void UpdateRow<T>(T RowData)
        {
            string TableName = GetTableName<T>();
            VerifyTableExists<T>(TableName);

            //Find the row we want to update and do the update
            string IDColumnValue = typeof(T).GetProperties()[0].GetValue(RowData).ToString();
            for (int r = 0; r < _Tables[TableName].Rows.Count; r++)
            {
                if (_Tables[TableName].Rows[r][0].ToString() == IDColumnValue)
                {
                    foreach (PropertyInfo prop in typeof(T).GetProperties())
                    {
                        _Tables[TableName].Rows[r][prop.Name] = prop.GetValue(RowData);
                    }
                    break;
                }
            }

            CommitChangesToFile(TableName);
        }

        public void DeleteRow<T>(T RowData)
        {
            string TableName = GetTableName<T>();
            VerifyTableExists<T>(TableName);

            string IDColumnValue = typeof(T).GetProperties()[0].GetValue(RowData).ToString();
            for (int r = 0; r < _Tables[TableName].Rows.Count; r++)
            {
                if (_Tables[TableName].Rows[r][0].ToString() == IDColumnValue)
                {
                    _Tables[TableName].Rows.RemoveAt(r);
                    break;
                }
            }

            CommitChangesToFile(TableName);
        }


        private void VerifyTableExists<T>(string TableName)
        {
            if (!_Tables.ContainsKey(TableName))
            {
                DataTable newTable = new DataTable();
                newTable.TableName = TableName;

                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    newTable.Columns.Add(prop.Name, prop.PropertyType);
                }

                ReadTableFromFile<T>(TableName, ref newTable);

                _Tables.Add(TableName, newTable);

            }
        }

        private void ReadTableFromFile<T>(string TableName, ref DataTable TableData)
        {
            string TablePath = _FileStoreLocation + TableName + ".csv";

            if (!File.Exists(TablePath)) return;

            string colSplitter = "\",\"";
            string[] tableLines = File.ReadAllLines(TablePath);
            for (int r = 1; r < tableLines.Length; r++)
            {
                string[] cols = tableLines[r].Split(colSplitter);
                //The first and last col will still start and end with " so go remove those
                cols[0] = cols[0].TrimStart('"');
                cols[cols.Length - 1] = cols[cols.Length - 1].TrimEnd('"');

                DataRow newRow = TableData.NewRow();
                for (int c = 0; c < typeof(T).GetProperties().Length; c++)
                {
                    newRow[typeof(T).GetProperties()[c].Name] = cols[c];
                }
                TableData.Rows.Add(newRow);
            }
        }

        private void CommitChangesToFile(string TableName)
        {
            string TempTablePath = _FileStoreLocation + TableName + ".csv.tmp";
            string RealTablePath = _FileStoreLocation + TableName + ".csv";

            if (File.Exists(TempTablePath)) { File.Delete(TempTablePath); }
            if (!Directory.Exists(_FileStoreLocation)) { Directory.CreateDirectory(_FileStoreLocation); }

            //Create the file contents
            StringBuilder sbOut = new StringBuilder();

            //Add column headers
            for (int c = 0; c < _Tables[TableName].Columns.Count; c++)
            {
                if (c > 0) sbOut.Append(",");
                sbOut.Append("\"" + _Tables[TableName].Columns[c].ColumnName + "\"");
            }
            sbOut.AppendLine("");

            //Add row data
            for (int r = 0; r < _Tables[TableName].Rows.Count; r++)
            {
                for (int c = 0; c < _Tables[TableName].Columns.Count; c++)
                {
                    if (c > 0) sbOut.Append(",");
                    sbOut.Append("\"" + _Tables[TableName].Rows[r][c].ToString() + "\"");
                }
                sbOut.AppendLine("");
            }

            //Dump the data to the correct files
            File.WriteAllText(TempTablePath, sbOut.ToString());
            if (File.Exists(RealTablePath)) { File.Delete(RealTablePath); }
            File.Move(TempTablePath, RealTablePath);
        }
    }
}
