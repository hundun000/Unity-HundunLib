using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hundun.unitygame.gamelib
{
    public interface ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>
    {
        void applyGameplaySaveData(T_GAMEPLAY_SAVE gameplaySave);
        void currentSituationToGameplaySaveData(T_GAMEPLAY_SAVE gameplaySave);
    }

    public interface ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>
    {
        void applySystemSetting(T_SYSTEM_SAVE systemSettingSave);
        void currentSituationToSystemSetting(T_SYSTEM_SAVE systemSettingSave);
    }
    public abstract class PairChildrenSaveHandler<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> : AbstractSaveHandler<T_ROOT_SAVE>
    {
        private Boolean gameSaveDirty = false;
        T_SYSTEM_SAVE starterRootSaveDataAsSystemSaveDataCache;
        List<T_GAMEPLAY_SAVE> starterRootSaveDataAsGameplaySaveDataCache;
        T_ROOT_SAVE fileRootSaveDataCache;
        Boolean _hasContinuedGameplaySave;
        private readonly List<ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>> subGameplaySaveHandlers = new();
        private readonly List<ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>> subSystemSettingSaveHandlers = new();
        private readonly IRootSaveExtension<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> rootSaveExtension;

        public PairChildrenSaveHandler(
                IFrontend frontend,
                IRootSaveExtension<T_ROOT_SAVE, T_SYSTEM_SAVE, T_GAMEPLAY_SAVE> factory,
                ISaveTool<T_ROOT_SAVE> saveTool
                ) : base(frontend, saveTool)
        {
            this.rootSaveExtension = factory;
        }

        override public void lazyInitOnGameCreate()
        {
            base.lazyInitOnGameCreate();
            if (saveTool.hasRootSave())
            {
                fileRootSaveDataCache = saveTool.readRootSaveData();
            }
            else
            {
                fileRootSaveDataCache = default(T_ROOT_SAVE);
            }
            var allStarterRootSaveData = this.genereateStarterRootSaveData();
            this.starterRootSaveDataAsSystemSaveDataCache = rootSaveExtension.getSystemSave(allStarterRootSaveData.get(0));
            allStarterRootSaveData.RemoveAt(0);
            this.starterRootSaveDataAsGameplaySaveDataCache = allStarterRootSaveData.Select(it => rootSaveExtension.getGameplaySave(it)).ToList();

            this._hasContinuedGameplaySave = fileRootSaveDataCache != null && rootSaveExtension.getGameplaySave(fileRootSaveDataCache) != null;
        }

        override public void systemSettingLoadOrStarter()
        {

            T_SYSTEM_SAVE systemSave;
            if (saveTool.hasRootSave())
            {
                systemSave = rootSaveExtension.getSystemSave(fileRootSaveDataCache);
            }
            else
            {
                systemSave = starterRootSaveDataAsSystemSaveDataCache;
            }

            if (systemSave != null)
            {
                subSystemSettingSaveHandlers.ForEach(it => it.applySystemSetting(systemSave));
            }
            frontend.log(this.getClass().getSimpleName(), "systemSettingLoadOrStarter call");
        }

        /**
         * must after systemSettingLoadOrStarter(), and rootSaveDataCache from LoadOrStarter will be not null.
         */
        override public Boolean hasContinuedGameplaySave()
        {
            return _hasContinuedGameplaySave;
        }
        /**
         * must after systemSettingLoadOrStarter(), and rootSaveDataCache from LoadOrStarter will be not null.
         */
        override public void gameplayLoadOrStarter(int starterIndex)
        {
            gameSaveDirty = true;

            T_GAMEPLAY_SAVE gameplaySave;
            if (starterIndex < 0)
            {
                gameplaySave = rootSaveExtension.getGameplaySave(fileRootSaveDataCache);
            }
            else
            {
                gameplaySave = starterRootSaveDataAsGameplaySaveDataCache.get(starterIndex);
            }

            if (gameplaySave != null)
            {
                subGameplaySaveHandlers.ForEach(it =>it.applyGameplaySaveData(gameplaySave));
            }
            frontend.log(this.getClass().getSimpleName(), "starterIndex = " + starterIndex);
        }



        override protected T_ROOT_SAVE currentSituationToRootSaveData()
        {
            frontend.log(this.getClass().getSimpleName(), "currentSituationToRootSaveData by gameSaveDirty = " + gameSaveDirty);
            T_GAMEPLAY_SAVE gameplaySave;
            if (gameSaveDirty)
            {
                gameplaySave = rootSaveExtension.newGameplaySave();
                subGameplaySaveHandlers.ForEach(it => it.currentSituationToGameplaySaveData(gameplaySave));
            }
            else
            {
                if (_hasContinuedGameplaySave)
                {
                    gameplaySave = rootSaveExtension.getGameplaySave(fileRootSaveDataCache);
                }
                else
                {
                    gameplaySave = default(T_GAMEPLAY_SAVE);
                }
            }

            T_SYSTEM_SAVE systemSettingSave;
            systemSettingSave = rootSaveExtension.newSystemSave();
            subSystemSettingSaveHandlers.ForEach(it => it.currentSituationToSystemSetting(systemSettingSave));
            frontend.log(this.getClass().getSimpleName(), "systemSettingSave = " + systemSettingSave);
            return rootSaveExtension.newRootSave(
                    gameplaySave,
                    systemSettingSave
                    );
        }

        

        override public void registerSubHandler(Object objecz)
        {
            if (objecz is ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>) {
                subGameplaySaveHandlers.Add((ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>)objecz);
                frontend.log(this.getClass().getSimpleName(), objecz.getClass().getSimpleName() + " register as " + typeof(ISubGameplaySaveHandler<T_GAMEPLAY_SAVE>).Name);
            }
            if (objecz is ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>) {
                subSystemSettingSaveHandlers.Add((ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>) objecz);
                frontend.log(this.getClass().getSimpleName(), objecz.getClass().getSimpleName() + " register as " + typeof(ISubSystemSettingSaveHandler<T_SYSTEM_SAVE>).Name);
            }
        }
    }
}
