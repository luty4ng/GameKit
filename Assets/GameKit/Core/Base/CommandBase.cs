namespace GameKit
{
    public abstract class CommandBase<T> : IReference, ICommand
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
        public abstract void Excute();
        public virtual void Revoke() { }
        public virtual void OnEnterExcute() { }
        public virtual void OnExitExcute() { }
        public virtual void OnEnterRevoke() { }
        public virtual void OnExitRevoke() { }
        public virtual void Clear() { }
    }

    public abstract class CommandBase : IReference, ICommand
    {
        protected CommandBase()
        {

        }
        public abstract void Excute();
        public virtual void Revoke() { }
        public virtual void OnEnterExcute() { }
        public virtual void OnExitExcute() { }
        public virtual void OnEnterRevoke() { }
        public virtual void OnExitRevoke() { }
        public virtual void Clear() { }
    }
}
