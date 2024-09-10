using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonWriter : MonoBehaviour
{

    public DataInput dataInput;
    public SubcategorySelection subcategorySelection;
    
    ActivityData newActivity = new ActivityData();
    JsonDataHandler jsonHandler = new JsonDataHandler();

    public void OnAddButtonClicked()
    {
        newActivity.date = DateTime.Now.ToString("MM/dd/yyyy");
        newActivity.subcategory = subcategorySelection.GetSelectedSubcategory();
        newActivity.timeCost = dataInput.GetTimeCost();
        newActivity.description = dataInput.GetDescription();
        jsonHandler.SaveData(newActivity);
    }
    
}

public class JsonDataHandler : MonoBehaviour
{
    private string filePathJson;

    private void Start()
    {
        filePathJson = Application.persistentDataPath + "/activity_data.json";
    }

    public void SaveData(ActivityData activity)
    {
        if (string.IsNullOrEmpty(filePathJson))
        {
            filePathJson = Application.persistentDataPath + "/activity_data.json";
        }

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
}