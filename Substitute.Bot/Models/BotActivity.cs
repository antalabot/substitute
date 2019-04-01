using Discord;

namespace Substitute.Bot.Models
{
    public class BotActivity : IActivity
    {
        public string Name { get; set; }

        public ActivityType Type { get; set; }
    }
}
