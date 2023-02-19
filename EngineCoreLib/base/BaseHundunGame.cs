using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using UnityEngine;

namespace hundun.unitygame.enginecorelib
{
    public abstract class BaseHundunGame<T_GAME, T_SAVE> where T_GAME : BaseHundunGame<T_GAME, T_SAVE>
    {
        public static T_GAME INSTANCE { get; }

        protected readonly int constMainViewportWidth;
        protected readonly int constMainViewportHeight;


        // ------ init in createStage1(), or keep null ------
        public AbstractSaveHandler<T_SAVE> saveHandler { get; protected set; }
        public UnityFrontend frontend { get; private set; }

        public BaseHundunGame(int viewportWidth, int viewportHeight)
        {
            this.constMainViewportWidth = viewportWidth;
            this.constMainViewportHeight = viewportHeight;
            this.frontend = new UnityFrontend();
        }

        /**
         * 只依赖Gdx static的成员
         */
        protected virtual void createStage1()
        {
            this.saveHandler.lazyInitOnGameCreate();
        }
        /**
         * 只依赖Stage1的成员
         */
        protected abstract void createStage2();
        /**
         * 自由依赖
         */
        protected abstract void createStage3();


        public void create()
        {
            createStage1();
            createStage2();
            createStage3();
        }


        // ====== ====== ======



        public void dispose()
        {

        }



        public int getWidth()
        {
            return constMainViewportWidth;
        }


        public int getHeight()
        {
            return constMainViewportHeight;
        }
    }

}
