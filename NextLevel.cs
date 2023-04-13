using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void Update()
    {
        if(GetComponent<PlayableDirector>().state != PlayState.Playing || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
