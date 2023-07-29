using Directum.MessagerDAL.Configuration;
using Directum.MessagerDAL.Enum;
using Directum.MessagerDAL.Models;
using Directum.MessagerDAL.Repositories;

using System.Text.Json;

var config = new Config
{
    ConnectionString = "Server=192.168.142.130; Database=test; User ID=sa; Password=Password10; MultipleActiveResultSets=true; TrustServerCertificate=True"
};

var userRepository = new UserRepository(config);

Console.WriteLine("UserRepository");
Console.WriteLine();

Console.WriteLine("userRepository.GetByIdAsync(1) result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await userRepository.GetByIdAsync(1)));
Console.WriteLine();

Console.WriteLine("userRepository.GetByNameAsync(\"ilia\") result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await userRepository.GetByNameAsync("ilia")));
Console.WriteLine();

Console.WriteLine("userRepository.GetAllByNameAsync(\"ilia\") result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await userRepository.GetAllByNameAsync("ilia")));
Console.WriteLine();

var testUser = new User
{
    Name = "Test User",
    Password = "TestPassword",
    State = UserStateEnum.Offline
};

Console.WriteLine($"userRepository.AddAsync({JsonSerializer.Serialize(testUser)}) result:");
var insertedUserId = await userRepository.AddAsync(testUser);
Console.WriteLine(insertedUserId);
Console.WriteLine();

testUser.Id = insertedUserId;
testUser.Name = "Test User updated";
testUser.Password = "TestPassword updated";

Console.WriteLine($"userRepository.UpdateAsync({JsonSerializer.Serialize(testUser)})");
await userRepository.UpdateAsync(testUser);
Console.WriteLine();

Console.WriteLine();

var contactRepository = new ContactRepository(config);

Console.WriteLine("ContactRepository");
Console.WriteLine();

Console.WriteLine("contactRepository.GetByUserIdAndContactIdAsync(1, 2) result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await contactRepository.GetByUserIdAndContactIdAsync(1, 2)));
Console.WriteLine();


Console.WriteLine("contactRepository.GetAllByIdAsync(2) result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await contactRepository.GetAllByIdAsync(2)));
Console.WriteLine();

Console.WriteLine("contactRepository.GetByNameAsync(2, \"lev\") result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await contactRepository.GetByNameAsync(2, "lev")));
Console.WriteLine();

var testContact = new Contact
{
    UserId = 1,
    ContactId = 7,
};

try
{
    Console.WriteLine($"contactRepository.AddAsync({JsonSerializer.Serialize(testContact)})");
    await contactRepository.AddAsync(testContact);
    Console.WriteLine();
}
catch (Exception)
{
    Console.WriteLine();
    // ignored
}

testContact.LastUpdateTime = DateTime.Now;

Console.WriteLine($"contactRepository.UpdateAsync({JsonSerializer.Serialize(testContact)})");
await contactRepository.UpdateAsync(testContact);
Console.WriteLine();

Console.WriteLine($"contactRepository.DeleteAsync(1, 7)");
await contactRepository.DeleteAsync(1, 7);
Console.WriteLine();

Console.WriteLine();

var messageRepository = new MessageRepository(config);

Console.WriteLine("messageRepository");
Console.WriteLine();

Console.WriteLine("messageRepository.GetAllByIdAsync(2) result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await messageRepository.GetAllByIdAsync(2)));
Console.WriteLine();

Console.WriteLine("messageRepository.GetBySubstringAsync(\" 7\") result:");
Console.WriteLine(
    JsonSerializer.Serialize(
        await messageRepository.GetBySubstringAsync(2, " 7")));
Console.WriteLine();

var testMessage = new Message 
{
    UserId = 1,
    ContactId = 1,
    SendTime = DateTime.Now,
    Content = "Test!"
};

Console.WriteLine($"messageRepository.AddAsync({JsonSerializer.Serialize(testMessage)})");
await messageRepository.AddAsync(testMessage);
Console.WriteLine();

Console.ReadKey();