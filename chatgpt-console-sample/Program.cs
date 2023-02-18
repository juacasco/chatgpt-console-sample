
Console.WriteLine("Ask me a question or type 'exit' to quit:");

// Initialize an HTTP client to make requests to the OpenAI API
var httpClient = new HttpClient();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (input.ToLower() == "exit")
    {
        break;
    }

    var openAiService = new OpenAIService("<TokenHere>");

    var completion = await openAiService.GetCompletionAsync(input);

    Console.WriteLine(completion);
}
