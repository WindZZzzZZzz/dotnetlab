public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}

public class UserService : IUserService
{
    private readonly List<User> _users;

    public UserService()
    {
        _users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com", Password = "password1"},
            new User { Id = 2, Name = "Bob", Email = "bob@example.com", Password = "password2"},
            new User { Id = 3, Name = "Charlie", Email = "charlie@example.com", Password = "password3"},
            new User { Id = 4, Name = "Diana", Email = "diana@example.com", Password = "password4"},
            new User { Id = 5, Name = "Eve", Email = "eve@example.com", Password = "password5"}
        };
    }



    public Task<User> GetUserByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task<User> CreateUserAsync(User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> UpdateUserAsync(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.UtcNow;
        }
        return Task.FromResult(existingUser);
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}