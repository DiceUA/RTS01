using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Determine Land Unit object
/// </summary>
public class LandUnit
{
    private int maxHealth = 100;
    private int currentHealth;
    private string name;
    private string type;

    public LandUnit(string unitName)
    {
        name = unitName;
        currentHealth = maxHealth;
    }


    public int CurrentHealth 
    { 
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Type 
    {
        get { return type; }
        set { type = value; }
    }

}

