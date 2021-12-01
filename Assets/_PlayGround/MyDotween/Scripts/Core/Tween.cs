namespace MyDOTween
{
    public abstract class Tween : Sequentiable
    {
        public bool active { get; internal set; }
        internal bool isSequenced = false;
        internal abstract void Update(float deltaTime);
    }
}