using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnOfApples : MonoBehaviour
{
    public GameObject prefab;
    int poolSize = 20; // Размер пула
    //float time = 6.0005f;
    public float spawnDelay = 3f;
    private List<GameObject> pool = new List<GameObject>();
    float[] forRandom = new float[100];
    int lengthOfArray = 0;

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

        // Инициализация пула объектов
        for (int i = 0; i < poolSize/2; i++)
        {
            Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]) + ProgrammManager.SpawnVector3;
            //Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]);
            GameObject obj = Instantiate(prefab, position, Quaternion.identity, prefab.transform);
            obj.SetActive(true);
            Score.countApples += 1;
            pool.Add(obj);
        }
        for (int i = poolSize / 2; i < poolSize; i++)
        {
            Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]) + ProgrammManager.SpawnVector3;
            //Vector3 position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]);
            GameObject obj = Instantiate(prefab, position, Quaternion.identity, prefab.transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
        StartCoroutine(Spawn());
    }
    private void Wait()
    {
        if (spawnDelay >= 1)
        {
            spawnDelay -= 0.05f;
        }
        Debug.Log(spawnDelay);
    }

    IEnumerator Spawn()
    {
        bool anyInactive = false;

        // Проверяем, есть ли хотя бы один неактивный объект в пуле
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                anyInactive = true;
                break;
            }
        }

        // Если есть хотя бы один неактивный объект, активируем его
        if (anyInactive)
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                {
                    Take(obj);
                    yield return new WaitForSeconds(spawnDelay);
                    Wait();
                }
            }
        }
        else
        {
            // Если все объекты активны, просто ждем следующего цикла спауна
            yield return new WaitForSeconds(spawnDelay);
        }

        // После активации всех объектов вызываем Spawn снова
        StartCoroutine(Spawn());
        //Debug.Log("Count: " + Score.countApples);
    }

    private void Take(GameObject @object)
    {
        @object.transform.position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]) + ProgrammManager.SpawnVector3;
        //@object.transform.position = new Vector3(forRandom[UnityEngine.Random.Range(0, lengthOfArray)], -0.4f, forRandom[UnityEngine.Random.Range(0, lengthOfArray)]);
        
        @object.SetActive(true);

        Score.countApples += 1;
    }
}
