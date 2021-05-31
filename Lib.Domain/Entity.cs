namespace Lib.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; init; }
    }
}
