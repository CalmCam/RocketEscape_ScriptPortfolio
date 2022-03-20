using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public float loadDelay = 1f;
    public AudioClip explosion;
    public AudioClip clapping;
    public ParticleSystem explosionParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    ScoreSystem getScore;
    LivesManager LivesManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                FinishSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void CrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        explosionParticle.Play();
        isTransitioning = true;

        LivesManager = GameObject.FindGameObjectWithTag("Lives Handler").GetComponent<LivesManager>();
        LivesManager.UseLife();

        int currentLifeCount = PlayerPrefs.GetInt(LivesManager.LifeCountKey, LivesManager.maxLifeCount);
        if (currentLifeCount > 0)
        {
            Invoke("ReloadLevel", loadDelay);
        }
        else
        {
            Invoke("QuitToMenu", loadDelay);
        }
    }

    void FinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(clapping);
        isTransitioning = true;

        getScore = GameObject.FindGameObjectWithTag("Score Handler").GetComponent<ScoreSystem>();
        getScore.SaveHighScore();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
