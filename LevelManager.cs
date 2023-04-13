using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject enemy;

    private void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if(enemy == null)
        {
            SceneManager.LoadScene("EndTimeLine");
        }
    }
}
