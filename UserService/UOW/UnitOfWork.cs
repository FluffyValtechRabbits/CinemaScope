using UserService.Interfaces;
using UserService.Contexts;
using UserService.Repositories;

namespace UserService.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IdentityContext _context;

        private IAboutUsRepository _aboutUsRepository;

        public UnitOfWork()
        {
            _context = new IdentityContext();
        }

        public IAboutUsRepository AboutUsRepository =>
            _aboutUsRepository ?? (_aboutUsRepository = new AboutUsRepository(_context));

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
