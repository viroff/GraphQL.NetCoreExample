namespace GraphQL.NetCoreExample.Types
{
    using HotChocolate.Types;

    public class QueryType: ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Field("message")
                .Type<StringType>()
                .Resolver(context => "Hello!");
        }
    }
}
