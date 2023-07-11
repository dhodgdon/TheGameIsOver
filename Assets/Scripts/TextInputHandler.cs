using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextInputHandler : MonoBehaviour
{
    private String input;

    private bool edited = false;

    public void OnEndEdit(string text)
    {
        if (!edited) {
            string filePath = "Assets/Scripts/all_players.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                // Append new name to the file
                writer.WriteLine(text);
                edited = true;
            }
        }
    }

    public void ReadStringInput(string s) {
        input = s;
    }
}
