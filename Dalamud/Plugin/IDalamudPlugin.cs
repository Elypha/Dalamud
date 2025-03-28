using System;

namespace Dalamud.Plugin
{
    /// <summary>
    /// This interface represents a basic Dalamud plugin. All plugins have to implement this interface.
    /// </summary>
    public interface IDalamudPlugin : IDisposable
    {
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        string Name { get; }
    }
}
