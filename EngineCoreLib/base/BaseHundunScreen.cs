using hundun.unitygame.gamelib;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace hundun.unitygame.enginecorelib
{
    public abstract class BaseHundunScreen<T_GAME, T_SAVE> where T_GAME : BaseHundunGame<T_GAME, T_SAVE>
    {

        // ------ unity adapter member ------
        public T_GAME game;

        // ------ lazy init ------
        public LogicFrameHelper logicFrameHelper;

        public BaseHundunScreen(T_GAME game)
        {
            this.game = game;
        }



        public abstract void show();

        virtual public void render(float delta)
        {

            if (logicFrameHelper != null)
            {
                bool isLogicFrame = logicFrameHelper.logicFrameCheck(delta);
                if (isLogicFrame)
                {
                    onLogicFrame();
                }
            }

            gameObjectDraw(delta);
            renderPopupAnimations(delta);
        }

        virtual protected void onLogicFrame()
        {
            // base-class do nothing
        }

        virtual protected void renderPopupAnimations(float delta)
        {
            // base-class do nothing
        }

        virtual protected void gameObjectDraw(float delta)
        {
            // base-class do nothing
        }
    }

}
