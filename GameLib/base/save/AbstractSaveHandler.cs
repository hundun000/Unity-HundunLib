using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;

namespace hundun.unitygame.gamelib
{
    public abstract class AbstractSaveHandler<T_SAVE>
    {
        protected ISaveTool<T_SAVE> saveTool;
        protected IFrontend frontend;
        public abstract void systemSettingLoadOrStarter();
        public abstract void gameplayLoadOrStarter(int starterIndex);
        protected abstract T_SAVE currentSituationToRootSaveData();
        protected abstract List<T_SAVE> genereateStarterRootSaveData();
        public abstract Boolean hasContinuedGameplaySave();
        public abstract void registerSubHandler(Object objecz);

        public AbstractSaveHandler(IFrontend frontend, ISaveTool<T_SAVE> saveTool)
        {
            this.saveTool = saveTool;
            this.frontend = frontend;
        }

        virtual public void lazyInitOnGameCreate()
        {
            this.saveTool.lazyInitOnGameCreate();
        }


        public void gameSaveCurrent()
        {
            frontend.log(this.getClass().getSimpleName(), "saveCurrent called");
            saveTool.writeRootSaveData(this.currentSituationToRootSaveData());
        }

    }
}
