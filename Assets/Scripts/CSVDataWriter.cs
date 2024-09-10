using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class CSVDataWriter : MonoBehaviour
{
    public SubcategorySelection subcategorySelection;
    public DataInput dataInput;
    private string filePath1, filePath2;
    private void Start()
    {
        filePath1 = Path.Combine(Application.persistentDataPath, "TimeRecord.csv");
        filePath2 = Path.Combine(Application.persistentDataPath, "TimeRecordWithDesc.csv");
    }

    

    public List<string[]> ReadCSV(string csvPath)
    {
        var lines = File.ReadAllLines(csvPath);
        return lines.Select(line => line.Split(',')).ToList();
    }

    public int FindRowByDate(List<string[]> data, string date)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i][0] == date)
            {
                return i;  // 返回日期对应的行号
            }
        }
        return -1;  // 如果未找到，返回 -1
    }
    public void UpdateOrAddData(string date, string category, float value)
    {
        var data = ReadCSV(filePath1);
        var dataWithDesc = ReadCSV(filePath2);
        int rowIndex = FindRowByDate(data, date);

        // 如果找到日期，更新对应列的值
        if (rowIndex != -1)
        {
            UpdateRow(data, rowIndex, category, value);
            UpdateDescRow(dataWithDesc, rowIndex, category, dataInput.GetDescription());
        }
        else
        {
            AddNewRow(data, date, category, value);
            AddDescRow(dataWithDesc, date, category, dataInput.GetDescription());
        }

        // 将数据重新写回 CSV 文件
        WriteCSV(data,filePath1);
        WriteCSV(dataWithDesc,filePath2);
    }
    
    private void UpdateDescRow(List<string[]> data, int rowIndex, string category, string desc)
    {
        int columnIndex = FindColumnIndexByCategory(category);  // Find the column index by category
        if (columnIndex != -1)
        {
            if (!string.IsNullOrEmpty(data[rowIndex][columnIndex]))
            {
                data[rowIndex][columnIndex] = data[rowIndex][columnIndex] + "; " + desc;
            }
            else
            {
                data[rowIndex][columnIndex] = desc;
            }
        }
    }
    
    private void AddDescRow(List<string[]> data, string date, string category, string desc)
    {
        var newRow = new string[data[0].Length];
        newRow[0] = date;
        int columnIndex = FindColumnIndexByCategory(category);

        if (columnIndex != -1)
        {
            newRow[columnIndex] = desc;
        }

        data.Add(newRow);
    }

    private void UpdateRow(List<string[]> data, int rowIndex, string category, float value)
    {
        int columnIndex = FindColumnIndexByCategory(category);  // Find the column index by category
        if (columnIndex != -1)
        {
            if (!string.IsNullOrEmpty(data[rowIndex][columnIndex]))
            {
                data[rowIndex][columnIndex] = (float.Parse(data[rowIndex][columnIndex]) + value).ToString();
            }
            else
            {
                data[rowIndex][columnIndex] = value.ToString();
            }
        }
    }

    private void AddNewRow(List<string[]> data, string date, string category, float value)
    {
        var newRow = new string[data[0].Length];
        newRow[0] = date;
        int columnIndex = FindColumnIndexByCategory(category);

        if (columnIndex != -1)
        {
            newRow[columnIndex] = value.ToString();
        }

        data.Add(newRow);
    }

    private void WriteCSV(List<string[]> data, string filePath)
    {
        var lines = data.Select(row => string.Join(",", row));
        File.WriteAllLines(filePath, lines);
        
    }
    
    private int FindColumnIndexByCategory(string category)
    {
        string[] headers = { "date","social","meetings","portfolio","linkedIn","behavior questions","gamejam","french","english","leetcode","minor projects","understand JD","language and API","classes and homework","major projects code","major project design","workout","drawing","other activites" };
        return Array.IndexOf(headers, category);  // 找到类别对应的列号
    }
    
    public void OnAddButtonClicked()
    {
        string selectedCategory = subcategorySelection.GetSelectedSubcategory();  // 获取选择的子类别
        float timeCost = float.Parse(dataInput.timeCostInputField.text);  // 获取输入的时间
        string date = DateTime.Now.ToString("MM/dd/yyyy");  // 当前日期

        UpdateOrAddData(date, selectedCategory, timeCost);  // 更新或添加数据
    }



}
