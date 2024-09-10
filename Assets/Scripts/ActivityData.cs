using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActivityData
{
    public string date;
    public string subcategory;
    public float timeCost;
    public string description;
}

[System.Serializable]
public class ActivityDataList
{
    public List<ActivityData> activities = new List<ActivityData>();
}



