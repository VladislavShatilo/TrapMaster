using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI kills;
   
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject newStageObject;
    public  string FormatNumber(float number)
    {
        if (number >= 1_000_000)
        {
            return (number / 1_000_000).ToString("0.##") + "m";
        }
        else if (number >= 1_000)
        {
            return (number / 1_000).ToString("0.##") + "k";
        }
        else
        {
            return number.ToString();
        }
    }
    void Start()
    {
        if (Storage.Instance.kills >= Storage.Instance.killsGoal)
        {
            newStageObject.SetActive(true);
        }

    }
    void Update()
    {
        kills.text = FormatNumber(Storage.Instance.kills) + " / "+ FormatNumber(Storage.Instance.killsGoal);
        slider.value = Storage.Instance.kills / Storage.Instance.killsGoal;
        if(Storage.Instance.kills == Storage.Instance.killsGoal)
        {
            newStageObject.SetActive(true);
        }
    }

    // Update is called once per frame

}
