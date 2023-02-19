using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hundun.unitygame.enginecorelib
{
    public abstract class StarterPlayScreen<T_GAME, T_SAVE> : BaseHundunScreen<T_GAME, T_SAVE> where T_GAME : BaseHundunGame<T_GAME, T_SAVE>
    {

        public String area { get; private set; }
        private readonly String startArea;

        protected List<ILogicFrameListener> logicFrameListeners = new List<ILogicFrameListener>();
        protected List<IGameAreaChangeListener> gameAreaChangeListeners = new List<IGameAreaChangeListener>();


        public StarterPlayScreen(T_GAME game, String startArea,
                int LOGIC_FRAME_PER_SECOND
                ) : base(game)
        {
            this.startArea = startArea;
            this.logicFrameHelper = new LogicFrameHelper(LOGIC_FRAME_PER_SECOND);
        }

        public void setAreaAndNotifyChildren(String current)
        {
            String last = this.area;
            this.area = current;

            foreach (IGameAreaChangeListener gameAreaChangeListener in gameAreaChangeListeners)
            {
                gameAreaChangeListener.onGameAreaChange(last, current);
            }

        }


        override public void show()
        {

            lazyInitBackUiAndPopupUiContent();

            lazyInitUiRootContext();

            lazyInitLogicContext();

            // start area
            setAreaAndNotifyChildren(startArea);
            game.frontend.log(this.getClass().getSimpleName(), "show done");
        }


        protected abstract void lazyInitLogicContext();

        protected abstract void lazyInitUiRootContext();

        protected abstract void lazyInitBackUiAndPopupUiContent();

        override protected void onLogicFrame()
        {
            base.onLogicFrame();

            foreach (ILogicFrameListener logicFrameListener in logicFrameListeners)
            {
                logicFrameListener.onLogicFrame();
            }
        }
    }
}


