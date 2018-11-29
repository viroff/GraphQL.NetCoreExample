# Самый простой пример

### Необходимые библиотеки

Требуется установить `HotChocolate.AspNetCore` пакет из `Nuget`

> `Install-Package HotChocolate.AspNetCore`

Устанавливает middleware для ASP.NET Core и core-библиотеку, в которой находятся абстракции для построения схемы, парсер GraphQL синтаксиса, runtime для валидации и выполнения входящих запросов.


### Минимально необходимый код

Прежде всего, Необходимо создать root-тип для всех операций чтения (**`queries`**)

```csharp
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
```

В каждой схеме обязательно должен присутствовать QueryType и быть единственным.

На верхнем уровне схемы могут также два других root-типа: MutationType и SubscriptionType. QueryType - обязательный, остальные опциональны.

> в этом примере в QueryType указано поле `message` с типом `string`, которое возращает **"Hello!"**


---

В Startup-файле необходимо указать, по какому адресу будет находиться GraphQL-endpoint. В методе конфигурации сервисов, нужно зарегистрировать схему и типы в ней:

```csharp
namespace GraphQL.NetCoreExample
{
    using GraphQL.NetCoreExample.Types;

    using HotChocolate;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(sp => Schema.Create(c =>
            {
                c.RegisterServiceProvider(sp);

                c.RegisterQueryType<QueryType>();
            }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL("/graphql");
        }
    }
}
```

### Первый запрос к GraphQL

Для тестирования запросов рекомендую использовать приложение GraphQL Playground: https://github.com/prisma/graphql-playground

Запустите проект в debug-режиме.

В GraphQL Playground укажите адрес: `localhost:<port>/graphql`. Чтобы убедиться, что все работает, нажмите на кнопку `Schema`. В появившейся панели под заголовком `Queries` должно быть поле `message`. Сверните панель.

Введите запрос в левой части:

```graphql
{
  message
}
```

и нажмите кнопку "Execute Query".

В правой части появится ответ от сервера:

```json
{
  "data": {
    "message": "Hello!"
  }
}
```
