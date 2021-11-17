using Dodo.Tools.Types;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreInMemoryProviderBug.TypeMapping
{
    public class UUIdTypeMapperPlugin: ITypeMappingSourcePlugin, IRelationalTypeMappingSourcePlugin
    {
        public CoreTypeMapping FindMapping(in TypeMappingInfo mappingInfo)
        {
            return mappingInfo.ClrType == typeof(UUId) ? new UUIdTypeMapper() : null;
        }

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            return mappingInfo.ClrType == typeof(UUId) ? new UUIdTypeMapper() : null;
        }
    }
}