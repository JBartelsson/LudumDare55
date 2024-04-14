using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;

    }

    public void OpenGameOver()
    {
        canvas.enabled = true;
        text.text = GameManager.Instance.round.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
