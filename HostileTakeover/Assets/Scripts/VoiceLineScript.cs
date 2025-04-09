using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineScript : MonoBehaviour
{
    public int character;
    public UIScript UI;

    public void GetEnemyCharacter(int _character)
    {
        character = _character;
    }

    public void EnemySpawned()
    {
        UI.DisplayPilot(character, 0);
    }

    public void EnemyHit()
    {
        UI.DisplayPilot(character, 1);
    }

    public void PlayerHit()
    {
        UI.DisplayPilot(character, 2);
    }

    public void PlayerDeath()
    {
        UI.DisplayPilot(character, 3);
    }
}
