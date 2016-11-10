using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CsvToCSharpClass.Library
{
    public class Pitcher
    {
        public static string CSharpClassCodeFromCsvFile(string filePath, string delimiter = ",", string classAttribute = "", string propertyAttribute = "")
        {
            //if (string.IsNullOrWhiteSpace(propertyAttribute) == false)
               // propertyAttribute += "\n\t";
           // if (string.IsNullOrWhiteSpace(propertyAttribute) == false)
               // classAttribute += "\n";

            string[] lines = File.ReadAllLines(filePath);
            string[] columnNames = lines.First().Split(',').Select(str => str.Trim()).ToArray();
            string[] data = lines.Skip(1).ToArray();

            string className = Path.GetFileNameWithoutExtension(filePath);
            // use StringBuilder for better performance
            string code = String.Format("{0}public class {1} {{ \n", classAttribute, className);

            for (int columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
            {
                var columnName = Regex.Replace(columnNames[columnIndex], @"[\s\.]", string.Empty, RegexOptions.IgnoreCase);
                if (string.IsNullOrEmpty(columnName))
                    columnName = "Column" + (columnIndex + 1);
                //code += "\t" + GetVariableDeclaration(data, columnIndex, columnName, propertyAttribute) + "\n\n";
            }

            code += "}\n";
            return code;
        }

    };
}
public class Stat
{
    string name;
    float stat;

    public Stat(string n, float s)
    {
        name = n;
        stat = s;
    }

    float getStat()
    {
        return stat;
    }
    string getName()
    {
        return name;
    }
};
