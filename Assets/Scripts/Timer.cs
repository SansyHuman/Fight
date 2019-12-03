using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public GameObject P1Wintext;
    public GameObject P2Wintext;
    public GameObject Drawtext;
    public GameObject Retrytext;
    Score score;
    
    // for 30 seconds
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
        time = 30f;
    }

    void Update()
    {
        if (time != 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
            }
        }
        if (time <= 0)
        {
            Retrytext.SetActive(true);

            if (score.P1score > score.P2score)
            {
                P1Wintext.SetActive(true);
                P2Wintext.SetActive(false);
                Drawtext.SetActive(false);
            }

            if (score.P1score < score.P2score)
            {
                P2Wintext.SetActive(true);
                P1Wintext.SetActive(false);
                Drawtext.SetActive(false);
            }

            if (score.P1score == score.P2score)
            {
                Drawtext.SetActive(true);
                P1Wintext.SetActive(false);
                P2Wintext.SetActive(false);
            }
        }
        int t = Mathf.FloorToInt(time);
        Text uiText = GetComponent<Text>();
        uiText.text = "Time : " + t.ToString();
    }
}
