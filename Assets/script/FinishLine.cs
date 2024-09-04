using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private string levelName = "SampleScene"; // Replace with your actual scene name
    private string highScoreKey = "HighScore";
    private float bestTime = float.MaxValue;
    private Label currentTimeLabel;
    private Label bestTimeLabel;

    private void Start()
    {
        // Load the best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat(highScoreKey, float.MaxValue);

        // Retrieve the UI Document and root VisualElement
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;

        // Retrieve the labels by name or class
        currentTimeLabel = rootVisualElement.Q<Label>("Score");
        bestTimeLabel = rootVisualElement.Q<Label>("Highscore");

        // Update the UI display for best time
        UpdateBestTimeDisplay(bestTime);
    }

    private void Update()
    {
        // Update the UI display for current time
        float currentTime = Time.timeSinceLevelLoad;
        currentTimeLabel.text = $"Current Time: {currentTime:F2}s";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float currentTime = Time.timeSinceLevelLoad; // Current level completion time

            // Check if the current time is better than the stored high score
            if (currentTime < bestTime)
            {
                // Update the best time and save it
                bestTime = currentTime;
                PlayerPrefs.SetFloat(highScoreKey, bestTime);
                PlayerPrefs.Save(); // Ensure that the new high score is saved
                UpdateBestTimeDisplay(bestTime); // Update the UI display for best time
            }

            // Restart the level
            SceneManager.LoadScene(levelName);
        }
    }

    private void UpdateBestTimeDisplay(float time)
    {
        // Update the best time label with the formatted time string
        bestTimeLabel.text = time < float.MaxValue ? $"Best Time: {time:F2}s" : "Best Time: --";
    }
}
