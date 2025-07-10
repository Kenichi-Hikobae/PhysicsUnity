using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialUI;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;

    private TutorialUI UI;

    private void Start()
    {
        UI = tutorialUI.GetComponent<TutorialUI>();
        tutorialUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        tutorialUI.SetActive(true);
        UI.ShowTutorial(title, description);
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialUI.SetActive(false);
    }

    private void Update()
    {
        transform.GetChild(0).transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 1) + 2, transform.position.z);
    }
}
