
// Create an instance of the configuration object
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

// Replace with your desired model ID
var modelId = "gpt-3.5-turbo";

// Create an instance of the OpenAIService class using dependency injection
var service = new OpenAIService(config, modelId);

// Start a while loop that continues until the user enters "exit"
while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    // If the user enters "exit", break out of the loop
    if (input == "exit")
        break;

    // Otherwise, create a list of messages comprising the conversation so far
    var messages = new[]
    {
        new { role = "system", content = "You are a helpful assistant." },
        new { role = "user", content = input }
    };

    // Call the GetCompletionAsync method to get a chat completion
    var response = await service.GetCompletionAsync(JsonSerializer.Serialize(messages));

    // Print the response content to the console
    Console.WriteLine(response);
}
