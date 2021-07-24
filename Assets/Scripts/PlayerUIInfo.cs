using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIInfo : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text healthText;
    public RectTransform healthFill;

    public TMP_Text cashText; // optional, check nullity!
    public Button changeOrigin; // optional, check nullity!
}
