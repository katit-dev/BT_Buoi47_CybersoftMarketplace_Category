using Infrastructure.Repositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Storage;
public interface IUnitOfWork : IAsyncDisposable
{
    ICartRepository CartRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IConversationRepository ConversationRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ICustomer1Repository Customer1Repository { get; }
    IMessageRepository MessageRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductImageRepository ProductImageRepository { get; }
    IProductVariantRepository ProductVariantRepository { get; }
    IRatingRepository RatingRepository { get; }
    IRoleRepository RoleRepository { get; }
    IShopRepository ShopRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}


public class UnitOfWork : IUnitOfWork
{
    private readonly CybersoftMarketPlaceContext _context;
    private IDbContextTransaction _transaction;

    public ICartRepository CartRepository { get; private set; }
    public ICartItemRepository CartItemRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IConversationRepository ConversationRepository { get; private set; }
    public ICustomerRepository CustomerRepository { get; private set; }
    public ICustomer1Repository Customer1Repository { get; private set; }
    public IMessageRepository MessageRepository { get; private set; }
    public IOrderRepository OrderRepository { get; private set; }
    public IOrderItemRepository OrderItemRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }
    public IProductImageRepository ProductImageRepository { get; private set; }
    public IProductVariantRepository ProductVariantRepository { get; private set; }
    public IRatingRepository RatingRepository { get; private set; }
    public IRoleRepository RoleRepository { get; private set; }
    public IShopRepository ShopRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }
    public IUserRoleRepository UserRoleRepository { get; private set; }


    public UnitOfWork(CybersoftMarketPlaceContext context)
    {
        _context = context;
        CartRepository = new CartRepository(_context);
        CartItemRepository = new CartItemRepository(_context);
        CategoryRepository = new CategoryRepository(_context);
        ConversationRepository = new ConversationRepository(_context);
        CustomerRepository = new CustomerRepository(_context);
        Customer1Repository = new Customer1Repository(_context);
        MessageRepository = new MessageRepository(_context);
        OrderRepository = new OrderRepository(_context);
        OrderItemRepository = new OrderItemRepository(_context);
        ProductRepository = new ProductRepository(_context);
        ProductImageRepository = new ProductImageRepository(_context);
        ProductVariantRepository = new ProductVariantRepository(_context);
        RatingRepository = new RatingRepository(_context);
        RoleRepository = new RoleRepository(_context);
        ShopRepository = new ShopRepository(_context);
        UserRepository = new UserRepository(_context);
        UserRoleRepository = new UserRoleRepository(_context);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
        await _context.DisposeAsync();
    }
}