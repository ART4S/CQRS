using System;
using System.Collections.Generic;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal interface IEntityMap
    {
        Type Type { get; }
        string Table { get; }
        PropertyMap Identity { get; }
        IEnumerable<PropertyMap> Mappings { get; }
    }
}
