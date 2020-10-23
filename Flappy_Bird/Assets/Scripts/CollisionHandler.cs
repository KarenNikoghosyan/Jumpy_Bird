using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pipeScore;
    MeshRenderer pipeChildren;
    int score = 0;

    bool isChangeable = true;

    void Start()
    {
        pipeScore.text = score.ToString();
        pipeChildren = GetComponent<MeshRenderer>();
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
            ChangePipeColor();
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
    private void ChangePipeColor()
    {
        if (score % 10 == 0)
        {
            FindObjectOfType<PipesSpawner>().ChangePipeColor(isChangeable);
        }
    }

}
