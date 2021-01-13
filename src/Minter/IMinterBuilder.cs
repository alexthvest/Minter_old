using Minter.Core.Abstractions;

namespace Minter
{
    public interface IMinterBuilder
    {
        /// <summary>
        /// Specify the startup type to be used by the Minter host.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void UseStartup<T>() where T : class;

        /// <summary>
        /// Specify the server core type to be used by the Minter host.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void UseServerCore<T>() where T : class, IMinterCore;
    }
}