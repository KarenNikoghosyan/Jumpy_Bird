using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pipeScore;
    int score = 0;

    void Start()
    {
        pipeScore.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            KillPlayer();
        }

        if (other.gameObject.tag == "Score")
        {
            AddScore();
        }
    }

    private void KillPlayer()
    {
        Destroy(gameObject, 1f);
        SceneManager.LoadScene(0); // todo remove this line of code
    }

    private void AddScore()
    {
        score++;
        pipeScore.text = score.ToString();
    }
}
