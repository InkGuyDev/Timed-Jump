using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    [SerializeField] GameObject FadeIn;

    public void StartTransition() => FadeIn.SetActive(true);

    public void ExitButton() => Application.Quit();
}
