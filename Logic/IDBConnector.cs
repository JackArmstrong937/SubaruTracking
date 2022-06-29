using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Logic
{
    public interface IDBConnector
    {
        public string GetTableName<T>();
        public List<T> SelectRows<T>();
        public void CreateRow<T>(T RowData);
        public void UpdateRow<T>(T RowData);
        public void DeleteRow<T>(T RowData);
        public void ClearCache();

    }
}
