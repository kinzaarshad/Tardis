using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipBasketballTutorialScript : MonoBehaviour
{
    public BasketballSceneManager basketballSceneManager;

    void SkipTutorial() => basketballSceneManager.HideTutorial();
}