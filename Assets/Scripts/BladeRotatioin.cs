using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotatioin : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Скорость вращения по осям X, Y и Z

    void Update()
    {
        // Вращаем объект каждый кадр
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
