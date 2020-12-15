using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour
{
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    private int lives;
    public int Lives { get { return lives; } }
 

    void _HideLives()
    {
        switch (lives)
        {
            case 0:
                break;
            case 1:
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                break;
            case 2:
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                break;
            case 3:
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                break;
            default:
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                break;
        }
    }

    public void AddLife()
    {
        lives++;
        if (lives > 3) { lives = 3; }
    }

    public void LoseLife()
    {
        lives--;
        if (lives < 0) { lives = 0; }
    }
}
