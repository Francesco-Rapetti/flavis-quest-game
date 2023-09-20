using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Keys : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject noKeysText;
    bool onetime1 = true;
    bool onetime2 = true;
    bool onetime3 = true;
    bool onetime4 = true;
    bool onetime5 = true;
    bool onetime6 = true;

    

    private void Update()
    {
        if (PlayerPrefs.GetInt("KeysFinished") == 1)
        {
            text.text = "My Friend Pedro: \r\nH96NI-3GNXL-RKFBK\r\n\r\nBefore Your Eyes\r\nI7BFE-W99X0-HCW9T\r\n\r\nOri And The Blind Forest \r\nDefinitive Edition\r\nKH7CW-OJGM9-TG03N\r\n\r\nGoing Under\r\nVDMTG-9J3XV-F6WBQ\r\n\r\nFinding Paradise\r\n3LHRP-8CGY5-JM2A2\r\n\r\nCult of the Lamb\r\n2IXCY-ENXY6-4A09R\r\n\r\n";
        }
        else
        {
            if (PlayerPrefs.GetInt("HasFindingParadise") == 0 && PlayerPrefs.GetInt("HasGoingUnder") == 0 && PlayerPrefs.GetInt("HasBeforeYourEyes") == 0 && PlayerPrefs.GetInt("HasOri") == 0 && PlayerPrefs.GetInt("HasMyFriendPedro") == 0 && PlayerPrefs.GetInt("HasCultOfTheLamb") == 0)
            {
                text.gameObject.SetActive(false);
                noKeysText.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(true);
                noKeysText.SetActive(false);
                if (PlayerPrefs.GetInt("HasFindingParadise") == 1 && onetime1)
                {
                    text.text += "Finding Paradise\r\n3LHRP - 8CGY5 - JM2A2\r\n\r\n";
                    onetime1 = false;
                }
                if (PlayerPrefs.GetInt("HasGoingUnder") == 1 && onetime2)
                {
                    text.text += "Going Under\r\nVDMTG-9J3XV-F6WBQ\r\n\r\n";
                    onetime2 = false;
                }
                if (PlayerPrefs.GetInt("HasOri") == 1 && onetime3)
                {
                    text.text += "Ori And The Blind Forest \r\nDefinitive Edition\r\nKH7CW-OJGM9-TG03N\r\n\r\n";
                    onetime3 = false;
                }
                if(PlayerPrefs.GetInt("HasBeforeYourEyes") == 1 && onetime4)
                {
                    text.text += "Before Your Eyes\r\nI7BFE-W99X0-HCW9T\r\n\r\n";
                    onetime4 = false;
                }
                if (PlayerPrefs.GetInt("HasMyFriendPedro") == 1 && onetime5)
                {
                    text.text += "My Friend Pedro: \r\nH96NI-3GNXL-RKFBK\r\n\r\n";
                    onetime5 = false;
                }
                if (PlayerPrefs.GetInt("HasCultOfTheLamb") == 1 && onetime6)
                {
                    text.text += "Cult of the Lamb\r\n2IXCY-ENXY6-4A09R\r\n\r\n";
                    onetime6 = false;
                }
            }
        }
    }

    
}
