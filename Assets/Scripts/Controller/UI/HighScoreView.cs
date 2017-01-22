using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{

    [Header("References")]
    public Text HighscoreValue;

    private GameDataController gameDataController;

    void Awake()
    {
        gameDataController = GameDataController.Instance;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        RefreshHighscore();
        //gameDataController.SaveHighScore(new Highscore { Seconds = 120 });
    }

    public void RefreshHighscore()
    {
        gameDataController.LoadHighScore();

        if (gameDataController.CurrentHighScore == null)
            HighscoreValue.text = "-------";
        else
            HighscoreValue.text = string.Format("{0} minutes :{1:00} seconds", gameDataController.CurrentHighScore.Seconds / 60, gameDataController.CurrentHighScore.Seconds % 60);
    }
}
