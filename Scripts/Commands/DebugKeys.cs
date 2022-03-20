using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        // Turns off Box Collider if assigned
        if (Input.GetKey(KeyCode.X))
        {
            DisableCollision();
        }
    }

    void LoadNextLevel()
    {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            int totalSceneIndex = SceneManager.sceneCountInBuildSettings;

            if (totalSceneIndex == nextSceneIndex)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
    }

    void DisableCollision()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
