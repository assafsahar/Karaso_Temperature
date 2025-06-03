using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SunEnergyStage2 : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] Slider bar1;
    [SerializeField] Slider bar2;
    [SerializeField] Slider bar3;
    [SerializeField] float barFillDuration = 1.2f;
    [SerializeField] TextMeshProUGUI percentageText1, percentageText2, percentageText3;
    [SerializeField] TextMeshProUGUI chemicalType1, chemicalType2;

    [Header("UI Elements")]
    [SerializeField] GameObject infoText;
    [SerializeField] GameObject animationPanel;
    [SerializeField] BigAtomsAnimation bigAtomsAnimation;

    [Header("Button")]
    [SerializeField] Button checkBtn;
    [SerializeField] Button resetBtn;

    [Header("Stage 3 Elements")]
    [SerializeField] GameObject tempCheckText; 
    [SerializeField] Button questionMark1;      // first question mark
    [SerializeField] Button questionMark2;      // second question mark
    [SerializeField] GameObject redBoxText;     // red info box
    [SerializeField] CanvasGroup redBoxCanvasGroup;

    private CanvasGroup infoTextCanvasGroup;
    private BreathingText[] InfoTextsBoxes;

    private void Awake()
    {
        bar1 = bar1.GetComponent<Slider>();
        bar2 = bar2.GetComponent<Slider>();
        bar3 = bar3.GetComponent<Slider>();
        infoTextCanvasGroup = infoText.GetComponent<CanvasGroup>();
        if (infoTextCanvasGroup == null)
            infoTextCanvasGroup = infoText.AddComponent <CanvasGroup>();
    }
    private void Start()
    {
        redBoxCanvasGroup.alpha = 0f;

        bar1.value = 0.5f;
        bar2.value = 0.5f;
        bar3.value = 0.5f;

        infoText.SetActive(false);
        infoTextCanvasGroup.alpha = 0f;
        animationPanel.transform.localScale = Vector3.one * 0.1f;

        checkBtn.onClick.AddListener(OnCheckClicked);
        resetBtn.onClick.AddListener(ResetStage);

        bar1.onValueChanged.AddListener((v) => percentageText1.text = Mathf.RoundToInt(v) + "%");
        bar2.onValueChanged.AddListener((v) => percentageText2.text = Mathf.RoundToInt(v) + "%");
        bar3.onValueChanged.AddListener((v) => percentageText3.text = Mathf.RoundToInt(v) + "%");

        questionMark1.onClick.AddListener(OnQuestionMark1Clicked);
        questionMark2.onClick.AddListener(OnQuestionMark2Clicked);

         InfoTextsBoxes = infoText.GetComponentsInChildren<BreathingText>();
    }

    public void ResetStage()
    {
        DOTween.Kill(bar1);
        DOTween.Kill(bar2);
        DOTween.Kill(bar3);
        DOTween.Kill("ShowInfoText");
        DOTween.Kill("PanelShow");
        DOTween.Kill("PanelScale");

        // Reset sliders
        bar1.value = 0.5f;
        bar2.value = 0.5f;
        bar3.value = 0.5f;

        // Reset percentage texts
        percentageText1.text = Mathf.RoundToInt(bar1.value) + "%";
        percentageText2.text = Mathf.RoundToInt(bar2.value) + "%";
        percentageText3.text = Mathf.RoundToInt(bar3.value) + "%";

        chemicalType1.text = "";
        chemicalType2.text = "";

        // Reset UI
        infoText.SetActive(false);
        if (infoTextCanvasGroup != null)
            infoTextCanvasGroup.alpha = 0f;
        animationPanel.transform.localScale = Vector3.one * 0.1f;
        animationPanel.SetActive(false);

        checkBtn.interactable = true;
        checkBtn.gameObject.SetActive(true);

        tempCheckText.SetActive(false);
        questionMark1.gameObject.SetActive(false);
        questionMark2.gameObject.SetActive(false);
        redBoxCanvasGroup.alpha = 0f;

        // Reset breathing texts
        foreach (BreathingText infoTxt in InfoTextsBoxes)
        {
            infoTxt.Stopanimation();
            infoTxt.IsActive = false;
        }

        // Reset big atoms animation
        if (bigAtomsAnimation != null)
        {
            bigAtomsAnimation.StopAllSequences();
        }

        foreach (var atom in animationPanel.GetComponentsInChildren<AtomAnimation>())
        {
            atom.StopFloating();
        }
    }


    private void OnCheckClicked()
    {
        

        checkBtn.interactable = false; 
        checkBtn.gameObject.SetActive(false);

        bar1.DOValue(74f, barFillDuration).SetEase(Ease.OutBounce);
        bar2.DOValue(24f, barFillDuration).SetEase(Ease.OutBounce).SetDelay(0.2f);
        bar3.DOValue(1f, barFillDuration).SetEase(Ease.OutBounce).SetDelay(0.2f);

        DOVirtual.DelayedCall(0.5f, () => {
            infoText.SetActive(true);
            infoTextCanvasGroup.alpha = 0f;
            infoTextCanvasGroup.DOFade(1f, 0.7f);

            DOVirtual.DelayedCall(0f, () => {
                animationPanel.SetActive(true);
                animationPanel.transform.DOScale(1f, 1f).SetEase(Ease.InOutCubic)
                .SetId("PanelScale")
                    .OnComplete(() => {
                        ShowStage3();
                        StartAtomsAnimation();
                    });
            }).SetId("PanelShow");
        }).SetId("ShowInfoText");
    }

    private void ShowStage3()
    {
        chemicalType1.text = "H";
        chemicalType2.text = "He";
        DOVirtual.DelayedCall(1f, () => {
          tempCheckText.SetActive(true);
          tempCheckText.transform.localPosition = new Vector3(-715f, -59f, tempCheckText.transform.localPosition.z);
          /*tempCheckText.transform.localScale = Vector3.one * 0.7f;
          tempCheckText.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);*/

          questionMark1.gameObject.SetActive(true);

          
      });
    }

    private void OnQuestionMark1Clicked()
    {
        //redBoxText.SetActive(true);
       
        redBoxCanvasGroup.DOFade(1f, 0.7f);
        questionMark2.gameObject.SetActive(true);
        questionMark1.gameObject.SetActive(false);
        foreach (BreathingText infoTxt in InfoTextsBoxes)
            infoTxt.StartFirstAnimation();
    }
    private void OnQuestionMark2Clicked()
    {
        questionMark2.gameObject.SetActive(false);
        redBoxCanvasGroup.alpha = 0f;

        // Play the atom animation
        if (bigAtomsAnimation != null)
        {
            bigAtomsAnimation.ShowInitialAtoms();
            bigAtomsAnimation.Invoke("PlaySequence", 6);
        }
        foreach (BreathingText infoTxt in InfoTextsBoxes)
            infoTxt.StartSecondAnimation();
    }


    private void StartAtomsAnimation()
    {
        // Find all AtomAnimation scripts under animationPanel and start their movement
        foreach (var atom in animationPanel.GetComponentsInChildren<AtomAnimation>())
        {
            atom.StartFloating();
        }
    }

}
