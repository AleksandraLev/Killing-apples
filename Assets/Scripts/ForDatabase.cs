using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ForDatabase : MonoBehaviour
{
    public string DataBaseName;
    public InputField NameInput;
    public InputField EmailInput;
    public InputField PasswordInput;

    public GameObject Menu;
    public GameObject Logged;
    public GameObject LoggedMessage;
    public static int MaxScoreTest;


    public Text NameTestForShow;
    //public Text EmailTextForFindInfo;
    //public Text PasswordTextForFindInfo;
    public Text MaxScore;
    public InputField _EmailInputForFindInfo;
    public InputField _PasswordInputForFindInfo;
    public static string Name;
    public static string Email;
    public static string Password;
    public GameObject SceneLogIn;
    public GameObject SceneLogedIn;


    public InputField NameInputForUpdate;
    public InputField EmailInputForUpdate;
    public InputField PasswordInputForUpdate;

    private void Start()
    {
        if (Email != null)
        {
            Logged.SetActive(true);
            Menu.SetActive(false);
            LoggedMessage.SetActive(false);
            NameInputForUpdate.Select();
            NameTestForShow.text = Name;
            if (MaxScore.text != MaxScoreTest.ToString())
            {
                MaxScore.text = "������: " + MaxScoreTest.ToString();
                UpdateMaxScore();
            }
        }
    }

    public Text RegisterPanelText;
    public GameObject SceneIsRegistered;
    public GameObject SceneRegister;
    public void InsertInfo()
    {
        var _NameInput = NameInput.text.Trim();
        var _EmailInput = EmailInput.text.Trim();
        var _PasswordInput = PasswordInput.text;
        if (!string.IsNullOrEmpty(_NameInput) && !string.IsNullOrEmpty(_EmailInput) && !string.IsNullOrEmpty(_PasswordInput))
        {
            string coon = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
            using (IDbConnection dbcon = new SqliteConnection(coon))
            {
                dbcon.Open();

                // �������� � ��������� SQL-�������
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    // ����������������� SQL-������ ��� ��������� ����������� ����� ������������
                    string SQLQuery = "SELECT Email FROM Users WHERE EMAIL=@Email";
                    dbcmd.CommandText = SQLQuery;
                    dbcmd.Parameters.Add(new SqliteParameter("@Email", _EmailInput));

                    // ���������� SQL-������� � ��������� ����������
                    object result = dbcmd.ExecuteScalar();

                    // �������� ���������� �� ������� ��������
                    if (result != null && result != DBNull.Value)
                    {
                        Debug.Log("������! ������������ � ����� ������ ��� ���������������!");
                        SceneIsRegistered.SetActive(false);
                        RegisterPanelText.text = "������������ � ����� ������ ��� ����������";
                        return;
                    }
                    else
                    {
                        RegisterPanelText.text = "";
                    }
                }
                dbcon.Close();
            }
            using (IDbConnection dbcon = new SqliteConnection(coon))
            {
                dbcon.Open();

                // �������� � ��������� SQL-�������
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    // SQL-������ ��� ���������� ������
                    string SQLQuery = "Insert Into Users(Name,Email,Password,MaxScore) " + "Values('" + _NameInput + "', '" + _EmailInput + "', '" + _PasswordInput + "', '0')";
                    dbcmd.CommandText = SQLQuery;

                    // ���������� SQL-�������
                    dbcmd.ExecuteNonQuery();

                    Name = _NameInput;
                    Email = _EmailInput;
                    Password = _PasswordInput;
                    MaxScore.text = "������: 0";
                    ScoreShow_AND_WinAndOver.maxScore = 0;
                    NameTestForShow.text = _NameInput;

                    NameInput.text = "";
                    EmailInput.text = "";
                    PasswordInput.text = "";
                }
                dbcon.Close();
            }
            SceneRegister.SetActive(false);
            SceneIsRegistered.SetActive(true);
            RegisterPanelText.text = "";
        }
        else
        {
            RegisterPanelText.text = "��������� ��� ����!";
        }

    }

    public Text LoginPanelText;
    public void FindInfo()
    {
        var _EmailInput = _EmailInputForFindInfo.text.Trim();
        var _PasswordInput = _PasswordInputForFindInfo.text;
        if (!string.IsNullOrEmpty(_EmailInput) && !string.IsNullOrEmpty(_PasswordInput))
        {
            string _coon = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
            using (IDbConnection dbcon = new SqliteConnection(_coon))
            {
                dbcon.Open();

                // �������� � ��������� SQL-�������
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    // ����������������� SQL-������ ��� ��������� ����������� ����� � ������ ������������
                    string SQLQuery = "SELECT Email FROM Users WHERE EMAIL=@Email And Password=@Password";
                    dbcmd.CommandText = SQLQuery;
                    dbcmd.Parameters.Add(new SqliteParameter("@Email", _EmailInput));
                    dbcmd.Parameters.Add(new SqliteParameter("@Password", _PasswordInput));
                    // ���������� SQL-������� � ��������� ����������
                    object result = dbcmd.ExecuteScalar();

                    // �������� ���������� �� ������� ��������
                    if (result == null || result == DBNull.Value)
                    {
                        LoginPanelText.text = "������ �������� �� ����������!";
                        return;
                    }
                    else
                    {
                        LoginPanelText.text = "";
                    }
                }
                dbcon.Close();
            }
            using (IDbConnection dbcon = new SqliteConnection(_coon))
            {
                dbcon.Open();

                // �������� � ��������� SQL-�������
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    //SQL-������ ��� ��������� ������ � ������������
                    string SQLQuery = "Select ID, Name, Email, Password, MaxScore FROM Users Where Email='" + _EmailInputForFindInfo.text.Trim() + "' And Password='" + _PasswordInputForFindInfo.text.Trim() + "'";
                    dbcmd.CommandText = SQLQuery;
                    // ���������� SQL-������� � ��������� ����������
                    using (IDataReader reader = dbcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // This part for item from database
                            //ID = reader.GetString(0);
                            Name = reader.GetString(1);
                            NameTestForShow.text = reader.GetString(1);
                            Email = reader.GetString(2);
                            Password = reader.GetString(3);
                            MaxScore.text = "������: " + reader.GetString(4);
                            ScoreShow_AND_WinAndOver.maxScore = int.Parse(reader.GetString(4));
                        }
                    }
                    SceneLogedIn.SetActive(true);
                    SceneLogIn.SetActive(false);
                    LoginPanelText.text = "";
                }
                dbcon.Close();
            }

            _EmailInputForFindInfo.Select();
            _EmailInputForFindInfo.text = "";
            _PasswordInputForFindInfo.Select();
            _PasswordInputForFindInfo.text = "";
            LoginPanelText.text = "";
        }
        else
        {
            LoginPanelText.text = "��������� ��� ����!";
        }
    }

    public void BeforeUpdateInfo()
    {
        NameInputForUpdate.Select();
        NameInputForUpdate.text = Name;
        EmailInputForUpdate.Select();
        EmailInputForUpdate.text = Email;
        PasswordInputForUpdate.Select();
        PasswordInputForUpdate.text = Password;
    }

    public Text UpdatePanelText;
    public void UpdateInfo()
    {
        if (!string.IsNullOrEmpty(NameInputForUpdate.text.Trim()) && !string.IsNullOrEmpty(EmailInputForUpdate.text.Trim()) && !string.IsNullOrEmpty(PasswordInputForUpdate.text.Trim()))
        {
            string emailCopy = Email; // ���������� � camelCase ��� ���������� EmailCopy

            // �������� ����������� � ���� ������
            string connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
            using (IDbConnection dbcon = new SqliteConnection(connectionString))
            {
                dbcon.Open();

                // �������� � ��������� SQL-�������
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    // ����������������� SQL-������ ��� ���������� ������
                    string SQLQuery = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Email = @EmailCopy";
                    dbcmd.CommandText = SQLQuery;

                    // ������� ���������� �������
                    dbcmd.Parameters.Add(new SqliteParameter("@Name", NameInputForUpdate.text));
                    dbcmd.Parameters.Add(new SqliteParameter("@Email", EmailInputForUpdate.text));
                    dbcmd.Parameters.Add(new SqliteParameter("@Password", PasswordInputForUpdate.text));
                    dbcmd.Parameters.Add(new SqliteParameter("@EmailCopy", emailCopy));

                    NameTestForShow.text = NameInputForUpdate.text;

                    // ���������� SQL-�������
                    dbcmd.ExecuteNonQuery();
                    UpdatePanelText.text = "������ ���� ��������";
                }

            }
        }
        else
        {
            UpdatePanelText.text = "��������� ��� ����!";
        }
    }

    public void UpdateMaxScore()
    {
        string emailCopy = Email; // ���������� � camelCase ��� ���������� EmailCopy

        // �������� ����������� � ���� ������
        string connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        using (IDbConnection dbcon = new SqliteConnection(connectionString))
        {
            dbcon.Open();

            // �������� � ��������� SQL-�������
            using (IDbCommand dbcmd = dbcon.CreateCommand())
            {
                // ����������������� SQL-������ ��� ���������� ������
                string SQLQuery = "UPDATE Users SET MaxScore = @MaxScore WHERE Email = @EmailCopy";
                dbcmd.CommandText = SQLQuery;

                // ������� ���������� �������
                dbcmd.Parameters.Add(new SqliteParameter("@MaxScore", MaxScoreTest.ToString()));
                //dbcmd.Parameters.Add(new SqliteParameter("@EmailCopy", emailCopy));

                NameTestForShow.text = NameInputForUpdate.text;

                // ���������� SQL-�������
                dbcmd.ExecuteNonQuery();
            }
        }
    }

    public void DeleteInfo()
    {
        try
        {
            string coon = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
            IDbConnection dbcon;
            IDbCommand dbcmd;

            dbcon = new SqliteConnection(coon);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();

            // SQL-������ ��� �������� �������� �� ����������� �����
            string SQLQuery = "DELETE FROM Users WHERE Email='" + Email + "'";
            dbcmd.CommandText = SQLQuery;

            // ���������� SQL-�������
            dbcmd.ExecuteNonQuery();

            // ������� ������ ������������
            Name = null;
            Email = null;
            Password = null;
            MaxScore.text = "";
            NameTestForShow.text = "";
            ScoreShow_AND_WinAndOver.maxScore = 0;
        }
        catch (Exception ex)
        {
            // ��������� ������ ��� �������� ��������
            Debug.LogError(ex.Message);
        }
    }
    public void Exist()
    {
        NameTestForShow.text = "";
        MaxScore.text = "";
        RegisterPanelText.text = "";
        LoginPanelText.text = "";
        UpdatePanelText.text = "";
    }
}
