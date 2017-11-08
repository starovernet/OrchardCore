using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Apis;
using OrchardCore.Modules;
using OrchardCore.Queries.Apis.GraphQL.Mutations;
using OrchardCore.Queries.Apis.GraphQL.Mutations.Types;
using OrchardCore.Queries.Apis.GraphQL.Queries;
using OrchardCore.Queries.Sql;

namespace OrchardCore.Queries.Apis.GraphQL
{
    public class GraphQLStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphMutationType<CreateQueryMutation<SqlQuery>>();
            services.AddScoped<CreateQueryOutcomeType<SqlQuery>>();

            services.AddGraphQueryType<SqlQueryQuery>();
            services.AddScoped<ContentItemType2>();
        }
    }
}
