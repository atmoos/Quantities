using System.Reflection;
using Quantities.Prefixes;
using Quantities.Units;
using static Quantities.Serialization.Reflection;

namespace Quantities.Serialization;

public sealed class UnitRepository
{
    private readonly Repository prefixes, siUnits, metricUnits, imperialUnits, nonStandardUnits;

    private UnitRepository(Repository prefixes, Repository siUnits, Repository metricUnits, Repository imperialUnits, Repository nonStandardUnits)
    {
        this.prefixes = prefixes;
        this.siUnits = siUnits;
        this.metricUnits = metricUnits;
        this.imperialUnits = imperialUnits;
        this.nonStandardUnits = nonStandardUnits;
    }
    internal Type Si(String name) => this.siUnits[name];
    internal Type Metric(String name) => this.metricUnits[name];
    internal Type Imperial(String name) => this.imperialUnits[name];
    internal Type NonStandard(String name) => this.nonStandardUnits[name];
    internal Type Prefix(String name) => this.prefixes[name];
    public static UnitRepository Create(IEnumerable<Assembly> assemblies)
    {
        var prefixes = new RepoBuilder("prefix", typeof(IPrefix));
        var siUnits = new RepoBuilder("si unit", typeof(ISiUnit));
        var metricUnits = new RepoBuilder("metric unit", typeof(IMetricUnit));
        var imperialUnits = new RepoBuilder("imperial unit", typeof(IImperialUnit));
        var nonStandardUnits = new RepoBuilder("non standard unit", typeof(INonStandardUnit));
        foreach (var type in assemblies.Append(typeof(UnitRepository).Assembly).SelectMany(a => a.GetExportedTypes().Where(t => t.IsValueType))) {
            prefixes.TryAdd(type);
            siUnits.TryAdd(type);
            metricUnits.TryAdd(type);
            imperialUnits.TryAdd(type);
            nonStandardUnits.TryAdd(type);
        }
        return new UnitRepository(prefixes.Build(), siUnits.Build(), metricUnits.Build(), imperialUnits.Build(), nonStandardUnits.Build());
    }

    private sealed class Repository
    {
        private readonly String kind;
        private readonly Dictionary<String, Type> repo;
        public Type this[String name] => Retrieve(name);
        public Repository(String kind, Dictionary<String, Type> repo) => (this.kind, this.repo) = (kind, repo);
        private Type Retrieve(String name)
        {
            if (this.repo.TryGetValue(name, out var type)) {
                return type;
            }
            var message = $"Could not find the {this.kind} '{name}'. Has the assembly that defines '{name}' been registered with the deserializer?";
            throw new ArgumentException(message);
        }
    }

    private sealed class RepoBuilder
    {
        private readonly Dictionary<String, Type> repo = new();
        private readonly String kind;
        private readonly Type interfaceType;
        public RepoBuilder(String kind, Type interfaceType) => (this.kind, this.interfaceType) = (kind, interfaceType);
        public void TryAdd(Type type)
        {
            if (type.IsAssignableTo(this.interfaceType)) {
                this.repo[GetRepresentation(type)] = type;
            }
        }
        public Repository Build() => new(this.kind, this.repo);
    }
}

