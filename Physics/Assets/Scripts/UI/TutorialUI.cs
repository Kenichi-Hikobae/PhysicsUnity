using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private Text titleTutorial;
    [SerializeField]
    private Text descriptionTutorial;

    public void ShowTutorial(string title, string description)
    {
        titleTutorial.text = title;
        descriptionTutorial.text = description;
    }
}
