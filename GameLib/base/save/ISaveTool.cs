namespace hundun.unitygame.gamelib
{
    public interface ISaveTool<T_SAVE>
    {
        void lazyInitOnGameCreate();
        bool hasRootSave();
        void writeRootSaveData(T_SAVE saveData);
        T_SAVE readRootSaveData();
    }
}
