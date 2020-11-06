using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerCollisionHandler : MonoBehaviour
{
    bool isChangeable = true, isDead = false;
    Material material;
    int score = 0;
    
    public TextMeshProUGUI pipeScore;

    [SerializeField] ParticleSystem splashVFX;

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
        if (!isDead) {
            if (other.gameObject.CompareTag("Sky"))
            {
                AudioManager.instance.Play("Death Sound");
                KillPlayer();
            }

            if (other.gameObject.CompareTag("Score"))
            {
                AddScore();
                UpdatePipeColor();
            }

            if (other.gameObject.CompareTag("Pipe"))
            {
                AudioManager.instance.Play("Death Sound");
                KillPlayer();
            }

            if (other.gameObject.CompareTag("Water"))
            {
                AudioManager.instance.Play("Water Splash Sound");
                splashVFX.Play();
                KillPlayer();
            }
        }
    }

    private void KillPlayer()
    {
        GetComponent<PlayerController>().isDead = true;
        isDead = true;
        GetComponent<Animator>().SetBool("Roll", true);
        Invoke("ReloadLevel", 0.8f); // todo remove this line of code - after adding main menu, pause and restart functionality.
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
