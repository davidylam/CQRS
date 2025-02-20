using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class DeletePostcommand: BaseCommand
    {
        public string? Username { get; set; }
    }
}
