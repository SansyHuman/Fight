using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Hp P1health;
    [SerializeField] private Hp P2health;
    [SerializeField] private Text P1scoreText;
    [SerializeField] private Text P2scoreText;
    public float P1score = 0;
    public float P2score = 0;
    
    void Start()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        while (true)
        {
            if (P1health.MyCurrentValue <= 0) P2score += 1;
            if (P2health.MyCurrentValue <= 0) P1score += 1;
            yield return new WaitUntil(() => P1health.MyCurrentValue > 0 && P2health.MyCurrentValue > 0);
            yield return new WaitForSeconds(1);
            
        }   
    }
    void Update()
    {
        P1scoreText.text = "P1 Score : " + P1score;
        P2scoreText.text = "P2 Score : " + P2score;
    }
}
