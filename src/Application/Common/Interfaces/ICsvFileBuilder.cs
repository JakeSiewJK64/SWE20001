using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildCsvFile<T>(IEnumerable<T> records);
    }
}
