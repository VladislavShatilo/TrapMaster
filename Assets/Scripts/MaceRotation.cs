using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MaceRotation : MonoBehaviour
{
    // Start is called before the first frame
    
    private Vector3 rotationSpeed;
    public GameObject particleObject;
    public bool isLeft = false;
  
    private void OnTriggerEnter(Collider other)
    {

        GameObject particle = Instantiate(particleObject, new Vector3(other.gameObject.transform.position.x, -0.5f, other.gameObject.transform.position.z), other.gameObject.transform.rotation);
        other.gameObject.GetComponent<AudioSource>().Play();
        Destroy(other.gameObject);

        YandexGame.savesData.money += Random.Range(1, 3);
        YandexGame.savesData.kills++;
        StartCoroutine(clear(particle));

    }
    IEnumerator clear(GameObject particle1)
    {
        yield return new WaitForSeconds(7);
        Destroy(particle1);
    }
    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            rotationSpeed = new Vector3(0, -YandexGame.savesData.speedMace, 0);
        }
        else
        {
            rotationSpeed = new Vector3(0,YandexGame.savesData.speedMace, 0);
        }
        
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
