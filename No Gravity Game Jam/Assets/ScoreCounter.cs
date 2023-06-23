using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreCount;
    public int distance = 0;
    public float distanceTimer = 0.1f;

    private void Start()
    {
        StartCoroutine(AddDistance());
    }

    void FixedUpdate()
    {
        scoreCount.text = "DISTANCE: " + distance;
    }

    IEnumerator AddDistance()
    {
        while (true)
        {
            distance++;
            yield return new WaitForSeconds(distanceTimer);
        }
        
    }
}
