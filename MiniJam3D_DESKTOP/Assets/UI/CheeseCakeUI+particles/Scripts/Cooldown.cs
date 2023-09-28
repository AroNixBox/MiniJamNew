using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class Cooldown : MonoBehaviour
{
    public Image HealthBar;
    public bool coolingDown;
    private int damage = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            HealthBar.fillAmount = HealthBar.fillAmount - (damage * 0.01f);

        }
    }
}