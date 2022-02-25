using System;

namespace UAP.Shared
{
    public interface ICurrentApp
    {
        Guid? AppId { get; }
    }
}
