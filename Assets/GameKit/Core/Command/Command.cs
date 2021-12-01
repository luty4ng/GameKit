namespace GameKit
{
    public abstract class Command<Target>
    {
        protected readonly Target target;
        protected Command()
        {
            this.target = default(Target);
        }
        protected Command(Target target)
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
