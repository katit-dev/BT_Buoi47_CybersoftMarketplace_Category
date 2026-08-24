using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IMessageRepository : IRepositoryBase<Message>
{
}

public class MessageRepository : RepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
