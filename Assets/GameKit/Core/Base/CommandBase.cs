namespace GameKit
{
    public abstract class CommandBase<T>
    {
        protected readonly T target;
        protected CommandBase()
        {
            this.target = default(T);
        }
        protected CommandBase(T target)
        {
            this.target = target;
        }
        public abstract void Do();
        public abstract void Undo();
        public virtual void OnEnterDo() { }
        public virtual void OnExitDo() { }
        public virtual void OnEnterUndo() { }
        public virtual void OnExitUndo() { }
    }
}
