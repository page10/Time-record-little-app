using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubcategorySelection : MonoBehaviour
{
    public List<Toggle> toggles; // Assign all the toggles in the inspector

    private string selectedSubcategory = string.Empty;

    void Start()
    {
        // Add listeners to each toggle in the list
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleChanged(toggle); });
            if (string.IsNullOrEmpty(selectedSubcategory)) toggle.isOn = true;  //自动选中
        }
    }

    public void OnToggleChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            // Unselect all other toggles
            UnselectOtherToggles(changedToggle);

            // Update selectedSubcategory based on the changed toggle's name (or custom property)
            selectedSubcategory = changedToggle.name; // Assuming toggles are named like "social", "meetings", etc.
            Debug.Log("Selected Subcategory: " + selectedSubcategory);
        }
    }

    private void UnselectOtherToggles(Toggle activeToggle)
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle != activeToggle)
            {
                toggle.isOn = false; // Uncheck other toggles
            }
        }
    }

    public string GetSelectedSubcategory()
    {
        return selectedSubcategory;
    }
    

}