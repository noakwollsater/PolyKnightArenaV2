using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text Currency;
    [SerializeField] private TMP_Text Level;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text LabelXP;

    [SerializeField] private Button addXPbtn;
    [SerializeField] private Button addCashBtn;

    private Coroutine sliderAnimationCoroutine;

    private void Start()
    {
        addXPbtn.onClick.AddListener(() => LevelAndCash.Instance.AddLevelProgress(10000));
        addCashBtn.onClick.AddListener(() => LevelAndCash.Instance.AddCash(10000));
    }

    void Update()
    {
        Currency.text = LevelAndCash.Instance.Cash.ToString();
        Level.text = LevelAndCash.Instance.Level.ToString();
        slider.maxValue = LevelAndCash.Instance.MaxLevelProgress;

        // Update XP text
        LabelXP.text = $"{Mathf.FloorToInt(LevelAndCash.Instance.LevelProgress)}/{Mathf.FloorToInt(LevelAndCash.Instance.MaxLevelProgress)}";

        // Start the slider animation
        if (sliderAnimationCoroutine == null)
        {
            sliderAnimationCoroutine = StartCoroutine(AnimateSlider(LevelAndCash.Instance.LevelProgress));
        }
    }

    private IEnumerator AnimateSlider(float targetValue)
    {
        // Smoothly animate the slider value
        float startValue = slider.value;
        float duration = 0.5f; // Duration of the animation
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        // Ensure the slider reaches the exact target value
        slider.value = targetValue;
        sliderAnimationCoroutine = null;
    }
}
