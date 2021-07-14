using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImage;
    public Text scoreText;
    public GameObject TitleScreen;
    private int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLives(ShipConst.DefaultLifes);
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = lives[currentLives];
    }
    public void UpdateScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }
    public void ShowTitleScreen()
    {
        scoreText.enabled = false;
        livesImage.enabled = false;
        TitleScreen.SetActive(true);
        totalScore = 0;
        UpdateScore(0);
    }
    public void HideTitleScreen()
    {
        scoreText.enabled = true;
        livesImage.enabled = true;
        TitleScreen.SetActive(false);
    }
   
}
