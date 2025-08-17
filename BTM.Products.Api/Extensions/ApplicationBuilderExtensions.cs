namespace BTM.Products.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            app.MapDefaultEndpoints(); // Fix: Ensure MapDefaultEndpoints is defined or imported

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
          //  app.MapControllers();

            return app;
        }
    }
}
