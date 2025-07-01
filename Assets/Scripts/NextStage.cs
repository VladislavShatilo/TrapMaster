using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextStage : MonoBehaviour
{
    [SerializeField] private Button nextStageButton;
    [SerializeField] private GameObject[] blades;
    [SerializeField] private GameObject manager;
    [SerializeField] private TextMeshProUGUI stageValueText;
    [SerializeField] private Image onTrapButtonImage;
    [SerializeField] private Sprite onTrapButtonSprite;

    void Start()
    {
        nextStageButton.onClick.AddListener(delegate { nextStage(); });
    }

    void nextStage()
    {
        manager.GetComponent<GameManager>().enabled = false;
        Storage.Instance.ResetSave();
        Storage.Instance.Save();
        Storage.Instance.killsGoal = 50000;
        Storage.Instance.scene = 2;
        Storage.Instance.Save();
        manager.GetComponent<Manager>().enabled = true;
        
        for(int i = 0;  i < blades.Length; i++)
        {
            blades[i].SetActive(false);
        }
       
        stageValueText.text = "2";
        onTrapButtonImage.sprite = onTrapButtonSprite;
        gameObject.SetActive(false);


    }
   

   
    void Update()
    {
        
    }
}
