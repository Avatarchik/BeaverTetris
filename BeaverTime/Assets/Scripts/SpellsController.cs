﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class SpellsController : MonoBehaviour {

    public GameObject[] spells;
    GamePlayerDataController _gamePlayerData;

    void Start ()
    {
        _gamePlayerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;
    }

    void Update () {

	}

    public void updateWithGameTime()
    {
        foreach(GameObject spellContainer in spells)
        {
            SpellCountdownController sepllCountdown = spellContainer.GetComponent<SpellCountdownController>();
            sepllCountdown.updateWithGameTime();
        }
    }

    public void removeCurrentShapeSpell()
    {

        useSpellForIndex(0);
    }

    public void removeRandomBlocksSpell()
    {
        useSpellForIndex(1);
    }

    public void firestormSpell()
    {
        useSpellForIndex(2);
    }

    public void cohessionSpell()
    {
        useSpellForIndex(3);
    }

    void useSpellForIndex(int aSpellIndex)
    {
        if (aSpellIndex < _gamePlayerData.getPlayerSpellsCount())
        {
            GameObject spellContainer = spells[aSpellIndex];
            
            SpellCountdownController spellCountdown = spellContainer.GetComponent<SpellCountdownController>();
            if(spellCountdown.availableSpell())
            {
                ISpell spell = spellContainer.GetComponent<ISpell>();
                spell.useSpell();
                spellCountdown.useSpell();
            }
            
        }
    }

}
