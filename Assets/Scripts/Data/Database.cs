using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public static class Database
{
    public static List<Location> Locations = new List<Location>()
    {
        new Location(
            "Outskirts of Town",
            "As your gaze spans the vast valley before you, you're greeted by a breathtaking panorama. " +
            "Towering mountains stand as sentinels in the distance, their snow-capped peaks piercing the azure sky. " +
            "Wisps of mist cascade down their slopes, creating an ethereal curtain that adds an air of mystique to the rugged terrain.\n\n" +
            "A long, winding river emerges from the heart of the mountains, its crystal-clear waters weaving a serpentine path through the valley. " +
            "The river's gentle current is both soothing and invigorating, reflecting the sunlight as it cascades over smooth stones and weaves through patches of vibrant wildflowers that line its banks.\n\n" +
            "At the valley's center lies the bustling town, nestled in the embrace of the surrounding hills. The town exudes an atmosphere of quaint charm, with cobblestone streets winding between sturdy stone buildings adorned with colorful banners. " +
            "The townsfolk go about their daily lives, tending to market stalls and engaging in friendly banter.\n\n" +
            "Dense woodlands to the north, conceal secrets within their ancient boughs. Amidst the towering trees, an old temple stands, its weathered spires reaching towards the heavens. " +
            "The temple exudes an aura of solemnity and antiquity, its walls adorned with faded murals that tell tales of forgotten deities.\n\n" +
            "Further to the west, a wooden fortress emerges from the heart of the forest, its watchtowers peering out like sentinels. The fortress serves as a protective bastion for the town, a strategic retreat in times of peril. " +
            "The wooden walls, reinforced with sturdy beams, create a formidable barrier against potential threats, while the banner of the town flutters proudly above the main gate.\n\n" +
            "As you take in the entirety of this picturesque valley, you sense a harmonious blend of nature, civilization, and preparedness. " +
            "It's a place where the town's heartbeat resonates with the pulse of the river, echoing the balance between the serene beauty of the valley and the potential dangers that lurk beyond its borders.",
            "StartingValley",
            new List<string>() { "player-start", "valley" }
        ),
        new Location(
            "Town Weaponsmith",
            "As you enter the town's weapon smith shop, the ringing of hammers on anvils and the rhythmic clanging of metal fill the air. " +
            "The distinct scent of heated iron and forging fires envelopes you, creating an atmosphere that resonates with the essence of craftsmanship. " +
            "The shop is dimly lit, with flickering torches casting dancing shadows upon the sturdy wooden walls adorned with various weapons and armor.\n\n" +
            "At the heart of the workshop stands Gotrek SteelAxe, the masterful dwarf weapon smith. Despite his grizzled appearance, his remaining eye glints with a keen intelligence and determination. " +
            "His long, braided beard, adorned with intricate metal clasps, brushes against the leather apron that shields him from the sparks flying in all directions.\n\n" +
            "Rows of gleaming weapons hang from the walls and dangle from the ceiling, each one a testament to Gotrek's skill. Massive warhammers, finely crafted axes, and razor-sharp swords are displayed alongside shields adorned with intricate dwarven motifs. " +
            "The clang of metal on metal harmonizes with Gotrek's deep, gravelly voice as he imparts wisdom to an apprentice, the rhythmic lesson punctuated by the occasional laugh or grunt.\n\n" +
            "Gotrek SteelAxe himself, despite his gruff exterior, greets customers with a genuine warmth. His patch over one eye tells tales of battles and hardships, but his craftsmanship speaks louder, inviting adventurers to peruse the arsenal of finely crafted weapons that stand ready to accompany them on their quests. " +
            "The town's weapon smith shop, with Gotrek at its helm, is not just a place to purchase arms; it's a living testament to the artistry and dedication of dwarven weapon crafting.",
            "weaponsmith1",
            new List<string>() { "weaponsmith", "town" }
        ),
        new Location(
            "Town Armoursmith",
            "As you step into the town's armor smith shop, the steady clinking of metal on metal resonates throughout the air. " +
            "The workshop is alive with the rhythmic sound of hammers shaping steel and the occasional hiss of cooling metal being quenched. " +
            "The aroma of heated iron and the smoky essence of forging permeate the space, creating an ambiance that speaks to the craft of armor making.\n\n" +
            "Standing tall at the heart of the shop is Grog Ironjaw, the formidable Goliath armor smith. His towering figure and chiseled features are softened by a genuine smile as he oversees the meticulous work of his apprentices. " +
            "Grog's large hands expertly manipulate pieces of metal, crafting armor with precision and care. A collection of intricate tattoos adorns his exposed arms, hinting at tribal affiliations.\n\n" +
            "Rows of gleaming suits of armor line the walls, each a testament to Grog's skill. Massive suits designed for Goliaths stand alongside finely detailed armor tailored for smaller folk. " +
            "Shields with resilient designs and helmets adorned with fearsome motifs are displayed with pride. The workshop resonates with the rhythmic clanking of metal as apprentices tirelessly work on new creations.\n\n" +
            "Grog Ironjaw, despite his imposing stature, greets customers with a warm demeanor. His gravelly voice resonates with tales of craftsmanship and battles, creating an atmosphere that invites patrons to explore the array of finely crafted armor. " +
            "The town's armor smith shop, under Grog's expert guidance, serves not only as a place to acquire protective gear but also as a living testament to the strength and artistry of Goliath armor crafting.",
            "armoursmith1",
            new List<string>() { "armoursmith", "town" }
        ),
        new Location(
            "Town Merchant Stalls",
            "As you stroll along the bustling street, a vibrant array of merchant stalls unfolds before you. The air is infused with a medley of scents – the enticing aroma of sizzling meats, the sweet fragrance of fresh fruits, and the earthy undertone of handcrafted wares. " +
            "The lively hubbub of merchants haggling and customers perusing creates a dynamic and bustling atmosphere.\n\n" +
            "Stalls adorned with colorful canopies offer an assortment of delectable treats. Roasted nuts, exotic spices, and succulent fruits are neatly arranged, enticing passersby with their vibrant displays. " +
            "Haggling merchants enthusiastically extol the virtues of their goods, inviting you to sample the diverse culinary delights.\n\n" +
            "Amidst the food stalls, skilled artisans showcase their handcrafted wares. Elaborate wooden utensils, intricately woven textiles, and finely sculpted trinkets are proudly displayed. " +
            "Craftsmen meticulously demonstrate their techniques, drawing in curious onlookers with the promise of quality, one-of-a-kind items.\n\n" +
            "Further down the bustling street, a makeshift potion stand captures attention. Vials filled with mysterious liquids, labeled with neatly written tags, shimmer in the sunlight. " +
            "The potion seller, with a weathered cloak and an air of arcane knowledge, beckons to those seeking magical remedies or elixirs to enhance their adventures.\n\n" +
            "As you navigate through the lively marketplace, the street becomes a tapestry of colors, sounds, and aromas. The merchant stalls, each telling a unique story, contribute to the vibrant energy of the town's commerce. " +
            "It's a place where the spirit of trade and the artistry of craftsmanship converge, inviting you to explore the diverse offerings that line the bustling street.",
            "merchantstall1",
            new List<string>() { "merchant", "town" }
        ),
        new Location(
            "Town Tavern",
            "As you swing open the sturdy tavern door, a wave of warmth and merriment washes over you. The air is thick with the savory aroma of roasted meats, spiced ales, and the faint hint of pipe smoke. " +
            "The flickering glow of oil lamps illuminates the cozy interior, revealing a labyrinth of wooden tables worn smooth by the countless tankards that have rested upon them.\n\n" +
            "At the heart of the bustling establishment stands Snorri Rockbitter, the hearty Dwarf owner of the tavern. His robust laughter echoes through the room as he tends to patrons with a welcoming grin. " +
            "Snorri's impressive beard, adorned with intricate braids, brushes against his polished armor, showcasing the pride he takes in both his establishment and his Dwarven heritage.\n\n" +
            "Amidst the lively crowd at the bar, a gnome dressed in a wolfen cloak stands out. The gnome's attire, resembling that of a druid, catches the eye amidst the usual patrons. " +
            "The gnome seems at ease, quietly observing the revelry while nursing a mug of mead.\n\n" +
            "The usual clientele, a motley crew of adventurers and locals, engage in lively banter and raucous laughter. A minstrel in the corner strums a merry tune, adding to the vibrant atmosphere. " +
            "The wooden beams overhead are adorned with a collection of peculiar artifacts, each telling a story of adventures past.\n\n" +
            "As you find a seat amidst the merry chaos, Snorri Rockbitter nods in greeting, a twinkle in his eye. The tavern, under his watchful gaze, serves not only as a place to indulge in food and drink but also as a haven for tales, camaraderie, and perhaps, unexpected encounters.",
            "tavern1",
            new List<string>() { "tavern", "town" }
        ),
        new Location(
            "Town Library",
            "As you push open the heavy oak doors, a wave of musty air, tinged with the scent of ancient parchment and aged wood, greets you. " +
            "The expansive town library unfolds before your eyes, shelves upon shelves of dusty tomes reaching towards a towering ceiling adorned with intricate murals depicting scholarly pursuits and magical realms.\n\n" +
            "Soft, ambient light filters through stained glass windows, casting a gentle glow upon worn wooden tables and plush reading chairs scattered throughout the grand hall. " +
            "The air is filled with the hushed whispers of scholars and the occasional scratch of quills against parchment as diligent scribes record their findings.\n\n" +
            "Elaborate tapestries adorn the walls, telling tales of forgotten civilizations and legendary wizards, adding a touch of historical richness to the scholarly atmosphere.\n\n" +
            "Lingering at the edges of the library, you notice secluded nooks and hidden alcoves where adventurers and wizards engrossed in their studies seek solace amidst the vast repository of knowledge. " +
            "The library stands as a sanctuary of wisdom, a place where the echoes of countless inquiries and discoveries resonate through the aged stones, inviting you to delve into the secrets and mysteries that await within its dusty pages.",
            "library1",
            new List<string>() { "library", "town" }
        ),
        new Location(
            "Town Mercenary Guild",
            "As you step through the imposing, iron-clad doors of the Mercenary Guild Hall, a rush of cool air laden with the scent of polished steel and faint echoes of battle envelops you. " +
            "The vast hall is alive with the clatter of armor, the ringing of swords being sharpened, and the hearty laughter of warriors swapping tales of conquest.\n\n" +
            "In the center of the hall, a massive wooden table serves as the war room, strewn with maps and battle plans. At its head sits Logan Wolfbane, the guild's formidable leader. " +
            "His weathered face and stern gaze reveal years of hard-fought experience, and the giant two-handed sword strapped to his back serves as a testament to his prowess.\n\n" +
            "Adorned with trophies and battle standards from various campaigns, the guild hall's walls echo with the history of victories and defeats. Rows of weapons line racks, each one telling a story of a battle fought and won. " +
            "Mercenaries clad in mismatched armor share drinks and engage in friendly bickering, while recruits undergo training under the watchful eyes of seasoned veterans.\n\n" +
            "The atmosphere is charged with an air of camaraderie and purpose, as mercenaries of all races and backgrounds gather under the same banner. A massive hearth roars with flames at one end of the hall, casting a warm glow upon the worn stone floor where a burly blacksmith works tirelessly on weapons and armor.\n\n" +
            "As you navigate through the bustling guild hall, Logan Wolfbane's presence commands respect, and his occasional nods to guild members indicate a silent understanding between warriors. " +
            "This is a place where contracts are forged, alliances are tested, and the clashing of steel is as much a part of the ambiance as the shared tales of valor. The Mercenary Guild Hall stands as a haven for those seeking fortune through the clash of arms, and Logan Wolfbane, with his imposing figure and legendary sword, embodies the very spirit of the guild.",
            "mercguild1",
            new List<string>() { "town" },
            new List<Action>() { new Action("Quest Board", ShowAvailableQuests )}
        ),
        new Location(
            "Forest",
            "Beyond the outskirts of town, a dense forest sprawls, its towering trees forming an imposing natural barrier. The forest expands in every direction, with paths weaving through the dense woodland in a labyrinthine pattern. The atmosphere within is both enchanting and ominous, the ancient trees creating a natural canopy that filters the sunlight, casting dappled shadows on the forest floor.\n\n" +
            "The paths through the forest, though well-worn, feel constricted as the thick foliage presses in from all sides. Merchants transporting goods, town militia patrolling the borders, and adventurers seeking untold mysteries all navigate these claustrophobic passages. The dense underbrush is teeming with life, the air alive with the rustle of leaves and the distant calls of unseen creatures.\n\n" +
            "As you traverse the winding paths, the forest unfolds its secrets. Ancient cobwebs cling to the trees, weaving a tapestry of long-forgotten stories. Vines hang like draperies, forming natural archways that lead to hidden clearings and forgotten ruins. The forest floor, soft with moss and fallen leaves, bears the imprint of countless footprints, both human and beast.\n\n" +
            "Mysteries and dangers lie within the heart of the woodland. Strange flora with vibrant hues line the path, their properties unknown. The air carries an arcane energy, hinting at the presence of mystical forces that have lingered for centuries. Yet, amidst the wonders, dangers lurk in the shadows – elusive creatures, ancient guardians, and the whispers of creatures that thrive in the secrecy of the dense forest.\n\n" +
            "The path that leads towards the mountains reveals an ascent into a deeper, more primeval section of the forest. As you climb, the density of cobwebs increases, and the air becomes thick with an otherworldly aura. The mysteries hidden within the ancient woodland are both alluring and foreboding, inviting adventurers to explore its depths and uncover the secrets that have long eluded the light of day.",

            "forest1",
            new List<string>() { "nature", "forest" }
        ),
        new Location(
            "Forest Temple",
            "In the heart of the northern forest, on the outskirts of town, lies the remnants of a once-sacred sanctuary – the Forest Temple. The air is thick with an ancient mystique as you approach the crumbling stone walls and dilapidated structures that bear the scars of a bygone era. The temple, once a haven for the druids of the region, now stands as a desolate ruin.\n\n" +
            "Nature's reclamation is evident, with vines and ivy reclaiming the stone surfaces. The temple's grandeur is a shadow of its former self, the intricate carvings and symbols now weathered and obscured by the passage of time. The haunting silence that permeates the air tells tales of a savage orc attack that unfolded a hundred years ago, leaving the temple abandoned and desolated.\n\n" +
            "The main hall, where druids once gathered to commune with the forces of nature, now echoes with emptiness. The altar, where rituals were once conducted, stands cracked and broken. Shafts of sunlight pierce through gaps in the ceiling, casting ethereal beams upon the moss-covered floor.\n\n" +
            "Despite its desolation, the Forest Temple still attracts adventurers seeking ancient knowledge or the remnants of forgotten treasures. The overgrown courtyard, now a tangle of wild flora, conceals hidden passages and forgotten chambers that beckon the curious and the brave.\n\n" +
            "The temple's tragic history is etched in the remnants of its stone walls, a silent testament to the druids who once revered this place. Yet, despite the desolation, a lingering energy, both melancholic and mystical, clings to the Forest Temple. It stands as a place frozen in time, waiting for those daring enough to explore its secrets or seeking refuge within its weathered walls.",
            "foresttemple1",
            new List<string>() { "nature", "forest" }
        ),
        new Location(
            "Mountain Cave",
            "As you ascend the rugged mountain path, you come across the foreboding entrance of a cave, its gaping maw inviting you into the depths of darkness. The air becomes heavy with an earthy scent, and an unsettling chill runs down your spine as you approach.\n\n" +
            "Upon entering, the oppressive darkness surrounds you, broken only by sporadic glimmers of faint light. The cave's interior is a labyrinthine network of winding tunnels and shadowy chambers. The dampness in the air clings to your skin, and the sound of water dripping from the ceiling echoes through the cavernous space.\n\n" +
            "The cave is not uninhabited. Sinister eyes gleam in the shadows, revealing another prescence, care will need to be taken in this dark place\n\n" +
            "A small ray of light penetrates the cave's ceiling, casting a feeble glow upon a section of the uneven floor. The meager light reveals the treacherous terrain and the myriad of menacing eyes that watch your every move. The cave is a realm of darkness and danger, a habitat for creatures that thrive in the shadows, and a chilling reminder of the perils that await within the mountainous depths.\n\n" +
            "As you press forward, the ominous sounds of echoing growls, skittering legs, and guttural goblin voices serve as a haunting symphony, underscoring the inherent peril of the mountain cave. Every step forward is a venture into the unknown, with danger lurking in the obsidian shadows.",
            "caves1",
            new List<string>() { "nature", "cave" }
        ),
        new Location(
            "Fortress",
            "Nestled on the western outskirts of town, a formidable wooden fortress rises from the edge of the dense forest. As you approach, the sturdy walls and towering structures loom overhead, projecting an air of strength and preparedness. The fortress serves as a vigilant guardian, tasked with the town's defense and safety.\n\n" +
            "A massive gate, reinforced with iron and adorned with the town's emblem, stands proudly at the entrance. Sentry towers flank the sides, providing a vantage point for vigilant guards to keep watch over the forest and the surrounding lands. The creaking of the gate and the occasional calls of stationed sentinels fill the air, emphasizing the fortress's role as a bastion of security.\n\n" +
            "Within the wooden walls, a bustling barracks houses the town militia. Soldiers clad in uniform armor move with purpose, engaged in drills and maintaining their equipment. The atmosphere is one of discipline and readiness, with the barracks serving as a hub for the defenders who stand ready to protect the town from any threat.\n\n" +
            "Towering above the barracks, a wizard tower stretches towards the sky. The tower's pinnacle is adorned with arcane symbols, indicating the presence of skilled spellcasters who contribute their magical prowess to the fortress's defenses. The occasional flicker of magical energy dances around the tower, hinting at the potent enchantments that safeguard the stronghold.\n\n" +
            "The fortress is not merely a passive guardian; it stands as a refuge in times of crisis. In the event of an attack on the town, the townsfolk can seek shelter within the sturdy walls, finding safety in the well-fortified structure. The wooden fortress, a testament to the town's commitment to defense, stands resolute against the backdrop of the sprawling forest, ready to repel any who would threaten the safety of the townsfolk.",
            "fortress1",
            new List<string>() { "nature", "plains" , "fortress"}
        ),
    };

    public static List<Location> GetAllLocationsByTag(string tag)
    {
        return Locations.FindAll(location => location.Tags.Contains(tag));
    }

    public static Location PickFirstLocationByTag(string tag)
    {
        var locations = GetAllLocationsByTag(tag);
        Assert.IsTrue(locations.Count > 0);

        return locations[0];
    }

    public static Location PickRandomLocationByTag(string tag)
    {
        var locations = GetAllLocationsByTag(tag);
        System.Random random = new System.Random();
        return locations[random.Next(locations.Count)];
    }

    public static List<NPC> NPCs = new List<NPC>()
    {
        new NPC("Gotrek Steelaxe", new List<string>() { "weaponsmith" }),
        new NPC("Grog Ironjaw", new List<string>() { "armoursmith" }),
        new NPC("Persie De'Rolo III", new List<string>() { "merchant" }),
        new NPC("Albus Merlin", new List<string>() { "library" }),
        new NPC("Snorri Rockbitter", new List<string>() { "tavern" }),
        new NPC("Irna Albrek", new List<string>() { "fortress" }),
        new NPC("Pike Greenleaf", new List<string>() { "tavern" }),
    };

    public static NPC PickRandomNPCNotHavingTag(string tag)
    {
        System.Random random = new System.Random();
        var npc = NPCs[random.Next(NPCs.Count)];
        while (npc.Tags.Contains(tag))
        {
            npc = NPCs[random.Next(NPCs.Count)];
        }
        
        return npc;
    }

    public static List<MobType> MobTypes = new List<MobType>()
    {
        new MobType("Goblins", new List<string>() { "forest", "cave" }),
        new MobType("Orcs", new List<string>() { "forest", "cave" }),
        new MobType("Wolves", new List<string>() { "forest", "cave" }),
        new MobType("Giant Spiders", new List<string>() { "forest", "cave" }),
        new MobType("Spider Matriarch", new List<string>() { "forest", "cave" }),
        new MobType("Orc Warboss", new List<string>() { "forest", "cave" }),
        new MobType("Kobolds", new List<string>() { "forest", "cave" }),
        new MobType("Kobold Shaman", new List<string>() { "forest" }),

    };

    public static List<int> KillQuantities = new List<int>()
    {
        1,
        3,
        5,
        10,
    };

    public static List<Item> Items = new List<Item>()
    {
        new Item("a dozen Steel Swords", new List<string>() { "weaponsmith" }, 300),
        new Item("an Iron Axe", new List<string>() { "weaponsmith" }, 300),
        new Item("a Steel Sword", new List<string>() { "weaponsmith" }, 400),
        new Item("a Steel Axe", new List<string>() { "weaponsmith" }, 400),
        new Item(" a dozen Chainmail Armour", new List<string>() { "armoursmith" }, 500),
        new Item("Leather Armour", new List<string>() { "armoursmith" }, 400),
        new Item("Platemail Armour", new List<string>() { "armoursmith" }, 600),
        new Item("a Health Potion", new List<string>() { "merchant" }, 100),
        new Item("a Mana Potion", new List<string>() { "merchant" }, 100),
        new Item("a Strength Potion", new List<string>() { "merchant" }, 100),
        new Item("a Dexterity Potion", new List<string>() { "merchant" }, 100),
        new Item("an Intelligence Potion", new List<string>() { "merchant" }, 100),
        new Item("Book of Beasts", new List<string>() {"library"}, 100),
        new Item ("Iron Ore", new List<string>() {"fortress"}, 100),
        new Item ("Giant Candle", new List<string>() {"cave", "forest"}, 100),
        new Item ("Barrels of Dwarven Stout", new List<string>() {"tavern"}, 300),
        new Item ("Steel Swords & Chain Mail Armours", new List<string>() {"town"}, 750),
        new Item ("Empty Vial", new List<string>() {"tavern"}, 100),
    };

    public static List<Gatherable> Gatherables = new List<Gatherable>()
    {
        new Gatherable("Copper Ore", new List<string>() { "cave" }),
        new Gatherable("Iron Ore", new List<string>() { "cave" }),
        new Gatherable("Logs", new List<string>() { "forest" }),
        new Gatherable("Berries", new List<string>() { "plains" }),
        new Gatherable("Nightshade", new List<string>() { "forest" }),
        new Gatherable("Blood Moss", new List<string>() { "forest", "cave" }),
        new Gatherable("Vial of Lightning", new List<string>() { "cave" }),
    };

    public static List<int> GatherableQuantities = new List<int>()
    {
        5,
        10,
        15
    };

    public static List<ObjectiveReason> ObjectiveReasons = new List<ObjectiveReason>()
    {
        new ObjectiveReason("to forge new equipment. I'm working on new equipment that will be better equip anyone. Do this task for me " +
        "and you shall be rewarded.", new List<string>() { "gather", "cave" }),
        new ObjectiveReason("for repairing a customer's equipment. A customer came in the other day none to happ as his equipment broke. I " +
        "need these last few things to get this equipment back in order.", new List<string>() { "gather", "cave" }),
        new ObjectiveReason("for repairs to the building. As you can see the building is falling apart. Please help me with this task" +
        "and i shall be greatly thankful for your help. ", new List<string>() { "gather", "forest" }),
        new ObjectiveReason("to make new stock. Our stock is running awefully low and no one is will to help. It would only take" +
        " a moment of your time. Please help me with this task.", new List<string>() { "gather", "forest" }),
        new ObjectiveReason("for a new potion recipe. This next batch of potions will cure you of anything so I'm told" +
        "Do this task for me and i shall be greatly appreciated.", new List<string>() { "gather", "plains" }),
        new ObjectiveReason("to make a salve. Our latest salve could only cure rashes, this next batch could actually save peoples' lifes" +
        " Do this for me and you shall be rewarded.", new List<string>() { "gather", "plains" }),
        new ObjectiveReason("to help defend the road after recent attacks. Attacks on the road have been recent and this will " +
        "help us defend ourselves that much better, by giving us the edge in future fights.", new List<string>() { "collect" }),
        new ObjectiveReason("as a gift for their son. Thier son has turn 13 and has asked for this gift for his birthday" +
        " please do this for me, and you will be rewarded. Don't let the parents down", new List<string>() { "collect" }),
        new ObjectiveReason("as a gift for their daughter. Thier daughter has turn 15 and has asked for this gift for her birthday" +
        " please do this for me, and you will be rewarded. Don't let the grandparents down." , new List<string>() { "collect" }),
        new ObjectiveReason("to fulfill a customer's order. A very important order has come in and is very beneficial to all involved. Please complete the order "
        + "so that this doesn't look bad on myself.", new List<string>() { "collect" }),
        new ObjectiveReason("As tranquility of the night was shattered by a brazen assault, leaving a supplier battered and  on the verge of death." +
        "Justice demands retribution for the harm inflicted upon anyone.", new List<string>() { "kill" }),
        new ObjectiveReason("after they stole a very valuable shipment dead of night. Go find this shipment and bring vengence on those " +
        "that have wrong us.", new List<string>() { "kill" }),
        new ObjectiveReason("after they killed my mother. I want vengenace. Go find these beings and bring them a swift death. " +
        "I can then bury my mother in peace.", new List<string>() { "kill" }),
        new ObjectiveReason("after they killed my father. I want vengenace. Go find these beings and bring them a swift death. " +
        "I can then bury my father in peace.", new List<string>() { "kill" }),
    };

    public static List<ObjectiveReason> GetAllObjectiveReasonsWithTags(List<string> tags)
    {
        return ObjectiveReasons.FindAll(reason => !reason.Tags.Except(tags).Any());
    }

    public static ObjectiveReason PickRandomObjectiveReasonByTags(List<string> tags)
    {
        var reasons = GetAllObjectiveReasonsWithTags(tags);
        System.Random random = new System.Random();
        return reasons[random.Next(reasons.Count)];
    }

    public static List<IReward> Rewards = new List<IReward>()
    {
        new GoldReward(),
        new XPReward(),
    };

    public static List<int> RewardQuantities = new List<int>()
    {
        100,
        150,
        250,
        300,
        350,
        500,
        750,
        900,
        1000
    };

    // Amount of XP required to level. Scales linearly (Level * BaseXPScale)
    public static int BaseXPScale = 1000;

    static void ShowAvailableQuests()
    {
        ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnViewAvailableQuests());
    }
}
