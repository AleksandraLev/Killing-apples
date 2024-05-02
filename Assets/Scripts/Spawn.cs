using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnOfApples : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10; // ������ ����
    //float time = 6.0005f;
    float time = 4f;
    private List<GameObject> pool = new List<GameObject>();
    float[] forRandom = new float[100];
    int lengthOfArray = 0;

    private IEnumerator delay;

    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        for (float i = -0.3f; i <= 0.3f; i += 0.1f)
        {
            forRandom[j] = i;
            j++;
        }
        lengthOfArray = j;

        // ������������� ���� ��������
        for (int i = 0; i < poolSize; i++)
        {
            //Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]) + ProgrammManager.SpawnVector3;
            Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]);
            GameObject obj = Instantiate(prefab, position, Quaternion.identity, prefab.transform);
            obj.SetActive(true);
            Score.countApples += 1;
            pool.Add(obj);
        }
        StartCoroutine(Spawn());
    }
    //private void Wait()
    //{
    //    if (time >= 1)
    //    {
    //        time -= 0.05f;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {       
    }

    IEnumerator Spawn()
    {
        //if (Score.countApples < poolSize)
        //{
            
        //}
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                Debug.Log("0");
            }
            else
            {
                Debug.Log("1");
            }
            if (!obj.activeSelf)
            {
                Take(obj);
                yield return new WaitForSeconds(time);
            }

        }
        StartCoroutine(Spawn());              
    }

    //private void Return(GameObject @object)
    //{
    //    @object.SetActive(false);
    //    //@object.transform.position = new Vector3(UnityEngine.Random.Range(-4, 4), -0.4f, UnityEngine.Random.Range(4, -4));
    //    Score.score += 1;
    //    Score.countApples -= 1;
    //    Debug.Log("Return\nScore: " + Score.score + "\nCount Apples: " + Score.countApples);
    //    //Debug.Log("Input.touchCount > 0");
    //}
    private void Take(GameObject @object)
    {
        //@object.transform.position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]) + ProgrammManager.SpawnVector3;
        
        @object.transform.position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]);
        @object.SetActive(true);

        Score.countApples += 1;
        //Debug.Log("Take\nCount Apples: " + Score.countApples);
        //Debug.Log("Input.touchCount > 0");
    }
    //private void OnMouseDown()
    //{
    //    Return(gameObject);
    //}

}