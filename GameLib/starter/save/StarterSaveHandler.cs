using hundun.unitygame.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace hundun.unitygame.gamelib
{
    /**
 * 
 */
    public abstract class StarterSaveHandler<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> : AbstractSaveHandler<T_ROOT_SAVE>
    {

        private Boolean gameSaveInited = false;
        private Boolean systemSettingInited = false;
        private readonly List<ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>> subGameplaySaveHandlers = new List<ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>>();
        private readonly List<ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>> subSystemSettingSaveHandlers = new List<ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>>();
        private readonly IFactory<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> factory;

        public StarterSaveHandler(
                IFrontend frontEnd,
                IFactory<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> factory,
                ISaveTool<T_ROOT_SAVE> saveTool
                ) : base(frontEnd, saveTool)
        {
            this.factory = factory;
        }


        override protected void applySystemSetting(T_ROOT_SAVE rootSaveData)
        {
            systemSettingInited = true;
            if (factory.getSystemSave(rootSaveData) != null)
            {
                subSystemSettingSaveHandlers.ForEach(it => it.applySystemSetting(factory.getSystemSave(rootSaveData)));
            }
        }


        override protected T_ROOT_SAVE currentSituationToSaveData()
        {
            T_GAMEPLAY_SAVE gameplaySave;
            if (gameSaveInited)
            {
                gameplaySave = factory.newGameplaySave();
                subGameplaySaveHandlers.ForEach(it => it.currentSituationToSaveData(gameplaySave));
            }
            else
            {
                gameplaySave = default(T_GAMEPLAY_SAVE);
            }

            T_SYSTEM_SAVE systemSettingSave;
            if (systemSettingInited)
            {
                systemSettingSave = factory.newSystemSave();
                subSystemSettingSaveHandlers.ForEach(it => it.currentSituationToSystemSetting(systemSettingSave));
            }
            else
            {
                systemSettingSave = default(T_SYSTEM_SAVE);
            }
            return factory.newRootSave(
                    gameplaySave,
                    systemSettingSave
                    );
        }


        override protected void applyGameSaveData(T_ROOT_SAVE rootSaveData)
        {
            gameSaveInited = true;
            if (factory.getGameplaySave(rootSaveData) != null)
            {
                subGameplaySaveHandlers.ForEach(it => it.applyGameSaveData(factory.getGameplaySave(rootSaveData)));
            }
        }

        


        override public void registerSubHandler(Object objecz)
        {
            if (objecz is ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>)
            {
                subGameplaySaveHandlers.Add((ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>)objecz);
                frontEnd.log(this.getClass().getSimpleName(), objecz.getClass().getSimpleName() + " register as " + "ISubGameplaySaveHandler");
            }
            if (objecz is ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>)
            {
                subSystemSettingSaveHandlers.Add((ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>)objecz);
                frontEnd.log(this.getClass().getSimpleName(), objecz.getClass().getSimpleName() + " register as " + "ISubSystemSettingSaveHandler");
            }
        }
    }

    public interface IFactory<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE>
    {

        // delegate getter
        T_SYSTEM_SAVE getSystemSave(T_ROOT_SAVE rootSaveData);
        T_GAMEPLAY_SAVE getGameplaySave(T_ROOT_SAVE rootSaveData);

        // delegate constructor
        T_ROOT_SAVE newRootSave(T_GAMEPLAY_SAVE gameplaySave, T_SYSTEM_SAVE systemSettingSave);
        T_GAMEPLAY_SAVE newGameplaySave();
        T_SYSTEM_SAVE newSystemSave();

    }

    public interface ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>
    {
        void applyGameSaveData(T_GAMEPLAY_SAVE gameplaySave);
        void currentSituationToSaveData(T_GAMEPLAY_SAVE gameplaySave);
    }

    public interface ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>
    {
        void applySystemSetting(T_SYSTEM_SAVE systemSettingSave);
        void currentSituationToSystemSetting(T_SYSTEM_SAVE systemSettingSave);
    }
}

