namespace DartsApp.Client.Services
{
    public interface IGameSessionHandler
    {
        public Guid CreateGame(List<Guid> palyersIds);
        public Game ConnectGame(Guid gameId);
        public Game UpdateScore(Guid gameId, Guid playerId, int score);
    }
    public class GameSessionHandler : IGameSessionHandler
    {
        private Dictionary<Guid, Game> Games = new Dictionary<Guid, Game>();

        public Game ConnectGame(Guid gameId)
        {
            return Games[gameId];
        }

        public Guid CreateGame(List<Guid> palyersIds)
        {
            var game = new Game()
            {
                Id = Guid.NewGuid(),
                PlayersIds = palyersIds,
                CurrentPlayerId = palyersIds[0],
            };

            game.Score = new Dictionary<Guid, int>();
            foreach (var playerId in palyersIds)
            {
                game.Score.Add(playerId, 301);
            }

            Games.Add(game.Id, game);
            return game.Id;
        }

        public Game UpdateScore(Guid gameId, Guid playerId, int score)
        { 
            if (!Games.ContainsKey(gameId)) 
                return null;

            var game = Games[gameId];

            game.Score[playerId] -= score;

            Guid previousPlayerGuid = playerId;
            for (int i = 0; i < game.PlayersIds.Count; i++)
            {
                
            }

            return Games[gameId];
        }
    }
    public class Game
    { 
        public Guid Id { get; set; }
        public List<Guid> PlayersIds { get; set; }
        public Dictionary<Guid, int> Score {  get; set; }
        public Guid CurrentPlayerId { get; set; }
    }
}
