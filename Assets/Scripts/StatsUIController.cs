using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI critText;
    [SerializeField] TextMeshProUGUI dodgeText;

    public void UpdateUI(Entity entity)
    {
        hpText.text = entity.entityFightingStats.HP.ToString();
        dmgText.text = entity.entityFightingStats.Attack.ToString();
        shieldText.text = entity.entityFightingStats.Block.ToString();
        critText.text = entity.entityFightingStats.Crit.ToString();
        dodgeText.text = entity.entityFightingStats.Dodge.ToString();

    }
}
