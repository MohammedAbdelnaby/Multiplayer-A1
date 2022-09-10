
/*
This RPG data streaming assignment was created by Fernando Restituto.
Pixel RPG characters created by Sean Browning.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


#region Assignment Instructions

/*  Hello!  Welcome to your first lab :)

Wax on, wax off.

    The development of saving and loading systems shares much in common with that of networked gameplay development.  
    Both involve developing around data which is packaged and passed into (or gotten from) a stream.  
    Thus, prior to attacking the problems of development for networked games, you will strengthen your abilities to develop solutions using the easier to work with HD saving/loading frameworks.

    Try to understand not just the framework tools, but also, 
    seek to familiarize yourself with how we are able to break data down, pass it into a stream and then rebuild it from another stream.


Lab Part 1

    Begin by exploring the UI elements that you are presented with upon hitting play.
    You can roll a new party, view party stats and hit a save and load button, both of which do nothing.
    You are challenged to create the functions that will save and load the party data which is being displayed on screen for you.

    Below, a SavePartyButtonPressed and a LoadPartyButtonPressed function are provided for you.
    Both are being called by the internal systems when the respective button is hit.
    You must code the save/load functionality.
    Access to Party Character data is provided via demo usage in the save and load functions.

    The PartyCharacter class members are defined as follows.  */

public partial class PartyCharacter
{
    public int classID;

    public int health;
    public int mana;

    public int strength;
    public int agility;
    public int wisdom;

    public LinkedList<int> equipment;

}


/*
    Access to the on screen party data can be achieved via …..

    Once you have loaded party data from the HD, you can have it loaded on screen via …...

    These are the stream reader/writer that I want you to use.
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader

    Alright, that’s all you need to get started on the first part of this assignment, here are your functions, good luck and journey well!
*/


#endregion


#region Assignment Part 1

static public class AssignmentPart1
{

    static public void SavePartyButtonPressed()
    {
        StreamWriter File = new StreamWriter("Save File.txt");

        foreach (PartyCharacter pc in GameContent.partyCharacters)
        {
            // writing all the player stats in one line so we would be easier read and the "," will be splitting the stats
            File.Write(pc.classID.ToString() + "," + pc.health.ToString() + "," + pc.mana.ToString() +"," + pc.strength.ToString() + "," + pc.agility.ToString() + "," + pc.wisdom.ToString() + ",");
            int count = pc.equipment.Count;
            //writes the players equipment on the same line as the stats
            for (int i = 0; i < count; i++)
            {
                File.Write(pc.equipment.First.Value);
                File.Write(",");
                pc.equipment.RemoveFirst();

            }
            //new line for the next player
            File.WriteLine();
            Debug.Log("PC class id == " + pc.classID);

        }
        File.Close();
    }

    static public void LoadPartyButtonPressed()
    {

        GameContent.partyCharacters.Clear();

        GameContent.partyCharacters = new LinkedList<PartyCharacter>();


        string[] lines = System.IO.File.ReadAllLines(@"E:\Unity\Multiplayer-A1\Multiplayer-A1\Save File.txt");
        //each line is a player stats
        foreach (string line in lines)
        {
            PartyCharacter pc = new PartyCharacter();
            GameContent.partyCharacters.AddLast(pc);
            //splits the player stats in a array ("1,2,3,4,5,.." -> ["1", "2", "3", ...])
            string[] stats = line.Split(',');
            pc.classID = Convert.ToInt32(stats[0]);
            pc.health = Convert.ToInt32(stats[1]);
            pc.mana = Convert.ToInt32(stats[2]);
            pc.strength = Convert.ToInt32(stats[3]);
            pc.agility = Convert.ToInt32(stats[4]);
            pc.wisdom = Convert.ToInt32(stats[5]);
            // there is 6 stats so the rest will be equipment so i start a loop at 6th spot of the array
            for (int i = 6; i < stats.Length; i++)
            {
                //at the end of the stats line is "," and when slip it will be come "" so adding "" to equipment will crash the code
                if (stats[i] == "")
                {
                    break;
                }
                pc.equipment.AddFirst(Convert.ToInt32(stats[i]));
            }

        }

        GameContent.RefreshUI();

    }

}


#endregion


#region Assignment Part 2

//  Before Proceeding!
//  To inform the internal systems that you are proceeding onto the second part of this assignment,
//  change the below value of AssignmentConfiguration.PartOfAssignmentInDevelopment from 1 to 2.
//  This will enable the needed UI/function calls for your to proceed with your assignment.
static public class AssignmentConfiguration
{
    public const int PartOfAssignmentThatIsInDevelopment = 1;
}

/*

In this part of the assignment you are challenged to expand on the functionality that you have already created.  
    You are being challenged to save, load and manage multiple parties.
    You are being challenged to identify each party via a string name (a member of the Party class).

To aid you in this challenge, the UI has been altered.  

    The load button has been replaced with a drop down list.  
    When this load party drop down list is changed, LoadPartyDropDownChanged(string selectedName) will be called.  
    When this drop down is created, it will be populated with the return value of GetListOfPartyNames().

    GameStart() is called when the program starts.

    For quality of life, a new SavePartyButtonPressed() has been provided to you below.

    An new/delete button has been added, you will also find below NewPartyButtonPressed() and DeletePartyButtonPressed()

Again, you are being challenged to develop the ability to save and load multiple parties.
    This challenge is different from the previous.
    In the above challenge, what you had to develop was much more directly named.
    With this challenge however, there is a much more predicate process required.
    Let me ask you,
        What do you need to program to produce the saving, loading and management of multiple parties?
        What are the variables that you will need to declare?
        What are the things that you will need to do?  
    So much of development is just breaking problems down into smaller parts.
    Take the time to name each part of what you will create and then, do it.

Good luck, journey well.

*/

static public class AssignmentPart2
{

    static public void GameStart()
    {

        GameContent.RefreshUI();

    }

    static public List<string> GetListOfPartyNames()
    {
        return new List<string>() {
            "sample 1",
            "sample 2",
            "sample 3"
        };

    }

    static public void LoadPartyDropDownChanged(string selectedName)
    {
        GameContent.RefreshUI();
    }

    static public void SavePartyButtonPressed()
    {
        GameContent.RefreshUI();
    }

    static public void NewPartyButtonPressed()
    {

    }

    static public void DeletePartyButtonPressed()
    {

    }

}

#endregion


