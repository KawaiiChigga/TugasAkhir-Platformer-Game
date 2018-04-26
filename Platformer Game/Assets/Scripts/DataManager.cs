using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    // Only one instance at a time
    public static DataManager instance;

    /// <summary>The player's current score.</summary>
    public int score;
    /// <summary>The player's remaining health.</summary>
    public int health;
    /// <summary>The player's remaining lives.</summary>
    public int lives;

    // Use this for initialization
    void Awake() {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }
}

