using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCannonTutorialScript : MonoBehaviour
{
    public CannonSceneManager cannonSceneManager;

    void SkipTutorial() => cannonSceneManager.HideTutorial();
}