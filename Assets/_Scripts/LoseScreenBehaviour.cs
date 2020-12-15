using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseScreenBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = "Score: " + ScoreBar.score;
    }
}
