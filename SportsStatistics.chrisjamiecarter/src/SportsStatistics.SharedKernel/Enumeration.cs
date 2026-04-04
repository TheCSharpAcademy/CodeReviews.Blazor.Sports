using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SportsStatistics.SharedKernel;

public abstract class Enumeration<TEnum> : IComparable<Enumeration<TEnum>>, IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Lazy<Dictionary<int, TEnum>> EnumerationsDictionary = 
        new(() => GetAllEnumerationOptions().ToDictionary(item => item.Value));

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration{TEnum}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Enumeration()
    {
        Value = default;
        Name = string.Empty;
    }

    public static IReadOnlyCollection<TEnum> List => [.. EnumerationsDictionary.Value.Values];

    public int Value { get; private set; }

    public string Name { get; private set; }

    public static bool ContainsValue(int value) => EnumerationsDictionary.Value.ContainsKey(value);

    public static Result<TEnum> Resolve(int value) 
        => EnumerationsDictionary.Value.TryGetValue(value, out var enumeration) ? enumeration : EnumerationErrors.Unresolved;

    public static int MaxNameLength => EnumerationsDictionary.Value.Values.Max(enumeration => enumeration.Name.Length);

    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return !(a == b);
    }

    public static bool operator <(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return a is not null && b is not null && a < b;
    }

    public static bool operator <=(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return a is not null && b is not null && a <= b;
    }

    public static bool operator >(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return a is not null && b is not null && a > b;
    }

    public static bool operator >=(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        return a is not null && b is not null && a >= b;
    }

    public int CompareTo(Enumeration<TEnum>? other)
    {
        return other == null ? 1 : Value.CompareTo(other.Value);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        return other is not null 
            && other.GetType() == GetType() 
            && other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null 
            && obj.GetType() == GetType()
            && obj is Enumeration<TEnum> enumeration
            && enumeration.Value == Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString() => Name;

    public string GetDescription()
    {
        var field = GetType().GetField(Name, BindingFlags.Public | BindingFlags.Static);
        var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name is not null 
            ? displayAttribute.Name 
            : Name;
    }

    private static List<TEnum> GetAllEnumerationOptions()
    {
        var enumerationType = typeof(TEnum);

        var enumerationTypes = Assembly.GetAssembly(enumerationType)!
                                       .GetTypes()
                                       .Where(type => type.IsAssignableTo(enumerationType));

        var enumerations = new List<TEnum>();

        foreach (var type in enumerationTypes)
        {
            var enumerationTypeOptions = GetFieldsOfType<TEnum>(enumerationType);
            enumerations.AddRange(enumerationTypeOptions);
        }

        return enumerations;
    }

    private static List<TFieldType> GetFieldsOfType<TFieldType>(Type type) 
        => [.. type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                   .Where(fieldInfo => fieldInfo.FieldType.IsAssignableTo(type))
                   .Select(fieldInfo => (TFieldType)fieldInfo.GetValue(null)!)];
}
