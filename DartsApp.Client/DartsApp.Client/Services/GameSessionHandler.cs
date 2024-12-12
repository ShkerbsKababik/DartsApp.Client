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
            };

            game.Score = new Dictionary<Guid, int>();
            foreach (var playerId in palyersIds)
            {
                game.Score.Add(playerId, 301);
            }

            // randomize players order
            var random = new Random();
            game.PlayersIds = game.PlayersIds.OrderBy(x => random.Next()).ToList();
            game.CurrentPlayerId = game.PlayersIds[0];

            Games.Add(game.Id, game);
            return game.Id;
        }

        public Game UpdateScore(Guid gameId, Guid playerId, int score)
        { 
            if (!Games.ContainsKey(gameId)) 
                return null;

            var game = Games[gameId];

            // apply score
            if (score <= game.Score[playerId])
            {
                game.Score[playerId] -= score;
            }

            // set new current player
            int lastPlayerIndex = -1;
            for (int i = 0; i < game.PlayersIds.Count; i++)
            {
                if (game.PlayersIds[i] == playerId)
                { 
                    lastPlayerIndex = i;
                }
            }
            if (lastPlayerIndex == game.PlayersIds.Count - 1)
            {
                game.CurrentPlayerId = game.PlayersIds[0];
            }
            else
            {
                game.CurrentPlayerId = game.PlayersIds[lastPlayerIndex + 1];
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
