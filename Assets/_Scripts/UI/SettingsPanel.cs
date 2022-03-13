using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanel : Scenegleton<SettingsPanel>
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private Slider dotCountSlider;
    [SerializeField] private TextMeshProUGUI dotCountText;


    private void OnEnable()
    {
        EventSystem.OnExperimentStarted += OnExperimentStarted;
    }

    private void OnDisable()
    {
        EventSystem.OnExperimentStarted -= OnExperimentStarted;
    }

    public void OnDotCountSlided()
    {
        if (DotSpawner.Instance.isSpawning) return;

        dotCountText.text = dotCountSlider.value.ToString();
        DotSpawner.Instance.dotCount = (int)dotCountSlider.value;
    }

    public void OnExperimentStarted()
    {
        startButton.SetActive(false);
    }
}
