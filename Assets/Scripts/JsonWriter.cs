using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonWriter : MonoBehaviour
{

    public DataInput dataInput;
    public SubcategorySelection subcategorySelection;
    
    ActivityData newActivity = new ActivityData();
    JsonDataHandler jsonHandler;

    private void Awake()
    {
        jsonHandler = new JsonDataHandler(Application.persistentDataPath + "/activity_data.json");
    }

    public void OnAddButtonClicked()
    {
        if (jsonHandler == null)
        {
            Debug.LogError("Json Handler has not been constructed");
            return;
        }
        newActivity.date = DateTime.Now.ToString("MM/dd/yyyy");
        newActivity.subcategory = subcategorySelection.GetSelectedSubcategory();
        newActivity.timeCost = dataInput.GetTimeCost();
        newActivity.description = dataInput.GetDescription();
        jsonHandler.SaveData(newActivity);
    }
    
}

//don't inherit from mono if not necessary
public class JsonDataHandler
{
    private string filePathJson;
    
    public void SaveData(ActivityData activity)
    {
        if (string.IsNullOrEmpty(filePathJson) || !File.Exists(filePathJson)) return;

        ActivityDataList dataList = new ActivityDataList();

        if (System.IO.File.Exists(filePathJson))
        {
            string jsonData = System.IO.File.ReadAllText(filePathJson);
            dataList = JsonUtility.FromJson<ActivityDataList>(jsonData);
        }

        dataList.activities.Add(activity);

        string json = JsonUtility.ToJson(dataList, true);
        System.IO.File.WriteAllText(filePathJson, json);
    }

    public JsonDataHandler(string path)
    {
        filePathJson = path;
    }
}