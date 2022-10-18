using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BabyUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private Image energybarSprite;
    [SerializeField] private Image diaperbarSprite;
    [SerializeField] private Image funbarSprite;
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject energybar;
    [SerializeField] private GameObject diaperbar;
    [SerializeField] private GameObject funbar;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Image dialogImage;
    [SerializeField] private Transform babyLocation;
    [SerializeField] private float height = 3f;
    private bool healthActive, energyActive, diaperActive, funActive;
    private bool? healthAlwaysActive = false;
    private bool? energyAlwaysActive = false;
    private bool? diaperAlwaysActive = false;
    private bool? funAlwaysActive = false;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }
    
    public void SetName(string name)
    {
        nameText.text = name;
    }

    // Update is called once per frame
    private void Update()
    {
        // make the UI face towards the camera
        var trans = transform;
        trans.position = babyLocation.position +  height * Vector3.up;
        trans.rotation = Quaternion.LookRotation(trans.position - _cam.transform.position);
        var eulerAngles = trans.eulerAngles;
        eulerAngles.y = 0;
        trans.eulerAngles = eulerAngles;
    }

    public void SetActive()
    {
        healthbar.SetActive(healthAlwaysActive.GetValueOrDefault(false) || healthActive);
        energybar.SetActive(energyAlwaysActive.GetValueOrDefault(false) || energyActive);
        diaperbar.SetActive(diaperAlwaysActive.GetValueOrDefault(false) || diaperActive);
        funbar.SetActive(funAlwaysActive.GetValueOrDefault(false) || funActive);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateEnergyBar(float maxEnergy, float currentEnergy)
    {
        energybarSprite.fillAmount = currentEnergy / maxEnergy;
    }

    public void UpdateDiaperBar(float maxDiaper, float currentDiaper)
    {
        diaperbarSprite.fillAmount = currentDiaper / maxDiaper;
    }
    
    public void UpdateFunBar(float maxFun, float currentFun)
    {
        funbarSprite.fillAmount = currentFun / maxFun;
    }
    
    public void SetAlwaysActive(bool? health = null, bool? energy = null, bool? diaper = null, bool? fun = null)
    {
        healthAlwaysActive = health.GetValueOrDefault(healthAlwaysActive.GetValueOrDefault(false));
        energyAlwaysActive = energy.GetValueOrDefault(energyAlwaysActive.GetValueOrDefault(false));
        diaperAlwaysActive = diaper.GetValueOrDefault(diaperAlwaysActive.GetValueOrDefault(false));
        funAlwaysActive = fun.GetValueOrDefault(funAlwaysActive.GetValueOrDefault(false));
        SetActive();
    }

    public void EnableStatusBars()
    {
        healthActive = true;
        energyActive = true;
        diaperActive = true;
        funActive = true;
        SetActive();
    }

    public void DisableStatusBars()
    {
        healthActive = false;
        energyActive = false;
        diaperActive = false;
        funActive = false;
        SetActive();
    }

    public void EnableDialogBox(Sprite sprite)
    {
        dialogBox.SetActive(true);
        dialogImage.sprite = sprite;
    }

    public void DisableDialogBox()
    {
        dialogBox.SetActive(false);
        dialogImage.sprite = null;
    }
}