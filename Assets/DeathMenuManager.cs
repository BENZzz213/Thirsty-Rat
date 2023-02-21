using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{

    public void RestartGame()
    {
        Debug.Log("COMEON");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
