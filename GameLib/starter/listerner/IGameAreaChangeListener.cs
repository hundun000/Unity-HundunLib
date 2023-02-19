using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hundun.unitygame.gamelib
{
    public interface IGameAreaChangeListener
    {
        void onGameAreaChange(String last, String current);
    }

}
