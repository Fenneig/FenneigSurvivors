﻿using FenneigSurvivors.Scripts.Visual;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Objects
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public HpBarView HpBarView { get; private set; }
        [field: SerializeField] public XpView XpView { get; private set; }
    }
}
