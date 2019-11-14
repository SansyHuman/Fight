using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enemy;
// Score +1 if Enemy dies(maybe I should add in Enemy Script)

namespace Enemy
{
    public class Score : MonoBehaviour
    {
        public static int score;
        private Text text;
        void Start()
        {
            text = GetComponent<Text>();
        }

        void Update()
        {
            text.text = "Score : " + score.ToString();
        }
    }
}