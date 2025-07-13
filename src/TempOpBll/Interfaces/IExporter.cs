using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempOpBll.Interfaces
{
    public interface IExporter<T>
    {
        /// <summary>
        /// Exports the specified input to a string representation.
        /// </summary>
        /// <param name="input">The input object to be exported. Cannot be null.</param>
        /// <returns>A string representation of the input object.</returns>
        string Export(T input);
    }
}
