using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerCollisionHandler : MonoBehaviour
{
    bool isChangeable = true, isAlive = true;
    Material material;
    int score = 0;
    
    public TextMeshProUGUI pipeScore;

    [SerializeField] ParticleSystem splashVFX;
    [SerializeField] GameObject graphy;

    void Start()
    {
        pipeScore.text = score.ToString();
        ChangeMaterialColor();
        PipesMovement._stop = false;
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
        if (graphy == null) { return; }
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
        if (!isAlive) { return; }
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

            if (other.gameObject.CompareTag("Water"))
            {
                AudioManager.instance.Play("Water Splash Sound");
                splashVFX.Play();
                KillPlayer();
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
        Invoke("ReloadLevel", 0.8f); // todo remove this line of code - after adding main menu, pause and restart functionality.
    }
    private void StopPipesMovement()
    {
        AudioManager.instance.Play("Death Sound");
        GetComponent<PlayerController>().isDead = true;
        GetComponent<Animator>().SetBool("Roll", true);
        FindObjectOfType<PipesMovement>().PipeMovement(stop: true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
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
