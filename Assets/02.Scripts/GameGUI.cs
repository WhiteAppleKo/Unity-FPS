using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    public void GameOver(){
        Application.Quit();
        Debug.Log("게임끝났다");
    }
}
