﻿using UnityEngine;
using UnityEngine.UI;

public class LoadGameController : MonoBehaviour {

    public float time;
    public FadingScript fadingController;
    public GameAnaliticsController gameAnaliticsController;

    Image circularLoader;

    void Start ()
    {
        circularLoader = GetComponent<Image>();
        circularLoader.fillAmount = 1f;
        gameAnaliticsController.sendPlayerPlatformData();
        setUpGameLanguage();
    }

    void setUpGameLanguage()
    {
        SmartLocalization.LanguageManager languageManager = SmartLocalization.LanguageManager.Instance;
        SmartLocalization.SmartCultureInfo deviceCulture = languageManager.GetDeviceCultureIfSupported();
        if (deviceCulture != null)
        {
            languageManager.ChangeLanguage(deviceCulture);
        }
        SmartLocalization.LanguageManager.SetDontDestroyOnLoad();
    }

    void Update ()
    {
        if (circularLoader.fillAmount > 0f)
        {
            circularLoader.fillAmount -= Time.deltaTime / time;
        }
        else
        {
            fadingController.startFade("MainMenuScreen", false);
            ServicesLocator.loadGameServices();
            enabled = false;
        }
	}
}
