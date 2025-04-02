using System.Collections.Generic;
using UnityEngine;

public class ObjectDescriptions 
{
    private static readonly Dictionary<string, string> descriptions = new Dictionary<string, string>{
        // THIS IS WHERE YOU DEFINE THE OBJECT AND ITS DESCRIPTION
        {"Key", "This key looks like it could unlock something."},
        {"Key 2", "This key is useless"},
        {"Painting","This was painted by Mrs. Magnate... I miss the smell of fresh oil and varnish. \nI wish I know where she went."},
        {"FictionShelf","This shelf is full of fiction books. \nMr. Magnate kept all of these works of fictions to read to the kids... I think its missing something."},
        {"FictionShelfComplete","This should be the last book in the shelf... Ready for a good read before putting the kids to bed."},
        {"MurderMystery","Mr. Magnate's collection of murder mystery novels goes here... I never was too fond of these."},
        {"MurderMysteryComplete","The collection is complete now... though I still don't understand why anyone would enjoy reading about such gruesome things."},
        {"Ladder","It seems I was out of commision for so long my pneumatics are not what they used to be... I will stick to the mess on the ground floor for the time being."},
        {"Romance", "Mrs. Magnate was always reading these romance books... Her favorite is missing, I hope I can find it before she comes in for her afternoon reading session."},
        {"RomanceComplete", "Mrs. Magante should be able to find her favorite book much easier now! My job here is done."},        
        {"Textbooks","Mr. Magnate's textbooks. Stacks of engineering manuials, physics compendium... he always had a mind for machines and innovation."},
        {"TextbooksComplete", "The collection is now perfectly organized, just the way Mr. Magnate liked it."},
        {"ScienceFiction","A collection of science fiction novels, brimming with tales of advanced technology and distant worlds. Mr. Magnate often mused about how science was catching up to these novels."},
        {"ScienceFictionComplete", "The collection is complete. Mr. Magnate would have loved to see how these stories mirror reality more and more each day."},
        {"History","Volumes of historical biographies line this shelf. Mr. Magnate admired the great engineers and inventors of the past—Da Vinci, Tesla, Brunel—learning from their successes."},
        {"HistoryComplete","The collection is now in order. A timeline of human ingenuity, just as Mr. Magnate liked it."},
        {"RomanceNovel","Mrs. Magnates favorite romance novel... Let me put it back so she doesn't notice."},
        {"StoryBook","The Magnates always read the Odyssey to the kids, they sure loved their mythology."},
        {"Textbook","A single worn textbook, filled with notes and annotations. Mr. Magnate must have studied this one extensively."},
        {"MysteryNovel","Another tale of crime and intrigue... Something about these books unsettles me. Why did Mr. Magnate enjoy them so much?"},
        {"SciFiBook","A story of distant worlds and impossible machines... Mr. Magnate always said science was catching up, but I wonder—should it?"},
        {"HistoryBook","A biography of a great mind from the past... Mr. Magnate always said we had much to learn from them."}
        // AND SO O,N... WHERE THE KEY IS THE NAME OF THE OBJECT AND THE VALUE IS THE DESCRIPTION
    };
    public static string GetDescription(string key)
    {
        if (descriptions.ContainsKey(key))
        {
            return descriptions[key];
        }
        return null;
    }

}
