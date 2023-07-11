using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;

public class TMPEditor : MonoBehaviour
{

    public TMP_Text allPlayers;


    // Start is called before the first frame update
    void Start()
    {
        string filePath = "Assets/Scripts/all_players.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Read the contents of the file line by line
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Process each line of the file
                allPlayers.text += line + "\n";
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
