namespace GraphQL.NetCoreExample.Types
{
    using GraphQL.NetCoreExample.Models;
    using HotChocolate.Types;

    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field("message")
                .Type<StringType>()
                .Resolver(context => "Hello!")
                .Description("Greeting message");

            descriptor
                .Field("cat")
                .Type<CatType>()
                .Argument("name", a => a.Type<StringType>())
                .Description("Find cat by name")
                .Resolver(c => CatResolver.GetCatByNameStatic(c.Argument<string>("name")));

            descriptor
                .Field<CatResolver>(c => c.GetCatByName(default(string)))
                .Name("anotherCat")
                .Type<CatType>()
                .Argument("name", a => a.Type<StringType>())
                .Description("Find another cat by name");
        }
    }
}
