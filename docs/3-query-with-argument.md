# Запросы с параметрами

В реальном приложении обязательно появится необходимость делать запросы с параметрами, например, получить пользователя по его ID.

Пример того, как зарегистрировать в схеме `query` с параметрами, приведён ниже:

```csharp
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
```

Здесь показаны два варианта, как можно передать входящие агрументы в резолвер-функцию. 

В первом случае, аргумент извлекается из контекста резолвер-функции: 
>`c.Argument<string>("name")`

Во втором случае используется типизированная версия метода:
> `Field<TResolver>(Expression<Func<TResolver, object>>)`

В runtime при исполнении запроса будет вызван метод `TResolver`-класса с входными аргументами запроса.

#### Примечание
> Типизированная версия метода `Field<T>(...)` доступна только когда класс унаследован от `ObjectType<TObject>`.
---


### Пример

запрос:

```graphql
{
  message
  cat(name: "Blackcat") {
    name
    color
  }
  anotherCat(name: "Zeus") {
  	name
    color
  }
}
```

ответ:

```json
{
  "data": {
    "message": "Hello!",
    "cat": {
      "name": "Blackcat",
      "color": "black"
    },
    "anotherCat": {
      "name": "Zeus",
      "color": "white"
    }
  }
}
```
