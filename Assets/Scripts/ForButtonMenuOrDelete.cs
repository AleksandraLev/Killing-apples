using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForButtonMenuOrDelete : MonoBehaviour
{
    public InputField NameInput;
    public InputField EmailInput;
    public InputField PasswordInput;
    public void ÑlearFields()
    {
        if (NameInput != null)
        {
            NameInput.Select();
            NameInput.text = "";
        }
        if (EmailInput != null)
        {
            EmailInput.Select();
            EmailInput.text = "";
        }
        if (PasswordInput != null)
        {
            PasswordInput.Select();
            PasswordInput.text = "";
        }
    }
}
