using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject rangerPrefab;
    //[SerializeField] private GameObject meleePrefab;

    [SerializeField] private float rangerInterval = 2;
    //[SerializeField] private float meleeInterval = 3.5f;

    public Transform Enemies;

    void Start()
    {
        //StartCoroutine(spawnEnemy(meleeInterval, meleePrefab));
        StartCoroutine(spawnEnemy(rangerInterval, rangerPrefab));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { StartCoroutine(spawnEnemy(rangerInterval, rangerPrefab)); }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        Debug.Log(Enemies);
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(14.84f, Random.Range(-6f, 6), 0), Quaternion.identity, Enemies);
        newEnemy.name = "Shooting Enemy";
       // StartCoroutine(spawnEnemy(interval, newEnemy));
    }

}
