using UnityEngine;

public class SelectionScreenUIManager : MonoBehaviour
{

    [Header("Cinemachine Brain")]
    [SerializeField] private GameObject m_PlayerStateDrivenCamera;
    [SerializeField] private Animator m_StateAnimator;


    [Header("Player Selection UI")]
    [SerializeField] private GameObject m_3DCanvas;
    [SerializeField] private GameObject m_PlayerSelection2DCanvas;
    [SerializeField] private GameObject m_SelectNextPlayerButton;
    [SerializeField] private GameObject m_SelectBackPlayerButton;

    [Header("Playes Statics UI")]
    [SerializeField] private GameObject m_PlayerWhiteScreen;
    [SerializeField] private GameObject m_PlayerBlackScreen;

    private void Start()
    {
        StartPlayerSelectionUI();
    }

    public void StartPlayerSelectionUI()
    {
        m_PlayerStateDrivenCamera.SetActive(true);

        m_3DCanvas.SetActive(true);

        m_PlayerSelection2DCanvas.SetActive(true);

        GoToWhitePlayer();
    }


    public void GoToWhitePlayer()
    {
        m_StateAnimator.Play("Player White Cam State");

        m_SelectBackPlayerButton.SetActive(true);
        m_SelectNextPlayerButton.SetActive(false);

        m_PlayerWhiteScreen.SetActive(true);
        m_PlayerBlackScreen.SetActive(false);
    }

    public void GoToBlackPlayer()
    {
        m_StateAnimator.Play("Player Black Cam State");

        m_SelectNextPlayerButton.SetActive(true);
        m_SelectBackPlayerButton.SetActive(false);

        m_PlayerWhiteScreen.SetActive(false);
        m_PlayerBlackScreen.SetActive(true);

    }


}
