using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("Healthbar")] public Healthbar healthbar;
    [FormerlySerializedAs("DiamondsScoreTextMaster")] public UIScoreTextMaster diamondsScoreTextMaster;
    [FormerlySerializedAs("SoulScoreTextMaster")] public UIScoreTextMaster soulScoreTextMaster;

    public void SetMaxHealth(float maxHealth)
    {
        healthbar.SetMaxHealth(maxHealth);        
    }

    public void SetHealth(float currentHealth)
    {
        healthbar.SetHealth(currentHealth);
    }

    public void SetDiamondsPoints(float diamondsPoints)
    {
        diamondsScoreTextMaster.SetPoints(diamondsPoints);
    }

    public void SetSoulsPoints(float soulsPoints)
    {
        soulScoreTextMaster.SetPoints(soulsPoints);
    }
}
