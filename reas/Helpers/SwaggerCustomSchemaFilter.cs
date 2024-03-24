
using BusinessObject.Model;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using BusinessObject.Model;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace reas.Helpers
{
    public class SwaggerCustomSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(LoginRequestModel))
            {
                schema.Example = new OpenApiObject
                {
                    [nameof(LoginRequestModel.UserName)] = new OpenApiString("nienket"),
                    [nameof(LoginRequestModel.Password)] = new OpenApiString("1234")
                };
            }

            if (context.Type == typeof(UserSignUpRequestModel))
            {
                var person = new Bogus.Person(locale: "vi");

                schema.Example = new OpenApiObject
                {

                    [nameof(UserSignUpRequestModel.UserName)] = new OpenApiString(person.UserName.ToLower()),
                    [nameof(UserSignUpRequestModel.Password)] = new OpenApiString("1234"),
                    [nameof(UserSignUpRequestModel.Email)] = new OpenApiString(person.Email.ToLower()),
                    [nameof(UserSignUpRequestModel.FullName)] = new OpenApiString(person.FullName),
                    [nameof(UserSignUpRequestModel.Address)] = new OpenApiString(person.Address.City),
                    [nameof(UserSignUpRequestModel.Phone)] = new OpenApiString(person.Phone),
                    [nameof(UserSignUpRequestModel.Avatar)] = new OpenApiString(person.Avatar)
                };
            }
        }
    }
}
