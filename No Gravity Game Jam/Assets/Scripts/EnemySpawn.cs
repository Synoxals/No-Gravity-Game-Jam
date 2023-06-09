using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private GameObject meleePrefab;

    [SerializeField] private float rangerInterval = 2;
    [SerializeField] private float meleeInterval = 3.5f;
    int y;

    public Transform Enemies;
    void Start()
    {
        StartCoroutine(spawnEnemy2(meleeInterval, meleePrefab));
        StartCoroutine(spawnEnemy1(rangerInterval, rangerPrefab));
    }

    private void Update()
    {
        rangerInterval = rangerInterval - 0.00001f;
        meleeInterval = meleeInterval + 0.00001f;
    }



    private IEnumerator spawnEnemy1(float interval, GameObject enemy)
    {
        Debug.Log(Enemies);
        yield return new WaitForSeconds(interval);
        Instantiate(enemy, new Vector3(14.84f, Random.Range(-6f, 6), 0), Quaternion.identity, Enemies);
        StartCoroutine(spawnEnemy1(interval, enemy));
    }
    private IEnumerator spawnEnemy2(float interval, GameObject enemy)
    {
        int rand= Random.Range(1,3);
        if (rand == 1) {  y = -8; }
        else if (rand == 2) {  y = 8; }
        Debug.Log(Enemies);
        yield return new WaitForSeconds(interval);
        Instantiate(enemy, new Vector3(Random.Range(-10f, 10),y,0), Quaternion.identity, Enemies);
        StartCoroutine(spawnEnemy2(interval, enemy));
    }
}
