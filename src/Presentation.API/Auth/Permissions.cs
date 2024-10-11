using System.Collections.Immutable;

namespace Presentation.API.Auth
{
    public class Permissions
    {
        public static ImmutableArray<PermissionInfo> PermissionsList = new[]
        {
            //new PermissionInfo(nameof(FindDessertsRequest), requiresRestaurant: true),
            new PermissionInfo(""),
        }.ToImmutableArray();
    }

    public class PermissionInfo
    {
        public PermissionInfo(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
