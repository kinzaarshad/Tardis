using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PredictionManager : MonoBehaviour
{
    public static PredictionManager Instance;
    public GameObject[] obstacles;
    public int maxIterations;

    Scene currentScene;
    Scene predictionScene;

    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;

    List<GameObject> dummyObstacles = new List<GameObject>();

    LineRenderer lineRenderer;
    GameObject dummy;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        Physics.autoSimulation = false;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void copyAllObstacles()
    {
        foreach (var obstacle in obstacles)
        {
            if (obstacle.GetComponent<Collider>() != null)
            {
                GameObject go = Instantiate(obstacle);


                go.transform.position = obstacle.transform.position;
                go.transform.rotation = obstacle.transform.rotation;
                Renderer rend = go.GetComponent<Renderer>();
                if (rend)
                {
                    rend.enabled = false;
                }

                SceneManager.MoveGameObjectToScene(go, predictionScene);
                dummyObstacles.Add(go);
            }

            foreach (Transform t in obstacle.transform)
            {
                if (t.gameObject.GetComponent<Collider>() != null)
                {
                    GameObject fakeT = Instantiate(t.gameObject);
                    fakeT.transform.position = t.position;
                    fakeT.transform.rotation = t.rotation;
                    Renderer fakeR = fakeT.GetComponent<Renderer>();
                    if (fakeR)
                    {
                        fakeR.enabled = false;
                    }

                    SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                    dummyObstacles.Add(fakeT);
                }
            }
        }
    }

    public void killAllObstacles()
    {
        foreach (var o in dummyObstacles)
        {
            Destroy(o);
        }

        dummyObstacles.Clear();
    }

    public void predict(GameObject subject, Vector3 currentPosition, Vector3 force)
    {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if (dummy == null)
            {
                dummy = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }

            dummy.transform.position = currentPosition;
            dummy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = maxIterations;


            for (int i = 0; i < maxIterations; i++)
            {
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, dummy.transform.position);
            }

            Destroy(dummy);
        }
    }

    void OnDestroy()
    {
        killAllObstacles();
    }
}