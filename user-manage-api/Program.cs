var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IUserService, UserService>();  // Register your user service
builder.Services.AddControllers();  // Register controller services

// Add Authentication (Cookie Authentication in this example)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "MyAppCookie";  // Cookie name
        options.LoginPath = "/Account/Login";  // Define where to redirect for login
        options.AccessDeniedPath = "/Account/AccessDenied";  // Define where to redirect if access is denied
    });

// Add Authorization (if needed)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

var app = builder.Build();

// Custom login middleware
app.Use(async (context, next) =>
{
    // Check if it's a login request
    if (context.Request.Path.StartsWithSegments("/login", StringComparison.OrdinalIgnoreCase) && context.Request.Method == "POST")
    {
        var username = context.Request.Form["username"];
        var password = context.Request.Form["password"];

        // Simplified authentication logic (you can replace this with database or other methods)
        if (username == "john.doe" && password == "strongpassword123")
        {
            // Create claims for the authenticated user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("Admin", "True") // Example claim
            };

            // Create identity and principal
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            // Issue authentication cookie
            await context.SignInAsync("CookieAuth", principal);

            // Redirect to a protected page or return a success message
            context.Response.Redirect("/");  // Or use Results.Ok() for API response
        }
        else
        {
            // Unauthorized if credentials are invalid
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid credentials");
        }
    }
    else
    {
        await next();
    }
});


// Add middleware
app.UseRouting();

// Authentication middleware
app.UseAuthentication();

app.UseAuthorization();

// Define routes
app.MapGet("/", () => "Hello!");

// Map controllers (this will include the UserController)
app.MapControllers();  // This is required to map your controller actions

// Run the application
app.Run();
