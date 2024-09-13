namespace Interface
{
    public interface INullExistable
    {
        bool IsNullExist();
    }

    public interface IEventable
    {
        void Start();
        void Update();
    }

    public interface IInitable
    {
        void Init();
    }
}