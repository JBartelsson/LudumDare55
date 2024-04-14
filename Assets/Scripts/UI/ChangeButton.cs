using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeButton : MonoBehaviour
{
    Color commonColor = new Color();
    Color uncommonColor = new Color();
    Color rareColor = new Color();
    // Start is called before the first frame update
    [SerializeField] private Image frame;
    [SerializeField] private Image bottomRight;
    [SerializeField] private Image bottomLeft;
    [SerializeField] private Image topRight;
    [SerializeField] private Image topLeft;
    [SerializeField] private Image smallFrame;

    [SerializeField] private Button backgroundButton;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text type;
    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text block;
    [SerializeField] private TMP_Text crit;
    [SerializeField] private TMP_Text dodge;


    [SerializeField] 

    void changeButtonContent(BodyPartSO item)
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
