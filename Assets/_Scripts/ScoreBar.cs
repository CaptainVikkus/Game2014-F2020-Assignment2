using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    public static int score = 2000;
    [SerializeField] TextMeshProUGUI scoreLabel;

    private void Start()
    {
        StartCoroutine(LoseScore());
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score.ToString();
    }

    IEnumerator LoseScore()
    {
        while (this.isActiveAndEnabled)
        {
            if (score > 0)
                score -= 1;
            else 
                score = 0;

            yield return new WaitForSeconds(1 / 15f);
        }
    }
}
