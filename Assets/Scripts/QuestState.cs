using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

public class QuestState : MonoBehaviour, IQuestStateEvents
{
    public static QuestState instance;

    public QuestList QuestList;

    public EventTitles EventTitles;

    private readonly HashSet<string> _seenQuests = new HashSet<string>();

    private readonly List<Quest> _availableQuests = new List<Quest>();

    public readonly List<Quest> AcceptedQuests = new List<Quest>();

    int _activeQuestIndex;

    public readonly List<Quest> CompletedQuests = new List<Quest>();

 // Public property to access the current mode, but only privately set
public int Mode { get; private set; }

// Unity's Awake function, called when the object is initialized
private void Awake()
{
    // Check if there's no existing instance of this object
    if (instance == null)
        // If not, set this instance as the singleton instance
        instance = this;

    // Create a new instance of System.Random for generating random numbers
    System.Random random = new System.Random();

    // Generate a random number between 0 (inclusive) and 2 (exclusive) and assign it to Mode
    Mode = random.Next(2);

    // If Mode is 0
    if (Mode == 0)
    {
        // Call a function to add procedural quests
        AddProceduralQuest();
    }
    // If Mode is not 0 (it's 1)
    else
    {
        // Call a function to add handmade quests
        AddHandmadeQuests();
    }
}


    // Update method called every frame
private void Update()
{
    EnsureMinimumQuests(); // Ensure minimum number of quests are available
}

// Method to ensure minimum number of quests are available
void EnsureMinimumQuests()
{
    int minimumQuests = 8; // Minimum number of quests to maintain
    int maximumQuests = 12; // Maximum number of quests allowed

    // If the current count is below the minimum, add quests up to the maximum limit
    if (_availableQuests.Count < minimumQuests)
    {
        // Calculate number of quests to add
        int questsToAdd = Mathf.Min(maximumQuests - _availableQuests.Count, maximumQuests);
        // Loop to add quests
        for (int i = 0; i < questsToAdd; i++)
        {
            Quest quest = new Quest(true); // Create new quest
            // Check if the quest has not been seen before
            if (!_seenQuests.Contains(quest.Title))
            {
                _seenQuests.Add(quest.Title); // Add quest title to seen list
                _availableQuests.Add(quest); // Add quest to available quests
            }
        }
    }
}

// Method to add procedural quests
void AddProceduralQuest()
{
    int questsToGenerate = 12; // Generates number of procedural quests
    // Loop to generate procedural quests
    for (int i = 0; i < questsToGenerate; i++)
    {
        Quest quest = new Quest(true); // Create new quest
        // Check if the quest has not been seen before
        if (!_seenQuests.Contains(quest.Title))
        {
            _seenQuests.Add(quest.Title); // Add quest title to seen list
            _availableQuests.Add(quest); // Add quest to available quests
        }
    }
}


    void AddHandmadeQuests()
    {
        {   // QUEST CHAIN START - GATHER 3 / COLLECT 4
            Quest quest11 = new Quest(false, true)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Can't Handle Their Drinks",
                Description = "TO ANY ADVENTURERS!\n \n" +
                "Blasted goblins attacked a local caravan that was carrying my supply of ale. Luckily for us, goblins hated the taste of dwarven stout. " +
                "However, it's be left on the road - good dwarven stout going warm in sun is an outrage. Please go collect my supply and I will pay you handsomely. " +
                "Also pick up some more logs first from the forest a long the way, need more wood for the fire. \n \n" +
                "Snorri Rockbitter - Inn Keeper\n \n" 
                //"<b>HINT:</b>"
            };

             Objective objective12 = new Objective(false)
            {
                Type = ObjectiveType.Gather,
                Quantity = 5,
                Target = Database.Gatherables[2],
                Source = Database.Locations[7],
            };

            Objective objective13 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[15],
                Source = Database.Locations[7],
            };

            quest11.Objectives.Add(objective12);
            quest11.Objectives.Add(objective13);
            quest11.Rewards.Add(Database.Rewards[0]);
            quest11.RewardQuantities.Add(350);
            quest11.Rewards.Add(Database.Rewards[1]);
            quest11.RewardQuantities.Add(500);
            // ADDITIONAL QUEST DEFINE END
        
            // QUEST DEFINE START - KILL 1
           // Quest quest = new Quest(false)
            Quest quest = new Quest(false, true, () => _availableQuests.Add(quest11))
            {
                Status = QuestStatus.Available,
                //QuestGiver = Database.NPCs[0],
                QuestGiverLocation = Database.Locations[1],
                Title = "Spider Menace",
                Description = "TO ANY ADVENTURERS!\n \n" +
                "I've been hearing rumours of late, that a cave just up the mountain pass is festered with giant spiders." +
                " These spiders are causing havoc for the merchants travelling into town, and we are loosing trade because of it." +
                " Please travel up the mountain pass and find the cave where they are nesting. Kill these insects, and return to my shop for your reward. \n \n" +
                "Safe travels adventurers! \n" +
                "Gotrek Steelaxe - Weaponsmith \n\n" 
                //"<b>HINT:</b>"                 
            };

            Objective objective = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Target = Database.MobTypes[3],
                Quantity = 5,
                Source = Database.Locations[9] 
            };

            quest.Objectives.Add(objective);
            quest.Rewards.Add(Database.Rewards[0]);
            quest.RewardQuantities.Add(500);
            quest.Rewards.Add(Database.Rewards[1]);
            quest.RewardQuantities.Add(500);

            _availableQuests.Add(quest);
        } // QUEST DEFINE END

        { // ADDITIONAL QUEST DEFINE START - KILL 2
            Quest quest2 = new Quest(false)
            {
                Status = QuestStatus.Available,
                // QuestGiver = Database.NPCs[1],
                QuestGiverLocation = Database.Locations[2],
                Title = "Urgent Request",
                Description = "TO ANY WILLING SOUL OUT THERE! \n \n" +
                "The armoury is running low on wooden logs used to make shields for the town guard, and with the last shipment gone missing I fear I will not be able to meet my quota for the month. " +
                "Any good soul out there willing to go to the forest and collect 10 logs and return them to me will be doing a good deed for this town. I will also pay well upon completion of this task. \n \n " +
                "Grog Ironjaw - Armoursmith \n \n"
                           // "<b>HINT:</b>"
            };

            Objective objective2 = new Objective(false)
            {
                Type = ObjectiveType.Gather,
                Target = Database.Gatherables[2],
                Quantity = 10,
                Source = Database.Locations[7]
            };

            quest2.Objectives.Add(objective2);
            quest2.Rewards.Add(Database.Rewards[0]);
            quest2.RewardQuantities.Add(250);
            quest2.Rewards.Add(Database.Rewards[1]);
            quest2.RewardQuantities.Add(150);

            _availableQuests.Add(quest2);
        } // ADDITIONAL QUEST DEFINE END

        { // ADDITIONAL QUEST DEFINE START - GATHER 1
            Quest quest3 = new Quest(false)
            {
                Status = QuestStatus.Available,
                // QuestGiver = Database.NPCs[1],
                QuestGiverLocation = Database.Locations[4],
                Title = "Gathering Nightshade",
                Description = "TO ANY WILLING ADVENTURER! \n \n" +
                "I'm currently in need of a worthy adventurer to go out and gather nightshade, so i can use this plant to make potions for some of the sick towns people. " +
                "I need 10 nightshade in order to do this and will pay well upon completion. To find this plant you will need to travel to the mountain pass and delve into the cave up there. " +
                "My residence currently is at the Town Inn, in the centre of town, please return here when you have the plant. \n \n" + 
                "Safe travels adventurer! \n" +
                "Pike Greenleaf - Druid \n \n" 
                //"<b>HINT:</b>"
            };

            Objective objective3 = new Objective(false)
            {
                Type = ObjectiveType.Gather,
                Target = Database.Gatherables[4],
                Quantity = 10,
                Source = Database.Locations[9],   
            };

            quest3.Objectives.Add(objective3);
            quest3.Rewards.Add(Database.Rewards[0]);
            quest3.RewardQuantities.Add(300);
            quest3.Rewards.Add(Database.Rewards[1]);
            quest3.RewardQuantities.Add(150);

            _availableQuests.Add(quest3);
        } // ADDITIONAL QUEST DEFINE END

        { // ADDITIONAL QUEST DEFINE START - KILL 3
            Quest quest4 = new Quest(false)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Further Concerns",
                Description = "HAIL ADVENTURERS! \n \n" +
                "We have had growing reports that Goblins have been spotted close by to town. I don't know the exact number, " +
                "but they are becoming a nuisance for merchant travellers and the town guardsmen. They were last spotted in the Forest Glade. " +
                "Please go there and eradicate these pests, and report back to me at the Inn. \n \n" +
                "Good hunting adventurers! \n" +
                "Snorri Rockbitter - Inn Keeper. \n \n" 
                //"<b>HINT:</b>"
            };

            Objective objective4 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Target = Database.MobTypes[0],
                Quantity = 5,
                Source = Database.Locations[7],   
            };

            quest4.Objectives.Add(objective4);
            quest4.Rewards.Add(Database.Rewards[0]);
            quest4.RewardQuantities.Add(750);
            quest4.Rewards.Add(Database.Rewards[1]);
            quest4.RewardQuantities.Add(750);

            _availableQuests.Add(quest4);
            // ADDITIONAL QUEST DEFINE END
        }

        { // ADDITIONAL QUEST DEFINE START - COLLECT 1
            Quest quest5 = new Quest(false)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[10],
                Title = "The Collector",
                Description = "HAIL ADVENTURERS! \n \n" +
                "My fool of an apprentice decided to go wondering off into the woods and never came back, only the gods know what has happen to him. " +
                "The fool was mean't to go to the Town Library and bring me back a rare book on Beasts, which i can use to help better defend the town against them. " +
                "Please go to the library and return to me at the Fortress outside of town. A reward will be given once the book is in my hands.\n \n" +
                "Irna Albrek - Town Wizard. \n \n" 
               // "<b>HINT:</b>"
            };

             Objective objective5 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[12],
                Source = Database.Locations[5],   
            };

            quest5.Objectives.Add(objective5);
            quest5.Rewards.Add(Database.Rewards[0]);
            quest5.RewardQuantities.Add(100);
            quest5.Rewards.Add(Database.Rewards[1]);
            quest5.RewardQuantities.Add(150);

            _availableQuests.Add(quest5);
            // ADDITIONAL QUEST DEFINE END
        }  

        { // ADDITIONAL QUEST DEFINE START - KILL 4
            Quest quest6 = new Quest(false)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[6],
                Title = "Wanted: 'RipEye' ",
                Description = "MERCENARIES! \n \n" +
                "The Orc Warboss known as RipEye has been spotted near the sacred Temple in the forest. " +
                "The time is now to strike and deal a blow to the Orcs of this land. A big reward will be given to whom ever completes this task. \n \n" +
                "Good hunting! \n" +
                "Logan Wolfbane - Guild Leader. \n \n" 
                //"<b>HINT:</b>"
            };

            Objective objective6 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 1,
                Target = Database.MobTypes[5],
                Source = Database.Locations[8],   
            };

            quest6.Objectives.Add(objective6);
            quest6.Rewards.Add(Database.Rewards[0]);
            quest6.RewardQuantities.Add(900);
            quest6.Rewards.Add(Database.Rewards[1]);
            quest6.RewardQuantities.Add(1000);

            _availableQuests.Add(quest6);
            // ADDITIONAL QUEST DEFINE END
        }

        { // ADDITIONAL QUEST DEFINE START - KILL 5
            Quest quest7 = new Quest(false, true)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[6],
                Title = "Spiders & Their Matriarch ",
                Description = "MERCENARIES! \n \n" +
                "The spiders have been causing disruption once more. " +
                "This time they have been attacking towns people and the militia on route to the fortess. They've been spotted nesting in the forest and I have it on good authority the Spider Matriarch has been seen entering the cave " +
                "up in the mountains. Kill these insects and report back here once the task in complete to recieve your reward \n \n" +
                "Good hunting! \n" +
                "Logan Wolfbane - Guild Leader. \n \n" 
                //"<b>HINT:</b>"
            };

            Objective objective6 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 5,
                Target = Database.MobTypes[3],
                Source = Database.Locations[7],   
            };

            Objective objective7 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 1,
                Target = Database.MobTypes[4],
                Source = Database.Locations[9],   
           };

            quest7.Objectives.Add(objective6);
            quest7.Objectives.Add(objective7);
            quest7.Rewards.Add(Database.Rewards[0]);
            quest7.RewardQuantities.Add(1000);
            quest7.Rewards.Add(Database.Rewards[1]);
            quest7.RewardQuantities.Add(900);

            _availableQuests.Add(quest7);
            // ADDITIONAL QUEST DEFINE END
        } 

        { // ADDITIONAL QUEST DEFINE START - COLLECT 2
            Quest quest8 = new Quest(false)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[1],
                Title = "Iron Shortage",
                Description = "HIRED HELP WANTED! \n \n" +
                "I need a strong able body to go collect the iron from the fortress and bring it back to my forge. " +
                "The merchant got to drunk with the guardsman over there and forgot to bring the shipment of iron here. " +
                "Anyone who is willing to do this will recieve payment once the iron is back at my shop. \n \n" +
                "Gotrek Steelaxe - Weapon Smith. \n \n" 
                //"<b>HINT:</b>"
            };

            Objective objective8 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[13],
                Source = Database.Locations[10],
            };

            quest8.Objectives.Add(objective8);
            quest8.Rewards.Add(Database.Rewards[0]);
            quest8.RewardQuantities.Add(250);
            quest8.Rewards.Add(Database.Rewards[1]);
            quest8.RewardQuantities.Add(150);

            _availableQuests.Add(quest8);
            // ADDITIONAL QUEST DEFINE END
        }

        { // ADDTIONAL QUEST DEFINE START - KILL 6 / COLLECT 3
            Quest quest9 = new Quest(false, true)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Kobold Candles",
                Description = "HAIL ADVENTURERS! \n \n" +
                "My brother and I run an seperate apothecary in business in Archet, and i need to send a shipment of candles to him. " +
                "Unfortunately I've ran out of candles and the merchant is not due for another week. I hear Kobolds up in the caves wear them on their heads. " +
                "Go to the cave and collect those candles from them, you would even be doing the town a favour by ridding us of them. "+
                "Once you have the candles come to my tavern, i will have a drink and gold waiting for you. \n \n" +
                "Snorri Rockbitter - Inn Keeper. \n \n" 
               // "<b>HINT:</b>"
            };

            Objective objective9 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 5,
                Target = Database.MobTypes[6],
                Source = Database.Locations[9],
            };

            Objective objective10 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[14],
                Source = Database.Locations[9],
            };

            quest9.Objectives.Add(objective9);
            quest9.Objectives.Add(objective10);
            quest9.Rewards.Add(Database.Rewards[0]);
            quest9.RewardQuantities.Add(900);
            quest9.Rewards.Add(Database.Rewards[1]);
            quest9.RewardQuantities.Add(900);

            _availableQuests.Add(quest9);
            // ADDITIONAL QUEST DEFINE END
        }

        {// ADDITIONAL QUEST DEFINE START - GATHER 2
            Quest quest10 = new Quest(false)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Blood Moss",
                Description = "GATHERERS NEEDED! \n \n" +
                "I'm currently staying at the local tavern and I'm in need of some more blood moss to create vials of potions for adventurers. If you are willing to travel into  the forest " +
                "for me and collect at least 10 of this non-vascular plant, i would be most greatful. Blood moss is not dangerous, it gets the name from the moss itself resemeblinmg blood. " +
                "I can spare a little amount of gold for the moss upon recieving. \n \n" +
                "Pike Greenleaf - Druid \n \n" 
                // "<b>HINT:</b>"
            };

             Objective objective11 = new Objective(false)
            {
                Type = ObjectiveType.Gather,
                Quantity = 10,
                Target = Database.Gatherables[5],
                Source = Database.Locations[7],
            };

            quest10.Objectives.Add(objective11);
            quest10.Rewards.Add(Database.Rewards[0]);
            quest10.RewardQuantities.Add(150);
            quest10.Rewards.Add(Database.Rewards[1]);
            quest10.RewardQuantities.Add(150);

            _availableQuests.Add(quest10);
            // ADDITIONAL QUEST DEFINE END
        }

        {
            // QUEST CHAIN START -  COLLECT 7 / KILL 7
            Quest quest14 = new Quest(false, true)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[10],
                Title = "Supplying The Frontline - Part III",
                Description = "ADVENTURERS!\n \n" +
                "The time has come to take this equipment to the frontline. "  +
                "Take this equipment to the fortress, however, we have got word orcs are attacking it as we speak. Kill them and take the equipment to the barracks inside.  " +
                "950 gold pieces will be given if you complete this task. \n \n" +
                "Logan Wolfbane - Guild Leader \n \n" 
                //"<b>HINT:</b>"
            }; 

             Objective objective16 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[16],
                Source = Database.Locations[6],
            };

            Objective objective17 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 10,
                Target = Database.MobTypes[1],
                Source = Database.Locations[10],
            };

            quest14.Objectives.Add(objective16);
            quest14.Objectives.Add(objective17);
            quest14.Rewards.Add(Database.Rewards[0]);
            quest14.RewardQuantities.Add(950);
            quest14.Rewards.Add(Database.Rewards[1]);
            quest14.RewardQuantities.Add(1000);
            // ADDITIONAL QUEST DEFINE END

           // QUEST CHAIN START -  COLLECT 6
            Quest quest13 = new Quest(false, true, () => _availableQuests.Add(quest14))
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[6],
                Title = "Supplying The Frontline - Part II",
                Description = "ADVENTURERS!\n \n" +
                "Grog Ironjaw has requested someone goes to pick up his armour for the frontline. "  +
                "There should be a dozen suits of chain mail armour. Go collect this armour and bring it back to the guild hall. " +
                "Gold will be given upon completing this task. Once the armour is here and inspected along with the swords from yesterday. I will post a new task on the quest board. \n \n" +
                "Logan Wolfbane - Guild Leader \n \n" 
                //"<b>HINT:</b>"
            }; 

             Objective objective15 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[4],
                Source = Database.Locations[2],
            };

            quest13.Objectives.Add(objective15);
            quest13.Rewards.Add(Database.Rewards[0]);
            quest13.RewardQuantities.Add(150);
            quest13.Rewards.Add(Database.Rewards[1]);
            quest13.RewardQuantities.Add(150);
            // ADDITIONAL QUEST DEFINE END

            // QUEST CHAIN DEFINE START - COLLECT 5
            Quest quest12 = new Quest(false, true, () => _availableQuests.Add(quest13))
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[6],
                Title = "Supplying The Frontline - Part I",
                Description = "ADVENTURERS! \n \n" +
                "The frontline needs supplying with new swords and armour. Gotrek Steelaxe has requested someone goes to pick up his swords today. " +
                "Please go collect these weapons and bring them back to the guild hall. Gold will be given upon completing this task. Once I have word from Grog Ironjaw, " +
                "I shall post the next stage of this task on the quest board. \n \n" +
                "Logan Wolfbane - Guild Leader \n \n" 
                //"<b>HINT:</b>"   
            };

            Objective objective14 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[0],
                Source = Database.Locations[1],
            };

            quest12.Objectives.Add(objective14);
            quest12.Rewards.Add(Database.Rewards[0]);
            quest12.RewardQuantities.Add(150);
            quest12.Rewards.Add(Database.Rewards[1]);
            quest12.RewardQuantities.Add(150);

            _availableQuests.Add(quest12);
        }

        {   // QUEST CHAIN DEFINE START - COLLECT 9 / KILL 8
            Quest quest17 = new Quest(false, true)
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Nature's Wraith - Part III",
                Description = "NOWS THE TIME TO STRIKE! \n \n" +
                "Come drink the intelligence potion from myself, I have also infused it with the ligthning you so bravely collected. " +
                "Before you leave the tavern you will feel empowered with natures energy to strike that kobald shaman down. " +
                "He was last spotted at the old temple in the forest. \n \n" +
                "Natures Fury Awaits! \n" +
                "Pike Greenleaf - Druid \n \n" 
                //"<b>HINT:</b>"
            };

             Objective objective22 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[17],
                Source = Database.Locations[4],
            };

            Objective objective23 = new Objective(false)
            {
                Type = ObjectiveType.Kill,
                Quantity = 1,
                Target = Database.MobTypes[7],
                Source = Database.Locations[8],
            };

            quest17.Objectives.Add(objective22);
            quest17.Objectives.Add(objective23);
            quest17.Rewards.Add(Database.Rewards[0]);
            quest17.RewardQuantities.Add(950);
            quest17.Rewards.Add(Database.Rewards[1]);
            quest17.RewardQuantities.Add(1000);


            // QUEST CHAIN DEFINE START - COLLECT 8 / GATHER 5
            Quest quest16 = new Quest(false, true, () => _availableQuests.Add(quest17))
            {
                Status = QuestStatus.Available,
                QuestGiverLocation = Database.Locations[4],
                Title = "Nature's Wraith - Part II",
                Description = " HAIL ADVENTURERS! \n \n" +
                "The intelligence potion is now ready for you. Although, I have one more task for you before you take on the kobold. " +
                "I need you to come and collect a empty vail from myself and go bottle lightning. Yes you read this correctly - LIGHTNING! " +
                "Travel to the cave high up in the mountains and when the weather turns, hold the vial up to the sky. Don't worry its totally safe - maybe. " +
                "Return to me once you have done this and recieve payment \n \n" +
                "Pike Greenleaf - Druid \n \n" +
                "<b>HINT:</b>"
            };

            Objective objective20 = new Objective(false)
            {
                Type = ObjectiveType.Collect,
                Target = Database.Items[17],
                Source = Database.Locations[4],
            };

            Objective objective21 = new Objective(false)
            {
                Type = ObjectiveType.Gather,
                Quantity = 1,
                Target = Database.Gatherables[6],
                Source = Database.Locations[9],
            };

            quest16.Objectives.Add(objective20);
            quest16.Objectives.Add(objective21);
            quest16.Rewards.Add(Database.Rewards[0]);
            quest16.RewardQuantities.Add(500);
            quest16.Rewards.Add(Database.Rewards[1]);
            quest16.RewardQuantities.Add(750);

    // Define a new quest titled "Nature's Wraith - Part I" which involves gathering ingredients for Pike Greenleaf, a Druid.
    // The quest is initiated by encountering Pike Greenleaf at Location 4.
    // If this quest is completed, it will unlock Quest 16.

    Quest quest15 = new Quest(false, true, () => _availableQuests.Add(quest16))
    {
        Status = QuestStatus.Available,
         QuestGiverLocation = Database.Locations[4],
        Title = "Nature's Wraith - Part I",
        Description = " HAIL ADVENTURERS! \n \n" +
         "A kobold sharman took me by surprise on my travels to the old temple within the forest. " +
        "The beings power was too much for myself, and I was lucky to get away. However, I want your help to get pay back. " +
        "I'm going to craft a potion which will boost intelligence, to give you an edge in the fight to come. But first i need a few ingridents. " +
        "Go to the caves and gather blood moss, and on your way back gather me some berries. You can find these in the bushes that grown around the fortress \n \n" +
        "Pike Greenleaf - Druid \n \n" 
    };

    // Define objectives for gathering ingredients.
    Objective objective18 = new Objective(false)
    {
        Type = ObjectiveType.Gather,
         Quantity = 5,
        Target = Database.Gatherables[5], // Blood Moss
        Source = Database.Locations[9], // Caves
    };

    Objective objective19 = new Objective(false)
    {
        Type = ObjectiveType.Gather,
         Quantity = 5,
        Target = Database.Gatherables[3], // Berries
        Source = Database.Locations[10], // Bushes around the fortress
    };

    // Add objectives to the quest.
    quest15.Objectives.Add(objective18);
    quest15.Objectives.Add(objective19);

    // Add rewards to the quest.
    quest15.Rewards.Add(Database.Rewards[0]); // Gold reward
    quest15.RewardQuantities.Add(300);
    quest15.Rewards.Add(Database.Rewards[1]); // Experience reward
    quest15.RewardQuantities.Add(350);

    // Add the quest to available quests.
    _availableQuests.Add(quest15);
        }
    }
    
    public void OnAcceptQuest(Quest quest)
    {
        _availableQuests.Remove(quest);
        quest.Status = QuestStatus.Accepted;
        AcceptedQuests.Add(quest);

        EventTitles.Run("QuestAccepted");
        SetActiveQuest(AcceptedQuests.Count - 1);
    }

    public void OnSetQuestActive(Quest quest)
    {
        SetActiveQuest(AcceptedQuests.IndexOf(quest));
    }

    public void OnCompleteQuest(Quest quest)
    {
        AcceptedQuests.Remove(quest);
        quest.Status = QuestStatus.Completed;
        CompletedQuests.Add(quest);

        for (int i = 0; i < quest.Rewards.Count; ++i)
        {
            quest.Rewards[i].Issue(quest.RewardQuantities[i]);
        }

        EventTitles.Run("QuestCompleted");
        SetActiveQuest(AcceptedQuests.Count - 1);
    }

    void SetActiveQuest(int index)
    {
        _activeQuestIndex = index;

        PlayerState.instance.PlayerStatePanel.OnActiveQuestChanged(
            _activeQuestIndex < 0
                ? null
                : AcceptedQuests[_activeQuestIndex]
        );

        PlayerState.instance.Actions.OnActionContextChanged();
    }

    public Quest GetActiveQuest()
    {
        if (_activeQuestIndex < 0 || AcceptedQuests.Count == 0)
        {
            return null;
        }

        return AcceptedQuests[_activeQuestIndex];
    }

    public bool IsQuestActive(Quest quest)
    {
        return _activeQuestIndex == AcceptedQuests.IndexOf(quest);
    }

    void ShowQuestList()
    {
        ExecuteEvents.Execute<ILeftPanelEvents>(GameObject.Find("LeftPanel"), null, (x, y) => x.OnShowPanel("QuestList"));
    }

    public void OnViewAvailableQuests()
    {
        ShowQuestList();
        QuestList.Refresh("Available Quests", _availableQuests);
    }

    public void OnViewAcceptedQuests()
    {
        ShowQuestList();
        QuestList.Refresh("Accepted Quests", AcceptedQuests);
    }

    public void OnViewCompletedQuests()
    {
        ShowQuestList();
        QuestList.Refresh("Completed Quests", CompletedQuests);
    }
}
