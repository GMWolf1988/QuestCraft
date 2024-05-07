using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour, IPlayerStateEvents
{
    public static PlayerState instance;

    public Location CurrentLocation;

    public GameObject LeftPanel;

    public GameObject WinPanel;

    public QuestState QuestManager;

    public Actions Actions;
    public TravelList TravelList;
    public PlayerStatePanel PlayerStatePanel;
    public EventTitles EventTitles;
    public TravelText TravelText;

    public GameObject ProceduralCodeNum;
    public GameObject HandcraftedCodeNum;
    private QuestState questState; // Reference to QuestState script

    public int XP = 1000;
    public int Gold = 0;
    public int TargetGold = 5000;
    public int MaxLevel = 99;

    private DateTime startTime;
    private DateTime endTime;

    private readonly List<int> levelXPLookupTable = new List<int>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        int accumulativeXP = 0;
        for (int i = 1; i <= MaxLevel; i++)
        {
            levelXPLookupTable.Add((Database.BaseXPScale * i) + accumulativeXP);
            accumulativeXP += Database.BaseXPScale * i;
        };
    }

    void Start()
    {
        // Capture the start time when the game begins
        startTime = DateTime.Now;

        ChangeLocation(Database.PickFirstLocationByTag("player-start"));
    }

    void IPlayerStateEvents.OnChangeLocation(Location location)
    {
        ChangeLocation(location);
    }

    void ChangeLocation(Location location)
    {
        CurrentLocation = location;
        Actions.OnActionContextChanged();
        TravelList.OnLocationChanged();
        PlayerStatePanel.OnLocationChanged();
        
        TravelText.SetText(location.TravelText);

        ExecuteEvents.Execute<ILeftPanelEvents>(LeftPanel, null,
            (x, y) => x.OnShowPanel("TravelText"));
    }

    public double GetLevel()
    {
        double level = 0;
        for (int i = 0; i < levelXPLookupTable.Count; i++)
        {
            int required = levelXPLookupTable[i];
            if (XP < required)
            {
                int prevRequired = levelXPLookupTable[i - 1];
                level = (double)i + ((double)(XP - prevRequired) / (double)(required - prevRequired));
                break;
            }
        }

        return level;
    }

    public int GetXPToNextLevel()
    {
        // If the player has reached the maximum level, return 0 for the XP to next level.
        if (GetLevel() == MaxLevel)
            return 0;

        // If the player has not reached the maximum level, return the XP to the next level.
        int nextLevelRequired = levelXPLookupTable[(int)GetLevel()];
        return nextLevelRequired - XP;
    }

    public void AwardXP(int amount)
    {
        XP += amount;
    }

    public void AwardGold(int amount)
    {
        Gold += amount;

        if (Gold >= TargetGold)
        {
             // Capture the end time when the game finishes
            endTime = DateTime.Now;
            WritePlayerReport();
            StartCoroutine(ActivateWinPanelWithDelay(2f));  
        }
    }

    private System.Collections.IEnumerator ActivateWinPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        WinPanel.SetActive(true);
        StartCoroutine(LoadMainMenuAfterDelay(6f));

        // Check if questState is not null
        if (questState != null)
        {
            // If questState's Mode is 0
            if (questState.Mode == 0)
            {
                // Activate ProceduralCodeNum GameObject
                ProceduralCodeNum.SetActive(true);
            }
            // If questState's Mode is not 0
            else
            {
                 // Activate HandcraftedCodeNum GameObject
                 HandcraftedCodeNum.SetActive(true);
            }
        }
    }

    // Coroutine to load the main menu scene after a delay
    private System.Collections.IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu"); 
    }

    // Method to generate a player report including session ID, game mode, completed quests, XP, gold, and time elapsed
void WritePlayerReport()
{
    // Generate a unique session ID
    Guid sessionID = Guid.NewGuid();
    
    // Determine the game mode based on the current QuestState mode
    string mode = QuestState.instance.Mode == 0 ? "Procedural" : "Handmade";
    
    // Create a report string with session ID, game mode, completed quests count, XP, and gold
    string report = $"Session ID: {sessionID}\n" +
                    $"Game mode: {mode}\n" +
                    $"Quests completed: {QuestState.instance.CompletedQuests.Count}\n" +
                    $"Total XP: {XP}\n" +
                    $"Total gold: {Gold}\n";

    // Calculate the time elapsed between start and end time and format it as minutes:seconds
    TimeSpan elapsedTime = endTime - startTime;
    string timeElapsed = $"Time elapsed: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}\n";
    
    // Add time elapsed to the report
    report += timeElapsed;

    // Write the report to a text file with the session ID as filename
    System.IO.File.WriteAllText($"PlayerReport_{sessionID}.txt", report);
    }
}
