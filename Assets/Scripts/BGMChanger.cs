﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour {

    public AudioClip calmBGM, actionBGM;

    //load the new music files
    void Awake() {
        if (calmBGM != null && actionBGM != null)
        {
            GameController.instance.calmBgm = this.calmBGM;
            GameController.instance.actionBGM = this.actionBGM;
            GameController.instance.LevelMusicChanged();
        }
	}
}
