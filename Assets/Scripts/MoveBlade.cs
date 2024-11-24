using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using static UnityEngine.ParticleSystem;

public class MoveBlade : MonoBehaviour
{
    public Vector3 pointA; // Первая точка
    public Vector3 pointB; // Вторая точка
    public float speed = 2.0f; // Скорость перемещения
    public GameObject particleObject;
    private Vector3 targetPosition; // Текущая целевая позиция
    void Start()
    {
        // Начинаем движение к первой точке
        targetPosition = pointB;
    }

    void Update()
    {
        // Перемещаем объект к текущей целевой позиции
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Проверяем, достиг ли объект целевой позиции
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Если достиг, меняем целевую позицию на противоположную
            targetPosition = targetPosition == pointA ? pointB : pointA;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);

        GameObject particle = Instantiate(particleObject, new Vector3(transform.position.x,-0.5f,transform.position.z), transform.rotation);

        YandexGame.savesData.money += Random.Range(1, 10) ;
        YandexGame.savesData.kills++;
        StartCoroutine(clear(particle));
 
    }
    IEnumerator clear(GameObject particle1)
    {
        yield return new WaitForSeconds(7);
        Destroy(particle1);
    }
}
