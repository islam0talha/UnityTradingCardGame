A Unity C# trading card game (similar to Hearthstone) 

You can run the game directly from unity.

Find the Game Object &quot;Board&quot; you can specify the AI level

Level 0:

Random decision making

Level 1:

The AI will start to generate tree for depth 1 and select the best move.

Level 2:

The AI will start to generate tree for depth 2 and try to make some compos attacks and the time for thinking will be long as you level up the AI

To play the game you will start with 1 mana and you must drag the card till the center of the Board.

### **Preparation**

| **Rule** |
| --- |
| Each player starts the game with 30 _Health_ and 0 _Mana_ slots. |
| Each player starts with a deck of 24 _Monster_ cards and 4 Magic cards |
| From the deck each player receives 3 random cards has his initial hand. |
| You are the _active player_. The _AI_  draws a 4th card from his deck to compensate him for not playing the first turn. |

### ** Basic Gameplay**

| **Step** | **Rule** |
| --- | --- |
| 1. | The active player receives 1 Mana slot up to a maximum of 10 total slots. |
| 2. | The active player&#39;s empty Mana slots are refilled. |
| 3. | The active player draws a random card from his deck. |
| 4. | The active player can play as many cards as he can afford. Any played card empties Mana slots and deals immediate damage to the opponent player equal to its Mana cost. |
| 5. | If the opponent player&#39;s Health drops to or below zero the active player wins the game. |
| 6. | If the active player can&#39;t (by either having no cards left in his hand or lacking sufficient Mana to pay for any hand card) or simply doesn&#39;t want to play another card, the opponent player becomes active. |

### **Healing**

| **Rule** |
| --- |
| When playing a card the active player can choose to use it for causing damage or for _healing himself_  |
| Players cannot heal up above 30 health. |

### **Minions**

| **Rule** |
| --- |
| Let players choose to play cards either as immediate damage _Attacks_ or as _Minions_ that are put on the board instead |
| Sleeping Minions will defend themselves in the same way when attacked by another Minion. |
| When a Minions health drops to or below zero it is removed from the board. |
