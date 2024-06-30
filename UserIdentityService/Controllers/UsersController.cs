

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly GraphServiceClient _graphClient;
    private readonly ICacheService _cacheService;

    public UsersController(GraphServiceClient graphClient, ICacheService cacheService)
    {
        _graphClient = graphClient;
        _cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var cacheKey = $"User_{id}";
        var cachedUser = await _cacheService.GetCacheValueAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedUser))
        {
            return Ok(cachedUser);
        }

        var user = await _graphClient.Users[id].GetAsync();
        await _cacheService.SetCacheValueAsync(cacheKey, user.DisplayName);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _graphClient.Users.GetAsync();
        var userList = new List<UserModels>();
        foreach (var user in users?.Value!)
        {
            userList.Add(new UserModels
            {
                Id = user.Id!,
                DisplayName = user.DisplayName!,
                Email = user.Mail!
            });
        }
        return Ok(userList);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserModels user)
    {
        var newUser = new User
        {
            DisplayName = user.DisplayName,
            Mail = user.Email,
            UserPrincipalName = user.Email, // Ensure this is a valid UPN
            AccountEnabled = true,
            PasswordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = true,
                Password = "SomeSecurePassword!" // Replace with your secure password generation logic
            }
        };
        var createdUser = await _graphClient.Users.PostAsync(newUser);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser?.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserModels user)
    {
        var updateUser = new User
        {
            DisplayName = user.DisplayName,
            Mail = user.Email
        };
        await _graphClient.Users[id].PatchAsync(updateUser);
        await _cacheService.RemoveCacheValueAsync($"User_{id}"); // Invalidate cache
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _graphClient.Users[id].DeleteAsync();
        await _cacheService.RemoveCacheValueAsync($"User_{id}"); // Invalidate cache
        return NoContent();
    }
}
