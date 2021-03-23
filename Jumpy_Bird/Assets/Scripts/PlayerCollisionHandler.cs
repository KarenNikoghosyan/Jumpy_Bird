using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerCollisionHandler : MonoBehaviour
{
    bool isChangeable = true, isDead = false, isAlive = true, isSpeed = false, isHighScore = false;
    Material material;
    private int score = 0;
    float textDelay = 1.5f;

    public TextMeshProUGUI pipeScore;

    [SerializeField] private ParticleSystem splashVFX;
    [SerializeField] private ParticleSystem highscoreVFX;
    [SerializeField] private GameObject graphy;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        pipeScore.text = score.ToString();
        ChangeMaterialColor();
        PipesMovement.Stop = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PipesSpawner.secondsBetweenSpawns = 2f;
        PipesMovement.movementFactor = 7f;
    }

    private void ChangeMaterialColor()
    {
        material = FindObjectOfType<PipesSpawner>().material;
        material.color = Color.yellow;
    }

    void Update()
    {
        if (Debug.isDebugBuild)
        {
            FPSCounter();
            DebugKeys();
        }
    }

    private void FPSCounter()
    {
        if (graphy == null) return;
        
        graphy.SetActive(true);
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
        if (other.gameObject.CompareTag("Water"))
        {
            AudioManager.instance.Play("Water Splash Sound");
            splashVFX.Play();
            KillPlayer();
        }

        if (!isAlive) return;
        
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

        if (other.gameObject.CompareTag("PipeBottom_Collider"))
        {
            AudioManager.instance.Play("Death Sound");
            KillPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            StopPipesMovement();
        }
    }

    private void KillPlayer()
    {
        GetComponent<PlayerController>().isDead = true;
        GetComponent<Animator>().SetBool("Roll", true);
        isAlive = false;
        PipesMovement.Stop = true;
        Invoke("OpenGameOverMenu", 1.5f);
    }

    private void StopPipesMovement()
    {
        AudioManager.instance.Play("Death Sound");
        GetComponent<PlayerController>().isDead = true;
        GetComponent<Animator>().SetBool("Roll", true);
        FindObjectOfType<PipesMovement>().PipeMovement(stop: true);
    }

    private void OpenGameOverMenu()
    {
        isDead = true;
        FindObjectOfType<GameButtonsGUI>().GameOverMenu(isDead);
    }

    private void AddScore()
    {
        AudioManager.instance.Play("Scroing Sound");
        score++;
        pipeScore.text = score.ToString();
        HighScoreParticles();
    }

    public void HighScoreParticles()
    {
        if (score == PlayerPrefs.GetInt("Highscore") + 1)
        {
            AudioManager.instance.Play("HighScore");
            highscoreVFX.Play();
            FindObjectOfType<GameButtonsGUI>().ShowHighScoreText(isHighScore);
        }
    }

    private void UpdatePipeColor()
    {
        if (score % 10 == 0)
        {
            FindObjectOfType<PipesSpawner>().SetRandomColor(isChangeable);
            FindObjectOfType<GameButtonsGUI>().ShowSpeedText(isSpeed);
            SpeedUpPipes();
        }
    }

    //Pipes speed based on the current score.
    private void SpeedUpPipes()
    {
        switch (score)
        {
            case 10:
                PipesSpawner.secondsBetweenSpawns = 1.9f;
                PipesMovement.movementFactor = 7.5f;
                break;
            case 20:
                PipesSpawner.secondsBetweenSpawns = 1.7f;
                PipesMovement.movementFactor = 8.5f;
                break;
            case 30:
                PipesSpawner.secondsBetweenSpawns = 1.5f;
                PipesMovement.movementFactor = 9.5f;
                break;
            case 40:
                PipesSpawner.secondsBetweenSpawns = 1.45f;
                PipesMovement.movementFactor = 10.5f;
                break;
            case 50:
                PipesSpawner.secondsBetweenSpawns = 1.4f;
                PipesMovement.movementFactor = 11.5f;
                break;
        }
    }

    //returns score value
    public int GetScore()
    {
        return score;
    }
}