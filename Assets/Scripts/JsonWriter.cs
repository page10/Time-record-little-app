using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonWriter : MonoBehaviour
{

    public DataInput dataInput;
    public SubcategorySelection subcategorySelection;
    
    
    //JsonDataHandler jsonHandler;
    private string _jsonPath;
    
    private void Awake()
    {
        _jsonPath = Application.persistentDataPath + "/activity_data.json";
        //jsonHandler = new JsonDataHandler(Application.persistentDataPath + "/activity_data.json");
    }

    public void OnAddButtonClicked()
    {
        if (string.IsNullOrEmpty(_jsonPath))
        {
            Debug.LogError("Json path is not set");
            return;
        }
        ActivityData newActivity = new ActivityData
        {
            date = DateTime.Now.ToString("MM/dd/yyyy"),
            subcategory = subcategorySelection.GetSelectedSubcategory(),
            timeCost = dataInput.GetTimeCost(),
            description = dataInput.GetDescription()
        };
        // newActivity.date = DateTime.Now.ToString("MM/dd/yyyy");
        // newActivity.subcategory = subcategorySelection.GetSelectedSubcategory();
        // newActivity.timeCost = dataInput.GetTimeCost();
        // newActivity.description = dataInput.GetDescription();
        //jsonHandler.SaveData(newActivity);
        
        ActivityDataList dataList = new ActivityDataList();

        if (File.Exists(_jsonPath))
        {
            string jsonData = File.ReadAllText(_jsonPath);
            dataList = JsonUtility.FromJson<ActivityDataList>(jsonData);
        }

        dataList.activities.Add(newActivity);

        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(_jsonPath, json);
    }
    
}

// public class JsonDataHandler
// {
//     private string filePathJson;
//     
//     public void SaveData(ActivityData activity)
//     {
//         if (string.IsNullOrEmpty(filePathJson) || !File.Exists(filePathJson)) return;
//
//         ActivityDataList dataList = new ActivityDataList();
//
//         if (System.IO.File.Exists(filePathJson))
//         {
//             string jsonData = System.IO.File.ReadAllText(filePathJson);
//             dataList = JsonUtility.FromJson<ActivityDataList>(jsonData);
//         }
//
//         dataList.activities.Add(activity);
//
//         string json = JsonUtility.ToJson(dataList, true);
//         System.IO.File.WriteAllText(filePathJson, json);
//     }
//
//     public JsonDataHandler(string path)
//     {
//         filePathJson = path;
//     }
// }