using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using MText;

public class PlayerCollisionHandler : MonoBehaviour
{
    private bool isChangeable = true, isDead = false, isAlive = true, isSpeed = false;
    Material material;
    private int score = 0;
    float textDelay = 1.5f;

    private PipesSpawner _pipesSpawner;
    private GameButtonsGUI _gameButtonsGUI;

    [Header("Score")]
    [SerializeField] private Modular3DText scoreUI;
    
    [Header("VFX")]
    [SerializeField] private ParticleSystem splashVFX;

    [Header("Confetti VFX")] 
    [SerializeField] private Modular3DText highscore;
    [SerializeField] private ParticleSystem[] confettiParticles = new ParticleSystem[5];
    
    [Header("Graphy")]
    [SerializeField] private GameObject graphy;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        scoreUI.UpdateText(score);
        ChangeMaterialColor();
        
        _pipesSpawner = FindObjectOfType<PipesSpawner>();
        _gameButtonsGUI = FindObjectOfType<GameButtonsGUI>();
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

    private void Update()
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
            scoreUI.UpdateText(score);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerPrefs.SetInt(Constants.HIGHSCORE, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            AddScore();
            UpdatePipeColor();
        }

        if (other.gameObject.CompareTag("Water"))
        {
            AudioManager.instance.Play(Constants.WATER_SPLASH_SOUND);
            
            splashVFX.Play();
            KillPlayer();
            Invoke("OpenGameOverMenu", 1f);
        }

        if (!isAlive) return;

        if (other.gameObject.CompareTag("Sky"))
        {
            AudioManager.instance.Play(Constants.DEATH_SOUND);
            KillPlayer();
            Invoke("OpenGameOverMenu", 0.3f);
        }

        if (other.gameObject.CompareTag("PipeTop_Collider"))
        {
            AudioManager.instance.Play(Constants.DEATH_SOUND);
            KillPlayer();
            Invoke("OpenGameOverMenu", 1f);
        }

        if (PipesMovement.Stop) return;

        if (other.gameObject.CompareTag("PipeBottom_Collider"))
        {
            AudioManager.instance.Play(Constants.DEATH_SOUND);
            KillPlayer();
            Invoke("OpenGameOverMenu", 0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PipesMovement.Stop) return;
       
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
    }

    private void StopPipesMovement()
    {
        AudioManager.instance.Play(Constants.DEATH_SOUND);
        GetComponent<PlayerController>().isDead = true;
        GetComponent<Animator>().SetBool("Roll", true);
        FindObjectOfType<PipesMovement>().PipeMovement(stop: true);
    }

    private void OpenGameOverMenu()
    {
        isDead = true;
        _gameButtonsGUI.GameOverMenu(isDead);
    }

    private void AddScore()
    {
        AudioManager.instance.Play(Constants.SCORING_SOUND);
        score++;
        scoreUI.UpdateText(score);
        ShowHighScoreParticles();
    }

    public void ShowHighScoreParticles()
    {
        if (score == PlayerPrefs.GetInt(Constants.HIGHSCORE) + 1 && PlayerPrefs.GetInt(Constants.HIGHSCORE) != 0)
        {
            AudioManager.instance.Play(Constants.HIGHSCORE_SOUND);
            StartCoroutine(PlayConfettiVFX());
        }
    }

    IEnumerator PlayConfettiVFX()
    {
        highscore.gameObject.SetActive(true);
        highscore.GetComponent<Animator>().SetBool("open", true);
        
        for (int i = 0; i < confettiParticles.Length; i++)
        {
            confettiParticles[i].Play();
        }

        yield return new WaitForSecondsRealtime(2.7f);
        
        highscore.GetComponent<Animator>().SetBool("open", false);

        yield return new WaitForSecondsRealtime(0.5f);
        
        highscore.gameObject.SetActive(false);

        for (int i = 0; i < confettiParticles.Length; i++)
        {
            confettiParticles[i].Stop();
        }
    }

    private void UpdatePipeColor()
    {
        if (score % 10 == 0)
        {
            _pipesSpawner.SetRandomColor(isChangeable);
            SpeedUpPipes();

            if (score <= 50)
            {
                _gameButtonsGUI.ShowSpeedText(isSpeed);
            }
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