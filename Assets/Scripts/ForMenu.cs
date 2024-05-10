using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ForMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Logged;
    public GameObject LoggedMessage;
    public GameObject CongratulationsOnRegistering;
    public GameObject UpdateInformationAboutAccount;
    public void LogfedOrNo()
    {
        if (ForDatabase.Email != null)
        {
            Logged.SetActive(true);
            Menu.SetActive(false);
            LoggedMessage.SetActive(false);
            CongratulationsOnRegistering.SetActive(false);
            UpdateInformationAboutAccount.SetActive(false);
        }
        else
        {
            Logged.SetActive(false);
            Menu.SetActive(true);
            LoggedMessage.SetActive(false);
            CongratulationsOnRegistering.SetActive(false);
            UpdateInformationAboutAccount.SetActive(false);
        }
    }
}
