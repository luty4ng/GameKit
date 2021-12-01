namespace GameKit
{
    public static class EventConfig
    {
        public enum InputEvent
        {
            Horizontal, Vertical, Jump
        }

        public enum ProcedureEvent
        {
            Success, Fail, Restart, Reset
        }

        public enum EntityEvent
        {
            Initialize, Destroy
        }

        public enum SceneEvent
        {
            LoadSceneAsynBefore, UnloadSceneAsynBefore, LoadSceneAsynAfter, UnloadSceneAsynAfter
        }
    }
}

