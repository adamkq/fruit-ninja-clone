using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public void TransitionToMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
