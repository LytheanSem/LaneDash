using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    public static float caloriesBurned = 0; // Added for calories tracking

    [SerializeField] GameObject coinDisplay;
    [SerializeField] GameObject coinEndDisplay;
    [SerializeField] GameObject caloriesDisplay; // Added for calories UI display
    [SerializeField] GameObject caloriesEndDisplay; // Added for calories UI display
    private CaloriesTracker caloriesTracker; // Reference to CaloriesTracker

    void Start()
    {
        // Find the CaloriesTracker script in the scene
        caloriesTracker = FindObjectOfType<CaloriesTracker>();
    }

    void Update()
    {
        // Update coin UI
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + coinCount;
        coinEndDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + coinCount;

        // Update calories UI (fetch latest data from CaloriesTracker)
        if (caloriesTracker != null)
        {
            caloriesBurned = caloriesTracker.GetCaloriesBurned();
            caloriesDisplay.GetComponent<TMPro.TMP_Text>().text = "CALORIES: " + Mathf.RoundToInt(caloriesBurned);
            caloriesEndDisplay.GetComponent<TMPro.TMP_Text>().text = "CALORIES: " + Mathf.RoundToInt(caloriesBurned);
        }
    }
}
