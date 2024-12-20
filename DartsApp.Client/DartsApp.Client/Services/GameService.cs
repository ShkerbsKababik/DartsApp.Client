using System.Net.Http;
using System;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DartsApp.Client.Services
{
    public interface IGameService
    {
        public Guid CreateGame(GameCreationInfo creationInfo);
        public void ApplyScore(ApplyScoreInfo scoreInfo);
        public Game GetGameInfo(Guid gameId);
    }
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;    
        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void ApplyScore(ApplyScoreInfo scoreInfo)
            => ApplyScoreAsync(scoreInfo);

        public async void ApplyScoreAsync(ApplyScoreInfo scoreInfo)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7030/GameService/ApplyScore", scoreInfo);
        }

        public Guid CreateGame(GameCreationInfo creationInfo)
            => CreateGameAsync(creationInfo).Result;

        public async Task<Guid> CreateGameAsync(GameCreationInfo creationInfo)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7030/GameService/CreateGame", creationInfo);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public Game GetGameInfo(Guid gameId)
            => GetGameInfoAsync(gameId).Result;

        public async Task<Game> GetGameInfoAsync(Guid gameId)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7030/GameService/GetGameInfoAsync", gameId);
           return await response.Content.ReadFromJsonAsync<Game>();      
        }
    }
    public class GameCreationInfo
    {
        public Guid OwnerId { get; set; }
        public List<Guid> Players { get; set; }
    }
    public class ApplyScoreInfo
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        public int FirstRun { get; set; }
        public int SecondRun { get; set; }
        public int ThirdRun { get; set; }
    }
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public Guid CurrentPlayer { get; set; }
        public GameStatus Status { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Guid[] Players { get; set; }
        public Guid[] Scores { get; set; }
    }
    public enum GameStatus
    {
        InProcess,
        Canceled,
        Ended
    }
}
