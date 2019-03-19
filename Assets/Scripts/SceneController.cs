using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public Text scoreTxt;
    //public Text passengersTxt;

    private string convert;

    [SerializeField] int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (scoreTxt) { scoreTxt.text = "Score: " + score.ToString(); }
    }

    // Update is called once per frame
    void Update()
    {
        if(score == 6)
        {
            SceneManager.LoadScene("win");
        }
    }

    public void UpdateScore(int point)      //add point to the current score then set point to 0
    {
        score = score + point;
        point = 0;

        scoreTxt.text = "Score: " + score.ToString();

        //Debug.Log(score);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("MASHClone");
    }
}
