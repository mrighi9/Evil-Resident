using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [SerializeField] GameObject fadeOutPrefab; // Optional: Assign a fade-out prefab for transitions

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(int intermediateSceneIndex, int targetSceneIndex, float intermediateDuration)
    {
        StartCoroutine(HandleSceneTransition(intermediateSceneIndex, targetSceneIndex, intermediateDuration));
    }

    private IEnumerator HandleSceneTransition(int intermediateSceneIndex, int targetSceneIndex, float intermediateDuration)
    {
        if (fadeOutPrefab != null)
        {
            Instantiate(fadeOutPrefab);
        }

        // Load the intermediate scene (door animation)
        SceneManager.LoadScene(intermediateSceneIndex, LoadSceneMode.Single);

        // Wait for the specified duration (e.g., door animation duration)
        yield return new WaitForSeconds(intermediateDuration);

        // Load the target scene
        SceneManager.LoadScene(targetSceneIndex, LoadSceneMode.Single);
    }
}
