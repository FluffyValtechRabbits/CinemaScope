using System;

namespace UserService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAboutUsRepository AboutUsRepository { get; }
    }
}
