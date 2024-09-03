using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public GameObject pointToSpawn;
    public GameObject objectToSpawn;
    public float spawnInterval; // Интервал в секундах
    private float random;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Button addBladeButton;
    [SerializeField] private Button addEnemyButton;
    [SerializeField] private Button addSpeedButton;

    [SerializeField] private TextMeshProUGUI addBladePriceText;
    [SerializeField] private TextMeshProUGUI addEnemyPriceText;
    [SerializeField] private TextMeshProUGUI addSpeedPriceText;

    [SerializeField] private Image addBladePriceImage;
    [SerializeField] private Image addEnemyPriceImage;
    [SerializeField] private Image addSpeedPriceImage;

    [SerializeField] private TextMeshProUGUI addBladeLevelText;
    [SerializeField] private TextMeshProUGUI addEnemyLevelText;
    [SerializeField] private TextMeshProUGUI addSpeedLevelText;

    [SerializeField] private GameObject[] blades;
    [SerializeField] private MoveBlade[] moveBlades;
    public static string FormatNumber(float number)
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
        //YandexGame.ResetSaveProgress();

        reloadGame();
        
        for (int i = 0; i < 4; i++)
        {
            blades[i].SetActive(false);
        }
        for (int i = 0; i < YandexGame.savesData.countOfBlades; i++)
        {
            blades[i].SetActive(true);
            Debug.Log(YandexGame.savesData.countOfBlades);
        }

        InvokeRepeating("saveGame", 2f, 2f);

    }
    void OnEnable()
    {
        addEnemyButton.onClick.AddListener(delegate { addEnemy(); });
        addBladeButton.onClick.AddListener(delegate { addBlade(); });
        addSpeedButton.onClick.AddListener(delegate { addSpeed(); });
        for (int i = 0; i < 4; i++)
        {
            moveBlades[i].speed = YandexGame.savesData.speedBlade;
        }
        spawnInterval = YandexGame.savesData.spawnInterval;
        InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);
        addBladePriceText.text = FormatNumber((int)YandexGame.savesData.addBladePrice);
        addEnemyPriceText.text = FormatNumber((int)YandexGame.savesData.addEnemyPrice);
        addSpeedPriceText.text = FormatNumber((int)YandexGame.savesData.addSpeedPrice);

        addBladeLevelText.text = YandexGame.savesData.addBladeLevel.ToString();
        addEnemyLevelText.text = YandexGame.savesData.addEnemyLevel.ToString();
        addSpeedLevelText.text = YandexGame.savesData.addSpeedLevel.ToString();
        if (YandexGame.savesData.countOfBlades < 4)
        {
            if (YandexGame.savesData.addBladePrice < YandexGame.savesData.money)
            {
                addBladePriceImage.color = new Color(0, 110f / 255f, 1);
                addBladeButton.enabled = true;

            }
            else
            {
                addBladePriceImage.color = Color.grey;
                addBladeButton.enabled = false;

            }
        }
        else
        {
            addBladePriceImage.color = Color.grey;
            addBladeButton.enabled = false;
        }
        if (YandexGame.savesData.addEnemyPrice < YandexGame.savesData.money)
        {
            addEnemyPriceImage.color = new Color(24f / 255f, 238f / 255f, 0);
            addEnemyButton.enabled = true;
        }
        else
        {
            addEnemyPriceImage.color = Color.grey;
            addEnemyButton.enabled = false;

        }
        if (YandexGame.savesData.addSpeedPrice < YandexGame.savesData.money)
        {
            addSpeedPriceImage.color = new Color(204f / 255f, 20f / 255f, 1);
            addBladeButton.enabled = true;

        }
        else
        {
            addSpeedPriceImage.color = Color.grey;
            addBladeButton.enabled = false;

        }
        coinText.text = YandexGame.savesData.money.ToString();





    }
    void OnDisable()
    {
        addEnemyButton.onClick.RemoveAllListeners();
        addBladeButton.onClick.RemoveAllListeners();
        addSpeedButton.onClick.RemoveAllListeners();
        CancelInvoke("SpawnObject");
    }
    void reloadGame()
    {
        gameObject.SetActive(false);
        CancelInvoke("SpawnObject");
        gameObject.SetActive(true);
    }
    void addEnemy()
    {
        if(YandexGame.savesData.money > (int)YandexGame.savesData.addEnemyPrice)
        {
            YandexGame.savesData.money -= (int)YandexGame.savesData.addEnemyPrice;
            addEnemyPub(); 

        }
       

    }
    public void addEnemyPub()
    {
        YandexGame.savesData.spawnInterval -= YandexGame.savesData.spawnInterval * 0.07f;
        YandexGame.savesData.addEnemyPrice *= 1.4f;
        YandexGame.savesData.addEnemyLevel++;
        reloadGame();

    }



    void addBlade()
    {
        if (YandexGame.savesData.money > (int)YandexGame.savesData.addBladePrice)
        {
            YandexGame.savesData.money -= (int)YandexGame.savesData.addBladePrice;
            YandexGame.savesData.countOfBlades++;
            blades[YandexGame.savesData.countOfBlades - 1].SetActive(true);
            YandexGame.savesData.addBladeLevel++;

            YandexGame.savesData.addBladePrice *= 3f;
            reloadGame();
        }


    }

    void addSpeed()
    {
        if (YandexGame.savesData.money > (int)YandexGame.savesData.addSpeedPrice)
        {
            YandexGame.savesData.money -= (int)YandexGame.savesData.addSpeedPrice;
            addSpeedPub();
        }

    }
    public void addSpeedPub()
    {
        YandexGame.savesData.addSpeedPrice *= 2f;
        YandexGame.savesData.addSpeedLevel++;
        YandexGame.savesData.speedBlade *= 1.5f;

        reloadGame();
    }


    void saveGame()
    {
        YandexGame.SaveProgress();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            YandexGame.ResetSaveProgress();
        }
        if (YandexGame.savesData.countOfBlades < 4)
        {
            if (YandexGame.savesData.addBladePrice < YandexGame.savesData.money)
            {
                addBladePriceImage.color = new Color(0, 110f / 255f, 1);
                addBladeButton.enabled = true;

            }
            else
            {
                addBladePriceImage.color = Color.grey;
                addBladeButton.enabled = false;

            }
        }
        else
        {
            
            addBladePriceImage.color = Color.grey;
            addBladeButton.enabled = false;
        }
        if (YandexGame.savesData.addEnemyPrice < YandexGame.savesData.money)
        {
            addEnemyPriceImage.color = new Color(24f / 255f, 238f / 255f, 0);
            addEnemyButton.enabled = true;
        }
        else
        {
            addEnemyPriceImage.color = Color.grey;
            addEnemyButton.enabled = false;

        }
        if (YandexGame.savesData.addSpeedPrice < YandexGame.savesData.money)
        {
            addSpeedPriceImage.color = new Color(204f / 255f, 20f / 255f, 1);
            addBladeButton.enabled = true;

        }
        else
        {
            addSpeedPriceImage.color = Color.grey;
            addBladeButton.enabled = false;

        }
        coinText.text =FormatNumber( YandexGame.savesData.money);
    }

    void SpawnObject()
    {
        random = Random.Range(20f, 22.5f);
        Instantiate(objectToSpawn,new Vector3(random, pointToSpawn.transform.position.y,  pointToSpawn.transform.position.z), pointToSpawn.transform.rotation);

        //objectToSpawn.GetComponent<playerRun>().enabled = true;
    }
}
