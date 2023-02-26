using hundun.unitygame.gamelib;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace hundun.unitygame.enginecorelib
{
    public abstract class BaseHundunScreen<T_GAME, T_SAVE> : MonoBehaviour where T_GAME : BaseHundunGame<T_GAME, T_SAVE>
    {
        protected void Start()
        {
            this.show();
        }

        protected void Update()
        {
            float delta = Time.deltaTime;
            this.render(delta);

        }

        void OnApplicationQuit()
        {
            game.dispose();
        }


        // ------ unity adapter member ------
        public T_GAME game;

        // ------ lazy init ------
        public LogicFrameHelper logicFrameHelper;

        virtual public void postMonoBehaviourInitialization(T_GAME game)
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
