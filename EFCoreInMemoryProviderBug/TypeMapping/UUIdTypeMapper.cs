using Dodo.Tools.Types;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreInMemoryProviderBug.TypeMapping
{
    public class UUIdTypeMapper: RelationalTypeMapping
    {
        private static readonly ValueConverter<UUId, byte[]> _converter
            = new ValueConverter<UUId, byte[]>(uuid => uuid.ToByteArray(),
                byteArray => new UUId(byteArray));

        protected UUIdTypeMapper(RelationalTypeMappingParameters parameters) : base(parameters)
        {
        }

        public UUIdTypeMapper() : base(new RelationalTypeMappingParameters(
            new CoreTypeMappingParameters(typeof(UUId), _converter), "binary(16)")
        )
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new UUIdTypeMapper(parameters);
        }

        public override string GenerateSqlLiteral(object value)
        {
            return $"0x{value}";
        }
    }
}