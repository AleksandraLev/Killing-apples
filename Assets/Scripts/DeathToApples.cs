using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class DeathToApples : MonoBehaviour
{
    void Start()
    {
        // ������ �� ������ ��� ������
    }

    private void Update()
    {
        // ���������, ��� �� �������� ����
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // ������� ��� �� ������� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������, ����� �� ��� � �����-�� ������
            if (Physics.Raycast(ray, out hit))
            {
                // ���������, ������� �� ������ � �������� �� �� �������
                if (hit.collider.gameObject.activeSelf && hit.collider.CompareTag("Apple"))
                {
                    // ���� ��, ������ ���� ������ ����������
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
    //    // ������� ��� �� ������� ����
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    // ���������, ����� �� ��� � �����-�� ������
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        // ���������, ������� �� ������ � �������� �� �� �������
    //        if (hit.collider.gameObject.activeSelf && hit.collider.CompareTag("Apple"))
    //        {
    //            // ���� ��, ������ ���� ������ ����������
    //            hit.collider.gameObject.SetActive(false);
    //            Score.score += 1;
    //            Score.countApples -= 1;
    //            //Debug.Log("Destroyed an apple.");
    //        }
    //    }
    //}
}