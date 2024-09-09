using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataInput : MonoBehaviour
{
    public InputField descriptionInputField;
    public InputField timeCostInputField;

    public string GetDescription()
    {
        return descriptionInputField.text;
    }

    public int GetTimeCost()
    {
        int timeCost;
        int.TryParse(timeCostInputField.text, out timeCost);
        return timeCost;
    }
}
