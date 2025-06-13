using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTriggers : MonoBehaviour
{
    
    public void DestroyOnEnd() => Destroy(gameObject);

    public void GoToScene(string scene) => SceneManager.LoadScene(scene);

}
