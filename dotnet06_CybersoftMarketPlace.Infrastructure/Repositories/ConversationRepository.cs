using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IConversationRepository : IRepositoryBase<Conversation>
{
}

public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
{
    public ConversationRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
