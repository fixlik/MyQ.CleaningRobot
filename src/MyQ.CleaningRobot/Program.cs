using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyQ.CleaningRobot.Business;
using MyQ.CleaningRobot.Converters;
using MyQ.CleaningRobot.Entities;
using MyQ.CleaningRobot.Entities.DTOs;
using MyQ.CleaningRobot.Extensions;
using MyQ.CleaningRobot.Validators;
using System.Text.Json;

namespace MyQ.CleaningRobot;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = CreateServices();

        if (args.Length < 2)
        {
            Console.WriteLine("Please provide input and output file paths as arguments.");
            return;
        }

        var inputFilePath = args[0];
        var outputFilePath = args[1];

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            var inputFileDto = ReadInputFileAsDto(inputFilePath);

            var commandSequenceValid = serviceProvider.GetRequiredService<ICommandSequenceValidator>().Validate(inputFileDto.Commands, inputFileDto.Battery);
            if (!commandSequenceValid)
            {
                Console.Write("Not enough battery for all the commands in the input file. Do you want to continue? (y/n): ");

                var notEnoughBatteryUserInput = Console.ReadLine();
                var notEnoughBatteryContinue = IsInputContinue(notEnoughBatteryUserInput);

                if (!notEnoughBatteryContinue)
                {
                    return;
                }
            }

            var validStartingPosition = serviceProvider.GetRequiredService<IStartingPointValidator>().Validate(inputFileDto.Map, inputFileDto.Start.X, inputFileDto.Start.Y);
            if (!validStartingPosition)
            {
                logger.LogError("Starting position is outside of the map.");
                return;
            }

            var cleaningRobotService = serviceProvider.GetRequiredService<ICleaningRobot>();

            var outputFile = cleaningRobotService.Clean(inputFileDto);

            WriteOutputFile(outputFilePath, outputFile);
        }
        catch (Exception ex)
        {
            logger.LogCritical($"Unexpected exception occured. Exception: {ex}");
        }
    }

    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            }))
            .AddScoped<ICommandSequenceValidator, CommandSequenceValidator>()
            .AddScoped<IStartingPointValidator, StartingPointValidator>()
            .AddScoped<ICardinalDirectionRotator, CardinalDirectionRotator>()
            .AddScoped<ICoordinateProvider, CoordinateProvider>()
            .AddScoped<IStrategyProvider, StrategyProvider>()
            .AddScoped<ICleaningRobot, Business.CleaningRobot>()
            .BuildServiceProvider();

        return serviceProvider;
    }

    private static InputFileDto ReadInputFileAsDto(string inputFilePath)
    {
        var jsonInputSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        };

        var fileExists = File.Exists(inputFilePath);
        if (!fileExists)
        {
            throw new ArgumentException($"Input file {inputFilePath} doesn't exist.");
        }

        var jsonString = File.ReadAllText(inputFilePath);
        var inputFile = JsonSerializer.Deserialize<InputFile>(jsonString, jsonInputSerializerOptions);
        var inputFileDto = inputFile.ToDto();

        return inputFileDto;
    }

    private static void WriteOutputFile(string outputFilePath, OutputFile outputFile)
    {
        var jsonOutputSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = {
                new IEnumerablePositionBaseSingleLineConverter(),
                new InputOutputPositionSingleLineConverter()
            }
        };

        var fileExists = File.Exists(outputFilePath);
        if (fileExists)
        {
            Console.Write($"Output file {outputFilePath} exist. Do you want to overwrite it? (y/n): ");

            var fileOverwriteUserInput = Console.ReadLine();
            var fileOverwriteContinue = IsInputContinue(fileOverwriteUserInput);

            if (!fileOverwriteContinue)
            {
                return;
            }
        }

        var outputFileJson = JsonSerializer.Serialize(outputFile, jsonOutputSerializerOptions);

        File.WriteAllText(outputFilePath, outputFileJson);
    }

    private static bool IsInputContinue(string input)
    {
        return input.ToUpperInvariant() switch
        {
            "Y" or "YES" => true,
            _ => false
        };
    }
}
