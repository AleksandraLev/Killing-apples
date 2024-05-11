using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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


    public Text NameTestForFindInfo;
    public Text EmailTextForFindInfo;
    public Text PasswordTextForFindInfo;
    public Text MaxScore;
    public InputField _EmailInputForFindInfo;
    public InputField _PasswordInputForFindInfo;
    public static string Name;
    public static string Email;
    public static string Password;
    public GameObject SceneLogIn;
    public GameObject SceneLogedIn;
    public GameObject AccountError;


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
            if (Name != null)
            {
                NameTestForFindInfo.text = Name;
            }
            if (MaxScore.text != MaxScoreTest.ToString())
            {
                MaxScore.text = MaxScoreTest.ToString();
                UpdateMaxScore();
            }
        }
    }
    public void InsertInfo()
    {
        var _NameInput = NameInput.text.Trim();
        var _EmailInput = EmailInput.text.Trim();
        var _PasswordInput = PasswordInput.text;




        string coon = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(coon);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string SQLQuery = "Insert Into Users(Name,Email,Password,MaxScore) " +
            "Values('" + _NameInput + "', '" + _EmailInput + "', '" + _PasswordInput + "', '" + 0 + "')";
        dbcmd.CommandText = SQLQuery;
        //string SQLQueryForID = "Select ID FROM Users Where Email='" + _EmailInput + "' And Password='" + _PasswordInput + "'";
        //dbcmd.CommandText = SQLQueryForID;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            //ID = reader.GetString(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;

        Name = _NameInput;
        Email = _EmailInput;
        Password = _PasswordInput;
        MaxScore.text = "0";
        ScoreShow_AND_WinAndOver.maxScore = 0;
        NameTestForFindInfo.text = _NameInput;

        NameInput.text = "";
        EmailInput.text = "";
        PasswordInput.text = "";
    }



    public void FindInfo()
    {
        try
        {
            string coon = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
            IDbConnection dbcon;
            IDbCommand dbcmd;
            IDataReader reader;
            dbcon = new SqliteConnection(coon);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();
            string SQLQuery = "Select ID, Name, Email, MaxScore FROM Users Where Email='" + _EmailInputForFindInfo.text.Trim() + "' And Password='" + _PasswordInputForFindInfo.text.Trim() + "'";
            dbcmd.CommandText = SQLQuery;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                // This part for item from batabase
                //ID = reader.GetString(0);
                NameTestForFindInfo.text = reader.GetString(1);
                EmailTextForFindInfo.text = reader.GetString(2);
                MaxScore.text = reader.GetString(3);
            }

            ScoreShow_AND_WinAndOver.maxScore = int.Parse(MaxScore.text);
            Name = NameTestForFindInfo.text;
            Email = EmailTextForFindInfo.text;
            Password = _PasswordInputForFindInfo.text;
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbcon.Close();
            dbcon = null;

            if (NameTestForFindInfo.text != "" || EmailTextForFindInfo.text != "")
            {
                SceneLogedIn.SetActive(true);
                SceneLogIn.SetActive(false);
                AccountError.SetActive(false);
            }
            else
            {
                AccountError.SetActive(true);
            }
        }
        catch
        {
            AccountError.SetActive(true);
        }

        _EmailInputForFindInfo.Select();
        _EmailInputForFindInfo.text = "";
        _PasswordInputForFindInfo.Select();
        _PasswordInputForFindInfo.text = "";

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

    public void UpdateInfo()
    {
        string emailCopy = Email; // Приведение к camelCase для переменной EmailCopy

        // Создание подключения к базе данных
        string connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        using (IDbConnection dbcon = new SqliteConnection(connectionString))
        {
            dbcon.Open();

            // Создание и настройка SQL-команды
            using (IDbCommand dbcmd = dbcon.CreateCommand())
            {
                // Параметризованный SQL-запрос для обновления данных
                string SQLQuery = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE Email = @EmailCopy";
                dbcmd.CommandText = SQLQuery;

                // Задание параметров запроса
                dbcmd.Parameters.Add(new SqliteParameter("@Name", NameInputForUpdate.text));
                dbcmd.Parameters.Add(new SqliteParameter("@Email", EmailInputForUpdate.text));
                dbcmd.Parameters.Add(new SqliteParameter("@Password", PasswordInputForUpdate.text));
                dbcmd.Parameters.Add(new SqliteParameter("@EmailCopy", emailCopy));

                NameTestForFindInfo.text = NameInputForUpdate.text;

                // Выполнение SQL-запроса
                dbcmd.ExecuteNonQuery();
            }
        }
    }

    public void UpdateMaxScore()
    {
        string emailCopy = Email; // Приведение к camelCase для переменной EmailCopy

        // Создание подключения к базе данных
        string connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        using (IDbConnection dbcon = new SqliteConnection(connectionString))
        {
            dbcon.Open();

            // Создание и настройка SQL-команды
            using (IDbCommand dbcmd = dbcon.CreateCommand())
            {
                // Параметризованный SQL-запрос для обновления данных
                string SQLQuery = "UPDATE Users SET MaxScore = @MaxScore WHERE Email = @EmailCopy";
                dbcmd.CommandText = SQLQuery;

                // Задание параметров запроса
                dbcmd.Parameters.Add(new SqliteParameter("@MaxScore", MaxScoreTest.ToString()));
                dbcmd.Parameters.Add(new SqliteParameter("@EmailCopy", emailCopy));

                NameTestForFindInfo.text = NameInputForUpdate.text;

                // Выполнение SQL-запроса
                dbcmd.ExecuteNonQuery();
            }
        }
    }

    public void Exist()
    {
        NameTestForFindInfo.text = "";
        MaxScore.text = "";
    }
}
