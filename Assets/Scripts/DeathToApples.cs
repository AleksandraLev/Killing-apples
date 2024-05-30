using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class DeathToApples : MonoBehaviour
{
    void Start()
    {
        // Ничего не делаем при старте
    }

    private void Update()
    {
        // Проверяем, был ли совершен клик
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // Создаем луч из позиции мыши
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, попал ли луч в какой-то объект
            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, активен ли объект и является ли он яблоком
                if (hit.collider.gameObject.activeSelf && hit.collider.CompareTag("Apple"))
                {
                    // Если да, делаем этот объект неактивным
                    hit.collider.gameObject.SetActive(false);
                    Score.score += 1;
                    Score.countApples -= 1;
                    //Debug.Log("Destroyed an apple.");
                }
            }
        }
    }
    //void OnButtonClick()
    //{
    //    // Создаем луч из позиции мыши
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    // Проверяем, попал ли луч в какой-то объект
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        // Проверяем, активен ли объект и является ли он яблоком
    //        if (hit.collider.gameObject.activeSelf && hit.collider.CompareTag("Apple"))
    //        {
    //            // Если да, делаем этот объект неактивным
    //            hit.collider.gameObject.SetActive(false);
    //            Score.score += 1;
    //            Score.countApples -= 1;
    //            //Debug.Log("Destroyed an apple.");
    //        }
    //    }
    //}
}