using FluentValidation;

namespace ToDos.MinimalAPI
{
    public static class ValidatorExtension
    {
        public static RouteHandlerBuilder WithValidator<T>(this RouteHandlerBuilder builder)
            where T : class
        {
            builder.Add(endpointBuilder =>
            {
                var originalDelegate = endpointBuilder.RequestDelegate;
                endpointBuilder.RequestDelegate = async httpContent =>
                {
                    var validator = httpContent.RequestServices.GetRequiredService<IValidator<T>>();

                    httpContent.Request.EnableBuffering();
                    var body = await httpContent.Request.ReadFromJsonAsync<T>();

                    if(body == null)
                    {
                        httpContent.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpContent.Response.WriteAsync("Couldn't map body to request model");
                        return;
                    }

                    var validationResult = validator.Validate(body);
                    if(!validationResult.IsValid)
                    {
                        httpContent.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpContent.Response.WriteAsJsonAsync(validationResult.Errors);
                        return;
                    }
                   
                    httpContent.Request.Body.Position = 0;
                    await originalDelegate(httpContent);
                };
            });

            return builder;
        }
    }
}
