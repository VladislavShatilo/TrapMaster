using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        for (int i = 0; i < Storage.Instance.countOfBlades; i++)
        {
            blades[i].SetActive(true);
            Debug.Log(Storage.Instance.countOfBlades);
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
            moveBlades[i].speed = Storage.Instance.speedBlade;
        }
        spawnInterval = Storage.Instance.spawnInterval;
        InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);
        addBladePriceText.text = FormatNumber((int)Storage.Instance.addBladePrice);
        addEnemyPriceText.text = FormatNumber((int)Storage.Instance.addEnemyPrice);
        addSpeedPriceText.text = FormatNumber((int)Storage.Instance.addSpeedPrice);

        addBladeLevelText.text = Storage.Instance.addBladeLevel.ToString();
        addEnemyLevelText.text = Storage.Instance.addEnemyLevel.ToString();
        addSpeedLevelText.text = Storage.Instance.addSpeedLevel.ToString();
        if (Storage.Instance.countOfBlades < 4)
        {
            if (Storage.Instance.addBladePrice < Storage.Instance.money)
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
        if (Storage.Instance.addEnemyPrice < Storage.Instance.money)
        {
            addEnemyPriceImage.color = new Color(24f / 255f, 238f / 255f, 0);
            addEnemyButton.enabled = true;
        }
        else
        {
            addEnemyPriceImage.color = Color.grey;
            addEnemyButton.enabled = false;

        }
        if (Storage.Instance.addSpeedPrice < Storage.Instance.money)
        {
            addSpeedPriceImage.color = new Color(204f / 255f, 20f / 255f, 1);
            addBladeButton.enabled = true;

        }
        else
        {
            addSpeedPriceImage.color = Color.grey;
            addBladeButton.enabled = false;

        }
        coinText.text = Storage.Instance.money.ToString();





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
        if(Storage.Instance.money > (int)Storage.Instance.addEnemyPrice)
        {
            Storage.Instance.money -= (int)Storage.Instance.addEnemyPrice;
            addEnemyPub(); 

        }
       

    }
    public void addEnemyPub()
    {
        if(Storage.Instance.addEnemyLevel <= 15)
        {
            Storage.Instance.spawnInterval -= Storage.Instance.spawnInterval * 0.15f;

        }
        else
        {
            Storage.Instance.spawnInterval -= Storage.Instance.spawnInterval * 0.07f;

        }
        Storage.Instance.addEnemyPrice *= 1.4f;
        Storage.Instance.addEnemyLevel++;
        
        reloadGame();

    }



    void addBlade()
    {
        if (Storage.Instance.money > (int)Storage.Instance.addBladePrice)
        {
            Storage.Instance.money -= (int)Storage.Instance.addBladePrice;
            Storage.Instance.countOfBlades++;
            blades[Storage.Instance.countOfBlades - 1].SetActive(true);
            Storage.Instance.addBladeLevel++;

            Storage.Instance.addBladePrice *= 7f;
            reloadGame();
        }


    }

    void addSpeed()
    {
        if (Storage.Instance.money > (int)Storage.Instance.addSpeedPrice)
        {
            Storage.Instance.money -= (int)Storage.Instance.addSpeedPrice;
            addSpeedPub();
        }

    }
    public void addSpeedPub()
    {
        Storage.Instance.addSpeedPrice *= 2f;
        Storage.Instance.addSpeedLevel++;
        Storage.Instance.speedBlade *= 1.25f;

        reloadGame();
    }


    void saveGame()
    {
        Storage.Instance.Save();
    }

    void Update()
    {
        
        if (Storage.Instance.countOfBlades < 4)
        {
            if (Storage.Instance.addBladePrice < Storage.Instance.money)
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
        if (Storage.Instance.addEnemyPrice < Storage.Instance.money)
        {
            addEnemyPriceImage.color = new Color(24f / 255f, 238f / 255f, 0);
            addEnemyButton.enabled = true;
        }
        else
        {
            addEnemyPriceImage.color = Color.grey;
            addEnemyButton.enabled = false;

        }
        if (Storage.Instance.addSpeedPrice < Storage.Instance.money)
        {
            addSpeedPriceImage.color = new Color(204f / 255f, 20f / 255f, 1);
            addSpeedButton.enabled = true;

        }
        else
        {
            addSpeedPriceImage.color = Color.grey;
            addSpeedButton.enabled = false;

        }
        coinText.text =FormatNumber( Storage.Instance.money);
    }

    void SpawnObject()
    {
        random = Random.Range(20f, 22.5f);
        Instantiate(objectToSpawn,new Vector3(random, pointToSpawn.transform.position.y,  pointToSpawn.transform.position.z), pointToSpawn.transform.rotation);

        //objectToSpawn.GetComponent<playerRun>().enabled = true;
    }
}
