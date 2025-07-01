using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadStage : MonoBehaviour
{
    [SerializeField] private GameObject[] blades;
    [SerializeField] private GameObject[] maces;
    [SerializeField] private GameObject manager;
    [SerializeField] private TextMeshProUGUI stageValueText;
    [SerializeField] private Image onTrapButtonImage;
    [SerializeField] private Sprite[] onTrapButtonSprite;


    void Awake ()
    {
     
        if(Storage.Instance.scene == 1)
        {
            manager.GetComponent<GameManager>().enabled = true;
            for (int i = 0; i < maces.Length; i++)
            {
                maces[i].SetActive(false);
            }
            manager.GetComponent<Manager>().enabled = false;
            stageValueText.text = "1";
            onTrapButtonImage.sprite = onTrapButtonSprite[0];

        }
        else if(Storage.Instance.scene == 2)
        {
            for (int i = 0; i < blades.Length; i++)
            {
                blades[i].SetActive(false);
            }
            manager.GetComponent<GameManager>().enabled = false;
            manager.GetComponent<Manager>().enabled = true;
            stageValueText.text = "2";
            onTrapButtonImage.sprite = onTrapButtonSprite[1];
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
