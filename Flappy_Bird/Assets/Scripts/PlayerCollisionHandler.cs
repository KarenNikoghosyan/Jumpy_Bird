using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerCollisionHandler : MonoBehaviour
{
    public TextMeshProUGUI pipeScore;
    bool isChangeable = true;
    int score = 0;
    Material material;

    void Start()
    {
        pipeScore.text = score.ToString();
        material = FindObjectOfType<PipesSpawner>().material;
        material.color = Color.yellow;
    }

    void Update()
    {
        if (Debug.isDebugBuild) 
        {
            DebugKeys();
        }
    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            score += 9;
            pipeScore.text = score.ToString();
        }
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
            UpdatePipeColor();
        }

        if (other.gameObject.tag == "Pipe")
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GetComponent<PlayerController>().isDead = true;
        AudioManager.instance.Play("Death Sound");
        GetComponent<Animator>().SetBool("isDead", true);
        Invoke("ReloadLevel", 0.7f); // todo remove this line of code - after adding main menu, pause and restart functionality.
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void AddScore()
    {
        AudioManager.instance.Play("Scroing Sound");
        score++;
        pipeScore.text = score.ToString();
    }
    private void UpdatePipeColor()
    {
        if (score % 10 == 0)
        {
            FindObjectOfType<PipesSpawner>().SetRandomColor(isChangeable);
        }
    }

}
