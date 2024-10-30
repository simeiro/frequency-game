using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class ScreenManage : MonoBehaviour
{
    private int screenNumber;

    [SerializeField] private List<Image> explanationImages;

    [SerializeField] private List<TextMeshProUGUI> explanationTexts;

    [SerializeField] private Button playButton;

    [SerializeField] private Button nextButton;

    [SerializeField] private Button backButton;

    void Start()
    {
        screenNumber = 0;
        changeScreen();
    }

    public void NextScreen()
    {
        screenNumber++;
        changeScreen();

    }

    public void BackScreen()
    {
        screenNumber--;
        changeScreen();
    }

    private void changeScreen()
    {
        foreach(Image image in explanationImages){image.gameObject.SetActive(false);}
        foreach(TextMeshProUGUI text in explanationTexts){text.gameObject.SetActive(false);}

        // int index = screenNumber % explanationImages.Count;
        explanationImages[screenNumber].gameObject.SetActive(true);
        explanationTexts[screenNumber].gameObject.SetActive(true);

        if(screenNumber == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }

        if(screenNumber == explanationImages.Count - 1)
        {
            playButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
    }
}
