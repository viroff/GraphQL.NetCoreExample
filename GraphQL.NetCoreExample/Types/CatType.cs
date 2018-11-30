namespace GraphQL.NetCoreExample.Types
{
    using GraphQL.NetCoreExample.Models;
    using HotChocolate.Types;

    public class CatType : ObjectType<Cat>
    {
        protected override void Configure(IObjectTypeDescriptor<Cat> descriptor)
        {
        }
    }
}