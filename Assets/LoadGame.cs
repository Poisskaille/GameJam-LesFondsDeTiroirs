using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame : MonoBehaviour
{
    public void LoadGameScene() 
    {
        SceneManager.LoadScene("MainGame");
    }

}
