//using UnityEngine;
//using UnityEngine.UI;

//public class CaloriesTracker : MonoBehaviour
//{
//    private float weight = 80f;   // Fixed weight
//    private float height = 170f;  // Fixed height
//    private string playerName = "Paveena";  // Fixed name
//    private float BMR;
//    private float totalCaloriesBurned = 0f;

//    private float crouchingMET = 7.0f;
//    private float jumpingMET = 10.0f;
//    private float laneSwitchMET = 5.0f;

//    public Text caloriesText;

//    void Start()
//    {
//        Debug.Log("Loaded Player Info: " + playerName + ", Weight: " + weight + ", Height: " + height);
//        CalculateBMR();
//    }

//    void CalculateBMR()
//    {
//        BMR = 500 + (10 * weight) + (6.25f * height);
//    }

//    public void AddJumpCalories()
//    {
//        float jumpCalories = ((jumpingMET * BMR) / 24f) * (1.5f / 3600f);
//        totalCaloriesBurned += jumpCalories;
//        Debug.Log("Jump Calories Burned: " + jumpCalories + " | Total Calories: " + totalCaloriesBurned);
//        UpdateCaloriesUI();
//    }

//    public void AddCrouchCalories()
//    {
//        float crouchCalories = ((crouchingMET * BMR) / 24f) * (2f / 3600f);
//        totalCaloriesBurned += crouchCalories;
//        Debug.Log("Crouch Calories Burned: " + crouchCalories + " | Total Calories: " + totalCaloriesBurned);
//        UpdateCaloriesUI();
//    }

//    public void AddLaneSwitchCalories()
//    {
//        float laneSwitchCalories = ((laneSwitchMET * BMR) / 24f) * (1f / 3600f);
//        totalCaloriesBurned += laneSwitchCalories;
//        Debug.Log("Lane Switch Calories Burned: " + laneSwitchCalories + " | Total Calories: " + totalCaloriesBurned);
//        UpdateCaloriesUI();
//    }

//    void UpdateCaloriesUI()
//    {
//        if (caloriesText != null)
//        {
//            caloriesText.text = "CALORIES: " + Mathf.RoundToInt(totalCaloriesBurned);
//        }
//    }

//    public float GetCaloriesBurned()
//    {
//        return totalCaloriesBurned;
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class CaloriesTracker : MonoBehaviour
{
    private float weight;
    private float height;
    private string playerName;
    private float BMR;
    private float totalCaloriesBurned = 0f;

    private float crouchingMET = 7.0f;
    private float jumpingMET = 10.0f;
    private float laneSwitchMET = 5.0f;

    public Text caloriesText;

    void Start()
    {
        // Load player data from PlayerPrefs
        playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayer");
        weight = PlayerPrefs.GetFloat("PlayerWeight", 80f);
        height = PlayerPrefs.GetFloat("PlayerHeight", 170f);

        Debug.Log("Loaded Player Info: " + playerName + ", Weight: " + weight + ", Height: " + height);

        CalculateBMR();
    }

    void CalculateBMR()
    {
        BMR = 500 + (10 * weight) + (6.25f * height);
    }

    public void AddJumpCalories()
    {
        float jumpCalories = ((jumpingMET * BMR) / 24f) * (1.5f / 3600f);
        totalCaloriesBurned += jumpCalories;
        Debug.Log("Jump Calories Burned: " + jumpCalories + " | Total Calories: " + totalCaloriesBurned);
        UpdateCaloriesUI();
    }

    public void AddCrouchCalories()
    {
        float crouchCalories = ((crouchingMET * BMR) / 24f) * (2f / 3600f);
        totalCaloriesBurned += crouchCalories;
        Debug.Log("Crouch Calories Burned: " + crouchCalories + " | Total Calories: " + totalCaloriesBurned);
        UpdateCaloriesUI();
    }

    public void AddLaneSwitchCalories()
    {
        float laneSwitchCalories = ((laneSwitchMET * BMR) / 24f) * (1f / 3600f);
        totalCaloriesBurned += laneSwitchCalories;
        Debug.Log("Lane Switch Calories Burned: " + laneSwitchCalories + " | Total Calories: " + totalCaloriesBurned);
        UpdateCaloriesUI();
    }

    void UpdateCaloriesUI()
    {
        if (caloriesText != null)
        {
            caloriesText.text = "CALORIES: " + Mathf.RoundToInt(totalCaloriesBurned);
        }
    }

    public float GetCaloriesBurned()
    {
        return totalCaloriesBurned;
    }
}