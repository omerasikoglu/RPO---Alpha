﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [SerializeField] private ResourcesUI resourcesUI;
    [SerializeField] private PlayerController player;
    [SerializeField] private SkillTreeUI skillTreeUI;

    private void Awake()
    {
        StarSystem starSystem = new StarSystem();
        //resourcesUI.SetStarSystem(starSystem);


        
    }
    private void Start()
    {
        skillTreeUI.SetPlayerSkills(player.GetPlayerSkills());

    }
}
