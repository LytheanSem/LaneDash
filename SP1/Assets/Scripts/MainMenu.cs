//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MainMenu : MonoBehaviour
//{
//    void Start()
//    {

//    }

//    void Update()
//    {

//    }

//    public void StartGame() {
//      SceneManager.LoadScene(1);
//    }
//}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro

public class MainMenu : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField weightInput;
    public TMP_InputField heightInput;
    public Button startButton;

    void Start()
    {
        // Check if UI elements are assigned in Inspector
        if (nameInput == null || weightInput == null || heightInput == null || startButton == null)
        {
            Debug.LogError("❌ One or more UI elements are not assigned in the Inspector!");
            return;
        }

        // Load saved values if they exist
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameInput.text = PlayerPrefs.GetString("PlayerName");
        }

        if (PlayerPrefs.HasKey("PlayerWeight"))
        {
            weightInput.text = PlayerPrefs.GetFloat("PlayerWeight").ToString();
        }

        if (PlayerPrefs.HasKey("PlayerHeight"))
        {
            heightInput.text = PlayerPrefs.GetFloat("PlayerHeight").ToString();
        }

        // Disable the start button if fields are empty
        startButton.interactable = false;

        // Add listeners to check for input changes
        nameInput.onValueChanged.AddListener(delegate { ValidateInput(); });
        weightInput.onValueChanged.AddListener(delegate { ValidateInput(); });
        heightInput.onValueChanged.AddListener(delegate { ValidateInput(); });
    }

    void ValidateInput()
    {
        bool isNameValid = !string.IsNullOrWhiteSpace(nameInput.text);
        bool isWeightValid = float.TryParse(weightInput.text, out float weight) && weight > 0;
        bool isHeightValid = float.TryParse(heightInput.text, out float height) && height > 0;

        startButton.interactable = isNameValid && isWeightValid && isHeightValid;
    }

    public void StartGame()
    {
        if (nameInput == null || weightInput == null || heightInput == null)
        {
            Debug.LogError("❌ Cannot start game: Missing input fields.");
            return;
        }

        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.SetFloat("PlayerWeight", float.Parse(weightInput.text));
        PlayerPrefs.SetFloat("PlayerHeight", float.Parse(heightInput.text));
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }
}