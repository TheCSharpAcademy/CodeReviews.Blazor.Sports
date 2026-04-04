namespace SportsStatistics.SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Identifier value is required.", nameof(id));
        }

        if (id.Version != 7)
        {
            throw new ArgumentException("Identifier values must be version 7 GUIDs.", nameof(id));
        }

        Id = id;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    protected Entity() { }

    public Guid Id { get; private set; }

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
