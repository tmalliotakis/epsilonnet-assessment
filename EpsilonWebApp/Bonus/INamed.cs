namespace EpsilonWebApp.Bonus
{
    /// <summary>
    /// Common abstraction for any type that exposes a name.
    /// Lets a single method accept an Employee, a Manager, or any future named type.
    /// </summary>
    public interface INamed
    {
        string Name { get; }
    }
}
