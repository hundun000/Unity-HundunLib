using hundun.unitygame.adapters;
using System;

namespace hundun.unitygame.gamelib
{
    public abstract class AbstractSaveHandler<T_SAVE>
    {
        private ISaveTool<T_SAVE> saveTool;
        protected IFrontend frontEnd;
        protected abstract void applySystemSetting(T_SAVE saveData);
        protected abstract void applyGameSaveData(T_SAVE saveData);
        protected abstract T_SAVE currentSituationToSaveData();
        protected abstract T_SAVE genereateStarterRootSaveData();
        public abstract void registerSubHandler(Object objecz);

        public AbstractSaveHandler(IFrontend frontEnd, ISaveTool<T_SAVE> saveTool)
        {
            this.saveTool = saveTool;
            this.frontEnd = frontEnd;
        }

        public void lazyInitOnGameCreate()
        {
            this.saveTool.lazyInitOnGameCreate();
        }

        public void systemSettingLoadOrNew()
        {

            T_SAVE saveData;
            if (saveTool.hasSave())
            {
                saveData = saveTool.readRootSaveData();
            }
            else
            {
                saveData = this.genereateStarterRootSaveData();
            }

            this.applySystemSetting(saveData);
            frontEnd.log(this.getClass().getSimpleName(), "systemSettingLoad call");
        }

        public void gameLoadOrNew(Boolean load)
        {

            T_SAVE saveData;
            if (load && saveTool.hasSave())
            {
                saveData = saveTool.readRootSaveData();
            }
            else
            {
                saveData = this.genereateStarterRootSaveData();
            }

            this.applyGameSaveData(saveData);
            frontEnd.log(this.getClass().getSimpleName(), load ? "load game done" : "new game done");
        }

        public void gameSaveCurrent()
        {
            frontEnd.log(this.getClass().getSimpleName(), "saveCurrent called");
            saveTool.writeRootSaveData(this.currentSituationToSaveData());
        }

        public Boolean gameHasSave()
        {
            return saveTool.hasSave();
        }


    }
}
