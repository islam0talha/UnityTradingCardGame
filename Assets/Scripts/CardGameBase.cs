using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CardGameBase : MonoBehaviour {
    public string _name = "";
}
public class AIGameState
{
    public AIGameState ParentState;
    public List<AIGameState> ChildsStatus;

    public HeroBehaviourScript PlayerHero;
    //public List<CardBehaviourScript> PlayerHandCards;// = new List<CardBehaviourScript>();
    public List<CardBehaviourScript> PlayerTableCards;// = new List<CardBehaviourScript>();

    public HeroBehaviourScript AIHero;
    public List<CardBehaviourScript> AIHandCards;// = new List<CardBehaviourScript>();
    public List<CardBehaviourScript> AITableCards;// = new List<CardBehaviourScript>();

    public int maxMana;
    public int PlayerMana;
    public int AIMana;

    public BoardBehaviourScript.Turn turn;

    public float State_Score = 0;
    float Attackweight = 1;
    float Healthweight = 1;
    float Manaweight = 1;
    float HeroHealthweight = 0.1f;

    #region Constructors 
    public AIGameState(
        //List<CardBehaviourScript> PlayerHand,
        List<CardBehaviourScript> PlayerTable,
        List<CardBehaviourScript> AIHand,
        List<CardBehaviourScript> AITable,
        HeroBehaviourScript _PlayerHero,
        HeroBehaviourScript _AIHero,
        int _MaxMana,
        int _PlayerMana,
        int _AIMana,
        BoardBehaviourScript.Turn _Turn
        )
    {
        //PlayerHandCards = GenericCopier <List<CardBehaviourScript>>.DeepCopy(PlayerHand);
        PlayerTableCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(PlayerTable);
        PlayerHero = _PlayerHero.Clone() as HeroBehaviourScript;

        AIHandCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(AIHand);
        AITableCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(AITable);
        AIHero = _AIHero.Clone() as HeroBehaviourScript;
        Calculate_State_Score();
    }
    public AIGameState(
        //List<GameObject> PlayerHand,
         List<GameObject> PlayerTable,
         List<GameObject> AIHand,
         List<GameObject> AITable,
         HeroBehaviourScript _PlayerHero,
         HeroBehaviourScript _AIHero,
         int _MaxMana,
         int _PlayerMana,
         int _AIMana,
         BoardBehaviourScript.Turn _Turn
        )
    {
        //List<CardBehaviourScript> _tempPlayerHand = new List<CardBehaviourScript>();
        //foreach (var item in PlayerHand)_tempPlayerHand.Add( item.GetComponent<CardBehaviourScript>());
        //PlayerHandCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(_tempPlayerHand);

        List<CardBehaviourScript> _tempPlayerTable = new List<CardBehaviourScript>();
        foreach (var item in PlayerTable) _tempPlayerTable.Add(item.GetComponent<CardBehaviourScript>());
        PlayerTableCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(_tempPlayerTable);

        PlayerHero = _PlayerHero.Clone() as HeroBehaviourScript;


        List<CardBehaviourScript> _tempAIHand = new List<CardBehaviourScript>();
        foreach (var item in AIHand) _tempAIHand.Add(item.GetComponent<CardBehaviourScript>());
        AIHandCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(_tempAIHand);

        List<CardBehaviourScript> _tempAITable = new List<CardBehaviourScript>();
        foreach (var item in AITable) _tempAITable.Add(item.GetComponent<CardBehaviourScript>());
        AITableCards = GenericCopier<List<CardBehaviourScript>>.DeepCopy(_tempAITable);

        AIHero = _AIHero.Clone() as HeroBehaviourScript;
        Calculate_State_Score();
    }
    #endregion

    #region Calculate Score
    float Calculate_State_Score()
    {
        float AI_Table_Score=0;

        foreach (CardBehaviourScript Card in AITableCards)
        {
            AI_Table_Score += Card._Attack * Attackweight + Card.health * Healthweight;
        }

        float Player_Table_Score = 0;
        foreach (CardBehaviourScript Card in PlayerTableCards)
        {
            Player_Table_Score -= Card._Attack * Attackweight + Card.health * Healthweight;
        }

        float AI_Hand_Score = 0;//Depend On Mana
        foreach (CardBehaviourScript Card in AIHandCards)
        {
            AI_Hand_Score += Card.mana * Manaweight;
        }

        float Player_Health_Score = 0;
        if (PlayerHero.health <= 0) Player_Table_Score = int.MaxValue;
        else
        Player_Health_Score -= PlayerHero.health * HeroHealthweight;


        float AI_Health_Score = 0;
        if (AIHero.health <= 0) AI_Health_Score = int.MinValue;
        else
        AI_Health_Score += AIHero.health * HeroHealthweight;

        State_Score = AI_Table_Score+ Player_Table_Score+ AI_Hand_Score+ Player_Health_Score+ AI_Health_Score;
        return State_Score;
    }
    #endregion

    public void GetAllPlacingAction()
    {
        if (turn == BoardBehaviourScript.Turn.AITurn)
        {
            if(AIHandCards.Count==0)
            {
                //EndTurn Nothing To Play
            }
            else
            {
                ///Generate All Possible Placing
                
            }
        }
    }
    public void GetAllAttackingActions()
    {

    }

    //Save Context
    //Evaluate State Score
    //
}
public static class GenericCopier<T>
{
    public static T DeepCopy(object objectToCopy)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, objectToCopy);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
