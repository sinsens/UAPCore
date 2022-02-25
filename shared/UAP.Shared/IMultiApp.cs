using System;

namespace UAP.Shared
{
    /// <summary>
    /// IMultiApp
    /// </summary>
    public interface IMultiApp
    {
        /// <summary>
        /// AppId
        /// </summary>
        public Guid? AppId { get; }
    }
}