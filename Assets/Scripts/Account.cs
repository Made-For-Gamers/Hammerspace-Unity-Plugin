using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Account : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI account;

    void Start()
    {
        account = GetComponent<TextMeshProUGUI>();
        account.text = PlayerPrefs.GetString("Account");
    }
 
}
