using System.Collections;
using UnityEngine;
using TMPro;

public class SceneScript : MonoBehaviour
{
    public GameObject winningPanel;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"Score: {PlayerPrefs.GetInt("Score")}";
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {PlayerPrefs.GetInt("Score")}";
        CheckWinning();
    }

    void CheckWinning()
    {
        if (PlayerPrefs.GetInt("Score") >= 10)
        {
            Debug.Log("You won");
            winningPanel.SetActive(true);
            PlayerPrefs.SetInt("Score", 0);
            scoreText.text = $"Score: {PlayerPrefs.GetInt("Score")}";
        }
    }
}
