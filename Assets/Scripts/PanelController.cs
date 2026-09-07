using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject solvePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        ShowHUDPanel();
    }

    private void ShowHUDPanel()
    {
        hudPanel.SetActive(true);
        solvePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

    }

    public void ShowSolvePanel()
    {
        solvePanel.SetActive(true);
        hudPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        solvePanel.SetActive(false);
        hudPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        hudPanel.SetActive(false);
        solvePanel.SetActive(false);
        winPanel.SetActive(false);
    }
}
