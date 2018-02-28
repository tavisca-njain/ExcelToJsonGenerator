using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace ExcelToJsonGenerator
{
    public static class Parser
    {
        public static List<Entity> ParseInfoFromDataSet(DataSet dataSet, string tableName, string codeRow, string nameRow)
        {
            var entities = new List<Entity>();

            DataTable table = dataSet.Tables[tableName];

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                var codeRowValue = row[codeRow];
                var code = codeRowValue.ToString();
                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("Invalid code : " + codeRowValue);

                var nameRowValue = row[nameRow];
                var name = nameRowValue.ToString();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Invalid name : " + nameRowValue);

                if (entities.FirstOrDefault(a => a.Code == code) == null)
                    entities.Add(new Entity(code, name));
            }

            return entities;
        }
    }
}
