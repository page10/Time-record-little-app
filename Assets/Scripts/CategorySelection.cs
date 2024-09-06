using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelection : MonoBehaviour
{
    public GameObject immigrationSubcategories;
    public GameObject careerSubcategories;
    public GameObject personalSubcategories;

    public void OnCategorySelected(string category)
    {
        immigrationSubcategories.SetActive(false);
        careerSubcategories.SetActive(false);
        personalSubcategories.SetActive(false);
    
        if (category == "immigration")
            immigrationSubcategories.SetActive(true);
        else if (category == "career")
            careerSubcategories.SetActive(true);
        else if (category == "personal")
            personalSubcategories.SetActive(true);
    }
    
    
}
